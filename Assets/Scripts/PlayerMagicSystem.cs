using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GRFON;
using System.IO;

public class PlayerMagicSystem : MonoBehaviour
{

    //Header will put text into the editor for the user to read
    [Header("Spellcasting Details")]
    [SerializeField] private Spell spellToCast; //what spell are we trying to cast, make this an array of spells later
    [SerializeField] private float maxMana = 100f; //maximum amount of mana the player has  to cast spell
    [SerializeField] private float currentMana; //how much mana does the player currently have out of the maximum
    [SerializeField] private float manaRechargeRate = 5f; //how much mana does the player regain when not casting a spell
    [SerializeField] private float timeBetweenRecharge = 0.09f; //how often the player can cast a spell, spell details can change this value
    [SerializeField] private float timeBetweenCasts = 0.25f; //how often the player can cast a spell, spell details can change this value
    //Should probably make a copy of timeBetweenCasts to restore it back to normal after a spell with a higher time cost has finished countingdown

    private float currentCastTimer; //how much of the cooldown between casting spells has been completed?
    private bool atMaxMana;
    [Header("Transform to cast magic from")]
    public Transform castPoint; //where the spell will originate from on the player

    private bool castingMagic = false; //are we already casting magic?

    //public List<Spell> spellBook; //array easier?
    [Header("List of spells that can be casted")]
    public Spell[] spellBook;
    [HideInInspector]
    private int currentSpellBookIndex = 0;

    //Text object that will hold the name of the currently equiped spell
    [Header("UI Elements")]
    public TMP_Text spellNameText;
    public TMP_Text manaCounterText;

    [HideInInspector]
    public GameObject lastHitGameObject = null;

    [HideInInspector] public enum languages { ENG, FRE, JAP, GER}
    [Header("Language to translate UI elements to")]
    public languages spellLanguage;
    [Header("Fonts to use for languages")]
    public TMP_FontAsset englishFont;
    public TMP_FontAsset japaneseFont;

    private void Awake()
    {

    }

    private void Start() //delete if dont need
    {
        currentMana = maxMana; //always give our player the maximum amount of mana to start rather than wait for it to recharge from 0
        //IF YOU WISH TO MAKE THE PLAYER START FROM 0 OR OTHER VALUE AND RECHARGE FROM THERE: Replace maxMana with a value or variable as needed

        //set the spell the player can cast to the current index within their spellbook, 0 by default but feel free to change in editor to reflect player preferences
        spellToCast = spellBook[currentSpellBookIndex];

        //Set the name of the currently equipped spell from the scriptable object
        spellNameText.text = translateSpellName(spellToCast.spellToCast.spellName);

        //Display the players current mana value
        manaCounterText.text = "Mana: " + currentMana;
    }

    private void Update()
    {
        if (!castingMagic && Input.GetKeyDown( KeyCode.Q ) ) //if we arent casting magic already or in cooldown and Q is pressed //REBIND AS NESSICARY FOR YOUR USE
        {
            //Update our boolean and reset our casting timer before we cast our spell
            castingMagic = true;
            currentCastTimer = 0; 
            if(currentMana - spellToCast.spellToCast.ManaCost >=0) //check if the player has enough mana to cast the spell
            {
                atMaxMana = false;
                castSpell(); //cast our equipped spell
            }
            
        }

        if (castingMagic) //if we are casting magic or in cooldown update our casting timer value until the cooldown has completed
        {
            currentCastTimer += Time.deltaTime;
            if(currentCastTimer > timeBetweenCasts)
            {
                castingMagic = false;
            }
        }

        if (castingMagic == false && atMaxMana == false) // if we arent casting a spell then we can start regenning mana
        {
            manaRecharge();
        }

        changeSpellText(); //Used for text objects, if you want a purely icon based display comment this out

        manaCounterText.text = "Mana: " + (int)currentMana; //update our mana value on the UI element


        

    }

    public void changeSpellText()
    {
        if (Input.GetKeyDown(KeyCode.E)) //change what spell to cast 
        {
            currentSpellBookIndex++;
            if (currentSpellBookIndex <= spellBook.Length - 1)
            {
                spellToCast = spellBook[currentSpellBookIndex];
                //spellToCast.caster = this.gameObject;
                spellNameText.text = translateSpellName(spellToCast.spellToCast.spellName); //update the text object to use the spells name
            }

            if (currentSpellBookIndex >= spellBook.Length)
            {
                currentSpellBookIndex = 0;
                spellToCast = spellBook[currentSpellBookIndex];
                //spellToCast.caster = this.gameObject;
                spellNameText.text = translateSpellName(spellToCast.spellToCast.spellName); //update the text object to use the spells name
            }
            //Debug.Log(spellToCast.spellToCast.spellName);

        }
    }    
/*    public void changeSpellText()
    {
        if (Input.GetKeyDown(KeyCode.E)) //change what spell to cast 
        {
            currentSpellBookIndex++;
            if (currentSpellBookIndex <= spellBook.Length - 1)
            {
                spellToCast = spellBook[currentSpellBookIndex];
                spellNameText.text = spellToCast.spellToCast.spellName; //update the text object to use the spells name
            }

            if (currentSpellBookIndex >= spellBook.Length)
            {
                currentSpellBookIndex = 0;
                spellToCast = spellBook[currentSpellBookIndex];
                spellNameText.text = spellToCast.spellToCast.spellName; //update the text object to use the spells name
            }

        }
    }*/

    public void castSpell()
    {
        spellToCast.caster = this.gameObject; //make us the owner of the spell
        Instantiate(spellToCast, castPoint.position, castPoint.rotation);  //Create the spell object  at the players casting position
        //deduct the amount of mana required to cast the spell from the player
        currentMana -= spellToCast.spellToCast.ManaCost;
        if (currentMana < 0) currentMana = 0; //reset mana to 0 if we somehow go to 0, should be prevented by if statement though so i may remove this

        atMaxMana = false;
    }

    //not applying the recharing mana, commented out for now
/*    IEnumerator manaRechargeRoutine()
    {
        yield return new WaitForSeconds(1f);
        currentMana += manaRechargeRate;
        if (currentMana > maxMana) currentMana = maxMana;
        Debug.Log("Current Mana: " +  currentMana);
    }
*/

    public void manaRecharge()
    {
        currentMana += manaRechargeRate * Time.deltaTime;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
            atMaxMana = true;
        }
    }

    public string translateSpellName(string nameIn)
    {
        if(spellLanguage.ToString() == "ENG")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataEnglish.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                nameIn = data.GetCollection("SpellName").GetString(nameIn, "notFound"); 

            }
            spellNameText.GetComponentInChildren<TextMeshProUGUI>().font = englishFont;

        }
        else if(spellLanguage.ToString() == "FRE")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataFrench.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                nameIn = data.GetCollection("SpellName").GetString(nameIn, "notFound"); 

            }
            spellNameText.GetComponentInChildren<TextMeshProUGUI>().font = englishFont;
        }
        else if(spellLanguage.ToString() == "JAP")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataJAP.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                nameIn = data.GetCollection("SpellName").GetString(nameIn, "notFound"); 

            }
            spellNameText.GetComponentInChildren<TextMeshProUGUI>().font = japaneseFont;

        }
        else if (spellLanguage.ToString() == "GER")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataGER.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                nameIn = data.GetCollection("SpellName").GetString(nameIn, "notFound"); 

            }
            spellNameText.GetComponentInChildren<TextMeshProUGUI>().font = englishFont;
        }


        return nameIn;
    }

    //Used to translate the UI component for the spells effect text if desired
    public string translateSpellEffectText(string inText)
    {
        if (spellLanguage.ToString() == "ENG")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataEnglish.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("EffectText").GetString(inText, "notFound"); 

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;

        }
        else if (spellLanguage.ToString() == "FRE")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataFrench.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("EffectText").GetString(inText, "notFound"); 

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;
        }
        else if (spellLanguage.ToString() == "JAP")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataJAP.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("EffectText").GetString(inText, "notFound"); 

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = japaneseFont;

        }
        else if (spellLanguage.ToString() == "GER")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataGER.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("EffectText").GetString(inText, "notFound"); 

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;
        }

        return inText;
    }

    //translate the UI component for the damage type of the spell if needed
    public string translateDamageType(string inText)
    {
        if (spellLanguage.ToString() == "ENG")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataEnglish.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("SpellDamageTyp").GetString(inText, "notFound"); 

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;

        }
        else if (spellLanguage.ToString() == "FRE")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataFrench.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("SpellDamageTyp").GetString(inText, "notFound"); 

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;
        }
        else if (spellLanguage.ToString() == "JAP")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataJAP.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("SpellDamageTyp").GetString(inText, "notFound"); 

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = japaneseFont;

        }
        else if (spellLanguage.ToString() == "GER")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataGER.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("SpellDamageTyp").GetString(inText, "notFound"); 

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;
        }


        return inText;
    }

    //Translate the UI component of the spell attack type if needed
    public string translateSpellAttackType(string inText)
    {
        if (spellLanguage.ToString() == "ENG")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataEnglish.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("SpellAttackType").GetString(inText, "notFound");

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;

        }
        else if (spellLanguage.ToString() == "FRE")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataFrench.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("SpellAttackType").GetString(inText, "notFound");

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;
        }
        else if (spellLanguage.ToString() == "JAP")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataJAP.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("SpellAttackType").GetString(inText, "notFound");

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = japaneseFont;

        }
        else if (spellLanguage.ToString() == "GER")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataGER.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                inText = data.GetCollection("SpellAttackType").GetString(inText, "notFound");

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;
        }
        return inText;
    }

    //Can either use this generic function for reduced code use or the specific translation function to reduce the number of parameters needed to pass in

    public string translateText(string collection, string text)
    {
        if (spellLanguage.ToString() == "ENG")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataEnglish.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                text = data.GetCollection(collection).GetString(text, text); //if we can find the translation of the passed in text just return the original text

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;

        }
        else if (spellLanguage.ToString() == "FRE")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataFrench.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                text = data.GetCollection(collection).GetString(text, text);

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;
        }
        else if (spellLanguage.ToString() == "JAP")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataJAP.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                text = data.GetCollection(collection).GetString(text, text);

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = japaneseFont;

        }
        else if (spellLanguage.ToString() == "GER")
        {
            string path = "Assets/Scripts/Spells/Translations/spellData/spellDataGER.grfon"; //refactored to other variable
            using (Stream stream = new FileStream(path, FileMode.Open)) //make a new stream object using the specified file path to read from
            {
                GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
                GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
                if (data == null) data = new GrfonCollection();
                text = data.GetCollection(collection).GetString(text, text);

            }
            //[TEXT OBJECT FOR YOUR UI].GetComponentInChildren<TextMeshProUGUI>().font = englishFont;
        }
        return text;
    }

}


///

/*            GrfonCollection names = GrfonCollection.FromString(nameIn);
            Debug.Log(names.ContainsKey("FireBolt"));
            nameIn = data.GetString("FireBolt", "nope");*/
//nameIn = data.GetString("FireBolt", "notFound"); //will not find the key/value pair inside that collection. Docs do not specify how to obtain things inside a collection


// Debug.Log(data.GetStringList("SpellName"));
//GRFON.GrfonValue temp = data.GetCollection("SpellName");
//Debug.Log(nameIn);
//Debug.Log(data.GetString(name));
//nameIn = data.ToString();
//Debug.Log(data.GetStringList("SpellName").ToString());
//data.GetString(("SpellName", nameIn);
//Debug.Log(data);
//nameIn = data.GetString(nameIn,"default");
//nameIn = data.GetString("SpellName");
//Debug.Log(data.GetString("SpellName"));

//nameIn = data.GetString("SpellName", "notFound");  //Can find the collection
//Debug.Log(data.ContainsKey("SpellName")); //TRUE output
//nameIn = data.GetString("FireBolt", "notFound"); //cannot find the key within that collection
//Debug.Log(data.GetCollection("SpellName").GetString("FireBolt")); //IT WORKS
//Debug.Log(data.GetCollection("SpellName").GetString("FlamingHands")); //hand passing in string works

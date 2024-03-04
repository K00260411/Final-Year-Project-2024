using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GRFON;
using System.IO;

public class PlayerMagicSystem : MonoBehaviour
{
    [SerializeField] private Spell spellToCast; //what spell are we trying to cast, make this an array of spells later
    [SerializeField] private float maxMana = 100f; //maximum amount of mana the player has  to cast spell
    [SerializeField] private float currentMana; //how much mana does the player currently have out of the maximum
    [SerializeField] private float manaRechargeRate = 5f; //how much mana does the player regain when not casting a spell
    [SerializeField] private float timeBetweenRecharge = 0.09f; //how often the player can cast a spell, spell details can change this value
    [SerializeField] private float timeBetweenCasts = 0.25f; //how often the player can cast a spell, spell details can change this value
    //Should probably make a copy of timeBetweenCasts to restore it back to normal after a spell with a higher time cost has finished countingdown

    private float currentCastTimer; //how much of the cooldown between casting spells has been completed?
    private bool atMaxMana;

    public Transform castPoint; //where the spell will originate from on the player

    private bool castingMagic = false; //are we already casting magic?

    //public List<Spell> spellBook; //array easier?
    public Spell[] spellBook;
    [HideInInspector]
    private int currentSpellBookIndex = 0;

    //Text object that will hold the name of the currently equiped spell
    public TMP_Text spellNameText;
    public TMP_Text manaCounterText;

    [HideInInspector]
    public GameObject lastHitGameObject = null;

    [HideInInspector] public enum languages { ENG, FRE, JAP, GER}
    public languages spellLanguage;
    public string translationFilePath;

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
        spellNameText.text = spellToCast.spellToCast.spellName;

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

        if (castingMagic == false && atMaxMana == false) //need to rework these conditions, is true when pressing Q for a sec
        {
            manaRecharge();
        }

        changeSpellText(); //Used for text objects, if you want a purely icon based display comment this out

        manaCounterText.text = "Mana: " + (int)currentMana;


        

    }

    public void changeSpellText()
    {
        if (Input.GetKeyDown(KeyCode.E)) //change what spell to cast 
        {
            currentSpellBookIndex++;
            if (currentSpellBookIndex <= spellBook.Length - 1)
            {
                spellToCast = spellBook[currentSpellBookIndex];
                spellNameText.text = getSpellName(spellToCast.spellToCast.spellName); //update the text object to use the spells name
            }

            if (currentSpellBookIndex >= spellBook.Length)
            {
                currentSpellBookIndex = 0;
                spellToCast = spellBook[currentSpellBookIndex];
                spellNameText.text = getSpellName(spellToCast.spellToCast.spellName); //update the text object to use the spells name
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

    public string getSpellName(string nameIn)
    {
        //Debug.Log(nameIn);
        string path = "Assets/Scripts/spellData.grfon"; //refactored to other variable
        using (Stream stream = new FileStream(translationFilePath, FileMode.Open)) //make a new stream object using the specified file path to read from
        {
            GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream)); //take in the contents as a node
            GrfonCollection data = des.Parse() as GrfonCollection; //save contents into a collection of nodes
            if (data == null) data = new GrfonCollection();
            //nameIn = data.GetString("SpellName", "notFound");  //Can find the collection
            //Debug.Log(data.ContainsKey("SpellName")); //TRUE output
            //nameIn = data.GetString("FireBolt", "notFound"); //cannot find the key within that collection
            //Debug.Log(data.GetCollection("SpellName").GetString("FireBolt")); //IT WORKS
            //Debug.Log(data.GetCollection("SpellName").GetString("FlamingHands")); //hand passing in string works
            nameIn = data.GetCollection("SpellName").GetString(nameIn, "notFound"); //ITS WORKING NOW LETS GOOOO
            //Debug.Log(data.ContainsKey("FireBolt")); // FALSE output

            Debug.Log(nameIn); //notFound output

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

        }


        return nameIn;
    }


/*    public static void Load()
    {
        string path = "Assets/Scripts/GRFON/spellData.grfon";
        using (Stream stream = new FileStream(path, FileMode.Open))
        {
            GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream));
            GrfonCollection data = des.Parse() as GrfonCollection;
            if (data == null) data = new GrfonCollection();

            gameMode = (GameMode)System.Enum.Parse(typeof(GameMode),
                    data.GetString("gameMode", "Sandbox"), true);
            spentXP = data.GetInt("spentXP");
            unspentXP = data.GetInt("unspentXP");
            projectsDone = data.GetStringList("projects");
        }
    }*/
}


using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.IO;

//load a level based of a text file

public class ASCIIlevelLoader : MonoBehaviour
{
    //Variables 

    //offdets for positions in the level
    public float xOffset;
    public float yOffset;

    //prefabs for the game object we want in the scene
    public GameObject player;
    public GameObject wall;
    public GameObject obstacle;
    public GameObject goal;

    //VARIABLE FOR THE CURRENT PLAYER
    public GameObject currentPlayer; 

    //variables for the starting position of our player
    Vector2 startPos;

    //name for the level file

    public string fileName;

    //variable for our current level number
    public int currentLevel = 0;

    //empty game object to hodl our level
    public GameObject level;

    //when the level changes we want to load the level
    //also make current level a proprerty
    public int Currentlevel
    {
        get { return currentLevel; }
        set
        {
            currentLevel = value;
            LoadLevel();
        }
    }


    //start is called before the first frame update

    private void Start()
    {
        LoadLevel();

    }
    //load a leve based on a Ascii text file
    void LoadLevel()
    {
        //destroy the current level
        Destroy(level);

        //create a new level gameobject
        level = new GameObject("level");

        //build a new lwvwl path based on the current level
        string current_file_path = Application.dataPath + "/resources/" + fileName.Replace("Num", currentLevel + "");
        //Pull the contents of thtat file into a string array
        //each line of the file will be an item in the array
        string[] fileLines = File.ReadAllLines(current_file_path);

        //loop through each line in the file
        for(int y = 0; y < fileLines.Length; y++)
        {
            //get the current line  
            string LineText = fileLines[y];

            //split the line into a character array
            char[] characters = LineText.ToCharArray(); 

            //loop through each character in the array we just made 
            for(int x = 0; x < characters.Length; x++)
            {
                //take the current character
                char c = characters[x];

                //variable for the new object
                GameObject newObject;
                //write a witch statement for the character to determne what it means
                switch (c) 
                {

                    //check if the character is the letter p and make that my player
                    case 'p': // checks if its p
                        //make a player a game object
                        newObject = Instantiate<GameObject>(player);
                        //check to see if we have a player already, and if you don't, make this the player
                        if (currentPlayer = null)
                            currentPlayer = newObject;
                        //save this position to the start pause to use for resetting 
                        startPos = new Vector2(x + xOffset, -y + yOffset);
                        break;

                    // write a case where if the character is W we make a wall
                    case 'w':
                        //make a wall
                        newObject = Instantiate<GameObject>(wall);
                        break;
                    //write a case where if the character is an * make a obstacle
                    case '*':
                        //make a obstacle
                        newObject = Instantiate<GameObject>(obstacle);
                        break;
                    // write a case where if the character is an & MAKE A GOAL
                    case '&':
                        // Make a goal
                        newObject = Instantiate< GameObject>(goal);
                        break;
                    // if it is any other character go to default and leave the space blank
                    default:
                        newObject = null;
                        break;

                
                }
                //take the new object and check if it's null
                if ( newObject != null )
                {

                    //check if it's a player
                    if ( newObject.name.Contains("Player"))
                    {
                        //make the level gameobject the parent of new object
                        newObject.transform.parent = level.transform;
                    }

                    //no matter what the nwe object is, set his position based on the offsets
                    //and also the position in the file
                    newObject.transform.position = new Vector3(x + xOffset, -y + yOffset, 0);


                }
            }
        }



    }
}

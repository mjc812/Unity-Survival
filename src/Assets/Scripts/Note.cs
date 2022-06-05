using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note
{
    /*
     * player inputs will have a dedicated script. No other scripts should be looking for player inputs
     * 
     * player input scripts will essentially run the game
     * 
     * 
     * 
     * tags will be for scripts that run the game
     * 
     * layer will be for identifying game objects like checking if raycast hit is a player. Or spheres that identify if a detected collisions is an
     * 
     * as heirarchy of characters grow, the layers should grow the same way. If there is Hostile that has Cannibal and Boar, then create layers Hostile, Cannibal and Boar
     * 
     * 
     * figure out how to organize folders for characters and items. 
     * - Have a folder for just the files for each leaf of a heirarchy. Cannibal and player subfolder would have the health scripts
     * - Another folder for heirarchy files. So abstract health script will be in this folder. A folder for each of the top most generic class, and folder for each subclass
     * 
     *     A      B
     *    /   \  / 
     *  C      D  
     *        / \
     *       E   F 
     *       
     *  There would be a folder for C and D/E/F. If D was Character, and E/F were player/cannibal, then the generic health script would be in folder D. 
     */
}

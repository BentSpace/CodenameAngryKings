using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int round = 1; // what round are we in?
    public Text roundtext;
    int currentplayer = 1; //whose turn is it?
    public Text playertext;
    public FireCatapult Player1;
    public FireCatapult Player2;


    enum gameState {PAUSED = -1, GAMESTART, PLAYMENU, ATTACK, AIMING, BUILD, GAMEEND};  // game states:
                                                                                        // PAUSED: pause menu loaded, can reset game, go back to main menu
                                                                                        // GAMESTART: Start of the game, decide who goes first
                                                                                        // PLAYMENU: Menu where current player selects whether to build or attack
                                                                                        // ATTACK: state where current player can select weapon to fire
                                                                                        // AIMING: state where current player can adjust selected weapon
                                                                                        // BUILD: state where current player can place bricks or weapons
                                                                                        // GAMEEND: results of match are displayed, and a new game can begin
    gameState state = gameState.GAMESTART;
    // Start is called before the first frame update
    void Start()
    {
        Player2.setActive(false);
        Player1.setActive(true);            
    }

    // Update is called once per frame
    void Update()
    {
        roundtext.text = round.ToString();
        playertext.text = currentplayer.ToString();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            endTurn();
        }

        switch (state)
        {
            case gameState.PAUSED:
                break;
            case gameState.GAMESTART:
                break;
            case gameState.PLAYMENU:
                break;
            case gameState.ATTACK:
                break;
            case gameState.AIMING:
                break;
            case gameState.BUILD:
                break;
            case gameState.GAMEEND:
                break;
        }
    }

    void endTurn()
    {
        if (currentplayer == 2)
        {
            currentplayer = 1;
            Player2.setActive(false);
            Player1.setActive(true);
            round++;
        }
        else
        {
            currentplayer = 2;
            Player1.setActive(false);
            Player2.setActive(true);
        }
    }
}

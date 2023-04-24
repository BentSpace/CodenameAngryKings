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
    bool changeRound = false; // do we change the round after this turn?
    public FireCatapult Player1;
    public FireCatapult Player2;


    public enum gameState {PAUSED = -1, GAMESTART, PLAYMENU, ATTACK, AIMING, FIRE, BUILD, GAMEEND};  // game states:
                                                                                        // PAUSED: pause menu loaded, can reset game, go back to main menu
                                                                                        // GAMESTART: Start of the game, decide who goes first
                                                                                        // PLAYMENU: Menu where current player selects whether to build or attack
                                                                                        // ATTACK: state where current player can select weapon to fire
                                                                                        // AIMING: state where current player can adjust selected weapon
                                                                                        // FIRE: state where control is taken away while attack plays out
                                                                                        // BUILD: state where current player can place bricks or weapons
                                                                                         // GAMEEND: results of match are displayed, and a new game can begin
    gameState state = gameState.GAMESTART; // current game state
    gameState refState = gameState.GAMESTART; // for checking state of game each frame
    gameState prevState = gameState.GAMESTART; // previous game state for unpausing game

    [SerializeField]
    GameObject PauseMenu;
    [SerializeField]
    GameObject PlayerMenu;
    [SerializeField]
    GameObject AttackMenu;
    [SerializeField]
    GameObject AimMenu;
    [SerializeField]
    GameObject BuildMenu;

    // Start is called before the first frame update
    void Start()
    {
        Player2.setActive(false);
        Player1.setActive(true);            
    }

    // Update is called once per frame
    void Update()
    {
        // check if we need to update previous state
        if(state != refState)
        {
            closeMenus();
            prevState = refState;
        }
        refState = state;

        roundtext.text = round.ToString();
        playertext.text = currentplayer.ToString();

        switch (state)
        {
            case gameState.PAUSED:
                PauseMenu.SetActive(true);
                //STOP THE GAME
                Time.timeScale = 0;
                // IF UNPAUSE IS PRESSED
                    //RETURN TO PREVIOUS STATE
                break;
            case gameState.GAMESTART:
                // FLIP A COIN TO DECIDE WHO GOES FIRST
                // SET CURRENT PLAYER TO WINNER OF COIN TOSS
                currentplayer = Random.Range(1, 2);
                // GO TO PLAY MENU STATE FOR CURRENT PLAYER
                AssignState((int)gameState.PLAYMENU);
                break;
            case gameState.PLAYMENU:
                // DRAW MENU
                PlayerMenu.SetActive(true);
                // IF PLAYER HAS ANY WEAPONS BUILT
                    // IF ATTACK IS CHOSEN
                        // GO TO ATTACK STATE
                // IF BUILD IS CHOSEN
                    // GO TO BUILD STATE
                break;
            case gameState.ATTACK:
                AttackMenu.SetActive(true);
                // HANDLE UNIT SELECTION
                // IF BACK IS PRESSED
                // GO TO PLAYER MENU
                // IF CONFIRM IS PRESSED
                // GO TO AIMING STATE
                break;
            case gameState.AIMING:
                AimMenu.SetActive(true);
                // HANDLE AIMING UNIT
                // IF BACK IS PRESSED
                // RESET UNIT POSITION
                // GO TO ATTACK STATE
                // IF FIRE IS PRESSED
                // GO TO FIRE STATE
                break;
            case gameState.FIRE:
                // FIRE SELECTED WEAPON
                // WAIT FOR AFTERMATH OF FIRING TO COMPLETE
                AssignState((int)gameState.PLAYMENU);
                // END THE CURRENT PLAYER'S TURN
                break;
            case gameState.BUILD:
                BuildMenu.SetActive(true);
                // HANDLE BUILDING MODE
                // IF BACK IS PRESSED
                // GO TO PLAYER MENU
                // IF CONFIRM IS PRESSED
                // END CURRENT PLAYER'S TURN
                break;
            case gameState.GAMEEND:
                // DISPLAY RESULTS OF GAME
                // IF PLAYER PRESSES NEW GAME
                    // RESTART THE GAME
                // IF PLAYER PRESSES MAIN MENU
                    // GO TO MAIN MENU
                break;
        }
    }

    public void endTurn()
    {
        if (currentplayer == 2)
        {
            currentplayer = 1;
            Player2.setActive(false);
            Player1.setActive(true);
        }
        else
        {
            currentplayer = 2;
            Player1.setActive(false);
            Player2.setActive(true);
        }
        if(changeRound)
        {
            round++;
        }
        changeRound = !changeRound;
    }

    public void AssignState(int gs)
    {
        state = (gameState)gs;
    }

    public void unpause()
    {
        Time.timeScale = 1;
        state = prevState;
    }

    void closeMenus()
    {
        PauseMenu.SetActive(false);
        PlayerMenu.SetActive(false);
        AttackMenu.SetActive(false);
        AimMenu.SetActive(false) ;
        BuildMenu.SetActive(false);
    }
}

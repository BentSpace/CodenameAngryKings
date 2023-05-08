using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    int round = 1; // what round are we in?
    public Text roundtext;
    int currentplayer = 1; //whose turn is it?
    public Text playertext;
    bool changeRound = false; // do we change the round after this turn?
    public HealthSystem Player1;
    public HealthSystem Player2;
    int winner = 0; // who won the game?

    public AudioSource backgroundMusic;
    public AudioSource backgroundAmbience;
    public AudioSource blockPlaceSE;

    [SerializeField]
    Weapon p1currentWeapon; // which weapon is in use right now?
    [SerializeField]
    Weapon p2currentWeapon;
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
    gameState refState = gameState.GAMESTART; // reference state for checking state of game each frame
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
    [SerializeField]
    GameObject WinScreen;
    [SerializeField]
    TextMeshProUGUI WinText;
    [SerializeField]
    TextMeshProUGUI WinnerText;
    [SerializeField]
    TextMeshProUGUI DrawText;

    public TurnHighlightController king1HighlightController;
    public TurnHighlightController king2HighlightController;
    
    // Start is called before the first frame update
    void Start()
    {
        backgroundAmbience.Play();
        backgroundMusic.Play();
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

        if(Player1.getCurrentHealth() <= 0 && Player2.getCurrentHealth() <= 0)
        {
            winner = 0;
            AssignState((int)gameState.GAMEEND);
        }
        else if (Player1.getCurrentHealth() <= 0)
        {
            winner = 2;
            AssignState((int)gameState.GAMEEND);
        }
        else if (Player2.getCurrentHealth() <= 0)
        {
            winner = 1;
            AssignState((int)gameState.GAMEEND);
        }

        switch (state)
        {
            case gameState.PAUSED:

                PauseMenu.SetActive(true);
                backgroundMusic.Pause();
                backgroundAmbience.Pause();
                //STOP THE GAME
                Time.timeScale = 0;
                break;
            case gameState.GAMESTART:
                // FLIP A COIN TO DECIDE WHO GOES FIRST
                // SET CURRENT PLAYER TO WINNER OF COIN TOSS
                currentplayer = Random.Range(1, 3);
                // GO TO PLAY MENU STATE FOR CURRENT PLAYER
                AssignState((int)gameState.PLAYMENU);
                if (currentplayer == 1)
                {
                    king1HighlightController.SetPlayerTurn(true);
                }
                else
                {
                    king2HighlightController.SetPlayerTurn(true);
                }
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
                WinScreen.SetActive(true);
                if(winner == 0)
                {
                    WinText.gameObject.SetActive(false);
                    WinnerText.gameObject.SetActive(false);
                    DrawText.gameObject.SetActive(true);
                }
                else
                {
                    WinText.gameObject.SetActive(true);
                    WinnerText.gameObject.SetActive(true);
                    WinnerText.text = winner.ToString();
                    DrawText.gameObject.SetActive(false);
                }
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
            king1HighlightController.SetPlayerTurn(true);
            king2HighlightController.SetPlayerTurn(false);
           // Player2.setActive(false);
           // Player1.setActive(true);
        }
        else
        {
            currentplayer = 2;
            king2HighlightController.SetPlayerTurn(true);
            king1HighlightController.SetPlayerTurn(false);
            //Player1.setActive(false);
            //Player2.setActive(true);
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
        backgroundAmbience.Play();
        backgroundMusic.Play();
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
        WinScreen.SetActive(false);
    }

    public void newGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int getCurrentPlayer()
    {
        return currentplayer;
    }

    public void aimCurrentWeapon(bool value)
    {
        switch (currentplayer)
        {
            case 1:
                p1currentWeapon.SendMessage("setActive", value);
                break;
            case 2:
                p2currentWeapon.SendMessage("setActive", value);
                break;
        }
    }
    public void fireCurrentWeapon()
    {
        switch (currentplayer)
        {
            case 1:
                p1currentWeapon.Fire();
                break;
            case 2:
                p2currentWeapon.Fire();
                break;
        }
    }
}

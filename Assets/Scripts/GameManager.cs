using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int round; // what round are we in?
    public Text roundtext;
    int currentplayer = 1; //whose turn is it?
    public Text playertext;
    public FireCatapult Player1;
    public FireCatapult Player2;

    // Start is called before the first frame update
    void Start()
    {
        Player2.enabled = false;
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

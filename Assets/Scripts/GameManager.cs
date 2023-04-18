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

    // Start is called before the first frame update
    void Start()
    {
        
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
            round++;
        }
        else currentplayer = 2;
    }
}

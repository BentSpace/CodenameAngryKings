using UnityEngine;

public class TurnHighlightController : MonoBehaviour
{
    public GameObject turnHighlight; // Reference to the TurnHighlight object
    public bool isPlayerTurn; // A flag indicating if it's the player's turn

    void Update()
    {
        // Set the TurnHighlight object's visibility based on the player's turn status
        turnHighlight.SetActive(isPlayerTurn);
    }

    // Method to set the player's turn status
    public void SetPlayerTurn(bool isTurn)
    {
        isPlayerTurn = isTurn;
    }
}


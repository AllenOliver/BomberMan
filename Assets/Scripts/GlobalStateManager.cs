



using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GlobalStateManager : MonoBehaviour {
    public int deadPlayers;
    public int deadPlayerNumber = -1;

    public string winText;
    public Text winStringOut;
    public GameObject winScreen;
    public bool playerOneDead = false;
    public bool playerTwoDead = false;

    private void Start()
    {
        winStringOut = GetComponent<Text>();
    }
    public void PlayerDied(int playerNumber)
    {
        deadPlayers++; // 1
        Debug.Log("DeadPlayers Called: " + deadPlayers.ToString() );
        if (deadPlayers == 1)
        { // 2
            deadPlayerNumber = playerNumber; // 3
            Invoke("CheckPlayersDeath", .2f); // 4
        }
    }

    public void CheckPlayersDeath()
    {
        if(deadPlayers == 1)
        {

            if(deadPlayerNumber == 1)
            {
                Debug.Log("P2 Win");
                winScreen.SetActive(true);
                winText = "Player 2 Wins!";
                winStringOut.text = winText;

            }
            else
            {
                Debug.Log("P1 Win");
                winScreen.SetActive(true);
                winText = "Player 1 Wins!";
                winStringOut.text = winText;

            }

        }
        else
        {
            Debug.Log("Draw!");

        }
    }
}

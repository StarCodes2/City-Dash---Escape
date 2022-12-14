using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public int currentPlayerIndex;

    //Array for Player AND Characters
    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);

        foreach (GameObject player in players)
            player.SetActive(false);

        players[currentPlayerIndex].SetActive(true);
    }
}

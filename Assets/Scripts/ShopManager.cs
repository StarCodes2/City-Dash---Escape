using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int currentPlayerIndex;

    //Array for Player AND Characters
    public GameObject[] playerStore;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);

        foreach (GameObject player in playerStore)
            player.SetActive(false);

        playerStore[currentPlayerIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeNext()
    {
        playerStore[currentPlayerIndex].SetActive(false);

        currentPlayerIndex++;
        if (currentPlayerIndex == playerStore.Length)
            currentPlayerIndex = 0;

        playerStore[currentPlayerIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
    }

    public void ChangePrevious()
    {
        playerStore[currentPlayerIndex].SetActive(false);

        currentPlayerIndex--;
        if (currentPlayerIndex == -1)
            currentPlayerIndex = playerStore.Length -1;

        playerStore[currentPlayerIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
    }
}

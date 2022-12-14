using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int currentPlayerIndex;

    //Array for Player AND Characters
    public GameObject[] playerStore;
    public PlayerBlueprint[] players;

    public Button buyButton;

    // Start is called before the first frame update
    void Start()
    {
        foreach(PlayerBlueprint player in players)
        {
            if (player.price == 0)
                player.isUnlock = true;
            else
                player.isUnlock = PlayerPrefs.GetInt(player.name, 0) == 0 ? false : true;
        }

        currentPlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);

        foreach (GameObject player in playerStore)
            player.SetActive(false);

        playerStore[currentPlayerIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void ChangeNext()
    {
        playerStore[currentPlayerIndex].SetActive(false);

        currentPlayerIndex++;
        if (currentPlayerIndex == playerStore.Length)
            currentPlayerIndex = 0;

        playerStore[currentPlayerIndex].SetActive(true);
        PlayerBlueprint p = players[currentPlayerIndex];
        if (!p.isUnlock)
            return;
        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
    }

    public void ChangePrevious()
    {
        playerStore[currentPlayerIndex].SetActive(false);

        currentPlayerIndex--;
        if (currentPlayerIndex == -1)
            currentPlayerIndex = playerStore.Length -1;

        playerStore[currentPlayerIndex].SetActive(true);
        PlayerBlueprint p = players[currentPlayerIndex];
        if (!p.isUnlock)
            return;

        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
    }

    public void UnlockPlayer()
    {
        PlayerBlueprint p = players[currentPlayerIndex];
        PlayerPrefs.SetInt(p.name, 1);
        PlayerPrefs.SetInt("SeletedPlayer", currentPlayerIndex);
        p.isUnlock = true;
        PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins", 0) - p.price);
    }


    private void UpdateUI()
    {
        PlayerBlueprint p = players[currentPlayerIndex];
        if(p.isUnlock)
        {
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy-" + p.price;
            if(p.price < PlayerPrefs.GetInt("NunmerOfCoins", 0))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }
}

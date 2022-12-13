using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    private GameObject[] characters;
    private int selectedCharacter = 0;

    public BluePrint[] chShop;
    public GameObject locked;
    public Button buyButton, selectButton;
    public Text coin, price;

    // Start is called before the first frame update
    private void Start()
    {
        // Shop lock
        foreach (BluePrint ch in chShop)
        {
            if (ch.price == 0)
                ch.isUnlocked = true;
            else
                ch.isUnlocked = PlayerPrefs.GetInt(ch.name, 0) == 0 ? false : true;
        }
        coin.text = PlayerPrefs.GetInt("Coin", 0).ToString();

        // Character Selection
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        characters = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            characters[i] = transform.GetChild(i).gameObject;

        foreach (GameObject ch in characters)
        {
            ch.SetActive(false);
        }

        if (characters[selectedCharacter])
            characters[selectedCharacter].SetActive(true);
    }

    private void Update()
    {
        LockUIUpdate();
    }

    public void ToggleLeft()
    {
        characters[selectedCharacter].SetActive(false);

        selectedCharacter--;
        if (selectedCharacter < 0)
            selectedCharacter = characters.Length - 1;

        characters[selectedCharacter].SetActive(true);
    }

    public void ToggleRight()
    {
        characters[selectedCharacter].SetActive(false);

        selectedCharacter++;
        if (selectedCharacter >= characters.Length)
            selectedCharacter = 0;

        characters[selectedCharacter].SetActive(true);
    }

    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }

    public void UnlockCh()
    {
        BluePrint c = chShop[selectedCharacter];

        PlayerPrefs.SetInt(c.name, 1);
        c.isUnlocked = true;
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) - c.price);
    }

    private void LockUIUpdate()
    {
        BluePrint c = chShop[selectedCharacter];
        if (c.isUnlocked)
        {
            locked.SetActive(false);
            selectButton.interactable = true;
        }
        else
        {
            locked.SetActive(true);
            selectButton.interactable = false;
            price.text = c.price.ToString();
            if (c.price < PlayerPrefs.GetInt("Coin", 0))
            {
                buyButton.interactable = true;
            } else
            {
                buyButton.interactable = false;
            }
        }
    }
}

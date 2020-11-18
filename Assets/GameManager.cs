using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject deckRef;

    private GameObject buttonRef;

    private bool unoCall = false;

    public bool GetUnoCall()
    {
        return unoCall;
    }

    public bool SetUnoCall(bool newCall)
    {
        unoCall = newCall;
        return unoCall;
    }

    public GameObject GetButtonRef()
    {
        return buttonRef;
    }

    // Start is called before the first frame update
    void Start()
    {
        deckRef = GameObject.FindGameObjectWithTag("Deck");

        buttonRef = GameObject.FindGameObjectWithTag("UnoButton");

        checkInteractiveButton(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkInteractiveButton(bool checkButton)
    {
        ColorBlock colors = buttonRef.GetComponent<Button>().colors;

        if (checkButton == true)
        {
            colors.selectedColor = Color.green;

            buttonRef.GetComponent<Button>().interactable = true;
        }
        else
        {
            colors.disabledColor = Color.red;
            colors.normalColor = Color.red;
            colors.selectedColor = Color.red;
            buttonRef.GetComponent<Image>().color = Color.white;

            buttonRef.GetComponent<Button>().interactable = false;

            unoCall = false;
        }

        buttonRef.GetComponent<Button>().colors = colors;
    }

    public void clickUnoButton()
    {
        buttonRef.GetComponent<Image>().color = Color.green;
        unoCall = true;
    }

    public void StartOfTurn(UserHand handOfUser)
    {
        Debug.Log("STARTOFTURN!");

        if (handOfUser.GetCardHand().Count == 2)
        {
            checkInteractiveButton(false);
        }
    }

    public void EndOfTurn(UserHand handOfUser)
    {
        Debug.Log("ENDOFTURN!");

        if (handOfUser.GetCardHand().Count == 1)
        {
            if (unoCall == false)
            {
                handOfUser.DrawCardInHand(handOfUser.GetCardHand().Count);
                handOfUser.DrawCardInHand(handOfUser.GetCardHand().Count);
            }
        }
        else if(handOfUser.GetCardHand().Count == 0)
        {
            if(unoCall == true)
            {
                Debug.Log("WONTHEMATCH!");
            }
        }
    }
}

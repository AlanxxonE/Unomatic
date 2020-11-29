using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    private Vector3 cardOverOffset = new Vector3(0, 0.1f, -0.01f);

    private Vector3 printedNumberOffset = new Vector3(0, 0.01f, 0);

    private Vector3 originalCardPos;

    private bool playCardCheck = false;

    private UserHand userCardHand;

    private int uniqueCardIDNumber = 0;
    private int uniqueCardIDColor = 0;

    private GameObject printedNumberRef;
    private GameObject printedNumberToAdd;

    public enum CardState
    {
        CardInDeck,
        CardInHand,
        CardInPile
    }

    private CardState cardStateReference;

    public CardState GetCardState()
    {
        return cardStateReference;
    }

    public CardState SetCardState(CardState cardState)
    {
        cardStateReference = cardState;
        return cardStateReference;
    }

    public GameObject GetAddedPrintedNumberRef()
    {
        return printedNumberToAdd;
    }

    public int GetUniqueCardIDNumber()
    {
        return uniqueCardIDNumber;
    }

    public int SetUniqueCardIDNumber(int uniqueID)
    {
        uniqueCardIDNumber = uniqueID;
        return uniqueCardIDNumber;
    }

    public int GetUniqueCardIDColor()
    {
        return uniqueCardIDColor;
    }

    public int SetUniqueCardIDColor(int uniqueColor)
    {
        uniqueCardIDColor = uniqueColor;
        return uniqueCardIDColor;
    }

    public Vector3 GetOriginalCardPos()
    {
        return originalCardPos;
    }

    public Vector3 SetOriginalCardPos(Vector3 originalPos)
    {
        originalCardPos = originalPos;
        return originalCardPos;
    }

    public bool GetCardCheck()
    {
        return playCardCheck;
    }

    public bool SetCardCheck(bool cardCheck)
    {
        playCardCheck = cardCheck;
        return playCardCheck;
    }

    public UserHand SetUserHand(UserHand userHand)
    {
        userCardHand = userHand;
        return userCardHand;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (userCardHand != null && userCardHand.GetHandMovement() == false)
        {
            if (cardStateReference == CardState.CardInHand)
            {
                SwitchColliders();
                userCardHand.SetCardSelected(true);

                if (this.transform.position == originalCardPos && userCardHand.GetCardHand().Count != 1)
                {
                    this.transform.position += cardOverOffset;
                }
            }
        }
    }

    private void OnMouseExit()
    {
        if (userCardHand.GetHandMovement() == false)
        {
            if (cardStateReference == CardState.CardInHand)
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    SwitchColliders();
                    userCardHand.SetCardSelected(false);
                }

                if (this.transform.position != originalCardPos && userCardHand.GetCardHand().Count != 1)
                {
                    this.transform.position = originalCardPos;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (cardStateReference == CardState.CardInHand)
        {
            if (userCardHand.GetHandState() == UserHand.HandState.PlayCard)
            {
                if (userCardHand.GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor() == this.uniqueCardIDColor || userCardHand.GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber() == this.uniqueCardIDNumber)
                {
                    userCardHand.SetCardSelected(false);
                    if (this.transform.position != originalCardPos)
                    {
                        this.transform.position = originalCardPos;
                    }

                    PlayCard();
                }
            }
        }
    }

    private void PlayCard()
    {
        Debug.Log("___________________________________________________________________");

        SetCardCheck(true);

        SetCardState(CardState.CardInPile);

        List<GameObject> userCardHandList = userCardHand.GetCardHand();

        int cardRemovedId = userCardHandList.Count;

        for (int i = 0; i < userCardHandList.Count; i++)
        {
            if (userCardHandList[i].GetComponent<CardBehaviour>().GetCardCheck() == true)
            {
                userCardHand.GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().AddCardToPile(userCardHandList[i]);
                cardRemovedId = i;
            } 
            else if (i > cardRemovedId)
            {
                userCardHandList[i].transform.position -= userCardHand.GetHandOffSet();
                userCardHandList[i].GetComponent<CardBehaviour>().SetOriginalCardPos(userCardHandList[i].transform.position);
            }
        }

        userCardHandList.RemoveAt(cardRemovedId);

        for (int i = 0; i < userCardHandList.Count; i++)
        {
            Debug.Log("CardInHANDNumber --> " + userCardHandList[i].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() + " CardInHANDColor --> " + userCardHandList[i].GetComponent<CardBehaviour>().GetUniqueCardIDColor());
        }

        userCardHand.UpdateCardHand(userCardHandList);

        if(userCardHandList.Count == 2)
        {
            userCardHand.GetDeck().GetComponent<DeckBehaviour>().GetGMRef().checkInteractiveButton(true);
        }

        StartCoroutine(userCardHand.GetDeck().GetComponent<DeckBehaviour>().GetGMRef().AITurn());

        //userCardHand.UpdateCardHand(userCardHandList);

        //userCardHand.SetHandState(UserHand.HandState.WaitForTurn);
    }

    private void SwitchColliders()
    {
        if (userCardHand.GetCardSelected() == false)
        {
            this.GetComponents<BoxCollider>()[0].enabled = false;
            this.GetComponents<BoxCollider>()[1].enabled = true;
        }
        else
        {
            this.GetComponents<BoxCollider>()[0].enabled = true;
            this.GetComponents<BoxCollider>()[1].enabled = false;
        }
    }

    public void AddPrintedNumbers(int uniqueNumber)
    {
        printedNumberRef = GameObject.FindGameObjectWithTag(uniqueNumber.ToString());

        printedNumberToAdd = Instantiate(printedNumberRef);

        printedNumberToAdd.transform.SetParent(this.transform);
        printedNumberToAdd.transform.position = this.transform.position + printedNumberOffset;
    }
}

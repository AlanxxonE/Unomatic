using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    private Vector3 cardOverOffset = new Vector3(0, 0.1f, -0.01f);

    private bool playCardCheck = false;

    private UserHand userCardHand;

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
        if (cardStateReference == CardState.CardInHand)
        {
            SwitchColliders();
        }
    }

    private void OnMouseExit()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            if (cardStateReference == CardState.CardInHand)
            {
                SwitchColliders();
            }
        }
    }

    private void OnMouseDown()
    {
        if (userCardHand.GetHandState() == UserHand.HandState.PlayCard)
        {
            if (cardStateReference == CardState.CardInHand)
            {
                PlayCard();
            }
        }
    }

    private void PlayCard()
    {
        SetCardCheck(true);

        SetCardState(CardState.CardInPile);

        List<GameObject> userCardHandList = userCardHand.GetCardHand();

        int cardRemovedId = userCardHandList.Count;

        for (int i = 0; i < userCardHandList.Count; i++)
        {
            if (userCardHandList[i].GetComponent<CardBehaviour>().GetCardCheck() == true)
            {
                userCardHandList[i].transform.parent = userCardHand.GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().transform.parent;
                userCardHandList[i].transform.SetPositionAndRotation(userCardHand.GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().transform.position, Quaternion.Euler(Vector3.zero));
                cardRemovedId = i;
            } 
            else if (i > cardRemovedId)
            {
                userCardHandList[i].transform.position -= userCardHand.GetHandOffSet();
            }
        }

        userCardHandList.RemoveAt(cardRemovedId);

        userCardHand.UpdateCardHand(userCardHandList);
    }

    private void SwitchColliders()
    {
        if (this.GetComponents<BoxCollider>()[0].enabled == true)
        {
            this.GetComponents<BoxCollider>()[0].enabled = false;
            this.GetComponents<BoxCollider>()[1].enabled = true;
            this.transform.position += cardOverOffset;
        }
        else if (this.GetComponents<BoxCollider>()[0].enabled == false)
        {
            this.GetComponents<BoxCollider>()[0].enabled = true;
            this.GetComponents<BoxCollider>()[1].enabled = false;
            this.transform.position -= cardOverOffset;
        }
    }

}

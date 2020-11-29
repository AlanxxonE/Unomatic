using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHand : MonoBehaviour
{
    private GameObject deckRef;

    private bool cardSelected = false;

    private List<GameObject> cardHand = new List<GameObject>();

    private Vector3 userHandRot = new Vector3(-60, 0, 0);
    private Vector3 handOffSet = new Vector3(0.14f, 0, 0.001f);

    private bool checkBeginOfRound = false;

    private bool handMoving = false;

    private int cardHandStartNumber = 7;

    private string userTag = " ";

    public enum HandState
    {
        WaitForTurn,
        PlayCard,
        DrawCard
    }

    private HandState handStateReference;

    public string GetUserTag()
    {
        return userTag;
    }

    public bool GetHandMovement()
    {
        return handMoving;
    }

    public bool GetCardSelected()
    {
        return cardSelected;
    }

    public bool SetCardSelected(bool select)
    {
        cardSelected = select;
        return cardSelected;
    }
    public HandState GetHandState()
    {
        return handStateReference;
    }

    public HandState SetHandState(HandState handState)
    {
        handStateReference = handState;
        return handStateReference;
    }

    public Vector3 GetHandOffSet()
    {
        return handOffSet;
    }

    public List<GameObject> GetCardHand()
    {
        return cardHand;
    }

    public List<GameObject> UpdateCardHand(List<GameObject> newCardHand)
    {
        cardHand = newCardHand;
        return cardHand;
    }

    public GameObject GetDeck()
    {
        return deckRef;
    }

    public GameObject SetDeck(GameObject deck)
    {
        deckRef = deck;
        return deckRef;
    }

    // Start is called before the first frame update
    void Start()
    {
        userTag = transform.root.gameObject.tag;

        handStateReference = HandState.WaitForTurn;

        for (int i = 0; i < cardHandStartNumber; i++)
        {
            StartCoroutine("StartDraw", i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checkBeginOfRound == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (handStateReference == HandState.WaitForTurn)
                {
                    handStateReference = HandState.PlayCard;

                    deckRef.GetComponent<DeckBehaviour>().GetGMRef().StartOfTurn(this.GetComponent<UserHand>());
                }
                else
                {
                    handStateReference = HandState.WaitForTurn;

                    deckRef.GetComponent<DeckBehaviour>().GetGMRef().EndOfTurn(this.GetComponent<UserHand>());
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (userTag != "AI")
            {
                if (handMoving == false)
                {
                    handMoving = true;

                    for (int i = 0; i < cardHand.Count; i++)
                    {
                        if (cardHand[i].transform.position != cardHand[i].GetComponent<CardBehaviour>().GetOriginalCardPos() && cardHand.Count != 1)
                        {
                            cardHand[i].transform.position = cardHand[i].GetComponent<CardBehaviour>().GetOriginalCardPos();
                        }
                    }
                }

                if (Input.mousePosition.y < Screen.height / 2)
                {
                    if (Input.mousePosition.x > Screen.width / 2)
                    {
                        if (this.cardHand[0].transform.position.x < -0.5f)
                        {
                            this.transform.Translate(0.01f, 0, 0);
                        }
                    }
                    else
                    {
                        if (this.cardHand[0].transform.position.x > (-0.11f * cardHand.Count))
                        {
                            this.transform.Translate(-0.01f, 0, 0);
                        }
                    }
                }
            }
        }
        else
        {
            if(handMoving == true)
            {
                for(int i = 0; i < cardHand.Count; i++)
                {
                    cardHand[i].GetComponent<CardBehaviour>().SetOriginalCardPos(cardHand[i].transform.position);
                }

                handMoving = false;
            }
        }
    }

    IEnumerator StartDraw(int i)
    {
        yield return new WaitForSeconds(i * 0.4f);
        DrawCardInHand(i);
        if(cardHand.Count == cardHandStartNumber)
        {
            if (userTag != "AI")
            {
                StartCoroutine(deckRef.GetComponent<DeckBehaviour>().GetGMRef().AITurn());
            }

            //handStateReference = HandState.PlayCard;
            checkBeginOfRound = true;
        }
    }

    public void DrawCardInHand(int i)
    {
        cardHand.Add(deckRef.GetComponent<DeckBehaviour>().DrawCard());
        cardHand[i].GetComponent<CardBehaviour>().SetUserHand(this.gameObject.GetComponent<UserHand>());
        cardHand[i].transform.SetParent(this.transform);
        cardHand[i].transform.position = this.transform.position + (i * handOffSet);
        if (cardHand[i].transform.root.gameObject.tag == "AI")
        {
            cardHand[i].GetComponent<CardBehaviour>().SetCardState(CardBehaviour.CardState.AICard);
            cardHand[i].transform.eulerAngles = userHandRot * -1;
        }
        else
        {
            cardHand[i].GetComponent<CardBehaviour>().SetCardState(CardBehaviour.CardState.CardInHand);
            cardHand[i].transform.eulerAngles = userHandRot;
            Debug.Log("CardInHANDNumber --> " + cardHand[i].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() + " CardInHANDColor --> " + cardHand[i].GetComponent<CardBehaviour>().GetUniqueCardIDColor());
        }        
        cardHand[i].GetComponent<CardBehaviour>().SetOriginalCardPos(cardHand[i].transform.position);
    }
}

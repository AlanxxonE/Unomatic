using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBehaviour : MonoBehaviour
{
    private List<GameObject> deckOfCards = new List<GameObject>();

    private Vector3 deckOffSett = new Vector3(0, 0.002f, 0);

    private PileOfCardsBehaviour pileOfCardsRef;

    private UserHand userHandRef;

    private GameObject cardRef;

    public List<GameObject> GetDeckOfCards()
    {
        return deckOfCards;
    }

    public GameObject DrawCard()
    {
        GameObject cardToDraw = deckOfCards[deckOfCards.Count - 1];

        deckOfCards.RemoveAt(deckOfCards.Count - 1);

        return cardToDraw;
    }

    public PileOfCardsBehaviour GetPileOfCards()
    {
        return pileOfCardsRef;
    }

    public GameObject GetCardRef()
    {
        return cardRef;
    }

    // Start is called before the first frame update
    void Start()
    {
        cardRef = GameObject.FindGameObjectWithTag("Card");

        userHandRef = GameObject.FindGameObjectWithTag("User").GetComponentInChildren<UserHand>();

        pileOfCardsRef = GameObject.FindGameObjectWithTag("PileOfCards").GetComponent<PileOfCardsBehaviour>();

        GenerateDeck(deckOfCards);

        userHandRef.SetDeck(this.gameObject);

        pileOfCardsRef.SetDeck(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //int cardHandCounter = 0;

        //for (int i = 0; i < userHandRef.GetCardHand().Count; i++)
        //{
        //    if(userHandRef.GetCardHand()[i].GetComponent<CardBehaviour>().GetCardCheck() == false)
        //    {
        //        cardHandCounter++;
        //    }
        //}

        //if(cardHandCounter == userHandRef.GetCardHand().Count)
        //{
        //    userHandRef.SetHandState(UserHand.HandState.DrawCard);
        //}

        //if (userHandRef.GetHandState() == UserHand.HandState.DrawCard)
        //{
        //    userHandRef.DrawCardInHand(userHandRef.GetCardHand().Count);

        //    userHandRef.SetHandState(UserHand.HandState.PlayCard);
        //}

        if (deckOfCards.Count != 0)
        {
            userHandRef.DrawCardInHand(userHandRef.GetCardHand().Count);
        }
    }

    public void GenerateDeck(List<GameObject> deckList)
    {
        int unoCardIndex = 0;
        for (int unoNumbers = 0; unoNumbers < 9; unoNumbers++)
        {
            for(int unoColors = 0; unoColors < 8; unoColors++)
            {
                GameObject cardClone = Instantiate(cardRef);
                deckList.Add(cardClone);
                deckList[unoCardIndex].GetComponent<CardBehaviour>().SetCardState(CardBehaviour.CardState.CardInDeck);
                deckList[unoCardIndex].transform.SetParent(this.transform);
                deckList[unoCardIndex].transform.position = this.transform.position;
                deckList[unoCardIndex].transform.Translate(unoCardIndex * deckOffSett);

                deckList[unoCardIndex].GetComponent<CardBehaviour>().uniqueCardIDNumber = unoNumbers;
                deckList[unoCardIndex].GetComponent<CardBehaviour>().uniqueCardIDColor = unoColors % 4;

                switch (unoColors % 4)
                {
                    case 0:
                        deckList[unoCardIndex].GetComponent<Renderer>().material.color = Color.yellow;
                        break;
                    case 1:
                        deckList[unoCardIndex].GetComponent<Renderer>().material.color = Color.green;
                        break;
                    case 2:
                        deckList[unoCardIndex].GetComponent<Renderer>().material.color = Color.blue;
                        break;
                    case 3:
                        deckList[unoCardIndex].GetComponent<Renderer>().material.color = Color.red;
                        break;
                }

                Debug.Log("CardUniqueIDNumber = " + deckList[unoCardIndex].GetComponent<CardBehaviour>().uniqueCardIDNumber + " CardUniqueIDColor = " + deckList[unoCardIndex].GetComponent<CardBehaviour>().uniqueCardIDColor);

                unoCardIndex++;
            }
        }

        ShuffleDeck(deckList);
    }

    public void ShuffleDeck(List<GameObject> deckList)
    {
        for(int numberOfCard= 0; numberOfCard < deckList.Count; numberOfCard++)
        {
            GameObject tempCard = deckList[numberOfCard];

            int randomIndex = Random.Range(numberOfCard, deckList.Count);

            deckList[numberOfCard] = deckList[randomIndex];

            deckList[randomIndex] = tempCard;
        }
    }

}

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

    private GameManager gMRef;

    private bool ableToDraw = false;

    public GameManager GetGMRef()
    {
        return gMRef;
    }

    public UserHand GetUserHand()
    {
        return userHandRef;
    }

    public List<GameObject> GetDeckOfCards()
    {
        return deckOfCards;
    }

    public GameObject DrawCard()
    {
        GameObject cardToDraw = deckOfCards[deckOfCards.Count - 1];

        deckOfCards.RemoveAt(deckOfCards.Count - 1);

        cardToDraw.GetComponent<CardBehaviour>().GetAddedPrintedNumberRef().SetActive(true);
        cardToDraw.GetComponent<Renderer>().material.color -= Color.white;

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
        gMRef = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

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
        if (deckOfCards.Count != 0)
        {
            checkHandToDraw();

            if (ableToDraw == true)
            {
                userHandRef.SetHandState(UserHand.HandState.DrawCard);
            }

            if (userHandRef.GetHandState() == UserHand.HandState.DrawCard)
            {
                userHandRef.DrawCardInHand(userHandRef.GetCardHand().Count);

                checkHandToDraw();

                if(ableToDraw == true)
                {
                    StartCoroutine(gMRef.AITurn());
                }
                else
                {
                    userHandRef.SetHandState(UserHand.HandState.PlayCard);
                }
            }
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

                deckList[unoCardIndex].GetComponent<CardBehaviour>().SetUniqueCardIDNumber(unoNumbers);

                deckList[unoCardIndex].GetComponent<CardBehaviour>().AddPrintedNumbers(deckList[unoCardIndex].GetComponent<CardBehaviour>().GetUniqueCardIDNumber());
                deckList[unoCardIndex].GetComponent<CardBehaviour>().GetAddedPrintedNumberRef().SetActive(false);

                deckList[unoCardIndex].GetComponent<CardBehaviour>().SetUniqueCardIDColor(unoColors % 4);

                switch (unoColors % 4)
                {
                    case 0:
                        deckList[unoCardIndex].GetComponent<Renderer>().material.color = Color.yellow + Color.white;
                        break;
                    case 1:
                        deckList[unoCardIndex].GetComponent<Renderer>().material.color = Color.green + Color.white;
                        break;
                    case 2:
                        deckList[unoCardIndex].GetComponent<Renderer>().material.color = Color.blue + Color.white;
                        break;
                    case 3:
                        deckList[unoCardIndex].GetComponent<Renderer>().material.color = Color.red + Color.white;
                        break;
                }

                //Debug.Log("CardUniqueIDNumber = " + deckList[unoCardIndex].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() + " CardUniqueIDColor = " + deckList[unoCardIndex].GetComponent<CardBehaviour>().GetUniqueCardIDColor());

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

    private bool checkHandToDraw()
    {
        int cardHandCounter = 0;

        bool checkDraw = false;

        for (int i = 0; i < userHandRef.GetCardHand().Count; i++)
        {
            if (userHandRef.GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() != pileOfCardsRef.GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber() && userHandRef.GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDColor() != pileOfCardsRef.GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor())
            {
                cardHandCounter++;
            }
        }

        if (cardHandCounter == userHandRef.GetCardHand().Count && userHandRef.GetHandState() != UserHand.HandState.WaitForTurn)
        {
            checkDraw = true;
        }
        else
        {
            checkDraw = false;
        }

        ableToDraw = checkDraw;
        return ableToDraw;
    }
}

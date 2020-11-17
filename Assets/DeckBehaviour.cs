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
        for (int i = 0; i < 72; i++)
        {
            int randomValue = Random.Range(0, 2);
            GameObject cardClone = Instantiate(cardRef);
            deckList.Add(cardClone);
            deckList[i].GetComponent<CardBehaviour>().SetCardState(CardBehaviour.CardState.CardInDeck);
            deckList[i].transform.SetParent(this.transform);
            deckList[i].transform.position = this.transform.position;
            deckList[i].transform.Translate(i * deckOffSett);
            deckList[i].GetComponent<CardBehaviour>().uniqueCardID = i;
            if (randomValue == 0)
            {
                deckList[i].GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                deckList[i].GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}

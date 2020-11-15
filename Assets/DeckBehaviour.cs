using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBehaviour : MonoBehaviour
{
    PileOfCardsBehaviour pileOfCardsRef;
    UserHand userHandRef;

    public PileOfCardsBehaviour GetPileOfCards()
    {
        return pileOfCardsRef;
    }

    // Start is called before the first frame update
    void Start()
    {
        userHandRef = GameObject.FindGameObjectWithTag("User").GetComponentInChildren<UserHand>();
        pileOfCardsRef = GameObject.FindGameObjectWithTag("PileOfCards").GetComponent<PileOfCardsBehaviour>();
        userHandRef.SetDeck(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        int cardHandCounter = 0;

        for (int i = 0; i < userHandRef.GetCardHand().Count; i++)
        {
            if(userHandRef.GetCardHand()[i].GetComponent<CardBehaviour>().GetCardCheck() == false)
            {
                cardHandCounter++;
            }
        }

        if(cardHandCounter == userHandRef.GetCardHand().Count)
        {
            userHandRef.SetHandState(UserHand.HandState.DrawCard);
        }

        if (userHandRef.GetHandState() == UserHand.HandState.DrawCard)
        {
            userHandRef.GenerateCard(userHandRef.GetCardHand().Count);

            userHandRef.SetHandState(UserHand.HandState.PlayCard);
        }
    }
}

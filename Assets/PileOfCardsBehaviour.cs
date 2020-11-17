using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOfCardsBehaviour : MonoBehaviour
{
    private GameObject cardRef;

    private GameObject deckRef;

    private Vector3 pileOffSett = new Vector3(0, -0.008f, 0);

    private List<GameObject> pileCardsList = new List<GameObject>();

    public List<GameObject> GetPileCard()
    {
        return pileCardsList;
    }
    
    public List<GameObject> AddCardToPile(GameObject card)
    {
        if(pileCardsList.Count != 0)
        {
            for(int i = 0; i < pileCardsList.Count; i++)
            {
                pileCardsList[i].transform.Translate(pileOffSett);
            }
        }

        pileCardsList.Add(card);
        DisplayLastCardInPile(card);
        return pileCardsList;
    }

    public GameObject GetCardRef()
    {
        return cardRef;
    }

    public GameObject SetCardRef(GameObject card)
    {
        cardRef = card;
        return cardRef;
    }

    public GameObject SetDeck(GameObject deck)
    {
        deckRef = deck;
        return deckRef;
    }

    // Start is called before the first frame update
    void Start()
    {
        AddCardToPile(deckRef.GetComponent<DeckBehaviour>().DrawCard());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject DisplayLastCardInPile(GameObject cardToDisplay)
    {
        cardRef = cardToDisplay;
        cardRef.transform.SetParent(this.transform);
        cardRef.transform.SetPositionAndRotation(this.transform.position, Quaternion.Euler(Vector3.zero));
        return cardRef;
    }
}

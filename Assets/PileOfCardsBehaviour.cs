using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOfCardsBehaviour : MonoBehaviour
{
    private GameObject cardRef;

    private GameObject deckRef;

    private GameObject cardToShow;

    private Vector3 pileOffSett = new Vector3(0, -0.08f, 0);

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
                //pileCardsList[i].transform.Translate(pileOffSett);
                if (pileCardsList[i].activeSelf == true)
                {
                    pileCardsList[i].SetActive(false);
                }
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
        AddCardToPile(deckRef.GetComponent<DeckBehaviour>().DrawCard(this.transform));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        cardToShow = Instantiate(cardRef);
        cardToShow.GetComponent<BoxCollider>().enabled = false;
        cardToShow.transform.SetPositionAndRotation(this.transform.position + (Vector3.up / 2), Quaternion.Euler(new Vector3(-90, 0, 0)));
        cardToShow.transform.Translate(Vector3.left / 2);
        cardToShow.transform.localScale *= 2;
    }

    private void OnMouseExit()
    {
        if (cardToShow != null)
        {
            Destroy(cardToShow);
        }
    }

    private GameObject DisplayLastCardInPile(GameObject cardToDisplay)
    {
        cardRef = cardToDisplay;
        cardRef.transform.SetParent(this.transform);
        cardRef.transform.SetPositionAndRotation(this.transform.position, Quaternion.Euler(Vector3.zero));
        //Debug.Log("CardInPILENumber --> " + cardToDisplay.GetComponent<CardBehaviour>().GetUniqueCardIDNumber() + " CardInPILEColor --> " + cardToDisplay.GetComponent<CardBehaviour>().GetUniqueCardIDColor());
        return cardRef;
    }
}

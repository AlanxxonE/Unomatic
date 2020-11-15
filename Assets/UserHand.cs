using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHand : MonoBehaviour
{
    private GameObject cardRef;
    private GameObject deckRef;
    private bool cardSelected = false;
    private List<GameObject> cardHand = new List<GameObject>();
    private Vector3 userHandRot = new Vector3(-60, 0, 0);
    private Vector3 handOffSet = new Vector3(0.14f, 0, 0.001f);

    public enum HandState
    {
        WaitForTurn,
        PlayCard,
        DrawCard
    }

    private HandState handStateReference;

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
        handStateReference = HandState.WaitForTurn;

        cardRef = GameObject.FindGameObjectWithTag("Card");

        for (int i = 0; i < 5; i++)
        {
            StartCoroutine("GenerateHand", i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GenerateHand(int i)
    {
        yield return new WaitForSeconds(i * 0.4f);
        GenerateCard(i);
        if(cardHand.Count == 5)
        {
            handStateReference = HandState.PlayCard;
        }
    }

    public void GenerateCard(int i)
    {
        GameObject cardClone = Instantiate(cardRef);
        cardHand.Add(cardClone);
        cardHand[i].GetComponent<CardBehaviour>().SetCardState(CardBehaviour.CardState.CardInHand);
        cardHand[i].GetComponent<CardBehaviour>().SetUserHand(this.gameObject.GetComponent<UserHand>());
        cardHand[i].transform.parent = this.transform;
        cardHand[i].transform.position = this.transform.position + (i * handOffSet);
        cardHand[i].transform.eulerAngles = userHandRot;
        cardHand[i].GetComponent<CardBehaviour>().SetOriginalCardPos(cardHand[i].transform.position);
    }
}

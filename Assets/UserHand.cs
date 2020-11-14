using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHand : MonoBehaviour
{
    private GameObject cardRef;
    private List<GameObject> cardHand = new List<GameObject>();
    private Vector3 userHandRot = new Vector3(-60, 0, 0);
    private Vector3 handOffSet = new Vector3(0.12f, 0, 0);
    
    public enum HandState
    {
        WaitForTurn,
        PlayCard,
        DrawCard
    }

    private HandState handStateReference;

    public HandState GetHandState()
    {
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
        GameObject cardClone = Instantiate(cardRef);
        cardHand.Add(cardClone);
        cardHand[i].GetComponent<CardBehaviour>().SetUserHand(this.gameObject.GetComponent<UserHand>());
        cardHand[i].transform.parent = this.transform;
        cardHand[i].transform.position = this.transform.position + (i * handOffSet);
        cardHand[i].transform.eulerAngles = userHandRot;
        if(cardHand.Count == 5)
        {
            handStateReference = HandState.PlayCard;
        }
    }
}

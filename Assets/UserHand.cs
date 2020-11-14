using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHand : MonoBehaviour
{
    private GameObject cardRef;
    private List<GameObject> cardHand = new List<GameObject>();
    private Vector3 userHandRot = new Vector3(-60, 0, 0);
    private Vector3 handOffSet = new Vector3(0.12f, 0, 0);


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
        cardRef = GameObject.FindGameObjectWithTag("Card");

        for (int i = 0; i < 5; i++)
        {
            StartCoroutine("GenerateHand", i);
        }
    }

    // Update is called once per frame
    void Update()
    {
    //    if(Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        if (cardHand.Count != 0)
    //        {
    //            Destroy(cardHand[cardHand.Count - 1]);
    //            cardHand.RemoveAt(cardHand.Count - 1);
    //        }
    //    }
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
    }
}

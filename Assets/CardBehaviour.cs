using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    private Vector3 cardOverOffset = new Vector3(0, 0.1f, -0.01f);

    private bool playCardCheck = false;

    private UserHand userCardHand;

    public bool GetCardCheck()
    {
        return playCardCheck;
    }

    public bool SetCardCheck(bool cardCheck)
    {
        playCardCheck = cardCheck;
        return playCardCheck;
    }

    public UserHand SetUserHand(UserHand userHand)
    {
        userCardHand = userHand;
        return userCardHand;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        SwitchColliders();
        this.transform.position += cardOverOffset;
    }

    private void OnMouseExit()
    {
        SwitchColliders();
        this.transform.position -= cardOverOffset;
    }

    private void OnMouseDown()
    {
        if (userCardHand.GetHandState() == UserHand.HandState.PlayCard)
        {
            PlayCard();
        }
    }

    private void PlayCard()
    {
        SetCardCheck(true);

        List<GameObject> userCardHandList = userCardHand.GetCardHand();

        int cardRemovedId = userCardHandList.Count;

        for (int i = 0; i < userCardHandList.Count; i++)
        {
            if (userCardHandList[i].GetComponent<CardBehaviour>().GetCardCheck() == true)
            {
                Destroy(userCardHandList[i].gameObject);
                cardRemovedId = i;
            } 
            else if (i > cardRemovedId)
            {
                userCardHandList[i].transform.position -= userCardHand.GetHandOffSet();
            }
        }

        userCardHandList.RemoveAt(cardRemovedId);

        userCardHand.UpdateCardHand(userCardHandList);
    }

    private void SwitchColliders()
    {
        if (this.GetComponents<BoxCollider>()[0].enabled == true)
        {
            this.GetComponents<BoxCollider>()[0].enabled = false;
            this.GetComponents<BoxCollider>()[1].enabled = true;
        }
        else if (this.GetComponents<BoxCollider>()[0].enabled == false)
        {
            this.GetComponents<BoxCollider>()[0].enabled = true;
            this.GetComponents<BoxCollider>()[1].enabled = false;
        }
    }
}

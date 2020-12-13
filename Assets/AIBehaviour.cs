using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    private float decisionTime = 0;

    public float GetDecisionTime()
    {
        return decisionTime;
    }

    public float SetDecisionTime(float time)
    {
        decisionTime = time;
        return decisionTime;
    }

    public enum DifficultyLevels
    {
        Easy,
        Medium,
        Hard
    }

    public static DifficultyLevels difficultyLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckAIHand()
    {
        if (difficultyLevel == DifficultyLevels.Easy)
        {
            if (GetComponentInChildren<UserHand>().GetHandState() == UserHand.HandState.PlayCard)
            {
                for (int i = 0; i < GetComponentInChildren<UserHand>().GetCardHand().Count; i++)
                {
                    if (GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber()
                        || GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor())
                    {
                        GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().PlayCard();

                        GetComponentInChildren<UserHand>().SetHandState(UserHand.HandState.WaitForTurn);

                        break;
                    }
                    else if (i == GetComponentInChildren<UserHand>().GetCardHand().Count - 1)
                    {
                        GetComponentInChildren<UserHand>().DrawCardInHand(GetComponentInChildren<UserHand>().GetCardHand().Count);

                        if (GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber()
                        || GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor())
                        {
                            GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().PlayCard();
                        }

                        GetComponentInChildren<UserHand>().SetHandState(UserHand.HandState.WaitForTurn);

                        break;
                    }
                }
            }
        }
        else if(difficultyLevel == DifficultyLevels.Medium)
        {
            Debug.Log("MEDIUMMEDIUM!");
        }
        else if(difficultyLevel == DifficultyLevels.Hard)
        {
            Debug.Log("HARDHARDHARD!");
        }
    }
}

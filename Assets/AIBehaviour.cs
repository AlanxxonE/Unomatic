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


    private List<GameObject> yellowCardList = new List<GameObject>();

    private List<GameObject> greenCardList = new List<GameObject>();

    private List<GameObject> blueCardList = new List<GameObject>();

    private List<GameObject> redCardList = new List<GameObject>();

    private List<List<GameObject>> orderOfColors = new List<List<GameObject>>();

    private List<List<GameObject>> originalOrderList = new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        difficultyLevel = DifficultyLevels.Hard;

        orderOfColors.Add(yellowCardList);
        orderOfColors.Add(greenCardList);
        orderOfColors.Add(blueCardList);
        orderOfColors.Add(redCardList);
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
            if (GetComponentInChildren<UserHand>().GetHandState() == UserHand.HandState.PlayCard)
            {
                for (int i = 0; i < GetComponentInChildren<UserHand>().GetCardHand().Count; i++)
                {
                    if (GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == 0)
                    {
                        yellowCardList.Add(GetComponentInChildren<UserHand>().GetCardHand()[i]);
                    }
                    else if (GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == 1)
                    {
                        greenCardList.Add(GetComponentInChildren<UserHand>().GetCardHand()[i]);
                    }
                    else if (GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == 2)
                    {
                        blueCardList.Add(GetComponentInChildren<UserHand>().GetCardHand()[i]);
                    }
                    else if (GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == 3)
                    {
                        redCardList.Add(GetComponentInChildren<UserHand>().GetCardHand()[i]);
                    }
                }

                for (int i = 0; i < orderOfColors.Count; i++)
                {
                    originalOrderList.Add(orderOfColors[i]);
                }

                Debug.Log("UNSORTED ----------------------");

                Debug.Log(yellowCardList.Count);
                Debug.Log(greenCardList.Count);
                Debug.Log(blueCardList.Count);
                Debug.Log(redCardList.Count);

                Debug.Log("SORTED !!!!!!!!!!!!!!!!!!!!!!!!!!!");

                SortOrderList();

                for (int i = 0; i < orderOfColors.Count; i++)
                {
                    Debug.Log(orderOfColors[i].Count);
                }
            }
        }
    }

    private void SortOrderList()
    {
        for (int numberOfElements = 1; numberOfElements < orderOfColors.Count; numberOfElements++)
        {
            if(orderOfColors[numberOfElements].Count > orderOfColors[numberOfElements - 1].Count)
            {
                List<GameObject> tempList = orderOfColors[numberOfElements - 1];
                orderOfColors[numberOfElements - 1] = orderOfColors[numberOfElements];
                orderOfColors[numberOfElements] = tempList;
                numberOfElements = 0;
            }
        }
    }
}

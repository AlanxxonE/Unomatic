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

    private int listDimension = -1;

    // Start is called before the first frame update
    void Start()
    {
        difficultyLevel = DifficultyLevels.Hard;
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

                for (int i = 0; i < 4; i++)
                {
                    if (listDimension < yellowCardList.Count)
                    {
                        listDimension = yellowCardList.Count;
                        if (!orderOfColors.Contains(yellowCardList))
                        {
                            orderOfColors.Add(yellowCardList);
                        }
                    }
                    
                    if (listDimension < greenCardList.Count)
                    {
                        listDimension = greenCardList.Count;
                        if (!orderOfColors.Contains(greenCardList))
                        {
                            orderOfColors.Add(greenCardList);
                        }
                    }
                    
                    if (listDimension < blueCardList.Count)
                    {
                        listDimension = blueCardList.Count;
                        if (!orderOfColors.Contains(blueCardList))
                        {
                            orderOfColors.Add(blueCardList);
                        }
                    }
                    
                    if (listDimension < redCardList.Count)
                    {
                        listDimension = redCardList.Count;
                        if (!orderOfColors.Contains(redCardList))
                        {
                            orderOfColors.Add(redCardList);
                        }
                    }
                }

                Debug.Log(yellowCardList.Count);
                Debug.Log(greenCardList.Count);
                Debug.Log(blueCardList.Count);
                Debug.Log(redCardList.Count);

                Debug.Log(orderOfColors);
            }
        }
    }
}

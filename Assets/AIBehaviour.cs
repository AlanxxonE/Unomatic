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
        EASY,
        MEDIUM,
        HARD
    }

    public static DifficultyLevels difficultyLevel;


    private List<GameObject> yellowCardList = new List<GameObject>();

    private List<GameObject> greenCardList = new List<GameObject>();

    private List<GameObject> blueCardList = new List<GameObject>();

    private List<GameObject> redCardList = new List<GameObject>();

    private List<List<GameObject>> orderOfColors = new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
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
        if (difficultyLevel == DifficultyLevels.EASY)
        {
            if (GetComponentInChildren<UserHand>().GetHandState() == UserHand.HandState.PlayCard)
            {
                for (int i = 0; i < GetComponentInChildren<UserHand>().GetCardHand().Count; i++)
                {
                    if (GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber()
                        || GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor())
                    {
                        GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().PlayCard();

                        goto NextTurn;
                    }
                    else if (i == GetComponentInChildren<UserHand>().GetCardHand().Count - 1)
                    {
                        GetComponentInChildren<UserHand>().DrawCardInHand(GetComponentInChildren<UserHand>().GetCardHand().Count);

                        if (GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber()
                        || GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor())
                        {
                            GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().PlayCard();
                        }

                        goto NextTurn;
                    }
                }

            NextTurn:

                GetComponentInChildren<UserHand>().SetHandState(UserHand.HandState.WaitForTurn);
            }
        }
        else if(difficultyLevel == DifficultyLevels.MEDIUM)
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

                SortOrderList();

                for (int j = 0; j < orderOfColors[0].Count; j++)
                {
                    if (orderOfColors[0][j].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber())
                    {
                        orderOfColors[0][j].GetComponent<CardBehaviour>().PlayCard();

                        goto NextTurn;
                    }

                    if (j == orderOfColors[0].Count - 1)
                    {
                        if (orderOfColors[0][j].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor())
                        {
                            orderOfColors[0][j].GetComponent<CardBehaviour>().PlayCard();

                            goto NextTurn;
                        }
                    }
                }

                for (int i = 0; i < GetComponentInChildren<UserHand>().GetCardHand().Count; i++)
                {
                    if (GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber()
                        || GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor())
                    {
                        GetComponentInChildren<UserHand>().GetCardHand()[i].GetComponent<CardBehaviour>().PlayCard();

                        goto NextTurn;
                    }
                    else if (i == GetComponentInChildren<UserHand>().GetCardHand().Count - 1)
                    {
                        GetComponentInChildren<UserHand>().DrawCardInHand(GetComponentInChildren<UserHand>().GetCardHand().Count);

                        if (GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber()
                        || GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor())
                        {
                            GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().PlayCard();
                        }

                        goto NextTurn;
                    }
                }

            NextTurn:

                for (int i = 0; i < orderOfColors.Count; i++)
                {
                    orderOfColors[i].Clear();
                }

                GetComponentInChildren<UserHand>().SetHandState(UserHand.HandState.WaitForTurn);
            }
        }
        else if(difficultyLevel == DifficultyLevels.HARD)
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

                SortOrderList();

                for (int i = 0; i < orderOfColors.Count; i++)
                {
                    for (int j = 0; j < orderOfColors[i].Count; j++)
                    {
                        if (orderOfColors[i][j].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber())
                        {
                            orderOfColors[i][j].GetComponent<CardBehaviour>().PlayCard();

                            goto NextTurn;
                        }

                        if(j == orderOfColors[i].Count - 1)
                        {
                            if (orderOfColors[i][j].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor())
                            {
                                orderOfColors[i][j].GetComponent<CardBehaviour>().PlayCard();

                                goto NextTurn;
                            }
                        }
                    }
                }

                GetComponentInChildren<UserHand>().DrawCardInHand(GetComponentInChildren<UserHand>().GetCardHand().Count);

                if (GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().GetUniqueCardIDNumber() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDNumber()
                || GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().GetUniqueCardIDColor() == GetComponentInChildren<UserHand>().GetDeck().GetComponent<DeckBehaviour>().GetPileOfCards().GetCardRef().GetComponent<CardBehaviour>().GetUniqueCardIDColor())
                {
                    GetComponentInChildren<UserHand>().GetCardHand()[GetComponentInChildren<UserHand>().GetCardHand().Count - 1].GetComponent<CardBehaviour>().PlayCard();

                    goto NextTurn;
                }

            NextTurn:

                for(int i = 0; i< orderOfColors.Count; i++)
                {
                    orderOfColors[i].Clear();
                }

                GetComponentInChildren<UserHand>().SetHandState(UserHand.HandState.WaitForTurn);
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

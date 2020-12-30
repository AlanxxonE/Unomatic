using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject deckRef;

    private GameObject buttonRef;

    private bool unoCall = false;

    private bool unoAICall = false;

    private GameObject DealingTextRef;

    private GameObject AITextRef;

    private GameObject UserTextRef;

    private GameObject UnosCallRef;

    private ParticleSystem textEffectRef;

    public bool GetUnoCall()
    {
        return unoCall;
    }

    public bool SetUnoCall(bool newCall)
    {
        unoCall = newCall;
        return unoCall;
    }

    public GameObject GetButtonRef()
    {
        return buttonRef;
    }

    // Start is called before the first frame update
    void Start()
    {
        deckRef = GameObject.FindGameObjectWithTag("Deck");

        buttonRef = GameObject.FindGameObjectWithTag("UnoButton");

        DealingTextRef = GameObject.FindGameObjectWithTag("DealingText");

        AITextRef = GameObject.FindGameObjectWithTag("AIText");

        UserTextRef = GameObject.FindGameObjectWithTag("UserText");

        UnosCallRef = GameObject.FindGameObjectWithTag("UnosText");

        textEffectRef = GameObject.FindGameObjectWithTag("TextEffect").GetComponent<ParticleSystem>();

        AITextRef.SetActive(false);

        UserTextRef.SetActive(false);

        UnosCallRef.SetActive(false);

        checkInteractiveButton(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkInteractiveButton(bool checkButton)
    {
        ColorBlock colors = buttonRef.GetComponent<Button>().colors;

        if (checkButton == true)
        {
            colors.selectedColor = Color.green;

            buttonRef.GetComponent<Button>().interactable = true;
        }
        else
        {
            colors.disabledColor = Color.red;
            colors.normalColor = Color.red;
            colors.selectedColor = Color.red;
            buttonRef.GetComponent<Image>().color = Color.white;

            buttonRef.GetComponent<Button>().interactable = false;

            unoCall = false;
        }

        buttonRef.GetComponent<Button>().colors = colors;
    }

    public void clickUnoButton()
    {
        buttonRef.GetComponent<Image>().color = Color.green;
        if (unoCall == false)
        {
            StartCoroutine(UnosTextRoutine());

            GetComponent<AudioSource>().Play();

            unoCall = true;
        }
    }

    public void AICallUnoButton()
    {
        StartCoroutine(UnosTextRoutine());

        GetComponent<AudioSource>().Play();

        unoAICall = false;
    }

    public void StartOfTurn(UserHand handOfUser)
    {
        //Debug.Log("STARTOFTURN!");

        if(handOfUser.GetCardHand().Count <= 2)
        {
            checkInteractiveButton(true);
        }
        else
        {
            checkInteractiveButton(false);
        }
    }

    public void EndOfTurn(UserHand handOfUser)
    {
        //Debug.Log("ENDOFTURN!");

        if (handOfUser.GetCardHand().Count == 1)
        {
            if (unoCall == false)
            {
                handOfUser.DrawCardInHand(handOfUser.GetCardHand().Count);
                handOfUser.DrawCardInHand(handOfUser.GetCardHand().Count);
            }
        }
    }

    public IEnumerator AITurn()
    {
        if (deckRef.GetComponent<DeckBehaviour>().GetUserHand().GetCardHand().Count == 0)
        {
            FinishGame();
        }

        StartCoroutine(AITextRoutine());

        deckRef.GetComponent<DeckBehaviour>().GetUserHand().SetHandState(UserHand.HandState.WaitForTurn);

        yield return new WaitForSeconds(1);

        EndOfTurn(deckRef.GetComponent<DeckBehaviour>().GetUserHand());

        yield return new WaitForSeconds(1);

        StartCoroutine(UserTextRoutine());

        deckRef.GetComponent<DeckBehaviour>().GetAIHand().SetHandState(UserHand.HandState.PlayCard);

        deckRef.GetComponent<DeckBehaviour>().GetAIHand().GetComponentInParent<AIBehaviour>().CheckAIHand();

        if(deckRef.GetComponent<DeckBehaviour>().GetAIHand().GetCardHand().Count == 1)
        {
            float callPercentage = Random.Range(0, 1f);

            Debug.Log(callPercentage);

            if (AIBehaviour.difficultyLevel == AIBehaviour.DifficultyLevels.EASY)
            {
                if(callPercentage > 0.6f) 
                {
                    unoAICall = true;
                }
            }
            else if (AIBehaviour.difficultyLevel == AIBehaviour.DifficultyLevels.MEDIUM)
            {
                if(callPercentage > 0.4f)
                {
                    unoAICall = true;
                }
            }
            else if(AIBehaviour.difficultyLevel == AIBehaviour.DifficultyLevels.HARD)
            {
                unoAICall = true;
            }
        }

        if(deckRef.GetComponent<DeckBehaviour>().GetAIHand().GetCardHand().Count == 1 && unoAICall != true)
        {
            deckRef.GetComponent<DeckBehaviour>().GetAIHand().DrawCardInHand(deckRef.GetComponent<DeckBehaviour>().GetAIHand().GetCardHand().Count);
            deckRef.GetComponent<DeckBehaviour>().GetAIHand().DrawCardInHand(deckRef.GetComponent<DeckBehaviour>().GetAIHand().GetCardHand().Count);
        }
        else if(deckRef.GetComponent<DeckBehaviour>().GetAIHand().GetCardHand().Count == 1 && unoAICall == true)
        {
            AICallUnoButton();
        }

        if (deckRef.GetComponent<DeckBehaviour>().GetAIHand().GetCardHand().Count == 0)
        {
            FinishGame();
        }

        StartOfTurn(deckRef.GetComponent<DeckBehaviour>().GetAIHand());

        if (deckRef.GetComponent<DeckBehaviour>().GetUserHand().GetCardHand().Count != 0 && deckRef.GetComponent<DeckBehaviour>().GetAIHand().GetCardHand().Count != 0)
        {
            StartOfTurn(deckRef.GetComponent<DeckBehaviour>().GetUserHand());

            deckRef.GetComponent<DeckBehaviour>().GetUserHand().SetHandState(UserHand.HandState.PlayCard);
        }
    }

    public void FinishGame()
    {
        deckRef.GetComponent<DeckBehaviour>().GetUserHand().SetHandState(UserHand.HandState.WaitForTurn);

        StartCoroutine(FinishGameRoutine());
    }

    private IEnumerator AITextRoutine()
    {
        AITextRef.SetActive(true);

        textEffectRef.Play();

        yield return new WaitForSeconds(2);

        AITextRef.SetActive(false);
    }

    private IEnumerator UserTextRoutine()
    {
        UserTextRef.SetActive(true);

        textEffectRef.Play();

        yield return new WaitForSeconds(2);

        UserTextRef.SetActive(false);
    }

    private IEnumerator UnosTextRoutine()
    {
        UnosCallRef.SetActive(true);

        textEffectRef.Play();

        yield return new WaitForSeconds(2);

        UnosCallRef.SetActive(false);
    }

    private IEnumerator FinishGameRoutine()
    {
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.2f);

        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.4f);

        GetComponent<AudioSource>().Play();

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    
}

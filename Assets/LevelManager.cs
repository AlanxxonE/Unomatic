using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Button startRef;

    private Button backRef;

    private Button difficultyRef;

    private Button easyDifRef;

    private Button mediumDifRef;

    private Button hardDifRef;

    private Button exitRef;

    private Text AIStatusTextRef;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("StartButton") != null)
        {
            startRef = GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>();
        }

        if (GameObject.FindGameObjectWithTag("BackButton") != null)
        {
            backRef = GameObject.FindGameObjectWithTag("BackButton").GetComponent<Button>();
        }

        if (GameObject.FindGameObjectWithTag("DifficultyButton") != null)
        {
            difficultyRef = GameObject.FindGameObjectWithTag("DifficultyButton").GetComponent<Button>();
        }

        if (GameObject.FindGameObjectWithTag("EasyButton") != null)
        {
            easyDifRef = GameObject.FindGameObjectWithTag("EasyButton").GetComponent<Button>();
        }

        if (GameObject.FindGameObjectWithTag("MediumButton") != null)
        {
            mediumDifRef = GameObject.FindGameObjectWithTag("MediumButton").GetComponent<Button>();
        }

        if (GameObject.FindGameObjectWithTag("HardButton") != null)
        {
            hardDifRef = GameObject.FindGameObjectWithTag("HardButton").GetComponent<Button>();
        }

        if (GameObject.FindGameObjectWithTag("ExitButton") != null)
        {
            exitRef = GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>();
        }

        if (GameObject.FindGameObjectWithTag("AIStatusText") != null)
        {
            AIStatusTextRef = GameObject.FindGameObjectWithTag("AIStatusText").GetComponent<Text>();
        }

        if (AIStatusTextRef != null)
        {
            AIStatusTextRef.text = "AI: " + AIBehaviour.difficultyLevel.ToString();
        }

        if (startRef != null)
        {
            startRef.onClick.AddListener(StartButtonMethod);
        }

        if (backRef != null)
        {
            backRef.onClick.AddListener(BackButtonMethod);
        }

        if (difficultyRef != null)
        {
            difficultyRef.onClick.AddListener(DifficultyButtonMethod);
        }

        if (easyDifRef != null)
        {
            easyDifRef.onClick.AddListener(EasyButtonMethod);
        }

        if (mediumDifRef != null)
        {
            mediumDifRef.onClick.AddListener(MediumButtonMethod);
        }

        if (hardDifRef != null)
        {
            hardDifRef.onClick.AddListener(HardButtonMethod);
        }

        if (exitRef != null)
        {
            exitRef.onClick.AddListener(ExitButtonMethod);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartButtonMethod()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    private void BackButtonMethod()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void DifficultyButtonMethod()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void EasyButtonMethod()
    {
        if (AIBehaviour.difficultyLevel != AIBehaviour.DifficultyLevels.EASY)
        {
            AIBehaviour.difficultyLevel = AIBehaviour.DifficultyLevels.EASY;
            AIStatusTextRef.text = "AI: " + AIBehaviour.difficultyLevel.ToString();
        }
    }

    private void MediumButtonMethod()
    {
        if (AIBehaviour.difficultyLevel != AIBehaviour.DifficultyLevels.MEDIUM)
        {
            AIBehaviour.difficultyLevel = AIBehaviour.DifficultyLevels.MEDIUM;
            AIStatusTextRef.text = "AI: " + AIBehaviour.difficultyLevel.ToString();
        }
    }
    private void HardButtonMethod()
    {
        if (AIBehaviour.difficultyLevel != AIBehaviour.DifficultyLevels.HARD)
        {
            AIBehaviour.difficultyLevel = AIBehaviour.DifficultyLevels.HARD;
            AIStatusTextRef.text = "AI: " + AIBehaviour.difficultyLevel.ToString();
        }
    }

    private void ExitButtonMethod()
    {
        Application.Quit();
    }
}

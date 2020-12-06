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

    private Button exitRef;

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

        if (GameObject.FindGameObjectWithTag("ExitButton") != null)
        {
            exitRef = GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>();
        }

        if(startRef != null)
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

    private void ExitButtonMethod()
    {
        Application.Quit();
    }
}

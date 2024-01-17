using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] Button mainMenuBtn, replayBtn;
    int currentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuBtn.GetComponent<Button>().onClick.AddListener(GoToMainMenu);
        replayBtn.GetComponent<Button>().onClick.AddListener(RePlayGame);
    }
    void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    void RePlayGame()
    {
        SceneManager.LoadScene(currentLevel);
    }
    // Update is called once per frame
}

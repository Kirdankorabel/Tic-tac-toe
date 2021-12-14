using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button buttonPrefab;
    public Button againstPlayer;
    public Button againstAI;
    public GameObject game;
    public GridLayoutGroup layoutGroup;
    public GameObject startMenu;
    public Text gameResult;

    public static int turn = 0;
    public static int[,] values = new int[15, 15];

    void Start()
    {
        turn = 0;
        Cell.Del del = new Cell.Del(GameOver);
        Cell.EndGame += del;

        againstPlayer.onClick.AddListener(() => 
        {
            AI.againstAI = false;
            StartGame();
        });

        againstAI.onClick.AddListener(() =>
        {
            AI.againstAI = true;
            StartGame();
        });
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        game.SetActive(true);
        if (layoutGroup.transform.childCount > 1)
        {
            for (int i = 224; i >= 0; i--)
            {
                Destroy(layoutGroup.transform.GetChild(i).gameObject);
            }
        }
        createPlayingField();
    }

    public void createPlayingField()
    {
        for (int i = 0; i < 225; i++)
        {
            Button button = Instantiate(buttonPrefab);
            button.transform.SetParent(layoutGroup.transform);
            button.GetComponent<Cell>().x = i % 15;
            button.GetComponent<Cell>().y = i / 15;
        }
        Canvas.ForceUpdateCanvases();
    }

    public void GameOver()
    {
        gameResult.text = LineFinder.GetGameResult();
        startMenu.SetActive(true);
        values = new int[15, 15];
        LineFinder.biggestStreak = 0;
    }
}

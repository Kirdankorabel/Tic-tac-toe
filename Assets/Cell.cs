using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;
    private Button button;
    private Text text;

    public delegate void Del();
    public static event Del EndGame;

    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        text = button.GetComponentInChildren<Text>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {        
         if (GameController.turn  == 226) EndGame?.Invoke();
        if (GameController.turn % 2 == 0)
        {
            GameController.values[this.x, this.y] = 1;
            text.text = "X";
            LineFinder.FindLine();
            if (LineFinder.biggestStreak == 5) EndGame?.Invoke();
        }
        else
        {
            text.text = "O";
            GameController.values[this.x, this.y] = -1;

            LineFinder.FindLine();
            if (LineFinder.biggestStreak == 5) EndGame?.Invoke();
        }
        
        GameController.turn++;
        button.interactable = false;

        if (AI.againstAI) AI.AIMove();        
    }
}

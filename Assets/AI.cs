using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    public static GridLayoutGroup layoutGroup;
    public static (int, int) lockPoint;
    public static bool againstAI = false;
    public static LinkedList<(int, int)> lockPoints;

    private void Awake()
    {
        lockPoints = new LinkedList<(int, int)>();
    }

    void Start()
    {
        int i = Random.Range(5, 10);
        int j = Random.Range(5, 10);
        lockPoint = (i, j);
        layoutGroup = this.transform.GetChild(0).GetComponent<GridLayoutGroup>();
    }

    public static void AIMove()
    {
        if (GameController.turn % 2 == 0) return;
        GetLockPoint();

        int buttonNumber = lockPoint.Item1  + lockPoint.Item2 * 15;

        Button button = layoutGroup.transform.GetChild(buttonNumber).GetComponent<Button>();

        button.onClick.Invoke();
    }

    public static void GetLockPoint()
    {
        if (GameController.values[lockPoint.Item1, lockPoint.Item2] == 0) return;
        else if (GameController.values[lockPoint.Item1, lockPoint.Item2] != 0)
        {
            if (lockPoints.Count == 0)
            {
                int i = Random.Range(5, 10);
                int j = Random.Range(5, 10);
                lockPoint = (i, j);
            }
            else
            {
                lockPoint = lockPoints.First.Value;
                lockPoints.RemoveFirst();
            }            
        }

        if (GameController.values[lockPoint.Item1, lockPoint.Item2] != 0) GetLockPoint();
    }
}

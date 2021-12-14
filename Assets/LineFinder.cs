using UnityEngine;

public class LineFinder : MonoBehaviour
{
    public static int biggestStreak; 
    public static (int, int) lockPoint;
    public static int size = 15;

    void Start()
    {
        biggestStreak = 0;
    }

    public static void FindLine()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (GameController.values[i, j] != 0)
                {
                    FindVerticalLine(i, j);
                    FindDiagonalLine1(i, j);
                    FindHorizontalLine(i, j);
                    FindDiagonalLine2(i, j);
                }
            }
        }
        Debug.Log(biggestStreak);
    }

    public static void FindVerticalLine(int i, int j)
    {
        int value = GameController.values[i, j];
        int streak = 1;
        int j2 = j;
        while (j2 + 1 < size && GameController.values[i, j2 + 1] == value)
        {
            j2++;
            streak++;
        }
        
        if (streak > 1)
        {
            if (streak > biggestStreak) biggestStreak = streak;

            if (j2 + 1 < size && GameController.values[i, j2 + 1] == 0)
                AI.lockPoints.AddFirst((i, j2 + 1));
            if (j - 1 > 0 && GameController.values[i, j - 1] == 0)
                AI.lockPoints.AddFirst((i, j - 1));
        }        
    }

    public static void FindHorizontalLine(int i, int j)
    {
        int value = GameController.values[i, j];
        int streak = 1;
        int i2 = i;
        while (i2 + 1 < size && GameController.values[i2 + 1, j] == value)
        {
            i2++;
            streak++;
        }

        if (streak > 1)
        {
            if (streak > biggestStreak) biggestStreak = streak;

            if (i2 + 1 < size && GameController.values[i2 + 1, j] == 0)
                AI.lockPoints.AddFirst((i2 + 1, j));
            else if (i - 1 > 0 && GameController.values[i - 1, j] == 0)
                AI.lockPoints.AddFirst((i - 1, j));
        }
    }

    public static void FindDiagonalLine1(int i, int j)
    {
        int value = GameController.values[i, j];
        int streak = 1;
        int i2 = i;
        int j2 = j;
        while (i2 + 1 < size && j2 + 1 < size && GameController.values[i2 + 1, j2 + 1] == value)
        {
            i2++;
            j2++;
            streak++;
        }
        if (streak > 1)
        {
            if (streak > biggestStreak) biggestStreak = streak;

            if (i2 + 1 < size && j2 + 1 < size && GameController.values[i2 + 1, j2 + 1] == 0)
                AI.lockPoints.AddFirst((i2 + 1, j2 + 1));
            else if (i - 1 > 0 && j - 1 > 0 && GameController.values[i - 1, j - 1] == 0)
                AI.lockPoints.AddFirst((i - 1, j - 1));
        }
    }

    public static void FindDiagonalLine2(int i, int j)
    {
        int value = GameController.values[i, j];
        int streak = 1;
        int i2 = i;
        int j2 = j;

        while (i2 + 1 < size && j2 > 0 && GameController.values[i2 + 1, j2 - 1] == value)
        {
            i2++;
            j2--;
            streak++;
        }
        if (streak > 1)
        {
            if (streak > biggestStreak) biggestStreak = streak;

            if (i + 2 < size && j - 2 > 0 && GameController.values[i + 2, j - 2] == 0)
                AI.lockPoints.AddFirst((i + 2, j - 2));
            if (i2 - 2 > 0 && j2 + 2 < size && GameController.values[i2 - 2, j2 + 2] == 0)
                AI.lockPoints.AddFirst((i2 - 2, j2 + 2));
        }
    }

    public static string GetGameResult()
    {
        string mes;
        if(GameController.turn == 226) mes = "ничья";
        else if(GameController.turn % 2 == 0) mes = "победил первый игрок";
        else mes = "победил второй игрок игрок";
        GameController.turn = 0;
        return mes;
    }
}

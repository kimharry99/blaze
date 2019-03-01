using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Furniture
{
    private readonly int[] thiefPreventionRate = { 0, 50, 75 };

    public int ThiefPreventionRate { get { return thiefPreventionRate[level - 1]; } }

    public void GoOutside()
    {
        SceneManager.LoadScene("Outdoor");
    }
}

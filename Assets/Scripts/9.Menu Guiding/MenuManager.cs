using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static void MenuSwitch(MenuName menuName)
    {
        switch (menuName)
        {
            case MenuName.Gameplay:
                SceneManager.LoadScene("GamePlay");
                break;
            case MenuName.Main:
                SceneManager.LoadScene("Main Menu");
                break;
        }
    }
}

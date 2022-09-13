using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        MenuManager.MenuSwitch(MenuName.Gameplay);
    }
    public void Main()
    {
        MenuManager.MenuSwitch(MenuName.Main);
    }    
}

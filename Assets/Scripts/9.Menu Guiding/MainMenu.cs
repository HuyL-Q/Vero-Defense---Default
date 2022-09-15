using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        MenuManager.MenuSwitch(MenuName.Gameplay);
    }
    public void LoadGame()
    {
        DontDestroyOnLoad(gameObject);
        MenuManager.MenuSwitch(MenuName.Gameplay);
        StartCoroutine(wait());
    }
    public void Main()
    {
        MenuManager.MenuSwitch(MenuName.Main);
    }    
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        GameController.instance.LoadData();
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    void Start()
    {
        GameObjectConverter converter = new GameObjectConverter();
        converter.setCurrentDir(@"/data.json");
        if (converter.GetText() == "")
        {
            GameObject.Find("BackGround").transform.GetChild(1).GetComponent<Button>().interactable = false;// code handle when data is null
        }
        //else
        //{

        //}
    }
}

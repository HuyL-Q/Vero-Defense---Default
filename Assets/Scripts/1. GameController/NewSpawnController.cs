using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Pool;

public class EnemyJs
{
    public string id;
    public int hp;
    public float speed;
    public int damage;
    public int killReward;
    public List<string> Effect;
}
public class NewSpawnController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> spawnStartPos;
    private int waveIndex;
    private bool isWaveEnded;
    [SerializeField]
    private int numOfEnemies;
    [SerializeField]
    private int currentNumOfEnemies;
    public GameObject[] EnemyPrefabs;
    public Transform TowerInScreen;
    public class EnemyConverter : JsonConverter<List<EnemyJs>> { }


    //private float timeBetweenWave;
    public int WaveIndex { get => waveIndex; set => waveIndex = value; }
    public bool IsWaveEnded { get => isWaveEnded; }

    public static NewSpawnController Instance { get; private set; }
    public int CurrentNumOfEnemies { get => currentNumOfEnemies; set => currentNumOfEnemies = value; }
    public int NumOfEnemies { get => numOfEnemies; set => numOfEnemies = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isWaveEnded = true;
        waveIndex = 0;
        numOfEnemies = 3;
        CurrentNumOfEnemies = NumOfEnemies;
        gameObject.AddComponent<MinionFactory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.instance.flag) return;
        if (GameController.instance.State == State.End_Defeat)
        {
            StopCoroutine(SpawnWave());
            GameObject.Find("UI").transform.GetChild(2).gameObject.SetActive(true);
            PlayerPrefs.DeleteKey("saveData");
            //yield break;
        }
        if (isWaveEnded)
        {
            StartCoroutine(SpawnWave());
            isWaveEnded = false;
        }
        if (CurrentNumOfEnemies == 0)
        {
            StartCoroutine(WaitForNewSpawn());
            numOfEnemies = waveIndex + 3;
            CurrentNumOfEnemies = numOfEnemies;
        }
    }

    IEnumerator WaitForNewSpawn()
    {
        yield return new WaitForSeconds(5f);
        isWaveEnded = true;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        List<TowerData> ls = new List<TowerData>();
        Transform tr = TowerInScreen;
        for (int i = 0; i < tr.childCount; i++)
        {
            ls.Add(new TowerData(tr.GetChild(i).gameObject));
        }
        SaveGame.Instance.ls = ls;
        SaveGame.Instance.lives = GameController.instance.PlayerLives;
        SaveGame.Instance.money = GameController.instance.PlayerMoney;
        SaveGame.Instance.playerPoint = GameController.instance.PlayerPoint;
        SaveGame.Instance.state = GameController.instance.State;
        SaveGame.Instance.stageIndex = NewSpawnController.Instance.WaveIndex - 1;
        StoryUIController.instance.UpdateWaveIndex();
        for (int i = 0; i < NumOfEnemies; i++)
        {
            gameObject.GetComponent<MinionFactory>().CreateEnemy(spawnStartPos[Random.Range(0, spawnStartPos.Count)], EnemyPrefabs[3]);// change EnemyPrefabs[0] to set Enemy data
            yield return new WaitForSeconds(.5f);
        }
        //TimeBetweenWave = 5f;
    }
}

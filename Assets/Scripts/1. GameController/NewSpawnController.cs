using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int WaveIndex { get => waveIndex; set => waveIndex = value; }
    public bool IsWaveEnded { get => isWaveEnded; }

    public static NewSpawnController Instance { get; private set; }
    public int CurrentNumOfEnemies { get => currentNumOfEnemies; set => currentNumOfEnemies = value; }
    public int NumOfEnemies { get => numOfEnemies; }

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
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        numOfEnemies = waveIndex + 2;
        yield return new WaitUntil(() => (GameController.instance.flag));
        if (GameController.instance.State == State.End_Defeat)
        {
            StopCoroutine(SpawnWave());
            yield break;
        }
        if (isWaveEnded)
        {
            StartCoroutine(SpawnWave());
            isWaveEnded = false;
        }
        if (CurrentNumOfEnemies == 0)
        {
            isWaveEnded = true;
            numOfEnemies = waveIndex + 2;
            CurrentNumOfEnemies = numOfEnemies;
            yield return new WaitForSeconds(20);
        }
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        StoryUIController.instance.UpdateWaveIndex();
        for (int i = 0; i < NumOfEnemies; i++)
        {
            gameObject.GetComponent<MinionFactory>().CreateEnemy(spawnStartPos[Random.Range(0, spawnStartPos.Count)]);
            yield return new WaitForSeconds(.5f);
        }
    }
}

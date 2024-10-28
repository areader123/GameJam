using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using DamageNumbersPro;
using SK;
using TMPro;
using UnityEngine;

public class monsterSpawner : MonoBehaviour, ISaveManager
{
    public static monsterSpawner instance;
    public List<Monster_Wave> monsterPrefabs; // �洢������˵�Ԥ����
    public float spawnInterval = 2f; // ÿ���������ɼ��
    public float spawnDistance = 1f; // ˢ�¾��루����Ļ�⣩
    public float NospawnDistance;
    [SerializeField] private float leastWaveTime;
    [SerializeField] private float restTime;
    public int currentWave = 0;
    public int listWave = 0;
    private float _restTime;
    public bool isResting;
    private bool canRest;
    private bool canBattle;
    private bool isBattling;
    private Camera mainCamera; // �������
    private List<GameObject> activeMonsters = new List<GameObject>(); // ��ǰ��Ծ�Ĺ����б�
    private int previousMonsters = 1; // 쳲��������е�ǰһ��
    private int currentMonsters = 1; // 쳲��������еĵ�ǰ��
    [SerializeField] private int perWaveAddedAmount;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Audio audioSource;
    private int totalEnenmyAmount;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    private float waveTimeCounter;
    private float restTimeCounter;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnMonsterWave());
    }

    void Update()
    {
        // ���� T ����ʼ������һ������
        if (totalEnenmyAmount == 0 && activeMonsters.Count == 0)
        {
            if (!isResting && isBattling && !canBattle)
            {
                canBattle = true;
                canRest = true;
                isBattling = false;
            }
        }
        if (canRest && !isResting && !isBattling)
        {
            canBattle = true;
            _restTime = restTime;
            //  restTime += Character_Controller.instance.GetLevel();
            textMeshProUGUI.gameObject.SetActive(true);
            audioSource.PlayMusic();
            StartCoroutine("TimeCounter");
        }
        if ((totalEnenmyAmount == 0 && activeMonsters.Count == 0) || waveTimeCounter < 0)
        {
            if (!isResting && canBattle)
            {
                textMeshProUGUI.gameObject.SetActive(false);

                StartCoroutine(SpawnMonsterWave());
            }
        }


        waveTimeCounter -= Time.deltaTime;
        restTimeCounter -= Time.deltaTime;
        // ���������ٵĹ���
        activeMonsters.RemoveAll(monster => monster == null);
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {

    }

    private IEnumerator SpawnMonsterWave()
    {

        // �ȴ� 10 ���ʼ���ɹ���
        audioSource.StopMusic();
        canBattle = false;
        isBattling = true;
        waveTimeCounter = leastWaveTime;
        totalEnenmyAmount = currentMonsters;
        for (int i = 0; i < currentMonsters; i++)
        {
            SpawnMonster();
            totalEnenmyAmount -= 1;
            if (listWave == monsterPrefabs.Count - 1)
            {
                listWave = -1;
                break;
            }
            yield return new WaitForSeconds(spawnInterval); // ÿ���������ɼ��
        }
       // Debug.Log("monsterPrefabs.Count" + monsterPrefabs.Count);
        // ������һ������������쳲��������е���һ��

    }

    private IEnumerator TimeCounter()
    {

        canRest = false;
        isResting = true;
        currentWave += 1;
        listWave += 1;
        currentMonsters += perWaveAddedAmount * currentWave;

        while (_restTime >= 0)
        {
            textMeshProUGUI.text = _restTime.ToString();
            yield return new WaitForSeconds(1f);
            _restTime -= 1f;
        }
        isResting = false;
    }

    private bool IsOutsideCameraView(Vector2 position)
    {
        // ��ȡ���������Ұ�߽�
        Vector2 screenBounds = GetScreenBounds();
        Vector2 cameraPosition = mainCamera.transform.position;

        // �������λ���Ƿ������������Ұ��
        return position.x < cameraPosition.x - screenBounds.x ||
               position.x > cameraPosition.x + screenBounds.x ||
               position.y < cameraPosition.y - screenBounds.y ||
               position.y > cameraPosition.y + screenBounds.y;
    }

    private void SpawnMonster()
    {
      //  Vector2 screenBounds = GetScreenBounds();
        Vector2 spawnPosition;

        // ��֤��������Ļ������
        //do
        //{
            spawnPosition = GetRandomSpawnPosition();
        //}
       // while (!IsOutsideCameraView(spawnPosition));

        // ���ѡ�����Ԥ����
        int randomIndex = Random.Range(0, monsterPrefabs[listWave].monsters.Count);
        GameObject selectedMonsterPrefab = monsterPrefabs[listWave].monsters[randomIndex];

        // ʵ�������ﲢ���ӵ���Ծ�����б�
        GameObject monster = Instantiate(selectedMonsterPrefab, spawnPosition, Quaternion.identity);
        activeMonsters.Add(monster);
    }


    private Vector2 GetScreenBounds()
    {
        // ������Ļ���������귶Χ
        Camera camera = Camera.main;
        float screenHeight = camera.orthographicSize * 2;
        float screenWidth = screenHeight * camera.aspect;

        return new Vector2(screenWidth / 2, screenHeight / 2);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // ���ѡ����Ļ��ķ���
        int side = Random.Range(0, 4); // 0:��, 1:��, 2:��, 3:��
        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: // �Ϸ�
                spawnPosition = new Vector2(Random.Range(-(transform.position.x+NospawnDistance/2), -(transform.position.x-spawnDistance/2)), Random.Range(transform.position.y-spawnDistance/2, transform.position.y+ spawnDistance/2));
                break;
            case 1: // �·�
                spawnPosition = new Vector2(Random.Range((transform.position.x+NospawnDistance/2), transform.position.x+spawnDistance/2), Random.Range(transform.position.y-spawnDistance/2, transform.position.y+ spawnDistance/2));
                break;
            case 2: // ���
                spawnPosition = new Vector2(Random.Range(transform.position.x-spawnDistance/2, transform.position.x+ spawnDistance/2), Random.Range(-(transform.position.y+NospawnDistance/2), -(transform.position.y+spawnDistance/2)));
                break;
            case 3: // �Ҳ�
                spawnPosition = new Vector2(Random.Range(transform.position.x-spawnDistance/2, transform.position.x+ spawnDistance/2), Random.Range((transform.position.y+NospawnDistance/2), transform.position.y+spawnDistance/2));
                break;
        }


        return spawnPosition;
    }

    public void LoadData(GameData _data)
    {
        if (SaveGame.Exists("CurrentWave"))
        {
            currentWave = SaveGame.Load<int>("CurrentWave");
        }
        if (SaveGame.Exists("ListWave"))
        {
            listWave = SaveGame.Load<int>("ListWave");
        }
        if (SaveGame.Exists("currentMonsters"))
        {
            currentMonsters = SaveGame.Load<int>("currentMonsters");
        }

    }

    public void SaveData(ref GameData _data)
    {
        SaveGame.Save("CurrentWave", currentWave);
        SaveGame.Save("ListWave", listWave);
        SaveGame.Save("currentMonsters", currentMonsters);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(NospawnDistance,NospawnDistance, 0.01f));
        Gizmos.DrawWireCube(transform.position,new Vector3( spawnDistance,spawnDistance,0.01f));

    }
}

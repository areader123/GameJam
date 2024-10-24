using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
using SK;
using TMPro;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    public static monsterSpawner instance;
    public GameObject[] monsterPrefabs; // �洢������˵�Ԥ����
    public float spawnInterval = 2f; // ÿ���������ɼ��
    public float spawnDistance = 1f; // ˢ�¾��루����Ļ�⣩
    [SerializeField] private float leastWaveTime;
    [SerializeField] private float restTime;
    private float _restTime;
    public bool isResting;
    public bool canRest;
    public bool canBattle;
    public bool isBattling;
    private Camera mainCamera; // �������
    private List<GameObject> activeMonsters = new List<GameObject>(); // ��ǰ��Ծ�Ĺ����б�
    private int previousMonsters = 1; // 쳲��������е�ǰһ��
    private int currentMonsters = 1; // 쳲��������еĵ�ǰ��
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

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
        if (activeMonsters.Count == 0)
        {
            if (!isResting && isBattling && !canBattle)
            {
                canBattle =true;
                canRest = true;
                isBattling = false;
            }
        }
        if (canRest && !isResting && !isBattling)
        {
            canBattle = true;
            _restTime = restTime;
            textMeshProUGUI.gameObject.SetActive(true);
            StartCoroutine("TimeCounter");
        }
        if (activeMonsters.Count == 0 || waveTimeCounter < 0)
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
        canBattle = false;
        isBattling = true;
        waveTimeCounter = leastWaveTime;
        for (int i = 0; i < currentMonsters; i++)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnInterval); // ÿ���������ɼ��
        }
        // ������һ������������쳲��������е���һ��
        int nextMonsters = previousMonsters + currentMonsters;
        previousMonsters = currentMonsters;
        currentMonsters = nextMonsters;
    
    }

    private IEnumerator TimeCounter()
    {
        canRest = false;
        isResting = true;

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
        Vector2 screenBounds = GetScreenBounds();
        Vector2 spawnPosition;

        // ��֤��������Ļ������
        do
        {
            spawnPosition = GetRandomSpawnPosition(screenBounds);
        }
        while (!IsOutsideCameraView(spawnPosition));

        // ���ѡ�����Ԥ����
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject selectedMonsterPrefab = monsterPrefabs[randomIndex];

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

    private Vector2 GetRandomSpawnPosition(Vector2 screenBounds)
    {
        // ���ѡ����Ļ��ķ���
        int side = Random.Range(0, 4); // 0:��, 1:��, 2:��, 3:��
        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: // �Ϸ�
                spawnPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + spawnDistance);
                break;
            case 1: // �·�
                spawnPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y - spawnDistance);
                break;
            case 2: // ���
                spawnPosition = new Vector2(-screenBounds.x - spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
                break;
            case 3: // �Ҳ�
                spawnPosition = new Vector2(screenBounds.x + spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
                break;
        }

        return spawnPosition;
    }



}

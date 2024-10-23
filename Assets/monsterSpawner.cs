using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // �洢������˵�Ԥ����
    public float spawnInterval = 2f; // ÿ���������ɼ��
    public float spawnDistance = 1f; // ˢ�¾��루����Ļ�⣩
    public float replenishDelay = 10f; // ���������ӳ�ʱ��

    private Camera mainCamera; // �������
    private List<GameObject> activeMonsters = new List<GameObject>(); // ��ǰ��Ծ�Ĺ����б�
    private int previousMonsters = 1; // 쳲��������е�ǰһ��
    private int currentMonsters = 1; // 쳲��������еĵ�ǰ��

    void Start()
    {
        mainCamera = Camera.main; // ��ȡ�������
    }

    void Update()
    {
        // ���� T ����ʼ������һ������
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(SpawnMonsterWave());
        }

        // ���������ٵĹ���
        activeMonsters.RemoveAll(monster => monster == null);
    }

    private IEnumerator SpawnMonsterWave()
    {
        yield return new WaitForSeconds(replenishDelay); // �ȴ� 10 ���ʼ���ɹ���

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

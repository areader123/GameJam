using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // �洢������˵�Ԥ����
    public float spawnInterval = 2f; // ˢ�¼��
    public float spawnDistance = 1f; // ˢ�¾��루����Ļ�⣩

    private Camera mainCamera; // �������
    private List<GameObject> activeMonsters = new List<GameObject>(); // ��ǰ��Ծ�ĵ����б�
    public int maxMonsters = 5; // ����������

    void Start()
    {
        mainCamera = Camera.main; // ��ȡ�������
        StartCoroutine(SpawnMonsters()); // ������������
    }

    private IEnumerator SpawnMonsters()
    {
        while (true) //�ɸı�ˢ������
        {
            if (activeMonsters.Count < maxMonsters) // ��鵱ǰ��������
            {
                SpawnMonster(); // ˢ�¹���
            }
            yield return new WaitForSeconds(spawnInterval); // �ȴ�ָ��ʱ��
        }
    }

    private void SpawnMonster()
    {
        // ��ȡ��Ļ�߽�
        Vector2 screenBounds = GetScreenBounds();

        // ���ѡ����Ļ��ı߽�λ��
        Vector2 spawnPosition = GetRandomSpawnPosition(screenBounds);

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

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(GetScreenBounds().x * 2,GetScreenBounds().y * 2,0.01f));
        Gizmos.DrawWireCube(transform.position,new Vector3((GetScreenBounds().x -spawnDistance ) * 2,(GetScreenBounds().y-spawnDistance)* 2,0.01f));
        
    }

}

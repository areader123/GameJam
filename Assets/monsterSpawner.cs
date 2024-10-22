using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // 存储多个敌人的预制体
    public float spawnInterval = 2f; // 每个怪物生成间隔
    public float spawnDistance = 1f; // 刷新距离（在屏幕外）
    public float replenishDelay = 10f; // 补充怪物的延迟时间

    private Camera mainCamera; // 主摄像机
    private List<GameObject> activeMonsters = new List<GameObject>(); // 当前活跃的怪物列表
    private int previousMonsters = 1; // 斐波那契数列的前一项
    private int currentMonsters = 1; // 斐波那契数列的当前项

    void Start()
    {
        mainCamera = Camera.main; // 获取主摄像机
    }

    void Update()
    {
        // 按下 T 键开始生成下一波怪物
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(SpawnMonsterWave());
        }

        // 清理已销毁的怪物
        activeMonsters.RemoveAll(monster => monster == null);
    }

    private IEnumerator SpawnMonsterWave()
    {
        yield return new WaitForSeconds(replenishDelay); // 等待 10 秒后开始生成怪物

        for (int i = 0; i < currentMonsters; i++)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnInterval); // 每个怪物生成间隔
        }

        // 计算下一波怪物数量：斐波那契数列的下一项
        int nextMonsters = previousMonsters + currentMonsters;
        previousMonsters = currentMonsters;
        currentMonsters = nextMonsters;
    }

    private bool IsOutsideCameraView(Vector2 position)
    {
        // 获取摄像机的视野边界
        Vector2 screenBounds = GetScreenBounds();
        Vector2 cameraPosition = mainCamera.transform.position;

        // 检查生成位置是否在摄像机的视野外
        return position.x < cameraPosition.x - screenBounds.x ||
               position.x > cameraPosition.x + screenBounds.x ||
               position.y < cameraPosition.y - screenBounds.y ||
               position.y > cameraPosition.y + screenBounds.y;
    }

    private void SpawnMonster()
    {
        Vector2 screenBounds = GetScreenBounds();
        Vector2 spawnPosition;

        // 保证怪物在屏幕外生成
        do
        {
            spawnPosition = GetRandomSpawnPosition(screenBounds);
        }
        while (!IsOutsideCameraView(spawnPosition));

        // 随机选择敌人预制体
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject selectedMonsterPrefab = monsterPrefabs[randomIndex];

        // 实例化怪物并添加到活跃怪物列表
        GameObject monster = Instantiate(selectedMonsterPrefab, spawnPosition, Quaternion.identity);
        activeMonsters.Add(monster);
    }


    private Vector2 GetScreenBounds()
    {
        // 计算屏幕的世界坐标范围
        Camera camera = Camera.main;
        float screenHeight = camera.orthographicSize * 2;
        float screenWidth = screenHeight * camera.aspect;

        return new Vector2(screenWidth / 2, screenHeight / 2);
    }

    private Vector2 GetRandomSpawnPosition(Vector2 screenBounds)
    {
        // 随机选择屏幕外的方向
        int side = Random.Range(0, 4); // 0:上, 1:下, 2:左, 3:右
        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: // 上方
                spawnPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + spawnDistance);
                break;
            case 1: // 下方
                spawnPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y - spawnDistance);
                break;
            case 2: // 左侧
                spawnPosition = new Vector2(-screenBounds.x - spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
                break;
            case 3: // 右侧
                spawnPosition = new Vector2(screenBounds.x + spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
                break;
        }

        return spawnPosition;
    }

}

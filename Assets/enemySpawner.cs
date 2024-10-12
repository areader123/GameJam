using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // 存储多个敌人的预制体
    public float spawnInterval = 2f; // 刷新间隔
    public float spawnDistance = 1f; // 刷新距离（在屏幕外）

    private Camera mainCamera; // 主摄像机
    private List<GameObject> activeMonsters = new List<GameObject>(); // 当前活跃的敌人列表
    public int maxMonsters = 5; // 最大敌人数量

    void Start()
    {
        mainCamera = Camera.main; // 获取主摄像机
        StartCoroutine(SpawnMonsters()); // 启动怪物生成
    }

    private IEnumerator SpawnMonsters()
    {
        while (true) //可改变刷怪条件
        {
            if (activeMonsters.Count < maxMonsters) // 检查当前敌人数量
            {
                SpawnMonster(); // 刷新怪物
            }
            yield return new WaitForSeconds(spawnInterval); // 等待指定时间
        }
    }

    private void SpawnMonster()
    {
        // 获取屏幕边界
        Vector2 screenBounds = GetScreenBounds();

        // 随机选择屏幕外的边界位置
        Vector2 spawnPosition = GetRandomSpawnPosition(screenBounds);

        // 随机选择敌人预制体
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject selectedMonsterPrefab = monsterPrefabs[randomIndex];

        // 实例化怪物并添加到活跃敌人列表
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

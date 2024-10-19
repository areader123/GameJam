using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;
using UnityEngine.Rendering.Universal;
//召唤 或者 给友军加buff
public class Enemy_Roar_Skill : Skill
{
    [SerializeField]
    private enum TypeSkill
    {
        call, buff, both
    }
    [SerializeField] private List<GameObject> prefabCalledList;
    [SerializeField] private float intializeAmount;
    [SerializeField] private Transform intialzeTransform;
    [SerializeField] private float intialzaDistance;
    [SerializeField] TypeSkill callOrBuff;
    [SerializeField] private GameObject prefabBuff;
    [SerializeField] private Transform buffCenter;
    [SerializeField] private float buffRange;
    [SerializeField]private float increaseSpeed;
    [SerializeField]private float bufferExistTime;
    [SerializeField]private float bufferPrefabExistTime;

    public override void UseSkill()
    {
        base.UseSkill();
        UseRoarSkill();
    }

    private void UseRoarSkill()
    {
        if (callOrBuff == TypeSkill.call || callOrBuff == TypeSkill.both)
        {
            for (int i = 0; i < intializeAmount; i++)
            {
                GameObject prefab = prefabCalledList[Random.Range(0,prefabCalledList.Count)];
                GameObject gameObject = Instantiate(prefab, RandomIntialize(), Quaternion.identity);
                gameObject.GetComponent<Enemy_Roar_Skill_Controller>().SetUpCall(gameObject.GetComponent<Enemy>(),increaseSpeed);
            }

        }
        if (callOrBuff == TypeSkill.buff || callOrBuff == TypeSkill.both)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(buffCenter.position, buffRange);
            //为每个角色创造buffprefab
            foreach (var hit in colliders)
            {
                if(hit.GetComponent<Enemy_Stat>() != null)
                {
                    GameObject gameObject= Instantiate(prefabBuff,hit.transform);
                    gameObject.GetComponent<Enemy_Roar_Skill_Controller>().SetUpBuffs(hit.GetComponent<Enemy>(),increaseSpeed,bufferExistTime,bufferPrefabExistTime);
                }
            }

            //只创造一个buffprefab
           // GameObject oneBuff= Instantiate(prefabBuff,buffCenter);

        }

    }

    private Vector3 RandomIntialize()
    {
        float randomX = Random.Range(intialzeTransform.position.x - intialzaDistance, intialzeTransform.position.x + intialzaDistance);
        float randomY = Random.Range(intialzeTransform.position.y - intialzaDistance, intialzeTransform.position.y + intialzaDistance);
        return new Vector3(randomX, randomY, 0);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(intialzeTransform.position, new Vector3(intialzaDistance * 2, intialzaDistance * 2, 0));
    }

}



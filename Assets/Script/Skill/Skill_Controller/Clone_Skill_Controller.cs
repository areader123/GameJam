using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public class Clone_Skill_Controller : MonoBehaviour
    {
        private SpriteRenderer sr;
        private float cloneTImer;
        [SerializeField] private float colorLosingSpeed;
        public void SetUpClone(Transform _newTransform, float _cloneDuration,Vector3 _offset)
        {
            transform.position = _newTransform.position + _offset;
            cloneTImer = _cloneDuration;
        }


        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            cloneTImer -= Time.deltaTime;
            if (cloneTImer < 0)
            {
                sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));
                if (sr.color.a < 0)
                {
                    //���������prefab�� ���Զ�Ӧ�����ĸ��� �����������ͳ�ʼ��
                    Destroy(gameObject);
                }
            }
        }
    }
}


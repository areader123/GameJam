using System.Collections;
using System.Collections.Generic;
using SK;
using UnityEngine;

namespace SK
{

    public class Character_Controller : MonoBehaviour
    {
        public static Character_Controller instance;

        public Character character;
        //此处为光亮值
        public int currency;

        private void Awake()
        {
            //�����������ظ���instance
            //ֱ������gameobject ��Ҫʱ�ٴ��� 
            if (instance != null)
            {
                Destroy(instance.gameObject);
            }
            else
            {
                instance = this;
            }

        }
        public int GetCurrency() => currency;

        public bool HaveEnoughcurrnecy(int _price)
        {
            if (currency < _price)
            {
                return false;
            }
            else
            {
                currency -= _price;
                return true;
            }
        }

        //数据保存和加载
        // public void LoadData(GameData _data)
        // {
        //     this.currency = _data.currency;
        // }

        // public void SaveData(ref GameData _data)
        // {
        //     _data.currency = this.currency;
        // }
    }
}

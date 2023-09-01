using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QFramework.Car
{
    public class PlayerWeapon
    {
        private readonly List<string> paths = new List<string>()
        {
            "Pistol",
            "Uzi",
            "Shotgun",
            "RocketLauncher"
        };

        private Transform m_weaponRoot;
        private Dictionary<string, List<GameObject>> m_currentWeapons = new Dictionary<string, List<GameObject>>();
        public EasyEvent<string> WeaponFull = new EasyEvent<string>();

        public void Init(Transform weaponRoot)
        {
            this.m_weaponRoot = weaponRoot;
        }


        public bool Add(int index)
        {
            return Add(ResLoader.Allocate().LoadSync<GameObject>(paths[index]));
        }

        /// <param name="path"></param>
        /// <returns>��ӳɹ�����true����������false</returns>
        public bool Add(string path)
        {
            return Add(ResLoader.Allocate().LoadSync<GameObject>(path));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weaponPrefab"></param>
        /// <returns>��ӳɹ�����true����������false</returns>
        public bool Add(GameObject weaponPrefab)
        {
            bool res = false;

            for (int i = 0; i < m_weaponRoot.childCount; i++)
            {
                if(m_weaponRoot.GetChild(i).childCount == 0)
                {
                    var go = GameObject.Instantiate(weaponPrefab, m_weaponRoot.GetChild(i));
                    if (!m_currentWeapons.ContainsKey(weaponPrefab.name))
                    {
                        m_currentWeapons[weaponPrefab.name] = new List<GameObject>();
                    }
                    m_currentWeapons[weaponPrefab.name].Add(go);
                    res = true;
                    break;
                }
            }

            //�����λ�Ƿ���
            bool weaponFull = true;
            for (int i = 0; i < m_weaponRoot.childCount; i++)
            {
                if (m_weaponRoot.GetChild(i).childCount == 0)
                {
                    weaponFull = false;
                    break;
                }
            }
            if(weaponFull)
                WeaponFull.Trigger("װ����������λ����");

            return res;
        }


        public void Remove(int index)
        {
            Remove(paths[index]);
        }

        public void Remove(string path)
        {
            if (m_currentWeapons.ContainsKey(path))
            {
                var delGo = m_currentWeapons[path][0];
                m_currentWeapons[path].RemoveAt(0);
                GameObject.Destroy(delGo);
                if (m_currentWeapons[path].Count == 0)
                {
                    m_currentWeapons[path] = null;
                    m_currentWeapons.Remove(path);
                }
            }
            else
            {
                Debug.LogWarning("�Ѿ�ж�¸�����:" + path);
            }
        }


    }
}
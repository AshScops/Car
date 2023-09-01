
using UnityEngine;
using UnityEngine.U2D;

namespace QFramework.Car
{

    public class ResUtil
    {
        private static ResLoader m_resLoader = ResLoader.Allocate();

        public static GameObject GenerateGO(string path, Vector3 pos)
        {
            GameObject prefab = m_resLoader.LoadSync<GameObject>(path);
            return GameObject.Instantiate(prefab, pos, Quaternion.identity);
        }

        public static GameObject GenerateGO(string path, Transform parent)
        {
            GameObject prefab = m_resLoader.LoadSync<GameObject>(path);
            return GameObject.Instantiate(prefab, parent);
        }
        public static Sprite LoadSprite(string path) 
        {
            return m_resLoader.LoadSync<Sprite>(path);
        }
        public static Texture2D LoadTexture2D(string path)
        {
            return m_resLoader.LoadSync<Texture2D>(path);
        }
             

    }
}
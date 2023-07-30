
using UnityEngine;
using UnityEngine.U2D;

namespace QFramework.Car
{

    public class ResUtil
    {

        public static GameObject GenerateGO(string path, Vector3 pos)
        {
            GameObject prefab = ResLoader.Allocate().LoadSync<GameObject>(path);
            return GameObject.Instantiate(prefab, pos, Quaternion.identity);
        }

        public static GameObject GenerateGO(string path, Transform parent)
        {
            GameObject prefab = ResLoader.Allocate().LoadSync<GameObject>(path);
            return GameObject.Instantiate(prefab, parent);
        }
        public static Sprite LoadSprite(string path) 
        {
            return ResLoader.Allocate().LoadSync<Sprite>(path);
        }
        public static Texture2D LoadTexture2D(string path)
        {
            return ResLoader.Allocate().LoadSync<Texture2D>(path);
        }

    }
}
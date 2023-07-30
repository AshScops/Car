using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class JsonUtil
    {

        public static void InitJsonData<T>(string fileName,ref List<T> list)
        {
            TextAsset jsonFile = ResLoader.Allocate().LoadSync<TextAsset>(fileName);
            var json = jsonFile.text;

            //JsonUtility不能解析数组
            //m_allPowerData = JsonUtility.FromJson<List<PowerData>>(powerJson);
            list = JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
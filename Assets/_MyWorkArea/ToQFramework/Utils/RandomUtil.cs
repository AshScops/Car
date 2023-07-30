using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace QFramework.Car
{
    public static class RandomUtil
    {
        /// <summary>
        /// 获取基地址的
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static int GetMemory(object o)
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);
            IntPtr addr = GCHandle.ToIntPtr(h);
            return int.Parse(addr.ToString());
        }

        /// <summary>
        /// 产生随机数
        /// 调用它就可以产生你要的随机数了,如果有需求可以自己重载
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="Max">最大值</param>
        /// <returns></returns>
        public static float GetRandom(int min, int Max, int iSeed)
        {
            System.Random rd = new System.Random(GetMemory(iSeed));
            return (rd.Next(min, Max));
        }

        /// <summary>
        /// 传入概率数组，返回选择第几个项
        /// </summary>
        /// <param name="probs"></param>
        /// <returns></returns>
        public static int RandomChoose(float[] probs)
        {
            float total = 0;
            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = UnityEngine.Random.value * total;
            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }

            return probs.Length - 1;
        }

        /// <summary>
        /// 随机洗牌
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="deck">迭代器</param>
        public static void Shuffle<T>(ref List<T> deck)
        {
            for (int i = 0; i < deck.Count; i++)
            {
                T temp = deck[i];
                int randomIndex = UnityEngine.Random.Range(i, deck.Count);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }

    }
}
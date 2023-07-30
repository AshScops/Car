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
        /// ��ȡ����ַ��
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
        /// ���������
        /// �������Ϳ��Բ�����Ҫ���������,�������������Լ�����
        /// </summary>
        /// <param name="min">��Сֵ</param>
        /// <param name="Max">���ֵ</param>
        /// <returns></returns>
        public static float GetRandom(int min, int Max, int iSeed)
        {
            System.Random rd = new System.Random(GetMemory(iSeed));
            return (rd.Next(min, Max));
        }

        /// <summary>
        /// ����������飬����ѡ��ڼ�����
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
        /// ���ϴ��
        /// </summary>
        /// <typeparam name="T">Ԫ������</typeparam>
        /// <param name="deck">������</param>
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
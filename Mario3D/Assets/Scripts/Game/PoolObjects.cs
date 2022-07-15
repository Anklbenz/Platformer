using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public sealed class PoolObjects<T> where T : MonoBehaviour
    {
        private List<T> pool;
        private bool canExpand = false;
        private Transform parentContainer;
        private T prefab;

        //public PoolObjects(T prefab, int poolAmount, bool canExpand)
        //{
        //    this.canExpand = canExpand;
        //    parentContainer = null;

        //    CreatePool(poolAmount);

        //}
        public PoolObjects(T prefab, int poolAmount, bool canExpand, Transform parentContainer)
        {
            this.canExpand = canExpand;
            this.parentContainer = parentContainer;
            this.prefab = prefab;

            CreatePool(poolAmount);
        }

        private void CreatePool(int poolAmount)
        {
            pool = new List<T>();

            for (int i = 0; i < poolAmount; i++)
                CreateElement();
        }

        private T CreateElement(bool isActiveAsDefault = false)
        {
            var createdObj = UnityEngine.Object.Instantiate(prefab, parentContainer);
            createdObj.gameObject.SetActive(isActiveAsDefault);
            pool.Add(createdObj);
            return createdObj;
        }


        public bool HasFreeElement(out T element)
        {
            foreach (var obj in pool)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    element = obj;
                    element.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;

            if (canExpand)
                return CreateElement(true);

            throw new Exception($"в пуле закончились {typeof(T)}");
        }
    }
}
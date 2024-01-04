using DatabaseSystem.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DatabaseSystem.Managers {
    public class EnemyDataManager : DataManager<int, Enemy> {
        #region Variables
        [SerializeField] private string resourcesItemsFolder = default;
        #endregion

        #region Private Methods
        private void LoadFromResources()
        {
            dataDictionary = new Dictionary<int, Enemy>();
            Enemy[] enemiesFromResources = Resources.LoadAll<Enemy>(resourcesItemsFolder);
            foreach (var enemy in enemiesFromResources)
            {
                TryPutDataItem(enemy.id, enemy);
            }
        }
        private Dictionary<int, Enemy> SortDictionaryByKey()
        {
            var sortedList = dataDictionary.ToList();
            sortedList.Sort((x, y) => x.Key.CompareTo(y.Key));
            return sortedList.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            LoadFromResources();
            dataDictionary = SortDictionaryByKey();
        }

        #endregion
    }
}
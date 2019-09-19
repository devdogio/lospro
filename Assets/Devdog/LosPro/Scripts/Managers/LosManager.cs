using UnityEngine;
using System.Collections;

namespace Devdog.LosPro
{
    [AddComponentMenu(LosPro.AddComponentMenuPath + "Los Manager", -20)]
    public class LosManager : MonoBehaviour
    {
        public LosSettings settings;

        private static LosManager _instance;
        public static LosManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LosManager>();

                    if (_instance == null)
                    {
                        Debug.LogWarning("No instance of " + typeof(LosManager).Name + " in the scene, yet it is requested. (one will be created)");
                        var obj = new GameObject("_Managers");
                        _instance = obj.AddComponent<LosManager>();
                    }
                }

                return _instance;
            }
        }
    }
}
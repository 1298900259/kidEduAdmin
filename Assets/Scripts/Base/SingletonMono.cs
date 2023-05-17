using UnityEngine;

namespace App.Base
{
    public class SingletonMono<T>:MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                return instance;
            }
        }

        public virtual void Awake()
        {
            instance = this as T;
        }
    }

}

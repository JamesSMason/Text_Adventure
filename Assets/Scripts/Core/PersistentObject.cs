using UnityEngine;

namespace Adventure.Core
{
    public class PersistentObject : MonoBehaviour
    {
        private void Awake()
        {
            PersistentObject[] persistentObject = FindObjectsOfType<PersistentObject>();
            if (persistentObject.Length > 0)
            {
                Destroy(this);
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }
    }
}
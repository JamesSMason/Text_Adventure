using UnityEngine;

namespace Adventure.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            else if (Input.GetKeyUp(KeyCode.L))
            {
                Load();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            StartCoroutine(GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile));
        }
    }
}
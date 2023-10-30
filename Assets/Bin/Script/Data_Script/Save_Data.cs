using UnityEngine;

namespace magic
{
    public class Save_Data : MonoBehaviour
    {
        [SerializeField, Header("技能池資料")]
        Skill_Pool_Data skill_pool_data;
        [SerializeField, Header("狀態資料")]
        State_Data state_data;

        private void Awake()
        {
            int saveSingleton = FindObjectsOfType<Save_Data>().Length; 
            if(saveSingleton >1)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

}

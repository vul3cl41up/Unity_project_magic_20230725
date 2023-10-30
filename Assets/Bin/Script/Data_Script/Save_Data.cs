using UnityEngine;

namespace magic
{
    public class Save_Data : MonoBehaviour
    {
        [SerializeField, Header("�ޯ�����")]
        Skill_Pool_Data skill_pool_data;
        [SerializeField, Header("���A���")]
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

using Fungus;
using TMPro;
using UnityEngine;
namespace Model_scene_3
{
    public class Level_Control : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text level_text;
        [SerializeField]
        private TMP_Text level_now_text;

        [SerializeField, Header("對話管理")]
        private Flowchart fungusGM;

        int level_now = 0;
        float timer = 0;


        private void Start()
        {
            level_text.text = transform.childCount.ToString();
            transform.GetChild(0).gameObject.SetActive(true);
            level_now++;
            level_now_text.text = level_now.ToString();
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= 20 && transform.childCount >0) 
            {
                timer = 0;
                transform.GetChild(0).gameObject.SetActive(true);
                level_now++;
                level_now_text.text = level_now.ToString();
            }


            if(transform.childCount == 0 && Enemy_Base_Control.Enemy_Number == 0)
            {
                fungusGM.SendFungusMessage("Win");
            }
        }
    }
}


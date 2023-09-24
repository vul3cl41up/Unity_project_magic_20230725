
using UnityEditor.MPE;
using UnityEngine;

namespace Model_scene_3
{
    public class HP_Contorl : MonoBehaviour
    {
        GameObject HP_0;
        GameObject HP_1;
        GameObject HP_2;
        GameObject HP_3;
        GameObject HP_4;
        GameObject HP_5;
        GameObject HP_6;
        GameObject HP_7;
        GameObject HP_8;
        GameObject HP_9;
        GameObject HP_10;

        [SerializeField]
        Character_Data current_character_data;

        private void Start()
        {
            HP_0 = transform.GetChild(0).gameObject;
            HP_1 = transform.GetChild(1).gameObject;
            HP_2 = transform.GetChild(2).gameObject;
            HP_3 = transform.GetChild(3).gameObject;
            HP_4 = transform.GetChild(4).gameObject;
            HP_5 = transform.GetChild(5).gameObject;
            HP_6 = transform.GetChild(6).gameObject;
            HP_7 = transform.GetChild(7).gameObject;
            HP_8 = transform.GetChild(8).gameObject;
            HP_9 = transform.GetChild(9).gameObject;
            HP_10 = transform.GetChild(10).gameObject;

        }

        private void Update()
        {
            if (current_character_data.blood_now / current_character_data.blood < 0.1)
            {
                if (HP_1.activeSelf)
                    HP_1.SetActive(false);
                if (HP_10.activeSelf)
                    HP_10.SetActive(false);
                if (HP_9.activeSelf)
                    HP_9.SetActive(false);
                if (HP_8.activeSelf)
                    HP_8.SetActive(false);
                if (HP_7.activeSelf)
                    HP_7.SetActive(false);
                if (HP_6.activeSelf)
                    HP_6.SetActive(false);
                if (HP_5.activeSelf)
                    HP_5.SetActive(false);
                if (HP_4.activeSelf)
                    HP_4.SetActive(false);
                if (HP_3.activeSelf)
                    HP_3.SetActive(false);
                if (HP_2.activeSelf)
                    HP_2.SetActive(false);

            }
            if (current_character_data.blood_now / current_character_data.blood >= 0.1)
            {
                if (HP_10.activeSelf)
                    HP_10.SetActive(false);
                if (HP_9.activeSelf)
                    HP_9.SetActive(false);
                if (HP_8.activeSelf)
                    HP_8.SetActive(false);
                if (HP_7.activeSelf)
                    HP_7.SetActive(false);
                if (HP_6.activeSelf)
                    HP_6.SetActive(false);
                if (HP_5.activeSelf)
                    HP_5.SetActive(false);
                if (HP_4.activeSelf)
                    HP_4.SetActive(false);
                if (HP_3.activeSelf)
                    HP_3.SetActive(false);
                if (HP_2.activeSelf)
                    HP_2.SetActive(false);

                if (!HP_1.activeSelf)
                    HP_1.SetActive(true);
            }
            if (current_character_data.blood_now / current_character_data.blood >= 0.2)
            {
                if (HP_10.activeSelf)
                    HP_10.SetActive(false);
                if (HP_9.activeSelf)
                    HP_9.SetActive(false);
                if (HP_8.activeSelf)
                    HP_8.SetActive(false);
                if (HP_7.activeSelf)
                    HP_7.SetActive(false);
                if (HP_6.activeSelf)
                    HP_6.SetActive(false);
                if (HP_5.activeSelf)
                    HP_5.SetActive(false);
                if (HP_4.activeSelf)
                    HP_4.SetActive(false);
                if (HP_3.activeSelf)
                    HP_3.SetActive(false);


                if (!HP_2.activeSelf)
                    HP_2.SetActive(true);
                if (!HP_1.activeSelf)
                    HP_1.SetActive(true);
            }
            if (current_character_data.blood_now / current_character_data.blood >= 0.3)
            {
                if (HP_10.activeSelf)
                    HP_10.SetActive(false);
                if (HP_9.activeSelf)
                    HP_9.SetActive(false);
                if (HP_8.activeSelf)
                    HP_8.SetActive(false);
                if (HP_7.activeSelf)
                    HP_7.SetActive(false);
                if (HP_6.activeSelf)
                    HP_6.SetActive(false);
                if (HP_5.activeSelf)
                    HP_5.SetActive(false);
                if (HP_4.activeSelf)
                    HP_4.SetActive(false);


                if (!HP_3.activeSelf)
                    HP_3.SetActive(true);
                if (!HP_2.activeSelf)
                    HP_2.SetActive(true);
                if (!HP_1.activeSelf)
                    HP_1.SetActive(true);
            }
            if (current_character_data.blood_now / current_character_data.blood >= 0.4)
            {
                if (HP_10.activeSelf)
                    HP_10.SetActive(false);
                if (HP_9.activeSelf)
                    HP_9.SetActive(false);
                if (HP_8.activeSelf)
                    HP_8.SetActive(false);
                if (HP_7.activeSelf)
                    HP_7.SetActive(false);
                if (HP_6.activeSelf)
                    HP_6.SetActive(false);
                if (HP_5.activeSelf)
                    HP_5.SetActive(false);


                if (!HP_4.activeSelf)
                    HP_4.SetActive(true);
                if (!HP_3.activeSelf)
                    HP_3.SetActive(true);
                if (!HP_2.activeSelf)
                    HP_2.SetActive(true);
                if (!HP_1.activeSelf)
                    HP_1.SetActive(true);
            }
            if (current_character_data.blood_now / current_character_data.blood >= 0.5)
            {
                if (HP_10.activeSelf)
                    HP_10.SetActive(false);
                if (HP_9.activeSelf)
                    HP_9.SetActive(false);
                if (HP_8.activeSelf)
                    HP_8.SetActive(false);
                if (HP_7.activeSelf)
                    HP_7.SetActive(false);
                if (HP_6.activeSelf)
                    HP_6.SetActive(false);


                if (!HP_5.activeSelf)
                    HP_5.SetActive(true);
                if (!HP_4.activeSelf)
                    HP_4.SetActive(true);
                if (!HP_3.activeSelf)
                    HP_3.SetActive(true);
                if (!HP_2.activeSelf)
                    HP_2.SetActive(true);
                if (!HP_1.activeSelf)
                    HP_1.SetActive(true);
            }
            if (current_character_data.blood_now / current_character_data.blood >= 0.6)
            {
                if (HP_10.activeSelf)
                    HP_10.SetActive(false);
                if (HP_9.activeSelf)
                    HP_9.SetActive(false);
                if (HP_8.activeSelf)
                    HP_8.SetActive(false);
                if (HP_7.activeSelf)
                    HP_7.SetActive(false);

                if (!HP_6.activeSelf)
                    HP_6.SetActive(true);
                if (!HP_5.activeSelf)
                    HP_5.SetActive(true);
                if (!HP_4.activeSelf)
                    HP_4.SetActive(true);
                if (!HP_3.activeSelf)
                    HP_3.SetActive(true);
                if (!HP_2.activeSelf)
                    HP_2.SetActive(true);
                if (!HP_1.activeSelf)
                    HP_1.SetActive(true);
            }
            if (current_character_data.blood_now / current_character_data.blood >= 0.7)
            {
                if (HP_10.activeSelf)
                    HP_10.SetActive(false);
                if (HP_9.activeSelf)
                    HP_9.SetActive(false);
                if (HP_8.activeSelf)
                    HP_8.SetActive(false);

                if (!HP_7.activeSelf)
                    HP_7.SetActive(true);
                if (!HP_6.activeSelf)
                    HP_6.SetActive(true);
                if (!HP_5.activeSelf)
                    HP_5.SetActive(true);
                if (!HP_4.activeSelf)
                    HP_4.SetActive(true);
                if (!HP_3.activeSelf)
                    HP_3.SetActive(true);
                if (!HP_2.activeSelf)
                    HP_2.SetActive(true);
                if (!HP_1.activeSelf)
                    HP_1.SetActive(true);
            }
            if (current_character_data.blood_now / current_character_data.blood >= 0.8)
            {
                if (HP_10.activeSelf)
                    HP_10.SetActive(false);
                if (HP_9.activeSelf)
                    HP_9.SetActive(false);

                if (!HP_8.activeSelf)
                    HP_8.SetActive(true);
                if (!HP_7.activeSelf)
                    HP_7.SetActive(true);
                if (!HP_6.activeSelf)
                    HP_6.SetActive(true);
                if (!HP_5.activeSelf)
                    HP_5.SetActive(true);
                if (!HP_4.activeSelf)
                    HP_4.SetActive(true);
                if (!HP_3.activeSelf)
                    HP_3.SetActive(true);
                if (!HP_2.activeSelf)
                    HP_2.SetActive(true);
                if (!HP_1.activeSelf)
                    HP_1.SetActive(true);
            }
            if (current_character_data.blood_now / current_character_data.blood >= 0.9)
            {
                if (HP_10.activeSelf)
                    HP_10.SetActive(false);

                if (!HP_9.activeSelf)
                    HP_9.SetActive(true);
                if (!HP_8.activeSelf)
                    HP_8.SetActive(true);
                if (!HP_7.activeSelf)
                    HP_7.SetActive(true);
                if (!HP_6.activeSelf)
                    HP_6.SetActive(true);
                if (!HP_5.activeSelf)
                    HP_5.SetActive(true);
                if (!HP_4.activeSelf)
                    HP_4.SetActive(true);
                if (!HP_3.activeSelf)
                    HP_3.SetActive(true);
                if (!HP_2.activeSelf)
                    HP_2.SetActive(true);
                if (!HP_1.activeSelf)
                    HP_1.SetActive(true);
            }
            if (current_character_data.blood_now / current_character_data.blood == 1)
            {
                if (!HP_10.activeSelf)
                    HP_10.SetActive(true);
                if (!HP_9.activeSelf)
                    HP_9.SetActive(true);
                if (!HP_8.activeSelf)
                    HP_8.SetActive(true);
                if (!HP_7.activeSelf)
                    HP_7.SetActive(true);
                if (!HP_6.activeSelf)
                    HP_6.SetActive(true);
                if (!HP_5.activeSelf)
                    HP_5.SetActive(true);
                if (!HP_4.activeSelf)
                    HP_4.SetActive(true);
                if (!HP_3.activeSelf)
                    HP_3.SetActive(true);
                if (!HP_2.activeSelf)
                    HP_2.SetActive(true);
                if (!HP_1.activeSelf)
                    HP_1.SetActive(true);

            }

            
            
            
            
            
            
            
            
            
            



        }
    }
}


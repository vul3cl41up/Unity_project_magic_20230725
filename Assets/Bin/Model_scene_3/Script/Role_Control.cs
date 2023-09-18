using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Model_scene_3
{
    public class Role_Control : MonoBehaviour
    {
        public enum Direction { Left = -1, Left_Up = -2, Left_Down = -3, Up = 0, Down = 1, Right = 2, Right_Up = 3, Right_Down = 4 }
        Direction direction;
        public Direction Direction_Now => direction;
        public Character_Data init_character_data;
        public Character_Data character_data;

        public Rigidbody2D Rb2D { get; private set; }

        private void Start()
        {
            Init();
            Rb2D = GetComponent<Rigidbody2D>();
            direction = Direction.Right;
        }
        private void FixedUpdate()
        {
            Move();
        }

        private void Init()
        {
            character_data.blood = init_character_data.blood;
            character_data.blood_now = init_character_data.blood_now;
            character_data.magic = init_character_data.magic;
            character_data.magic_now = init_character_data.magic_now;
            character_data.move_speed = init_character_data.move_speed;
            character_data.attack_speed = init_character_data.attack_speed;
            character_data.skill_1 = init_character_data.skill_1;
            character_data.skill_2 = init_character_data.skill_2;
            character_data.skill_3 = init_character_data.skill_3;
            character_data.skill_4 = init_character_data.skill_4;
        }

        private void Move()
        {
            Vector2 delta_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
            delta_input = delta_input.normalized;
            Rb2D.velocity = delta_input * character_data.move_speed;

            if(delta_input.x > 0 &&  delta_input.y > 0)
            {
                direction = Direction.Right_Up;
            }
            else if(delta_input.x > 0 && delta_input.y < 0)
            {
                direction = Direction.Right_Down;
            }
            else if(delta_input.x>0)
            {
                direction = Direction.Right;
            }
            else if(delta_input.x < 0 && delta_input.y > 0)
            {
                direction = Direction.Left_Up;
            }
            else if(delta_input.x < 0 && delta_input.y < 0)
            {
                direction = Direction.Left_Down;
            }
            else if(delta_input.x < 0)
            {
                direction = Direction.Left;
            }
            else if(delta_input.y >0)
            {
                direction = Direction.Up;
            }
            else if(delta_input.y < 0)
            {
                direction = Direction.Down;
            }
        }
    }
}
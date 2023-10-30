using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_control : MonoBehaviour
{
    public static void Start_Scene()
    {
        SceneManager.LoadScene("Start_scene");
    }
    public void Select_Scene()
    {
        SceneManager.LoadScene("select_scene");
    }
    public static void Skill_Select_Scene()
    {
        SceneManager.LoadScene("skill_select_scene");
    }
    public static void Skill_Scene()
    {
        SceneManager.LoadScene("skill_scene");
    }
    public static void Map_1_Scene()
    {
        SceneManager.LoadScene("map_1_scene");
    }
    public static void Map_2_Scene()
    {
        SceneManager.LoadScene("map_2_scene");
    }
    public static void Map_3_Scene()
    {
        SceneManager.LoadScene("map_3_scene");
    }
    public static void Exit_Game()
    {
        Application.Quit();
    }

}

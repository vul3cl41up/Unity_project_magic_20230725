using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_control : MonoBehaviour
{
    public static void Start_Scene()
    {
        SceneManager.LoadScene(0);
    }
    public void Select_Scene()
    {
        SceneManager.LoadScene(1);
    }
    public static void Skill_Select_Scene()
    {
        SceneManager.LoadScene(2);
    }
    public static void Model_3_Scene()
    {
        SceneManager.LoadScene(3);
    }
    public static void Exit_Game()
    {
        Application.Quit();
    }

}

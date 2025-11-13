using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    // Hàm gọi khi bấm nút Play
    public void PlayGame()
    {
        // Load scene chính (ví dụ Level1)
        SceneManager.LoadScene("Character Select Screen");
    }

    // Hàm gọi khi bấm nút Options
    public void OpenOptions()
    {
       SceneManager.LoadScene("Options");
    }

    // Hàm gọi khi bấm nút Quit
    public void QuitGame()
    {
        Debug.Log("Thoát game!");
        Application.Quit();
    }
}
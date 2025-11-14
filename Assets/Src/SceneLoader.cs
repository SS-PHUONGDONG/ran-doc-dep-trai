using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panelLoading;
    public Slider progressBar;  // nếu có thanh tiến trình
    public string sceneToLoad = "GameScene"; // tên scene bạn muốn chuyển tới

    public void OnPlayButtonClicked()
    {
        // Khi nhấn nút Play -> bật panel loading và bắt đầu load scene
        panelLoading.SetActive(true);
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Bắt đầu load scene bất đồng bộ
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; // chờ load xong mới chuyển

        // Cập nhật tiến trình
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (progressBar != null)
                progressBar.value = progress;

            // Khi tiến trình gần hoàn tất
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f); // đợi animation loading 1 chút
                operation.allowSceneActivation = true; // chuyển scene
            }

            yield return null;
        }
    }
}

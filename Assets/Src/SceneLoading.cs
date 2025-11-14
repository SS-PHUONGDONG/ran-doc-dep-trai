using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Thêm nếu dùng Slider hoặc Text trong loading panel
using System.Collections;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel; // Panel Loading
    [SerializeField] private Text loadingText; // (Tuỳ chọn) Text hiển thị tiến trình
    [SerializeField] private Slider loadingSlider; // (Tuỳ chọn) Slider hiển thị tiến trình

    // Hàm để bắt đầu tải scene
    public void LoadScene(string Host)
    {
        // Hiển thị Loading Panel
        loadingPanel.SetActive(true);

        // Bắt đầu Coroutine để tải scene
        StartCoroutine(LoadSceneAsync(Host));
    }

    // Coroutine để tải scene
    IEnumerator LoadSceneAsync(string Host)
    {
        // Bắt đầu tải scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Host);

        // Đảm bảo scene không được load đồng thời (đợi khi hoàn tất)
        asyncLoad.allowSceneActivation = false;

        // Trong khi scene vẫn đang tải
        while (!asyncLoad.isDone)
        {
            // Cập nhật thanh tiến trình hoặc text
            if (loadingSlider != null)
            {
                // Cập nhật giá trị tiến trình (0 - 1)
                loadingSlider.value = asyncLoad.progress;
            }

            if (loadingText != null)
            {
                // Cập nhật text với tiến trình
                loadingText.text = "Đang tải... " + (asyncLoad.progress * 100f).ToString("F0") + "%";
            }

            // Khi tải xong (khi asyncLoad.progress đạt 0.9), bắt đầu chuyển scene
            if (asyncLoad.progress >= 0.9f)
            {
                loadingText.text = "Đang chuyển...";

                // Để scene tự chuyển sau khi tải hoàn tất
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        // Ẩn Loading Panel khi chuyển scene xong
        loadingPanel.SetActive(false);
    }
}
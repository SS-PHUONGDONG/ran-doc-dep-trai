using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;


public class LoadingPanelWithVideo : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panelLoading;      // Panel chứa video và slider
    public Slider loadingSlider;         // Thanh tiến độ          // Hiển thị %
    public VideoPlayer videoPlayer;      // Video Player
    public string sceneToLoad = "GameScene";  // Tên scene cần load
    public TextMeshProUGUI loadingText;
    private bool isLoading = false;

    void Start()
    {
        panelLoading.SetActive(false);
        loadingSlider.value = 0f;
    }

    // Gọi hàm này khi bấm nút "Play" hoặc chuyển scene
    public void StartLoading()
    {
        if (!isLoading)
            StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        isLoading = true;
        panelLoading.SetActive(true);

        // Chuẩn bị phát video
        videoPlayer.Play();

        // Bắt đầu load scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            // Lấy tiến trình tải (0 -> 0.9)
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // Cập nhật slider và text
            loadingSlider.value = progress;
            loadingText.text = Mathf.RoundToInt(progress * 100f) + "%";

            // Cho video chạy theo tiến độ slider
            if (videoPlayer.frameCount > 0)
            {
                long targetFrame = (long)(videoPlayer.frameCount * progress);
                videoPlayer.frame = targetFrame;
            }

            // Khi tải gần xong
            if (progress >= 1f)
            {
                yield return new WaitForSeconds(0.5f); // Đợi video chạy thêm 0.5s
                asyncLoad.allowSceneActivation = true; // Chuyển scene
            }

            yield return null;
        }
    }
}

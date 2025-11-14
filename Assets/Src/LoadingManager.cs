using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButtonController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject loadingPanel;   // Panel loading (ẩn sẵn)
    public Slider progressBar;        // Thanh tiến trình (Slider)
    public Text warningText;          // Thông báo nếu chưa đủ điều kiện (có thể để trống)

    [Header("Loading Settings")]
    public string nextSceneName = "GameScene"; // Tên scene cần chuyển tới
    public float loadingDuration = 2f;         // Thời gian giả lập loading

    [Header("Điều kiện")]
    public bool isReadyToPlay = false; // Điều kiện, ví dụ: đã chọn nhân vật xong

    private bool isLoading = false;

    // Hàm gọi khi bấm nút Play
    public void OnPlayButtonClicked()
    {
        // Kiểm tra điều kiện
        if (!isReadyToPlay)
        {
            Debug.LogWarning("Chưa sẵn sàng để chơi!");
            if (warningText != null)
                warningText.text = "⚠️ Hãy chọn nhân vật trước khi bắt đầu!";
            return;
        }

        // Ngăn bấm lại
        if (isLoading) return;
        isLoading = true;

        // Hiện panel loading
        loadingPanel.SetActive(true);
        StartCoroutine(ShowLoadingThenChangeScene());
    }

    // Hàm chạy thanh loading
    IEnumerator ShowLoadingThenChangeScene()
    {
        progressBar.value = 0f;
        float elapsed = 0f;

        while (elapsed < loadingDuration)
        {
            elapsed += Time.deltaTime;
            progressBar.value = Mathf.Clamp01(elapsed / loadingDuration);
            yield return null;
        }

        // Khi loading xong → chuyển scene
        SceneManager.LoadScene(nextSceneName);
    }

    // Gọi hàm này từ script khác khi nhân vật được chọn
    public void SetReadyToPlay(bool ready)
    {
        isReadyToPlay = ready;
        if (warningText != null)
            warningText.text = "";
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PlayerControler : MonoBehaviour
{
    public float speed;
    private bool isWalk;
    private Vector2 diChuyenHuong;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    public int score = 0;

    [SerializeField] private GameObject LoadingCanvas;
    [SerializeField] private Slider LoadingSlider;
    [SerializeField] private TextMeshProUGUI percentTMP;

    [SerializeField] private TextMeshProUGUI scoreText; // Text hiển thị điểm số
    [SerializeField] private GameObject gameOverPanel; // Thêm tham chiếu đến game over panel
    [SerializeField] private TextMeshProUGUI livesTextMeshPro; // Text hiển thị số mạng
    private int lives = 3; // Mạng bắt đầu là 3
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // Hiển thị điểm số ban đầu
        UpdateScoreText();
        livesTextMeshPro.text = lives.ToString();
    }

    void Update()
    {
        GetInput();
        PlayerMove();
        Flip();
    }

    // Xử lý va chạm với xu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            AnXu(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives--;
            if (lives > 0)
            {
                // Reset scene when the player loses a life
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                livesTextMeshPro.text = lives.ToString();
            }
            else
            {
                Destroy(gameObject);
                gameOverPanel.SetActive(true); // Hiển thị game over panel
                Time.timeScale = 0; // Dừng thời gian (pause game)
            }
        }

        if (collision.gameObject.CompareTag("Next"))
        {
            LoadingCanvas.SetActive(true);
            StartCoroutine(Loading());
        }
    }

    IEnumerator Loading()
    {
        LoadingSlider.value = 0;
        percentTMP.text = "0%";

        // Tải Scene tiếp theo bất đồng bộ
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false; // Chưa chuyển sang scene mới ngay

        // Delay the progress to simulate 20% initially
        bool initialProgressSet = false;

        while (!asyncLoad.isDone)
        {
            // Cập nhật thanh tải và tỷ lệ phần trăm
            float progress = asyncLoad.progress;

            // If the progress is below 0.2, set it to 0.2 to simulate 20% loading
            if (!initialProgressSet && progress > 0 && progress < 0.2f)
            {
                progress = 0.2f; // Manually set to 20% if progress is less than 20%
                initialProgressSet = true; // Ensure it's only done once
            }

            // Normal progress calculation with clamp
            float normalizedProgress = Mathf.Clamp01(progress / 0.9f); // Progress max is 0.9
            LoadingSlider.value = normalizedProgress;
            percentTMP.text = Mathf.RoundToInt(normalizedProgress * 100) + "%";

            // Khi tải xong (progress đạt 0.9), cho phép chuyển cảnh
            if (asyncLoad.progress >= 0.9f)
            {
                Debug.Log("Scene loaded. Ready to activate.");
                LoadingSlider.value = 1f;
                percentTMP.text = "100%";
                asyncLoad.allowSceneActivation = true; // Kích hoạt chuyển cảnh
            }

            yield return null;
        }

        Debug.Log("Scene transition completed.");
    }

    // Xử lý ăn xu
    void AnXu(GameObject coin)
    {
        // Tăng điểm khi ăn xu
        score += 1;

        // Cập nhật điểm số trên UI
        UpdateScoreText();

        // Ẩn hoặc hủy đối tượng xu
        Destroy(coin);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void GetInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Chặn tăng tốc độ đường chéo
        diChuyenHuong = new Vector2(moveX, moveY).normalized;

        // Cập nhật lại animator
        isWalk = diChuyenHuong != Vector2.zero;
        animator.SetBool("isWalk", isWalk);
    }

    void PlayerMove()
    {
        Vector3 move = new Vector3(diChuyenHuong.x, diChuyenHuong.y, 0) * speed * Time.deltaTime;
        transform.position += move;
    }

    // Lật hình
    void Flip()
    {
        if (diChuyenHuong.x > 0) { sr.flipX = false; }
        else if (diChuyenHuong.x < 0) { sr.flipX = true; }
    }
}
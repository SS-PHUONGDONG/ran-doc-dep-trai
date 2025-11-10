using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f; // Tốc độ của viên đạn
    [SerializeField] private Transform target; // Tham chiếu đến mục tiêu (player)
    private Rigidbody2D rb;   // Rigidbody2D của viên đạn

    private void Awake()
    {
        // Lấy Rigidbody2D từ viên đạn
        rb = GetComponent<Rigidbody2D>();
    }

    // Phương thức này dùng để thiết lập tham chiếu đến mục tiêu (player) và tốc độ viên đạn
    public void Initialize(Transform target, float bulletSpeed)
    {
        this.target = target;  // Gán player làm mục tiêu
        this.bulletSpeed = bulletSpeed; // Thiết lập tốc độ viên đạn

        // Sau khi thiết lập mục tiêu và tốc độ, viên đạn sẽ di chuyển về phía player
        MoveTowardsPlayer();
    }

    // Di chuyển viên đạn về phía player
    private void MoveTowardsPlayer()
    {
        if (target != null)
        {
            // Tính toán vector hướng từ viên đạn đến player
            Vector2 direction = (target.position - transform.position).normalized;

            // Cập nhật vận tốc cho viên đạn
            rb.linearVelocity = direction * bulletSpeed;
        }
    }

    // Kiểm tra va chạm với player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Xử lý va chạm với player, ví dụ: trừ máu cho player (nếu cần)
            Destroy(gameObject);  // Hủy viên đạn khi va chạm với player
        }
    }
}

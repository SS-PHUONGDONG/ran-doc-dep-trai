using UnityEngine;
using UnityEngine.UI; // Thư viện UI
using System.Collections; // Thư viện coroutine
using UnityEngine.Events; // Quản lý events
public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 100;
    
    void Start()
    {
        // Thiết lập slider
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu va chạm với đạn địch
        if(collision.gameObject.tag == "EnemyBullet")
        {
            // Trừ máu
            healthSlider.value -= 10;

            // Hủy đạn
            Destroy(collision.gameObject);

            // Kiểm tra chết
            if(healthSlider.value <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
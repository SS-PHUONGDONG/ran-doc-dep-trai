using UnityEngine;
using UnityEngine.UI; // Thư viện UI
using System.Collections; // Thư viện coroutine
using UnityEngine.Events;
public class EnemyHealth : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 50;
    [SerializeField] private GameObject hitEffectPrefab;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Thiết lập slider
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Nếu va chạm với đạn player
        if(collider.gameObject.CompareTag("PlayerBullet"))
        {   
            // Trừ máu 
            healthSlider.value -= 10;

             // Tạo particle effect khi trúng đạn
            if (hitEffectPrefab != null)
            {
                // Tạo particle effect tại vị trí của enemy
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }

            // Hủy đạn
            Destroy(collider.gameObject);

            // Kiểm tra chết
            if(healthSlider.value <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
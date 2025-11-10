using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f; // Thời gian tồn tại của đạn
    [SerializeField] private GameObject hitEffect; // Hiệu ứng nổ khi đạn chạm tường
    
    private void Start()
    {
        // Hủy đạn sau một khoảng thời gian
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Danh sách tag không cho phép đạn đi qua
        string[] blockTags = { "Wall", "Obstacle" };

        // Kiểm tra va chạm
        if (Array.Exists(blockTags, tag => collision.CompareTag(tag)))
        {
            // Hiệu ứng nổ tại điểm va chạm nếu muốn
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }

            // Hủy đạn
            Destroy(gameObject);
        }
    }
}
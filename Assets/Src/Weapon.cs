using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private Transform checkSungNo;
    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private AudioSource audioSource;
    public float TimeBtwFire = 0.2f;
    public float bulletForce;
    private float timeBtwFire;
    private void Start() {
        
    }

    private void Update() {
        RotateGun();
        timeBtwFire -= Time.deltaTime;
        if(Input.GetMouseButton(0) && timeBtwFire < 0) 
        {
            FireBullet();  
        }
    }

    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            transform.localScale = new Vector3(1, -1, 0);
        else
            transform.localScale = new Vector3(1, 1, 0);

    }

    void FireBullet()
    {
        timeBtwFire = TimeBtwFire;
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);

        //Effect
        Instantiate(fireEffect, checkSungNo.position, transform.rotation, transform);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        // thêm âm thanh cho súng
        if(gunShotSound != null && audioSource != null) { audioSource.PlayOneShot(gunShotSound); }
    }
}


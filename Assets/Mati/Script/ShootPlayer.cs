using System;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{

    [SerializeField] private Transform gun;
    [SerializeField] private int shootSpeed;
    private Vector3 targetRotation;
    private Vector3 finalTarget;
    private BulletPool bulletPool;

    public int maxAmmo = 5;
    public float fireRate = .5f;

    private float nextFireTime = 0f; 

    void Awake() 
    {
        bulletPool = FindFirstObjectByType<BulletPool>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameManager.instance.ChangeCurrentFoodType();
        }

        targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && GameManager.instance.GetAmmo() > 0)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Calcular el próximo disparo permitido.
        }
    }

    void Shoot()
    {
        
        GameManager.instance.HasShot(fireRate);
        var shoot = bulletPool.GetBullet(GameManager.instance.GetCurrentFoodType(), gun.position, gun.rotation);
        targetRotation.z = 0;
        finalTarget = (targetRotation - transform.position).normalized;
        shoot.GetComponent<Rigidbody2D>().AddForce(finalTarget * shootSpeed, ForceMode2D.Impulse);
    }
}

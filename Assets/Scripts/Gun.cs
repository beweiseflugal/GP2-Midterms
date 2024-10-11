using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform barrelTransform;
    [SerializeField] float fireRate;
    float currentFireRate;

    private void Update()
    {
        currentFireRate -= Time.deltaTime;
        if (Player.Instance.GetCurrentTarget() != null)
        {
            if (currentFireRate <= 0)
            {
                currentFireRate = fireRate;
                ShootGun();
            }
        }
    }

    void ShootGun() {
        Projectile newProjectile = Instantiate(projectilePrefab, barrelTransform.position, barrelTransform.rotation);
        newProjectile.SetDirection(barrelTransform.forward);
    }
}

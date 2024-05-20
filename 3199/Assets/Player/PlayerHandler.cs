using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHandler : MonoBehaviour, HumanoidInterface
{
    PhotonView view;
    public Rigidbody2D body;
    public int speed = 5;
    public int sprintMultiplier = 2;
    public SpriteRenderer spriteRenderer;

    public GameObject bulletPrefab; // Modèle de balle à cloner
    public Transform bulletSpawnPoint; // Point de départ des balles
    public int bulletPerShot = 1;
    public int clipAmmo = 10; // Munitions actuelles dans le chargeur
    public int clipSize = 10; // Taille maximale du chargeur
    public int ammo = 100; // Munitions totales disponibles
    public int bulletDamage = 10; // Dégâts causés par chaque balle
    public float bulletLifetime = 5f; // Durée de vie de la balle en secondes
    public float fireRate = 1f;
    public float recoil = 2f; // Variance angulaire due au recul, en degrés
    private float lastFireTime;

     public int MaxHealth = 100;
     public int health = 100;

    public void Start()
    {
        view = GetComponent<PhotonView>();
        lastFireTime = -fireRate;
    }
    public void Died()
    {
        speed = 0;
    }
    
    public void TakeDamage(int damages)
    {
        health -= damages;
        if (health <= 0)
        {
            health = 0;
            Died();
        }
        else 
                {
                    // Faites trembler le sprite
                    StartCoroutine(ShakeSprite()); 
                }
    }
    private bool canShake = true;
    IEnumerator ShakeSprite()
    {
        if (canShake)
        {
            canShake = false;
            Vector3 originalPosition = spriteRenderer.transform.position;
            float shakeDuration = 0.1f;
            float shakeMagnitude = 0.1f;
            float elapsedTime = 0f;
            float coolDownDuration = .2f;
    
            while (elapsedTime < shakeDuration)
            {
                float x = Random.Range(-1f, 1f) * shakeMagnitude;
                float y = Random.Range(-1f, 1f) * shakeMagnitude;
    
                // Ajoutez les tremblements à la position originale
                spriteRenderer.transform.position = originalPosition + new Vector3(x, y, 0f);
    
                elapsedTime += Time.deltaTime;
                yield return null;
            }
    
            // Réinitialisez la position à la fin du tremblement
            spriteRenderer.transform.position = originalPosition;
    
            // Pause pendant la période de cooldown
            yield return new WaitForSeconds(coolDownDuration);
    
            canShake = true;
        }
    }



    void Update()
    {
        if (view.IsMine)
        {
            HandleMovement();
             if (Input.GetButton("Fire1"))
                {
                    TryFireBullets();
                }
        }
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool sprinting = Input.GetButton("Fire3");
        int currentSpeed = sprinting ? speed * sprintMultiplier : speed;
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
        body.velocity = movement * currentSpeed;
        spriteRenderer.flipX = horizontalInput < 0;
    }

    void TryFireBullets()
    {
        if (Time.time > lastFireTime + fireRate && clipAmmo > 0)
        {
            lastFireTime = Time.time;
            FireBullets();
        }
    }

    void FireBullets()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDirection = (mousePosition - (Vector2)bulletSpawnPoint.position).normalized;

        for (int i = 0; i < bulletPerShot; i++)
        {
            Vector2 modifiedDirection = Quaternion.Euler(0, 0, Random.Range(-recoil, recoil)) * fireDirection;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = modifiedDirection * 20f; // Ajustez la vitesse de la balle si nécessaire
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.damage = bulletDamage;
                bulletScript.SetupBullet(bulletLifetime); // Configure la durée de vie de la balle
            }
        }
        clipAmmo--;
        if (clipAmmo <= 0 && ammo > 0)
        {
            Reload();
        }
    }

    void Reload()
    {
        int ammoNeeded = clipSize - clipAmmo;
        if (ammo >= ammoNeeded)
        {
            clipAmmo += ammoNeeded;
            ammo -= ammoNeeded;
        }
        else
        {
            clipAmmo += ammo;
            ammo = 0;
        }
    }
}

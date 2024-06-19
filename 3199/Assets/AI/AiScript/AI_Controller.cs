using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class AI_Controller : MonoBehaviour, HumanoidInterface
{
    private List<Transform> targets = new List<Transform>();
    private Transform closestTarget;
    [SerializeField] private Transform rayOrigin;
    private UnityEngine.AI.NavMeshAgent agent;


    public int MaxHealth = 100;
    public int health = 100;
    
    private string targetTag = "Player";
    public SpriteRenderer spriteRenderer;
    private bool canChase = false;
    
    public int rayDistance = 20;
    public int angle = 45; // Total vision angle
    public int numberOfRays = 5; // Number of raycasts to send
    private float timeSinceAcquiredTarget = 0f;
    public float timeToLoseTarget = 5f;
    private bool lookLeft = false;

    private Vector3 lastPosition; // To store the last position and compare movement
    public float wanderRadius = 10f; // Radius for random wandering
    

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
    public bool auto = false; // Permet le tir automatique si true
    public float maxShootRange = 25f; // Portée maximale de tir
    private float lastFireTime;
    
    public void Died()
    {
        canChase = false;
        wanderRadius = 0;
        targets.Clear();
        
        
        Destroy(gameObject);
        GameManager.instance.Kill ();
    }
    
    
    
    public void TakeDamage (int damage) // imported from Humanoid
    {
        health -= damage;
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



    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        lastPosition = transform.position;
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = lookLeft;
        }

        ScanForTargets();
        UpdateTargetSelection();
        UpdateLookDirection();
    }

    private void ScanForTargets()
    {
        float startAngle = Mathf.Lerp(angle / 2, -angle / 2, 0);
        float endAngle = Mathf.Lerp(angle / 2, -angle / 2, 1);
        float angleStep = (endAngle - startAngle) / (numberOfRays - 1);
        bool targetFound = false;
    
        // Créez un LayerMask pour les AI
        LayerMask aiLayerMask = LayerMask.GetMask("AI");
    
        for (int i = 0; i < numberOfRays; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Vector2 direction = lookLeft ? Quaternion.Euler(0, 0, currentAngle) * rayOrigin.right * -1 : Quaternion.Euler(0, 0, currentAngle) * rayOrigin.right;
            
            // Utilisez Physics2D.RaycastNonAlloc pour stocker tous les résultats du raycast dans un tableau
            RaycastHit2D[] hits = new RaycastHit2D[1]; // On utilise un tableau de longueur 1 car on ne veut récupérer qu'une collision
            int numHits = Physics2D.RaycastNonAlloc(rayOrigin.position, direction, hits, rayDistance, ~aiLayerMask); // Utilisez ~ pour exclure les collisions avec les AI
            
            // Vérifiez si le raycast a frappé quelque chose
            if (numHits > 0)
            {
                RaycastHit2D hit = hits[0];
                if (hit.collider != null && hit.collider.CompareTag(targetTag))
                {
                    if (!targets.Contains(hit.transform))
                    {
                        targets.Add(hit.transform);
                    }
                    targetFound = true;
                    timeSinceAcquiredTarget = 0f; // Reset time
                }
            }
        }
    
        if (!targetFound)
        {
            timeSinceAcquiredTarget += Time.deltaTime;
    
            if (timeSinceAcquiredTarget >= timeToLoseTarget)
            {
                canChase = false;
                WanderRandomly(); // Call random movement function
            }
        }
    }


    private void UpdateTargetSelection()
    {
        if (targets.Count > 0 && health>0)
        {
            closestTarget = null;
            float minDistance = float.MaxValue;

            foreach (Transform target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (distance < minDistance)
                {
                    closestTarget = target;
                    minDistance = distance;
                }
            }

            if (closestTarget != null)
            {
                canChase = true;
                agent.SetDestination(closestTarget.position);
            }
        }
    }

    private void WanderRandomly()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, -1);
        agent.SetDestination(navHit.position);
    }

    private void UpdateLookDirection()
    {
        if (transform.position.x < lastPosition.x)
        {
            lookLeft = true;
        }
        else if (transform.position.x > lastPosition.x)
        {
            lookLeft = false;
        }
        lastPosition = transform.position;
    }
    
    
    

    bool IsTargetVisible(Transform target)
    {
        // Créez un LayerMask pour ignorer les collisions avec le layer AI
        LayerMask layerMask = LayerMask.GetMask("AI");
        
        // Vérifiez si la cible est visible en effectuant un raycast
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.position, target.position - rayOrigin.position, maxShootRange, ~layerMask);
        
        if (hit.collider != null && hit.collider.CompareTag(targetTag))
        {
            return true;
        }
        
        Debug.Log("Cible non détectée");
        return false;
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
        Vector2 fireDirection = (closestTarget.position - bulletSpawnPoint.position).normalized;
    
        for (int i = 0; i < bulletPerShot; i++)
        {
            Vector2 modifiedDirection = Quaternion.Euler(0, 0, Random.Range(-recoil, recoil)) * fireDirection;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            
            if (bulletRb != null)
            {
                bulletRb.velocity = modifiedDirection * 20f; // Ajustez la vitesse de la balle si nécessaire
            }
    
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.damage = bulletDamage;
                bulletScript.SetupBullet(bulletLifetime); // Configure la durée de vie de la balle
                
               bulletScript.damagePlayer = true;
                bulletScript.damageAI = false;
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

    void FixedUpdate()
    {
        if (canChase && closestTarget != null)
        {
            agent.SetDestination(closestTarget.position);
             if (Vector3.Distance(transform.position, closestTarget.position) <= maxShootRange && IsTargetVisible(closestTarget))
             {
                 TryFireBullets();
             }
        }
        else 
        {
            WanderRandomly();
        }
    }
}

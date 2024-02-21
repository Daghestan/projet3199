using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Transform rayOrigin;
    private UnityEngine.AI.NavMeshAgent agent;

    private string targetTag = "Player";
    public SpriteRenderer spriteRenderer;
    private bool canChase = false;
    
    public int rayDistance = 20;
    public int angle = 45; // Angle total de vision
    public int numberOfRays = 5; // Nombre de raycasts à envoyer
    private float timeSinceAcquiredTarget = 0f;
    public float timeToLoseTarget = 5f;
    private bool lookLeft = false;

    private Vector3 lastPosition; // Pour stocker la dernière position et comparer le mouvement
    public float wanderRadius = 10f; // Rayon pour le déplacement aléatoire

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
        
       float startAngle = Mathf.Lerp(angle / 2, -angle / 2, 0);
       float endAngle = Mathf.Lerp(angle / 2, -angle / 2, 1);
       float angleStep = (endAngle - startAngle) / (numberOfRays - 1);
       bool targetFound = false;
        for (int i = 0; i < numberOfRays; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
                    // Convertir l'angle en direction en prenant en compte la rotation de rayOrigin
                    Vector2 direction =  lookLeft ?Quaternion.Euler(0, 0, currentAngle) * rayOrigin.right * -1: Quaternion.Euler(0, 0, currentAngle) * rayOrigin.right * 1;
                    
                    RaycastHit2D hit = Physics2D.Raycast(rayOrigin.position, direction, rayDistance);
                    Debug.DrawRay(rayOrigin.position, direction * rayDistance, Color.red);
            
             if (hit.collider != null && hit.collider.CompareTag(targetTag))
                    {
                        target = hit.transform;
                        canChase = true;
                        timeSinceAcquiredTarget = 0f; // Réinitialiser le temps
                        targetFound = true;
                        break;
                    }
        }
        

        if (targetFound == false)
        {
            timeSinceAcquiredTarget += Time.deltaTime;

            if (timeSinceAcquiredTarget >= timeToLoseTarget)
            {
                canChase = false;
                WanderRandomly(); // Appeler la fonction de déplacement aléatoire
            }
        }

        UpdateLookDirection();
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
        // Comparer la position actuelle avec la dernière position pour déterminer la direction
        if (transform.position.x < lastPosition.x)
        {
            lookLeft = true;
        }
        else if (transform.position.x > lastPosition.x)
        {
            lookLeft = false;
        }
        // Mettre à jour la dernière position pour la prochaine comparaison
        lastPosition = transform.position;
    }
    
    void FixedUpdate()
        {
            if (canChase && target != null)
            {
                agent.SetDestination(target.position);
            }
            else 
            {
                WanderRandomly();
            }
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    // Ce script 
    private Transform target;
    [SerializeField] Transform rayOrigin; // ceci est un transform défini depuis l'éditeur lorsque l'on importe ce code dans un NPC.
    UnityEngine.AI.NavMeshAgent agent;

    public string targetTag = "Player";
    private bool canChase = false;
    public int rayDistance = 100;
    public float rayOrigineDist = 0.02999999f;
    private float timeSinceAcquiredTarget = 0f;
    public float timeToLoseTarget = 5f; // Temps en secondes avant de perdre la cible

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        // Utiliser un raycast 2D pour détecter le joueur
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.position, rayOrigin.right, rayDistance);

        // Vérifier si le raycast a touché un objet avec le tag du joueur
        if (hit.collider != null && hit.collider.CompareTag(targetTag))
        {
            // Redéfinir la cible sur le joueur
            target = hit.transform;
            canChase = true;
            timeSinceAcquiredTarget = 0f; // Réinitialiser le temps
        }
        else
        {
            // Aucun joueur détecté, augmenter le temps
            timeSinceAcquiredTarget += Time.deltaTime; // delta time est plus ou moins le temp d'une frame (entre 1/180 et 1/12 selon les jeux)

            // Si le temps dépasse la limite, désactiver la poursuite
            if (timeSinceAcquiredTarget >= timeToLoseTarget)
            {
                canChase = false;
            }
        }
    }

    void FixedUpdate()
    {
        // Mettre à jour la destination si la poursuite est activée
        if (canChase)
        {
            agent.SetDestination(target.position);
        }
    }
}
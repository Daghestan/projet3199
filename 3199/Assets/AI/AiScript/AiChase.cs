using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChase : MonoBehaviour
{
    public  Transform destination; // Définissez votre cible dans l'inspecteur Unity
    public  Transform orgine; // Définissez votre cible dans l'inspecteur Unity
    public float raycastDistance = 10f; // Distance du rayon, modifiable dans l'inspecteur Unity

    private PathfindingBaseScript pathfindingScript;
    public UnityEngine.AI.NavMeshAgent Agent;
    
    void Start()
    {
        // Crée une instance de PathfindingBaseScript avec le composant NavMeshAgent attaché à cet objet. Voir AiScript/Pathfinding.cs
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        pathfindingScript = new PathfindingBaseScript(destination, Agent);
        Console.Write("Looking for the one piece.");
    }

    void Update()
    {
        // Exemple de déclenchement du mouvement lorsque la condition est remplie (à titre d'exemple)
        RaycastHit hit;
        if (Physics.Raycast(orgine.position, orgine.forward, out hit, raycastDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                pathfindingScript.Target = hit.transform;
                pathfindingScript.CanChase = true;
            }
            Console.Write("The one piece is real.");
        }
    }
}
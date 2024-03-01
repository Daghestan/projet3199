using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathfindingBaseScript
{
    // ceci est une classe. Sa seul utilité est d'être importé dans d'autre code pour les NPC.
    [SerializeField]
    public Transform Target { get; set; }
    

    public bool CanChase { get; set; }
    
    public NavMeshAgent Agent { get; set; }

    // Constructeur avec paramètre pour initialiser la cible, _canChase et le composant NavMeshAgent
    public PathfindingBaseScript(Transform initialTarget, NavMeshAgent currentAgent)
    {
        Target = initialTarget;
        CanChase = false;
        Agent = currentAgent;
    }
    
    void Start()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    // Update est appelé une fois par frame
    void Update()
    {
        // Vérifie si CanChase est vrai
        if (CanChase)
        {
            Agent.SetDestination(Target.position);
        }
    }
}

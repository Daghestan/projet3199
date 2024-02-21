using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ToolConfig
{
    // Propriétés
    string Name { get; } // Le nom de l'outil.
    int Durability { get; set; } // La durabilité de l'outil, accessible en lecture et écriture.

    // Méthodes
    void Use(); // Méthode pour utiliser l'outil.
    void StopUse(); // Méthode pour arrêter d'utiliser l'outil.
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D  body;
    public int speed = 5;
    public int sprintMultiplier = 2;

    void Update()
    {
        // Obtenir les entrées de déplacement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Obtenir l'état de Fire1 (peut être un bouton de la manette ou une touche du clavier)
        bool sprinting = Input.GetButton("Fire1");

        // Appliquer un multiplicateur de vitesse si Fire1 est pressé
        int currentSpeed = sprinting ? speed * sprintMultiplier : speed;

        // Créer un vecteur de mouvement en fonction des entrées
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        // Appliquer le mouvement au Rigidbody du joueur
        body.velocity = movement * currentSpeed;
    }
}

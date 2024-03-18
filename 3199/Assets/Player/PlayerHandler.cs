using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, HumanoidInterface
{
    public Rigidbody2D body;
    public int speed = 5;
    public int sprintMultiplier = 2;
    public SpriteRenderer spriteRenderer;

    public float health { get; set; } = 100;

   
    public void Died()
    {
        speed = 0;
    }
    
    public void takedamage (int damages)
    {
        health -= damages;
        if (health <= 0)
        {
            health = 0;
            Died();
        }
    }
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

        // Modifier flipX du spriteRenderer en fonction de la direction du mouvement horizontal
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false; // Déplacement vers la droite, pas de flip
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true; // Déplacement vers la gauche, flip le sprite
        }
        // Note : Pas de changement si horizontalInput est 0 pour éviter de flipper le sprite quand le joueur s'arrête
    }
}

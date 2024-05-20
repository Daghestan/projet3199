using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Bullet : MonoBehaviour
{
    public int damage;
    public bool damagePlayer = false;
    public bool damageAI = true;
    
     private Rigidbody2D rb; // Le Rigidbody2D de la balle


    private void Start()
       {
           rb = GetComponent<Rigidbody2D>(); // Obtenez la référence au Rigidbody2D du bullet
       }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si l'objet touché est un objet qui peut être collisionné
        if (other.CompareTag("Collision"))
        {
            Rigidbody2D otherRigidbody = other.GetComponent<Rigidbody2D>();
            if (otherRigidbody != null)
            {
                Vector2 bulletDirection = rb.velocity.normalized; // Obtenir la direction normalisée de la vélocité du bullet
                float forceMagnitude = rb.velocity.magnitude/100 * damage; // Appliquer la magnitude de la vélocité du bullet comme force
                 // Appliquer la force dans la direction du bullet avec une magnitude dépendante de la vélocité du bullet
                otherRigidbody.AddForce(bulletDirection * forceMagnitude, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
        else
        {
            HumanoidInterface humanoidInterface = other.GetComponent<HumanoidInterface>();
             if (humanoidInterface != null)
             {
                 if ((damagePlayer && other.CompareTag("Player")) || (damageAI && other.CompareTag("AI")))
                 {
                     humanoidInterface.TakeDamage(damage);
                     Destroy(gameObject); 
                 }
             }
        }
    }

    
     public void SetupBullet(float lifetime)
        {
            Destroy(gameObject, lifetime); // Détruire la balle après le temps de vie défini
        }
}
using UnityEngine;
public class HealthHandler : MonoBehaviour
{
    public Transform bar;
    public Transform fill;

    // Ou utilisez un champ sérialisable pour stocker le contrôleur
    [SerializeField]
    private AI_Controller aiController;
    [SerializeField]
    private PlayerHandler playerController;

    void Update()
    {
       float healthRatio = 1f;
        if (aiController != null)
        {
            healthRatio = (float)aiController.health / aiController.MaxHealth;
        }
        else if (playerController != null)
        {
            healthRatio = (float)playerController.health / playerController.MaxHealth;
        }
        fill.localScale = new Vector3(healthRatio, 1f, 1f);
    }
}

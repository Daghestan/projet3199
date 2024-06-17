using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // La cible que la caméra doit suivre
    public Vector3 offset; // Décalage de la caméra par rapport à la cible

    void Start()
    {
        // Trouver le joueur localement
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();
        foreach (PhotonView pv in photonViews)
        {
            if (pv.IsMine)
            {
                target = pv.transform;
                break;
            }
        }

        if (target == null)
        {
            Debug.LogError("Local player not found!");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}

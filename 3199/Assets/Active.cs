using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{

    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam.SetActive(true);
    }

    // Update is called once per frame
}

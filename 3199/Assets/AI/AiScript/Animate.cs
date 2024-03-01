using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Animator animator;
    public string SpriteName = "RobotWalk";
    
    
     private void Start()
         {
             animator = GetComponent<Animator>();
             if (animator == null) 
             {
                 Debug.LogError("Animator component not found.");
             }
         }
    // Update is called once per frame
    void Update()
    {
        animator.Play(SpriteName);// modifie pour être facilement modifiable
    }
}

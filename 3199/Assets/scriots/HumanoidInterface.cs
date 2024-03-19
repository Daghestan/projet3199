using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HumanoidInterface
{
   public float health {get; set;}

   public void takedamage(int damage);

   public void Died();
}

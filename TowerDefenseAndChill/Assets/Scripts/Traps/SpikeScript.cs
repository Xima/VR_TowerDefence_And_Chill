using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : Trap {
   public float range = 0.5f;
   public float returnTime = 8f;
   public float reloadTime = 30f;
    bool used = false;
   float timeUsed;
   public GameObject needle;

    public override void highlight(bool h)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            if (h)
            {
                renderers[i].material.shader = Shader.Find("Standard");

                renderers[i].material.SetColor("Albedo", Color.white);
                /*
            renderers[i].material.shader =  Shader.Find ("Mobile/Bumped Specular");

                renderers[i].material.SetFloat("_Shininess", 0.03f);*/
            }
            else
            {
                renderers[i].material.shader = Shader.Find("Mobile/Diffuse");
            }
        }

    }

   void Update(){
      if (used && Time.time - timeUsed > returnTime) {
         needle.transform.Translate (Vector3.down * 2);
      }
      if (used && Time.time - timeUsed > reloadTime) {
         used = false;
      }
   }

    public override void launch()
    {
        if (!used) {
            used = true;
            timeUsed = Time.time;
            GetComponent<Animation>().Play();
            List<EnemyHealth> enemies = EnemyManager.getEnemies();
            for (int i = 0; i < enemies.Count; i++)
            {
                if(Vector3.Distance(enemies[i].transform.position, transform.position) < range)
                {
                    enemies[i].TakeDamage(100);
                }
            }
       }
    }

    public override bool selectable()
    {
        return !used;
    }
}

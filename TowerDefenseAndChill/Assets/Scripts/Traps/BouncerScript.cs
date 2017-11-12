using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScript : Trap {

    bool used = false;
    public GameObject sphere;
    public float range = 1f;

    private void Start()
    {

    }

    private void Update()
    {

    }

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

    public override void launch()
    {
        if (!used)
        {
            used = true;
            GetComponent<Animation>().Play();
            List<EnemyHealth> enemies = EnemyManager.getEnemies();
            for (int i = 0; i < enemies.Count; i++)
            {
               Debug.Log (Vector3.Distance (enemies [i].transform.position, sphere.transform.position));
               if (Vector3.Distance(enemies[i].transform.position, sphere.transform.position) < range)
                {
                     Debug.Log ("dsf");
                    enemies[i].TakeDamage(200);
                    enemies[i].gameObject.GetComponent<Rigidbody>().velocity = (gameObject.transform.forward + gameObject.transform.up) * 50;
                }
            }
        }
    }


    public override bool selectable()
    {
        return true;
    }
}

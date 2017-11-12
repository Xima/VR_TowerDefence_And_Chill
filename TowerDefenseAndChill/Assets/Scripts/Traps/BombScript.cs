using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : Trap {

    public float blowUp = 100f;
    public GameObject explosion;
    public float range = 3f;

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
        GameObject ex = Instantiate(explosion);
        ex.transform.position = transform.position;
        Destroy(this.gameObject, 0.5f);
        Destroy(ex, 1);
        List<EnemyHealth> enemies = EnemyManager.getEnemies();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (Vector3.Distance(enemies[i].transform.position, transform.position) < range)
            {
                enemies[i].TakeDamage(200);
                Vector3 dir = ((Vector3.up*3 + enemies[i].gameObject.transform.position) - transform.position);
                dir /= (dir.magnitude * dir.magnitude);
                enemies[i].gameObject.GetComponent<Rigidbody>().velocity = dir * 50;//(dir * blowUp);
            }
        }
    }


    public override bool selectable()
    {
        return true;
    }
}


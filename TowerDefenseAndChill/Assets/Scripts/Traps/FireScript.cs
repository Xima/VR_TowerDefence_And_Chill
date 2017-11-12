using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : Trap {
    public float fireTime = 10f;
    public float reloadTime = 20f;
    float timer = 0f;
    bool can = true;
    bool fire = false;
    public GameObject flames;
    public  Renderer renderer;
    public float range = 0.5f;
    public float fireUpdate = 100f;
    public int fireDamage = 5;
    float lastChecked; 

    private void Start()
    {
        timer = Time.time;
        flames.SetActive(false);
        lastChecked = 0f;
    }

    
    private void Update()
    {

        if(fire)
        {
            if(Time.time - timer > fireTime)
            {
                fire = false;
                flames.SetActive(false);
                timer = Time.time;
            }
            if(Time.time-lastChecked > fireUpdate / 1000f) { 
                List<EnemyHealth> enemies = EnemyManager.getEnemies();
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (Vector3.Distance(enemies[i].transform.position, transform.position) < range)
                    {
                        enemies[i].TakeDamage(fireDamage);
                    }
                }
            }
        }
        if(!can)
        {
            if(Time.time - timer > reloadTime)
            {
                can = true;
            }
        }
    }

    public override void highlight(bool h)
    {

        if(h)
        {


            renderer.material.shader = Shader.Find("Standard");

            renderer.material.SetColor("Albedo", Color.red);
        }
        else
        {

            renderer.material.shader = Shader.Find("Mobile/Diffuse");

        }
       
    }

    public override bool selectable()
    {
        return !fire && can;
    }

    public override void launch()
    {
        if (!fire && can)
        {
            fire = true;
            can = false;
            timer = Time.time;
            flames.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float speed = 0.3f;
    private Transform[] path;
    private Transform target;
    private int wavepointIndex = 0;
    EnemyHealth enemyHealth;

    // Use this for initialization
    void Start () {
        int i = Random.Range(0, Waypoints.paths.Count);
        path = Waypoints.paths[i];
        target = path[0];
        enemyHealth = GetComponent<EnemyHealth>();
    }
	
	// Update is called once per frame
	void Update () {
   

        if (!enemyHealth.isDead)
        {
            Vector3 dir = target.position - transform.position;
            //float rot = Mathf.Abs(target.eulerAngles.y - transform.eulerAngles.y);
            float angle = Quaternion.Angle(transform.rotation, target.rotation);

            //transform.Rotate(Vector3.up * angleSign * 20 * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, speed * 0.053f);
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.5f && Mathf.Abs(angle) <= 5.0f)
            {
                GetNextWaypoint();


            }
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= path.Length - 1)
        {

            //Destroy(gameObject);
            
            //if (enemyHealth != null)
            //{
            //    enemyHealth.TakeDamage(10);
            //}
            return;
        }

        wavepointIndex++;
        target = path[wavepointIndex];
    }
}

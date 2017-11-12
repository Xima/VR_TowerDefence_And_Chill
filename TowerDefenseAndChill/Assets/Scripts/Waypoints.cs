using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {
    public static List<Transform[]> paths;
	public static Transform[] pts;

    private void Awake()
    {
        paths = new List<Transform[]>();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            pts = new Transform[transform.GetChild(i).childCount];
            for (int j = 0; j < pts.Length; j++)
            {
                pts[j] = transform.GetChild(i).GetChild(j);
            }
            paths.Add(pts);
        }
    }
   
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public PlayerHealth player;


    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        
    }


    void Update ()
    {
        text.text = "Gold: " + player.currentGold;
    }
}

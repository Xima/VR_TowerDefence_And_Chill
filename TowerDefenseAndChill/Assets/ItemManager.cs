using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
   public float scale = 0.2f;


   public List<Trap> itemList;

   public Trap getItem(int itemID) {
      Trap trap =  Instantiate (itemList [itemID-1]);
      trap.transform.localScale = new Vector3 (scale, scale, scale);
      return trap;
   }

}

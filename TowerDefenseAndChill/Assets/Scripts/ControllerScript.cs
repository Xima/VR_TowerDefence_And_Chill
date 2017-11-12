using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -------------------------------------------------------------------------
// Bridge client code sample for Unity
// Copyright Logitech - 2017
// 
// This code sample shows a set of example calls to the Bridge runtime.
// (Non-exhaustive list, please refer to the documentation for the full
//  set of functions)
//
// Note that the Bridge software has to run prior to running
// this code sample.
// -------------------------------------------------------------------------

public class ControllerScript : MonoBehaviour {

   enum GameState {PLACING, DEFENDING};
   enum SelectionState {NOTHING, DISPLAYING, PLACING};

   GameState gameState = GameState.PLACING;
   SelectionState selectedState = SelectionState.NOTHING;


   public float itemSelectThreshold = 2f;
   public float gridSize = 2f;
   public float angleThreshold = 3.5f;
   public bool emulateHead = false;
   public LayerMask castLayerMask = -1;
   public LayerMask defendLayerMask = -1;
   public float movementSpeed = 1.0f;
   public float cameraSpeed = 25.0f;
   	public float selectedItemDistance = 1.0f;
    public ItemManager itemManager;
    float smoothTime = 0.05f;
    private Vector3 velocity = Vector3.zero;
    int errorCode;
    Trap selectedItem;
   	Vector3 selectedItemTargetPosition;
    Vector3 selectedItemOffset;

    public EnemyManager enemyManager;
    Vector3 initCameraDirection;

   private List<Trap> itemsList = new List<Trap>();

   void selectItem(int itemID) {
      if (itemID == 0) {
         selectedState = SelectionState.NOTHING;
         if(selectedItem)
            {
                Destroy(selectedItem.gameObject);
            }
         selectedItem = null;
      } else {
         if (itemManager != null) {
            if(selectedItem != null)
            {
                Destroy(selectedItem.gameObject);
            }
            selectedItem = itemManager.getItem (itemID);
            Vector3 cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            selectedItemOffset = selectedItemDistance * cameraForward.normalized;
            selectedItem.transform.position = transform.position +  selectedItemOffset;
            selectedState = SelectionState.DISPLAYING;
            initCameraDirection = Camera.main.transform.forward;
         }
      }
   }

   void Start() {
   }

   void Update() {

    
   }

    // Listen to keys events
    void FixedUpdate () {
         Vector3 dirRight = new Vector3 (Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
         Vector3 dirForward = new Vector3 (Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
         transform.Translate(Input.GetAxis("Horizontal") * dirRight * Time.deltaTime * movementSpeed);
         transform.Translate(Input.GetAxis("Vertical") * dirForward * Time.deltaTime * movementSpeed);

         if (Input.GetKey ("b") && emulateHead) {
            Camera.main.transform.Rotate (Input.GetAxis ("Mouse X") * Vector3.up * Time.deltaTime * cameraSpeed);
            Camera.main.transform.Rotate (Input.GetAxis ("Mouse Y") * Vector3.left * Time.deltaTime * cameraSpeed);
         }

      if(gameState == GameState.PLACING) {
         if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) {
            gameState = GameState.DEFENDING;
            enemyManager.startSpawn();
         }
         switch (selectedState) {
            case SelectionState.NOTHING:
               break;
            case SelectionState.DISPLAYING: 
               selectedItem.transform.position = transform.position + selectedItemOffset;
               if (Vector3.Angle (Camera.main.transform.forward, initCameraDirection) > angleThreshold) {
                  selectedState = SelectionState.PLACING;
                    selectedItem.transform.localRotation = new Quaternion();
                    selectedItemTargetPosition = Vector3.zero;

               }
               break;
			case SelectionState.PLACING:
                  // prepare raycast
				RaycastHit hit;
				Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
				if (Physics.Raycast (ray, out hit, 1000, castLayerMask)) {
					selectedItemTargetPosition = new Vector3 (Mathf.Round (hit.point.x / gridSize) * gridSize, 0, Mathf.Round (hit.point.z / gridSize) * gridSize);
				}
				bool collision = false;
				for (int i = 0; i < itemsList.Count; i++) {
					if (Vector3.Distance (selectedItemTargetPosition, itemsList [i].transform.position) < 0.5) {
						collision = true;
						break;
					}
				}
				if (!collision) {
					selectedItem.transform.position = Vector3.SmoothDamp (selectedItem.transform.position, selectedItemTargetPosition, ref velocity, smoothTime);
				}
                  // Place item
                if (Input.GetKeyDown ("space")) {
                    if(selectedItemTargetPosition != Vector3.zero)
                    {
                        
                        itemsList.Add(selectedItem);
                        selectedItem.transform.position = selectedItemTargetPosition;
                        selectedItem = null;
                        selectedState = SelectionState.NOTHING;
                    }
               }

               break;
            }

            if(Input.GetKeyDown("left shift") || Input.GetKeyDown("tab") || Input.GetKeyDown("q") || Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("caps lock") || Input.GetKeyDown("<") || Input.GetKeyDown("a") || Input.GetKeyDown("y"))
            {

                selectItem(1);
            }
            else if (Input.GetKeyDown("e") || Input.GetKeyDown("r") || Input.GetKeyDown("t") || Input.GetKeyDown("d") || Input.GetKeyDown("f") || Input.GetKeyDown("g") || Input.GetKeyDown("x") || Input.GetKeyDown("c") || Input.GetKeyDown("v"))
            {

                selectItem(2);
            }
            else if (Input.GetKeyDown("z") || Input.GetKeyDown("u") || Input.GetKeyDown("i") || Input.GetKeyDown("h") || Input.GetKeyDown("j") || Input.GetKeyDown("k") || Input.GetKeyDown("b") || Input.GetKeyDown("n") || Input.GetKeyDown("m"))
            {

                selectItem(3);
            }
            else if (Input.GetKeyDown("o") || Input.GetKeyDown("p") || Input.GetKeyDown("l")  || Input.GetKeyDown(",") || Input.GetKeyDown(".") || Input.GetKeyDown("-"))
            {

                selectItem(4);
            }

         }

      if (gameState == GameState.DEFENDING) {
         RaycastHit hit;
         Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
         Trap temp = null;
         if (Physics.Raycast (ray, out hit, 1000, castLayerMask)) {
            float minDist = itemSelectThreshold;
            for (int i = 0; i < itemsList.Count; i++) {
                if (itemsList[i].selectable())
                {
                    float distance = Vector3.Distance(itemsList[i].transform.position, hit.point);
                    if (distance < minDist)
                    {
                        minDist = distance;
                        temp = itemsList[i];
                    }
                }
            }
         }

        if (temp != null && temp != selectedItem)
        {
            if (selectedItem != null)
            {
                selectedItem.highlight(false);
            }
            selectedItem = temp;
            if (selectedItem != null)
            {
                selectedItem.highlight(true);
            }
        }
        if (temp == null && selectedItem != null)  {
            selectedItem.highlight(false);
            selectedItem = null;
         }
            

         if (selectedItem != null) {
            if (Input.GetKeyDown ("space")) {
               selectedItem.launch();
               if(selectedItem.name == "BombBall(Clone)")
                {
                    itemsList.Remove(selectedItem);
                }
            }
         }

      }
         
    }
    
}

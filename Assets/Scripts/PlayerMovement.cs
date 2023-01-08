using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    public InputMaster inputMaster;
    public Vector3 lookDir;
    public float maxSpeed;
    public float accelForce;
    public float deccelForce;

    Vector3 moveDir;
    float jump;

    Rigidbody rb;
    ShopManager shopManager;
    // Start is called before the first frame update
    void Awake(){

        inputMaster = new InputMaster();
        rb = GetComponent<Rigidbody>();
        shopManager = GetComponent<ShopManager>();

        if(FindObjectsOfType<PlayerMovement>().Length > 1){

            Destroy(gameObject);

        }
        
        DontDestroyOnLoad(gameObject);

    }

    private void OnEnable(){

        inputMaster.Enable();

    }
    private void OnDisable(){

        inputMaster.Disable();

    }

    // Update is called once per frame
    void Update(){

        moveDir = inputMaster.Player.MoveDirection.ReadValue<Vector2>();
        jump = inputMaster.Player.Jump.ReadValue<float>();
        RotatePlayer();
        
    }

    void FixedUpdate() {

        MovePlayer();

        if(rb.velocity.magnitude >= maxSpeed){

            rb.AddForce(-rb.velocity.normalized * deccelForce);

        }
        
    }

    void RotatePlayer(){

        lookDir = new Vector3(moveDir.x,0,moveDir.y).normalized;
        
        //Debug.Log(lookDir);

        if(moveDir.x != 0 || moveDir.y != 0){

            transform.rotation = Quaternion.LookRotation(lookDir) * Quaternion.Euler(new Vector3(0,45f,0));

        }

    }

    void MovePlayer(){

        if(moveDir.x != 0
         || moveDir.y != 0){

            rb.AddForce(transform.forward * accelForce * lookDir.magnitude);

        } else {

            rb.AddForce(-rb.velocity.normalized * deccelForce);

        }

    }

    void LeaveShop(){

        shopManager.DisableUIPanel();

    }

}

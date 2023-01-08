using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public float value;

    void OnTriggerEnter(Collider collider){

        if(collider.transform.root.tag == "Player"){

            collider.transform.root.GetComponent<PlayerInventory>().playerWallet += value;

        }

    }

}

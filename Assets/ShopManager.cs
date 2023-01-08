using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public List<ShopItem> shopItems;
    public List<PaymentOption> paymentOptions;
    public Transform shopPanel;
    public bool microtransactionShop;

    void Start(){

        DisableUIPanel();

    }
    
    void EnableUIPanel(){

        shopPanel.gameObject.SetActive(true);

    }

    public void DisableUIPanel(){

        shopPanel.gameObject.SetActive(false);

    }


    public void OnTriggerEnter(Collider collider){

        if(collider.transform.root.tag == "Player"){

            EnableUIPanel();

        }

    }

    public void OnTriggerExit(Collider collider){

        if(collider.transform.root.tag == "Player"){

            DisableUIPanel();

        }

    }

}

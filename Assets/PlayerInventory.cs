using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public float playerWallet;
    public List<ShopItem> playerItems;
    public Transform itemParent;
    UIManager uIManager;

    public void OnEnable(){

        playerWallet = PlayerPrefs.GetFloat("PlayerWallet",500);

    }

    void OnDisable(){

        PlayerPrefs.SetFloat ("PlayerWallet", playerWallet);
        PlayerPrefs.Save();

    }

    public void EquipItems(){

        for(int i = 0; i < itemParent.childCount;i++){

            Destroy(itemParent.GetChild(i).gameObject);
            
        }

        foreach(ShopItem item in playerItems){

            GameObject itemObj = (GameObject)Instantiate(item.itemPrefab,transform.position,transform.rotation);
            itemObj.transform.parent = itemParent;
            itemObj.transform.localPosition = Vector3.zero;

        }

    }

    public ItemType PlayerHasItemOfType(ShopItem item){

        if(playerItems.Find(x => x.itemType == item.itemType)){

            return playerItems.Find(x => x.itemType == item.itemType).itemType;

        } else {

            return ItemType.NONE;

        }

    }

    public ShopItem FindItemOfType(ItemType itemType){

        if(playerItems.Find(x => x.itemType == itemType)){

            return playerItems.Find(x => x.itemType == itemType);

        } else {

            return null;

        }


    }

}

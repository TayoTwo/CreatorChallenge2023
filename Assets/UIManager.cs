using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("Transforms")]
    public Transform shopContainer;
    public Transform playerInventoryContainer;

    public Transform bundleContainer;
    public Transform shopItemPrefab;
    public Transform bundleItemPrefab;

    [Header("UI Settings")]
    public float shopItemOffset;
    public float shopItemHeight;

    public float bundleOptionOffset;
    public float bundleWidth;

    [Header("UI")]

    public TMP_Text playerWalletText;

    public ShopManager shop;
    public ShopManager microtransactionShop;
    public PlayerInventory playerInventory;

    public void Start(){

        RefreshUI();

    }

    void RefreshUI(){

        for(int i = 0; i < shopContainer.childCount;i++){

            Destroy(shopContainer.GetChild(i).gameObject);

        }

        for(int i = 0; i < shop.shopItems.Count;i++){

            CreateItemButton(shop.shopItems[i],i,shopContainer);

        }

        for(int i = 0; i < playerInventoryContainer.childCount;i++){

            Destroy(playerInventoryContainer.GetChild(i).gameObject);

        }

        for(int i = 0; i < playerInventory.playerItems.Count;i++){

            CreateItemButton(playerInventory.playerItems[i],i,playerInventoryContainer);

        }

        //Setup payment UI


        for(int i = 0; i < bundleContainer.childCount;i++){

            Destroy(bundleContainer.GetChild(i).gameObject);

        }


        for(int i = 0; i < microtransactionShop.paymentOptions.Count;i++){

            CreatePaymentOption(microtransactionShop.paymentOptions[i],i,bundleContainer);

        }

        playerWalletText.text = '$' + playerInventory.playerWallet.ToString();

    }

    void CreateItemButton(ShopItem item, int positionIndex, Transform container){

        Transform shopItemTransform = Instantiate(shopItemPrefab,container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        shopItemRectTransform.anchoredPosition = new Vector2(0,shopItemOffset + (-shopItemHeight * positionIndex));

        shopItemTransform.Find("Name").GetComponent<TMP_Text>().SetText(item.itemName);
        shopItemTransform.Find("Price").GetComponent<TMP_Text>().SetText("$" + item.itemPrice.ToString());

        if(container == shopContainer){

            shopItemTransform.GetComponent<Button>().onClick.AddListener( delegate{BuyItem(item);});

        } else {

            shopItemTransform.GetComponent<Button>().onClick.AddListener( delegate{SellItem(item);});

        }


    }

    public void BuyItem(ShopItem item){

        if(playerInventory.playerWallet >= item.itemPrice){
            //Player can buy item
            Debug.Log("Player has bought " + item.itemName);

            if(playerInventory.PlayerHasItemOfType(item) != ItemType.NONE){

                SellItem(playerInventory.FindItemOfType(playerInventory.PlayerHasItemOfType(item)));

            }

            playerInventory.playerWallet -= item.itemPrice;

            playerInventory.playerItems.Add(item);
            shop.shopItems.Remove(item);

            RefreshUI();

        } else {

            Debug.Log("Player cannot afford " + item.itemName);

        }

        playerInventory.EquipItems();

    }

    public void SellItem(ShopItem item){

        //Player can buy item
        Debug.Log("Player has sold " + item.itemName);

        playerInventory.playerWallet += item.itemPrice;

        shop.shopItems.Add(item);
        playerInventory.playerItems.Remove(item);

        RefreshUI();

        playerInventory.EquipItems();

    }

    void CreatePaymentOption(PaymentOption option, int positionIndex, Transform container){

        Transform paymentOptionTransform = Instantiate(bundleItemPrefab,bundleContainer);
        RectTransform paymentOptionRectTransform = paymentOptionTransform.GetComponent<RectTransform>();
        paymentOptionRectTransform.anchoredPosition = new Vector2(bundleOptionOffset + (-bundleWidth * positionIndex),0);

        paymentOptionTransform.Find("Name").GetComponent<TMP_Text>().SetText(option.name);
        paymentOptionTransform.Find("Real Price").GetComponent<TMP_Text>().SetText("£" + option.realPrice.ToString());
        paymentOptionTransform.Find("Gold Amount").GetComponent<TMP_Text>().SetText("$" + option.goldAmount.ToString());

        paymentOptionTransform.GetComponent<Button>().onClick.AddListener( delegate{BuyBundle(option);});

    }

    void BuyBundle(PaymentOption paymentOption){

        playerInventory.playerWallet += paymentOption.goldAmount;
        RefreshUI();

    }

}

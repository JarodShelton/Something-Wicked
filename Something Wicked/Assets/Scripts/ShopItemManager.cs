using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemManager : MonoBehaviour
{
    public GameObject purchaseUI;
    public int candyCost;

    private bool playerInRange = false;

    ShopManager shopManager;
    PowerUpManager powerUpManager;

    public GameObject buyParticle;

    // Start is called before the first frame update
    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>().GetComponent<ShopManager>();
        powerUpManager = FindObjectOfType<PowerUpManager>().GetComponent<PowerUpManager>();

        UpdateText();
        purchaseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange){
            Purchase();
        }
    }

    void Purchase(){
        //Check if purchase is valid
        
        if(powerUpManager.weaponLevel < 4  && StatManager.Stats.CandyAmount > shopManager.UpgradeCosts[powerUpManager.weaponLevel]){
            Debug.Log("Purchased Weapon");
            
            AudioManager.PlayMusic("test");
            StatManager.Stats.changeCandyAmount(-shopManager.UpgradeCosts[powerUpManager.weaponLevel]);
            FindObjectOfType<UIManager>().GetComponent<UIManager>().UpdateUI();
            
            powerUpManager.weaponLevel++;
            UpdateText();
            Instantiate(buyParticle, transform.position, transform.rotation);
        }
    }

    void UpdateText(){
        if(powerUpManager.weaponLevel >= 4){
            purchaseUI.GetComponentInChildren<Text>().text = "MAX UPGRADE REACHED";
        }else{
            candyCost = shopManager.UpgradeCosts[powerUpManager.weaponLevel];
            purchaseUI.GetComponentInChildren<Text>().text = "PRESS [E] TO UPGRADE WEAPONS FOR " + candyCost + " CANDY";
        }
        

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInRange = true;
            purchaseUI.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInRange = false;
            purchaseUI.SetActive(false);
        }
    }
}

using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData Data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite=Data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        
        textName.text=Data.itemName;

    }
    private void OnEnable()
    {
        textLevel.text = "Lv." + (level);
        switch (Data.itemType)
        {
            case ItemData.ItemType.Melle:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(Data.itemDesc, Data.damages[level]*100, Data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(Data.itemDesc, Data.damages[level]*100);
                break;
            case ItemData.ItemType.Heal:
                textDesc.text = string.Format(Data.itemDesc);
                break;
            default:
                textDesc.text = string.Format(Data.itemDesc);
                break;

        }
    }


    public void OnClick()
    {
        switch (Data.itemType)
        {
            case ItemData.ItemType.Melle:
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon= newWeapon.AddComponent<Weapon>();
                    weapon.Init(Data);
                    
                }
                else
                {
                    float nextDamage=Data.baseDamage;
                    int nextCount = 0;
                    nextDamage += Data.baseDamage* Data.damages[level];
                    nextCount += Data.counts[level];

                    weapon.levelUp(nextDamage, nextCount);
                }
                level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(Data);
                }
                else
                {
                    float nextRate=Data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.player.Health =GameManager.instance.player.maxHealth;
                break;

        }

        
        if(level == Data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
        
    }
}

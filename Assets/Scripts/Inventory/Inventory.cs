using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Variables
    //list
    public static List<Item> inv = new List<Item>();
    public static bool showInv;
    public Item selectedItem;
    //public Vector2 scr;
    public static int money;
    public Vector2 scrollPos;
    public string sortType = "All";
    public string[] typeNames = new string[9] { "All", "Armour", "Weapon", "Potion", "Food", "Ingredients", "Craftable", "Quest", "Misc" };
    public Transform dropLocation;
    [System.Serializable]
    public struct EquippedItems
    {
        public string slotName;
        public Transform equippedLocation;
        public GameObject equippedItem;
    };
    public EquippedItems[] equippedItemsSlot;
    /*
      IMGUIScript.scr.x
        IMGUIScript.scr.y
    */
    #endregion

    //start - setting up items
    private void Start()
    {
        //shows item in pause menu
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(100));
        inv.Add(ItemData.CreateItem(102));
        inv.Add(ItemData.CreateItem(200));
        inv.Add(ItemData.CreateItem(300));
        inv.Add(ItemData.CreateItem(301));
        inv.Add(ItemData.CreateItem(400));
        inv.Add(ItemData.CreateItem(500));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(0));
    }
    //update - toggle inv and add more items
    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            inv.Add(ItemData.CreateItem(Random.Range(0, 3)));
        }
    }
    //Ongui - 
    private void OnGUI()
    {
        for (int i = 0; i < typeNames.Length; i++)
        {
            if (GUI.Button(new Rect(4 * IMGUIScript.scr.x + i * 1.5f, 0, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), typeNames[i]))
            {
                sortType = typeNames[i];
            }
        }
        DisplayInv();
        if (selectedItem != null)
        {
            UseItem();
        }
    }
    //DisplayInv
    void DisplayInv()
    {
        //if we have a Type selected (filter)
        if (!(sortType == "All" || sortType == ""))
        {
            ItemTypes type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), sortType);
            //the amount of this type
            int a = 0;
            //new slot position of the item
            int s = 0;
            //fine all items of type in our inv
            for (int i = 0; i < inv.Count; i++)
            {
                //if current element matches type
                if (inv[i].ItemType == type)
                {
                    //add to amount of this type
                    a++;
                }
            }
            //display our type that has been filtered if its under 34
            if (a <= 34)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].ItemType == type)
                    {
                        if (GUI.Button(new Rect(IMGUIScript.scr.x * 0.5f, 0.25f * IMGUIScript.scr.y + s * (0.25f * IMGUIScript.scr.y), IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 0.25f), inv[i].Name))
                        {
                            selectedItem = inv[i];
                        }
                    }
                }
            }
            else
            {
                //our move position of our scroll window
                scrollPos =
                //the start of our scroll view
                GUI.BeginScrollView(
                //our position and size of our window
                new Rect(0, 0.25f * IMGUIScript.scr.y, 3.75f * IMGUIScript.scr.x, 8.5f * IMGUIScript.scr.y),
                //our current position in the scroll view
                scrollPos,
                //viewable area
                new Rect(0, 0, 0, a * 0.25f * IMGUIScript.scr.y),
                //can we see our Horiztonal bar?
                false,
                //can we see our Vertical bar?
                true);
                #region loop inside scroll space
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(IMGUIScript.scr.x * 0.5f, s * (0.25f * IMGUIScript.scr.y), IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 0.25f), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                    s++;
                }
                #endregion
                GUI.EndScrollView();
            }
        }
        //All items are shown
        else
        {
            if (inv.Count <= 34)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(IMGUIScript.scr.x*0.5f, 0.25f * IMGUIScript.scr.y + (i * 0.25f * IMGUIScript.scr.y), IMGUIScript.scr.x*3, IMGUIScript.scr.y*0.25f),inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
            }
            //we have more items then screen space
            else
            {
                //our move position of our scroll window
                scrollPos =
                //the start of our scroll view
                GUI.BeginScrollView(
                //our position and size of our window
                new Rect(0, 0.25f * IMGUIScript.scr.y, 3.75f * IMGUIScript.scr.x, 8.5f * IMGUIScript.scr.y),
                //our current position in the scroll view
                scrollPos,
                //viewable area
                new Rect(0, 0, 0, inv.Count * 0.25f * IMGUIScript.scr.y),
                //can we see our Horiztonal bar?
                false,
                //can we see our Vertical bar?
                true);
                #region loop inside scroll space
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(IMGUIScript.scr.x * 0.5f, i * (0.25f * IMGUIScript.scr.y), IMGUIScript.scr.x * 3, IMGUIScript.scr.y * 0.25f), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
                #endregion
                GUI.EndScrollView();
            }
        }
    }
    //UseItem
    void UseItem()
    {
        //name
        GUI.Box(new Rect(4f*IMGUIScript.scr.x, 0.25f*IMGUIScript.scr.y, 3*IMGUIScript.scr.x, 0.25f*IMGUIScript.scr.y), selectedItem.Name);
        //icon
        GUI.Box(new Rect(4f * IMGUIScript.scr.x, 0.5f * IMGUIScript.scr.y, 3 * IMGUIScript.scr.x, 3 * IMGUIScript.scr.y), selectedItem.IconName);
        //description, amount, value
        GUI.Box(new Rect(4f * IMGUIScript.scr.x, 3.5f * IMGUIScript.scr.y, 3 * IMGUIScript.scr.x, 0.75f * IMGUIScript.scr.y), selectedItem.Description + "\nAmount: " + selectedItem.Amount+"\nValue: $"+selectedItem.Value);
        //switch via type
        switch (selectedItem.ItemType)
        {
            case ItemTypes.Armour:
                if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y),"Wear"))
                {
                    //wear the thing

                }
                break;
            case ItemTypes.Weapon:
                if (equippedItemsSlot[1].equippedItem == null || selectedItem.Name != equippedItemsSlot[1].equippedItem.name)//this is checking the weapon and if its already equipped we shall check the slot
                {
                    if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Equip"))
                    {
                        //Equip the thing
                        if (equippedItemsSlot[1].equippedItem != null)
                        {
                            Destroy(equippedItemsSlot[1].equippedItem);
                        }
                        equippedItemsSlot[1].equippedItem = Instantiate(selectedItem.MeshName, equippedItemsSlot[1].equippedLocation);
                        equippedItemsSlot[1].equippedItem.name = selectedItem.Name;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Unequipped"))
                    {
                        Destroy(equippedItemsSlot[1].equippedItem);
                        equippedItemsSlot[1].equippedItem = null;
                    }
                }
                break;
            case ItemTypes.Potion:
                if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Drink"))
                {
                    //Drink the thing

                }
                break;
            case ItemTypes.Money:
                break;
            case ItemTypes.Quest:
                break;
            case ItemTypes.Food:
                if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Eat"))
                {
                    //consume the thing

                }
                break;
            case ItemTypes.Ingredient:
                if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Cook"))
                {
                    //cook the thing

                }
                break;
            case ItemTypes.Craftable:
                if (GUI.Button(new Rect(4f * IMGUIScript.scr.x, 4.25f * IMGUIScript.scr.y, 1.5f * IMGUIScript.scr.x, 0.25f * IMGUIScript.scr.y), "Craft"))
                {
                    //craft the thing

                }
                break;
            case ItemTypes.Misc:
                break;
            default:
                break;
        }
        //discard button
    }
}

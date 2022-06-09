using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UserInterface;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Level level;
    public InventoryObject inventory;
    public InventoryObject equipment;
    public GameObject Panel;
    public GameObject Panel2;
    public bool panelIsClosed;
    public float moveSpeed = 5f;
    public Transform movePoint;
    public int gold;

    public LayerMask whatStopsMovement;

    public Attribute[] attributes;

    void Start()
    {
        panelIsClosed = true;

        movePoint.parent = null;
        level = new Level(1, OnLevelUp);

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }

    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void OnLevelUp()
    {
        print("I leveled up!");
    }

    void Update()
    {
        #region Open Inventory
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if(panelIsClosed == true)
            {
                Panel.SetActive(true);
                Panel2.SetActive(true);
                panelIsClosed = false;
            }
            else
            {
                Panel.SetActive(false);
                Panel2.SetActive(false);
                panelIsClosed = true;
            }
        }
        #endregion

        //Test Exp 
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            level.AddExp(100);
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }

            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }


        }

        #region Save and Load
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.Save();
            equipment.Save();
            Debug.Log("Game Saved");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.Load();
            equipment.Load();
            Debug.Log("Game Loaded");
        }
        #endregion

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if(inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }

}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public PlayerMovement parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(PlayerMovement _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }
    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}

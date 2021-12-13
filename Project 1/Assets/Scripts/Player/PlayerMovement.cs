using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UserInterface;

public class PlayerMovement : MonoBehaviour
{
    public Level level;
    public InventoryObject inventory;
    public InventoryObject equipment;
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

    void Start()
    {
        movePoint.parent = null;
        level = new Level(1, OnLevelUp);
    }

    public void OnLevelUp()
    {
        print("I leveled up!");
    }

    void Update()
    {
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

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equipment.Container.Clear();
    }



}

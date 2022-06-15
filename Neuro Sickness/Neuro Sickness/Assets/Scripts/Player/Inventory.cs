using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObjectArrayVariable slots;
    public GameObjectVariable player;
    public GameObjectVariable collectable;
    public IntVariable ItemInHand;
    public BoolVariable canControl;

    public Sprite slotSprite;
    public Image[] itemSlot;

    public GameEvent pickUpItem;
    public GameEvent inventoryFull;
    public GameEvent tooFar;
    public GameEvent dangerArea;
    public SoundVariable collectSound, dropSound;
   
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (canControl.Value)
        {
            SelectItem();
        }
    }

    public void Collect()
    {
        if (collectable.Value != null) 
        {
            for (int i = 0; i < slots.Value.Length; i++)
            {
                if (slots.Value[i] == null)
                {
                    _audio.clip = collectSound.Value;
                    _audio.Play();
                    pickUpItem.Raise();
                    slots.Value[i] = collectable.Value;
                    itemSlot[i].sprite = collectable.Value.GetComponent<SpriteRenderer>().sprite;
                    itemSlot[i].material = collectable.Value.GetComponent<SpriteRenderer>().material;
                    itemSlot[i].preserveAspect = true;
                    itemSlot[i].transform.localScale = new Vector3(2, 2, 1);
                    collectable.Value.SetActive(false);
                    collectable.Value = null;
                    i = itemSlot.Length + 1;
                }
                else if (slots.Value[i] != null && i >= slots.Value.Length - 1)
                {
                    inventoryFull.Raise();
                }
            }
        }
    }

    public void Drop()
    {
        _audio.clip = dropSound.Value;
        _audio.Play();
        itemSlot[ItemInHand.Value].sprite = slotSprite;
        itemSlot[ItemInHand.Value].transform.localScale = new Vector3(1, 1, 1);
        slots.Value[ItemInHand.Value].SetActive(true);
        slots.Value[ItemInHand.Value].transform.position = player.Value.transform.position;
        slots.Value[ItemInHand.Value] = null;
    }

    public void SelectItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetItemInHand(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetItemInHand(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetItemInHand(2);
        }
        switch (ItemInHand.Value)
        {
            case 0:
                if(slots.Value[ItemInHand.Value] != null)
                {
                    itemSlot[0].transform.localScale = new Vector3(2.5f, 2.5f, 1);
                }
                break;
            case 1:
                if (slots.Value[ItemInHand.Value] != null)
                {
                    itemSlot[1].transform.localScale = new Vector3(2.5f, 2.5f, 1);
                }
                break;
            case 2:
                if (slots.Value[ItemInHand.Value] != null)
                {
                    itemSlot[2].transform.localScale = new Vector3(2.5f, 2.5f, 1);
                }
                break;
            default:
                break;
        }
    }

    public void SetItemInHand(int slot)
    {
        if (slots.Value[ItemInHand.Value] != null)
        {
            itemSlot[ItemInHand.Value].transform.localScale = new Vector3(2, 2, 1);
        }
        else
        {
            itemSlot[ItemInHand.Value].transform.localScale = new Vector3(1, 1, 1);
        }
        ItemInHand.Value = slot;
    }
}

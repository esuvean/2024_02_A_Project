using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int crystalCount = 0;
    public int plantCount = 0;
    public int bushCount = 0;
    public int treeCount = 0;

    public void AddIten(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Crystal:
                crystalCount++;
                Debug.Log($"Å©¸®½ºÅ» È¹µæ ! ÇöÀç °³¼ö : {crystalCount}");
                break;
            case ItemType.Plant:
                plantCount++;
                Debug.Log($"Å©¸®½ºÅ» È¹µæ ! ÇöÀç °³¼ö : {plantCount}");
                break;
            case ItemType.Bush:
                bushCount++;
                Debug.Log($"Å©¸®½ºÅ» È¹µæ ! ÇöÀç °³¼ö : {bushCount}");
                break;
            case ItemType.Tree:
                treeCount++;
                Debug.Log($"Å©¸®½ºÅ» È¹µæ ! ÇöÀç °³¼ö : {treeCount}");
                break;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("=======ÀÎº¥Åä¸®=======");
        Debug.Log($"Å©¸®½ºÅ» : {crystalCount}°³");
        Debug.Log($"½Ä¹° : {plantCount}°³");
        Debug.Log($"¼öÇ® : {bushCount}°³");
        Debug.Log($"³ª¹« : {treeCount}°³");
        Debug.Log("======================");
    }
}

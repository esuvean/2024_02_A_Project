using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemType itemType;
    public string itemName;
    public float respawnTime = 30.0f;
    public bool canCollect = true;

    public void CollectItem(PlayerInventory inventory)
    {
        if (!canCollect) return;

        inventory.AddItem(itemType);

        if (FloatingTextMananger.instance != null)
        {
            Vector3 textPosting = transform.position + Vector3.up * 0.5f;
            FloatingTextMananger.instance.Show($"+{itemName}", textPosting);
        }

        Debug.Log($"{itemName} 수집 완료");
        StartCoroutine(RespawnRoutione());
    }

    public IEnumerator RespawnRoutione()
    {
        canCollect = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        yield return new WaitForSeconds(respawnTime);

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<MeshCollider>().enabled = true;
        canCollect = true;
    }
}

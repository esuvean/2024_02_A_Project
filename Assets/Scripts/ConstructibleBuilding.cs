using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructibleBuilding : MonoBehaviour
{
    [Header("Building Settings")]
    public BuildingType buildingType;
    public string buildingName;
    public int requiredTree = 5;
    public float constructionTime = 2.0f;

    public bool canBuild = true;
    public bool isConstructed = false;

    private Material buildingMaterial;

    // Start is called before the first frame update
    void Start()
    {
        buildingMaterial = GetComponent<MeshRenderer>().material;
        Color color = buildingMaterial.color;
        color.a = 0.5f;
        buildingMaterial.color = color;
    }

    private IEnumerator CostructionRoutine()
    {
        canBuild = false;
        float timer = 0;

        Color color = buildingMaterial.color;

        while(timer < constructionTime)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f, 1f, timer / constructionTime);
            buildingMaterial.color = color;
            yield return null;
        }
        isConstructed = true;

        if(FloatingTextMananger.instance ! = null)
        {
            FloatingTextMananger.instance.Show($"{buildingName} �Ǽ� �Ϸ� !", transform.position + Vector3.up);
        }
    }

    public void StartConstruction(PlayerInventory inventory)
    {
        if(!canBuild || isConstructed) return;

        if(inventory.treeCount >= requiredTree)
        {
            inventory.RemoveItem(ItemType.Tree, requiredTree);
            if(FloatingTextMananger.instance != null)
            {
                FloatingTextMananger.instance.Show($"{buildingName} �Ǽ� ���� !", transform.position + Vector3.up);
            }
        }
        else
        {
            if(FloatingTextMananger.instance != null)
            {
                FloatingTextMananger.instance.Show(
                    $"������ �����մϴ�! ({inventory.treeCount} / {requiredTree})", transform.position + Vector3.up);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
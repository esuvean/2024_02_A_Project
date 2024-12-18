using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;
    public Vector3 lastPostion;
    public float moveThreshold = 0.1f;
    public ConstructibleBuilding currentNearbyBuilding;
    public BuildingCrafter currenBuildingCrafter;

    private void CheckForBuilding()
    {
        Collider[]hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;
        ConstructibleBuilding closestBuilding = null;
        BuildingCrafter closesCrafter = null;

        foreach (Collider collier in hitColliders)
        {
            ConstructibleBuilding building = collier.GetComponent<ConstructibleBuilding>();
            if (building != null && building.canBuild && !building.isConstructed)
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);
                if (building != null)
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closesCrafter = building.GetComponent<BuildingCrafter>();
                }
            } 
            if(closestBuilding != currentNearbyBuilding)
            {
                currentNearbyBuilding = closestBuilding;
                currenBuildingCrafter = closesCrafter;

                if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstructed)
                {
                    if (FloatingTextMananger.instance != null)
                    {
                        Vector3 textPostion = transform.position + Vector3.up * 0.5f;
                        FloatingTextMananger.instance.Show(
                            $"[F] 키로 {currentNearbyBuilding.buildingName} 건설 (나무 {currentNearbyBuilding.requiredTree} 개 필요)"
                            , currentNearbyBuilding.transform.position + Vector3.up);
                    }

                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPostion = transform.position;
        CheckForBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastPostion, transform.position) > moveThreshold)
        {
            CheckForBuilding();
            lastPostion= transform.position;
        }

        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if (!currentNearbyBuilding.isConstructed)
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());
            }
            else if(currenBuildingCrafter != null)
            {
                Debug.Log($"{currentNearbyBuilding.buildingName} 의 제작 메뉴 열기");
                CraftingUIManager.Instance?.ShowUI(currenBuildingCrafter);
            }
        } 
    }
}

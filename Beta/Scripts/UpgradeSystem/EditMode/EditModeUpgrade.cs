using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModeUpgrade : MonoBehaviour
{
    public static EditModeUpgrade Instance;
    private bool edit = false;
    public Transform cameratransform;
    public GameObject selectedPrefab;
    public StorageInfo selectedStorageInfo;
    public LayerMask lm;
    RaycastHit hit;
    float roteuler = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if(edit)
        {
            if (selectedPrefab != null)
            {
                if (Physics.Raycast(cameratransform.position, cameratransform.forward, out hit, 1000, lm))
                {
                    BuildGrid bg = hit.collider.GetComponent<BuildGrid>();
                    selectedPrefab.SetActive(true);
                    selectedPrefab.transform.position = bg.CellToWorldPos(bg.WorldToCellPos(hit.point));
                    selectedPrefab.transform.rotation = hit.collider.gameObject.transform.rotation * Quaternion.Euler(0, roteuler, 0);
                }
                else
                {
                    selectedPrefab.SetActive(false);
                }
            }
        }
    }

    public void UpdateEdit(bool ed)
    {
        Destroy(selectedPrefab);
        selectedPrefab = null;
        edit = ed;
    }

    public void Rotate()
    {
        roteuler += 90;
        if (roteuler >= 360)
            roteuler = 0;
    }

    public void Select(StorageInfo si)
    {
        Destroy(selectedPrefab);
        selectedPrefab = Instantiate(si.Prefab);
        selectedPrefab.SetActive(false);
        selectedStorageInfo = si;
    }

    public void Place()
    {
        if(selectedPrefab.activeSelf)
        {
            selectedPrefab = null;
            Inventory.ChangeCountOfStorages(selectedStorageInfo.Id, -1);
        }
    }
}

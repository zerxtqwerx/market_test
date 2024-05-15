using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    GameObject target;
    Transform characterTransform;
    bool pickupTrigger;

    void Start()
    {
        characterTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        pickupTrigger = false;
    }

    private void OnMouseDown()
    {
        if (pickupTrigger == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1))
            {
                target = transform.gameObject;

                if (target.tag == "Pickable")
                {
                    target.transform.SetParent(characterTransform);
                    target.transform.position = new Vector3(1, 0, 1);
                    pickupTrigger = true;
                }
            }
        }
    }
}

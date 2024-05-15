/*using UnityEngine;

public class AddingProductsObserver : MonoBehaviour
{
    [SerializeField] private List<ItemGameObject> actionClass;
    [SerializeField] private LevelProductsList listener;

    ItemGameObject itemGameObject;

    private void OnEnable()
    {
        actionClass.OnAddingProducts += AddProduct;
    }
    private void OnDisable()
    {
        actionClass.OnAddingProducts -= AddProduct;
    }

    private void AddProduct()
    {
        try
        {
            actionClass.TargetData(itemGameObject);
            listener.AddProduct(itemGameObject);
        }
        catch
        {
            Debug.LogError("AddingProductsObserver / AddProduct");
        }
    }
}
*/
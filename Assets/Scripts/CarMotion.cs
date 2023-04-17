using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMotion : MonoBehaviour
{
    public static CarMotion Instance { get; private set; }
    
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float stopPosition = -6f;
    [SerializeField] private float destroyPosition = 36f;
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private Vector3 spawnPosition;

    public bool deliverySuccess = false;
    private GameObject spawnedObject; // reference to the instantiated object
    private void Start()
    {
        Instance = this;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        int randomIndex = Random.Range(0, objectsToSpawn.Length); // generate a random index
        spawnedObject = Instantiate(objectsToSpawn[randomIndex], spawnPosition, Quaternion.identity); // initialize the spawnedObject variable
        speed = Random.Range(10, 21);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        Debug.Log("DeliveryManager_OnRecipeSuccess Fired!!!!!");
        deliverySuccess = true;
        speed = Random.Range(10, 21);
    }

    void Update()
    {
        if (KitchenGameManager.Instance.IsGamePlaying())
        {
            if (spawnedObject != null)
            {
                if (spawnedObject.transform.position.z >= stopPosition && !deliverySuccess)
                {
                    speed = 0.0f;
                }

                if (spawnedObject.transform.position.z >= destroyPosition)
                {
                    deliverySuccess = false;
                    int randomIndex = Random.Range(0, objectsToSpawn.Length); // generate a random index
                    Destroy(spawnedObject.gameObject);
                    spawnedObject = Instantiate(objectsToSpawn[randomIndex], spawnPosition, Quaternion.identity);
                    speed = Random.Range(10, 21);
                }
                spawnedObject.transform.Translate(Vector3.forward * speed * Time.deltaTime); // move the prefab forward
            }
        }

    }


}

using UnityEngine;

public class AsyncGPULoading : MonoBehaviour
{
    //Attach Child gameObjects and Camera for FrustumCulling
    public GameObject[] gameObjects;
    public Camera MainCamera;
    private void Awake()
    {
        MainCamera = Camera.main;
        if(MainCamera == null || gameObjects.Length == 0)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        for (int i = 0; i < gameObjects.Length; i++) {
            gameObjects[i].SetActive(false);
        }
    }
    private void FrustumCulling(GameObject gameObject)
    {
        if (IsVisibleFromCamera(MainCamera,gameObject))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public static bool IsVisibleFromCamera(Camera camera,GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), renderer.bounds);
    }
    private void Update()
    {
        for(int index = 0;index < gameObjects.Length;index++)
        {
            FrustumCulling(gameObjects[index]);
        }
    }
}

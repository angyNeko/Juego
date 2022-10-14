using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class AutoScreenShot : MonoBehaviour
{
    public bool takeScreenshot;
    public bool nextItem;
    public Queue objectQueue;

    [SerializeField]
    GameObject[] gameObjects;
    [SerializeField]
    Transform parentTransform;

    AutoScreenShot instance;

    private void Awake()
    {
        parentTransform = GetComponent<Transform>();
    }

    public void Start()
    {
        instance = this;
        RecordFrame();

         = new Queue();

        foreach(var obj in gameObjects)
        {
            objectQueue.Enqueue(obj);
        }
    }

    private void Update()
    {
        if (nextItem)

        if (takeScreenshot)
            RecordFrame();
    }

    private void LoadItem()
    {
        GameObject model = GameObject.Instantiate(objectQueue, parentTransform) as GameObject;
    }

    private void RecordFrame()
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Data/2D Assets (Raw)/Item Icons");
        foreach(GameObject objecte in gameObjects)
        {
            

            model.SetActive(true);
            Debug.Log("Ready to take screenshot");
            string fullPath = Path.Combine(dir.FullName, objecte.name + ".png");

            ScreenCapture.CaptureScreenshot(fullPath);
            string name = objecte.name;

            Debug.Log(fullPath);

            GameObject.Destroy(model);
        }
    }
}

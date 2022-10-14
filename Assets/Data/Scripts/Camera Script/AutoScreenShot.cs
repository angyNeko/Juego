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
}

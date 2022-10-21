using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class Activatable : MonoBehaviour
    {
        public virtual void DoSomething()
        {
            Debug.Log("Object Activated");
        }
    }
}
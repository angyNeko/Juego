using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class ActivateDoor : Activatable
    {
        public override void DoSomething()
        {
            Destroy(this.gameObject);
        }
    }
}
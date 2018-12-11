using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Customcommand
{
    class mono : MonoBehaviour
    {
        public Vector3 Size;

        public virtual void Start()
        {
            transform.localScale = Size;
        }
        public void Setsize(float scalex, float scaley, float scalez)
        {
            Size = new Vector3(scalex, scaley, scalez);
            transform.localScale = Size;
        }
    }
}

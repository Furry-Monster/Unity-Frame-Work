using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is used to pack some utilities into one script file
//so that you can reach them easily by typing "Utilities." in your code editor

namespace Utilities.Helper
{
    public class Helper
    {
        public void CleanChildren(Transform parent)
        {
            Transform[] children = parent.GetComponentsInChildren<Transform>();

            foreach (Transform child in children)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        
    }
}

namespace Utilities.Math
{
    public class Math
    {
        
    }

}

namespace Utilities.Random
{
    public class Random
    {

    }
}

namespace Utilities.Time
{
    public class Time
    {

    }
}


namespace Utilities.Serialization
{
    public class Serialization
    {

    }
}

namespace Utilities.String
{
    public class String
    {

    }
}

namespace Utilities.Debug
{
    public class Debug
    {

    }
}

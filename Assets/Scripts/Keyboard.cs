using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public static bool UP {
        get {
            return Input.GetKey(KeyCode.UpArrow);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    

    public void Rotate()
    {
        transform.Rotate(Vector3.up * 2);
    }
    

}

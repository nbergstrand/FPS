using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandPosition : MonoBehaviour
{
    [SerializeField]
    Transform _handTransform;
    

    void LateUpdate()
    {
                
        transform.position = _handTransform.position;
        transform.rotation = Quaternion.Euler(_handTransform.rotation.eulerAngles);

    }
}

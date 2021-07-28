using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandPosition : MonoBehaviour
{
    [SerializeField]
    Transform _handTransform;

    [SerializeField]
    float _lerpSpeed;


    void LateUpdate()
    {

        Vector3 targetPosition = _handTransform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _lerpSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(_handTransform.rotation.eulerAngles);

    }
}

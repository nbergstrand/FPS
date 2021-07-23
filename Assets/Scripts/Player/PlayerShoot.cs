using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    

    [SerializeField]
    GameObject _bulletSpark;

    

   
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        Vector3 screenCentre = new Vector3(.5f, .5f, 0);
        Ray ray = Camera.main.ViewportPointToRay(screenCentre);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            GameObject go = Instantiate(_bulletSpark, hit.point, Quaternion.identity);
            IDamageable target = hit.transform.GetComponent<IDamageable>();

            if (target != null)
            {

                target.Damage(10);

            }

            Destroy(go, 1);
        }

    }
}

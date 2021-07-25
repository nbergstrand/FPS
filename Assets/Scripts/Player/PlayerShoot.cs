using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    
    [SerializeField]
    GameObject _bulletSpark;

    [SerializeField]
    GameObject _blood;

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

            if(hit.collider.tag != "Enemy")
            {
                GameObject go = Instantiate(_bulletSpark, hit.point, Quaternion.identity);
                Destroy(go, 1);

            }
            else
            {
                GameObject go = Instantiate(_blood, hit.point + hit.normal *.1f, Quaternion.LookRotation(-hit.normal));
                Destroy(go, 1);

                IDamageable target = hit.transform.GetComponent<IDamageable>();

                if (target != null)
                {

                    target.Damage(10);

                }
            }
            

            
        }

    }
}

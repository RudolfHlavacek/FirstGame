using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float zivoty;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetDammage(float poskozeni) // nová metoda musí být public, abychom ji mohli zavolat z jiného skriptu
    {
        zivoty -= poskozeni; // odečte jednorázově poškození od životů
        if (zivoty <= 0)
        {
            Destroy(transform.gameObject); // metoda, která ničí herní objekty, jako parametr můžete nastavit čas od zavolání této metody, než se zničí
        }
    }
}

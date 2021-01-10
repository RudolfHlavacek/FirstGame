using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class shoot : MonoBehaviour
{
    RaycastHit hitInfo;
    Animator objectwithAnim;
    public float dostrel;
    public float sila;
    public float poskozeni;
    public GameObject efekttrefy;
    void Start()
    {
        objectwithAnim = GameObject.FindGameObjectWithTag("Animobject").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !objectwithAnim.GetBool("Run") && !objectwithAnim.GetBool("Holster") && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Inspect")) // pokud stiskneme levé tlačítko, neběžíme, nemáme schovanou zbraň a ani není spuštěna animace prohlížení
        {
            InvokeRepeating("Shoot", 0, 0.25f); // opakuje metodu Shoot(), začíná okamžitě bez prodlevy, opakuje se každých 0.25 sekundy
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Shoot"); // přestává se volat metoda Shoot()
        }
    }
    void Shoot() // námi nově vytvořená metoda
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, dostrel)) // pokud něco trefíme, parametry jsou: místo, odkud půjde polopřímka, směr, kam má ukládat informace o tom, co se zasáhlo, a dálka, kam až povede.
        {
            GameObject trefa = Instantiate(efekttrefy, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(trefa, 1);
            if (hitInfo.transform.GetComponent<Rigidbody>()) // pokud objekt, který jsme trefili, má komponentu Rigidbody
            {
                hitInfo.transform.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * sila); // přidáme sílu ve směru, kterým se díváme
            }
            if (hitInfo.transform.GetComponent<EnemyHealth>()) // pokud objekt, který jsme trefili, má komponentu EnemyHealth
            {
                hitInfo.transform.GetComponent<EnemyHealth>().GetDammage(poskozeni); // spouštíme metodu v jiném skriptu, parametrem jsme si nastavili množství poškození
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class shoot : MonoBehaviour
{
    RaycastHit hitInfo;
    Animator objectwithAnim;
    public float dostrel;
    public float sila;
    public float poskozeni;
    public GameObject efekttrefy;
    Image crosshair;
    ParticleSystem effekt;

    void Start()
    {
        objectwithAnim = GameObject.FindGameObjectWithTag("Animobject").GetComponent<Animator>();
        crosshair = GameObject.FindGameObjectWithTag("crosshair").GetComponent<Image>();
        effekt = GameObject.FindGameObjectWithTag("effekt").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !objectwithAnim.GetBool("Run") && !objectwithAnim.GetBool("Holster") && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Inspect")) // pokud zmáčkneme levé tlačítko a neběžíme
        {
            InvokeRepeating("Shoot", 0, 0.25f); // opakuje metodu shoot, začíná okamžitě bez prodlevy, opakuje se každých 0.25 sekundy
        }
        if (objectwithAnim.GetBool("Aim"))
        {
            crosshair.enabled = false; // změní hodnotu na false
        }
        else
        {
            crosshair.enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Shoot"); // přestává se volat metoda Shoot()
        }
        if (effekt.isPlaying && effekt.time >= 0.15f) // pokud efekt výstřelu pořád hraje a je
                                                      // to delší doba než 15 setin sekundy, tak se efekt zastaví
        {
            effekt.Stop();
        }
    }

    void Shoot() // námi nově vytvořená metoda
    {
        effekt.Stop();
        effekt.Play();
        transform.GetComponent<AudioSource>().Stop(); // kdyby byl spuštěn ještě předchozí výstřel, tak ho zastaví, takto nedojde k tomu, že uslyšíme 15 výstřelů najednou
        transform.GetComponent<AudioSource>().Play(); // spustí zvuk, který už je
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, dostrel)) // pokud něco trefíme, parametry jsou: místo, odkud půjde polopřímka, směr, kam má ukládat informace o tom, co se zasáhlo, dálka, kam až povede
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

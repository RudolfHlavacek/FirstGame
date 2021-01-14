using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class shoot : MonoBehaviour
{
    RaycastHit hitInfo;
    Animator objectwithAnim; // hráč
    public float dostrel;
    public float sila; // udělovaná síla Rigidbody
    public float poskozeni;
    public GameObject efekttrefy; // impact
    Image crosshair;
    ParticleSystem effekt; // výstřel
    Text nabojeText;
    public int naboje;
    int maxnaboje;
    bool reloading;
    void Start()
    {
        objectwithAnim = GameObject.FindGameObjectWithTag("Animobject").GetComponent<Animator>();
        crosshair = GameObject.FindGameObjectWithTag("crosshair").GetComponent<Image>();
        effekt = GameObject.FindGameObjectWithTag("effekt").GetComponent<ParticleSystem>();
        nabojeText = GameObject.FindGameObjectWithTag("naboje").GetComponent<Text>();
        maxnaboje = naboje;
        nabojeText.text = naboje.ToString() + "/" + maxnaboje.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !objectwithAnim.GetBool("Run") && !objectwithAnim.GetBool("Holster") && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Inspect") && naboje > 0 && !reloading) // pokud stiskneme levé tlačítko a neběžíme
        {
            InvokeRepeating("Shoot", 0, 0.25f); // opakuje metodu Shoot(), začíná okamžitě bez prodlevy, opakuje se každých 0.25 sekundy
        }
        if (objectwithAnim.GetBool("Aim") || objectwithAnim.GetBool("Holster"))
        {
            crosshair.enabled = false; // změní hodnotu na false
        }
        else
        {
            crosshair.enabled = true;
        }
        if (naboje <= 0 || reloading)
        {
            CancelInvoke("Shoot");
            objectwithAnim.SetBool("Shoot", false);
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Shoot"); // přestává se volat metoda Shoot()
            objectwithAnim.SetBool("Shoot", false);
        }
        if (effekt.isPlaying && effekt.time >= 0.15f)
        {
            effekt.Stop();
        }
        if (objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload Ammo Left") || objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload Out Of Ammo"))
        {
            CancelInvoke("Shoot");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot() // námi nově vytvořená metoda
    {
        effekt.Stop();
        effekt.Play();
        transform.GetComponent<AudioSource>().Stop(); // kdyby byl spuštěn ještě předchozí výstřel, tak se zastaví, aby nedošlo k tomu, že uslyšíme 15 výstřelů najednou
        transform.GetComponent<AudioSource>().Play(); // spustí zvuk, který už je
        naboje -= 1;
        nabojeText.text = naboje.ToString() + "/" + maxnaboje.ToString();
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

    IEnumerator Reload() // typ metody, ve které můžeme používat WaitUntil(zastaví metodu, dokud něco neplatí)
    {
        reloading = true;
        if (naboje > 0)
        {
            objectwithAnim.SetTrigger("Reload");
            yield return new WaitUntil(() => objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload Ammo Left")); // čeká, dokud nedostane hodnotu true od animátoru, že se přehrává animace
            yield return new WaitUntil(() => !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload Ammo Left")); // čeká, dokud nedostane hodnotu false od animátoru, že se už nepřehrává animace
            naboje = maxnaboje;

        }
        else
        {
            objectwithAnim.SetTrigger("ReloadNoAmmo");
            yield return new WaitUntil(() => objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload Out Of Ammo"));
            yield return new WaitUntil(() => !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload Out Of Ammo"));
            naboje = maxnaboje;
        }
        nabojeText.text = naboje.ToString() + "/" + maxnaboje.ToString();
        reloading = false;
    }

}

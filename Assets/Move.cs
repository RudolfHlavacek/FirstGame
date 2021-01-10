using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb; // proměnná, která zastupuje naši komponentu Rigidbody
    Animator objectwithAnim;
    bool running; // hodnota running je rovna true tehdy, když běžíme a nemůžeme dělat nic jiného
    bool nicnedelani;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>(); // získáme komponentu Rigidbody objektu se skriptem
        objectwithAnim = GameObject.FindGameObjectWithTag("Animobject").GetComponent<Animator>(); // metoda FindGameObjectWithTag() vyhledá herní objekt, který má tag = Animobject. Tag našemu objektu nastavíme později v editoru.
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !objectwithAnim.GetBool("Aim") && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Inspect")) // pokud držíme W a zároveň levý Shift, nemíříme a není aktivní animace Inspect
        {
            rb.AddRelativeForce(new Vector3(0, 0, 70)); // přidáme v aktuálním směru hodnotu větší než při chůzi, protože držíme Shift a "běžíme"
                                                        // Bool Run rozhoduje o tom, jestli se spustí animace běhu nebo ne
            objectwithAnim.SetBool("Run", true); // spustíme animaci běhu změněním bool parametru
            running = true; // náš bool, pomocí kterého pozná náš program, že hráč běží
        }
        else // pokud se nespustí podmínka výše, nastaví se bool hodnota na false a program ví, že hráč zrovna neběží
        {
            objectwithAnim.SetBool("Run", false); // zastaví se animace
            running = false;
        }
        if (!running) // pokud hráč neběží, tak se spustí podmínky níže
        {
            if (Input.GetMouseButtonDown(0) && !objectwithAnim.GetCurrentAnimatorStateInfo(0).IsName("Inspect") && !objectwithAnim.GetBool("Holster")) // pokud se stisklo levé tlačítko (označení 0), nepřehrává se animace Inspect a zároveň není zbraň schována
            {
                objectwithAnim.SetBool("Run", false); // zastavujeme animaci běhu a chůze, aby se mohla spustit animace střelby
                objectwithAnim.SetBool("Shoot", true); // aktivujeme parametry trigger, jako jsou Shoot a Inspect v našem případě
            }
            if (Input.GetMouseButtonUp(0))
            {
                objectwithAnim.SetBool("Shoot", false);

            }
            if (objectwithAnim.GetBool("Shoot"))
            {
                objectwithAnim.SetBool("Walk", false); // použijeme objekt, který jsme si definovali a získáme z něj komponentu Animator, tím získáme možnost upravovat animace, které jsou k tomuto objektu přichycené
            }
            if (Input.GetKey(KeyCode.D)) // pokud někdo stiskne klávesu W, spustí se tato podmínka, dokud bude klávesa držena
            {
                rb.AddRelativeForce(new Vector3(50, 0, 0)); // na proměnnou přešly vlastnosti a metody komponenty Rigidbody.
                                                            // metoda AddForce() přidá sílu do určitého směru, který nastavujeme pomocí os x y z.
                if (!objectwithAnim.GetBool("Shoot"))
                {
                    objectwithAnim.SetBool("Walk", true); // pomocí animátoru získáme možnost upravovat animace, které jsou k tomuto objektu přichycené.
                }                                                            // jednou z funkcí komponenty Animator je změnit hodnotu parametru
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddRelativeForce(new Vector3(-50, 0, 0));
                if (!objectwithAnim.GetBool("Shoot"))
                {
                    objectwithAnim.SetBool("Walk", true);
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(new Vector3(0, 0, 50));
                if (!objectwithAnim.GetBool("Shoot"))
                {
                    objectwithAnim.SetBool("Walk", true);
                }

            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeForce(new Vector3(0, 0, -50));
                if (!objectwithAnim.GetBool("Shoot"))
                {
                    objectwithAnim.SetBool("Walk", true);
                }
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)) // pokud pustíme W a S nebo D, tak se spustí tato podmínka
            {
                objectwithAnim.SetBool("Walk", false);
            }
            if (Input.GetMouseButton(1)) // pravé tlačítko (označení 1)
            {
                objectwithAnim.SetBool("Aim", true); // nastaví hodnotu parametru Aim
            }
            else
            {
                objectwithAnim.SetBool("Aim", false); // nastaví hodnotu parametru Aim typu bool na false
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                objectwithAnim.SetTrigger("Inspect");

            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                objectwithAnim.SetBool("Holster", !objectwithAnim.GetBool("Holster"));
            }
        }
    }
}

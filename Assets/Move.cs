using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb; // proměnná, která zastupuje naši komponentu Rigidbody
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>(); // definujeme rb jakou má hodnotu; transform je objekt, na který je skript napojen
        // funkce GetComponent() nám umožňuje získat komponentu jako proměnou, se kterou můžeme dále pracovat

    }

    // Update is called once per frame= tento skript se spouští každý snímek znovu a znovu pokud máte například 60fps(frames per second) skript se vám spustí 60krát za jednu sekundu.
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) // využíváme podmínky, že pokud někdo stiskne klávesu W, tak se spustí tato podmínka, dokud bude klávesa zmáčknuta
        {
            rb.AddForce(new Vector3(5, 0, 0)); // Zde využíváme toho, že na proměnou přešly vlastnosti a metody komponenty RIGIDBODY.
            // Metoda AddForce() přidá sílu do určitého směru, který nastavujeme pomocí os x, y a z. Zde přidáme hodnotu síly 5 na osu x (do strany).
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3(-5, 0, 0));
            // Zde přidáme hodnotu síly -5 na osu x (do strany).
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(0, 0, 5));
            // Zde přidáme hodnotu síly 5 na osu z (dopředu).
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(0, 0, -5));
            // Zde přidáme hodnotu síly -5 na osu z (dozadu).
        }
    }
}

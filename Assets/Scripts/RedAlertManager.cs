using UnityEngine;

/*********************************************
 
Ce script gère la rotation de la lumière des
alarmes du jeu.

*********************************************/
public class RedAlertManager : MonoBehaviour
{
    private float alertSpeed = 1.0f;

    void Update()
    {
         transform.Rotate(0, alertSpeed, 0);
    }
}

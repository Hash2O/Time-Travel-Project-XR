using UnityEngine;

/*********************************************
 
Ce script g�re la rotation de la lumi�re des
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

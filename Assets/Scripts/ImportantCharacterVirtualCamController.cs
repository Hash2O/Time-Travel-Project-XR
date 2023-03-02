using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ImportantCharacterVirtualCamController : MonoBehaviour
{
    [SerializeField] List<GameObject> characterCameras;

    [SerializeField] TextMeshProUGUI affichageVCam1;
    [SerializeField] TextMeshProUGUI affichageVCam2;
    [SerializeField] TextMeshProUGUI affichageVCam3;
    [SerializeField] TextMeshProUGUI affichageVCam4;

    private float chrono;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            characterCameras[0].SetActive(true);
            characterCameras[1].SetActive(false);
            characterCameras[2].SetActive(false);
            characterCameras[3].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            characterCameras[0].SetActive(false);
            characterCameras[1].SetActive(true);
            characterCameras[2].SetActive(false);
            characterCameras[3].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            characterCameras[0].SetActive(false);
            characterCameras[1].SetActive(false);
            characterCameras[2].SetActive(true);
            characterCameras[3].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            characterCameras[0].SetActive(false);
            characterCameras[1].SetActive(false);
            characterCameras[2].SetActive(false);
            characterCameras[3].SetActive(true);
        }

        chrono += Time.deltaTime;

        affichageVCam1.SetText("Virtual Camera 1 - Time : " + chrono);
        affichageVCam2.SetText("Virtual Camera 2 - Time : " + chrono);
        affichageVCam3.SetText("Virtual Camera 3 - Time : " + chrono);
        affichageVCam4.SetText("Virtual Camera 4 - Time : " + chrono);

    }
}

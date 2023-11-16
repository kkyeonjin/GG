using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhaseChangeTrigger : MonoBehaviour
{
    public RawImage minimap;
    public GameObject mapBackground;
    public GameObject pp;
    public GameObject Timer;

    // Start is called before the first frame update

    public void GoToNextPhase()
    {
        if(!Timer.activeSelf)
        {
            SceneManager.LoadScene("Apartment_Phase3");
            minimap.color = Color.white;
            mapBackground.gameObject.SetActive(true);
            pp.transform.localScale = new Vector3(15, 0.1f, 15);
            //SceneManager.LoadScene("Apartment_Phase3");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhaseChangeTrigger : MonoBehaviour
{
    public RawImage minimap;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            minimap.color = Color.white;
            SceneManager.LoadScene("Apartment_Phase3");
        }
    }
}

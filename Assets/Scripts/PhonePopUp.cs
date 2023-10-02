using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonePopUp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Phone;
    public bool phoneCheck;

    public void PhoneBtn()
    {
        phoneCheck = !phoneCheck;
        Phone.gameObject.SetActive(phoneCheck);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

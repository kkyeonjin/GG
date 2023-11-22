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
    ScreenTransition Transition;
    // Start is called before the first frame update

    private void Start()
    {
        Transition = ScreenTransition.Instance;
    }
    public void GoToNextPhase()
    {
        if(!Timer.activeSelf)
        {
            Transition.EndScreen();
            Invoke("ChangeScene", Transition.Get_TransitionTime());
            //SceneManager.LoadScene("Apartment_Phase3");
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.V))
        {
            SceneManager.LoadScene("Apartment_Phase3");
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Apartment_Phase3");
        SceneManager.sceneLoaded += Transition.StartScreen;
        minimap.color = Color.white;
        mapBackground.gameObject.SetActive(true);
        pp.transform.localScale = new Vector3(15, 0.1f, 15);
    }
    public void Clear_Map()
    {
        minimap.color = Color.white;
        mapBackground.gameObject.SetActive(true);
        pp.transform.localScale = new Vector3(15, 0.1f, 15);
    }
}
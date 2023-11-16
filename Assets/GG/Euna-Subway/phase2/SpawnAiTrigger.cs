using UnityEditor;
using UnityEngine;


public class SpawnAiTrigger : MonoBehaviour
{
    public GameObject CompAIs;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CompAIs.SetActive(true);
        }
    }
}
       
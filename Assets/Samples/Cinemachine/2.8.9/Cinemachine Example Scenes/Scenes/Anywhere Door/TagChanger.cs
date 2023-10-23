using UnityEngine;

public class TagChanger : MonoBehaviour
{
    public void PlayerTagChanger()
    {
        this.tag = "player";
    }
    public void UntaggedTagChanger()
    {
        this.tag = "Untagged";
    }
}
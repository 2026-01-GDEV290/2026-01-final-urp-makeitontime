using UnityEngine;

public class GameLoadStart : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<LoadingScreenScript>().LoadLevel(1);          
    }
}

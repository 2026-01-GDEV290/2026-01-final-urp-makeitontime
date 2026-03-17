using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenURLstring(string url)
    {
        Application.OpenURL(url);
    }

}

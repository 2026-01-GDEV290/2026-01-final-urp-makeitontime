using UnityEngine;

public class LevelPageHolder : MonoBehaviour
{
    public LevelPageScript[] pages;

    public void resetPages()
    {
        foreach (var p in pages)
        {
            p.gameObject.GetComponent<RectTransform>().SetAsLastSibling();
            p.deselectPage();
        }
    }
}

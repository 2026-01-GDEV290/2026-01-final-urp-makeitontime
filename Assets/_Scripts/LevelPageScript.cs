using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelPageScript : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    GameObject notSelectedGO;
    [SerializeField]
    GameObject selectedGO;
    [SerializeField]
    int defaultLayer;
    public bool selected;

    [SerializeField]
    public RectTransform myRect;

    [SerializeField]
    LevelPageHolder levelPageHolder;

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }
    private void Start()
    {
        levelPageHolder = GameObject.FindFirstObjectByType<LevelPageHolder>();
    }
    private void OnMouseOver()
    {
        Debug.Log("bring to front");
        selectPage();
    }
    public void selectPage()
    {
        levelPageHolder.resetPages();
        myRect.SetAsLastSibling();
        notSelectedGO.SetActive(false);
        selectedGO.SetActive(true);

        selected = true;
    }
    public void deselectPage()
    {
        notSelectedGO.SetActive(true);
        selectedGO.SetActive(false);

        selected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectPage();
        Debug.Log("hover Enter");
    }
}

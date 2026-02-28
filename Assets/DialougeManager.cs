using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    [SerializeField]
    Image leftChar;
    [SerializeField]
    Image rightChar;
    [SerializeField]
    TMP_Text textBox;
    [SerializeField]
    GameObject leftName;
    [SerializeField]
    GameObject rightName;
    [SerializeField]
    TMP_Text leftNameText;
    [SerializeField]
    TMP_Text rightNameText;

    [SerializeField]
    DialougeScriptableObject scene;
    [SerializeField]
    AudioSource source;
    int sNum;

    float h;
    float s;
    float v;
    private void Start()
    {
        sNum = -1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            updateDialouge();
        }
    }
    void updateDialouge()
    {
        sNum++;

        if (sNum >= scene.screen.Length)
        {
            Debug.Log("Dialouge is Done");
            return;
        }

        ScreenScriptableOBJ screen = scene.screen[sNum];

        leftChar.sprite = screen.charOne;
        rightChar.sprite = screen.charTwo;
        textBox.text = screen.text;
        textBox.color = screen.textColor;
        if (screen.leftSpeaking)
        {
            leftNameText.text = screen.charSpeakingName;
            leftName.GetComponent<Image>().color = screen.nameColor;
            leftName.SetActive(true);
            rightName.SetActive(false);

            leftChar.color = Color.white;
            
            leftChar.rectTransform.localPosition = new Vector3(-175,75,0);
            leftChar.rectTransform.localScale = Vector3.one;

            Color.RGBToHSV(Color.white, out h, out s, out v);

            rightChar.color = Color.HSVToRGB(h, s, 0.6f);

            rightChar.rectTransform.localPosition = new Vector3(200,45,0);
            rightChar.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        else
        {
            rightNameText.text = screen.charSpeakingName;
            leftName.SetActive(false);
            rightName.GetComponent<Image>().color = screen.nameColor;
            rightName.SetActive(true);

            rightChar.color = Color.white;

            rightChar.rectTransform.localPosition = new Vector3(175, 75, 0);
            rightChar.rectTransform.localScale = Vector3.one;

            Color.RGBToHSV(Color.white, out h, out s, out v);

            leftChar.color = Color.HSVToRGB(h, s, 0.6f);

            leftChar.rectTransform.localPosition = new Vector3(-200, 45, 0);
            leftChar.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        source.PlayOneShot(screen.voiceOver);
    }
}

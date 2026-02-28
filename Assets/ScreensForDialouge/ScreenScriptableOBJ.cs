using UnityEngine;

[CreateAssetMenu(fileName = "ScreenScriptableOBJ", menuName = "Scriptable Objects/ScreenScriptableOBJ")]
public class ScreenScriptableOBJ : ScriptableObject
{
    public bool leftSpeaking;
    public Color textColor;
    public Color nameColor;
    public Sprite charOne;
    public Sprite charTwo;
    public string charSpeakingName;
    public string text;
}

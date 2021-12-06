using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using System.Text;

public class RelicDescHolder : MonoBehaviour
{
    public string relicName;
    public string relicDesc;
    private PauseMenu pauseMenu;
    public int upgradeCount = 0;
    public Text countText;

    private void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenu>();
    }

    private void Update()
    {
        if (upgradeCount > 1)
        {
            countText.text = upgradeCount.ToString() + "x";
        }
        else
        {
            countText.text = "";
        }
    }

    public void ShowRelicDesc()
    {
        string appendName = AddSpacesToSentence(relicName);

        pauseMenu.relicDesc.text = appendName + ": " + relicDesc;
        
    }

    public void HideRelicDesc()
    {
        pauseMenu.relicDesc.text = "";
    }

    public string AddSpacesToSentence(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";

        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }
}


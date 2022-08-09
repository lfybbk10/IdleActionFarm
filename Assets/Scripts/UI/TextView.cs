using System;
using TMPro;
using UnityEngine;


public class TextView : MonoBehaviour
{
    private TextMeshProUGUI _textComponent;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string text)
    {
        _textComponent.SetText(text);
    }
}

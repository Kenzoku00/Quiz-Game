using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InsideBox : MonoBehaviour
{
    public void inIt(DictionaryData dictionaryData)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = dictionaryData.dictionaryIcon;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dictionaryData.dictionaryName;
    }
}

[System.Serializable]
public class DictionaryData
{
    public Sprite dictionaryIcon;
    public string dictionaryName;
}

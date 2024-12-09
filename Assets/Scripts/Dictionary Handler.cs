using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryHandler : MonoBehaviour
{
    [SerializeField] private DictionaryData[] dictionaryDatas;
    [SerializeField] private InsideBox insideBox;
    [SerializeField] private Transform container;

    public void Start()
    {
        foreach (DictionaryData data in dictionaryDatas)
        {
            Instantiate(insideBox, container).inIt(data); 
        }
    }
}

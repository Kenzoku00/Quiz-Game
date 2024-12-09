using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToggleUIManager : MonoBehaviour
{
    [SerializeField] GameObject mainGameObject;
    [SerializeField] GameObject dictionaryGameObject;

    public void ShowDictionary()
    {
        if (mainGameObject != null && dictionaryGameObject != null)
        {
            mainGameObject.transform.DOScale(0f, 0.3f).OnComplete(() => mainGameObject.SetActive(false));
            dictionaryGameObject.SetActive(true);
            dictionaryGameObject.transform.localScale = Vector3.zero;
            dictionaryGameObject.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        }
    }

    public void ShowMain()
    {
        if (mainGameObject != null && dictionaryGameObject != null)
        {
            dictionaryGameObject.transform.DOScale(0f, 0.3f).OnComplete(() => dictionaryGameObject.SetActive(false));
            mainGameObject.SetActive(true);
            mainGameObject.transform.localScale = Vector3.zero;
            mainGameObject.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        }
    }
}

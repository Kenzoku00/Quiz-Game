using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private Button menuButton;

    private void Start()
    {
        menuButton.onClick.AddListener(() => OnButtonPress());
    }

    private void OnButtonPress()
    {
        menuButton.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 3f, 1, 1f).SetEase(Ease.InOutElastic);
    }
}

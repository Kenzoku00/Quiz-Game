using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform[] buttons;
    [SerializeField] private RectTransform titleImage;
    [SerializeField] private float delayBetweenButtons = 0.3f;
    [SerializeField] private Vector3 largeScale = new Vector3(1f, 1f, 0.5f);
    [SerializeField] private Vector3 smallScale = new Vector3(0.8f, 0.8f, 0.5f);
    [SerializeField] private float bounceDelay = 2f;
    [SerializeField] private float bounceScaleFactor = 1.1f;

    [SerializeField] private float titleScaleDuration = 1.5f;
    [SerializeField] private float titleScaleFactor = 1.1f;

    private void Start()
    {
        AnimateButtonsWithCallback(() =>
        {
            StartCoroutine(AnimateButtonsBounce());
            StartCoroutine(AnimateTitleImage());
        });
    }

    private void AnimateButtonsWithCallback(TweenCallback onComplete)
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform button = buttons[i];

            button.localScale = Vector3.zero;

            Vector3 targetScale = i == 1 ? largeScale : smallScale;

            sequence.Append(button.DOScale(targetScale, 0.5f).SetEase(Ease.OutBack))
                    .AppendInterval(delayBetweenButtons);
        }

        sequence.OnComplete(onComplete);
    }

    private IEnumerator AnimateButtonsBounce()
    {
        while (true)
        {
            foreach (var button in buttons)
            {
                button.DOScale(button.localScale * bounceScaleFactor, 0.3f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        button.DOScale(button.localScale / bounceScaleFactor, 0.3f).SetEase(Ease.InQuad);
                    });

                yield return new WaitForSeconds(bounceDelay);
            }
        }
    }

    private IEnumerator AnimateTitleImage()
    {
        while (true)
        {
            titleImage.DOScale(titleImage.localScale * titleScaleFactor, titleScaleDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    titleImage.DOScale(titleImage.localScale / titleScaleFactor, titleScaleDuration)
                        .SetEase(Ease.InBack);
                });

            yield return new WaitForSeconds(titleScaleDuration * 2); 
        }
    }
}

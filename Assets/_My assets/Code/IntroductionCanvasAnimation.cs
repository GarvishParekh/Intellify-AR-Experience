using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroductionCanvasAnimation : MonoBehaviour
{
    [SerializeField] private GameObject IntroductionCanvas;
    [SerializeField] private GameObject okayButton;
    [SerializeField] private List<GameObject> experienceAnimation = new List<GameObject>();

    [SerializeField] private RectTransform maskingImageRectTransform;
    [SerializeField] private RectTransform boothImageRectTransform;
    [SerializeField] private RectTransform phoneScanningImage;

    [Space]
    [SerializeField] private Vector2 maskingImageInitialSize;
    [SerializeField] private Vector2 maskingImageFinalSize;

    [Space]
    [SerializeField] private Vector2 boothImageInitialSize;
    [SerializeField] private Vector2 boothImageFinalSize;
    [SerializeField] private Vector2 boothImageInitialPos;
    [SerializeField] private Vector2 boothImageFinalPos;

    [Space]
    [SerializeField] private Vector2 phoneImageInitialSize;
    [SerializeField] private Vector2 phoneImageFinalSize;

    [SerializeField] private float animationSpeed = 0.35f;

    private void Start()
    {
        okayButton.SetActive(false);
        StartCoroutine(nameof(StartAnimation));
    }

    private void ResetAnimation()
    {
        maskingImageRectTransform.sizeDelta = maskingImageInitialSize;
        boothImageRectTransform.sizeDelta = boothImageInitialSize;
        boothImageRectTransform.anchoredPosition = boothImageInitialPos;

        phoneScanningImage.sizeDelta= phoneImageInitialSize;
    }

    private IEnumerator StartAnimation()
    {
        ResetAnimation();
        LeanTween.size(maskingImageRectTransform, maskingImageFinalSize, animationSpeed).setEaseInOutSine();
        yield return new WaitForSeconds(1);
        LeanTween.size(boothImageRectTransform, boothImageFinalSize, animationSpeed).setEaseInOutSine();
        LeanTween.move(boothImageRectTransform, boothImageFinalPos, animationSpeed).setEaseInOutSine();
        yield return new WaitForSeconds(1);
        LeanTween.size(phoneScanningImage, phoneImageFinalSize, animationSpeed).setEaseInOutSine().setOnComplete(()=>
        {
            okayButton.SetActive(true);
        });
    }

    public void _OkayButton()
    {
        foreach (GameObject animation in experienceAnimation)
        {
            animation.SetActive(true);
        }
        IntroductionCanvas.SetActive(false);
    }
}

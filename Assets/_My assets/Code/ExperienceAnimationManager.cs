using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ExperienceAnimationManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainHolder;
    [SerializeField] private RectTransform mainHolderRectTransform;
    [SerializeField] private CanvasGroup informationHolder;

    [Space]
    [SerializeField] private Vector2 initialPosition = new Vector2 (750, 220);
    [SerializeField] private Vector2 endPosition = new Vector2(750, 1200);

    [Space]
    [SerializeField] private Image productLine;
    [SerializeField] private Image categoryLine;
    [SerializeField] private Image functionLine;

    [SerializeField] private float lineFillValueMultiplier;

    [SerializeField] private List<Image> glowImageList = new List<Image>();
    [SerializeField] private float glowFillValueMultiplier;

    [Space]
    [SerializeField] private CanvasGroup productGradient;
    [SerializeField] private CanvasGroup categoryGradient;
    [SerializeField] private CanvasGroup functionGradient;

    [Space]
    [SerializeField] private CanvasGroup tapHereText;
    [SerializeField] private GameObject blurImage;

    [Space]
    [SerializeField] private Color productColor;
    [SerializeField] private Image informationDivider;
    [SerializeField] private TMP_Text userCaseText;

    private void Start()
    {
        informationDivider.color = productColor;
        userCaseText.color = productColor;

        tapHereText.GetComponent<TMP_Text>().text = "View";
    }

    private void ResetAnimation()
    {
        mainHolder.alpha = 0;
        productGradient.alpha = 0;
        categoryGradient.alpha = 0;
        functionGradient.alpha = 0;
        tapHereText.alpha = 0;

        productLine.fillAmount = 0;
        categoryLine.fillAmount = 0;
        functionLine.fillAmount = 0;

        mainHolderRectTransform.sizeDelta = initialPosition;
        informationHolder.alpha = 0;    

        foreach (Image glowImage in glowImageList) 
        {
            CanvasGroup cg = glowImage.GetComponent<CanvasGroup>();
            cg.alpha = 0;
            glowImage.fillAmount = 0;
        }
    }

    bool isPlayingAnimation = false;
    private IEnumerator StartAnimation()
    { 
        isPlayingAnimation = true;
        ResetAnimation();
        LeanTween.alphaCanvas(mainHolder, 1, 0.25f).setEaseInOutSine();

        yield return new WaitForSeconds(1);

        float lineImageFillValue = 0;
        while (lineImageFillValue < 1)
        {
            lineImageFillValue += Time.deltaTime * lineFillValueMultiplier;

            productLine.fillAmount = lineImageFillValue;
            categoryLine.fillAmount = lineImageFillValue;
            functionLine.fillAmount = lineImageFillValue;
            yield return null;
        }
        LeanTween.alphaCanvas(productGradient, 1, 0.8f).setEaseInOutSine();
        LeanTween.alphaCanvas(categoryGradient, 1, 0.8f).setEaseInOutSine();
        LeanTween.alphaCanvas(functionGradient, 1, 0.8f).setEaseInOutSine();

        //yield return new WaitForSeconds(1);
        float glowImageFillValue = 0;
        while (glowImageFillValue < 1)
        {
            glowImageFillValue += Time.deltaTime * glowFillValueMultiplier;
            foreach (Image glowImage in glowImageList) 
            {
                CanvasGroup cg = glowImage.GetComponent<CanvasGroup>();
                cg.alpha = 1;
                glowImage.fillAmount = glowImageFillValue;
            }
            yield return null;
        }
        foreach (Image glowImage in glowImageList)
        {
            CanvasGroup cg = glowImage.GetComponent<CanvasGroup>();
            LeanTween.alphaCanvas(cg , 0, 2f).setEaseInOutSine().setOnComplete(()=>
            {
                LeanTween.alphaCanvas(cg, 1, 2f).setEaseInOutSine().setLoopPingPong(1);
            });
        }

        yield return new WaitForSeconds(1);
        LeanTween.alphaCanvas(tapHereText, 1, 0.25f).setEaseInOutSine();

        yield return new WaitForSeconds(0.5f);
        LeanTween.alphaCanvas(tapHereText, 0, 1f).setEaseInOutSine().setLoopPingPong();

        isPlayingAnimation = false;
    }

    bool isOpen = false;
    public void _OpenAnimation()
    {
        if (isPlayingAnimation) return;

        if (!isOpen)
        {
            LeanTween.alphaCanvas(tapHereText, 0, 0.25f).setEaseInOutSine();
            LeanTween.size(mainHolderRectTransform, endPosition, 0.25f).setEaseInOutSine().setOnComplete(() =>
            {
                LeanTween.alphaCanvas(informationHolder, 1, 0.25f).setEaseInOutSine();
                isOpen = true;
            });
        }
        else
        {
            LeanTween.alphaCanvas(informationHolder, 0, 0.25f).setEaseInOutSine().setOnComplete(() =>
            {
                LeanTween.size(mainHolderRectTransform, initialPosition, 0.25f).setEaseInOutSine();
                isOpen = false;
            });
        }
    }

    bool onceSeen = false;
    [ContextMenu("Play animation")]
    public void _OnSeen()
    {
        //if (onceSeen) return;

        if (isPlayingAnimation)
        {
            StopCoroutine(nameof(StartAnimation));  
        }
        StartCoroutine(nameof(StartAnimation));

        //onceSeen = true;
    }

    public void _OnNotSeen()
    {

    }

    private IEnumerator UseCaseGowingEffect()
    {
        while(true)
        {

            yield return null;
        }
    }


    //#TESTING
    bool isImageOn = true;
    public void _BlurImageSwitch()
    {
        if (isImageOn) isImageOn = false;
        else isImageOn = true;

        blurImage.SetActive(isImageOn);
    }
}



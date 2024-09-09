using UnityEngine;
using UnityEngine.UI;

public class TheIntellifyAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform iconInfoRectTransform;
    [SerializeField] private RectTransform revealImage;

    [SerializeField] private RectTransform productRectTransform;
    private Image productImage;
    [SerializeField] private CanvasGroup productImageInformation;

    [SerializeField] private RectTransform functionRectTransform;
    private Image functionImage;
    [SerializeField] private CanvasGroup functionImageInformation;

    [SerializeField] private RectTransform categoryRectTransform;
    private Image categoryImage;
    [SerializeField] private CanvasGroup categoryImageInformation;

    [Space]
    [SerializeField] private Vector2 cardInitialSizeValue;
    [SerializeField] private Vector2 cardFinalSizeValue;

    [Space]
    [SerializeField] private Vector2 productCardInitialPosValue;
    [SerializeField] private Vector2 productCardFinalPosValue;

    [SerializeField] private Vector2 functionCardInitialPosValue;
    [SerializeField] private Vector2 functionCardFinalPosValue;

    [SerializeField] private Vector2 categoryCardInitialPosValue;
    [SerializeField] private Vector2 categoryCardFinalPosValue;

    private Image iconInfoImage;

    [Space]
    [SerializeField] private Vector2 mainTittleInitialSizeValue = new Vector3(120, 120);
    [SerializeField] private Vector2 mainTittleFinalSizevalue = new Vector3(880, 255);

    [Space]
    [SerializeField] private CanvasGroup iconCanvasGroup;
    [SerializeField] private CanvasGroup iconInfoCanvasGroup;

    [Space]
    [SerializeField] private float iconInfoScalingAnimationDuration = 2f;
    [SerializeField] private float iconInfoRoundAnimationDuration = 2f;

    [Space]
    [SerializeField] private RectTransform userCaseRectTransform;
    [SerializeField] private CanvasGroup informationMainHolder;
    [SerializeField] private RectTransform infromationTextholder;
    [SerializeField] private Image useCaseImage;
    [SerializeField] private CanvasGroup informationText;

    [Space]
    [SerializeField] private Vector2 usecaseInitialPos;
    [SerializeField] private Vector2 usecaseFinalPos;

    [Space]
    [SerializeField] private Vector2 usecaseInitalSize;
    [SerializeField] private Vector2 usecaseFinalSize;

    private void Awake()
    {
        iconInfoImage = iconInfoRectTransform.GetComponent<Image>();

        productImage = productRectTransform.GetComponent<Image>();
        functionImage = functionRectTransform.GetComponent<Image>();
        categoryImage = categoryRectTransform.GetComponent<Image>();
        useCaseImage = userCaseRectTransform.GetComponent<Image>();
    }

    private void ResetAnimation()
    {
        iconInfoRectTransform.sizeDelta = mainTittleInitialSizeValue;
        revealImage.sizeDelta = new Vector2(531, 120);

        iconCanvasGroup.alpha = 1;
        iconInfoCanvasGroup.alpha = 0;

        productRectTransform.sizeDelta = Vector2.zero;
        categoryRectTransform.sizeDelta = Vector2.zero;
        functionRectTransform.sizeDelta = Vector2.zero;

        productRectTransform.anchoredPosition = productCardInitialPosValue;
        categoryRectTransform.anchoredPosition = categoryCardInitialPosValue;
        functionRectTransform.anchoredPosition = functionCardInitialPosValue;

        userCaseRectTransform.anchoredPosition = usecaseInitialPos;
        userCaseRectTransform.sizeDelta = usecaseInitalSize;
        useCaseImage.pixelsPerUnitMultiplier = 1;

        productImageInformation.alpha = 0;
        functionImageInformation.alpha = 0;
        categoryImageInformation.alpha = 0;

        infromationTextholder.sizeDelta = new Vector2(1480, 0);
        informationText.alpha = 0;

        userCaseRectTransform.sizeDelta = Vector2.zero;

        informationMainHolder.alpha = 0;
    }

    private void StartAnimation()
    {
        ResetAnimation();

        LeanTween.size(iconInfoRectTransform, mainTittleFinalSizevalue, iconInfoScalingAnimationDuration).setEaseInOutSine();
        LeanTween.alphaCanvas(iconCanvasGroup, 0, iconInfoScalingAnimationDuration).setEaseInOutSine()
            .setOnComplete(() =>
        {
            iconInfoCanvasGroup.alpha = 1;
            LeanTween.size(revealImage, new Vector2(0, 120), 0.25f).setEaseInOutSine().setOnComplete(() =>
            {
                LeanTween.size(productRectTransform, new Vector2(60, 60), 0.2f).setEaseInOutSine().setOnComplete(() =>
                {
                    LeanTween.size(functionRectTransform, new Vector2(60, 60), 0.2f).setEaseInOutSine().setOnComplete(() =>
                    {
                        LeanTween.size(categoryRectTransform, new Vector2(60, 60), 0.2f).setEaseInOutSine().setOnComplete(()=>
                        {
                            ProductCardAnimation();
                        });
                    });
                });
            });
        });

        LeanTween.value(gameObject, 1, 3.8f, iconInfoRoundAnimationDuration).setEaseInOutSine().setOnUpdate((float value) =>
        {
            iconInfoImage.pixelsPerUnitMultiplier = value;
        });
    }

    private void ProductCardAnimation()
    {
        LeanTween.move(productRectTransform, productCardFinalPosValue, 0.25f).setEaseInOutSine();
        LeanTween.value(gameObject, 1, 6f, iconInfoRoundAnimationDuration).setEaseInOutSine().setOnUpdate((float value) =>
        {
            categoryImage.pixelsPerUnitMultiplier = value;
        });
        LeanTween.size(productRectTransform, cardFinalSizeValue, 0.25f).setEaseInOutSine().setOnComplete(()=>
        {
            LeanTween.alphaCanvas(productImageInformation, 1, 0.25f).setEaseInOutSine().setOnComplete(() =>
            {
                FuctionCardAnimation();
            });
        });
    }

    private void FuctionCardAnimation()
    {
        LeanTween.move(functionRectTransform, functionCardFinalPosValue, 0.25f).setEaseInOutSine();
        LeanTween.value(gameObject, 1, 6f, iconInfoRoundAnimationDuration).setEaseInOutSine().setOnUpdate((float value) =>
        {
            functionImage.pixelsPerUnitMultiplier = value;
        });
        LeanTween.size(functionRectTransform, cardFinalSizeValue, 0.25f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.alphaCanvas(functionImageInformation, 1, 0.25f).setEaseInOutSine().setOnComplete(() =>
            {
                CategoryCardAnimation();
            });
        });
    }

    private void CategoryCardAnimation()
    {
        LeanTween.move(categoryRectTransform, categoryCardFinalPosValue, 0.25f).setEaseInOutSine();
        LeanTween.value(gameObject, 1, 6f, iconInfoRoundAnimationDuration).setEaseInOutSine().setOnUpdate((float value) =>
        {
            categoryImage.pixelsPerUnitMultiplier = value;
        });
        LeanTween.size(categoryRectTransform, cardFinalSizeValue, 0.25f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.alphaCanvas(categoryImageInformation, 1, 0.25f).setEaseInOutSine().setOnComplete(()=>
            {
                UseCaseCardAnimation();
            });
        });
    }

    private void UseCaseCardAnimation()
    {
        LeanTween.size(userCaseRectTransform, usecaseInitalSize, 0.25f).setEaseInOutSine().setOnComplete(()=>
        {
            LeanTween.value(gameObject, 1, 6f, iconInfoRoundAnimationDuration).setEaseInOutSine().setDelay(0.5f).setOnUpdate((float value) =>
            {
                useCaseImage.pixelsPerUnitMultiplier = value;
            });
            LeanTween.size(userCaseRectTransform, usecaseFinalSize, 0.3f).setDelay(0.5f).setEaseInOutSine();
            LeanTween.move(userCaseRectTransform, usecaseFinalPos, 0.3f).setDelay(0.5f).setEaseInOutSine().setOnComplete(()=>
            {
                LeanTween.alphaCanvas(informationMainHolder, 1, 0.3f).setEaseInOutSine().setOnComplete(()=>
                {
                    LeanTween.size(infromationTextholder, new Vector2(1480, 470), 0.3f).setEaseInOutSine();
                    LeanTween.alphaCanvas(informationText, 1, 0.3f).setEaseInOutSine();
                });
            });
        });
    }

    public void _OnSeen()
    {
        ResetAnimation();
        StartAnimation();
    }

    public void _OnNotSeen()
    {
        LeanTween.reset();
    }
}

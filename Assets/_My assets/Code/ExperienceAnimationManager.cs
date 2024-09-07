using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    [Space]
    [SerializeField] private CanvasGroup productGradient;
    [SerializeField] private CanvasGroup categoryGradient;
    [SerializeField] private CanvasGroup functionGradient;

    [Space]
    [SerializeField] private CanvasGroup tapHereText;

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
    }

    private IEnumerator StartAnimation()
    { 
        ResetAnimation();
        LeanTween.alphaCanvas(mainHolder, 1, 0.25f).setEaseInOutSine();

        yield return new WaitForSeconds(1);

        float productLineFillVlaue = 0;
        while (productLineFillVlaue < 1)
        {
            productLineFillVlaue += Time.deltaTime;
            productLine.fillAmount = productLineFillVlaue;
            yield return null;
        }
        LeanTween.alphaCanvas(productGradient, 1, 0.25f).setEaseInOutSine();

        yield return new WaitForSeconds(1);

        float categoryLineFillVlaue = 0;
        while (categoryLineFillVlaue < 1)
        {
            categoryLineFillVlaue += Time.deltaTime;
            categoryLine.fillAmount = categoryLineFillVlaue;
            yield return null;
        }
        LeanTween.alphaCanvas(categoryGradient, 1, 0.25f).setEaseInOutSine();

        yield return new WaitForSeconds(1);
        
        float functionLineFillVlaue = 0;
        while (functionLineFillVlaue < 1)
        {
            functionLineFillVlaue += Time.deltaTime;
            functionLine.fillAmount = functionLineFillVlaue;
            yield return null;
        }
        LeanTween.alphaCanvas(functionGradient, 1, 0.25f).setEaseInOutSine();

        yield return new WaitForSeconds(1);
        LeanTween.alphaCanvas(tapHereText, 1, 0.25f).setEaseInOutSine();

        yield return new WaitForSeconds(0.5f);
        LeanTween.alphaCanvas(tapHereText, 0, 0.25f).setEaseInOutSine().setLoopPingPong();
    }

    bool isOpen = false;
    public void _OpenAnimation()
    {
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
                LeanTween.alphaCanvas(tapHereText, 1, 0.25f).setEaseInOutSine();
                isOpen = false;
            });
        }
    }

    public void _CloseAnimation()
    {
        
    }

    bool onceSeen = false;
    public void _OnSeen()
    {
        if (onceSeen) return;
        StartCoroutine(nameof(StartAnimation));
        onceSeen = true;
    }

    public void _OnNotSeen()
    {

    }
}

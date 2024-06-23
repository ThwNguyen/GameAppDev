using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour,IEndDragHandler
{
   
    [SerializeField] int maxPage;

    [SerializeField]
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed,barOpen;

    [SerializeField] Button previousBtn, nextBtn;


    float dragthreshould;
    private void Awake()
    {  
        currentPage =1   ;
        targetPos=levelPagesRect.localPosition;
        dragthreshould = Screen.width / 15;
        UpdateBar();
        UpdateArrowButton();
       // targetPos.y = 0 ;

    }
    public void NextPage()
    {Debug.Log("aa");
        if(currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if(currentPage > 1) {
        currentPage--;
            targetPos -= pageStep;
        }
        MovePage();
    }


    public void MovePage()
    {
       
        LeanTween.reset();
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrowButton();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       if(Mathf.Abs(eventData.position.x - eventData.pressPosition.x)>dragthreshould)
        {
            if(eventData.position.x >eventData.pressPosition.x)
            {
                Previous();
            }else NextPage();
        }else MovePage();
    }
    void UpdateBar()
    {
        foreach(var item in barImage)
        {
            item.sprite = barClosed;
        }
        barImage[currentPage - 1].sprite = barOpen;
    }
    void UpdateArrowButton()
    {
        nextBtn.interactable = true;
        previousBtn.interactable = true;
        if(currentPage==1) previousBtn.interactable = false;
        else if (currentPage==maxPage) nextBtn.interactable = false;
    }
 

}

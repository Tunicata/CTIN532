using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public RoomManager Manager;
    private int index;
    private bool releaseCard;
    private bool selected;
    
    public void Init(Dictionary<string, string> data)
    {
        this.releaseCard = false;
        this.selected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (releaseCard) {
            transform.DOScale(1f, 0.25f);
            transform.SetAsLastSibling();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (releaseCard) {
            transform.DOScale(0.75f, 0.25f);
            transform.SetSiblingIndex(index);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Manager.OnKeyCardClicked();
        if (releaseCard&&!selected)
        {
            SelectCard();
        } else if (releaseCard&&selected) {
            CancelSelect();
        }
    }

    private void Start()
    {
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        this.index = transform.GetSiblingIndex();
    }

    private void SelectCard()
    {
        selected = true;
    }

    public void CancelSelect()
    {
        selected = false;
        transform.DOScale(0.75f, 0.25f);
    }

    public bool IsSelected()
    {
        return selected;
    }

    public void ActivateCard()
    {
        releaseCard = true;
    }

    public void DeactivateCard()
    {
        releaseCard = false;
    }
}
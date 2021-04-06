using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class Card : MonoBehaviour,IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    #region Card effect variables

    private List<CardEffect> m_effects = new List<CardEffect>();

    private List<CardEffect> m_effectsOnPlay = new List<CardEffect>();

    #endregion

    #region UI variables

    private Transform m_baseParent;
    private int m_childNumber = 0;

    #endregion

    private void Start()
    {
        m_effects = GetComponents<CardEffect>().ToList();

        m_effectsOnPlay = m_effects.FindAll(x => x.TriggerMoment == CardTriggerMoment.OnPlayed);

        m_baseParent = transform.parent;
        m_childNumber = transform.GetSiblingIndex();
    }

    private void OnPlay()
    {
        for(int i = 0; i < m_effectsOnPlay.Count; i++)
        {
            m_effectsOnPlay[i].ApplyEffect();
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.position.y > m_baseParent.position.y + 10f)
        {
            OnPlay();
        }
        transform.SetParent(m_baseParent);
        transform.SetSiblingIndex(m_childNumber);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(m_baseParent);
        transform.SetSiblingIndex(m_childNumber);
    }

   
}

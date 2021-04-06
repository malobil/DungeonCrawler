using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    public CardTriggerMoment m_triggerMoment ;

    public CardTriggerMoment TriggerMoment { get => m_triggerMoment; set => m_triggerMoment = value; }

    public virtual void ApplyEffect()
    {
       
    }
}

public enum CardTriggerMoment { OnPlayed }
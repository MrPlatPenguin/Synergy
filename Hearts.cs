using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Hearts : MonoBehaviour
{
    [SerializeField] Image white, black;
    [SerializeField] Sprite whiteFull, whiteCracked, whiteBroken, blackFull, blackCracked, blackBroken;
    [SerializeField] Animator whiteAnim, blackAnim;

    bool _whiteNearDeath, _blackNearDeath;


    public bool whiteNearDeath
    {
        get { return _whiteNearDeath; }
        set
        {
            _whiteNearDeath = value;
            whiteAnim.SetBool("IsNearDeath", value);
            white.sprite = value ? whiteCracked : whiteFull;
        }
    }

    public bool blackNearDeath
    {
        get { return _blackNearDeath; }
        set
        {
            _blackNearDeath = value;
            blackAnim.SetBool("IsNearDeath", value);
            black.sprite = value ? blackCracked : blackFull;
        }
    }
}

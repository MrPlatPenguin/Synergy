using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterTrigger : MonoBehaviour
{
    [SerializeField] LayerMask triggerLayers;

    [SerializeField] UnityEvent enterEvent, exitEvent;

    [Header("Trigger Sync")]
    [SerializeField] EnterTrigger[] syncWith;
    [HideInInspector] public int triggers;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerLayers == (triggerLayers | (1 << other.gameObject.layer)))
        {
            enterEvent?.Invoke();
            triggers ++;
            if (syncWith != null)
            {
                foreach (EnterTrigger sync in syncWith)
                {
                    sync.triggers++;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerLayers == (triggerLayers | (1 << other.gameObject.layer)))
        {
            triggers--;
            if (syncWith != null)
            {
                foreach (EnterTrigger sync in syncWith)
                {
                    sync.triggers--;
                }
            }
            if (triggers == 0)
                exitEvent?.Invoke();
        }
    }
}

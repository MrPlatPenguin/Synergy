using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PortalMaster master;
    public bool portalOne, portalUsed;

    private void Start()
    {
        master = GetComponentInParent<PortalMaster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (master.portals[0].portalUsed && master.portals[1].portalUsed)
        {
            master.portals[0].portalUsed = false;
            master.portals[1].portalUsed = false;
            master.portalInUse = false;
        }
        else if (master.portals[0].portalUsed != master.portals[1].portalUsed)
        {
            master.portalInUse = true;
        }

        if (!master.portalInUse && master.characterLayer == (master.characterLayer | (1 << other.gameObject.layer)))
        {
            other.gameObject.GetComponent<CharacterMovement>().warpPos = master.Teleport(other.transform, Convert.ToInt32(portalOne), Convert.ToInt32(!portalOne));
            master.portalInUse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (master.portalInUse && master.characterLayer == (master.characterLayer | (1 << other.gameObject.layer)))
        {
            portalUsed = true;
        }
    }
}
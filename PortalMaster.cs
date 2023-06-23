using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMaster : MonoBehaviour
{
    public Portal[] portals = new Portal[2];

    public LayerMask characterLayer;

    public bool portalInUse;

    // Start is called before the first frame update
    void Start()
    {
        portals[1].portalOne = true;
    }

    public Vector3 Teleport(Transform character, int enterPortal, int exitPortal)
    {
        Vector3 offset = character.transform.position - portals[enterPortal].transform.position;
        return portals[exitPortal].transform.position + offset;
    }
}

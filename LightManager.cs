using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Transform lightCharacter, darkCharacter;

    public float lightStrength, darkStrength;

    [SerializeField] List<Light> characterLights = new List<Light>();

    public float boarderline;

    [SerializeField] LayerMask lightBlockingLayers;

    //public float angle;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] lights = GameObject.FindGameObjectsWithTag("UseLight");
        foreach (GameObject light in lights)
            characterLights.Add(light.GetComponent<Light>());
        
    }

    // Update is called once per frame
    void Update()
    {
        darkStrength = 0;
        lightStrength = 0;
        for (int i = 0; i < characterLights.Count; i++)
        {
            darkStrength += GetStrength(darkCharacter, characterLights[i]);
            lightStrength += GetStrength(lightCharacter, characterLights[i]);
        }
    }

    public float GetStrength(Transform character, Light light)
    {
        RaycastHit hit;
        float strength = 0;
        float distance = Vector3.Distance(light.transform.position, character.position);
        if (light.type == LightType.Directional || distance < light.range)
        {
            switch (light.type)
            {
                case LightType.Spot:
                    Vector3 direction = character.position - light.transform.position;
                    float angle = Vector3.Angle(light.transform.forward, direction.normalized);
                    Debug.DrawRay(light.transform.position, light.transform.forward);
                    Debug.DrawRay(light.transform.position, direction);
                    if (angle < light.spotAngle)
                    {
                        if (Physics.Raycast(light.transform.position, character.position - light.transform.position, out hit, light.range, lightBlockingLayers) && hit.collider.gameObject.layer == character.gameObject.layer)
                        {
                            //float n = (distance * distance) / (light.range * light.range);
                            //n = 1f - Mathf.Clamp01(n * n);
                            //strength = (light.intensity * (n * n)) / 1000f;
                            ////strength = light.intensity / (distance * distance);
                            //Vector3 closestPointOnLightRay = light.transform.position + Vector3.Project(direction, light.transform.forward);
                            //Vector3 maxDistance = light.transform.position + (light.transform.forward * Mathf.Cos(Mathf.Deg2Rad * (light.spotAngle / 2)) * light.range);
                            //distance = Vector3.Distance(character.position, ClosestPoint(light.transform.position, closestPointOnLightRay, maxDistance));
                            ////strength *= 1f - Mathf.Clamp01(distance / (Mathf.Sin(Mathf.Deg2Rad * (light.spotAngle / 2)) * light.range));
                            //strength /= (distance * distance);

                            float distanceSqr = distance * distance;
                            float rangeAttenuation;
                            rangeAttenuation = Mathf.Pow(Mathf.Clamp01(1f - Mathf.Pow(distanceSqr / Mathf.Pow(light.range, 2), 2)), 2);

                            float innerCos = Mathf.Cos(Mathf.Deg2Rad * 0.5f * light.innerSpotAngle);
                            float outerCos = Mathf.Cos(Mathf.Deg2Rad * 0.5f * light.spotAngle);

                            float angleAttenuation = Mathf.Pow(Mathf.Clamp01((Vector3.Dot(light.transform.forward, direction.normalized) - (outerCos / innerCos - outerCos))),2);
                            strength = light.intensity * (rangeAttenuation / distanceSqr) * angleAttenuation;


                        }
                    }
                    break;
                case LightType.Directional:
                    Debug.DrawRay(character.position, -light.transform.forward * 100);
                    if (!Physics.Raycast(character.position, -light.transform.forward, Mathf.Infinity, lightBlockingLayers))
                    {
                        strength = light.intensity;
                    }
                    break;
                case LightType.Point:
                    if (Physics.Raycast(light.transform.position, character.position - light.transform.position, out hit, light.range, lightBlockingLayers) && hit.collider.gameObject.layer == character.gameObject.layer)
                    {
                        Debug.DrawLine(light.transform.position, hit.point);


                        float distanceSqr = distance * distance;
                        float rangeAttenuation;
                        rangeAttenuation = Mathf.Pow(Mathf.Clamp01(1f - Mathf.Pow(distanceSqr / Mathf.Pow(light.range, 2), 2)), 2);
                        strength = light.intensity * (rangeAttenuation / distanceSqr);

                    }
                    break;
                case LightType.Area:
                    break;
                default:
                    break;
            }
        }
        return strength;
    }
    Vector3 ClosestPoint(Vector3 origin, Vector3 pointA, Vector3 pointB)
    {
        float distanceToPointA = Vector3.Distance(origin, pointA);
        float distanceToPointB = Vector3.Distance(origin, pointB);
        return distanceToPointA < distanceToPointB ? pointA : pointB;
    }
}
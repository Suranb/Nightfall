using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class FadeObjectsBlockingObject : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float fadedAlpha = 0.33f;
    [SerializeField]
    private bool retainShadows = true;
    [SerializeField]
    private Vector3 targetPositionOffset = Vector3.up;

    [SerializeField]
    private float fadeSpeed = 1;

    [Header("Read Only Daya")]
    [SerializeField]
    private List<FadingObject> ObjectsBlockingView = new List<FadingObject>();
    private Dictionary<FadingObject, Coroutine> RunningCoroutines = new Dictionary<FadingObject, Coroutine>();

    private RaycastHit[] Hits = new RaycastHit[100];

    private void Start()
    {
        StartCoroutine(CheckForObjects());
    }

    private IEnumerator CheckForObjects()
    {
        while (true)
        {
            int hits = Physics.RaycastNonAlloc(
                cam.transform.position, 
                (target.transform.position + targetPositionOffset - cam.transform.position).normalized,
                Hits,
                Vector3.Distance(cam.transform.position, target.transform.position + targetPositionOffset),
                layerMask
            );

            if(hits > 0)
            {
                for(int i = 0; i < hits; i++)
                {
                    FadingObject fadingObject = GetFadingObjectsFromHit(Hits[i]);

                    if(fadingObject != null && !ObjectsBlockingView.Contains(fadingObject))
                    {
                        if (RunningCoroutines.ContainsKey(fadingObject))
                        {
                            if(RunningCoroutines[fadingObject] != null)
                            {
                                StopCoroutine(RunningCoroutines[fadingObject]);
                            }

                            RunningCoroutines.Remove(fadingObject);
                        }

                        RunningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectOut(fadingObject)));
                        ObjectsBlockingView.Add(fadingObject);
                    }
                }
            }

            FadeObjectsNoLongerBeingHit();

            ClearHits();

            yield return null;
        }
    }

    private void FadeObjectsNoLongerBeingHit()
    {
        List<FadingObject> objectsToRemove = new List<FadingObject>(ObjectsBlockingView.Count);

        foreach(FadingObject fadingObject in ObjectsBlockingView)
        {
            bool objectsIsBeingHit = false;
            for(int i = 0; i < Hits.Length; i++)
            {
                FadingObject hitFadingObject = GetFadingObjectsFromHit(Hits[i]);
                if(hitFadingObject != null && fadingObject == hitFadingObject)
                {
                    objectsIsBeingHit = true;
                    break;
                }
            }

            if (!objectsIsBeingHit)
            {
                if (RunningCoroutines.ContainsKey(fadingObject))
                {
                    if (RunningCoroutines[fadingObject] != null)
                    {
                        StopCoroutine(RunningCoroutines[fadingObject]);
                    }
                    RunningCoroutines.Remove(fadingObject);
                }

                RunningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectIn(fadingObject)));
                objectsToRemove.Add(fadingObject);
            }

        }
        
        foreach(FadingObject removeObject in objectsToRemove)
        {
            ObjectsBlockingView.Remove(removeObject);
        }
    }


    private IEnumerator FadeObjectOut(FadingObject fadingObject)
    {
        foreach(Material material in fadingObject.Materials)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.SetInt("_Surface", 1);

            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            material.SetShaderPassEnabled("DepthOnlt", false);
            material.SetShaderPassEnabled("SHADOWCASTER", retainShadows);

            material.SetOverrideTag("RenderType", "Transparent");

            material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        }

        float time = 0;

        while (fadingObject.Materials[0].color.a > fadedAlpha)
        {
            foreach(Material material in fadingObject.Materials)
            {
                if (material.HasProperty("_Color"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp(fadingObject.initialAlpha, fadedAlpha, time * fadeSpeed)
                    );
                }
            }

            time += Time.deltaTime;
            yield return null;
        }

        if (RunningCoroutines.ContainsKey(fadingObject))
        {
            StopCoroutine(RunningCoroutines[fadingObject]);
            RunningCoroutines.Remove(fadingObject);
        }
    }

    private IEnumerator FadeObjectIn(FadingObject fadingObject)
    {
        float time = 0;

        while (fadingObject.Materials[0].color.a < fadingObject.initialAlpha)
        {
            foreach (Material material in fadingObject.Materials)
            {
                if (material.HasProperty("_Color"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp( fadedAlpha, fadingObject.initialAlpha, time * fadeSpeed)
                    );
                }
            }

            time += Time.deltaTime;
            yield return null;
        }

        foreach (Material material in fadingObject.Materials)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstcBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.SetInt("_Surface", 0);

            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

            material.SetShaderPassEnabled("DepthOnlt", true);
            material.SetShaderPassEnabled("SHADOWCASTER", true);

            material.SetOverrideTag("RenderType", "Opaque");

            material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }

        if (RunningCoroutines.ContainsKey(fadingObject))
        {
            StopCoroutine(RunningCoroutines[fadingObject]);
            RunningCoroutines.Remove(fadingObject);
        }

        
    }

    private void ClearHits()
    {
        System.Array.Clear(Hits, 0, Hits.Length);
    }

    private FadingObject GetFadingObjectsFromHit(RaycastHit Hit)
    {
        return Hit.collider != null ? Hit.collider.GetComponent<FadingObject>() : null;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    List<Renderer> geometryRenderers;

    [SerializeField] Material vaporwaveMaterial;
    [SerializeField] Material Y2KMaterial;

    private void Start()
    {
        //SetMaterialState(GravityState.Down);
        LevelManager.Instance.StartLevelEvent.AddListener(SetLevel);
    }

    void SetLevel(Level level)
    {
        geometryRenderers = level.geometryRenderers;
        SetMaterialState(GravityState.Down);

        foreach (Renderer r in geometryRenderers)
        {
            r.material = Y2KMaterial;
            //r.sharedMaterial.SetFloat("_FadeValue", 0);
        }
    }

    public void SetMaterialState(GravityState state)
    {
        Material m;
        if (state == GravityState.Up)
        {
            m = vaporwaveMaterial;
        }
        else
        {
            m = Y2KMaterial;
        }

        foreach (Renderer r in geometryRenderers)
        {
            r.material = m;

            r.sharedMaterial.SetFloat("_FadeValue", 0.5f);
        }
    }

    public void SetFadeState(float fade)
    {
        if (geometryRenderers != null)
        {
            foreach (Renderer r in geometryRenderers)
            {
                if (fade == 0)
                {
                    r.material = Y2KMaterial;
                } else
                {
                    r.material = vaporwaveMaterial;
                    r.sharedMaterial.SetFloat("_FadeValue", fade);
                }
            }
        }
        

    }
}

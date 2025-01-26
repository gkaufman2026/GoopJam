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
        }
    }
}

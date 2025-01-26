using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TestTurningOffOutlines : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] UniversalRendererData rednerObj;
    ScriptableRendererFeature rendererFeature;
    void Start()
    {
        rendererFeature = rednerObj.rendererFeatures.ElementAt(2);
        rednerObj.rendererFeatures.RemoveAt(2);
        rednerObj.rendererFeatures.Add(rendererFeature);
      
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}

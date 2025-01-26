using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] UniversalRendererData piplineObj;
    ScriptableRendererFeature rendererCopy;
    [SerializeField] GravityTraveller gravityTraveller;
    bool isOn = true;

    void Start()
    {
        //grab the copy
        if (piplineObj == null)
        {
            Debug.Log("Object NOT BOUND");
        }

        int top = piplineObj.rendererFeatures.Capacity;
        rendererCopy = piplineObj.rendererFeatures[top - 1];

        //add listener
        gravityTraveller.gravityEvent.AddListener(disableProcess);


    }

    // Update is called once per frame
    void Update()
    {

    }

    void disableProcess(GravityState state)
    {
        Debug.Log("recived event");
        if(state == GravityState.Down)
        {
            //want shader
            // piplineObj.rendererFeatures.Add(rendererCopy);
            // isOn = true;
            rendererCopy.SetActive(true);

        }
        else if (state == GravityState.Up)
        {
            //lose it
            rendererCopy.SetActive(false);
            
            //int cap = piplineObj.rendererFeatures.Capacity - 1;
            //Debug.Log("Removing post process " + cap);
            //piplineObj.rendererFeatures.RemoveAt(cap);
            //isOn = false;


        }
       
    }
}

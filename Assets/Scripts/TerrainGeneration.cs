using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{

    [SerializeField] private float maxXRange = 10;
    [SerializeField] private float pas = 1;
    [SerializeField] private GameObject point = null;
    [SerializeField] private GameObject pointD = null;
    [SerializeField] private GameObject pointDD = null;

    //[SerializeField] private FunctionsType functionsType = FunctionsType.SINUS;

    private Functions currentFunction = null;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentFunction = new SineFunc(FunctionsType.SINUS, 0.0f, 3f,1f,0f);
        float i2 = -Mathf.PI / 2.0f;
        float offset = 0;
        for (float i = Mathf.PI / 2.0f; i < (Mathf.PI*3.0)/2; i+=pas)
        {
            i2 += pas;
            float y = currentFunction.useFunc(i);
            float yd = currentFunction.useFirstDerivativeFunc(i);
            float ydd = currentFunction.useSecondDerivativeFunc(i);

            GameObject.Instantiate(point, new Vector3(i, y, 0), Quaternion.identity);
            
            float y2 = currentFunction.useFunc(i2);
            float y2d = currentFunction.useFirstDerivativeFunc(i2);
            float y2dd = currentFunction.useSecondDerivativeFunc(i2);
            
            GameObject.Instantiate(pointD, new Vector3(((Mathf.PI*3.0f)/2.0f)+offset, y2, 0), Quaternion.identity);
            offset += pas;

        }
        float offsetMax = ((Mathf.PI * 3.0f) / 2.0f) - (Mathf.PI / 2.0f);
        float yoff = currentFunction.useFunc(i2 +offsetMax);
        i2 = Mathf.PI / 2.0f;
        currentFunction = new SineFunc(FunctionsType.SINUS, 0.0f, 0.2f,1f,0f);
        for (float i = Mathf.PI / 2.0f; i < (Mathf.PI*3.0)/2; i+=pas)
        {
            i2 += pas;
            float y2 = currentFunction.useFunc(i2);
            float y2d = currentFunction.useFirstDerivativeFunc(i2);
            float y2dd = currentFunction.useSecondDerivativeFunc(i2);
            
            GameObject.Instantiate(pointDD, new Vector3((((Mathf.PI*3.0f)/2.0f))+offset, y2-1.065f*yoff, 0), Quaternion.identity);
            offset += pas;
            //GameObject.Instantiate(pointD, new Vector3(((Mathf.PI*3.0f)/2.0f)+i2, y2d, 0), Quaternion.identity);

        }
        //currentFunction = new Functions(functionsType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

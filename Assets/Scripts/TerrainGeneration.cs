using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{

    [SerializeField] private float maxXRange = 0;
    [SerializeField] private float minXRange = 0;
    [SerializeField] private float minYRange = 0;
    [SerializeField] private float maxYRange = 0;

    [SerializeField] private FunctionsType functionsType = FunctionsType.SINUS;

    private Functions currentFunction = null;
    
    // Start is called before the first frame update
    void Start()
    {
        currentFunction = new Functions(functionsType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

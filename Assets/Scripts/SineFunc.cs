using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineFunc : Functions
{
    private float offset;
    private float A;
    private float omega;// w grec
    private float phi; //op grec    
    
    public SineFunc(FunctionsType _functionsType)
    {
        functionType = _functionsType;   
    }
    public SineFunc(FunctionsType _functionsType, float _offset, float _A, float _omega, float _phi)
    {
        functionType = _functionsType;
        A = _A;
        offset = _offset;
        omega = _omega;
        phi = _phi;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // k = omega c = phi
    public override float useFunc(float x)
    {
        return offset + (A * Mathf.Sin((omega*x)+phi));
    }

    public override float useFirstDerivativeFunc(float x)
    {
        return A * Mathf.Cos((omega*x)+phi) * omega;
    }

    public override float useSecondDerivativeFunc(float x)
    {
        return -A * (omega*omega)* Mathf.Sin((omega*x)+phi);
    }
}

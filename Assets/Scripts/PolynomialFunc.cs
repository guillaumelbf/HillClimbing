using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PolynomialFunc : Functions
{
    public float a;
    public float b;
    public float c;
    public float d;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public PolynomialFunc(FunctionsType _functionsType, float _a, float _b, float _c, float _d)
    {
        functionType = _functionsType;
        a = _a;
        b = _b;
        c = _c;
        d = _d;
    }

    private float Horner(float x, float[] p)
    {

        int n = p.Length;
        float result = 0;
        for (int i = n-1; i > -1; i--)
        {
            result = result * x + p[i];
        }


        return result;
    }
    public override float useFunc(float x)
    {
        return Horner(x,  new []{d, c, b, a});
    }

    public override float useFirstDerivativeFunc(float x)
    {
        return 3.0f * a * (x*x) + 2.0f * b * x +c;
    }

    public override float useSecondDerivativeFunc(float x)
    {
        return 6.0f * a * x + 2.0f * b;
    }
}

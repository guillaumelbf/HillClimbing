using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperbolicFunc : Functions
{
    // Start is called before the first frame update
    public float a;
    public float b;
    public float k;
    public float kPrime;
    public float alpha = -6;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public HyperbolicFunc(FunctionsType _functionsType, float _a, float _b, float _k, float _kP)
    {
        functionType = _functionsType;
        a = _a;
        b = _b;
        k = _k;
        kPrime = _kP;
    }

    public override float useFunc(float x)
    {
        return a + kPrime * ((1.0f-Mathf.Exp(alpha * k*(x-b)))/(1.0f+Mathf.Exp(alpha * k*(x-b))));
    }

    public override float useFirstDerivativeFunc(float x)
    {
        return -((2.0f * kPrime * Mathf.Exp(k*x*alpha - b*k*alpha) * k*alpha)/
                 ((1.0f+Mathf.Exp(k*x*alpha - b*k*alpha))*(1.0f + Mathf.Exp(k * x * alpha - b * k * alpha))));
    }

    public override float useSecondDerivativeFunc(float x)
    {
        
        return ((2.0f*k*k*kPrime*alpha*alpha*Mathf.Exp(2*k*x*a-2*b*k*alpha))-
                (2.0f*k*k*kPrime*alpha*alpha*Mathf.Exp(k*x*a-b*k*alpha))) /
                ((1.0f + Mathf.Exp(k * x * a - b * k * alpha)) 
                 *(1.0f + Mathf.Exp(k * x * a - b * k * alpha)) 
                 *(1.0f + Mathf.Exp(k * x * a - b * k * alpha)));
    }
}

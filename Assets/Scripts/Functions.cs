using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum FunctionsType
{
    SINUS,
    ELLIPTIC,
    HYPERBOLIC,
    POLYNOMIAL,
    RANDOM
}

public class Functions : MonoScript
{
    protected FunctionsType functionType;

    public Functions()
    {
        
    }
    /*public Functions(FunctionsType _functionsType)
    {
        functionType = _functionsType;
    }*/

    public virtual float useFunc(float x)
    {
        return 0;
    }
    public virtual float useFirstDerivativeFunc(float x)
    {
        return 0;
    }
    public virtual float useSecondDerivativeFunc(float x)
    {
        return 0;
    }
}

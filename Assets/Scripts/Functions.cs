using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FunctionsType
{
    SINUS
}

public class Functions
{
    private FunctionsType functionType;

    public Functions(FunctionsType _functionsType)
    {
        functionType = _functionsType;
    }
}

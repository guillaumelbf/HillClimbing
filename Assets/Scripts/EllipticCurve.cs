using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipticCurve : Functions
{
    // Start is called before the first frame update
    private EllipticFunc BeginCircle;
    private EllipticFunc EndCircle;
    public float MiddleX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public EllipticCurve(FunctionsType _functionsType, Vector2 Begin, Vector2 End)
    {
        functionType = _functionsType;
        MiddleX = (End.x - Begin.x) / 2.0f;
        BeginCircle = new EllipticFunc();
        BeginCircle.a = End.y;
        BeginCircle.kPrime = Begin.y -End.y;
        BeginCircle.k = Mathf.Sqrt(3)/ (End.x - Begin.x);
        BeginCircle.b = Begin.x;
        EndCircle = new EllipticFunc();
        EndCircle.a = Begin.y;
        EndCircle.kPrime = -(Begin.y -End.y);
        EndCircle.k = Mathf.Sqrt(3)/ (End.x - Begin.x);
        EndCircle.b = End.x;
        
    }

    public override float useFunc(float x)
    {
        return x <= MiddleX ? BeginCircle.useFunc(x) :
            EndCircle.useFunc((x));
    }

    public override float useFirstDerivativeFunc(float x)
    {
        return x <= MiddleX ? BeginCircle.useFirstDerivativeFunc(x) :
            EndCircle.useFirstDerivativeFunc((x));
    }

    public override float useSecondDerivativeFunc(float x)
    {
        return x <= MiddleX ? BeginCircle.useSecondDerivativeFunc(x) : 
            EndCircle.useSecondDerivativeFunc((x));
    }
}

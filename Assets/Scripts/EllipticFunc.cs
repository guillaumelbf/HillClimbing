using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipticFunc : Functions
{
    public float a;
    public float kPrime;
    public float k;
    public float b;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override float useFunc(float x)
    {
        return a + kPrime * Mathf.Sqrt(1.0f- ( (k * (x - b)) * (k * (x - b)) ));
    }

    public override float useFirstDerivativeFunc(float x)
    {
        return (-(k*k) * kPrime * x + b * (k*k) * kPrime) / 
               Mathf.Sqrt(1.0f - (k*k) * (x*x) + 2.0f *b*(k*k) *x - (b*b)*(k*k));
    }

    public override float useSecondDerivativeFunc(float x)
    {
        return -( ((k*k)*kPrime) / 
                ( Mathf.Sqrt(1.0f - (k*k*x*x) + 2.0f*b*(k*k)*x -(b*b*k*k)) *
                ( 1.0f - (k*k*x*x)+ 2.0f*b*(k*k)*x -(b*b*k*k) ) ) );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FuncBox : MonoScript
{
    [SerializeField]
    private Vector2 beginPoint;
    [SerializeField]
    private Vector2 endPoint;
    
    [SerializeField]
    private Functions func;

    private bool isInit = false;
    private List<Vector2> points = new List<Vector2>();
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void InitFunc(Vector2 Begin, Vector2 MaxSize , Vector2 MinSize)
    {
        if (MinSize.x < 0)
            MinSize.x = -MinSize.x;
        if (MinSize.y < 0)
            MinSize.y = -MinSize.y;
        if (MaxSize.x < 0)
            MaxSize.x = -MaxSize.x;
        if (MaxSize.y< 0)
            MaxSize.y = -MaxSize.y;
        float MIN = 0.5f;
        if (MinSize.x < MIN)
            MinSize.x = MIN;
        if (MinSize.y < MIN)
            MinSize.y = MIN;

        
        if (MaxSize.x < MinSize.x)
            MaxSize.x = MinSize.x + 1.0f;
        if (MaxSize.y < MinSize.y)
            MaxSize.y = MinSize.y + 1.0f;
        if (isInit)
            return;
        beginPoint = Begin;
        int rand = Random.Range(1, 100);
        //Debug.LogWarning($"Rand {rand}");
        // begin point is endpoint of last box
        float coef = 1.0f - Begin.y / 10.0f;
        rand = (int)(rand * coef);
        if (rand >= 20)
        {
            //ascendante
            float x = Random.Range(MinSize.x,  MaxSize.x);
            float y = Random.Range(MinSize.y,  MaxSize.y);
            float omega = (1.0f / (x/2.0f)) * (Mathf.PI / 2.0f);
            //Debug.LogWarning($"OFFSET = {y/2.0f} , A = {y/2.0f}, OMEGA = {omega}, PHI = {-(Mathf.PI /2.0f)}");
            func = new SineFunc(FunctionsType.SINUS, y/2.0f, y/2.0f,omega, -(Mathf.PI /2.0f));
            endPoint = beginPoint +new Vector2(x,y);
        }
        else
        {
            //descendante
            float x = Random.Range(+MinSize.x,   MaxSize.x);
            float y = Random.Range(-MinSize.y, - MaxSize.y);
            float omega = (1.0f / (x/2.0f)) * (Mathf.PI / 2.0f);
            //Debug.LogWarning($"OFFSET = {-y/2.0f} , A = {y/2.0f}, OMEGA = {omega}, PHI = {-(Mathf.PI /2.0f)*3.0f}");
            func = new SineFunc(FunctionsType.SINUS, y/2.0f, y/2.0f,omega, -(Mathf.PI /2.0f)/**3.0f*/);
            endPoint = BeginPoint +new Vector2(x,y);
        }

        isInit = true;
    }
    public List<Vector2> Compute(float pas)
    {
        if (!isInit)
            return null;
        points.Clear();
        float max = endPoint.x - beginPoint.x;
        for (float i = 0; i < max; i+=pas)
        {
            float x = beginPoint.x + i;
            float y = beginPoint.y + func.useFunc(i);
            points.Add(new Vector2(x,y));
        }



        return points;
    }

    public Vector2 BeginPoint => beginPoint;

    public Vector2 EndPoint => endPoint;

    // Update is called once per frame
    void Update()
    {
        
    }
}

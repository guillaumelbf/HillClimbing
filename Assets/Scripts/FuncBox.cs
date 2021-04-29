using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class FuncBox : MonoScript
{
    [SerializeField]
    private Vector2 beginPoint;
    [SerializeField]
    private Vector2 endPoint;
    
    [SerializeField]
    private Functions func;

    private bool isInit = false;
    public List<Vector2> points = new List<Vector2>();
    public List<Vector2> Primepoints = new List<Vector2>();
    public List<Vector4> localpoints = new List<Vector4>();
    [SerializeField] 
    public GameObject quad;

    public Material overMat;
    public Material underMat;
    private GameObject overquad;
    private GameObject underquad;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void InitFuncSine(Vector2 Begin, Vector2 MaxSize , Vector2 MinSize)
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

        quad.transform.position = (beginPoint + endPoint) * 0.5f;
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        quad.GetComponent<MeshRenderer>().material.color = new Color(r,g,b,1);
        quad.GetComponent<DebugBox>().BeginPoint = beginPoint;
        quad.GetComponent<DebugBox>().EndPoint = endPoint;
        
        quad.transform.localScale = new Vector3(endPoint.x-beginPoint.x,Mathf.Abs(endPoint.y-beginPoint.y),1);
        overquad = GameObject.Instantiate(quad, quad.transform.position, Quaternion.identity);
        overquad.transform.localScale = new Vector3(quad.transform.localScale.x, 20,1);
        overquad.transform.position += new Vector3(0,quad.transform.localScale.y /2 + overquad.transform.localScale.y/2,0);
        overquad.GetComponent<MeshRenderer>().material = overMat;
        underquad = GameObject.Instantiate(quad, quad.transform.position, Quaternion.identity);
        underquad.transform.localScale = new Vector3(quad.transform.localScale.x, 20,1);

        underquad.transform.position -= new Vector3(0,quad.transform.localScale.y /2 + underquad.transform.localScale.y/2,0);

        underquad.GetComponent<MeshRenderer>().material = underMat;
        isInit = true;
    }

    public void InitFuncElliptic(Vector2 Begin, Vector2 MaxSize, Vector2 MinSize)
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
          
            //Debug.LogWarning($"OFFSET = {y/2.0f} , A = {y/2.0f}, OMEGA = {omega}, PHI = {-(Mathf.PI /2.0f)}");
            
            endPoint = beginPoint +new Vector2(x,y);
            func = new EllipticCurve(FunctionsType.ELLIPTIC, new Vector2(0,0), new Vector2(x,y));
        }
        else
        {
            //descendante
            float x = Random.Range(+MinSize.x,   MaxSize.x);
            float y = Random.Range(-MinSize.y, - MaxSize.y);
           
            func = new EllipticCurve(FunctionsType.ELLIPTIC, new Vector2(0,0), new Vector2(x,y));

            endPoint = BeginPoint +new Vector2(x,y);
        }
        
        quad.transform.position = (beginPoint + endPoint) * 0.5f;
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        quad.GetComponent<MeshRenderer>().material.color = new Color(r,g,b,1);
        quad.GetComponent<DebugBox>().BeginPoint = beginPoint;
        quad.GetComponent<DebugBox>().EndPoint = endPoint;
        
        quad.transform.localScale = new Vector3(endPoint.x-beginPoint.x,Mathf.Abs(endPoint.y-beginPoint.y),1);
        overquad = GameObject.Instantiate(quad, quad.transform.position, Quaternion.identity);
        overquad.transform.localScale = new Vector3(quad.transform.localScale.x, 20,1);
        overquad.transform.position += new Vector3(0,quad.transform.localScale.y /2 + overquad.transform.localScale.y/2,0);
        overquad.GetComponent<MeshRenderer>().material = overMat;
        underquad = GameObject.Instantiate(quad, quad.transform.position, Quaternion.identity);
        underquad.transform.localScale = new Vector3(quad.transform.localScale.x, 20,1);

        underquad.transform.position -= new Vector3(0,quad.transform.localScale.y /2 + underquad.transform.localScale.y/2,0);

        underquad.GetComponent<MeshRenderer>().material = underMat;
        isInit = true;
    }
    public void InitFuncHyperbolic(Vector2 Begin, Vector2 MaxSize, Vector2 MinSize)
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
          
            //Debug.LogWarning($"OFFSET = {y/2.0f} , A = {y/2.0f}, OMEGA = {omega}, PHI = {-(Mathf.PI /2.0f)}");
            
            endPoint = beginPoint +new Vector2(x,y);
            Vector2 beg = new Vector2(0, 0);
            Vector2 end = new Vector2(x, y);
            func = new HyperbolicFunc(FunctionsType.HYPERBOLIC, (end.y + beg.y)/2.0f,
                (beg.x + end.x)/2.0f,
                2.0f/(end.x-beg.x),
                (end.y - beg.y)/2.0f);
        }
        else
        {
            //descendante
            float x = Random.Range(+MinSize.x,   MaxSize.x);
            float y = Random.Range(-MinSize.y, - MaxSize.y);
            Vector2 beg = new Vector2(0, 0);
            Vector2 end = new Vector2(x, y);
            func = new HyperbolicFunc(FunctionsType.HYPERBOLIC, (end.y + beg.y)/2.0f,
                (beg.x + end.x)/2.0f,
                2.0f/(end.x-beg.x),
                (end.y - beg.y)/2.0f);

            endPoint = BeginPoint +end;
        }
        
        quad.transform.position = (beginPoint + endPoint) * 0.5f;
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        quad.GetComponent<MeshRenderer>().material.color = new Color(r,g,b,1);
        quad.GetComponent<DebugBox>().BeginPoint = beginPoint;
        quad.GetComponent<DebugBox>().EndPoint = endPoint;
        
        quad.transform.localScale = new Vector3(endPoint.x-beginPoint.x,Mathf.Abs(endPoint.y-beginPoint.y),1);
        overquad = GameObject.Instantiate(quad, quad.transform.position, Quaternion.identity);
        overquad.transform.localScale = new Vector3(quad.transform.localScale.x, 20,1);
        overquad.transform.position += new Vector3(0,quad.transform.localScale.y /2 + overquad.transform.localScale.y/2,0);
        overquad.GetComponent<MeshRenderer>().material = overMat;
        underquad = GameObject.Instantiate(quad, quad.transform.position, Quaternion.identity);
        underquad.transform.localScale = new Vector3(quad.transform.localScale.x, 20,1);

        underquad.transform.position -= new Vector3(0,quad.transform.localScale.y /2 + underquad.transform.localScale.y/2,0);

        underquad.GetComponent<MeshRenderer>().material = underMat;
        isInit = true;
    }
    public List<Vector2> Compute(float pas)
    {
        if (!isInit)
            return null;
        points.Clear();
        float max = endPoint.x - beginPoint.x;
        float fmax = endPoint.y - beginPoint.y;
        for (float i = 0; i < max; i+=pas)
        {
            float x = beginPoint.x + i;
            float y = beginPoint.y + func.useFunc(i);
            points.Add(new Vector2(x,y));
        }
        for (float i = 0; i < max; i+=pas)
        {
            float x =  i;
            float y =  func.useFirstDerivativeFunc(i);
            Primepoints.Add(new Vector2(x,y));
        }
        //POINT FOR SHADER
        for (float i = 0; i < max; i+= 0.39f)
        {
            if(fmax >= 0)
                localpoints.Add(new Vector2(i/max, func.useFunc(i)/fmax));
            else
            {
                localpoints.Add(new Vector2(i/max, 1.0f- func.useFunc(i)/fmax));
            }
        }
        quad.GetComponent<MeshRenderer>().material.SetInt("pointsNum", localpoints.Count);
        Texture2D t = new Texture2D(localpoints.Count, 1);
        for (int i = 0; i < localpoints.Count; i++)
        {
            t.SetPixel(i,0, localpoints[i]);
        }
       //t.SetPixel(localpoints.Count-1,0, Color.yellow);
       t.Apply();
       quad.GetComponent<MeshRenderer>().material.SetTexture("pointsT",t);
       quad.GetComponent<MeshRenderer>().material.SetColor("underCol",underMat.color);
       quad.GetComponent<MeshRenderer>().material.SetColor("overCol",overMat.color);

        quad.GetComponent<DebugBox>().localpoints = localpoints;
        
        return points;
    }

    public Vector2 BeginPoint => beginPoint;

    public Vector2 EndPoint => endPoint;

    public bool IsInBox(Vector2 v)
    {
        return (v.x >= BeginPoint.x && v.x <= endPoint.x);

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        Destroy(quad);
        Destroy(overquad);
        Destroy(underquad);
    }
}

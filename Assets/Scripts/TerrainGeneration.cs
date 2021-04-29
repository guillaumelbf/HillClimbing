using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject quad ;
    [SerializeField] private int NumOfChuncks ;
    [SerializeField] private float min ;
    [SerializeField] private float max ;
    [SerializeField] private float pas ;
    [SerializeField] private Vector2 BeginGen = new Vector2(0,0);
    [SerializeField] private float Speed = 0.001f;
    [SerializeField] private float view = 5.0f;
    [SerializeField]public Material overMat;
    [SerializeField]public Material underMat;
    [SerializeField]public FunctionsType funcType;
    private PlayerMove pMove;
    
    private List<FuncBox> Boxes;
    private List<GameObject> Spheres;
    // Start is called before the first frame update
    void Start()
    {
        Boxes = new List<FuncBox>();
        Spheres = new List<GameObject>();
        for (int i = 0; i < NumOfChuncks; i++)
        {
            Vector2 oldPos = BeginGen;
            if (Boxes.Count != 0)
                oldPos = Boxes[Boxes.Count - 1].EndPoint;
            FuncBox box = new FuncBox();
            box.quad = GameObject.Instantiate(quad, new Vector3(0,0 ,0), Quaternion.identity);;
            box.underMat = underMat;
            box.overMat = overMat;
            switch (funcType)
            {
                case FunctionsType.SINUS:
                    box.InitFuncSine(oldPos, new Vector2(max,max), new Vector2(min,min));
                    break;
                case FunctionsType.ELLIPTIC:
                    box.InitFuncElliptic(oldPos, new Vector2(max,max), new Vector2(min,min));
                    break;
                case FunctionsType.HYPERBOLIC:
                    box.InitFuncHyperbolic(oldPos, new Vector2(max,max), new Vector2(min,min));
                    break;
                case FunctionsType.POLYNOMIAL:
                    box.InitFuncPolynomial(oldPos, new Vector2(max,max), new Vector2(min,min));
                    break;
                case FunctionsType.RANDOM:
                    int r = Random.Range(1, 5);
                    switch (r)
                    {
                        case 1:
                            box.InitFuncSine(oldPos, new Vector2(max,max), new Vector2(min,min));
                            break;
                        case 2:
                            box.InitFuncElliptic(oldPos, new Vector2(max,max), new Vector2(min,min));
                            break;
                        case 3:
                            box.InitFuncHyperbolic(oldPos, new Vector2(max,max), new Vector2(min,min));
                            break;
                        case 4:
                            box.InitFuncPolynomial(oldPos, new Vector2(max,max), new Vector2(min,min));
                            break;
                    }
                    break;
                
            }
            List<Vector2> points = box.Compute(pas);
           // foreach (var vec in points)
            {
               // GameObject g= GameObject.Instantiate(point, new Vector3(vec.x,vec.y ,0), Quaternion.identity);
              //  Spheres.Add(g);
            }
            Boxes.Add(box);
        }

        pMove = Player.GetComponent<PlayerMove>();
        Player.transform.position = Boxes[0].BeginPoint;
    }

    // Update is called once per frame
    void Update()
    {
        //FORCEPlayer.transform.position = Player.transform.position + new Vector3(Speed,0,0);
        if (Boxes.Count != 0)
        {
            float Pos = Player.transform.position.x + (view*max) ;
            Vector2 End = Boxes[Boxes.Count - 1].EndPoint;
            if (End.x <= Pos)
            {
                FuncBox box = new FuncBox();    
                box.quad = GameObject.Instantiate(quad, new Vector3(0,0 ,0), Quaternion.identity);
                box.underMat = underMat;
                box.overMat = overMat;
                switch (funcType)
                {
                    case FunctionsType.SINUS:
                        box.InitFuncSine(End, new Vector2(max,max), new Vector2(min,min));
                        break;
                    case FunctionsType.ELLIPTIC:
                        box.InitFuncElliptic(End, new Vector2(max,max), new Vector2(min,min));
                        break;
                    case FunctionsType.HYPERBOLIC:
                        box.InitFuncHyperbolic(End, new Vector2(max,max), new Vector2(min,min));
                        break;
                    case FunctionsType.POLYNOMIAL:
                        box.InitFuncPolynomial(End, new Vector2(max,max), new Vector2(min,min));
                        break;
                    case FunctionsType.RANDOM:
                        int r = Random.Range(1, 5);
                        switch (r)
                        {
                            case 1:
                                box.InitFuncSine(End, new Vector2(max,max), new Vector2(min,min));
                                break;
                            case 2:
                                box.InitFuncElliptic(End, new Vector2(max,max), new Vector2(min,min));
                                break;
                            case 3:
                                box.InitFuncHyperbolic(End, new Vector2(max,max), new Vector2(min,min));
                                break;
                            case 4:
                                box.InitFuncPolynomial(End, new Vector2(max,max), new Vector2(min,min));
                                break;
                        }
                        break;
                }
                List<Vector2> points = box.Compute(pas);
               // foreach (var vec in points)
                {
                    //GameObject g =GameObject.Instantiate(point, new Vector3(vec.x,vec.y ,0), Quaternion.identity);
                   // Spheres.Add(g);
                }
                Boxes.Add(box);
            }
        }
        if (Spheres.Count != 0)
        {
            float Pos = Player.transform.position.x  -view;
            float End = Spheres[0].transform.position.x;
            if (End <= Pos)
            {
                Destroy(Spheres[0]);
                Spheres.RemoveAt(0);
            }
        }
        if (Boxes.Count != 0)
        {
            float Pos = Player.transform.position.x  -view;
            float End = Boxes[0].EndPoint.x;
            if (End <= Pos)
            {
                Boxes[0].OnDestroy();
                Destroy(Boxes[0]);
                Boxes.RemoveAt(0);
            }
        }
        foreach (var box in Boxes)
        {
            if (box.IsInBox(Player.transform.position))
            {
                float pos = Player.transform.position.x - box.BeginPoint.x;
                int index = (int)(pos / pas);
                pMove.Compute(box.points[index], box.Primepoints[index]);
                //FORCEPlayer.transform.position = box.points[index];
            }
            //box.quad.transform.localScale = new Vector3(box.quad.transform.localScale.x, (Player.GetComponentInChildren<Camera>().orthographicSize *2 + Mathf.Abs(Player.transform.position.y)) * 2,1);
        }
    }
}

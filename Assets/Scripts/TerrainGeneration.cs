using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    
    [SerializeField] private GameObject point = null;
    [SerializeField] private GameObject Player = null;
    [SerializeField] private int NumOfChuncks = 2;
    [SerializeField] private float min = 2;
    [SerializeField] private float max = 5;
    [SerializeField] private float pas = 0.01f;
    [SerializeField] private Vector2 BeginGen = new Vector2(0,0);
    [SerializeField] private float Speed = 0.001f;
    [SerializeField] private float view = 5.0f;
    
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
            box.InitFunc(oldPos, new Vector2(max,max), new Vector2(min,min));
            List<Vector2> points = box.Compute(pas);
            foreach (var vec in points)
            {
                GameObject g= GameObject.Instantiate(point, new Vector3(vec.x,vec.y ,0), Quaternion.identity);
                Spheres.Add(g);
            }
            Boxes.Add(box);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Player.transform.position = Player.transform.position + new Vector3(Speed, 0, 0);
        if (Boxes.Count != 0)
        {
            float Pos = Player.transform.position.x + view;
            Vector2 End = Boxes[Boxes.Count - 1].EndPoint;
            if (End.x <= Pos)
            {
                FuncBox box = new FuncBox();    
                box.InitFunc(End, new Vector2(max,max), new Vector2(min,min));
                List<Vector2> points = box.Compute(pas);
                foreach (var vec in points)
                {
                    GameObject g =GameObject.Instantiate(point, new Vector3(vec.x,vec.y ,0), Quaternion.identity);
                    Spheres.Add(g);
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
    }
}

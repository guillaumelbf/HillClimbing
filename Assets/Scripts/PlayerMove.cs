using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 floor;
    [SerializeField] private float Grav;
    private Vector2 Force;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Compute(Vector2 floorPoint)
    {
        /*float dtGrav = Grav * Time.deltaTime;
        Force += new Vector2(0, transform.position.y - dtGrav);
        Vector2 nPos = new Vector2(transform.position.x, transform.position.y) + Force;
        if (nPos.y < floorPoint.y)
        {
            nPos.y = floorPoint.y;
        }
        floor = floorPoint;*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    private Vector2 BaseScale;
    // Start is called before the first frame update
    void Start()
    {
        BaseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = BaseScale * GetComponentInParent<Camera>().orthographicSize;
    }
}

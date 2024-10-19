using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // destory the object when go below ground
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }
}

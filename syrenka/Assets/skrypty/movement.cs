using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float MoveSpeed=5f;
    public Transform movepoint;

   void Start()
    {
        movepoint.parent = null;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movepoint.position) < 0.5f)
            {

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movepoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }
        }
    }
}

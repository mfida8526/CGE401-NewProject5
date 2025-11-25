using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Mimi Davis
* MoveForward
* Project5
* Has prefabs that spawn move forward
*/
public class MoveForward : MonoBehaviour
{
    public float speed = 40;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}

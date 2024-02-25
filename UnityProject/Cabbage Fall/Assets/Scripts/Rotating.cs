using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] private float speedRotating = 0.5f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 360 * speedRotating * Time.deltaTime);
    }
}

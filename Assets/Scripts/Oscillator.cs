using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;

    Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;                      // Grows over time

        const float tau = Mathf.PI * 2;                         // Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);             // Goes from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;                // Recalculated to 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}

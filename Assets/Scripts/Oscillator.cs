using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) {return;} //Protect from NAN (Not A Number Eror ( / 0 ))
        float cycles = Time.time / period; // Constantly Cycle

        const float tau = Mathf.PI * 2; // Constant Value (6.282(Pi*2))
        float rawSignWave = Mathf.Sin(cycles * tau); // Sin wave with range -1, 1

       // Debug.Log(rawSignWave); //Uncomment to Debug
        movementFactor = (rawSignWave + 1f) / 2f; // Calc from 0 to 1 (Cleaner Numbers)

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}

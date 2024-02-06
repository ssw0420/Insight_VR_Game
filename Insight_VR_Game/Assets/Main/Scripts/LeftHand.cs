using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    LineRenderer render;
    public int jumpPower;

    private void Awake()
    {
        render = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        jumpPower = 1;
    }
    // Update is called once per frame
    private void Update()
    {
        PredictTrajectory(transform.position, Vector3.right * 3f + Vector3.up * jumpPower);
    }

    void PredictTrajectory(Vector3 startPos, Vector3 vet)
    {
        int stop = 60;
        float deltaTime = Time.fixedDeltaTime;
        Vector3 gravity = Physics.gravity;

        Vector3 position = startPos;
        Vector3 velocity = vet;

        for(int i = 0; i < stop; i++)
        {
            position += velocity * deltaTime + 0.5f * gravity * deltaTime * deltaTime;
            velocity += gravity * deltaTime;
        }

        render.SetPosition(1, position);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletTrial_VFX : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float speed;
    private LineRenderer lr;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Move towards 0
        color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * speed);

        // Update color
        lr.startColor = color;
        lr.endColor = color;
    }
}

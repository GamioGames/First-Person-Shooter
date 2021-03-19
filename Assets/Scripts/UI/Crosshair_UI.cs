using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair_UI : MonoBehaviour
{
    public float restingSize;
    public float maxSize;
    public float speed;
    public float size { get; private set; }
    private bool isMoving;

    private RectTransform croshair;

    private void Awake()
    {
        croshair = GetComponent<RectTransform>();
        size = restingSize;
        croshair.sizeDelta = new Vector2(size,size);
    }

    private void OnEnable()
    {
        EventManager.current.PlayerMovementEvent.AddListener(UpdateMoving);
    }

    private void OnDisable()
    {
        EventManager.current.PlayerMovementEvent.RemoveListener(UpdateMoving);
    }

    void UpdateMoving(bool isMoving)
    {
        this.isMoving = isMoving;
    }

    private void Update()
    {
        if (isMoving)
        {
            size = Mathf.SmoothStep(size, maxSize, Time.deltaTime * speed * 1.8f);
        }
        else
        {
            size = Mathf.Lerp(size, restingSize, Time.deltaTime * speed);
        }
        croshair.sizeDelta = new Vector2(size, size);
    }
}

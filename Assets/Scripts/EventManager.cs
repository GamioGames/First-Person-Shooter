using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Bool1Event : UnityEvent<bool> { }
public class Int2Event : UnityEvent<int, int> { }
public class String1Sprite1Event : UnityEvent<string, Sprite> { }
public class EventManager : MonoBehaviour
{
    #region Signgleton
    public static EventManager current;

    private void Awake()
    {
        if(current == null) { current = this; } else if (current != null) { Destroy(this); }
    }
    #endregion

    // Player
    public Bool1Event PlayerMovementEvent = new Bool1Event();
    public Bool1Event DamageEnemyEvent = new Bool1Event();

    // Weapons
    public Int2Event UpdateBulletsEvent = new Int2Event();
    public UnityEvent NewGunEvent = new UnityEvent();
    public String1Sprite1Event PickUpEvent = new String1Sprite1Event();
    public UnityEvent PickDownEvent = new UnityEvent();
}

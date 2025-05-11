using System;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory Instance { get; private set; }
    public static event Action OnKeyCollected;

    public bool HasKey { get; private set; }

    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CollectKey() {
        if (HasKey) return;
        HasKey = true;
        OnKeyCollected?.Invoke();
    }
}

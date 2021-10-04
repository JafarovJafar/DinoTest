using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool CanPass => _canPass;
    protected bool _canPass = true; // обычные вейпоинты мы можем проходить сразу не останавливаясь
}
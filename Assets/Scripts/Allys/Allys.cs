using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Allys : MonoBehaviour
{
    private int _numberAlly;
    public int NumberAlly{get{return _numberAlly;}}
    private static int _countAllys = 0;


    private void Awake()
    {
        _numberAlly = ++_countAllys;

        gameObject.AddComponent<AllysMovement>();
    }

    private void FixedUpdate()
    {
        
    }
}
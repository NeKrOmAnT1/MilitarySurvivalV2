using UnityEngine;

public class TargetDesignator : MonoBehaviour
{
    [SerializeField] private int _timeToDelete = 5;

    private void Start() => 
        Destroy(this.gameObject, _timeToDelete);
}

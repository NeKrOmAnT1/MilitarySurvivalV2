using UnityEngine;

public class AllysMovement : MonoBehaviour
{    
    private Allys _ally;
    private CharacterController _controller;
    private float _moveSpeed = 5f;
    private Vector2 _directionAnimation;
    private Transform _pointTransform;
    private PlayerView _playerView;
    
    public Vector3 MousePoint { get; private set; }

   
    private void Awake()
    {
        _ally = GetComponent<Allys>();     
        _controller = GetComponent<CharacterController>(); 
        
        _playerView = new PlayerView(gameObject.GetComponent<Animator>());
    }

    private void Start() => 
        _pointTransform = SquadScript.PointsSquad[_ally.NumberAlly - 1].transform;

    private void Update()
    {
        UpdateDirection();
        Move();             
    }


    private void UpdateDirection()
    {    
        if(SquadScript.PointsSquad[_ally.NumberAlly - 1].transform.position != null)
        {
            var position = SquadScript.PointsSquad[_ally.NumberAlly - 1].transform.position;
            Vector3 directionVector = position - transform.position;
            Quaternion rotation = Quaternion.Euler(0f, -transform.rotation.eulerAngles.y, 0f);
            directionVector = rotation * directionVector;
            _directionAnimation = new Vector2(directionVector.x, directionVector.z).normalized;
        }          
    }

    private void Move()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _pointTransform.position);

        if (distanceToTarget > SquadScript._distanceToPoint)
        {
            Vector3 direction = (_pointTransform.position - transform.position).normalized;
            _controller.Move(direction * _moveSpeed * Time.deltaTime);       
            Vector2 roundedDirection = new Vector2(Mathf.Round(_directionAnimation.x), Mathf.Round(_directionAnimation.y));
            _playerView.AnimationMove(roundedDirection);
        }
        else
        {          
            _directionAnimation = Vector2.zero;
            _playerView.AnimationMove(_directionAnimation);
        }
        transform.rotation = _pointTransform.rotation;
    }
}
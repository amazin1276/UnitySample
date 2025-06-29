using UnityEngine;
using UnityEngine.InputSystem;
using Expansion;
using Range = Expansion.Range;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    GameObject targetPlayer;
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    Rigidbody2D rb2;

    [SerializeField, Space]
    bool isRun;
    public bool IsRun { get { return isRun; } }
    [SerializeField]
    bool isRunToRight;
    public bool IsRunToRight { get { return isRunToRight; } }
    [SerializeField]
    bool isRunToLeft;
    public bool IsRunToLeft { get { return isRunToLeft; } }
    [SerializeField]
    bool isJump;
    public bool IsJump { get { return isJump; } }
    [SerializeField]
    bool isOnGround;
    [SerializeField]
    float jumpPower;
    [SerializeField]
    int maxJumpTime;
    [SerializeField]
    Range.Int rngLeastOfJumpTime;
    Range.Int Int;
    bool CanJump
    {
        get
        {
            bool _canJump = rngLeastOfJumpTime >= 1;
            return _canJump;
        }
    }

    [SerializeField, Space]
    float movingSpeed;
    public float MovingSpeed { get { return movingSpeed; } }
    [SerializeField, Tooltip("加速度")]
    /// <summary>
    /// 加速度
    /// </summary>
    float acceleration;
    public float Acceleration { get { return acceleration; } }
    [SerializeField]
    float maxSpeed;
    public float MaxSpeed { get { return maxSpeed; } }
    [SerializeField]
    public Vector2 initialPosition{ get; private set; }
    [SerializeField]
    Collider2D footCollider;


    void Start()
    {
        initialPosition = this.transform.position;
        rngLeastOfJumpTime = new Expansion.Range.Int(
            _max: maxJumpTime,
            _min: 0,
            _initValue: maxJumpTime,
            _isVariableRange: true,
            _isKeepInRange: true
            );
    }

    void Update()
    {
        rngLeastOfJumpTime.Max = maxJumpTime;

        ControlMovementBooleans();
        ControlMovingSpeed();
        Run();
    }


    void OnJump(InputValue _value)
    {
        if (!CanJump) return;

        rb2.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        isOnGround = false;
        --rngLeastOfJumpTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool _isGround = other.gameObject.tag == GameManager.TagReference.GROUND;
        if (_isGround)
        {
            isOnGround = true;
            rngLeastOfJumpTime.Value = maxJumpTime;
        }
    }

    void ControlMovementBooleans()
    {
        isRunToRight = playerInput.currentActionMap["RunToRight"].IsPressed();
        isRunToLeft = playerInput.currentActionMap["RunToLeft"].IsPressed();
        isJump = playerInput.currentActionMap["Jump"].IsPressed();

        bool _isRunToRightAndLeft =
            isRunToRight && isRunToLeft;
        if (_isRunToRightAndLeft)
        {
            isRunToRight = false;
            isRunToLeft = false;
        }

        isRun = isRunToRight || isRunToLeft;
    }


    void ControlMovingSpeed()
    {
        bool _isNotRun = !isRun;
        if (_isNotRun)
        {
            movingSpeed = 0;
            return;
        }

        float _instanceAcceleration = acceleration * Time.deltaTime;
        if (isRunToRight)
            movingSpeed += _instanceAcceleration;
        else if (isRunToLeft)
            movingSpeed -= _instanceAcceleration;

        movingSpeed = Mathf.Clamp(movingSpeed, -maxSpeed, maxSpeed);
    }

    void Run()
    {
        Vector2 _position = targetPlayer.transform.position;
        _position.x += movingSpeed * Time.deltaTime;
        targetPlayer.transform.position = _position;
    }

    
}

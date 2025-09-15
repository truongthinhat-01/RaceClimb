using UnityEngine;

public class DriveCar : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _frontTireRB;
    [SerializeField] private Rigidbody2D _backTireRB;
    [SerializeField] private Rigidbody2D _carRb;
    [SerializeField] private float _torquePower = 250f;
    [SerializeField] private float _rotationPower = 400f;
    [SerializeField] private float _forwardForce = 10f;

    private float _moveInput;
    private int direction;

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        ApplyTorqueInput();
        ApplyInput();
    }

    void GetInput()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        
    }

    void ApplyTorqueInput()
    {
        float torque = -_moveInput * _torquePower * Time.fixedDeltaTime;
        _frontTireRB.AddTorque(torque, ForceMode2D.Force);
        _backTireRB.AddTorque(torque, ForceMode2D.Force);

        float rotation = _moveInput * _rotationPower * Time.fixedDeltaTime;
        _carRb.AddTorque(rotation, ForceMode2D.Force);

        // Thêm lực đẩy về phía trước để hỗ trợ leo dốc
        Vector2 forward = transform.right * _moveInput * _forwardForce;
        _carRb.AddForce(forward, ForceMode2D.Force);
    }

    void ApplyInput()
    {
        float torque = direction * _torquePower * Time.deltaTime;
        _frontTireRB.AddTorque(torque, ForceMode2D.Force);
        _backTireRB.AddTorque(torque, ForceMode2D.Force);

        float rotation = direction * _rotationPower * Time.deltaTime;
        _carRb.AddTorque(rotation, ForceMode2D.Force);

        Vector2 forward = transform.right * direction * _forwardForce;
        _carRb.AddForce(forward, ForceMode2D.Force);
    }

    public void CarInput(int dir)
    {
        SoundManager.PlaySound("Vehicle_Car_Engine");
        direction = dir;
    }
}

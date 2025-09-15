using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [SerializeField] private float speed = 2f;         // tốc độ di chuyển sang trái
    [SerializeField] private float resetX = -10f;      // vị trí X khi reset về ban đầu
    [SerializeField] private float startX = 10f;       // vị trí X ban đầu khi xuất hiện lại

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);  // di chuyển sang trái

        if (transform.position.x < resetX)                            // nếu ra khỏi màn hình bên trái
        {
            Vector3 pos = transform.position;                         // lấy vị trí hiện tại
            pos.x = startX;                                           // đặt lại vị trí X ban đầu
            transform.position = pos;                                 // cập nhật lại vị trí
        }
    }
}

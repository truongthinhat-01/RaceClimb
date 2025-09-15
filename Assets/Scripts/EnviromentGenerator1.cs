using UnityEngine;
using UnityEngine.U2D;

[ExecuteAlways] // Để OnValidate chạy cả khi chưa bấm Play
public class EnviromentGenerator1 : MonoBehaviour
{
    [SerializeField] private SpriteShapeController spriteShapeController;
    [SerializeField, Range(3, 300)] private int levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float xMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] private float yMultiplier = 2f;
    [SerializeField, Range(0f, 1f)] private float curveSmoothness = 0.5f;
    [SerializeField] private float noiseStep = 0.5f;
    [SerializeField] private float bottom = 10f;

    private Vector3 lastPos;

    private void OnValidate()
    {
        // Kiểm tra SpriteShapeController đã gán chưa
        if (spriteShapeController == null || spriteShapeController.spline == null)
        {
            Debug.LogWarning("Chưa gán SpriteShapeController cho " + gameObject.name);
            return;
        }

        // Xóa các điểm cũ
        spriteShapeController.spline.Clear();

        // Thêm các điểm mới vào spline
        for (int i = 0; i < levelLength; i++)
        {
            float x = i * xMultiplier;
            float y = Mathf.PerlinNoise(0, i * noiseStep) * yMultiplier;
            lastPos = transform.position + new Vector3(x, y, 0f);
            spriteShapeController.spline.InsertPointAt(i, lastPos);

            if (i != 0 && i != levelLength - 1)
            {
                spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                spriteShapeController.spline.SetLeftTangent(i, Vector3.left * xMultiplier * curveSmoothness);
                spriteShapeController.spline.SetRightTangent(i, Vector3.right * xMultiplier * curveSmoothness);
            }
        }

        // Thêm điểm đóng ở dưới (bottom) nếu muốn tạo nền khép kín
        if (spriteShapeController.spline.GetPointCount() >= 2)
        {
            Vector3 lastPoint = spriteShapeController.spline.GetPosition(spriteShapeController.spline.GetPointCount() - 1);
            Vector3 firstPoint = spriteShapeController.spline.GetPosition(0);

            spriteShapeController.spline.InsertPointAt(spriteShapeController.spline.GetPointCount(), new Vector3(lastPoint.x, transform.position.y - bottom, 0));
            spriteShapeController.spline.InsertPointAt(spriteShapeController.spline.GetPointCount(), new Vector3(firstPoint.x, transform.position.y - bottom, 0));
        }
    }
}

using UnityEngine;

public class DriverDeathFromHead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && GameManager.instance != null)
        {
            SoundManager.PlaySound("Bone_Crack");
            GameManager.instance.CheckHeadCollision(collision);
        }
    }
}

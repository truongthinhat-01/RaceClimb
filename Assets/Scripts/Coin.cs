//using UnityEngine;

//[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(AudioSource))]
//public class Coin : MonoBehaviour
//{
//    [SerializeField] private int coinValue = 1;
//    [SerializeField] private AudioClip coinClip;
//    [SerializeField] private float destroyDelay = 0.5f;

//    private AudioSource audioSource;
//    private SpriteRenderer spriteRenderer;
//    private Collider2D coinCollider;

//    private void Awake()
//    {
//        // Lấy các component cần thiết một lần
//        audioSource = GetComponent<AudioSource>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        coinCollider = GetComponent<Collider2D>();
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (!other.CompareTag("Player")) return;
//        if (GameManager.instance == null)
//        {
//            Debug.LogWarning("GameManager.instance is null!");
//            return;
//        }

//        // Cộng coin
//        GameManager.instance.AddCoin(coinValue);

//        // Phát âm thanh nếu có
//        if (coinClip && audioSource)
//            audioSource.PlayOneShot(coinClip);

//        // Ẩn coin ngay lập tức
//        if (spriteRenderer) spriteRenderer.enabled = false;
//        if (coinCollider) coinCollider.enabled = false;

//        // Hủy coin sau delay
//        Destroy(gameObject, destroyDelay);
//    }
//}

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] int value = 5;
    [SerializeField] UnityEvent OnPickupEvent;

    GameManager gameManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {  
            OnPickupEvent.Invoke();
            GameManager.instance.AddCoin(value);
            SoundManager.PlaySound("coin");
            Destroy(gameObject, 2f);
        }
    }
}


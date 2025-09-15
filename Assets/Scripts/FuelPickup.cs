using UnityEngine;

public class FuelPickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.CompareTag("Player"))
        {
            if (GameUIManager.Instance != null)
            { 
                GameUIManager.Instance.FuelFull();
                SoundManager.PlaySound("fuel");
                Destroy(gameObject);
            }
           

           
        }
    }
}

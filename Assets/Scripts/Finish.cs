using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.PlaySound("Win");
        winCanvas.SetActive(true);
        Time.timeScale = 0;
       
    }
}

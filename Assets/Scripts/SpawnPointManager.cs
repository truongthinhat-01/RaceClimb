using UnityEngine;
using Unity.Cinemachine;

public class SpawnPointManager : MonoBehaviour
{
    [Header("Car Database")]
    [SerializeField] private CarDatabase carDatabase;
    [SerializeField] private Transform spawnPoint;

    [Header("Camera")]
    [SerializeField] private CinemachineCamera vcam;

    public GameObject currentVehicle { get; private set; }
    public Transform player { get; private set; }
    public Rigidbody2D playerRb { get; private set; }

    //private void Awake()
    //{
    //    SpawnSelectedCar();
    //}
    private void Start()
    {
        SpawnSelectedCar();
    }


    private void SpawnSelectedCar()
    {
        int selectedCar = PlayerPrefs.GetInt("SelectedCar", 0);

        if (carDatabase && selectedCar < carDatabase.cars.Length)
        {
            CarData car = carDatabase.cars[selectedCar];
            currentVehicle = Instantiate(
                car.carPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );

            // Gán player & rigidbody
            player = currentVehicle.transform;
            playerRb = currentVehicle.GetComponent<Rigidbody2D>();

            if (GameUIManager.Instance != null)
            {
                GameUIManager.Instance.SetCar(player);
            }

            // Camera follow xe
            if (vcam != null)
            {
                vcam.Follow = player;
                vcam.LookAt = player;
            }
        }
        else
        {
            Debug.LogError("CarDatabase null hoặc index xe không hợp lệ!");
        }
    }
}

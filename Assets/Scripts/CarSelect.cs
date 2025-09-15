using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarSelect : MonoBehaviour
{
    [Header("Database")]
    public CarDatabase carDatabase;

    [Header("UI Elements")]
    public Image carPreviewImage;
    public TextMeshProUGUI carNameText;
    public Button nextButton;
    public Button backButton;

    private int selectedCarIndex = 0;

    void Start()
    {
        ShowCar(selectedCarIndex);

        nextButton.onClick.AddListener(NextCar);
        backButton.onClick.AddListener(PreviousCar);
    }

    void ShowCar(int index)
    {
        CarData car = carDatabase.cars[index];
        carPreviewImage.sprite = car.carSprite;
        carNameText.text = car.carName;


        // Lưu lại index để khi sang GameScene biết xe nào được chọn
        PlayerPrefs.SetInt("SelectedCar", index);
    }

    public void NextCar()
    {
        SoundManager.PlaySound("Button");
        selectedCarIndex++;
        if (selectedCarIndex >= carDatabase.cars.Length)
            selectedCarIndex = 0;
        ShowCar(selectedCarIndex);
    }

    public void PreviousCar()
    {
        SoundManager.PlaySound("Button");
        selectedCarIndex--;
        if (selectedCarIndex < 0)
            selectedCarIndex = carDatabase.cars.Length - 1;
        ShowCar(selectedCarIndex);
    }
}



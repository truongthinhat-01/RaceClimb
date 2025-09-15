using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static int selectedVehicle = 0;
    public static int selectedStage = 0;

    [Header("Scene Names (Build Settings)")]
    public string[] sceneNames;

    // Gọi từ nút chọn xe
    public void SetVehicle(int index)
    {
        selectedVehicle = index;
        PlayerPrefs.SetInt("SelectedVehicle", index);
    }

    // Gọi từ StageSelect.cs
    public void SetStage(int index)
    {
        selectedStage = index;
        PlayerPrefs.SetInt("SelectedStage", index);
    }

    // Nút Start
    public void StartGame()
    {
        int stageToLoad = PlayerPrefs.GetInt("SelectedStage", 0);
        int vehicleToUse = PlayerPrefs.GetInt("SelectedCar", 0);

        if (sceneNames == null || sceneNames.Length == 0)
        {
            Debug.LogError("Chưa gán sceneNames trong MenuManager!");
            return;
        }

        if (stageToLoad < 0 || stageToLoad >= sceneNames.Length)
        {
            Debug.LogError("SelectedStage index out of range! Kiểm tra mảng sceneNames trong Inspector.");
            return;
        }

        string sceneName = sceneNames[stageToLoad];
        Debug.Log("▶ Loading scene: " + sceneName + " | Vehicle: " + vehicleToUse);

        SceneManager.LoadScene(sceneName);
    }
}

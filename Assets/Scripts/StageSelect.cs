using UnityEngine;
using UnityEngine.UI;
using TMPro; // để dùng TextMeshProUGUI

public class StageSelect : MonoBehaviour
{
    [Header("UI References")]
    public Image stagePreviewImage;       // ảnh hiển thị preview stage
    public TextMeshProUGUI stageNameText; // hiển thị tên stage bằng TMP

    [Header("Stage Data")]
    public Sprite[] stageImages;  // ảnh preview của stage
    public string[] stageNames;   // tên stage

    private int currentStage = 0;

    void Start()
    {
        // lấy stage đã chọn trước đó, nếu chưa có thì mặc định 0
        currentStage = PlayerPrefs.GetInt("SelectedStage", 0);
        ShowStage(currentStage);
    }

    public void NextStage()
    {
        SoundManager.PlaySound("Button");
        currentStage++;
        if (currentStage >= stageImages.Length)
            currentStage = 0;

        ShowStage(currentStage);
    }

    public void PreviousStage()
    {
        SoundManager.PlaySound("Button");
        currentStage--;
        if (currentStage < 0)
            currentStage = stageImages.Length - 1;

        ShowStage(currentStage);
    }

    private void ShowStage(int index)
    {
        
        if (stageImages == null || stageImages.Length == 0) return;
        if (index < 0 || index >= stageImages.Length) index = 0;

        // đổi ảnh
        stagePreviewImage.sprite = stageImages[index];

        // đổi tên stage (nếu có nhập sẵn thì lấy, không thì auto)
        if (stageNames != null && stageNames.Length > index)
            stageNameText.text = stageNames[index];
        else
            stageNameText.text = "Stage " + (index + 1);

        // lưu lại stage được chọn
        PlayerPrefs.SetInt("SelectedStage", index);
    }
}

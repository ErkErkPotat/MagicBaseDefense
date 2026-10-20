using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public AEController aeController;
    public WardController wardController;
    public BaseController baseController;
    public TMP_Text aeText;
    public TMP_Text aeGenerationText;
    public TMP_Text baseHealthText;

    void Update()
    {
        aeText.text = $"Ambient Energy:           {aeController.currentAE:F0} / {aeController.maxAE:F0}";
        aeGenerationText.text = $"+{wardController.aeGenerationRate:F0} AE/s";
        baseHealthText.text = $"Base Health: {baseController.baseCurrentHealth:F0}";
    }
}
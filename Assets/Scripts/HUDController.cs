using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public AEController aeController;
    public WardController wardController;
    public BaseController baseController;
    public WaveController waveController;

    public TMP_Text aeText;
    public TMP_Text aeGenerationText;
    public TMP_Text baseHealthText;
    public TMP_Text currentWaveText;

    public TMP_Text wardLevelText;
    public TMP_Text wardGenerationText;
    public TMP_Text wardDamageText;

    void Update()
    {
        //AE related texts
        aeText.text = $"Ambient Energy:           {aeController.currentAE:F0} / {aeController.maxAE:F0}";
        if(wardController.wardstones.Count >= 3 && wardController.isActive)
        {
            aeGenerationText.text = $"+{wardController.aeGenerationRate:F0} AE/s";
        } else {
            aeGenerationText.text = "";
        }

        //Base related texts.
        baseHealthText.text = $"Base Health: {baseController.baseCurrentHealth:F0}";

        //add if that asks for which level you are on and calculates the final wave based on that
        //Wave related texts.
        currentWaveText.text = $"Current Wave: {waveController.currentWaveNumber + 1} / 25";

        //Wave related texts
        wardLevelText.text = $"Ward Level: {wardController.wardLevel}";
    }
}
using Modules.Score.Visual;
using UnityEngine;
using UnityEngine.Events;

public class WaterReservoirVisual : MonoBehaviour,IInitializable
{
    [SerializeField] private WaterReservoir reservoir;
    [SerializeField] private ScoreCounterVisualText scoreCounterVisual;
    [SerializeField] private GameObject waterVisualObject;
    [SerializeField] private float maxSizeY = 5;
    [SerializeField] private bool textIsShow = false;

    public UnityEvent<bool> OnChangeStateVisual;
    private void Start() => Initialize();
    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();
   
    
    public void Initialize()
    {
        scoreCounterVisual.SetScoreCounter(reservoir.WaterCount);
    }
    
    private void Subscribe()
    {
        reservoir.OnValueWaterUpdate += UpdateWaterBalance;
        reservoir.OnClickWaterReservoir += SwitchState;
        UpdateWaterBalance();
    }

    private void Unsubscribe()
    {
        reservoir.OnValueWaterUpdate -= UpdateWaterBalance;
        reservoir.OnClickWaterReservoir -= SwitchState;
    }

    private void UpdateWaterBalance(float value = 0)
    {
        float percent = reservoir.GetWaterCountInPercent();
        float newSize = Mathf.Lerp(0, maxSizeY, percent);
        float newPos = Mathf.Lerp(-1, 1, percent);
        
        Vector3 startScale = waterVisualObject.transform.localScale;
        Vector3 startLocalPosition = waterVisualObject.transform.localPosition;
        
        waterVisualObject.transform.localScale = new Vector3(startScale.x, newSize, startScale.z);
        waterVisualObject.transform.localPosition = new Vector3(startLocalPosition.x, -1 + newSize, startLocalPosition.z) ;
    }

    private void SwitchState()
    {
        textIsShow = !textIsShow;
        OnChangeStateVisual?.Invoke(textIsShow);
    }
}
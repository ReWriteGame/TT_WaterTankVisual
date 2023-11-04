using System;
using Modules.Score;
using UnityEngine;

[SelectionBase]
public class WaterReservoir : MonoBehaviour, IInitializable
{
    [SerializeField] private ScoreCounter waterCount;
    
    public Action<float> OnValueWaterUpdate;
    public Action OnClickWaterReservoir;

    public ScoreCounter WaterCount => waterCount;

    
    private void Start() => Initialize();
    private void OnDestroy() => Unsubscribe();
    private void OnMouseDown() => OnClickWaterReservoir?.Invoke();


    public void Initialize()
    {
        waterCount.SetData(new ScoreCounterData(0, 0, 1000));
        Subscribe();
    }

    private void Subscribe()
    {
        waterCount.OnChangeValue += OnValueWaterUpdate;
    }

    private void Unsubscribe()
    {
        waterCount.OnChangeValue -= OnValueWaterUpdate;
    }

    public void IncreaseWaterCount(float value)
    {
        waterCount.IncreaseValue(value);
    }

    public void DecreaseWaterCount(float value)
    {
        waterCount.DecreaseValue(value);
    }

    public float GetWaterCountInPercent()
    {
        return Mathf.InverseLerp(waterCount.MinValue, waterCount.MaxValue, waterCount.Value);
    }
}
using Modules.Score;
using Modules.Score.Visual;
using UnityEngine;
using UnityEngine.Events;

public class ConnectingWaterPipeController : MonoBehaviour, IInitializable
{
    [SerializeField] private ScoreCounter verticalPositionSettings = new ScoreCounter();
    [SerializeField] private ScoreCounterVisualText scoreCounterVisualText;
    [SerializeField] private float shiftClick = 0.5f;
   
    
    private void Start() => Initialize();
    private void OnDestroy() => Unsubscribe();

    public void Initialize()
    {
        scoreCounterVisualText.SetScoreCounter(verticalPositionSettings);
        UpdatePosition(verticalPositionSettings.Value);
        Subscribe();
    }

    private void Subscribe()
    {
        verticalPositionSettings.OnChangeValue += UpdatePosition;
    }

    private void Unsubscribe()
    {
        verticalPositionSettings.OnChangeValue -= UpdatePosition;
    }

    public void MoveUpPipe()
    {
        verticalPositionSettings.IncreaseValue(shiftClick);
    }

    public void MoveDownPipe()
    {
        verticalPositionSettings.DecreaseValue(shiftClick);
    }
    
    private void UpdatePosition(float value)
    {
        transform.position = new Vector3(transform.position.x, value, transform.position.z);
    }
}
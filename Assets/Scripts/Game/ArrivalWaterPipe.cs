using System.Collections;
using Modules.Score;
using Modules.Score.Visual;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class ArrivalWaterPipe : MonoBehaviour, IInitializable
{
    [SerializeField] private ScoreCounter waterVolume = new ScoreCounter();
    [SerializeField] private float shiftClick = 0.5f;
    [Min(0)] [SerializeField] private float timeMoveWaterInSeconds = 0.1f;
    [Min(0)] [SerializeField] private bool isWork = true;
    [Min(0)] [SerializeField] private ScoreCounterVisualText scoreCounterVisual ;

    private WaterReservoir waterReservoir;
    private Coroutine coroutine;

    public UnityEvent<bool> OnChangeState;

    private void Start() => Initialize();
    private void OnDestroy() => StopCoroutine(coroutine);
    private void OnTriggerEnter(Collider other) => TriggerEnter(other);
    private void OnTriggerExit(Collider other) => TriggerExit(other);
    private void OnMouseDown() => SwitchState();


    public void Initialize()
    {
        coroutine = StartCoroutine(PutWaterLogicRoutine());
        scoreCounterVisual.SetScoreCounter(waterVolume);
    }

    public void IncreaseValueWaterVolume()
    {
        waterVolume.IncreaseValue(shiftClick);
    }

    public void DecreaseValueWaterVolume()
    {
        waterVolume.DecreaseValue(shiftClick);
    }

    private void TriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out WaterReservoir newWaterReservoir))
        {
            waterReservoir = newWaterReservoir;
        }
    }

    private void TriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out WaterReservoir newWaterReservoir))
        {
            if (waterReservoir == newWaterReservoir)
                waterReservoir = null;
        }
    }

    private IEnumerator PutWaterLogicRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeMoveWaterInSeconds);

            if (waterReservoir != null && isWork)
                waterReservoir.IncreaseWaterCount(waterVolume.Value);

            yield return null;
        }
    }

    private void SwitchState()
    {
        isWork = !isWork;
        OnChangeState?.Invoke(isWork);
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class ConnectingWaterPipe : MonoBehaviour
{
    [SerializeField] private WaterReservoir reservoir1;
    [SerializeField] private WaterReservoir reservoir2;
    
    [Min(0)] [SerializeField] private float waterVolume = 1;
    [Min(0)] [SerializeField] private float timeMoveWaterInSeconds = 1;
    [Min(0)] [SerializeField] private bool isWork = true;

    private Coroutine coroutine;
    private bool collidedWater = false;

    public UnityEvent<bool> OnChangeState;

    private void Start() => Initialize();
    private void OnDestroy() => StopCoroutine(coroutine);
    private void OnTriggerStay(Collider other) => TriggerStay(other);
    private void OnMouseDown() => SwitchState();

    
    
    public void Initialize()
    {
        coroutine = StartCoroutine(PourWaterLogicRoutine());
    }
  
    private void TriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Water water))
            collidedWater = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Water water))
            collidedWater = false;
    }

    private IEnumerator PourWaterLogicRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeMoveWaterInSeconds);

            if (isWork && collidedWater)
                PourWater();

            yield return null;
        }
    }

    private void PourWater()
    {
        if(reservoir1 == null || reservoir2 == null)return;

        float percent1 = reservoir1.GetWaterCountInPercent();
        float percent2 = reservoir2.GetWaterCountInPercent();

        bool isOneMoreTho = percent1 > percent2;

        if (isOneMoreTho)
        {
            reservoir1.DecreaseWaterCount(waterVolume);
            reservoir2.IncreaseWaterCount(waterVolume);
        }
        else
        {
            reservoir2.DecreaseWaterCount(waterVolume);
            reservoir1.IncreaseWaterCount(waterVolume);
        }
    }
    
    private void SwitchState()
    {
        isWork = !isWork;
        OnChangeState?.Invoke(isWork);
    }

}

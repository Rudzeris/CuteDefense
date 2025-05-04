using Assets.Scripts.Unit;
using UnityEngine;

public enum EControllerStatus
{
    Attack, Moving
}
public class UnitController : MonoBehaviour, IControl
{
    public EControllerStatus Status { get; private set; }
    void Update()
    {
        
    }
    public void Shutdown()
    {
        Debug.Log("UnitController shutdown");
    }

    public void Startup()
    {
        Status = EControllerStatus.Moving;
    }
}

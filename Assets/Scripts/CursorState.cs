using UnityEngine;

public class CursorState : MonoBehaviour
{
    public enum PositionState
    {
        PositionGo,
        PositionQuit,
    }
    private PositionState _positionState = PositionState.PositionGo;
    public PositionState State
    {
        set { _positionState = value; }
        get { return _positionState; }
    }
}

using UnityEngine;

public class PooledObject : MonoBehaviour
{
    //åªç›égÇ¶ÇÈÇ©Ç«Ç§Ç©
    private bool _isActive = false;
    private void OnEnable()
    {
        _isActive = true;
    }
    private void OnDisable()
    {
        _isActive = false;
    }
    public bool GetIsActive()
    {
        return _isActive;
    }
}

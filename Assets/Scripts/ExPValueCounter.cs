using UnityEngine;

public class ExPValueCounter : MonoBehaviour, IExperienceReceiver
{
    [SerializeField]
    private ExPContainer _container;
    public void GainExperiencePoint(float exPValue)
    {
        _container.AddExPValue(exPValue);
    }
}

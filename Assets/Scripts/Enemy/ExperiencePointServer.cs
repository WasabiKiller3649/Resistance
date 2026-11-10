using UnityEngine;

public class ExperiencePointServer : MonoBehaviour
{
    //加算する経験値
    [SerializeField]
    private float value;
    //ここで経験値を加算
    private void AddExP(IExperienceReceiver receiver)
    {
        receiver.GainExperiencePoint(value);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collisionにIExperienceReceiverが存在したら
        if (collision.gameObject.TryGetComponent<IExperienceReceiver>(out IExperienceReceiver receiver))
        {
            AddExP(receiver);

            //ExP消失
            gameObject.SetActive(false);
        }
    }
}

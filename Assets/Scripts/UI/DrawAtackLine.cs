using UnityEngine;

public class DrawAtackLine : MonoBehaviour
{
    //線を書く人
    [SerializeField]
    private LineRenderer _lineRenderer;

    //こいつに向かって線を引く
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private float _bufferDistence;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
    }
    void DrawLine()
    {
        //線の始点
        _lineRenderer.SetPosition(0, transform.position);

        //線の終点
        //_lineRenderer.SetPosition(
        //    1, _target.transform.position + _target.transform.position * _bufferDistence);
        _lineRenderer.SetPosition(
            1, _target.transform.position);
    }
}

using UnityEngine;
using System;
using System.Collections;
public class BoneBulletMove : MonoBehaviour
{
    //âÒì]ìxêî
    [SerializeField]
    private float _rotateSpeed;

    //âÒì]Ç≥ÇπÇÈSprite
    [SerializeField]
    private GameObject _looks;

    //è¡Ç¶ÇÈÇ‹Ç≈ÇÃéûä‘
    [SerializeField]
    private float _disappearanceTime;
    private WaitForSeconds _waitForDisappearance;

    //à⁄ìÆë¨ìx
    [SerializeField]
    private float _movespeed;

    [SerializeField]
    private PhisicsController _phisicsController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _waitForDisappearance = new WaitForSeconds(_disappearanceTime);
    }
    private void OnEnable()
    {
        StartCoroutine(Destroy());
    }
    private void OnDisable()
    {
        _phisicsController.Stop();
    }
    public void MoveBullet(Vector2 vector)
    {
        _phisicsController.MoveSurface(vector * _movespeed);
    }
    // Update is called once per frame
    void Update()
    {
        _looks.transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime, Space.Self);
    }
    IEnumerator Destroy()
    {
        yield return _waitForDisappearance;
        gameObject.SetActive(false);
    }
}

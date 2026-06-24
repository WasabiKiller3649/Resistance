// ---------------------------------------------------------  
// TrackingCamera.cs  
//   
// 作成日:  2021/11/22
// 作成者:  MasterM
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class TrackingCamera : MonoBehaviour
{

    #region 変数  
    // 描画判定するためのターゲットオブジェクトを指定
    public GameObject g_player = default;
    // ターゲットとカメラの差
    Vector3 g_playerToCameraDiff = default;
    // 追従速度
    public float g_moveSpeed =5.0f;

    // スクロールロックフラグ
    public bool g_isScrollLock = true;

    // 移動する範囲の最大位置を指定する
    public float g_maxUp = 0;
    public float g_maxDown = 0;
    public float g_maxLeft = 0;
    public float g_maxRight = 0;
    #endregion

    #region プロパティ  

    #endregion

    #region メソッド 

    void Start() {
        // カメラとターゲット位置の差を取得
        g_playerToCameraDiff = g_player.transform.position - transform.position;
    }
    private void Update() {
        // Lerpを使ってゆっくり追跡するようにする
        transform.position = Vector3.Lerp(
            transform.position, 
            g_player.transform.position - g_playerToCameraDiff,
            Time.deltaTime * g_moveSpeed
            );

        if (g_isScrollLock) {
            // 移動範囲制限
            this.transform.position = new Vector3(
                Mathf.Clamp(this.transform.position.x, g_maxLeft, g_maxRight),
                Mathf.Clamp(this.transform.position.y, g_maxDown, g_maxUp),
                this.transform.position.z);
        }
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    /// <summary>  
    /// プレイヤー  
    /// </summary>  
    [SerializeField] private Player player_ = null;

    /// <summary>  
    /// ワールド行列   
    /// </summary>  
    private Matrix4x4 worldMatrix_ = Matrix4x4.identity;

    /// <summary>  
    /// ターゲットとして設定する  
    /// </summary>  
    /// <param name="enable">true:設定する / false:解除する</param>  
    public void SetTarget(bool enable)
    {
        // マテリアルの色を変更する  
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.materials[0].color = enable ? Color.red : Color.white;
    }

	/// <summary>
	/// 開始処理
	/// </summary>
	public void Start()
    {
		
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Update()
    {
		//ベクトルを求める
		var toPlayer = (player_.transform.position - transform.position).normalized;
		//前方の情報を読み取る
		var forward = transform.forward;

		//内積でcosを求める
		var dot = Vector3.Dot(forward, toPlayer);
		//内積が1の場合回転しない
		if (0.999f < dot)
		{
			return;
		}

		//cosから角度を求める
		var radian = Mathf.Acos(dot);

		//cosから回転軸を求める
		var cross = Vector3.Cross(forward, toPlayer);

		//回転軸が上向きか下向きかで反転させる
		radian *= (cross.y / Mathf.Abs(cross.y));

    }
}

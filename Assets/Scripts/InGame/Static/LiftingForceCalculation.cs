using UnityEngine;

// 揚力計算
/*
 * 参考サイト
 * 揚力計算：https://ja.wikipedia.org/wiki/%E6%8F%9A%E5%8A%9B
 * 合力計算：https://kotobank.jp/word/%E5%90%88%E5%8A%9B-63346
 * 抗力係数：http://skomo.o.oo7.jp/f28/hp28_63.htm
 */

namespace Sailing
{

	public class LiftingForceCalculation : MonoBehaviour
	{

		// 揚力計算
		// 定数
		private const float P = 1.293f;                      // 風の密度
		private const float S = 1.0f;                        // 風が当たる帆の面積
		private const float CL = 1.12f;                      // 揚力係数

		private static CourseManager courseManager;

		private void Start()
		{

			courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();

		}

		/// <summary>
		/// @brief 船の角度と風の角度に応じて風力を変化させる
		/// </summary>
		public static float LiftingCalculation(float shipSpeed)
		{
			float lifting;   // 揚力
			float windSpeed = courseManager.WindManager.GetAllWindForce();

			// 上記から求めた風向きの影響と揚力を加味して船を進める
			// 風力が船の推進力より高いとき　⇒　加速（ただし、風力を超えることはない）
			if (windSpeed > shipSpeed)
			{
				float v = windSpeed - shipSpeed;                                       // 相対速度計算
				lifting = (P * (v * v) * CL * S) / 2;                               // 揚力計算
				shipSpeed = Mathf.Sqrt(Mathf.Pow(lifting, 2) + Mathf.Pow(shipSpeed, 2));  // 合力加算
			}
			// 風力が船の推進力より低いとき　⇒　減速
			else if (windSpeed < shipSpeed)
			{
				float v = shipSpeed - windSpeed;                                     // 相対速度計算
				lifting = (P * (v * v) * CL * S) / 2;                                     // 揚力計算
				shipSpeed = Mathf.Sqrt(Mathf.Pow(lifting, 2) - Mathf.Pow(shipSpeed, 2));  // 減算加算
			}
            
			return shipSpeed;
		}

	}

}
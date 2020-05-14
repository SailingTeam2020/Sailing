using System.Collections.Generic;
using UnityEngine;

namespace Sailing
{
	public class WindManager : MonoBehaviour
	{

		public int WindNum {
			get;
			private set;
		}

		public Dictionary<int, WindObject> WindObjectList {
			get;
			private set;
		}

		private void Awake()
		{

			WindNum = 0;
			WindObjectList = new Dictionary<int, WindObject>();

			VejeInitalize();

		}

		private void Start()
		{

			WindFactory windFactory = gameObject.AddComponent<WindFactory>();
			CourseData course = GetComponent<CourseManager>().CourseData;
			int maxWindNum = course.windTransformList.Count;

			for (int i = 0, end = maxWindNum; i < end; i++)
			{
				WindNum++;
				GameObject obj = windFactory.Create();

				obj.transform.position = course.windTransformList[i].position;
				obj.transform.parent = transform;
				obj.name = "WindObject_" + WindNum.ToString();

				WindObjectList.Add(WindNum, obj.GetComponent<WindObject>());
				WindObjectList[WindNum].SetWindSpeed(course.windTransformList[i].speed);
			}

		}

		public void CreateWind(Vector3 pos, float windSpeed)
		{

			WindNum++;
			GameObject obj = new GameObject();
			obj.transform.position = pos;
			obj.transform.parent = this.transform;
			obj.name = "WindObject_" + WindNum.ToString();
			WindObjectList.Add(WindNum, obj.AddComponent<WindObject>());
			WindObjectList[WindNum].SetWindSpeed(windSpeed);

		}

		public float GetAllWindForce()
		{

			int n = WindObjectList.Count;
			float windSpeedSum = 0.0f;

			for (int i = 1; i <= n; i++)
			{
				windSpeedSum += WindObjectList[i].WindSpeed;
			}

			return windSpeedSum / n;
		}

		// 現状ひとつの風しかないので、配列の最初を取り出してそのまま相対角を求めている。
		// 本来なら、すべての風の合力計算を行ったあとに行う

		// ベジェ曲線用
		class Position
		{
			public float x;
			public float y;

			public Position(float xx, float yy)
			{
				x = xx;
				y = yy;
			}
		}

		private Position[] pos = null;                      // 0-45
		private Position[] pos2 = null;                     // 46-90
		private Position[] pos3 = null;                     // 91-180

		private void VejeInitalize()
		{

			// ベジェ曲線用の制御点の初期化
			//0～45°
			pos = new Position[4]
			{
			new Position(0.0f, 30.0f),
			new Position(40.0f, 0.0f),
			new Position(50.0f, 0.0f),
			new Position(45.0f, 80.0f),
			};
			//46～90°
			pos2 = new Position[4]
			{
			new Position(45.0f, 80.0f),
			new Position(70.0f, 100.0f),
			new Position(80.0f, 110.0f),
			new Position(90.0f, 120.0f),
			};
			//91～180°
			pos3 = new Position[4]
			{
			new Position(90.0f, 120.0f),
			new Position(100.0f, 110.0f),
			new Position(150.0f, 30.0f),
			new Position(180.0f, 30.0f),
			};

		}

		public float GetInfluence(Transform ship)
		{

			// 角度計算...船の角度と風の角度の2点の角度を割り出す
			// NOTE:本来ならすべての風の合力をしてから
			float va = GetAngle(this.transform.position, WindObjectList[1].transform.position);
			float shipAngle = ship.transform.eulerAngles.y;
			float trans_va = Mathf.Abs(shipAngle - va);
			if (trans_va > 180.0f)
			{
				trans_va = Mathf.Abs(180.0f - trans_va);
			}
			//Debug.Log(trans_va);
			float windPercent = 100.0f;
			// (0%)0 ~ 45(80%)
			if (trans_va <= 45.0f)
			{
				float b = trans_va / 45.0f;
				float a = 1.0f - b;
				windPercent = (Mathf.Pow(a, 3) * pos[0].y + 3 * Mathf.Pow(a, 2) * b * pos[1].y + 3 * a * Mathf.Pow(b, 2) * pos[2].y + Mathf.Pow(b, 3) * pos[3].y);
			}
			// (80%)46 ~ 90(120%)
			else if (45 < trans_va && trans_va <= 90)
			{
				float b = (trans_va - 45.0f) / 45.0f;
				float a = 1.0f - b;
				windPercent = (Mathf.Pow(a, 3) * pos2[0].y + 3 * Mathf.Pow(a, 2) * b * pos2[1].y + 3 * a * Mathf.Pow(b, 2) * pos2[2].y + Mathf.Pow(b, 3) * pos2[3].y);
			}
			// (120%)91 ~ 180(30%)
			else if (90 < trans_va)
			{
				float b = (trans_va - 90.0f) / 90.0f;
				float a = 1.0f - b;
				windPercent = (Mathf.Pow(a, 3) * pos3[0].y + 3 * Mathf.Pow(a, 2) * b * pos3[1].y + 3 * a * Mathf.Pow(b, 2) * pos3[2].y + Mathf.Pow(b, 3) * pos3[3].y);
			}

			return windPercent / 100;
		}

		public float GetAngle(Vector3 ship, Vector3 wind)
		{

			float dx = wind.x - ship.x;
			float dz = wind.z - ship.z;
			float rad = Mathf.Atan2(dx, dz);
			float degree = rad * Mathf.Rad2Deg;

			if (degree < 0)
			{
				degree += 360;
			}

			return degree;
		}

	}

}
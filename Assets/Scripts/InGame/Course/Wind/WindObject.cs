using UnityEngine;

namespace Sailing
{

	public class WindObject : MonoBehaviour
	{

		public float WindSpeed {
			get;
			private set;
		}

		public void SetWindSpeed(float speed)
		{

			WindSpeed = speed;

		}

	}

}
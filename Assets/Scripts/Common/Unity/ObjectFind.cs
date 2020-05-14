using UnityEngine;

namespace Common
{

	public class ObjectFind : MonoBehaviour
	{

		public static Transform ChildFind(string findname, Transform transform)
		{

			for (int i = 0; i < transform.childCount; i++)
			{
				if (transform.GetChild(i).name == findname)
				{
					return transform.GetChild(i);
				}
			}

			Debug.Log(findname + "は見つかりませんでした");

			return null;
		}

	}

}
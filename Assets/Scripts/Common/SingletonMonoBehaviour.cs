using UnityEngine;

namespace Common
{
	/// <summary>
	/// 継承したオブジェクトは1つ以上生成できなくなります。
	/// また、生成コードを書かなくてもInstanceで呼ぶことで自動生成されます。
	/// 
	/// NOTE：abstractをつけると継承しない限り使えなくなります
	/// </summary>
	/// <typeparam name="T">継承したいオブジェクト</typeparam>
	public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					// OPTIMIZE: 本当に生成されてないか調べるためすべてのオブジェクトを検索している
					instance = (T)FindObjectOfType(typeof(T));

					// なければオブジェクトを生成します。
					if (instance == null)
					{
						instance = (new GameObject()).AddComponent<T>();
						instance.name = "Singleton_" + typeof(T);
						Debug.Log("[" + instance.name + "]を生成しました");
					}
				}
				return instance;
			}
		}

		virtual protected void Awake()
		{
			CheckInstance();
		}

		// 生成されたときもinstance
		protected void CheckInstance()
		{
			if (instance != null)
			{
				Debug.LogError(typeof(T) + "は既に存在しています\n" + this.name + "のSingletonを削除しました");
				Destroy(gameObject);
				return;
			}
			else
			{
				instance = this as T;
				//DontDestroyOnLoad(gameObject);
			}
		}
	}
}
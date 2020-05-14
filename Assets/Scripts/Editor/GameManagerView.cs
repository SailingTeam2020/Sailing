using UnityEngine;
using UnityEditor;

namespace Sailing
{

	public class GameManagerView : EditorWindow
	{
		// ツールバーに生成
		[MenuItem("Custom/Static Class Window")]
		static void ShowWindow()
		{

			GetWindow<GameManagerView>();

		}

		private ShipManager shipManager;

		private void OnEnable()
		{

			if (!shipManager)
			{
				shipManager = GameObject.Find("ShipManager").GetComponent<ShipManager>();
			}

		}

		private void Update()
		{

			// 変数が更新された時に即座に表示をアップデート
			Repaint();

		}

		private Vector2 scroll;
		private bool isListFolding = false; // ShipStateListの折りたたみ用
													//private bool[] shipStateListItem_folding = new bool[8]; // ShipStateList要素の折りたたみ用	//NOTE:要素を決めておかないとリストが開けない
		private void OnGUI()
		{

			if (!EditorApplication.isPlaying) return;   // エディタプレビュー中以外は作動させない
														// 項目数が多い時に自動的にスクロールバーを出す
			scroll = EditorGUILayout.BeginScrollView(scroll);

			// ↓表示項目はここ↓

			// SingletonMonoBehaviour<GameState>
			EditorGUILayout.LabelField("SingletonMonoBehaviour<GameState>");
			GUILayoutSpece(1);
			EditorGUILayout.LabelField("船の数", shipManager.ShipObjectList.Count.ToString());
			GUILayoutSpece(1);

			// ShipStateListの折りたたみ
			if (isListFolding = EditorGUILayout.Foldout(isListFolding, "各船情報"))
			{
				if (shipManager.ShipObjectList != null)   // これをかませないとプレビューしてないときもエラーが出る
				{
					int shipObjeectListnum = shipManager.ShipObjectList.Count;
					// ShipStateListItemの折りたたみ
					for (int i = 0; i < shipObjeectListnum; i++)
					{
						if (i >= 8)
						{
							Debug.LogError("フィールドにプレイヤーが9名以上いるため、9隻目以降は SingletonVariableView に表示されていません。\nこのエラーを解消するには、shipStateListItem_folding のサイズを大きくしてください。");   //NOTE:配列の大きさが決まっているため、これ以上プレイヤーがいた場合はエラーを出します。
							break;
						}
						EditorGUI.indentLevel++;
						//if (shipStateListItem_folding[i] = EditorGUILayout.Foldout(shipStateListItem_folding[i], "ShipID_[" + ShipManager.Instance.shipObjectList[shipObjeectListnum].shipID.ToString() + "]"))
						//{
						//	EditorGUILayout.LabelField("入力受付", ShipManager.Instance.shipObjectList[shipObjeectListnum].isInput ? "可" : "不可");
						//}
						EditorGUI.indentLevel--;    // NOTE：これを入れないとマトリョーシカができる（気になるならコメントアウトしてみてね。エラーにはなりません）
					}
				}
			}

			// 項目数が多い時に自動的にスクロールバーを出す
			EditorGUILayout.EndScrollView();

		}

		private void GUILayoutSpece(byte cnt)
		{

			if (cnt < 0) return;
			for (int i = 0; i < cnt; i++)
			{
				EditorGUILayout.Space();
			}

		}
	}

}
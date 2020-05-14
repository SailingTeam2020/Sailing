using UnityEngine;
using Photon.Pun;

namespace Sailing
{

	public class NextMakerNavi : MonoBehaviour
	{

		private PhotonView photonView;
		private ShipObject shipObject;
		private CourseManager courseManager;
		private Transform nextMakerLine;
		private LineRenderer lineRenderer;

		private void Start()
		{

			if (!photonView)
			{
				photonView = PhotonView.Get(transform.root);
			}

			shipObject = GameObject.Find("ShipManager").GetComponent<ShipManager>().MainShipObject;
			courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();
			lineRenderer = GetComponent<LineRenderer>();

			//線の幅を決める
			lineRenderer.startWidth = 0.1f;
			lineRenderer.endWidth = 0.1f;
			//頂点の数を決める
			lineRenderer.positionCount = 2;

		}

		void Update()
		{

			if (!photonView.IsMine || shipObject.IsGoal) { 
				return;
			}

			if (!shipObject.PassEnterMaker && shipObject.NextMakerNumber != courseManager.MakerManager.MakerNum)
			{
				nextMakerLine = FindChild(courseManager.MakerManager.MakerObjectList[shipObject.NextMakerNumber].gameObject.transform, "EnterLine");
				nextMakerLine = FindChild(nextMakerLine, "NavPoint");
			}
			else if (shipObject.PassEnterMaker && shipObject.NextMakerNumber != courseManager.MakerManager.MakerNum)
			{
				nextMakerLine = FindChild(courseManager.MakerManager.MakerObjectList[shipObject.NextMakerNumber].gameObject.transform, "OutLine");
				nextMakerLine = FindChild(nextMakerLine, "NavPoint");
			}
			else
			{
				nextMakerLine = FindChild(courseManager.MakerManager.MakerObjectList[shipObject.NextMakerNumber].gameObject.transform, "FinishLine");
			}

			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, nextMakerLine.position);

		}

		private Transform FindChild(Transform transform, string str)
		{

			for (int i = 0; i < transform.childCount; i++)
			{
				if (transform.GetChild(i).name == str)
				{
					return transform.GetChild(i);
				}
			}

			return null;
		}

	}

}
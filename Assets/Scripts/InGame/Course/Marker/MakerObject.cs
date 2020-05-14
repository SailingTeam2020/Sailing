using UnityEngine;

namespace Sailing
{

	public class MakerObject : MonoBehaviour
    {

        public bool IsGoalMaker {
            get;
            set;
        }

        public int MakerNumber { 
            get;
            set;
        }

        private void Awake()
        {

            IsGoalMaker = false;

        }

        // Start is called before the first frame update
        void Start()
        {

			if (IsGoalMaker)
			{
				GoalMakerTagInitalize();
			}
			else
			{
				MakerTagInitalize();
			}

        }

		private void MakerTagInitalize()
		{

			Transform n = FindChild(transform, "EnterLine");
			if (n)
			{
				n.tag = "Enter";
			}

			n = FindChild(transform, "OutLine");
			if (n)
			{
				n.tag = "Out";
			}

		}

		private void GoalMakerTagInitalize()
		{

			Transform n = FindChild(transform, "FinishLine");
			if (n)
			{
				n.tag = "Finish";
			}

		}

		private static Transform FindChild(Transform transform, string str)
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
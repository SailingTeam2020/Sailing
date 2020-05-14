using System.Collections.Generic;
using UnityEngine;

namespace Sailing.Common
{
	public interface IStateMachine { }

	[SerializeField]
	public class StateMachine<TState> : IStateMachine where TState : struct
	{
		public delegate void ChangeStateEvent(TState state);
		public ChangeStateEvent changeStateEvent;

		public TState currentState { get; private set; }
		public TState prevState { get; private set; }

		public void ChangeState(TState state)
		{
			if (EqualityComparer<TState>.Default.Equals(currentState, state)) return;
			prevState = currentState;
			currentState = state;

			if (changeStateEvent != null)
			{
				changeStateEvent.Invoke(currentState);
			}
		}

		public void ChangePrevState()
		{
			TState prev = currentState;
			currentState = prevState;
			prevState = prev;

			// NOTE:C#6以上でないと　changeStateEvent?.Invoke(currentState);　という風に書けない...。
			if (changeStateEvent != null)
			{
				changeStateEvent.Invoke(currentState);
			}
		}

		public void Dispose()
		{
			Debug.Log("StateMachine Disposed !");
		}

		public StateMachine() { }
	}
}
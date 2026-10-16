using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x0200000C RID: 12
	public abstract class FiniteStateMachine<TStateEnum, TTransitionEnum, TOwner> : ISerializationCallbackReceiver where TOwner : global::UnityEngine.Object
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000026A1 File Offset: 0x000008A1
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000026A9 File Offset: 0x000008A9
		public bool Initialized { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026B2 File Offset: 0x000008B2
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000026BA File Offset: 0x000008BA
		public bool Paused { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026C3 File Offset: 0x000008C3
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000026CB File Offset: 0x000008CB
		public TOwner Owner { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000026D4 File Offset: 0x000008D4
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000026DC File Offset: 0x000008DC
		public TStateEnum CurrentState { get; private set; }

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000034 RID: 52 RVA: 0x000026E8 File Offset: 0x000008E8
		// (remove) Token: 0x06000035 RID: 53 RVA: 0x00002720 File Offset: 0x00000920
		public event FiniteStateMachine<TStateEnum, TTransitionEnum, TOwner>.StateChangeDelegate StateChangeEvent;

		// Token: 0x06000036 RID: 54 RVA: 0x00002755 File Offset: 0x00000955
		public FiniteStateMachine(TOwner owner)
		{
			Debug.LogWarning("Creating FSM withot an IEquality comparer.  If you are using a value type this will result in boxing");
			this.m_stateDictionary = new Dictionary<TStateEnum, State<TStateEnum, TTransitionEnum, TOwner>>();
			this.m_states = new ScriptableObject[0];
			this.Owner = owner;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002785 File Offset: 0x00000985
		public FiniteStateMachine(TOwner owner, IEqualityComparer<TStateEnum> comparer)
		{
			this.m_stateDictionary = new Dictionary<TStateEnum, State<TStateEnum, TTransitionEnum, TOwner>>(comparer);
			this.m_states = new ScriptableObject[0];
			this.Owner = owner;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027AC File Offset: 0x000009AC
		public void Initialize(TStateEnum startState, object userData = null)
		{
			this.CurrentState = startState;
			this.m_stateDictionary[this.CurrentState].OnEnterState(default(TStateEnum), this.Owner, userData);
			this.Initialized = true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027F0 File Offset: 0x000009F0
		public TState AddState<TState>() where TState : State<TStateEnum, TTransitionEnum, TOwner>
		{
			ScriptableObject[] array = new ScriptableObject[this.m_states.Length + 1];
			this.m_states.CopyTo(array, 0);
			TState tstate = ScriptableObject.CreateInstance<TState>();
			array[array.Length - 1] = tstate;
			this.m_stateDictionary.Add(tstate.GetEnum(), tstate);
			this.m_states = array;
			return tstate;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002851 File Offset: 0x00000A51
		public void ReloadStates()
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002853 File Offset: 0x00000A53
		public void FixedUpdate(TOwner owner, object userData = null)
		{
			if (this.Initialized && !this.Paused)
			{
				this.m_stateDictionary[this.CurrentState].FixedUpdateState(owner, userData);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000287D File Offset: 0x00000A7D
		public void Update(TOwner owner, object userData = null)
		{
			if (this.Initialized && !this.Paused)
			{
				this.m_stateDictionary[this.CurrentState].UpdateState(owner, userData);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028A7 File Offset: 0x00000AA7
		public void AnimatorMove(TOwner owner, object userData = null)
		{
			if (this.Initialized && !this.Paused)
			{
				this.m_stateDictionary[this.CurrentState].OnOwnerAnimatorMove(owner, userData);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028D1 File Offset: 0x00000AD1
		public void AnimatorIK(TOwner owner, object userData = null)
		{
			if (this.Initialized && !this.Paused)
			{
				this.m_stateDictionary[this.CurrentState].OnOwnerAnimatorIK(owner, userData);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028FB File Offset: 0x00000AFB
		public void LateUpdate(TOwner owner, object userData = null)
		{
			if (this.Initialized && !this.Paused)
			{
				this.m_stateDictionary[this.CurrentState].LateUpdateState(owner, userData);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002928 File Offset: 0x00000B28
		public bool Transition(TTransitionEnum transition, object userData = null)
		{
			if (this.m_stateDictionary[this.CurrentState].CanTransition(transition))
			{
				this.m_stateDictionary[this.CurrentState].OnExitState(this.Owner, userData);
				TStateEnum currentState = this.CurrentState;
				this.CurrentState = this.m_stateDictionary[this.CurrentState].HandleTransition(transition);
				this.m_stateDictionary[this.CurrentState].OnEnterState(currentState, this.Owner, userData);
				if (this.StateChangeEvent != null)
				{
					this.StateChangeEvent(this.CurrentState, transition);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029CA File Offset: 0x00000BCA
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029CC File Offset: 0x00000BCC
		public void OnAfterDeserialize()
		{
			this.ReloadStates();
		}

		// Token: 0x04000017 RID: 23
		[SerializeField]
		private ScriptableObject[] m_states;

		// Token: 0x04000018 RID: 24
		private Dictionary<TStateEnum, State<TStateEnum, TTransitionEnum, TOwner>> m_stateDictionary;

		// Token: 0x02000021 RID: 33
		// (Invoke) Token: 0x060000A5 RID: 165
		public delegate void StateChangeDelegate(TStateEnum enterState, TTransitionEnum transition);
	}
}

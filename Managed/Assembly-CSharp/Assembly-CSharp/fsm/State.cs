using System;
using System.Collections.Generic;
using UnityEngine;

namespace fsm
{
	// Token: 0x02000350 RID: 848
	public class State
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x00099576 File Offset: 0x00097776
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x000994DC File Offset: 0x000976DC
		public State parent
		{
			get
			{
				return this.parent_;
			}
			set
			{
				if (this.parent_ != value)
				{
					State state = this.parent_;
					while (this.parent_ != null)
					{
						if (this.parent_ == this)
						{
							Debug.LogWarning("can't add self or child as parent");
							return;
						}
						this.parent_ = this.parent_.parent;
					}
					if (state != null)
					{
						if (state.initState == this)
						{
							state.initState = null;
						}
						state.children.Remove(this);
					}
					if (value != null)
					{
						value.children.Add(this);
						if (value.children.Count == 1)
						{
							value.initState = this;
						}
					}
					this.parent_ = value;
				}
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x00099580 File Offset: 0x00097780
		public Machine machine
		{
			get
			{
				if (this.machine_ != null)
				{
					return this.machine_;
				}
				State state = this;
				for (State state2 = this.parent; state2 != null; state2 = state2.parent)
				{
					state = state2;
				}
				this.machine_ = state as Machine;
				return this.machine_;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x000995C5 File Offset: 0x000977C5
		// (set) Token: 0x06001F8D RID: 8077 RVA: 0x000995CD File Offset: 0x000977CD
		public State initState
		{
			get
			{
				return this.initState_;
			}
			set
			{
				if (this.initState_ != value)
				{
					if (value != null && this.children.IndexOf(value) == -1)
					{
						Debug.LogError("FSM error: You must use child state as initial state.");
						this.initState_ = null;
						return;
					}
					this.initState_ = value;
				}
			}
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x00099604 File Offset: 0x00097804
		public State(string _name, State _parent = null)
		{
			this.name = _name;
			this.parent = _parent;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00099654 File Offset: 0x00097854
		public void ClearCurrentStatesRecursively()
		{
			this.currentStates.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].ClearCurrentStatesRecursively();
			}
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00099694 File Offset: 0x00097894
		public T Add<T>(State _targetState, Func<bool> _onCheck = null, Action _onStart = null, Func<bool> _onTransition = null, Action _onEnd = null) where T : Transition, new()
		{
			T t = new T();
			t.source = this;
			t.target = _targetState;
			if (_onCheck != null)
			{
				t.onCheck = _onCheck;
			}
			if (_onStart != null)
			{
				t.onStart = _onStart;
			}
			if (_onTransition != null)
			{
				t.onTransition = _onTransition;
			}
			if (_onEnd != null)
			{
				t.onEnd = _onEnd;
			}
			this.transitionList.Add(t);
			return t;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00099714 File Offset: 0x00097914
		public void OnAction()
		{
			if (this.onAction != null)
			{
				this.onAction(this);
			}
			for (int i = 0; i < this.currentStates.Count; i++)
			{
				this.currentStates[i].OnAction();
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0009975C File Offset: 0x0009795C
		public void CheckConditions()
		{
			if (this.inTransition)
			{
				return;
			}
			for (int i = 0; i < this.currentStates.Count; i++)
			{
				State state = this.currentStates[i];
				for (int j = 0; j < state.transitionList.Count; j++)
				{
					Transition transition = state.transitionList[j];
					if (transition.onCheck())
					{
						transition.source.parent.ExitStates(transition.target, transition.source);
						if (transition.onStart != null)
						{
							transition.onStart();
						}
						if (transition.target.onFadeIn != null)
						{
							transition.target.onFadeIn(transition);
						}
						this.currentTransition = transition;
						this.inTransition = true;
						break;
					}
				}
				if (!this.inTransition)
				{
					state.CheckConditions();
				}
			}
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0009983C File Offset: 0x00097A3C
		public void UpdateTransitions()
		{
			if (this.inTransition)
			{
				if (this.currentTransition.onTransition())
				{
					if (this.currentTransition.onEnd != null)
					{
						this.currentTransition.onEnd();
					}
					State state = this.currentTransition.target;
					if (state == null)
					{
						state = this.currentTransition.source;
					}
					if (state.parent != null)
					{
						state.parent.EnterStates(state, this.currentTransition.source);
					}
					else
					{
						Debug.Log("targetState = " + state.name + ", " + this.name);
					}
					this.currentTransition = null;
					this.inTransition = false;
					return;
				}
			}
			else
			{
				for (int i = 0; i < this.currentStates.Count; i++)
				{
					this.currentStates[i].UpdateTransitions();
				}
			}
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00099918 File Offset: 0x00097B18
		public void EnterStates(State _toEnter, State _toExit)
		{
			this.currentStates.Add(_toEnter);
			if (this.machine != null && this.machine.logDebugInfo)
			{
				Debug.Log("FSM Debug: Enter State - " + _toEnter.name + " at " + Time.time.ToString());
			}
			if (_toEnter.onEnter != null)
			{
				_toEnter.onEnter(_toExit, _toEnter);
			}
			if (_toEnter.children.Count != 0)
			{
				if (_toEnter.mode == State.Mode.Exclusive)
				{
					if (_toEnter.initState != null)
					{
						_toEnter.EnterStates(_toEnter.initState, _toExit);
						return;
					}
					Debug.LogError("FSM error: can't find initial state in " + _toEnter.name);
					return;
				}
				else
				{
					for (int i = 0; i < _toEnter.children.Count; i++)
					{
						_toEnter.EnterStates(_toEnter.children[i], _toExit);
					}
				}
			}
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000999EC File Offset: 0x00097BEC
		public void ExitStates(State _toEnter, State _toExit)
		{
			_toExit.ExitAllStates(_toEnter);
			if (this.machine != null && this.machine.logDebugInfo)
			{
				Debug.Log("FSM Debug: Exit State - " + _toExit.name + " at " + Time.time.ToString());
			}
			if (_toExit.onExit != null)
			{
				_toExit.onExit(_toExit, _toEnter);
			}
			this.currentStates.Remove(_toExit);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x00099A60 File Offset: 0x00097C60
		public bool IsInChildState(State state, bool containsTransTarget = true)
		{
			if (this.inTransition && containsTransTarget && this.currentTransition.target == state)
			{
				return true;
			}
			for (int i = 0; i < this.currentStates.Count; i++)
			{
				State state2 = this.currentStates[i];
				if (state2 == state)
				{
					return true;
				}
				if (state2.IsInChildState(state, containsTransTarget))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x00099AC0 File Offset: 0x00097CC0
		protected void ExitAllStates(State _toEnter)
		{
			for (int i = 0; i < this.currentStates.Count; i++)
			{
				State state = this.currentStates[i];
				state.ExitAllStates(_toEnter);
				if (state.onExit != null)
				{
					state.onExit(state, _toEnter);
				}
				if (this.machine != null && this.machine.logDebugInfo)
				{
					Debug.Log("FSM Debug: Exit State - " + state.name + " at " + Time.time.ToString());
				}
			}
			this.currentStates.Clear();
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x00099B54 File Offset: 0x00097D54
		public int TotalStates()
		{
			int num = 1;
			for (int i = 0; i < this.children.Count; i++)
			{
				num += this.children[i].TotalStates();
			}
			return num;
		}

		// Token: 0x04001039 RID: 4153
		public string name = "";

		// Token: 0x0400103A RID: 4154
		public State.Mode mode;

		// Token: 0x0400103B RID: 4155
		protected State parent_;

		// Token: 0x0400103C RID: 4156
		protected Machine machine_;

		// Token: 0x0400103D RID: 4157
		protected State initState_;

		// Token: 0x0400103E RID: 4158
		protected List<Transition> transitionList = new List<Transition>();

		// Token: 0x0400103F RID: 4159
		protected List<State> children = new List<State>();

		// Token: 0x04001040 RID: 4160
		protected bool inTransition;

		// Token: 0x04001041 RID: 4161
		protected Transition currentTransition;

		// Token: 0x04001042 RID: 4162
		protected List<State> currentStates = new List<State>();

		// Token: 0x04001043 RID: 4163
		public Action<Transition> onFadeIn;

		// Token: 0x04001044 RID: 4164
		public Action<State, State> onEnter;

		// Token: 0x04001045 RID: 4165
		public Action<State, State> onExit;

		// Token: 0x04001046 RID: 4166
		public Action<State> onAction;

		// Token: 0x0200175D RID: 5981
		public enum Mode
		{
			// Token: 0x04005E45 RID: 24133
			Exclusive,
			// Token: 0x04005E46 RID: 24134
			Parallel
		}
	}
}

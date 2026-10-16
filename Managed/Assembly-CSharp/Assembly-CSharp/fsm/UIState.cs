using System;

namespace fsm
{
	// Token: 0x0200034D RID: 845
	[Serializable]
	public class UIState : State
	{
		// Token: 0x06001F7B RID: 8059 RVA: 0x00099230 File Offset: 0x00097430
		public UIState(exUIPanel _panel, State _parent = null)
			: base(_panel.name, _parent)
		{
			this.panel = _panel;
			this.onEnter = (Action<State, State>)Delegate.Combine(this.onEnter, new Action<State, State>(delegate(State _from, State _to)
			{
				this.panel.Enter();
			}));
			this.onExit = (Action<State, State>)Delegate.Combine(this.onExit, new Action<State, State>(delegate(State _from, State _to)
			{
				this.panel.Exit();
			}));
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00099298 File Offset: 0x00097498
		public void to(UIState _targetState, Func<bool> _onCheck, float _duration)
		{
			UITransition uitransition = new UITransition();
			uitransition.source = this;
			uitransition.target = _targetState;
			if (_onCheck != null)
			{
				uitransition.onCheck = _onCheck;
			}
			uitransition.duration = _duration;
			uitransition.from = this.panel;
			uitransition.to = _targetState.panel;
			this.transitionList.Add(uitransition);
		}

		// Token: 0x0400102A RID: 4138
		public exUIPanel panel;
	}
}

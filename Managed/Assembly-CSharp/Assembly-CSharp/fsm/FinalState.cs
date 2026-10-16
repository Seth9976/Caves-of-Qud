using System;

namespace fsm
{
	// Token: 0x02000351 RID: 849
	public class FinalState : State
	{
		// Token: 0x06001F99 RID: 8089 RVA: 0x00099B8E File Offset: 0x00097D8E
		public FinalState(string _name, State _parent = null)
			: base(_name, _parent)
		{
			this.onEnter = (Action<State, State>)Delegate.Combine(this.onEnter, new Action<State, State>(this.OnFinished));
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x00099BBA File Offset: 0x00097DBA
		private void OnFinished(State _from, State _to)
		{
		}
	}
}

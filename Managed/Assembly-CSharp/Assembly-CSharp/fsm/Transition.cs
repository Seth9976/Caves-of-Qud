using System;

namespace fsm
{
	// Token: 0x02000352 RID: 850
	public class Transition
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x00099BBC File Offset: 0x00097DBC
		public Machine machine
		{
			get
			{
				if (this.source != null)
				{
					return this.source.machine;
				}
				return null;
			}
		}

		// Token: 0x04001047 RID: 4167
		public State source;

		// Token: 0x04001048 RID: 4168
		public State target;

		// Token: 0x04001049 RID: 4169
		public Func<bool> onCheck = () => false;

		// Token: 0x0400104A RID: 4170
		public Action onStart;

		// Token: 0x0400104B RID: 4171
		public Func<bool> onTransition = () => true;

		// Token: 0x0400104C RID: 4172
		public Action onEnd;
	}
}

using System;
using Unity.IntegerTime;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200002D RID: 45
	internal class NavigationEventRepeatHelper
	{
		// Token: 0x060000FB RID: 251 RVA: 0x000054CB File Offset: 0x000036CB
		public void Reset()
		{
			this.m_ConsecutiveMoveCount = 0;
			this.m_LastDirection = NavigationEvent.Direction.None;
			this.m_PrevActionTime = DiscreteTime.Zero;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000054E8 File Offset: 0x000036E8
		public bool ShouldSendMoveEvent(DiscreteTime timestamp, NavigationEvent.Direction direction, bool axisButtonsWherePressedThisFrame)
		{
			bool flag = axisButtonsWherePressedThisFrame || direction != this.m_LastDirection || timestamp > this.m_PrevActionTime + ((this.m_ConsecutiveMoveCount == 1) ? this.m_InitialRepeatDelay : this.m_ConsecutiveRepeatDelay);
			bool flag2;
			if (flag)
			{
				this.m_ConsecutiveMoveCount = ((direction == this.m_LastDirection) ? (this.m_ConsecutiveMoveCount + 1) : 1);
				this.m_LastDirection = direction;
				this.m_PrevActionTime = timestamp;
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x040000DC RID: 220
		private int m_ConsecutiveMoveCount;

		// Token: 0x040000DD RID: 221
		private NavigationEvent.Direction m_LastDirection;

		// Token: 0x040000DE RID: 222
		private DiscreteTime m_PrevActionTime;

		// Token: 0x040000DF RID: 223
		private readonly DiscreteTime m_InitialRepeatDelay = new DiscreteTime(0.5f);

		// Token: 0x040000E0 RID: 224
		private readonly DiscreteTime m_ConsecutiveRepeatDelay = new DiscreteTime(0.1f);
	}
}

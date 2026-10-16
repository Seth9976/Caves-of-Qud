using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000317 RID: 791
	public abstract class PointerManipulator : MouseManipulator
	{
		// Token: 0x060016BE RID: 5822 RVA: 0x00057FCC File Offset: 0x000561CC
		protected bool CanStartManipulation(IPointerEvent e)
		{
			foreach (ManipulatorActivationFilter manipulatorActivationFilter in base.activators)
			{
				bool flag = manipulatorActivationFilter.Matches(e);
				if (flag)
				{
					this.m_CurrentPointerId = e.pointerId;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00058040 File Offset: 0x00056240
		protected bool CanStopManipulation(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && e.pointerId == this.m_CurrentPointerId;
		}

		// Token: 0x04000C6C RID: 3180
		private int m_CurrentPointerId;
	}
}

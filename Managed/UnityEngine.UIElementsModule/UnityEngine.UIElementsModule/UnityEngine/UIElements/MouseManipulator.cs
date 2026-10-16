using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002ED RID: 749
	public abstract class MouseManipulator : Manipulator
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x00055699 File Offset: 0x00053899
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x000556A1 File Offset: 0x000538A1
		public List<ManipulatorActivationFilter> activators { get; private set; }

		// Token: 0x06001550 RID: 5456 RVA: 0x000556AA File Offset: 0x000538AA
		protected MouseManipulator()
		{
			this.activators = new List<ManipulatorActivationFilter>();
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x000556C0 File Offset: 0x000538C0
		protected bool CanStartManipulation(IMouseEvent e)
		{
			foreach (ManipulatorActivationFilter manipulatorActivationFilter in this.activators)
			{
				bool flag = manipulatorActivationFilter.Matches(e);
				if (flag)
				{
					this.m_currentActivator = manipulatorActivationFilter;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00055730 File Offset: 0x00053930
		protected bool CanStopManipulation(IMouseEvent e)
		{
			bool flag = e == null;
			return !flag && e.button == (int)this.m_currentActivator.button;
		}

		// Token: 0x04000B8B RID: 2955
		private ManipulatorActivationFilter m_currentActivator;
	}
}

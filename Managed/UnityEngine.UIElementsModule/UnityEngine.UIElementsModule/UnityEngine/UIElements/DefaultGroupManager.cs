using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D6 RID: 726
	internal class DefaultGroupManager : IGroupManager
	{
		// Token: 0x060014CB RID: 5323 RVA: 0x00053628 File Offset: 0x00051828
		public void Init(IGroupBox groupBox)
		{
			this.m_GroupBox = groupBox;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00053634 File Offset: 0x00051834
		public IGroupBoxOption GetSelectedOption()
		{
			return this.m_SelectedOption;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0005364C File Offset: 0x0005184C
		public void OnOptionSelectionChanged(IGroupBoxOption selectedOption)
		{
			bool flag = this.m_SelectedOption == selectedOption;
			if (!flag)
			{
				this.m_SelectedOption = selectedOption;
				foreach (IGroupBoxOption groupBoxOption in this.m_GroupOptions)
				{
					groupBoxOption.SetSelected(groupBoxOption == this.m_SelectedOption);
				}
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x000536C4 File Offset: 0x000518C4
		public void RegisterOption(IGroupBoxOption option)
		{
			bool flag = !this.m_GroupOptions.Contains(option);
			if (flag)
			{
				this.m_GroupOptions.Add(option);
				this.m_GroupBox.OnOptionAdded(option);
			}
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00053701 File Offset: 0x00051901
		public void UnregisterOption(IGroupBoxOption option)
		{
			this.m_GroupOptions.Remove(option);
			this.m_GroupBox.OnOptionRemoved(option);
		}

		// Token: 0x04000B3E RID: 2878
		private List<IGroupBoxOption> m_GroupOptions = new List<IGroupBoxOption>();

		// Token: 0x04000B3F RID: 2879
		private IGroupBoxOption m_SelectedOption;

		// Token: 0x04000B40 RID: 2880
		private IGroupBox m_GroupBox;
	}
}

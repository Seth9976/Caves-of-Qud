using System;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x020003CF RID: 975
	public class ListBoxItem : ItemContainer
	{
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x000B7E5E File Offset: 0x000B605E
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x000B7E66 File Offset: 0x000B6066
		public override bool IsSelected
		{
			get
			{
				return base.IsSelected;
			}
			set
			{
				if (base.IsSelected != value)
				{
					this.m_toggle.isOn = value;
					base.IsSelected = value;
				}
			}
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000B7E84 File Offset: 0x000B6084
		protected override void AwakeOverride()
		{
			this.m_toggle = base.GetComponent<Toggle>();
			this.m_toggle.interactable = false;
			this.m_toggle.isOn = this.IsSelected;
		}

		// Token: 0x04001247 RID: 4679
		private Toggle m_toggle;
	}
}

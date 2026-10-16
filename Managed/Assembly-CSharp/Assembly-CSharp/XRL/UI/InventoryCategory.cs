using System;
using XRL.Core;

namespace XRL.UI
{
	// Token: 0x020014A0 RID: 5280
	public class InventoryCategory
	{
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x0600CF69 RID: 53097 RVA: 0x004BC618 File Offset: 0x004BA818
		// (set) Token: 0x0600CF6A RID: 53098 RVA: 0x004BC620 File Offset: 0x004BA820
		public bool Expanded
		{
			get
			{
				return this._Expanded;
			}
			set
			{
				this._Expanded = value;
				if (this.StateKey != null)
				{
					XRLCore.Core.Game.SetBooleanGameState(this.StateKey, value);
				}
			}
		}

		// Token: 0x0600CF6B RID: 53099 RVA: 0x004BC648 File Offset: 0x004BA848
		public InventoryCategory(string Name, bool Persist = false)
		{
			this.Name = Name;
			if (Persist)
			{
				this.StateKey = "ExpandState" + Name;
				this._Expanded = XRLCore.Core.Game.GetBooleanGameState(this.StateKey, true);
			}
		}

		// Token: 0x04005090 RID: 20624
		public string Name = "";

		// Token: 0x04005091 RID: 20625
		public int Weight;

		// Token: 0x04005092 RID: 20626
		public int Items;

		// Token: 0x04005093 RID: 20627
		private string StateKey;

		// Token: 0x04005094 RID: 20628
		private bool _Expanded = true;
	}
}

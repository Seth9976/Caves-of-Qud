using System;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x0200046A RID: 1130
	public class InventoryLineData : PooledFrameworkDataElement<InventoryLineData>
	{
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x000E4101 File Offset: 0x000E2301
		// (set) Token: 0x06002AFA RID: 11002 RVA: 0x000E4135 File Offset: 0x000E2335
		public string sortString
		{
			get
			{
				if (this._sortString == null)
				{
					string displayName = this.displayName;
					string text;
					if (displayName == null)
					{
						text = null;
					}
					else
					{
						string text2 = displayName.Strip();
						text = ((text2 != null) ? text2.ToLower() : null);
					}
					this._sortString = text;
				}
				return this._sortString;
			}
			set
			{
				this._sortString = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x000E413E File Offset: 0x000E233E
		// (set) Token: 0x06002AFC RID: 11004 RVA: 0x000E4166 File Offset: 0x000E2366
		public string displayName
		{
			get
			{
				if (this._displayName == null)
				{
					GameObject gameObject = this.go;
					this._displayName = ((gameObject != null) ? gameObject.DisplayName : null);
				}
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000E4170 File Offset: 0x000E2370
		public InventoryLineData set(bool category, string categoryName, bool categoryExpanded, int categoryAmount, int categoryWeight, int categoryOffset, GameObject go, HotkeySpread spread, InventoryAndEquipmentStatusScreen screen)
		{
			this.categoryWeight = categoryWeight;
			this.categoryExpanded = categoryExpanded;
			this.categoryName = categoryName;
			this.categoryOffset = categoryOffset;
			this.category = category;
			this.go = go;
			this.spread = spread;
			this.screen = screen;
			this.displayName = null;
			this._sortString = null;
			return this;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000E41CC File Offset: 0x000E23CC
		public override void free()
		{
			this._sortString = null;
			this.category = false;
			this.categoryName = null;
			this.categoryExpanded = false;
			this.categoryOffset = 0;
			this._displayName = null;
			this.screen = null;
			this.spread = null;
			this.go = null;
			base.free();
		}

		// Token: 0x0400170C RID: 5900
		public bool category;

		// Token: 0x0400170D RID: 5901
		public string categoryName;

		// Token: 0x0400170E RID: 5902
		public bool categoryExpanded;

		// Token: 0x0400170F RID: 5903
		public int categoryWeight;

		// Token: 0x04001710 RID: 5904
		public int categoryAmount;

		// Token: 0x04001711 RID: 5905
		public int categoryOffset;

		// Token: 0x04001712 RID: 5906
		private string _sortString;

		// Token: 0x04001713 RID: 5907
		private string _displayName;

		// Token: 0x04001714 RID: 5908
		public GameObject go;

		// Token: 0x04001715 RID: 5909
		public HotkeySpread spread;

		// Token: 0x04001716 RID: 5910
		public InventoryAndEquipmentStatusScreen screen;
	}
}

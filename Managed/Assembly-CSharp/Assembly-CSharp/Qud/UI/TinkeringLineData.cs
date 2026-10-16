using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Tinkering;

namespace Qud.UI
{
	// Token: 0x0200047E RID: 1150
	public class TinkeringLineData : PooledFrameworkDataElement<TinkeringLineData>
	{
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x000EAF99 File Offset: 0x000E9199
		public virtual string uiCategory
		{
			get
			{
				if (this._uiCategory == null)
				{
					this._uiCategory = this.data.UICategory;
				}
				return this._uiCategory;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000EAFBC File Offset: 0x000E91BC
		public virtual string sortString
		{
			get
			{
				if (this._sortString == null)
				{
					TinkerData tinkerData = this.data;
					string text;
					if ((text = ColorUtility.StripFormatting((tinkerData != null) ? tinkerData.DisplayName : null).ToLower()) == null)
					{
						GameObject gameObject = this.modObject;
						if (gameObject == null)
						{
							text = null;
						}
						else
						{
							string displayNameOnlyStripped = gameObject.DisplayNameOnlyStripped;
							text = ((displayNameOnlyStripped != null) ? displayNameOnlyStripped.ToLower() : null);
						}
					}
					this._sortString = text;
				}
				return this._sortString;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x000EB01B File Offset: 0x000E921B
		public string costString
		{
			get
			{
				if (this._costString == null)
				{
					BitCost cost = this.cost;
				}
				return this._costString;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000EB034 File Offset: 0x000E9234
		public BitCost cost
		{
			get
			{
				if (this._cost == null)
				{
					if (this.mode == 0)
					{
						this._cost = new BitCost();
						this._cost.Import(TinkerItem.GetBitCostFor(this.data.Blueprint));
						ModifyBitCostEvent.Process(The.Player, this._cost, this.data.Type);
						this._costString = this._cost.ToString();
					}
					else if (this.mode == 1)
					{
						this._cost = new BitCost();
						if (this.modObject == null)
						{
							string text = "no modobject on mod line ";
							TinkerData tinkerData = this.data;
							MetricsManager.LogError(text + ((tinkerData != null) ? tinkerData.DisplayName : null));
							return this._cost;
						}
						int num = Tier.Constrain(this.data.Tier);
						int num2 = Tier.Constrain(this.modObject.GetModificationSlotsUsed() - this.modObject.GetIntProperty("NoCostMods", 0) + this.modObject.GetTechTier());
						this._cost.Clear();
						this._cost.Increment(BitType.TierBits[num]);
						this._cost.Increment(BitType.TierBits[num2]);
						ModifyBitCostEvent.Process(this.modObject, this._cost, "Mod");
						this._costString = this._cost.ToString();
					}
				}
				return this._cost;
			}
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000EB19C File Offset: 0x000E939C
		public TinkeringLineData set(TinkeringStatusScreen screen, int mode, bool category, string categoryName, int categoryCount, bool categoryExpanded, int categoryOffset, TinkerData data, GameObject modObject)
		{
			this.screen = screen;
			this.mode = mode;
			this.modObject = modObject;
			this.category = category;
			this.categoryName = categoryName;
			this.categoryCount = categoryCount;
			this.categoryExpanded = categoryExpanded;
			this.categoryOffset = categoryOffset;
			this.data = data;
			this.applicableObjects.Clear();
			this.applicableKeys.Clear();
			this._uiCategory = null;
			this._cost = null;
			this._costString = null;
			this._sortString = null;
			return this;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000EB224 File Offset: 0x000E9424
		public override void free()
		{
			this.modObject = null;
			this.mode = 0;
			this.category = false;
			this.categoryName = null;
			this.categoryExpanded = true;
			this.categoryOffset = 0;
			this._cost = null;
			this._costString = null;
			this._sortString = null;
			this._uiCategory = null;
			this.data = null;
			this.applicableObjects.Clear();
			this.applicableKeys.Clear();
			base.free();
		}

		// Token: 0x04001808 RID: 6152
		public bool category;

		// Token: 0x04001809 RID: 6153
		public string categoryName;

		// Token: 0x0400180A RID: 6154
		public bool categoryExpanded;

		// Token: 0x0400180B RID: 6155
		public int categoryCount;

		// Token: 0x0400180C RID: 6156
		public int categoryOffset;

		// Token: 0x0400180D RID: 6157
		public TinkeringStatusScreen screen;

		// Token: 0x0400180E RID: 6158
		public TinkerData data;

		// Token: 0x0400180F RID: 6159
		public GameObject modObject;

		// Token: 0x04001810 RID: 6160
		public List<GameObject> applicableObjects = new List<GameObject>(8);

		// Token: 0x04001811 RID: 6161
		public List<string> applicableKeys = new List<string>(8);

		// Token: 0x04001812 RID: 6162
		public int mode;

		// Token: 0x04001813 RID: 6163
		private BitCost _cost;

		// Token: 0x04001814 RID: 6164
		private string _costString;

		// Token: 0x04001815 RID: 6165
		public string _uiCategory;

		// Token: 0x04001816 RID: 6166
		public string _sortString;
	}
}

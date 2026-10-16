using System;

namespace XRL.World
{
	// Token: 0x0200095C RID: 2396
	public class ModEntry
	{
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060062F5 RID: 25333 RVA: 0x001E6465 File Offset: 0x001E4665
		public string[] TableList
		{
			get
			{
				if (this._TableList == null)
				{
					this._TableList = (this.Tables.IsNullOrEmpty() ? Array.Empty<string>() : this.Tables.Split(',', StringSplitOptions.None));
				}
				return this._TableList;
			}
		}

		// Token: 0x04002E7A RID: 11898
		public string Part;

		// Token: 0x04002E7B RID: 11899
		public string Tables;

		// Token: 0x04002E7C RID: 11900
		public int Rarity;

		// Token: 0x04002E7D RID: 11901
		public int MinTier = 1;

		// Token: 0x04002E7E RID: 11902
		public int MaxTier = 99;

		// Token: 0x04002E7F RID: 11903
		public int NativeTier;

		// Token: 0x04002E80 RID: 11904
		public int TinkerTier = 1;

		// Token: 0x04002E81 RID: 11905
		public double Value = 1.0;

		// Token: 0x04002E82 RID: 11906
		public string TinkerDisplayName = "";

		// Token: 0x04002E83 RID: 11907
		public string Description = "";

		// Token: 0x04002E84 RID: 11908
		public string TinkerIngredient = "";

		// Token: 0x04002E85 RID: 11909
		public string TinkerCategory = "utility";

		// Token: 0x04002E86 RID: 11910
		public bool TinkerAllowed = true;

		// Token: 0x04002E87 RID: 11911
		public bool BonusAllowed = true;

		// Token: 0x04002E88 RID: 11912
		public bool CanAutoTinker = true;

		// Token: 0x04002E89 RID: 11913
		public bool NoSparkingQuest;

		// Token: 0x04002E8A RID: 11914
		[NonSerialized]
		private string[] _TableList;
	}
}

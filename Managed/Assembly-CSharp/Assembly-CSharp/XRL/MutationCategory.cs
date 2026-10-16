using System;
using System.Collections.Generic;

namespace XRL
{
	// Token: 0x0200052F RID: 1327
	[Serializable]
	public class MutationCategory
	{
		// Token: 0x060038D0 RID: 14544 RVA: 0x00116EE8 File Offset: 0x001150E8
		public MutationCategory()
		{
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x00116F64 File Offset: 0x00115164
		public MutationCategory(string _Name)
		{
			this.Name = _Name;
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x00116FE6 File Offset: 0x001151E6
		public void Add(MutationEntry Entry)
		{
			Entry.Category = this;
			this.Entries.Add(Entry);
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x00116FFB File Offset: 0x001151FB
		public string CategoryModifierName
		{
			get
			{
				if (this._CategoryModifierName == null)
				{
					this._CategoryModifierName = ((this.Name == null) ? "UnknownMutationLevelModifier" : (this.Name + "MutationLevelModifier"));
				}
				return this._CategoryModifierName;
			}
		}

		// Token: 0x04001CB2 RID: 7346
		private string _CategoryModifierName;

		// Token: 0x04001CB3 RID: 7347
		public string Name = "";

		// Token: 0x04001CB4 RID: 7348
		public string DisplayName = "";

		// Token: 0x04001CB5 RID: 7349
		public string Help = "";

		// Token: 0x04001CB6 RID: 7350
		public string Stat = "";

		// Token: 0x04001CB7 RID: 7351
		public string Property = "";

		// Token: 0x04001CB8 RID: 7352
		public string ForceProperty = "";

		// Token: 0x04001CB9 RID: 7353
		public string Foreground = "w";

		// Token: 0x04001CBA RID: 7354
		public string Detail = "W";

		// Token: 0x04001CBB RID: 7355
		[Obsolete("Read MutationEntry.ExcludeFromPool on individual entries.")]
		public bool IncludeInMutatePool;

		// Token: 0x04001CBC RID: 7356
		public List<MutationEntry> Entries = new List<MutationEntry>(128);
	}
}

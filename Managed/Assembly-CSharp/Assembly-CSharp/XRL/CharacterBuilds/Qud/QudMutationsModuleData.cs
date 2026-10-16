using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005AB RID: 1451
	public class QudMutationsModuleData : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06003DDC RID: 15836 RVA: 0x0012FC74 File Offset: 0x0012DE74
		[JsonIgnore]
		public static List<MutationCategory> categories
		{
			get
			{
				return MutationFactory.GetCategories();
			}
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x0012FC7B File Offset: 0x0012DE7B
		public static MutationEntry getMutationEntryByName(string name)
		{
			return MutationFactory.GetMutationEntryByName(name);
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x0012FC83 File Offset: 0x0012DE83
		public QudMutationsModuleData()
		{
			this.Version = QudMutationsModuleData.CURRENT_VERSION;
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x0012FCA8 File Offset: 0x0012DEA8
		[OnDeserialized]
		private void LegacySupport(StreamingContext Context)
		{
			if (this.Version < QudMutationsModuleData.CURRENT_VERSION)
			{
				foreach (QudMutationModuleDataRow qudMutationModuleDataRow in this.selections)
				{
					qudMutationModuleDataRow.Upgrade(this.Version);
				}
			}
		}

		// Token: 0x04001EB1 RID: 7857
		public static readonly Version CURRENT_VERSION = new Version(1, 2, 0, 0);

		// Token: 0x04001EB2 RID: 7858
		public int mp = -1;

		// Token: 0x04001EB3 RID: 7859
		public List<QudMutationModuleDataRow> selections = new List<QudMutationModuleDataRow>();
	}
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005AA RID: 1450
	[Serializable]
	public class QudMutationModuleDataRow
	{
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06003DD8 RID: 15832 RVA: 0x0012FB34 File Offset: 0x0012DD34
		[JsonIgnore]
		public string DisplayName
		{
			get
			{
				MutationEntry entry = this.Entry;
				BaseMutation genericMutation = Mutations.GetGenericMutation((entry != null) ? entry.Class : null, this.Variant, null);
				if (genericMutation == null)
				{
					return ((entry != null) ? entry.Name : null) ?? this.Mutation;
				}
				string text = genericMutation.GetDisplayName(true);
				if (this.Variant.IsNullOrEmpty() || text.Contains('('))
				{
					return text;
				}
				string variantName = genericMutation.GetVariantName(this.Variant);
				if (!variantName.IsNullOrEmpty() && !text.Contains(variantName))
				{
					text = text + " (" + variantName + ")";
				}
				return text;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x0012FBCD File Offset: 0x0012DDCD
		[JsonIgnore]
		public MutationEntry Entry
		{
			get
			{
				return QudMutationsModuleData.getMutationEntryByName(this.Mutation);
			}
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x0012FBDC File Offset: 0x0012DDDC
		internal void Upgrade(Version Version)
		{
			int num;
			if (!this.Variant.IsNullOrEmpty() && char.IsDigit(this.Variant[0]) && int.TryParse(this.Variant, out num))
			{
				BaseMutation mutation = this.Entry.Mutation;
				List<string> list = ((mutation != null) ? mutation.GetVariants() : null);
				if (list.IsNullOrEmpty<string>())
				{
					this.Variant = null;
					return;
				}
				if (num < 0 || num >= list.Count)
				{
					this.Variant = list.GetRandomElement<string>(new Random());
					return;
				}
				this.Variant = list[num];
			}
		}

		// Token: 0x04001EAE RID: 7854
		public string Mutation;

		// Token: 0x04001EAF RID: 7855
		public string Variant;

		// Token: 0x04001EB0 RID: 7856
		public int Count;
	}
}

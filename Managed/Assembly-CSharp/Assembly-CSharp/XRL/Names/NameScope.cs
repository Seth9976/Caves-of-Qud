using System;
using System.Collections.Generic;
using System.Text;
using XRL.World;

namespace XRL.Names
{
	// Token: 0x0200057F RID: 1407
	public class NameScope : NameElement
	{
		// Token: 0x06003C73 RID: 15475 RVA: 0x001276EC File Offset: 0x001258EC
		public bool ApplyTo(string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, string Type = null, bool HasHonorific = false, bool HasEpithet = false)
		{
			return !(Type != this.Type) && (!this.SkipIfHasHonorific || !HasHonorific) && (!this.SkipIfHasEpithet || !HasEpithet) && ((this.Special.IsNullOrEmpty() && Special.IsNullOrEmpty()) || !(Special != this.Special)) && (this.Tag.IsNullOrEmpty() || !(Tag != this.Tag)) && (this.Gender.IsNullOrEmpty() || !(Gender != this.Gender)) && (this.Mutation.IsNullOrEmpty() || (Mutations != null && Mutations.Contains(this.Mutation))) && (this.Genotype.IsNullOrEmpty() || !(Genotype != this.Genotype)) && (this.Subtype.IsNullOrEmpty() || !(Subtype != this.Subtype)) && (this.Species.IsNullOrEmpty() || !(Species != this.Species)) && (this.Culture.IsNullOrEmpty() || !(Culture != this.Culture)) && (this.Faction.IsNullOrEmpty() || !(Faction != this.Faction)) && (this.Region.IsNullOrEmpty() || !(Region != this.Region)) && this.Chance.in100();
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x00127868 File Offset: 0x00125A68
		public override string ToString()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (!this.Type.IsNullOrEmpty())
			{
				stringBuilder.Compound("Type:", ',').Append(this.Type);
			}
			if (!this.Special.IsNullOrEmpty())
			{
				stringBuilder.Compound("Special:", ',').Append(this.Special);
			}
			if (!this.Genotype.IsNullOrEmpty())
			{
				stringBuilder.Compound("Genotype:", ',').Append(this.Genotype);
			}
			if (!this.Subtype.IsNullOrEmpty())
			{
				stringBuilder.Compound("Subtype:", ',').Append(this.Subtype);
			}
			if (!this.Species.IsNullOrEmpty())
			{
				stringBuilder.Compound("Species:", ',').Append(this.Species);
			}
			if (!this.Culture.IsNullOrEmpty())
			{
				stringBuilder.Compound("Culture:", ',').Append(this.Culture);
			}
			if (!this.Faction.IsNullOrEmpty())
			{
				stringBuilder.Compound("Faction:", ',').Append(this.Faction);
			}
			if (!this.Region.IsNullOrEmpty())
			{
				stringBuilder.Compound("Region:", ',').Append(this.Region);
			}
			if (!this.Gender.IsNullOrEmpty())
			{
				stringBuilder.Compound("Gender:", ',').Append(this.Gender);
			}
			if (!this.Mutation.IsNullOrEmpty())
			{
				stringBuilder.Compound("Mutation:", ',').Append(this.Mutation);
			}
			if (!this.Tag.IsNullOrEmpty())
			{
				stringBuilder.Compound("Tag:", ',').Append(this.Tag);
			}
			stringBuilder.Compound("Priority:", '/').Append(this.Priority);
			if (this.Chance != 100)
			{
				stringBuilder.Compound("Chance:", '/').Append(this.Chance);
			}
			stringBuilder.Compound("Combine:", '/').Append(this.Combine);
			if (this.SkipIfHasHonorific)
			{
				stringBuilder.Compound("SkipIfHasHonorific", '/');
			}
			if (this.SkipIfHasEpithet)
			{
				stringBuilder.Compound("SkipIfHasEpithet", '/');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x00127AA8 File Offset: 0x00125CA8
		public string Summarize(string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, string Type = null, bool HasHonorific = false, bool HasEpithet = false)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (!Type.IsNullOrEmpty())
			{
				stringBuilder.Compound("Type:", ',').Append(Type);
			}
			if (!Special.IsNullOrEmpty())
			{
				stringBuilder.Compound("Special:", ',').Append(Special);
			}
			if (!Genotype.IsNullOrEmpty())
			{
				stringBuilder.Compound("Genotype:", ',').Append(Genotype);
			}
			if (!Subtype.IsNullOrEmpty())
			{
				stringBuilder.Compound("Subtype:", ',').Append(Subtype);
			}
			if (!Species.IsNullOrEmpty())
			{
				stringBuilder.Compound("Species:", ',').Append(Species);
			}
			if (!Culture.IsNullOrEmpty())
			{
				stringBuilder.Compound("Culture:", ',').Append(Culture);
			}
			if (!Faction.IsNullOrEmpty())
			{
				stringBuilder.Compound("Faction:", ',').Append(Faction);
			}
			if (!Region.IsNullOrEmpty())
			{
				stringBuilder.Compound("Region:", ',').Append(Region);
			}
			if (!Gender.IsNullOrEmpty())
			{
				stringBuilder.Compound("Gender:", ',').Append(Gender);
			}
			if (!Mutations.IsNullOrEmpty<string>())
			{
				stringBuilder.Compound("Mutations:", ',').Append(string.Join("+", Mutations.ToArray()));
			}
			if (!Tag.IsNullOrEmpty())
			{
				stringBuilder.Compound("Tag:", ',').Append(Tag);
			}
			if (HasHonorific)
			{
				stringBuilder.Compound("HasHonorific", '/');
			}
			if (HasEpithet)
			{
				stringBuilder.Compound("HasEpithet", '/');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001E29 RID: 7721
		public string Genotype;

		// Token: 0x04001E2A RID: 7722
		public string Subtype;

		// Token: 0x04001E2B RID: 7723
		public string Species;

		// Token: 0x04001E2C RID: 7724
		public string Culture;

		// Token: 0x04001E2D RID: 7725
		public string Faction;

		// Token: 0x04001E2E RID: 7726
		public string Region;

		// Token: 0x04001E2F RID: 7727
		public string Gender;

		// Token: 0x04001E30 RID: 7728
		public string Mutation;

		// Token: 0x04001E31 RID: 7729
		public string Tag;

		// Token: 0x04001E32 RID: 7730
		public string Special;

		// Token: 0x04001E33 RID: 7731
		public string Type;

		// Token: 0x04001E34 RID: 7732
		public int Priority;

		// Token: 0x04001E35 RID: 7733
		public int Chance = 100;

		// Token: 0x04001E36 RID: 7734
		public bool Combine;

		// Token: 0x04001E37 RID: 7735
		public bool SkipIfHasHonorific;

		// Token: 0x04001E38 RID: 7736
		public bool SkipIfHasEpithet;
	}
}

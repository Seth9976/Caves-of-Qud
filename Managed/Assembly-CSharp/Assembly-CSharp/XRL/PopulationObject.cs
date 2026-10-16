using System;
using System.Collections.Generic;
using UnityEngine;

namespace XRL
{
	// Token: 0x02000538 RID: 1336
	public class PopulationObject : PopulationItem
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06003959 RID: 14681 RVA: 0x0011AC11 File Offset: 0x00118E11
		// (set) Token: 0x0600395A RID: 14682 RVA: 0x0011AC19 File Offset: 0x00118E19
		public string Blueprint
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x0011AC22 File Offset: 0x00118E22
		public PopulationObject()
		{
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x0011AC2A File Offset: 0x00118E2A
		public PopulationObject(string blueprint, string number, uint weight, string builder)
			: this()
		{
			this.Blueprint = blueprint;
			this.Number = number;
			this.Weight = weight;
			this.Builder = builder;
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x0011AC4F File Offset: 0x00118E4F
		public override string ToString()
		{
			return this.Blueprint + "[wt=" + this.Weight.ToString() + "]";
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x0011AC71 File Offset: 0x00118E71
		public override void GetEachUniqueObject(List<string> List)
		{
			if (!List.CleanContains(this.Blueprint))
			{
				List.Add(this.Blueprint);
			}
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x0011AC8D File Offset: 0x00118E8D
		public override void GenerateStructured(PopulationStructuredResult Result, Dictionary<string, string> Vars = null)
		{
			this.Generate(Result.Objects, Vars, null);
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x0011ACA0 File Offset: 0x00118EA0
		public override void Generate(List<PopulationResult> Result, Dictionary<string, string> Vars = null, string DefaultHint = null)
		{
			if (this.Blueprint == null)
			{
				Debug.LogError("NULL blueprint");
				return;
			}
			string text = this.Blueprint;
			if (!Vars.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				PopulationManager.ReplaceVariables(ref text, Vars);
			}
			if (text.StartsWith("$CALLBLUEPRINTMETHOD"))
			{
				text = PopulationManager.resolveCallBlueprintSlug(text);
			}
			int i = 0;
			int num = base.RollChance(this.Chance);
			while (i < num)
			{
				Result.Add(new PopulationResult(text, base.RollNumber(this.Number), this.Hint ?? DefaultHint, this.Builder));
				i++;
			}
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x0011AD30 File Offset: 0x00118F30
		public override PopulationResult GenerateOne(Dictionary<string, string> Vars, string DefaultHint)
		{
			if (this.Blueprint == null)
			{
				Debug.LogError("NULL blueprint");
				return null;
			}
			string text = this.Blueprint;
			if (!Vars.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				PopulationManager.ReplaceVariables(ref text, Vars);
			}
			if (text.StartsWith("$CALLBLUEPRINTMETHOD"))
			{
				text = PopulationManager.resolveCallBlueprintSlug(text);
			}
			if (base.RollChance(this.Chance) > 0)
			{
				return new PopulationResult(text, base.RollNumber(this.Number), this.Hint ?? DefaultHint, this.Builder);
			}
			return null;
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x0011ADB0 File Offset: 0x00118FB0
		public double Probability()
		{
			return base.CalculateProbability(this.Chance);
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x0011ADBE File Offset: 0x00118FBE
		public double AverageNumber()
		{
			if (!this.Number.IsNullOrEmpty())
			{
				return this.Number.GetCachedDieRoll().Average();
			}
			return 1.0;
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x0011ADE7 File Offset: 0x00118FE7
		public override PopulationItem Find(Predicate<PopulationItem> Predicate)
		{
			if (!Predicate(this))
			{
				return null;
			}
			return this;
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x0011ADF8 File Offset: 0x00118FF8
		public override void MergeFrom(PopulationItem Item, PopulationInfo Info)
		{
			base.MergeFrom(Item, Info);
			PopulationObject populationObject = (PopulationObject)Item;
			if (populationObject.Number != null)
			{
				this.Number = populationObject.Number;
			}
			if (populationObject.Chance != null)
			{
				this.Chance = populationObject.Chance;
			}
			if (populationObject.Builder != null)
			{
				this.Builder = populationObject.Builder;
			}
		}

		// Token: 0x04001CE5 RID: 7397
		public string Number;

		// Token: 0x04001CE6 RID: 7398
		public string Chance;

		// Token: 0x04001CE7 RID: 7399
		public string Builder;
	}
}

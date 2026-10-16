using System;
using System.Collections.Generic;

namespace XRL
{
	// Token: 0x0200053B RID: 1339
	public class PopulationTable : PopulationItem
	{
		// Token: 0x06003968 RID: 14696 RVA: 0x0011AE93 File Offset: 0x00119093
		public override string ToString()
		{
			return "<Table>";
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x0011AE9C File Offset: 0x0011909C
		public override void MergeFrom(PopulationItem Item, PopulationInfo Info)
		{
			base.MergeFrom(Item, Info);
			PopulationTable populationTable = (PopulationTable)Item;
			if (populationTable.Number != null)
			{
				this.Number = populationTable.Number;
			}
			if (populationTable.Chance != null)
			{
				this.Chance = populationTable.Chance;
			}
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x0011AEE0 File Offset: 0x001190E0
		public override void GetEachUniqueObject(List<string> List)
		{
			if (PopulationManager.Populations.ContainsKey(this.Name))
			{
				PopulationManager.Populations[this.Name].GetEachUniqueObject(List);
			}
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x0011AF0A File Offset: 0x0011910A
		public override void GenerateStructured(PopulationStructuredResult Result, Dictionary<string, string> Vars = null)
		{
			this.Generate(Result.Objects, Vars, null);
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x0011AF1C File Offset: 0x0011911C
		public override void Generate(List<PopulationResult> Result, Dictionary<string, string> Vars = null, string DefaultHint = null)
		{
			PopulationInfo populationInfo;
			if (!PopulationManager.TryResolvePopulation(this.Name, Vars, out populationInfo))
			{
				return;
			}
			int i = 0;
			int num = base.RollChance(this.Chance);
			while (i < num)
			{
				int j = 0;
				int num2 = base.RollNumber(this.Number);
				while (j < num2)
				{
					populationInfo.Generate(Result, Vars, this.Hint ?? DefaultHint);
					j++;
				}
				i++;
			}
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x0011AF84 File Offset: 0x00119184
		public override PopulationResult GenerateOne(Dictionary<string, string> Vars, string DefaultHint)
		{
			PopulationInfo populationInfo;
			if (!PopulationManager.TryResolvePopulation(this.Name, Vars, out populationInfo))
			{
				return null;
			}
			int i = 0;
			int num = base.RollChance(this.Chance);
			while (i < num)
			{
				int j = 0;
				int num2 = base.RollNumber(this.Number);
				while (j < num2)
				{
					PopulationResult populationResult = populationInfo.GenerateOne(Vars, this.Hint ?? DefaultHint);
					if (populationResult != null)
					{
						return populationResult;
					}
					j++;
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x0011AFF3 File Offset: 0x001191F3
		public double Probability()
		{
			return base.CalculateProbability(this.Chance);
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x0011B001 File Offset: 0x00119201
		public double AverageNumber()
		{
			if (!this.Number.IsNullOrEmpty())
			{
				return this.Number.GetCachedDieRoll().Average();
			}
			return 1.0;
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x0011B02A File Offset: 0x0011922A
		public override PopulationItem Find(Predicate<PopulationItem> Predicate)
		{
			if (!Predicate(this))
			{
				return null;
			}
			return this;
		}

		// Token: 0x04001CEF RID: 7407
		public string Number;

		// Token: 0x04001CF0 RID: 7408
		public string Chance;
	}
}

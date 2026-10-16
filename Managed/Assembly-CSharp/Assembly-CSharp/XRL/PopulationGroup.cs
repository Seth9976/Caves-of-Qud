using System;
using System.Collections.Generic;

namespace XRL
{
	// Token: 0x02000533 RID: 1331
	public class PopulationGroup : PopulationList
	{
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060038FA RID: 14586 RVA: 0x00117DA0 File Offset: 0x00115FA0
		public PopulationInfo Info
		{
			get
			{
				PopulationGroup populationGroup;
				for (PopulationList populationList = this.Parent; populationList != null; populationList = ((populationGroup != null) ? populationGroup.Parent : null))
				{
					PopulationInfo populationInfo = populationList as PopulationInfo;
					if (populationInfo != null)
					{
						return populationInfo;
					}
					populationGroup = populationList as PopulationGroup;
				}
				return null;
			}
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x00117DDC File Offset: 0x00115FDC
		public override string ToString()
		{
			string text = "";
			if (!this.Name.IsNullOrEmpty())
			{
				text = text + this.Name + ", ";
			}
			text = text + this.Style + " of [";
			foreach (PopulationItem populationItem in this.Items)
			{
				text = text + populationItem.ToString() + ",";
			}
			text += "]";
			return text;
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x00117E80 File Offset: 0x00116080
		public override void MergeFrom(PopulationItem Item, PopulationInfo Info)
		{
			PopulationGroup populationGroup = (PopulationGroup)Item;
			if (populationGroup.Number != null)
			{
				this.Number = populationGroup.Number;
			}
			if (populationGroup.Chance != null)
			{
				this.Chance = populationGroup.Chance;
			}
			base.MergeFrom(Item, Info);
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x00117EC4 File Offset: 0x001160C4
		public override void GetEachUniqueObject(List<string> Ret)
		{
			foreach (PopulationItem populationItem in this.Items)
			{
				populationItem.GetEachUniqueObject(Ret);
			}
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x00117F18 File Offset: 0x00116118
		public override void GenerateStructured(PopulationStructuredResult Result, Dictionary<string, string> Vars = null)
		{
			PopulationStructuredResult populationStructuredResult = new PopulationStructuredResult();
			populationStructuredResult.Hint = this.Hint;
			Result.ChildGroups.Add(populationStructuredResult);
			int i = 0;
			int num = base.RollChance(this.Chance);
			while (i < num)
			{
				int j = 0;
				int num2 = base.RollNumber(this.Number);
				while (j < num2)
				{
					base.GenerateStructured(populationStructuredResult, Vars);
					j++;
				}
				i++;
			}
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x00117F80 File Offset: 0x00116180
		public override void Generate(List<PopulationResult> Result, Dictionary<string, string> Vars = null, string DefaultHint = null)
		{
			int i = 0;
			int num = base.RollChance(this.Chance);
			while (i < num)
			{
				int j = 0;
				int num2 = base.RollNumber(this.Number);
				while (j < num2)
				{
					base.Generate(Result, Vars, DefaultHint);
					j++;
				}
				i++;
			}
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x00117FC8 File Offset: 0x001161C8
		public override PopulationResult GenerateOne(Dictionary<string, string> Vars = null, string DefaultHint = null)
		{
			int i = 0;
			int num = base.RollChance(this.Chance);
			while (i < num)
			{
				int j = 0;
				int num2 = base.RollNumber(this.Number);
				while (j < num2)
				{
					PopulationResult populationResult = base.GenerateOne(Vars, DefaultHint);
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

		// Token: 0x06003901 RID: 14593 RVA: 0x00118019 File Offset: 0x00116219
		public double Probability()
		{
			return base.CalculateProbability(this.Chance);
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x00118027 File Offset: 0x00116227
		public double AverageNumber()
		{
			if (!this.Number.IsNullOrEmpty())
			{
				return this.Number.GetCachedDieRoll().Average();
			}
			return 1.0;
		}

		// Token: 0x04001CD3 RID: 7379
		public PopulationList Parent;

		// Token: 0x04001CD4 RID: 7380
		public string Number;

		// Token: 0x04001CD5 RID: 7381
		public string Chance;
	}
}

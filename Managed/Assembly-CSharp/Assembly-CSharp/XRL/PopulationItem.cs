using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL
{
	// Token: 0x02000535 RID: 1333
	public abstract class PopulationItem
	{
		// Token: 0x06003908 RID: 14600
		public abstract void Generate(List<PopulationResult> Result, Dictionary<string, string> Vars = null, string DefaultHint = null);

		// Token: 0x06003909 RID: 14601 RVA: 0x00118130 File Offset: 0x00116330
		public List<PopulationResult> Generate(Dictionary<string, string> Vars = null, string DefaultHint = null)
		{
			List<PopulationResult> list = new List<PopulationResult>();
			this.Generate(list, Vars, DefaultHint);
			return list;
		}

		// Token: 0x0600390A RID: 14602
		public abstract PopulationResult GenerateOne(Dictionary<string, string> Vars = null, string DefaultHint = null);

		// Token: 0x0600390B RID: 14603
		public abstract void GenerateStructured(PopulationStructuredResult Result, Dictionary<string, string> Vars = null);

		// Token: 0x0600390C RID: 14604
		public abstract void GetEachUniqueObject(List<string> List);

		// Token: 0x0600390D RID: 14605
		public abstract PopulationItem Find(Predicate<PopulationItem> Predicate);

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x0011814D File Offset: 0x0011634D
		public bool Merge
		{
			get
			{
				return this.Load == "Merge";
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x0011815F File Offset: 0x0011635F
		public bool Remove
		{
			get
			{
				return this.Load == "Remove";
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x00118171 File Offset: 0x00116371
		public bool Replace
		{
			get
			{
				return this.Load == "Replace";
			}
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x00118183 File Offset: 0x00116383
		public virtual void MergeFrom(PopulationItem Item, PopulationInfo Info)
		{
			if (Item.Hint != null)
			{
				this.Hint = Item.Hint;
			}
			if (Item.Weight != 1U)
			{
				this.Weight = Item.Weight;
			}
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x001181B0 File Offset: 0x001163B0
		protected double CalculateProbability(string Chance)
		{
			if (Chance.IsNullOrEmpty())
			{
				return 1.0;
			}
			double num = 1.0;
			DelimitedEnumeratorChar enumerator = Chance.DelimitedBy(',').GetEnumerator();
			while (enumerator.MoveNext())
			{
				double num2;
				if (double.TryParse(enumerator.Current, out num2))
				{
					num *= (100.0 - Math.Clamp(num2, 0.0, 100.0)) / 100.0;
				}
			}
			return 1.0 - num;
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x00118240 File Offset: 0x00116440
		protected int RollChance(string Chance)
		{
			if (Chance.IsNullOrEmpty())
			{
				return 1;
			}
			int num = 0;
			foreach (ReadOnlySpan<char> readOnlySpan in Chance.DelimitedBy(','))
			{
				double num2;
				if (!readOnlySpan.IsEmpty && double.TryParse(readOnlySpan, out num2) && Stat.Rnd.NextDouble() <= num2 / 100.0)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x001182AD File Offset: 0x001164AD
		protected int RollNumber(string Number)
		{
			if (Number.IsNullOrEmpty())
			{
				return 1;
			}
			return Number.RollCached();
		}

		// Token: 0x04001CD7 RID: 7383
		public string Name;

		// Token: 0x04001CD8 RID: 7384
		public string Hint;

		// Token: 0x04001CD9 RID: 7385
		public string Load;

		// Token: 0x04001CDA RID: 7386
		public uint Weight = 1U;
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;

namespace XRL.World
{
	// Token: 0x02000930 RID: 2352
	public class DescriptionBuilder : Dictionary<string, int>
	{
		// Token: 0x06005A52 RID: 23122 RVA: 0x001B63C1 File Offset: 0x001B45C1
		public DescriptionBuilder()
		{
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x001B63D4 File Offset: 0x001B45D4
		public DescriptionBuilder(int Cutoff)
			: this()
		{
			this.Cutoff = Cutoff;
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x001B63E3 File Offset: 0x001B45E3
		public DescriptionBuilder(int Cutoff, bool BaseOnly)
			: this(Cutoff)
		{
			this.BaseOnly = BaseOnly;
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x001B63F3 File Offset: 0x001B45F3
		public new void Add(string Desc, int Order = 0)
		{
			if (Order < this.Cutoff)
			{
				if (base.ContainsKey(Desc))
				{
					if (base[Desc] < Order)
					{
						base[Desc] = Order;
						this.LastAdded = Desc;
						return;
					}
				}
				else
				{
					base.Add(Desc, Order);
					this.LastAdded = Desc;
				}
			}
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x001B6430 File Offset: 0x001B4630
		public new void Remove(string Desc)
		{
			base.Remove(Desc);
			if (Desc == this.PrimaryBase)
			{
				this.PrimaryBase = null;
			}
		}

		// Token: 0x06005A57 RID: 23127 RVA: 0x001B6450 File Offset: 0x001B4650
		public new void Clear()
		{
			base.Clear();
			this.PrimaryBase = null;
			this.LastAdded = null;
			this.Color = null;
			this.SizeAdjective = null;
			List<string> epithets = this.Epithets;
			if (epithets != null)
			{
				epithets.Clear();
			}
			Dictionary<string, int> epithetOrder = this.EpithetOrder;
			if (epithetOrder != null)
			{
				epithetOrder.Clear();
			}
			List<string> titles = this.Titles;
			if (titles != null)
			{
				titles.Clear();
			}
			Dictionary<string, int> titleOrder = this.TitleOrder;
			if (titleOrder != null)
			{
				titleOrder.Clear();
			}
			List<string> withClauses = this.WithClauses;
			if (withClauses == null)
			{
				return;
			}
			withClauses.Clear();
		}

		// Token: 0x06005A58 RID: 23128 RVA: 0x001B64D3 File Offset: 0x001B46D3
		public void AddBase(string Base, int OrderAdjust = 0, bool Secondary = false)
		{
			this.Add(Base, 10 + OrderAdjust);
			if (this.PrimaryBase == null && !Secondary)
			{
				this.PrimaryBase = Base;
			}
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x001B64F2 File Offset: 0x001B46F2
		public void ReplacePrimaryBase(string Base, int OrderAdjust = 0)
		{
			if (this.PrimaryBase != null)
			{
				this.Remove(this.PrimaryBase);
			}
			this.Add(Base, 10 + OrderAdjust);
			this.PrimaryBase = Base;
		}

		// Token: 0x06005A5A RID: 23130 RVA: 0x001B651A File Offset: 0x001B471A
		public void AddAdjective(string Adjective, int OrderAdjust = 0)
		{
			if (!this.BaseOnly)
			{
				this.Add(Adjective, -500 + OrderAdjust);
			}
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x001B6532 File Offset: 0x001B4732
		public void ApplySizeAdjective(string Adjective, int Priority = 10, int OrderAdjust = 0)
		{
			if (this.SizeAdjective == null || Priority > this.SizeAdjectivePriority)
			{
				this.SizeAdjective = Adjective;
				this.SizeAdjectivePriority = Priority;
				this.SizeAdjectiveOrderAdjust = OrderAdjust;
			}
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x001B655A File Offset: 0x001B475A
		public void AddClause(string Clause, int OrderAdjust = 0)
		{
			if (!this.BaseOnly)
			{
				this.Add(Clause, 600 + OrderAdjust);
			}
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x001B6572 File Offset: 0x001B4772
		public void AddHonorific(string Honorific, int OrderAdjust = 0)
		{
			if (!this.BaseOnly)
			{
				this.AddAdjective(Honorific, 60);
			}
		}

		// Token: 0x06005A5E RID: 23134 RVA: 0x001B6588 File Offset: 0x001B4788
		public void AddEpithet(string Epithet, int OrderAdjust = 0)
		{
			if (!this.BaseOnly)
			{
				if (this.Epithets == null)
				{
					this.Epithets = new List<string>();
				}
				this.Epithets.Add(Epithet);
				if (OrderAdjust != 0)
				{
					if (this.EpithetOrder == null)
					{
						this.EpithetOrder = new Dictionary<string, int>();
					}
					this.EpithetOrder[Epithet] = OrderAdjust;
				}
			}
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x001B65E0 File Offset: 0x001B47E0
		public void AddTitle(string Title, int OrderAdjust = 0)
		{
			if (!this.BaseOnly)
			{
				if (this.Titles == null)
				{
					this.Titles = new List<string>();
				}
				this.Titles.Add(Title);
				if (OrderAdjust != 0)
				{
					if (this.TitleOrder == null)
					{
						this.TitleOrder = new Dictionary<string, int>();
					}
					this.TitleOrder[Title] = OrderAdjust;
				}
			}
		}

		// Token: 0x06005A60 RID: 23136 RVA: 0x001B6637 File Offset: 0x001B4837
		public void AddWithClause(string Clause)
		{
			if (!this.BaseOnly)
			{
				if (this.WithClauses == null)
				{
					this.WithClauses = new List<string>();
				}
				this.WithClauses.Add(Clause);
			}
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x001B6660 File Offset: 0x001B4860
		public void AddTag(string Tag, int OrderAdjust = 0)
		{
			if (!this.BaseOnly)
			{
				this.Add(Tag, 1100 + OrderAdjust);
			}
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x001B6678 File Offset: 0x001B4878
		public void AddMark(string Mark, int OrderAdjust = 0)
		{
			if (!this.BaseOnly)
			{
				this.Add(Mark, -800 + OrderAdjust);
			}
		}

		// Token: 0x06005A63 RID: 23139 RVA: 0x001B6690 File Offset: 0x001B4890
		public void AddColor(string Color, int Priority = 0)
		{
			if (Priority >= this.ColorPriority)
			{
				this.Color = Color;
				this.ColorPriority = Priority;
			}
		}

		// Token: 0x06005A64 RID: 23140 RVA: 0x001B66A9 File Offset: 0x001B48A9
		public void AddColor(char Color, int Priority = 0)
		{
			if (Priority >= this.ColorPriority)
			{
				this.Color = Color.ToString() ?? "";
				this.ColorPriority = Priority;
			}
		}

		// Token: 0x06005A65 RID: 23141 RVA: 0x001B66D4 File Offset: 0x001B48D4
		public void Reset()
		{
			this.Clear();
			this.Object = null;
			this.Cutoff = int.MaxValue;
			this.Color = null;
			this.ColorPriority = int.MinValue;
			this.SizeAdjectivePriority = int.MinValue;
			this.SizeAdjectiveOrderAdjust = 0;
			this.BaseOnly = false;
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x001B6724 File Offset: 0x001B4924
		private int SortEpithets(string A, string B)
		{
			if (this.EpithetOrder != null && this.EpithetOrder.Count > 0)
			{
				int num;
				this.EpithetOrder.TryGetValue(A, out num);
				int num2;
				this.EpithetOrder.TryGetValue(B, out num2);
				int num3 = num.CompareTo(num2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return ColorUtility.CompareExceptFormattingAndCase(A, B);
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x001B677C File Offset: 0x001B497C
		private int SortTitles(string A, string B)
		{
			if (this.TitleOrder != null && this.TitleOrder.Count > 0)
			{
				int num;
				this.TitleOrder.TryGetValue(A, out num);
				int num2;
				this.TitleOrder.TryGetValue(B, out num2);
				int num3 = num.CompareTo(num2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return ColorUtility.CompareExceptFormattingAndCase(A, B);
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x001B67D4 File Offset: 0x001B49D4
		public void Resolve()
		{
			if (!this.SizeAdjective.IsNullOrEmpty())
			{
				this.AddAdjective(this.SizeAdjective, this.SizeAdjectiveOrderAdjust);
				this.SizeAdjective = null;
				this.SizeAdjectivePriority = int.MinValue;
				this.SizeAdjectiveOrderAdjust = 0;
			}
			if (this.Epithets != null && this.Epithets.Count > 0)
			{
				string text;
				if (this.Epithets.Count > 1)
				{
					this.Epithets.Sort(new Comparison<string>(this.SortEpithets));
					text = string.Join(", ", this.Epithets.ToArray());
				}
				else
				{
					text = this.Epithets[0];
				}
				this.AddBase(text, 60, false);
				this.Epithets.Clear();
				Dictionary<string, int> epithetOrder = this.EpithetOrder;
				if (epithetOrder != null)
				{
					epithetOrder.Clear();
				}
			}
			if (this.Titles != null && this.Titles.Count > 0)
			{
				string text2;
				if (this.Titles.Count > 1)
				{
					this.Titles.Sort(new Comparison<string>(this.SortTitles));
					text2 = ", " + Grammar.MakeAndList(this.Titles, false);
				}
				else if ((this.Epithets != null && this.Epithets.Count > 0) || (this.Object != null && this.Object.HasProperName))
				{
					text2 = ", " + this.Titles[0];
				}
				else
				{
					text2 = "and " + this.Titles[0];
				}
				this.AddClause(text2, -60);
				this.Titles.Clear();
				Dictionary<string, int> titleOrder = this.TitleOrder;
				if (titleOrder != null)
				{
					titleOrder.Clear();
				}
			}
			if (this.WithClauses != null && this.WithClauses.Count > 0)
			{
				if (this.WithClauses.Count > 1)
				{
					ColorUtility.SortExceptFormattingAndCase(this.WithClauses);
				}
				this.AddClause("with " + Grammar.MakeAndList(this.WithClauses, true), 0);
				this.WithClauses.Clear();
			}
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x001B69D0 File Offset: 0x001B4BD0
		public int descOrderComparison(string a, string b)
		{
			int num = base[a].CompareTo(base[b]);
			if (num != 0)
			{
				return num;
			}
			return ColorUtility.CompareExceptFormattingAndCase(a, b);
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x001B6A00 File Offset: 0x001B4C00
		public override string ToString()
		{
			this.Resolve();
			int count = base.Count;
			if (count == 0)
			{
				return "";
			}
			if (count != 1)
			{
				DescriptionBuilder.Descs.Clear();
				DescriptionBuilder.Descs.AddRange(base.Keys);
				if (DescriptionBuilder.Descs.Count > 1)
				{
					DescriptionBuilder.Descs.Sort(new Comparison<string>(this.descOrderComparison));
				}
				DescriptionBuilder.SB.Clear();
				bool flag = false;
				if (!this.Color.IsNullOrEmpty())
				{
					DescriptionBuilder.SB.Append("{{").Append(this.Color).Append('|');
					flag = true;
				}
				int i = 0;
				int count2 = DescriptionBuilder.Descs.Count;
				while (i < count2)
				{
					string text = DescriptionBuilder.Descs[i];
					if (i > 0)
					{
						if (flag && base[text] > 600)
						{
							DescriptionBuilder.SB.Append("}}");
							flag = false;
						}
						if (text.Length < 1 || (text[0] != ':' && text[0] != ',' && text[0] != '-'))
						{
							DescriptionBuilder.SB.Append(' ');
						}
					}
					DescriptionBuilder.SB.Append(text);
					i++;
				}
				if (flag)
				{
					DescriptionBuilder.SB.Append("}}");
				}
				return DescriptionBuilder.SB.ToString();
			}
			if (this.Color.IsNullOrEmpty())
			{
				return this.LastAdded;
			}
			DescriptionBuilder.SB.Clear();
			DescriptionBuilder.SB.Append("{{").Append(this.Color).Append('|')
				.Append(this.LastAdded)
				.Append("}}");
			return DescriptionBuilder.SB.ToString();
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x001B6BC0 File Offset: 0x001B4DC0
		public string GetDebugInfo()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			bool flag = true;
			foreach (KeyValuePair<string, int> keyValuePair in this)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(keyValuePair.Key).Append(':').Append(keyValuePair.Value);
			}
			stringBuilder.Append(";Cutoff:").Append(this.Cutoff).Append(";PrimaryBase:")
				.Append(this.PrimaryBase)
				.Append(";LastAdded:")
				.Append(this.LastAdded)
				.Append(";Color:")
				.Append(this.Color)
				.Append(";ColorPriority:")
				.Append(this.ColorPriority)
				.Append(";SizeAdjective:")
				.Append(this.SizeAdjective)
				.Append(";SizeAdjectivePriority:")
				.Append(this.SizeAdjectivePriority)
				.Append(";SizeAdjectiveOrderAdjust:")
				.Append(this.SizeAdjectiveOrderAdjust)
				.Append(";BaseOnly:")
				.Append(this.BaseOnly);
			if (this.Epithets != null && this.Epithets.Count > 0)
			{
				stringBuilder.Append(";Epithets:");
				bool flag2 = true;
				foreach (string text in this.Epithets)
				{
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(text);
				}
			}
			if (this.Titles != null && this.Titles.Count > 0)
			{
				stringBuilder.Append(";Titles:");
				bool flag3 = true;
				foreach (string text2 in this.Titles)
				{
					if (flag3)
					{
						flag3 = false;
					}
					else
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(text2);
				}
			}
			if (this.WithClauses != null && this.WithClauses.Count > 0)
			{
				stringBuilder.Append(";WithClauses:");
				bool flag4 = true;
				foreach (string text3 in this.WithClauses)
				{
					if (flag4)
					{
						flag4 = false;
					}
					else
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(text3);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002CAA RID: 11434
		public const int ORDER_BASE = 10;

		// Token: 0x04002CAB RID: 11435
		public const int ORDER_ADJECTIVE = -500;

		// Token: 0x04002CAC RID: 11436
		public const int ORDER_CLAUSE = 600;

		// Token: 0x04002CAD RID: 11437
		public const int ORDER_TAG = 1100;

		// Token: 0x04002CAE RID: 11438
		public const int ORDER_MARK = -800;

		// Token: 0x04002CAF RID: 11439
		public const int ORDER_ADJUST_EXTREMELY_EARLY = -60;

		// Token: 0x04002CB0 RID: 11440
		public const int ORDER_ADJUST_VERY_EARLY = -40;

		// Token: 0x04002CB1 RID: 11441
		public const int ORDER_ADJUST_EARLY = -20;

		// Token: 0x04002CB2 RID: 11442
		public const int ORDER_ADJUST_SLIGHTLY_EARLY = -5;

		// Token: 0x04002CB3 RID: 11443
		public const int ORDER_ADJUST_SLIGHTLY_LATE = 5;

		// Token: 0x04002CB4 RID: 11444
		public const int ORDER_ADJUST_LATE = 20;

		// Token: 0x04002CB5 RID: 11445
		public const int ORDER_ADJUST_VERY_LATE = 40;

		// Token: 0x04002CB6 RID: 11446
		public const int ORDER_ADJUST_EXTREMELY_LATE = 60;

		// Token: 0x04002CB7 RID: 11447
		public const int SHORT_CUTOFF = 1040;

		// Token: 0x04002CB8 RID: 11448
		public const int PRIORITY_VERY_LOW = 5;

		// Token: 0x04002CB9 RID: 11449
		public const int PRIORITY_LOW = 10;

		// Token: 0x04002CBA RID: 11450
		public const int PRIORITY_MEDIUM = 20;

		// Token: 0x04002CBB RID: 11451
		public const int PRIORITY_HIGH = 30;

		// Token: 0x04002CBC RID: 11452
		public const int PRIORITY_OVERRIDE = 40;

		// Token: 0x04002CBD RID: 11453
		public const int PRIORITY_ADJUST_SMALL = 1;

		// Token: 0x04002CBE RID: 11454
		public const int PRIORITY_ADJUST_MEDIUM = 2;

		// Token: 0x04002CBF RID: 11455
		public const int PRIORITY_ADJUST_LARGE = 3;

		// Token: 0x04002CC0 RID: 11456
		public GameObject Object;

		// Token: 0x04002CC1 RID: 11457
		public int Cutoff = int.MaxValue;

		// Token: 0x04002CC2 RID: 11458
		public string PrimaryBase;

		// Token: 0x04002CC3 RID: 11459
		public string LastAdded;

		// Token: 0x04002CC4 RID: 11460
		public string Color;

		// Token: 0x04002CC5 RID: 11461
		public int ColorPriority;

		// Token: 0x04002CC6 RID: 11462
		public string SizeAdjective;

		// Token: 0x04002CC7 RID: 11463
		public int SizeAdjectivePriority;

		// Token: 0x04002CC8 RID: 11464
		public int SizeAdjectiveOrderAdjust;

		// Token: 0x04002CC9 RID: 11465
		public List<string> Epithets;

		// Token: 0x04002CCA RID: 11466
		public Dictionary<string, int> EpithetOrder;

		// Token: 0x04002CCB RID: 11467
		public List<string> Titles;

		// Token: 0x04002CCC RID: 11468
		public Dictionary<string, int> TitleOrder;

		// Token: 0x04002CCD RID: 11469
		public List<string> WithClauses;

		// Token: 0x04002CCE RID: 11470
		public bool BaseOnly;

		// Token: 0x04002CCF RID: 11471
		private static List<string> Descs = new List<string>();

		// Token: 0x04002CD0 RID: 11472
		private static StringBuilder SB = new StringBuilder();
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Units
{
	// Token: 0x020009A6 RID: 2470
	[Serializable]
	public class GameObjectAttributeUnit : GameObjectUnit
	{
		// Token: 0x06006AB3 RID: 27315 RVA: 0x00225D18 File Offset: 0x00223F18
		public override void Apply(GameObject Object)
		{
			if (this.Attribute == "*" || this.Attribute.EqualsNoCase("All"))
			{
				using (List<string>.Enumerator enumerator = Statistic.Attributes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						this.Apply(Object, text);
					}
					return;
				}
			}
			this.Apply(Object, this.Attribute);
		}

		// Token: 0x06006AB4 RID: 27316 RVA: 0x00225D9C File Offset: 0x00223F9C
		public void Apply(GameObject Object, string Attribute)
		{
			Statistic stat = Object.GetStat(Attribute);
			if (this.Percent)
			{
				stat.BaseValue += stat.BaseValue * this.Value / 100;
				return;
			}
			stat.BaseValue += this.Value;
		}

		// Token: 0x06006AB5 RID: 27317 RVA: 0x00225DEC File Offset: 0x00223FEC
		public override void Remove(GameObject Object)
		{
			if (this.Attribute == "*" || this.Attribute.EqualsNoCase("All"))
			{
				using (List<string>.Enumerator enumerator = Statistic.Attributes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						this.Remove(Object, text);
					}
					return;
				}
			}
			this.Remove(Object, this.Attribute);
		}

		// Token: 0x06006AB6 RID: 27318 RVA: 0x00225E70 File Offset: 0x00224070
		public void Remove(GameObject Object, string Attribute)
		{
			Statistic stat = Object.GetStat(Attribute);
			if (this.Percent)
			{
				stat.BaseValue = (int)((float)stat.BaseValue / (1f + (float)this.Value / 100f));
				return;
			}
			Object.GetStat(Attribute).BaseValue -= this.Value;
		}

		// Token: 0x06006AB7 RID: 27319 RVA: 0x00225EC9 File Offset: 0x002240C9
		public override void Reset()
		{
			base.Reset();
			this.Attribute = null;
			this.Value = 0;
			this.Percent = false;
		}

		// Token: 0x06006AB8 RID: 27320 RVA: 0x00225EE8 File Offset: 0x002240E8
		public override string GetDescription(bool Inscription = false)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.AppendSigned(Statistic.IsInverseBenefit(this.Attribute) ? (-this.Value) : this.Value);
			if (this.Percent)
			{
				stringBuilder.Append("%");
			}
			if (this.Attribute == "*" || this.Attribute.EqualsNoCase("All"))
			{
				stringBuilder.Compound("to all stats");
			}
			else
			{
				stringBuilder.Compound(Statistic.GetStatDisplayName(this.Attribute));
			}
			return Event.FinalizeString(stringBuilder);
		}

		// Token: 0x040030D7 RID: 12503
		public string Attribute;

		// Token: 0x040030D8 RID: 12504
		public int Value;

		// Token: 0x040030D9 RID: 12505
		public bool Percent;
	}
}

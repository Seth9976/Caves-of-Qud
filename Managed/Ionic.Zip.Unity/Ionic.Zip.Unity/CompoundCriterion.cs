using System;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001E RID: 30
	internal class CompoundCriterion : SelectionCriterion
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002AD4 File Offset: 0x00000CD4
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002ADC File Offset: 0x00000CDC
		internal SelectionCriterion Right
		{
			get
			{
				return this._Right;
			}
			set
			{
				this._Right = value;
				if (value == null)
				{
					this.Conjunction = LogicalConjunction.NONE;
					return;
				}
				if (this.Conjunction == LogicalConjunction.NONE)
				{
					this.Conjunction = LogicalConjunction.AND;
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002B00 File Offset: 0x00000D00
		internal override bool Evaluate(string filename)
		{
			bool flag = this.Left.Evaluate(filename);
			switch (this.Conjunction)
			{
			case LogicalConjunction.AND:
				if (flag)
				{
					flag = this.Right.Evaluate(filename);
				}
				break;
			case LogicalConjunction.OR:
				if (!flag)
				{
					flag = this.Right.Evaluate(filename);
				}
				break;
			case LogicalConjunction.XOR:
				flag ^= this.Right.Evaluate(filename);
				break;
			default:
				throw new ArgumentException("Conjunction");
			}
			return flag;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002B78 File Offset: 0x00000D78
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(").Append((this.Left != null) ? this.Left.ToString() : "null").Append(" ")
				.Append(this.Conjunction.ToString())
				.Append(" ")
				.Append((this.Right != null) ? this.Right.ToString() : "null")
				.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002C10 File Offset: 0x00000E10
		internal override bool Evaluate(ZipEntry entry)
		{
			bool flag = this.Left.Evaluate(entry);
			switch (this.Conjunction)
			{
			case LogicalConjunction.AND:
				if (flag)
				{
					flag = this.Right.Evaluate(entry);
				}
				break;
			case LogicalConjunction.OR:
				if (!flag)
				{
					flag = this.Right.Evaluate(entry);
				}
				break;
			case LogicalConjunction.XOR:
				flag ^= this.Right.Evaluate(entry);
				break;
			}
			return flag;
		}

		// Token: 0x0400004C RID: 76
		internal LogicalConjunction Conjunction;

		// Token: 0x0400004D RID: 77
		internal SelectionCriterion Left;

		// Token: 0x0400004E RID: 78
		private SelectionCriterion _Right;
	}
}

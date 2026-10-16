using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001A RID: 26
	internal class SizeCriterion : SelectionCriterion
	{
		// Token: 0x06000060 RID: 96 RVA: 0x0000254C File Offset: 0x0000074C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("size ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ")
				.Append(this.Size.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000025A0 File Offset: 0x000007A0
		internal override bool Evaluate(string filename)
		{
			FileInfo fileInfo = new FileInfo(filename);
			return this._Evaluate(fileInfo.Length);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000025C0 File Offset: 0x000007C0
		private bool _Evaluate(long Length)
		{
			bool flag;
			switch (this.Operator)
			{
			case ComparisonOperator.GreaterThan:
				flag = Length > this.Size;
				break;
			case ComparisonOperator.GreaterThanOrEqualTo:
				flag = Length >= this.Size;
				break;
			case ComparisonOperator.LesserThan:
				flag = Length < this.Size;
				break;
			case ComparisonOperator.LesserThanOrEqualTo:
				flag = Length <= this.Size;
				break;
			case ComparisonOperator.EqualTo:
				flag = Length == this.Size;
				break;
			case ComparisonOperator.NotEqualTo:
				flag = Length != this.Size;
				break;
			default:
				throw new ArgumentException("Operator");
			}
			return flag;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002653 File Offset: 0x00000853
		internal override bool Evaluate(ZipEntry entry)
		{
			return this._Evaluate(entry.UncompressedSize);
		}

		// Token: 0x04000041 RID: 65
		internal ComparisonOperator Operator;

		// Token: 0x04000042 RID: 66
		internal long Size;
	}
}

using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001D RID: 29
	internal class TypeCriterion : SelectionCriterion
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000029C6 File Offset: 0x00000BC6
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000029D3 File Offset: 0x00000BD3
		internal string AttributeString
		{
			get
			{
				return this.ObjectType.ToString();
			}
			set
			{
				if (value.Length != 1 || (value.get_Chars(0) != 'D' && value.get_Chars(0) != 'F'))
				{
					throw new ArgumentException("Specify a single character: either D or F");
				}
				this.ObjectType = value.get_Chars(0);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002A0C File Offset: 0x00000C0C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("type ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ")
				.Append(this.AttributeString);
			return stringBuilder.ToString();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002A5C File Offset: 0x00000C5C
		internal override bool Evaluate(string filename)
		{
			bool flag = ((this.ObjectType == 'D') ? Directory.Exists(filename) : File.Exists(filename));
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002A94 File Offset: 0x00000C94
		internal override bool Evaluate(ZipEntry entry)
		{
			bool flag = ((this.ObjectType == 'D') ? entry.IsDirectory : (!entry.IsDirectory));
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x0400004A RID: 74
		private char ObjectType;

		// Token: 0x0400004B RID: 75
		internal ComparisonOperator Operator;
	}
}

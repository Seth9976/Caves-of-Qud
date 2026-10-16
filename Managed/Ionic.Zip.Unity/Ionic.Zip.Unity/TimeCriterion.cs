using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001B RID: 27
	internal class TimeCriterion : SelectionCriterion
	{
		// Token: 0x06000065 RID: 101 RVA: 0x0000266C File Offset: 0x0000086C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Which.ToString()).Append(" ").Append(EnumUtil.GetDescription(this.Operator))
				.Append(" ")
				.Append(this.Time.ToString("yyyy-MM-dd-HH:mm:ss"));
			return stringBuilder.ToString();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000026DC File Offset: 0x000008DC
		internal override bool Evaluate(string filename)
		{
			DateTime dateTime;
			switch (this.Which)
			{
			case WhichTime.atime:
				dateTime = File.GetLastAccessTime(filename).ToUniversalTime();
				break;
			case WhichTime.mtime:
				dateTime = File.GetLastWriteTime(filename).ToUniversalTime();
				break;
			case WhichTime.ctime:
				dateTime = File.GetCreationTime(filename).ToUniversalTime();
				break;
			default:
				throw new ArgumentException("Operator");
			}
			return this._Evaluate(dateTime);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000274C File Offset: 0x0000094C
		private bool _Evaluate(DateTime x)
		{
			bool flag;
			switch (this.Operator)
			{
			case ComparisonOperator.GreaterThan:
				flag = x > this.Time;
				break;
			case ComparisonOperator.GreaterThanOrEqualTo:
				flag = x >= this.Time;
				break;
			case ComparisonOperator.LesserThan:
				flag = x < this.Time;
				break;
			case ComparisonOperator.LesserThanOrEqualTo:
				flag = x <= this.Time;
				break;
			case ComparisonOperator.EqualTo:
				flag = x == this.Time;
				break;
			case ComparisonOperator.NotEqualTo:
				flag = x != this.Time;
				break;
			default:
				throw new ArgumentException("Operator");
			}
			return flag;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000027E8 File Offset: 0x000009E8
		internal override bool Evaluate(ZipEntry entry)
		{
			DateTime dateTime;
			switch (this.Which)
			{
			case WhichTime.atime:
				dateTime = entry.AccessedTime;
				break;
			case WhichTime.mtime:
				dateTime = entry.ModifiedTime;
				break;
			case WhichTime.ctime:
				dateTime = entry.CreationTime;
				break;
			default:
				throw new ArgumentException("??time");
			}
			return this._Evaluate(dateTime);
		}

		// Token: 0x04000043 RID: 67
		internal ComparisonOperator Operator;

		// Token: 0x04000044 RID: 68
		internal WhichTime Which;

		// Token: 0x04000045 RID: 69
		internal DateTime Time;
	}
}

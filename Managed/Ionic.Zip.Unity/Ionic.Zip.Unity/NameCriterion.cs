using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001C RID: 28
	internal class NameCriterion : SelectionCriterion
	{
		// Token: 0x1700000E RID: 14
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002848 File Offset: 0x00000A48
		internal virtual string MatchingFileSpec
		{
			set
			{
				if (Directory.Exists(value))
				{
					this._MatchingFileSpec = ".\\" + value + "\\*.*";
				}
				else
				{
					this._MatchingFileSpec = value;
				}
				this._regexString = "^" + Regex.Escape(this._MatchingFileSpec).Replace("\\\\\\*\\.\\*", "\\\\([^\\.]+|.*\\.[^\\\\\\.]*)").Replace("\\.\\*", "\\.[^\\\\\\.]*")
					.Replace("\\*", ".*")
					.Replace("\\?", "[^\\\\\\.]") + "$";
				this._re = new Regex(this._regexString, 1);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000028EC File Offset: 0x00000AEC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("name ").Append(EnumUtil.GetDescription(this.Operator)).Append(" '")
				.Append(this._MatchingFileSpec)
				.Append("'");
			return stringBuilder.ToString();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002945 File Offset: 0x00000B45
		internal override bool Evaluate(string filename)
		{
			return this._Evaluate(filename);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002950 File Offset: 0x00000B50
		private bool _Evaluate(string fullpath)
		{
			string text = ((this._MatchingFileSpec.IndexOf('\\') == -1) ? Path.GetFileName(fullpath) : fullpath);
			bool flag = this._re.IsMatch(text);
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002994 File Offset: 0x00000B94
		internal override bool Evaluate(ZipEntry entry)
		{
			string text = entry.FileName.Replace("/", "\\");
			return this._Evaluate(text);
		}

		// Token: 0x04000046 RID: 70
		private Regex _re;

		// Token: 0x04000047 RID: 71
		private string _regexString;

		// Token: 0x04000048 RID: 72
		internal ComparisonOperator Operator;

		// Token: 0x04000049 RID: 73
		private string _MatchingFileSpec;
	}
}

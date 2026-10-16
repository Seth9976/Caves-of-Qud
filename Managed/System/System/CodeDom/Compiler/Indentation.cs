using System;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000352 RID: 850
	internal sealed class Indentation
	{
		// Token: 0x06001C13 RID: 7187 RVA: 0x00066871 File Offset: 0x00064A71
		internal Indentation(ExposedTabStringIndentedTextWriter writer, int indent)
		{
			this._writer = writer;
			this._indent = indent;
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x00066888 File Offset: 0x00064A88
		internal string IndentationString
		{
			get
			{
				if (this._s == null)
				{
					string tabString = this._writer.TabString;
					switch (this._indent)
					{
					case 0:
						this._s = string.Empty;
						break;
					case 1:
						this._s = tabString;
						break;
					case 2:
						this._s = tabString + tabString;
						break;
					case 3:
						this._s = tabString + tabString + tabString;
						break;
					case 4:
						this._s = tabString + tabString + tabString + tabString;
						break;
					default:
					{
						string[] array = new string[this._indent];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = tabString;
						}
						return string.Concat(array);
					}
					}
				}
				return this._s;
			}
		}

		// Token: 0x04000E6F RID: 3695
		private readonly ExposedTabStringIndentedTextWriter _writer;

		// Token: 0x04000E70 RID: 3696
		private readonly int _indent;

		// Token: 0x04000E71 RID: 3697
		private string _s;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace XRL.Help
{
	// Token: 0x02000549 RID: 1353
	public class XRLManualPage
	{
		// Token: 0x060039AB RID: 14763 RVA: 0x0011D2DE File Offset: 0x0011B4DE
		public XRLManualPage(string Data)
		{
			this.Data = Data.Replace("\r", "");
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x0011D2FC File Offset: 0x0011B4FC
		public string GetData(bool StripBrackets = true)
		{
			if (!StripBrackets)
			{
				return this.Data;
			}
			return this.Data.Replace("[[", "").Replace("]]", "");
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x0011D32C File Offset: 0x0011B52C
		public List<string> Lines
		{
			get
			{
				List<string> list;
				if ((list = this._lines) == null)
				{
					list = (this._lines = this.GetData(true).Split('\n', StringSplitOptions.None).ToList<string>());
				}
				return list;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x0011D360 File Offset: 0x0011B560
		public List<string> LinesStripped
		{
			get
			{
				return this.Lines;
			}
		}

		// Token: 0x04001D09 RID: 7433
		public string Topic;

		// Token: 0x04001D0A RID: 7434
		public string Data;

		// Token: 0x04001D0B RID: 7435
		private List<string> _lines;
	}
}

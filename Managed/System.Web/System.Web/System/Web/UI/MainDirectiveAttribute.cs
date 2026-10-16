using System;

namespace System.Web.UI
{
	// Token: 0x020001E2 RID: 482
	internal sealed class MainDirectiveAttribute<T>
	{
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00032B09 File Offset: 0x00030D09
		public string UnparsedValue
		{
			get
			{
				return this.unparsedValue;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x00032B11 File Offset: 0x00030D11
		public bool IsExpression
		{
			get
			{
				return this.isExpression;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00032B19 File Offset: 0x00030D19
		public T Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00032B21 File Offset: 0x00030D21
		public MainDirectiveAttribute(string value)
		{
			this.unparsedValue = value;
			if (value != null)
			{
				this.isExpression = BaseParser.IsExpression(value);
			}
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00032B3F File Offset: 0x00030D3F
		public MainDirectiveAttribute(T value, bool unused)
		{
			this.value = value;
		}

		// Token: 0x0400146D RID: 5229
		private string unparsedValue;

		// Token: 0x0400146E RID: 5230
		private T value;

		// Token: 0x0400146F RID: 5231
		private bool isExpression;
	}
}

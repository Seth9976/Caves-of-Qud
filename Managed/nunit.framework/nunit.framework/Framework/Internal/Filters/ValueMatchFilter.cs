using System;
using System.Text.RegularExpressions;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	public abstract class ValueMatchFilter : TestFilter
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000D93B File Offset: 0x0000BB3B
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0000D943 File Offset: 0x0000BB43
		public string ExpectedValue { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000D94C File Offset: 0x0000BB4C
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000D954 File Offset: 0x0000BB54
		public bool IsRegex { get; set; }

		// Token: 0x060005ED RID: 1517 RVA: 0x0000D95D File Offset: 0x0000BB5D
		public ValueMatchFilter(string expectedValue)
		{
			this.ExpectedValue = expectedValue;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000D96C File Offset: 0x0000BB6C
		protected bool Match(string input)
		{
			if (this.IsRegex)
			{
				return input != null && new Regex(this.ExpectedValue).IsMatch(input);
			}
			return this.ExpectedValue == input;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000D99C File Offset: 0x0000BB9C
		public override TNode AddToXml(TNode parentNode, bool recursive)
		{
			TNode tnode = parentNode.AddElement(this.ElementName, this.ExpectedValue);
			if (this.IsRegex)
			{
				tnode.AddAttribute("re", "1");
			}
			return tnode;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005F0 RID: 1520
		protected abstract string ElementName { get; }
	}
}

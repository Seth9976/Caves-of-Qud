using System;
using System.Collections;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x02000097 RID: 151
	[Serializable]
	public class PropertyFilter : ValueMatchFilter
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x0000D52E File Offset: 0x0000B72E
		public PropertyFilter(string propertyName, string expectedValue)
			: base(expectedValue)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000D540 File Offset: 0x0000B740
		public override bool Match(ITest test)
		{
			IList list = test.Properties[this._propertyName];
			if (list != null)
			{
				foreach (object obj in list)
				{
					string text = (string)obj;
					if (base.Match(text))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		public override TNode AddToXml(TNode parentNode, bool recursive)
		{
			TNode tnode = base.AddToXml(parentNode, recursive);
			tnode.AddAttribute("name", this._propertyName);
			return tnode;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0000D5D3 File Offset: 0x0000B7D3
		protected override string ElementName
		{
			get
			{
				return "prop";
			}
		}

		// Token: 0x04000123 RID: 291
		private string _propertyName;
	}
}

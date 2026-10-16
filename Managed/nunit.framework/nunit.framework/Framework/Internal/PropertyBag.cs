using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000073 RID: 115
	public class PropertyBag : IPropertyBag, IXmlNodeBuilder
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000952C File Offset: 0x0000772C
		public void Add(string key, object value)
		{
			IList list;
			if (!this.inner.TryGetValue(key, out list))
			{
				list = new List<object>();
				this.inner.Add(key, list);
			}
			list.Add(value);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00009564 File Offset: 0x00007764
		public void Set(string key, object value)
		{
			Guard.ArgumentNotNull(key, "key");
			Guard.ArgumentNotNull(value, "value");
			IList list = new List<object>();
			list.Add(value);
			this.inner[key] = list;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000095A4 File Offset: 0x000077A4
		public object Get(string key)
		{
			IList list;
			if (!this.inner.TryGetValue(key, out list) || list.Count <= 0)
			{
				return null;
			}
			return list[0];
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000095D3 File Offset: 0x000077D3
		public bool ContainsKey(string key)
		{
			return this.inner.ContainsKey(key);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x000095E1 File Offset: 0x000077E1
		public ICollection<string> Keys
		{
			get
			{
				return this.inner.Keys;
			}
		}

		// Token: 0x170000A1 RID: 161
		public IList this[string key]
		{
			get
			{
				IList list;
				if (!this.inner.TryGetValue(key, out list))
				{
					list = new List<object>();
					this.inner.Add(key, list);
				}
				return list;
			}
			set
			{
				this.inner[key] = value;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00009630 File Offset: 0x00007830
		public TNode ToXml(bool recursive)
		{
			return this.AddToXml(new TNode("dummy"), recursive);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00009644 File Offset: 0x00007844
		public TNode AddToXml(TNode parentNode, bool recursive)
		{
			TNode tnode = parentNode.AddElement("properties");
			foreach (string text in this.Keys)
			{
				foreach (object obj in this[text])
				{
					TNode tnode2 = tnode.AddElement("property");
					tnode2.AddAttribute("name", text.ToString());
					tnode2.AddAttribute("value", obj.ToString());
				}
			}
			return tnode;
		}

		// Token: 0x0400009B RID: 155
		private Dictionary<string, IList> inner = new Dictionary<string, IList>();
	}
}

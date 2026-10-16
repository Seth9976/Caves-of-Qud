using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003D8 RID: 984
	internal sealed class NamedCssStyleCollection
	{
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x0006E692 File Offset: 0x0006C892
		public CssStyleCollection Collection
		{
			get
			{
				if (this.collection == null)
				{
					this.collection = new CssStyleCollection();
				}
				return this.collection;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x0006E6AD File Offset: 0x0006C8AD
		// (set) Token: 0x06002A5A RID: 10842 RVA: 0x0006E6B5 File Offset: 0x0006C8B5
		public string Name { get; private set; }

		// Token: 0x06002A5B RID: 10843 RVA: 0x0006E6BE File Offset: 0x0006C8BE
		public NamedCssStyleCollection(string name)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			this.Name = name;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x0006E6D8 File Offset: 0x0006C8D8
		public NamedCssStyleCollection CopyFrom(CssStyleCollection coll)
		{
			if (coll == null)
			{
				return this;
			}
			CssStyleCollection cssStyleCollection = this.Collection;
			foreach (object obj in coll.Keys)
			{
				string text = (string)obj;
				cssStyleCollection.Add(text, coll[text]);
			}
			return this;
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0006E748 File Offset: 0x0006C948
		public NamedCssStyleCollection Add(HtmlTextWriterStyle key, string value)
		{
			this.Collection.Add(key, value);
			return this;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x0006E758 File Offset: 0x0006C958
		public NamedCssStyleCollection Add(string key, string value)
		{
			this.Collection.Add(key, value);
			return this;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0006E768 File Offset: 0x0006C968
		public NamedCssStyleCollection Add(Style style)
		{
			if (style != null)
			{
				this.CopyFrom(style.GetStyleAttributes(null));
			}
			return this;
		}

		// Token: 0x04001ADD RID: 6877
		private CssStyleCollection collection;
	}
}

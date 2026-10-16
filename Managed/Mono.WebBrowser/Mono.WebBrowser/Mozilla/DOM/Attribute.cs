using System;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000131 RID: 305
	internal class Attribute : Node, IAttribute, INode
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x00005BFD File Offset: 0x00003DFD
		public Attribute(WebBrowser control, nsIDOMAttr domAttribute)
			: base(control, domAttribute)
		{
			if (control.platform != control.enginePlatform)
			{
				this.attribute = nsDOMAttr.GetProxy(control, domAttribute);
				return;
			}
			this.attribute = domAttribute;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00005C2A File Offset: 0x00003E2A
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.attribute = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00005C45 File Offset: 0x00003E45
		public string Name
		{
			get
			{
				this.attribute.getName(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00005C64 File Offset: 0x00003E64
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x00005C83 File Offset: 0x00003E83
		public new string Value
		{
			get
			{
				this.attribute.getValue(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				this.attribute.setValue(this.storage);
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00005CA3 File Offset: 0x00003EA3
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x0400010E RID: 270
		private nsIDOMAttr attribute;
	}
}

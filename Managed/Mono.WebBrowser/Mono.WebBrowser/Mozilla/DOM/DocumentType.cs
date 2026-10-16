using System;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000137 RID: 311
	internal class DocumentType : Node, IDocumentType, INode
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x00006EE7 File Offset: 0x000050E7
		public DocumentType(WebBrowser control, nsIDOMDocumentType doctype)
			: base(control, doctype)
		{
			if (control.platform != control.enginePlatform)
			{
				this.doctype = nsDOMDocumentType.GetProxy(control, doctype);
				return;
			}
			this.doctype = doctype;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00006F14 File Offset: 0x00005114
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.resources.Clear();
				this.doctype = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00006F3A File Offset: 0x0000513A
		internal nsIDOMDocumentType ComObject
		{
			get
			{
				return this.doctype;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00006F42 File Offset: 0x00005142
		public string Name
		{
			get
			{
				this.doctype.getName(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00006F64 File Offset: 0x00005164
		public INamedNodeMap Entities
		{
			get
			{
				nsIDOMNamedNodeMap nsIDOMNamedNodeMap;
				this.doctype.getEntities(out nsIDOMNamedNodeMap);
				return new NamedNodeMap(this.control, nsIDOMNamedNodeMap);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00006F8C File Offset: 0x0000518C
		public INamedNodeMap Notations
		{
			get
			{
				nsIDOMNamedNodeMap nsIDOMNamedNodeMap;
				this.doctype.getNotations(out nsIDOMNamedNodeMap);
				return new NamedNodeMap(this.control, nsIDOMNamedNodeMap);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00006FB3 File Offset: 0x000051B3
		public string PublicId
		{
			get
			{
				this.doctype.getPublicId(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00006FD2 File Offset: 0x000051D2
		public string SystemId
		{
			get
			{
				this.doctype.getSystemId(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00006FF1 File Offset: 0x000051F1
		public string InternalSubset
		{
			get
			{
				this.doctype.getInternalSubset(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00007010 File Offset: 0x00005210
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x0400011B RID: 283
		internal nsIDOMDocumentType doctype;
	}
}

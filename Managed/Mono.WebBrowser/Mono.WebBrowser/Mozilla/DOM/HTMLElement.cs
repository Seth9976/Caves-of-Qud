using System;
using System.IO;
using System.Text;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x0200013A RID: 314
	internal class HTMLElement : Element, IElement, INode
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00007AA5 File Offset: 0x00005CA5
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00007AB2 File Offset: 0x00005CB2
		protected new nsIDOMHTMLElement node
		{
			get
			{
				return base.node as nsIDOMHTMLElement;
			}
			set
			{
				base.node = value;
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00007ABB File Offset: 0x00005CBB
		public HTMLElement(WebBrowser control, nsIDOMHTMLElement domHtmlElement)
			: base(control, domHtmlElement)
		{
			this.node = domHtmlElement;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00007ACC File Offset: 0x00005CCC
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.node = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00007AE8 File Offset: 0x00005CE8
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x00007B20 File Offset: 0x00005D20
		public new string InnerHTML
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return null;
				}
				nsIDOMNSHTMLElement.getInnerHTML(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMNSHTMLElement.setInnerHTML(this.storage);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00007B58 File Offset: 0x00005D58
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x00007CBC File Offset: 0x00005EBC
		public override string OuterHTML
		{
			get
			{
				string text;
				try
				{
					this.control.DocEncoder.Flags = DocumentEncoderFlags.OutputRaw;
					if (this.Equals(base.Owner.DocumentElement))
					{
						text = this.control.DocEncoder.EncodeToString((Document)base.Owner);
					}
					else
					{
						text = this.control.DocEncoder.EncodeToString(this);
					}
				}
				catch
				{
					string tagName = this.TagName;
					string text2 = "<" + tagName;
					foreach (object obj in this.Attributes)
					{
						IAttribute attribute = (IAttribute)obj;
						text2 = string.Concat(new string[] { text2, " ", attribute.Name, "=\"", attribute.Value, "\"" });
					}
					(this.node as nsIDOMNSHTMLElement).getInnerHTML(this.storage);
					text2 = string.Concat(new string[]
					{
						text2,
						">",
						Base.StringGet(this.storage),
						"</",
						tagName,
						">"
					});
					text = text2;
				}
				return text;
			}
			set
			{
				nsIDOMRange nsIDOMRange;
				(((Document)this.control.Document).XPComObject as nsIDOMDocumentRange).createRange(out nsIDOMRange);
				nsIDOMRange.setStartBefore(this.node);
				nsIDOMNSRange nsIDOMNSRange = nsIDOMRange as nsIDOMNSRange;
				Base.StringSet(this.storage, value);
				nsIDOMDocumentFragment nsIDOMDocumentFragment;
				nsIDOMNSRange.createContextualFragment(this.storage, out nsIDOMDocumentFragment);
				nsIDOMNode proxy;
				this.node.getParentNode(out proxy);
				proxy = nsDOMNode.GetProxy(this.control, proxy);
				nsIDOMNode nsIDOMNode;
				proxy.replaceChild(nsIDOMDocumentFragment, this.node, out nsIDOMNode);
				this.node = nsIDOMNode as nsIDOMHTMLElement;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00007D50 File Offset: 0x00005F50
		public override Stream ContentStream
		{
			get
			{
				Stream stream;
				try
				{
					this.control.DocEncoder.Flags = DocumentEncoderFlags.OutputRaw;
					if (this.Equals(base.Owner.DocumentElement))
					{
						stream = this.control.DocEncoder.EncodeToStream((Document)base.Owner);
					}
					else
					{
						stream = this.control.DocEncoder.EncodeToStream(this);
					}
				}
				catch
				{
					string tagName = this.TagName;
					string text = "<" + tagName;
					foreach (object obj in this.Attributes)
					{
						IAttribute attribute = (IAttribute)obj;
						text = string.Concat(new string[] { text, " ", attribute.Name, "=\"", attribute.Value, "\"" });
					}
					(this.node as nsIDOMNSHTMLElement).getInnerHTML(this.storage);
					text = string.Concat(new string[]
					{
						text,
						">",
						Base.StringGet(this.storage),
						"</",
						tagName,
						">"
					});
					stream = new MemoryStream(Encoding.UTF8.GetBytes(text));
				}
				return stream;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00007EC4 File Offset: 0x000060C4
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x00007EE5 File Offset: 0x000060E5
		public override bool Disabled
		{
			get
			{
				return this.HasAttribute("disabled") && bool.Parse(this.GetAttribute("disabled"));
			}
			set
			{
				if (this.HasAttribute("disabled"))
				{
					this.SetAttribute("disabled", value.ToString());
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00007F08 File Offset: 0x00006108
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x00007F29 File Offset: 0x00006129
		public override int TabIndex
		{
			get
			{
				int num;
				((nsIDOMNSHTMLElement)this.node).getTabIndex(out num);
				return num;
			}
			set
			{
				((nsIDOMNSHTMLElement)this.node).setTabIndex(value);
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00007F3D File Offset: 0x0000613D
		public override int GetHashCode()
		{
			return this.hashcode;
		}
	}
}

using System;
using System.IO;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000138 RID: 312
	internal class Element : Node, IElement, INode
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00007018 File Offset: 0x00005218
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00007025 File Offset: 0x00005225
		internal new nsIDOMElement node
		{
			get
			{
				return base.node as nsIDOMElement;
			}
			set
			{
				base.node = value;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0000702E File Offset: 0x0000522E
		public Element(WebBrowser control, nsIDOMElement domElement)
			: base(control, domElement)
		{
			this.node = domElement;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0000703F File Offset: 0x0000523F
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.node = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0000705C File Offset: 0x0000525C
		public virtual IElement AppendChild(IElement child)
		{
			Element element = (Element)child;
			nsIDOMNode nsIDOMNode;
			this.node.appendChild(element.node, out nsIDOMNode);
			return new Element(this.control, nsIDOMNode as nsIDOMElement);
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00007098 File Offset: 0x00005298
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x000070EC File Offset: 0x000052EC
		public virtual string InnerText
		{
			get
			{
				nsIDOMRange nsIDOMRange;
				(((Document)this.control.Document).XPComObject as nsIDOMDocumentRange).createRange(out nsIDOMRange);
				nsIDOMRange.selectNodeContents(this.node);
				nsIDOMRange.toString(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				this.node.setNodeValue(this.storage);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0000710C File Offset: 0x0000530C
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0000716C File Offset: 0x0000536C
		public virtual string OuterText
		{
			get
			{
				nsIDOMRange nsIDOMRange;
				(((Document)this.control.Document).XPComObject as nsIDOMDocumentRange).createRange(out nsIDOMRange);
				nsIDOMNode nsIDOMNode;
				this.node.getParentNode(out nsIDOMNode);
				nsIDOMRange.selectNodeContents(nsIDOMNode);
				nsIDOMRange.toString(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				nsIDOMNode nsIDOMNode;
				this.node.getParentNode(out nsIDOMNode);
				nsIDOMNode.setNodeValue(this.storage);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x000071A0 File Offset: 0x000053A0
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x000071A7 File Offset: 0x000053A7
		public virtual string InnerHTML
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x000071A9 File Offset: 0x000053A9
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x000071B0 File Offset: 0x000053B0
		public virtual string OuterHTML
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x000071B2 File Offset: 0x000053B2
		public virtual Stream ContentStream
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x000071B8 File Offset: 0x000053B8
		public IElementCollection All
		{
			get
			{
				if (!this.resources.Contains("All"))
				{
					HTMLElementCollection htmlelementCollection = new HTMLElementCollection(this.control);
					this.Recurse(htmlelementCollection, this.node);
					this.resources.Add("All", htmlelementCollection);
				}
				return this.resources["All"] as IElementCollection;
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00007218 File Offset: 0x00005418
		private void Recurse(HTMLElementCollection col, nsIDOMNode parent)
		{
			nsIDOMNodeList nsIDOMNodeList;
			parent.getChildNodes(out nsIDOMNodeList);
			uint num;
			nsIDOMNodeList.getLength(out num);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				nsIDOMNode nsIDOMNode;
				nsIDOMNodeList.item((uint)num2, out nsIDOMNode);
				ushort num3;
				nsIDOMNode.getNodeType(out num3);
				if (num3 == 1)
				{
					col.Add(new HTMLElement(this.control, (nsIDOMHTMLElement)nsIDOMNode));
					this.Recurse(col, nsIDOMNode);
				}
				num2++;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00007280 File Offset: 0x00005480
		public IElementCollection Children
		{
			get
			{
				if (!this.resources.Contains("Children"))
				{
					nsIDOMNodeList nsIDOMNodeList;
					this.node.getChildNodes(out nsIDOMNodeList);
					this.resources.Add("Children", new HTMLElementCollection(this.control, nsIDOMNodeList));
				}
				return this.resources["Children"] as IElementCollection;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x000072DE File Offset: 0x000054DE
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x000072E1 File Offset: 0x000054E1
		public virtual int TabIndex
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x000072E3 File Offset: 0x000054E3
		public virtual string TagName
		{
			get
			{
				this.node.getTagName(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00007302 File Offset: 0x00005502
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x00007305 File Offset: 0x00005505
		public virtual bool Disabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00007308 File Offset: 0x00005508
		public virtual int ClientWidth
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getClientWidth(out num);
				return num;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00007334 File Offset: 0x00005534
		public virtual int ClientHeight
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getClientHeight(out num);
				return num;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00007360 File Offset: 0x00005560
		public virtual int ScrollHeight
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getScrollHeight(out num);
				return num;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0000738C File Offset: 0x0000558C
		public virtual int ScrollWidth
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getScrollWidth(out num);
				return num;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x000073B8 File Offset: 0x000055B8
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x000073E4 File Offset: 0x000055E4
		public virtual int ScrollLeft
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getScrollLeft(out num);
				return num;
			}
			set
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return;
				}
				nsIDOMNSHTMLElement.setScrollLeft(value);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0000740C File Offset: 0x0000560C
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00007438 File Offset: 0x00005638
		public virtual int ScrollTop
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getScrollTop(out num);
				return num;
			}
			set
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return;
				}
				nsIDOMNSHTMLElement.setScrollTop(value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00007460 File Offset: 0x00005660
		public virtual int OffsetHeight
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getOffsetHeight(out num);
				return num;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0000748C File Offset: 0x0000568C
		public virtual int OffsetWidth
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getOffsetWidth(out num);
				return num;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000074B8 File Offset: 0x000056B8
		public virtual int OffsetLeft
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getOffsetLeft(out num);
				return num;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x000074E4 File Offset: 0x000056E4
		public virtual int OffsetTop
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return 0;
				}
				int num = 0;
				nsIDOMNSHTMLElement.getOffsetTop(out num);
				return num;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00007510 File Offset: 0x00005710
		public virtual IElement OffsetParent
		{
			get
			{
				nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
				if (nsIDOMNSHTMLElement == null)
				{
					return null;
				}
				nsIDOMElement nsIDOMElement;
				nsIDOMNSHTMLElement.getOffsetParent(out nsIDOMElement);
				if (nsIDOMElement is nsIDOMHTMLElement)
				{
					return new HTMLElement(this.control, nsIDOMElement as nsIDOMHTMLElement);
				}
				return new Element(this.control, nsIDOMElement);
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00007560 File Offset: 0x00005760
		public void Blur()
		{
			nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
			if (nsIDOMNSHTMLElement != null)
			{
				nsIDOMNSHTMLElement.blur();
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00007584 File Offset: 0x00005784
		public void Focus()
		{
			nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
			if (nsIDOMNSHTMLElement != null)
			{
				nsIDOMNSHTMLElement.focus();
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000075A8 File Offset: 0x000057A8
		public IElementCollection GetElementsByTagName(string name)
		{
			if (!this.resources.Contains("GetElementsByTagName" + name))
			{
				nsIDOMNodeList nsIDOMNodeList;
				this.node.getElementsByTagName(this.storage, out nsIDOMNodeList);
				this.resources.Add("GetElementsByTagName" + name, new HTMLElementCollection(this.control, nsIDOMNodeList));
			}
			return this.resources["GetElementsByTagName" + name] as IElementCollection;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0000761E File Offset: 0x0000581E
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00007628 File Offset: 0x00005828
		public virtual bool HasAttribute(string name)
		{
			Base.StringSet(this.storage, name);
			bool flag;
			this.node.hasAttribute(this.storage, out flag);
			return flag;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00007658 File Offset: 0x00005858
		public virtual string GetAttribute(string name)
		{
			UniString uniString = new UniString(string.Empty);
			Base.StringSet(this.storage, name);
			this.node.getAttribute(this.storage, uniString.Handle);
			return uniString.ToString();
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0000769C File Offset: 0x0000589C
		public void ScrollIntoView(bool alignWithTop)
		{
			nsIDOMNSHTMLElement nsIDOMNSHTMLElement = this.node as nsIDOMNSHTMLElement;
			if (nsIDOMNSHTMLElement != null)
			{
				nsIDOMNSHTMLElement.scrollIntoView(alignWithTop);
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x000076C0 File Offset: 0x000058C0
		public virtual void SetAttribute(string name, string value)
		{
			UniString uniString = new UniString(value);
			Base.StringSet(this.storage, name);
			this.node.setAttribute(this.storage, uniString.Handle);
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x000076F8 File Offset: 0x000058F8
		internal int Top
		{
			get
			{
				int num;
				((nsIDOMNSHTMLElement)this.node).getOffsetTop(out num);
				return num;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0000771C File Offset: 0x0000591C
		internal int Left
		{
			get
			{
				int num;
				((nsIDOMNSHTMLElement)this.node).getOffsetLeft(out num);
				return num;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00007740 File Offset: 0x00005940
		internal int Width
		{
			get
			{
				int num;
				((nsIDOMNSHTMLElement)this.node).getOffsetWidth(out num);
				return num;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00007764 File Offset: 0x00005964
		internal int Height
		{
			get
			{
				int num;
				((nsIDOMNSHTMLElement)this.node).getOffsetHeight(out num);
				return num;
			}
		}
	}
}

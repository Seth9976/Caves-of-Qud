using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000136 RID: 310
	internal class Document : Node, IDocument, INode
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00006293 File Offset: 0x00004493
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x000062A0 File Offset: 0x000044A0
		internal new nsIDOMDocument node
		{
			get
			{
				return base.node as nsIDOMDocument;
			}
			set
			{
				base.node = value;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000062A9 File Offset: 0x000044A9
		public Document(WebBrowser control, nsIDOMHTMLDocument document)
			: base(control, document)
		{
			if (control.platform != control.enginePlatform)
			{
				this.node = nsDOMHTMLDocument.GetProxy(control, document);
				return;
			}
			this.node = document;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000062D6 File Offset: 0x000044D6
		public Document(WebBrowser control, nsIDOMDocument document)
			: base(control, document)
		{
			if (control.platform != control.enginePlatform)
			{
				this.node = nsDOMDocument.GetProxy(control, document);
				return;
			}
			this.node = document;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00006303 File Offset: 0x00004503
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.resources.Clear();
				this.node = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00006329 File Offset: 0x00004529
		internal new nsIDOMDocument XPComObject
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00006334 File Offset: 0x00004534
		public IElement Active
		{
			get
			{
				nsIWebBrowserFocus nsIWebBrowserFocus = (nsIWebBrowserFocus)this.control.navigation.navigation;
				if (nsIWebBrowserFocus == null)
				{
					return null;
				}
				nsIDOMElement nsIDOMElement;
				nsIWebBrowserFocus.getFocusedElement(out nsIDOMElement);
				return (IElement)base.GetTypedNode(nsIDOMElement);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00006374 File Offset: 0x00004574
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x000063C4 File Offset: 0x000045C4
		public string ActiveLinkColor
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getALink(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setALink(this.storage);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00006410 File Offset: 0x00004610
		public IElementCollection Anchors
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				nsIDOMHTMLCollection nsIDOMHTMLCollection;
				((nsIDOMHTMLDocument)this.node).getAnchors(out nsIDOMHTMLCollection);
				return new HTMLElementCollection(this.control, (nsIDOMNodeList)nsIDOMHTMLCollection);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00006450 File Offset: 0x00004650
		public IElementCollection Applets
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				nsIDOMHTMLCollection nsIDOMHTMLCollection;
				((nsIDOMHTMLDocument)this.node).getApplets(out nsIDOMHTMLCollection);
				return new HTMLElementCollection(this.control, (nsIDOMNodeList)nsIDOMHTMLCollection);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00006490 File Offset: 0x00004690
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x000064E0 File Offset: 0x000046E0
		public string Background
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getBackground(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setBackground(this.storage);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0000652C File Offset: 0x0000472C
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0000657C File Offset: 0x0000477C
		public string BackColor
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getBgColor(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setBgColor(this.storage);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x000065C8 File Offset: 0x000047C8
		public IElement Body
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				if (!this.resources.Contains("Body"))
				{
					nsIDOMHTMLElement nsIDOMHTMLElement;
					((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
					this.resources.Add("Body", base.GetTypedNode(nsIDOMHTMLElement));
				}
				return this.resources["Body"] as IElement;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00006638 File Offset: 0x00004838
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x000066A0 File Offset: 0x000048A0
		public string Charset
		{
			get
			{
				nsIDOMAbstractView nsIDOMAbstractView;
				((nsIDOMDocumentView)this.node).getDefaultView(out nsIDOMAbstractView);
				IntPtr intPtr;
				((nsIInterfaceRequestor)nsIDOMAbstractView).getInterface(typeof(nsIDocCharset).GUID, out intPtr);
				nsIDocCharset nsIDocCharset = (nsIDocCharset)Marshal.GetObjectForIUnknown(intPtr);
				IntPtr intPtr2 = Marshal.StringToHGlobalUni(new StringBuilder(30).ToString());
				nsIDocCharset.getCharset(ref intPtr2);
				return Marshal.PtrToStringAnsi(intPtr2);
			}
			set
			{
				nsIDOMAbstractView nsIDOMAbstractView;
				((nsIDOMDocumentView)this.node).getDefaultView(out nsIDOMAbstractView);
				IntPtr intPtr;
				((nsIInterfaceRequestor)nsIDOMAbstractView).getInterface(typeof(nsIDocCharset).GUID, out intPtr);
				((nsIDocCharset)Marshal.GetTypedObjectForIUnknown(intPtr, typeof(nsIDocCharset))).setCharset(value);
				this.control.navigation.Go(this.Url, LoadFlags.CharsetChange);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00006714 File Offset: 0x00004914
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0000674B File Offset: 0x0000494B
		public string Cookie
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				((nsIDOMHTMLDocument)this.node).getCookie(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				((nsIDOMHTMLDocument)this.node).setCookie(this.storage);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0000677E File Offset: 0x0000497E
		public string Domain
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				((nsIDOMHTMLDocument)this.node).getDomain(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x000067B8 File Offset: 0x000049B8
		public IElement DocumentElement
		{
			get
			{
				if (!this.resources.Contains("DocumentElement"))
				{
					nsIDOMElement nsIDOMElement;
					this.node.getDocumentElement(out nsIDOMElement);
					this.resources.Add("DocumentElement", base.GetTypedNode(nsIDOMElement));
				}
				return this.resources["DocumentElement"] as IElement;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00006814 File Offset: 0x00004A14
		public IDocumentType DocType
		{
			get
			{
				nsIDOMDocumentType nsIDOMDocumentType;
				this.node.getDoctype(out nsIDOMDocumentType);
				return new DocumentType(this.control, nsIDOMDocumentType);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0000683C File Offset: 0x00004A3C
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0000688C File Offset: 0x00004A8C
		public string ForeColor
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getText(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setText(this.storage);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000068D8 File Offset: 0x00004AD8
		public IElementCollection Forms
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				nsIDOMHTMLCollection nsIDOMHTMLCollection;
				((nsIDOMHTMLDocument)this.node).getForms(out nsIDOMHTMLCollection);
				return new HTMLElementCollection(this.control, (nsIDOMNodeList)nsIDOMHTMLCollection);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00006918 File Offset: 0x00004B18
		public IElementCollection Images
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				nsIDOMHTMLCollection nsIDOMHTMLCollection;
				((nsIDOMHTMLDocument)this.node).getImages(out nsIDOMHTMLCollection);
				return new HTMLElementCollection(this.control, (nsIDOMNodeList)nsIDOMHTMLCollection);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00006958 File Offset: 0x00004B58
		public IDOMImplementation Implementation
		{
			get
			{
				nsIDOMDOMImplementation nsIDOMDOMImplementation;
				this.node.getImplementation(out nsIDOMDOMImplementation);
				return new DOMImplementation(this.control, nsIDOMDOMImplementation);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00006980 File Offset: 0x00004B80
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x000069D0 File Offset: 0x00004BD0
		public string LinkColor
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getLink(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setLink(this.storage);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00006A1C File Offset: 0x00004C1C
		public IElementCollection Links
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return null;
				}
				nsIDOMHTMLCollection nsIDOMHTMLCollection;
				((nsIDOMHTMLDocument)this.node).getLinks(out nsIDOMHTMLCollection);
				return new HTMLElementCollection(this.control, (nsIDOMNodeList)nsIDOMHTMLCollection);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00006A5C File Offset: 0x00004C5C
		public IStylesheetList Stylesheets
		{
			get
			{
				nsIDOMStyleSheetList nsIDOMStyleSheetList;
				((nsIDOMDocumentStyle)this.node).getStyleSheets(out nsIDOMStyleSheetList);
				return new StylesheetList(this.control, nsIDOMStyleSheetList);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00006A88 File Offset: 0x00004C88
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00006ABF File Offset: 0x00004CBF
		public string Title
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				((nsIDOMHTMLDocument)this.node).getTitle(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				((nsIDOMHTMLDocument)this.node).setTitle(this.storage);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00006AE4 File Offset: 0x00004CE4
		public string Url
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				((nsIDOMHTMLDocument)this.node).getURL(this.storage);
				return Base.StringGet(this.storage);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00006B1C File Offset: 0x00004D1C
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x00006B6C File Offset: 0x00004D6C
		public string VisitedLinkColor
		{
			get
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return string.Empty;
				}
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).getVLink(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				if (!(this.node is nsIDOMHTMLDocument))
				{
					return;
				}
				Base.StringSet(this.storage, value);
				nsIDOMHTMLElement nsIDOMHTMLElement;
				((nsIDOMHTMLDocument)this.node).getBody(out nsIDOMHTMLElement);
				((nsIDOMHTMLBodyElement)nsIDOMHTMLElement).setVLink(this.storage);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00006BB8 File Offset: 0x00004DB8
		public IWindow Window
		{
			get
			{
				nsIDOMAbstractView nsIDOMAbstractView;
				((nsIDOMDocumentView)this.node).getDefaultView(out nsIDOMAbstractView);
				nsIInterfaceRequestor nsIInterfaceRequestor = (nsIInterfaceRequestor)nsIDOMAbstractView;
				if (nsIInterfaceRequestor == null)
				{
					return null;
				}
				IntPtr intPtr;
				nsIInterfaceRequestor.getInterface(typeof(nsIDOMWindow).GUID, out intPtr);
				nsIDOMWindow nsIDOMWindow = (nsIDOMWindow)Marshal.GetObjectForIUnknown(intPtr);
				return new Window(this.control, nsIDOMWindow);
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00006C14 File Offset: 0x00004E14
		public IAttribute CreateAttribute(string name)
		{
			Base.StringSet(this.storage, name);
			nsIDOMAttr nsIDOMAttr;
			this.node.createAttribute(this.storage, out nsIDOMAttr);
			return new Attribute(this.control, nsIDOMAttr);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00006C50 File Offset: 0x00004E50
		public IElement CreateElement(string tagName)
		{
			Base.StringSet(this.storage, tagName);
			nsIDOMElement nsIDOMElement;
			this.node.createElement(this.storage, out nsIDOMElement);
			if (this.node is nsIDOMHTMLDocument)
			{
				return new HTMLElement(this.control, (nsIDOMHTMLElement)nsIDOMElement);
			}
			return new Element(this.control, nsIDOMElement);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public IElement GetElementById(string id)
		{
			if (!this.resources.Contains("GetElementById" + id))
			{
				Base.StringSet(this.storage, id);
				nsIDOMElement nsIDOMElement;
				this.node.getElementById(this.storage, out nsIDOMElement);
				if (nsIDOMElement == null)
				{
					return null;
				}
				this.resources.Add("GetElementById" + id, base.GetTypedNode(nsIDOMElement));
			}
			return this.resources["GetElementById" + id] as IElement;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00006D2C File Offset: 0x00004F2C
		public IElementCollection GetElementsByTagName(string name)
		{
			if (!this.resources.Contains("GetElementsByTagName" + name))
			{
				nsIDOMNodeList nsIDOMNodeList;
				this.node.getElementsByTagName(this.storage, out nsIDOMNodeList);
				if (nsIDOMNodeList == null)
				{
					return null;
				}
				this.resources.Add("GetElementsByTagName" + name, new HTMLElementCollection(this.control, nsIDOMNodeList));
			}
			return this.resources["GetElementsByTagName" + name] as IElementCollection;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public IElement GetElement(int x, int y)
		{
			nsIDOMNodeList nsIDOMNodeList;
			this.node.getChildNodes(out nsIDOMNodeList);
			NodeList nodeList = new HTMLElementCollection(this.control, nsIDOMNodeList);
			IElement element = null;
			foreach (object obj in nodeList)
			{
				Element element2 = (Element)obj;
				if (element2.Left <= x && element2.Top <= y && element2.Left + element2.Width >= x && element2.Top + element2.Height >= y)
				{
					element = element2;
					break;
				}
			}
			return element;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00006E4C File Offset: 0x0000504C
		public void Write(string text)
		{
			if (!(this.node is nsIDOMHTMLDocument))
			{
				return;
			}
			Base.StringSet(this.storage, text);
			((nsIDOMHTMLDocument)this.node).write(this.storage);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00006E7F File Offset: 0x0000507F
		public string InvokeScript(string script)
		{
			return Base.EvalScript(this.control, script);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00006E8D File Offset: 0x0000508D
		internal new EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000941 RID: 2369 RVA: 0x00006EA8 File Offset: 0x000050A8
		// (remove) Token: 0x06000942 RID: 2370 RVA: 0x00006EBB File Offset: 0x000050BB
		public event EventHandler LoadStopped
		{
			add
			{
				this.Events.AddHandler(Document.LoadStoppedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Document.LoadStoppedEvent, value);
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00006ECE File Offset: 0x000050CE
		public override int GetHashCode()
		{
			return this.node.GetHashCode();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00006EDB File Offset: 0x000050DB
		// Note: this type is marked as 'beforefieldinit'.
		static Document()
		{
			Document.LoadStoppedEvent = new object();
		}

		// Token: 0x04000119 RID: 281
		private EventHandlerList events;
	}
}

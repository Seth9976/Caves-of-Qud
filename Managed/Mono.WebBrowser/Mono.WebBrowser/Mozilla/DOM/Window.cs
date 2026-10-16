using System;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000143 RID: 323
	internal class Window : DOMObject, IWindow
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x00009BB3 File Offset: 0x00007DB3
		public Window(WebBrowser control, nsIDOMWindow domWindow)
			: base(control)
		{
			this.hashcode = domWindow.GetHashCode();
			this.window = domWindow;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00009BCF File Offset: 0x00007DCF
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.resources.Clear();
				this.window = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00009BF8 File Offset: 0x00007DF8
		internal static bool FindDocument(ref nsIDOMWindow window, int docHashcode)
		{
			nsIDOMDocument nsIDOMDocument;
			window.getDocument(out nsIDOMDocument);
			if (nsIDOMDocument.GetHashCode() == docHashcode)
			{
				return true;
			}
			uint num = 1U;
			nsIDOMWindowCollection nsIDOMWindowCollection;
			window.getFrames(out nsIDOMWindowCollection);
			nsIDOMWindowCollection.getLength(out num);
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				nsIDOMWindowCollection.item(num2, out window);
				if (Window.FindDocument(ref window, docHashcode))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00009C50 File Offset: 0x00007E50
		public IDocument Document
		{
			get
			{
				nsIDOMDocument nsIDOMDocument;
				this.window.getDocument(out nsIDOMDocument);
				if (!this.control.documents.ContainsKey(nsIDOMDocument.GetHashCode()))
				{
					this.control.documents.Add(nsIDOMDocument.GetHashCode(), new Document(this.control, (nsIDOMHTMLDocument)nsIDOMDocument));
				}
				return this.control.documents[nsIDOMDocument.GetHashCode()] as IDocument;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public IWindowCollection Frames
		{
			get
			{
				nsIDOMWindowCollection nsIDOMWindowCollection;
				this.window.getFrames(out nsIDOMWindowCollection);
				return new WindowCollection(this.control, nsIDOMWindowCollection);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00009CFB File Offset: 0x00007EFB
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x00009D1A File Offset: 0x00007F1A
		public string Name
		{
			get
			{
				this.window.getName(this.storage);
				return Base.StringGet(this.storage);
			}
			set
			{
				Base.StringSet(this.storage, value);
				this.window.setName(this.storage);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00009D3C File Offset: 0x00007F3C
		public IWindow Parent
		{
			get
			{
				nsIDOMWindow nsIDOMWindow;
				this.window.getParent(out nsIDOMWindow);
				return new Window(this.control, nsIDOMWindow);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00009D64 File Offset: 0x00007F64
		public IWindow Top
		{
			get
			{
				nsIDOMWindow nsIDOMWindow;
				this.window.getTop(out nsIDOMWindow);
				return new Window(this.control, nsIDOMWindow);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00009D8B File Offset: 0x00007F8B
		public string StatusText
		{
			get
			{
				return this.control.StatusText;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00009D98 File Offset: 0x00007F98
		public IHistory History
		{
			get
			{
				Navigation navigation = new Navigation(this.control, this.window as nsIWebNavigation);
				return new History(this.control, navigation);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00009DC8 File Offset: 0x00007FC8
		private EventListener EventListener
		{
			get
			{
				if (this.eventListener == null)
				{
					this.eventListener = new EventListener(this.window as nsIDOMEventTarget, this);
				}
				return this.eventListener;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00009DEF File Offset: 0x00007FEF
		public void AttachEventHandler(string eventName, EventHandler handler)
		{
			this.EventListener.AddHandler(handler, eventName);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00009DFE File Offset: 0x00007FFE
		public void DetachEventHandler(string eventName, EventHandler handler)
		{
			this.EventListener.RemoveHandler(handler, eventName);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00009E0D File Offset: 0x0000800D
		public void Focus()
		{
			((nsIWebBrowserFocus)this.window).setFocusedWindow(this.window);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00009E26 File Offset: 0x00008026
		public void Open(string url)
		{
			((nsIWebNavigation)this.window).loadURI(url, 0U, null, null, null);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00009E3E File Offset: 0x0000803E
		public void ScrollTo(int x, int y)
		{
			this.window.scrollTo(x, y);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00009E4E File Offset: 0x0000804E
		public override bool Equals(object obj)
		{
			return this == obj as Window;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00009E5C File Offset: 0x0000805C
		public static bool operator ==(Window left, Window right)
		{
			return left == right || (left != null && right != null && left.hashcode == right.hashcode);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00009E7A File Offset: 0x0000807A
		public static bool operator !=(Window left, Window right)
		{
			return !(left == right);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00009E86 File Offset: 0x00008086
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06000A44 RID: 2628 RVA: 0x00009E8E File Offset: 0x0000808E
		// (remove) Token: 0x06000A45 RID: 2629 RVA: 0x00009EAD File Offset: 0x000080AD
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(Window.LoadEvent, value);
				this.AttachEventHandler("load", value);
			}
			remove
			{
				base.Events.RemoveHandler(Window.LoadEvent, value);
				this.DetachEventHandler("load", value);
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000A46 RID: 2630 RVA: 0x00009ECC File Offset: 0x000080CC
		// (remove) Token: 0x06000A47 RID: 2631 RVA: 0x00009EEB File Offset: 0x000080EB
		public event EventHandler Unload
		{
			add
			{
				base.Events.AddHandler(Window.UnloadEvent, value);
				this.AttachEventHandler("unload", value);
			}
			remove
			{
				base.Events.RemoveHandler(Window.UnloadEvent, value);
				this.DetachEventHandler("unload", value);
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000A48 RID: 2632 RVA: 0x00009F0A File Offset: 0x0000810A
		// (remove) Token: 0x06000A49 RID: 2633 RVA: 0x00009F18 File Offset: 0x00008118
		public event EventHandler OnFocus
		{
			add
			{
				this.AttachEventHandler("focus", value);
			}
			remove
			{
				this.DetachEventHandler("focus", value);
			}
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000A4A RID: 2634 RVA: 0x00009F26 File Offset: 0x00008126
		// (remove) Token: 0x06000A4B RID: 2635 RVA: 0x00009F34 File Offset: 0x00008134
		public event EventHandler OnBlur
		{
			add
			{
				this.AttachEventHandler("blur", value);
			}
			remove
			{
				this.DetachEventHandler("blur", value);
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000A4C RID: 2636 RVA: 0x00009F42 File Offset: 0x00008142
		// (remove) Token: 0x06000A4D RID: 2637 RVA: 0x00009F50 File Offset: 0x00008150
		public event EventHandler Error
		{
			add
			{
				this.AttachEventHandler("error", value);
			}
			remove
			{
				this.DetachEventHandler("error", value);
			}
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000A4E RID: 2638 RVA: 0x00009F5E File Offset: 0x0000815E
		// (remove) Token: 0x06000A4F RID: 2639 RVA: 0x00009F6C File Offset: 0x0000816C
		public event EventHandler Scroll
		{
			add
			{
				this.AttachEventHandler("scroll", value);
			}
			remove
			{
				this.DetachEventHandler("scroll", value);
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00009F7C File Offset: 0x0000817C
		public void OnLoad()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Window.LoadEvent];
			if (eventHandler != null)
			{
				EventArgs e = new EventArgs();
				eventHandler(this, e);
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00009FB0 File Offset: 0x000081B0
		public void OnUnload()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Window.UnloadEvent];
			if (eventHandler != null)
			{
				EventArgs e = new EventArgs();
				eventHandler(this, e);
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00009FE4 File Offset: 0x000081E4
		// Note: this type is marked as 'beforefieldinit'.
		static Window()
		{
			Window.LoadEvent = new object();
			Window.UnloadEvent = new object();
		}

		// Token: 0x04000131 RID: 305
		internal nsIDOMWindow window;

		// Token: 0x04000132 RID: 306
		private EventListener eventListener;

		// Token: 0x04000133 RID: 307
		private int hashcode;
	}
}

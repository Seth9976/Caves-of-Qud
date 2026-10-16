using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Mozilla.DOM;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla
{
	// Token: 0x02000064 RID: 100
	internal class WebBrowser : IWebBrowser
	{
		// Token: 0x0600028C RID: 652 RVA: 0x000042F3 File Offset: 0x000024F3
		public WebBrowser(Platform platform)
		{
			this.platform = platform;
			this.callbacks = new Callback(this);
			this.loaded = Base.Init(this, platform);
			this.documents = new Hashtable();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00004326 File Offset: 0x00002526
		public bool Load(IntPtr handle, int width, int height)
		{
			this.loaded = Base.Bind(this, handle, width, height);
			return this.loaded;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00004340 File Offset: 0x00002540
		private bool Created
		{
			get
			{
				if (!this.creating && !this.created)
				{
					this.creating = true;
					this.created = Base.Create(this);
					if (this.created && this.isDirty)
					{
						this.isDirty = false;
						Base.Resize(this, this.width, this.height);
					}
				}
				return this.created;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000439F File Offset: 0x0000259F
		public void Shutdown()
		{
			Base.Shutdown(this);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000043A7 File Offset: 0x000025A7
		internal void Reset()
		{
			this.document = null;
			this.DomEvents.Dispose();
			this.domEvents = null;
			this.documents.Clear();
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000043CD File Offset: 0x000025CD
		public bool Initialized
		{
			get
			{
				return this.loaded;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000043D8 File Offset: 0x000025D8
		public IWindow Window
		{
			get
			{
				if (this.Navigation != null)
				{
					nsIDOMWindow nsIDOMWindow;
					((nsIWebBrowserFocus)this.navigation.navigation).getFocusedWindow(out nsIDOMWindow);
					if (nsIDOMWindow == null)
					{
						((nsIWebBrowser)this.navigation.navigation).getContentDOMWindow(out nsIDOMWindow);
					}
					if (nsIDOMWindow != null)
					{
						return new Window(this, nsIDOMWindow);
					}
				}
				return null;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000442C File Offset: 0x0000262C
		public IDocument Document
		{
			get
			{
				if (this.Navigation != null && this.document == null)
				{
					this.document = this.navigation.Document;
				}
				return this.document;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000445C File Offset: 0x0000265C
		public INavigation Navigation
		{
			get
			{
				if (!this.Created)
				{
					return null;
				}
				if (this.navigation == null)
				{
					nsIWebNavigation webNavigation = Base.GetWebNavigation(this);
					this.navigation = new Navigation(this, webNavigation);
				}
				return this.navigation;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00004495 File Offset: 0x00002695
		public string StatusText
		{
			get
			{
				return this.statusText;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000044A0 File Offset: 0x000026A0
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000044C6 File Offset: 0x000026C6
		public bool Offline
		{
			get
			{
				if (!this.Created)
				{
					return true;
				}
				bool flag;
				this.IOService.getOffline(out flag);
				return flag;
			}
			set
			{
				this.IOService.setOffline(value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000044D5 File Offset: 0x000026D5
		internal EventHandlerList DomEvents
		{
			get
			{
				if (this.domEvents == null)
				{
					this.domEvents = new EventHandlerList();
				}
				return this.domEvents;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000044F0 File Offset: 0x000026F0
		internal EventHandlerList Events
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000450B File Offset: 0x0000270B
		private ContentListener ContentListener
		{
			get
			{
				if (this.contentListener == null)
				{
					this.contentListener = new ContentListener(this);
				}
				return this.contentListener;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00004527 File Offset: 0x00002727
		internal nsIServiceManager ServiceManager
		{
			get
			{
				if (this.servMan == null)
				{
					this.servMan = Base.GetServiceManager(this);
				}
				return this.servMan;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00004544 File Offset: 0x00002744
		internal nsIIOService IOService
		{
			get
			{
				if (this.ioService == null)
				{
					IntPtr zero = IntPtr.Zero;
					this.ServiceManager.getServiceByContractID("@mozilla.org/network/io-service;1", typeof(nsIIOService).GUID, out zero);
					if (zero == IntPtr.Zero)
					{
						throw new Mono.WebBrowser.Exception(Mono.WebBrowser.Exception.ErrorCodes.IOService);
					}
					try
					{
						this.ioService = (nsIIOService)Marshal.GetObjectForIUnknown(zero);
					}
					catch (global::System.Exception ex)
					{
						throw new Mono.WebBrowser.Exception(Mono.WebBrowser.Exception.ErrorCodes.IOService, ex);
					}
				}
				return this.ioService;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000045C8 File Offset: 0x000027C8
		internal nsIAccessibilityService AccessibilityService
		{
			get
			{
				if (this.accessibilityService == null)
				{
					IntPtr zero = IntPtr.Zero;
					this.ServiceManager.getServiceByContractID("@mozilla.org/accessibilityService;1", typeof(nsIAccessibilityService).GUID, out zero);
					if (zero == IntPtr.Zero)
					{
						throw new Mono.WebBrowser.Exception(Mono.WebBrowser.Exception.ErrorCodes.AccessibilityService);
					}
					try
					{
						this.accessibilityService = (nsIAccessibilityService)Marshal.GetObjectForIUnknown(zero);
					}
					catch (global::System.Exception ex)
					{
						throw new Mono.WebBrowser.Exception(Mono.WebBrowser.Exception.ErrorCodes.AccessibilityService, ex);
					}
				}
				return this.accessibilityService;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00004650 File Offset: 0x00002850
		internal nsIErrorService ErrorService
		{
			get
			{
				if (this.errorService == null)
				{
					IntPtr zero = IntPtr.Zero;
					this.ServiceManager.getServiceByContractID("@mozilla.org/xpcom/error-service;1", typeof(nsIErrorService).GUID, out zero);
					if (zero == IntPtr.Zero)
					{
						return null;
					}
					try
					{
						this.errorService = (nsIErrorService)Marshal.GetObjectForIUnknown(zero);
					}
					catch (global::System.Exception)
					{
						return null;
					}
				}
				return this.errorService;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000046D0 File Offset: 0x000028D0
		internal DocumentEncoder DocEncoder
		{
			get
			{
				if (this.docEncoder == null)
				{
					this.docEncoder = new DocumentEncoder(this);
				}
				return this.docEncoder;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000046EC File Offset: 0x000028EC
		public void FocusIn(FocusOption focus)
		{
			if (!this.created)
			{
				return;
			}
			Base.Focus(this, focus);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000046FE File Offset: 0x000028FE
		public void FocusOut()
		{
			if (!this.created)
			{
				return;
			}
			Base.Blur(this);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000470F File Offset: 0x0000290F
		public void Activate()
		{
			if (!this.Created)
			{
				return;
			}
			Base.Activate(this);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00004720 File Offset: 0x00002920
		public void Deactivate()
		{
			if (!this.created)
			{
				return;
			}
			Base.Deactivate(this);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00004731 File Offset: 0x00002931
		public void Resize(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.isDirty = true;
			if (!this.created)
			{
				return;
			}
			Base.Resize(this, width, height);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000475C File Offset: 0x0000295C
		public void Render(byte[] data)
		{
			if (!this.Created)
			{
				return;
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			string @string = Encoding.UTF8.GetString(data);
			this.Render(@string);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00004793 File Offset: 0x00002993
		public void Render(string html)
		{
			if (!this.Created)
			{
				return;
			}
			this.Render(html, "file:///", "text/html");
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000047B0 File Offset: 0x000029B0
		public void Render(string html, string uri, string contentType)
		{
			if (!this.Created)
			{
				return;
			}
			if (this.Navigation != null)
			{
				nsIWebBrowserStream nsIWebBrowserStream = (nsIWebBrowserStream)this.navigation.navigation;
				AsciiString asciiString = new AsciiString(uri);
				nsIURI nsIURI;
				this.IOService.newURI(asciiString.Handle, null, null, out nsIURI);
				HandleRef handle = new AsciiString(contentType).Handle;
				nsIWebBrowserStream.openStream(nsIURI, handle);
				IntPtr intPtr = Marshal.StringToHGlobalAnsi(html);
				nsIWebBrowserStream.appendToStream(intPtr, (uint)html.Length);
				Marshal.FreeHGlobal(intPtr);
				nsIWebBrowserStream.closeStream();
				return;
			}
			throw new Mono.WebBrowser.Exception(Mono.WebBrowser.Exception.ErrorCodes.Navigation);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000483F File Offset: 0x00002A3F
		public void ExecuteScript(string script)
		{
			if (!this.Created)
			{
				return;
			}
			Base.EvalScript(this, script);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00004854 File Offset: 0x00002A54
		internal void AttachEvent(INode node, string eve, EventHandler handler)
		{
			string text = string.Intern(node.GetHashCode().ToString() + ":" + eve);
			this.DomEvents.AddHandler(text, handler);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00004890 File Offset: 0x00002A90
		internal void DetachEvent(INode node, string eve, EventHandler handler)
		{
			string text = string.Intern(node.GetHashCode().ToString() + ":" + eve);
			this.DomEvents.RemoveHandler(text, handler);
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060002AB RID: 683 RVA: 0x000048C9 File Offset: 0x00002AC9
		// (remove) Token: 0x060002AC RID: 684 RVA: 0x000048DC File Offset: 0x00002ADC
		public event NodeEventHandler KeyDown
		{
			add
			{
				this.Events.AddHandler(WebBrowser.KeyDownEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.KeyDownEvent, value);
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060002AD RID: 685 RVA: 0x000048EF File Offset: 0x00002AEF
		// (remove) Token: 0x060002AE RID: 686 RVA: 0x00004902 File Offset: 0x00002B02
		public event NodeEventHandler KeyPress
		{
			add
			{
				this.Events.AddHandler(WebBrowser.KeyPressEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.KeyPressEvent, value);
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060002AF RID: 687 RVA: 0x00004915 File Offset: 0x00002B15
		// (remove) Token: 0x060002B0 RID: 688 RVA: 0x00004928 File Offset: 0x00002B28
		public event NodeEventHandler KeyUp
		{
			add
			{
				this.Events.AddHandler(WebBrowser.KeyUpEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.KeyUpEvent, value);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060002B1 RID: 689 RVA: 0x0000493B File Offset: 0x00002B3B
		// (remove) Token: 0x060002B2 RID: 690 RVA: 0x0000494E File Offset: 0x00002B4E
		public event NodeEventHandler MouseClick
		{
			add
			{
				this.Events.AddHandler(WebBrowser.MouseClickEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.MouseClickEvent, value);
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060002B3 RID: 691 RVA: 0x00004961 File Offset: 0x00002B61
		// (remove) Token: 0x060002B4 RID: 692 RVA: 0x00004974 File Offset: 0x00002B74
		public event NodeEventHandler MouseDoubleClick
		{
			add
			{
				this.Events.AddHandler(WebBrowser.MouseDoubleClickEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.MouseDoubleClickEvent, value);
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060002B5 RID: 693 RVA: 0x00004987 File Offset: 0x00002B87
		// (remove) Token: 0x060002B6 RID: 694 RVA: 0x0000499A File Offset: 0x00002B9A
		public event NodeEventHandler MouseDown
		{
			add
			{
				this.Events.AddHandler(WebBrowser.MouseDownEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.MouseDownEvent, value);
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060002B7 RID: 695 RVA: 0x000049AD File Offset: 0x00002BAD
		// (remove) Token: 0x060002B8 RID: 696 RVA: 0x000049C0 File Offset: 0x00002BC0
		public event NodeEventHandler MouseEnter
		{
			add
			{
				this.Events.AddHandler(WebBrowser.MouseEnterEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.MouseEnterEvent, value);
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060002B9 RID: 697 RVA: 0x000049D3 File Offset: 0x00002BD3
		// (remove) Token: 0x060002BA RID: 698 RVA: 0x000049E6 File Offset: 0x00002BE6
		public event NodeEventHandler MouseLeave
		{
			add
			{
				this.Events.AddHandler(WebBrowser.MouseLeaveEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.MouseLeaveEvent, value);
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060002BB RID: 699 RVA: 0x000049F9 File Offset: 0x00002BF9
		// (remove) Token: 0x060002BC RID: 700 RVA: 0x00004A0C File Offset: 0x00002C0C
		public event NodeEventHandler MouseMove
		{
			add
			{
				this.Events.AddHandler(WebBrowser.MouseMoveEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.MouseMoveEvent, value);
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060002BD RID: 701 RVA: 0x00004A1F File Offset: 0x00002C1F
		// (remove) Token: 0x060002BE RID: 702 RVA: 0x00004A32 File Offset: 0x00002C32
		public event NodeEventHandler MouseUp
		{
			add
			{
				this.Events.AddHandler(WebBrowser.MouseUpEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.MouseUpEvent, value);
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060002BF RID: 703 RVA: 0x00004A45 File Offset: 0x00002C45
		// (remove) Token: 0x060002C0 RID: 704 RVA: 0x00004A58 File Offset: 0x00002C58
		public event EventHandler Focus
		{
			add
			{
				this.Events.AddHandler(WebBrowser.FocusEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.FocusEvent, value);
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060002C1 RID: 705 RVA: 0x00004A6B File Offset: 0x00002C6B
		// (remove) Token: 0x060002C2 RID: 706 RVA: 0x00004A7E File Offset: 0x00002C7E
		public event EventHandler Blur
		{
			add
			{
				this.Events.AddHandler(WebBrowser.BlurEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.BlurEvent, value);
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060002C3 RID: 707 RVA: 0x00004A91 File Offset: 0x00002C91
		// (remove) Token: 0x060002C4 RID: 708 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public event CreateNewWindowEventHandler CreateNewWindow
		{
			add
			{
				this.Events.AddHandler(WebBrowser.CreateNewWindowEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.CreateNewWindowEvent, value);
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060002C5 RID: 709 RVA: 0x00004AB7 File Offset: 0x00002CB7
		// (remove) Token: 0x060002C6 RID: 710 RVA: 0x00004ACA File Offset: 0x00002CCA
		public event AlertEventHandler Alert
		{
			add
			{
				this.Events.AddHandler(WebBrowser.AlertEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.AlertEvent, value);
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060002C7 RID: 711 RVA: 0x00004ADD File Offset: 0x00002CDD
		// (remove) Token: 0x060002C8 RID: 712 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public event EventHandler Loaded
		{
			add
			{
				this.Events.AddHandler(WebBrowser.LoadEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.LoadEvent, value);
			}
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060002C9 RID: 713 RVA: 0x00004B03 File Offset: 0x00002D03
		// (remove) Token: 0x060002CA RID: 714 RVA: 0x00004B16 File Offset: 0x00002D16
		public event EventHandler Unloaded
		{
			add
			{
				this.Events.AddHandler(WebBrowser.UnloadEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.UnloadEvent, value);
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060002CB RID: 715 RVA: 0x00004B29 File Offset: 0x00002D29
		// (remove) Token: 0x060002CC RID: 716 RVA: 0x00004B3C File Offset: 0x00002D3C
		public event StatusChangedEventHandler StatusChanged
		{
			add
			{
				this.Events.AddHandler(WebBrowser.StatusChangedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.StatusChangedEvent, value);
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060002CD RID: 717 RVA: 0x00004B4F File Offset: 0x00002D4F
		// (remove) Token: 0x060002CE RID: 718 RVA: 0x00004B62 File Offset: 0x00002D62
		public event SecurityChangedEventHandler SecurityChanged
		{
			add
			{
				this.Events.AddHandler(WebBrowser.SecurityChangedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.SecurityChangedEvent, value);
			}
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060002CF RID: 719 RVA: 0x00004B75 File Offset: 0x00002D75
		// (remove) Token: 0x060002D0 RID: 720 RVA: 0x00004B88 File Offset: 0x00002D88
		public event LoadStartedEventHandler LoadStarted
		{
			add
			{
				this.Events.AddHandler(WebBrowser.LoadStartedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.LoadStartedEvent, value);
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060002D1 RID: 721 RVA: 0x00004B9B File Offset: 0x00002D9B
		// (remove) Token: 0x060002D2 RID: 722 RVA: 0x00004BAE File Offset: 0x00002DAE
		public event LoadCommitedEventHandler LoadCommited
		{
			add
			{
				this.Events.AddHandler(WebBrowser.LoadCommitedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.LoadCommitedEvent, value);
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060002D3 RID: 723 RVA: 0x00004BC1 File Offset: 0x00002DC1
		// (remove) Token: 0x060002D4 RID: 724 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public event Mono.WebBrowser.ProgressChangedEventHandler ProgressChanged
		{
			add
			{
				this.Events.AddHandler(WebBrowser.ProgressChangedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.ProgressChangedEvent, value);
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x060002D5 RID: 725 RVA: 0x00004BE7 File Offset: 0x00002DE7
		// (remove) Token: 0x060002D6 RID: 726 RVA: 0x00004BFA File Offset: 0x00002DFA
		public event LoadFinishedEventHandler LoadFinished
		{
			add
			{
				this.Events.AddHandler(WebBrowser.LoadFinishedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.LoadFinishedEvent, value);
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060002D7 RID: 727 RVA: 0x00004C0D File Offset: 0x00002E0D
		// (remove) Token: 0x060002D8 RID: 728 RVA: 0x00004C20 File Offset: 0x00002E20
		public event ContextMenuEventHandler ContextMenuShown
		{
			add
			{
				this.Events.AddHandler(WebBrowser.ContextMenuEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.ContextMenuEvent, value);
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060002D9 RID: 729 RVA: 0x00004C33 File Offset: 0x00002E33
		// (remove) Token: 0x060002DA RID: 730 RVA: 0x00004C41 File Offset: 0x00002E41
		public event NavigationRequestedEventHandler NavigationRequested
		{
			add
			{
				this.ContentListener.AddHandler(value);
			}
			remove
			{
				this.ContentListener.RemoveHandler(value);
			}
		}

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060002DB RID: 731 RVA: 0x00004C4F File Offset: 0x00002E4F
		// (remove) Token: 0x060002DC RID: 732 RVA: 0x00004C62 File Offset: 0x00002E62
		internal event EventHandler Generic
		{
			add
			{
				this.Events.AddHandler(WebBrowser.GenericEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(WebBrowser.GenericEvent, value);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00004C78 File Offset: 0x00002E78
		// Note: this type is marked as 'beforefieldinit'.
		static WebBrowser()
		{
			WebBrowser.KeyDownEvent = new object();
			WebBrowser.KeyPressEvent = new object();
			WebBrowser.KeyUpEvent = new object();
			WebBrowser.MouseClickEvent = new object();
			WebBrowser.MouseDoubleClickEvent = new object();
			WebBrowser.MouseDownEvent = new object();
			WebBrowser.MouseEnterEvent = new object();
			WebBrowser.MouseLeaveEvent = new object();
			WebBrowser.MouseMoveEvent = new object();
			WebBrowser.MouseUpEvent = new object();
			WebBrowser.FocusEvent = new object();
			WebBrowser.BlurEvent = new object();
			WebBrowser.CreateNewWindowEvent = new object();
			WebBrowser.AlertEvent = new object();
			WebBrowser.LoadStartedEvent = new object();
			WebBrowser.LoadCommitedEvent = new object();
			WebBrowser.ProgressChangedEvent = new object();
			WebBrowser.LoadFinishedEvent = new object();
			WebBrowser.LoadEvent = new object();
			WebBrowser.UnloadEvent = new object();
			WebBrowser.StatusChangedEvent = new object();
			WebBrowser.SecurityChangedEvent = new object();
			WebBrowser.ProgressEvent = new object();
			WebBrowser.ContextMenuEvent = new object();
			WebBrowser.NavigationRequestedEvent = new object();
			WebBrowser.GenericEvent = new object();
		}

		// Token: 0x040000DA RID: 218
		private bool loaded;

		// Token: 0x040000DB RID: 219
		internal bool created;

		// Token: 0x040000DC RID: 220
		private bool creating;

		// Token: 0x040000DD RID: 221
		internal Document document;

		// Token: 0x040000DE RID: 222
		internal Navigation navigation;

		// Token: 0x040000DF RID: 223
		internal Platform platform;

		// Token: 0x040000E0 RID: 224
		internal Platform enginePlatform;

		// Token: 0x040000E1 RID: 225
		internal Callback callbacks;

		// Token: 0x040000E2 RID: 226
		private EventHandlerList events;

		// Token: 0x040000E3 RID: 227
		private EventHandlerList domEvents;

		// Token: 0x040000E4 RID: 228
		private string statusText;

		// Token: 0x040000E5 RID: 229
		private bool streamingMode;

		// Token: 0x040000E6 RID: 230
		internal Hashtable documents;

		// Token: 0x040000E7 RID: 231
		private int width;

		// Token: 0x040000E8 RID: 232
		private int height;

		// Token: 0x040000E9 RID: 233
		private bool isDirty;

		// Token: 0x040000EA RID: 234
		private ContentListener contentListener;

		// Token: 0x040000EB RID: 235
		private nsIServiceManager servMan;

		// Token: 0x040000EC RID: 236
		private nsIIOService ioService;

		// Token: 0x040000ED RID: 237
		private nsIAccessibilityService accessibilityService;

		// Token: 0x040000EE RID: 238
		private nsIErrorService errorService;

		// Token: 0x040000EF RID: 239
		private DocumentEncoder docEncoder;

		// Token: 0x04000102 RID: 258
		internal static object LoadEvent;

		// Token: 0x04000103 RID: 259
		internal static object UnloadEvent;

		// Token: 0x04000106 RID: 262
		internal static object ProgressEvent;

		// Token: 0x04000107 RID: 263
		internal static object ContextMenuEvent;
	}
}

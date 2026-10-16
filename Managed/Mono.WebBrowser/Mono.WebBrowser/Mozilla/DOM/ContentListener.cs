using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Mono.WebBrowser;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000133 RID: 307
	internal class ContentListener : nsIURIContentListener
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00005E43 File Offset: 0x00004043
		public ContentListener(WebBrowser instance)
		{
			this.owner = instance;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00005E52 File Offset: 0x00004052
		public EventHandlerList Events
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

		// Token: 0x060008FE RID: 2302 RVA: 0x00005E70 File Offset: 0x00004070
		public void AddHandler(NavigationRequestedEventHandler value)
		{
			if (this.Events[WebBrowser.NavigationRequestedEvent] == null && this.owner.Navigation != null)
			{
				((nsIWebBrowser)this.owner.navigation.navigation).setParentURIContentListener(this);
			}
			this.Events.AddHandler(WebBrowser.NavigationRequestedEvent, value);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00005EC9 File Offset: 0x000040C9
		public void RemoveHandler(NavigationRequestedEventHandler value)
		{
			this.Events.RemoveHandler(WebBrowser.NavigationRequestedEvent, value);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00005EDC File Offset: 0x000040DC
		bool nsIURIContentListener.onStartURIOpen(nsIURI aURI)
		{
			NavigationRequestedEventHandler navigationRequestedEventHandler = (NavigationRequestedEventHandler)this.Events[WebBrowser.NavigationRequestedEvent];
			if (navigationRequestedEventHandler != null)
			{
				AsciiString asciiString = new AsciiString("");
				aURI.getSpec(asciiString.Handle);
				NavigationRequestedEventArgs e = new NavigationRequestedEventArgs(asciiString.ToString());
				navigationRequestedEventHandler(this, e);
				return e.Cancel;
			}
			return true;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00005F36 File Offset: 0x00004136
		bool nsIURIContentListener.doContent(string aContentType, bool aIsContentPreferred, nsIRequest aRequest, out nsIStreamListener aContentHandler)
		{
			aContentHandler = null;
			return true;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00005F3D File Offset: 0x0000413D
		bool nsIURIContentListener.isPreferred(string aContentType, ref string aDesiredContentType)
		{
			return true;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00005F40 File Offset: 0x00004140
		bool nsIURIContentListener.canHandleContent(string aContentType, bool aIsContentPreferred, ref string aDesiredContentType)
		{
			return true;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00005F43 File Offset: 0x00004143
		[return: MarshalAs(UnmanagedType.Interface)]
		IntPtr nsIURIContentListener.getLoadCookie()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00005F4A File Offset: 0x0000414A
		void nsIURIContentListener.setLoadCookie([MarshalAs(UnmanagedType.Interface)] IntPtr value)
		{
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00005F4C File Offset: 0x0000414C
		nsIURIContentListener nsIURIContentListener.getParentContentListener()
		{
			return null;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00005F4F File Offset: 0x0000414F
		void nsIURIContentListener.setParentContentListener(nsIURIContentListener value)
		{
		}

		// Token: 0x04000110 RID: 272
		private WebBrowser owner;

		// Token: 0x04000111 RID: 273
		private EventHandlerList events;
	}
}

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000139 RID: 313
	internal class EventListener : nsIDOMEventListener
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00007785 File Offset: 0x00005985
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x000077A0 File Offset: 0x000059A0
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x000077A8 File Offset: 0x000059A8
		public nsIDOMEventTarget Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000077B4 File Offset: 0x000059B4
		public EventListener(nsIDOMEventTarget target, object owner)
		{
			this.target = target;
			this.owner = owner;
			IntPtr intPtr = Base.StringInit();
			this.storage = new HandleRef(this, intPtr);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000077E8 File Offset: 0x000059E8
		~EventListener()
		{
			this.Dispose(false);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00007818 File Offset: 0x00005A18
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					Base.StringFinish(this.storage);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00007837 File Offset: 0x00005A37
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00007848 File Offset: 0x00005A48
		public void AddHandler(EventHandler handler, string _event)
		{
			string text = string.Intern(this.target.GetHashCode().ToString() + ":" + _event);
			this.Events.AddHandler(text, handler);
			Base.StringSet(this.storage, _event);
			this.target.addEventListener(this.storage, this, true);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000078A8 File Offset: 0x00005AA8
		public void RemoveHandler(EventHandler handler, string _event)
		{
			string text = string.Intern(this.target.GetHashCode().ToString() + ":" + _event);
			this.Events.RemoveHandler(text, handler);
			Base.StringSet(this.storage, _event);
			this.target.removeEventListener(this.storage, this, true);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00007908 File Offset: 0x00005B08
		public void AddHandler(NodeEventHandler handler, string _event)
		{
			string text = string.Intern(this.target.GetHashCode().ToString() + ":" + _event);
			this.Events.AddHandler(text, handler);
			Base.StringSet(this.storage, _event);
			this.target.addEventListener(this.storage, this, true);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00007968 File Offset: 0x00005B68
		public void RemoveHandler(NodeEventHandler handler, string _event)
		{
			string text = string.Intern(this.target.GetHashCode().ToString() + ":" + _event);
			this.Events.RemoveHandler(text, handler);
			Base.StringSet(this.storage, _event);
			this.target.removeEventListener(this.storage, this, true);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000079C8 File Offset: 0x00005BC8
		public int handleEvent(nsIDOMEvent _event)
		{
			_event.getType(this.storage);
			string text = Base.StringGet(this.storage);
			string text2 = string.Intern(this.target.GetHashCode().ToString() + ":" + text);
			EventHandler eventHandler = this.Events[text2] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this.owner, new EventArgs());
				return 0;
			}
			NodeEventHandler nodeEventHandler = this.Events[text2] as NodeEventHandler;
			if (nodeEventHandler != null)
			{
				nodeEventHandler(this.owner, new NodeEventArgs((INode)this.owner));
				return 0;
			}
			WindowEventHandler windowEventHandler = this.Events[text2] as WindowEventHandler;
			if (windowEventHandler != null)
			{
				windowEventHandler(this.owner, new WindowEventArgs((IWindow)this.owner));
				return 0;
			}
			return 0;
		}

		// Token: 0x0400011C RID: 284
		private HandleRef storage;

		// Token: 0x0400011D RID: 285
		private bool disposed;

		// Token: 0x0400011E RID: 286
		private object owner;

		// Token: 0x0400011F RID: 287
		private EventHandlerList events;

		// Token: 0x04000120 RID: 288
		private nsIDOMEventTarget target;
	}
}

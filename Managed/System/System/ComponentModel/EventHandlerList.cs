using System;

namespace System.ComponentModel
{
	/// <summary>Provides a simple list of delegates. This class cannot be inherited.</summary>
	// Token: 0x0200036F RID: 879
	public sealed class EventHandlerList : IDisposable
	{
		// Token: 0x06001D0F RID: 7439 RVA: 0x00068322 File Offset: 0x00066522
		internal EventHandlerList(Component parent)
		{
			this._parent = parent;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventHandlerList" /> class. </summary>
		// Token: 0x06001D10 RID: 7440 RVA: 0x0000219B File Offset: 0x0000039B
		public EventHandlerList()
		{
		}

		/// <summary>Gets or sets the delegate for the specified object.</summary>
		/// <returns>The delegate for the specified key, or null if a delegate does not exist.</returns>
		/// <param name="key">An object to find in the list. </param>
		// Token: 0x170005DA RID: 1498
		public Delegate this[object key]
		{
			get
			{
				EventHandlerList.ListEntry listEntry = null;
				if (this._parent == null || this._parent.CanRaiseEventsInternal)
				{
					listEntry = this.Find(key);
				}
				if (listEntry == null)
				{
					return null;
				}
				return listEntry._handler;
			}
			set
			{
				EventHandlerList.ListEntry listEntry = this.Find(key);
				if (listEntry != null)
				{
					listEntry._handler = value;
					return;
				}
				this._head = new EventHandlerList.ListEntry(key, value, this._head);
			}
		}

		/// <summary>Adds a delegate to the list.</summary>
		/// <param name="key">The object that owns the event. </param>
		/// <param name="value">The delegate to add to the list. </param>
		// Token: 0x06001D13 RID: 7443 RVA: 0x000683A0 File Offset: 0x000665A0
		public void AddHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry._handler = Delegate.Combine(listEntry._handler, value);
				return;
			}
			this._head = new EventHandlerList.ListEntry(key, value, this._head);
		}

		/// <summary>Adds a list of delegates to the current list.</summary>
		/// <param name="listToAddFrom">The list to add.</param>
		// Token: 0x06001D14 RID: 7444 RVA: 0x000683E0 File Offset: 0x000665E0
		public void AddHandlers(EventHandlerList listToAddFrom)
		{
			for (EventHandlerList.ListEntry listEntry = listToAddFrom._head; listEntry != null; listEntry = listEntry._next)
			{
				this.AddHandler(listEntry._key, listEntry._handler);
			}
		}

		/// <summary>Disposes the delegate list.</summary>
		// Token: 0x06001D15 RID: 7445 RVA: 0x00068412 File Offset: 0x00066612
		public void Dispose()
		{
			this._head = null;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0006841C File Offset: 0x0006661C
		private EventHandlerList.ListEntry Find(object key)
		{
			EventHandlerList.ListEntry listEntry = this._head;
			while (listEntry != null && listEntry._key != key)
			{
				listEntry = listEntry._next;
			}
			return listEntry;
		}

		/// <summary>Removes a delegate from the list.</summary>
		/// <param name="key">The object that owns the event. </param>
		/// <param name="value">The delegate to remove from the list. </param>
		// Token: 0x06001D17 RID: 7447 RVA: 0x00068448 File Offset: 0x00066648
		public void RemoveHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry._handler = Delegate.Remove(listEntry._handler, value);
			}
		}

		// Token: 0x04000EDA RID: 3802
		private EventHandlerList.ListEntry _head;

		// Token: 0x04000EDB RID: 3803
		private Component _parent;

		// Token: 0x02000370 RID: 880
		private sealed class ListEntry
		{
			// Token: 0x06001D18 RID: 7448 RVA: 0x00068472 File Offset: 0x00066672
			public ListEntry(object key, Delegate handler, EventHandlerList.ListEntry next)
			{
				this._next = next;
				this._key = key;
				this._handler = handler;
			}

			// Token: 0x04000EDC RID: 3804
			internal EventHandlerList.ListEntry _next;

			// Token: 0x04000EDD RID: 3805
			internal object _key;

			// Token: 0x04000EDE RID: 3806
			internal Delegate _handler;
		}
	}
}

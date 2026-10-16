using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a Windows menu or toolbar command item.</summary>
	// Token: 0x02000472 RID: 1138
	public class MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommand" /> class.</summary>
		/// <param name="handler">The event to raise when the user selects the menu item or toolbar button. </param>
		/// <param name="command">The unique command ID that links this menu command to the environment's menu. </param>
		// Token: 0x0600248F RID: 9359 RVA: 0x0008189A File Offset: 0x0007FA9A
		public MenuCommand(EventHandler handler, CommandID command)
		{
			this._execHandler = handler;
			this.CommandID = command;
			this._status = 3;
		}

		/// <summary>Gets or sets a value indicating whether this menu item is checked.</summary>
		/// <returns>true if the item is checked; otherwise, false.</returns>
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x000818B7 File Offset: 0x0007FAB7
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x000818C4 File Offset: 0x0007FAC4
		public virtual bool Checked
		{
			get
			{
				return (this._status & 4) != 0;
			}
			set
			{
				this.SetStatus(4, value);
			}
		}

		/// <summary>Gets a value indicating whether this menu item is available.</summary>
		/// <returns>true if the item is enabled; otherwise, false.</returns>
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x000818CE File Offset: 0x0007FACE
		// (set) Token: 0x06002493 RID: 9363 RVA: 0x000818DB File Offset: 0x0007FADB
		public virtual bool Enabled
		{
			get
			{
				return (this._status & 2) != 0;
			}
			set
			{
				this.SetStatus(2, value);
			}
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000818E8 File Offset: 0x0007FAE8
		private void SetStatus(int mask, bool value)
		{
			int num = this._status;
			if (value)
			{
				num |= mask;
			}
			else
			{
				num &= ~mask;
			}
			if (num != this._status)
			{
				this._status = num;
				this.OnCommandChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the public properties associated with the <see cref="T:System.ComponentModel.Design.MenuCommand" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the public properties of the <see cref="T:System.ComponentModel.Design.MenuCommand" />. </returns>
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x00081928 File Offset: 0x0007FB28
		public virtual IDictionary Properties
		{
			get
			{
				IDictionary dictionary;
				if ((dictionary = this._properties) == null)
				{
					dictionary = (this._properties = new HybridDictionary());
				}
				return dictionary;
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is supported.</summary>
		/// <returns>true if the item is supported, which is the default; otherwise, false.</returns>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x0008194D File Offset: 0x0007FB4D
		// (set) Token: 0x06002497 RID: 9367 RVA: 0x0008195A File Offset: 0x0007FB5A
		public virtual bool Supported
		{
			get
			{
				return (this._status & 1) != 0;
			}
			set
			{
				this.SetStatus(1, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is visible.</summary>
		/// <returns>true if the item is visible; otherwise, false.</returns>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x00081964 File Offset: 0x0007FB64
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x00081972 File Offset: 0x0007FB72
		public virtual bool Visible
		{
			get
			{
				return (this._status & 16) == 0;
			}
			set
			{
				this.SetStatus(16, !value);
			}
		}

		/// <summary>Occurs when the menu command changes.</summary>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x0600249A RID: 9370 RVA: 0x00081980 File Offset: 0x0007FB80
		// (remove) Token: 0x0600249B RID: 9371 RVA: 0x000819B8 File Offset: 0x0007FBB8
		public event EventHandler CommandChanged;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> associated with this menu command.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.CommandID" /> associated with the menu command.</returns>
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x000819ED File Offset: 0x0007FBED
		public virtual CommandID CommandID { get; }

		/// <summary>Invokes the command.</summary>
		// Token: 0x0600249D RID: 9373 RVA: 0x000819F8 File Offset: 0x0007FBF8
		public virtual void Invoke()
		{
			if (this._execHandler != null)
			{
				try
				{
					this._execHandler(this, EventArgs.Empty);
				}
				catch (CheckoutException ex)
				{
					if (ex != CheckoutException.Canceled)
					{
						throw;
					}
				}
			}
		}

		/// <summary>Invokes the command with the given parameter.</summary>
		/// <param name="arg">An optional argument for use by the command.</param>
		// Token: 0x0600249E RID: 9374 RVA: 0x00081A3C File Offset: 0x0007FC3C
		public virtual void Invoke(object arg)
		{
			this.Invoke();
		}

		/// <summary>Gets the OLE command status code for this menu item.</summary>
		/// <returns>An integer containing a mixture of status flags that reflect the state of this menu item.</returns>
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x00081A44 File Offset: 0x0007FC44
		public virtual int OleStatus
		{
			get
			{
				return this._status;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.MenuCommand.CommandChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060024A0 RID: 9376 RVA: 0x00081A4C File Offset: 0x0007FC4C
		protected virtual void OnCommandChanged(EventArgs e)
		{
			EventHandler commandChanged = this.CommandChanged;
			if (commandChanged == null)
			{
				return;
			}
			commandChanged(this, e);
		}

		/// <summary>Returns a string representation of this menu command.</summary>
		/// <returns>A string containing the value of the <see cref="P:System.ComponentModel.Design.MenuCommand.CommandID" /> property appended with the names of any flags that are set, separated by pipe bars (|). These flag properties include <see cref="P:System.ComponentModel.Design.MenuCommand.Checked" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Enabled" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Supported" />, and <see cref="P:System.ComponentModel.Design.MenuCommand.Visible" />.</returns>
		// Token: 0x060024A1 RID: 9377 RVA: 0x00081A60 File Offset: 0x0007FC60
		public override string ToString()
		{
			string text = this.CommandID.ToString() + " : ";
			if ((this._status & 1) != 0)
			{
				text += "Supported";
			}
			if ((this._status & 2) != 0)
			{
				text += "|Enabled";
			}
			if ((this._status & 16) == 0)
			{
				text += "|Visible";
			}
			if ((this._status & 4) != 0)
			{
				text += "|Checked";
			}
			return text;
		}

		// Token: 0x040010F3 RID: 4339
		private EventHandler _execHandler;

		// Token: 0x040010F4 RID: 4340
		private int _status;

		// Token: 0x040010F5 RID: 4341
		private IDictionary _properties;

		// Token: 0x040010F6 RID: 4342
		private const int ENABLED = 2;

		// Token: 0x040010F7 RID: 4343
		private const int INVISIBLE = 16;

		// Token: 0x040010F8 RID: 4344
		private const int CHECKED = 4;

		// Token: 0x040010F9 RID: 4345
		private const int SUPPORTED = 1;
	}
}

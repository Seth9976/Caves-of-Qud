using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemClick" /> and <see cref="E:System.Web.UI.WebControls.Menu.MenuItemDataBound" /> events of a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited. </summary>
	// Token: 0x020002E2 RID: 738
	public sealed class MenuEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> class using the specified menu item, command source, and event arguments.</summary>
		/// <param name="item">For the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemClick" /> event, this parameter represents the menu item clicked by the user. For the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemDataBound" /> event, this parameter represents the menu item being bound to data.</param>
		/// <param name="commandSource">The <see cref="T:System.Object" /> that raised the event.</param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the command name and command argument values for the menu item.</param>
		// Token: 0x06001B61 RID: 7009 RVA: 0x0004368E File Offset: 0x0004188E
		public MenuEventArgs(MenuItem item, object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this._item = item;
			this._commandSource = commandSource;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> class using the specified menu item.</summary>
		/// <param name="item">For the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemClick" /> event, this parameter represents the menu item clicked by the user. For the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemDataBound" /> event, this parameter represents the menu item being bound to data.</param>
		// Token: 0x06001B62 RID: 7010 RVA: 0x000436A5 File Offset: 0x000418A5
		public MenuEventArgs(MenuItem item)
			: this(item, null, new CommandEventArgs(string.Empty, null))
		{
		}

		/// <summary>Gets the <see cref="T:System.Object" /> that raised the event.</summary>
		/// <returns>The <see cref="T:System.Object" /> that raised the event.</returns>
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x000436BA File Offset: 0x000418BA
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		/// <summary>Gets the menu item associated with the event raised.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the menu item associated with the event raised.</returns>
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x000436C2 File Offset: 0x000418C2
		public MenuItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x04001716 RID: 5910
		private MenuItem _item;

		// Token: 0x04001717 RID: 5911
		private object _commandSource;
	}
}

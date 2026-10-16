using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the Command event.</summary>
	// Token: 0x02000283 RID: 643
	public class CommandEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> class with another <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> object.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06001A38 RID: 6712 RVA: 0x00043325 File Offset: 0x00041525
		public CommandEventArgs(CommandEventArgs e)
			: this(e.CommandName, e.CommandArgument)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> class with the specified command name and argument.</summary>
		/// <param name="commandName">The name of the command. </param>
		/// <param name="argument">A <see cref="T:System.Object" /> that contains the arguments for the command. </param>
		// Token: 0x06001A39 RID: 6713 RVA: 0x00043339 File Offset: 0x00041539
		public CommandEventArgs(string commandName, object argument)
		{
			this.commandName = commandName;
			this.argument = argument;
		}

		/// <summary>Gets the name of the command.</summary>
		/// <returns>The name of the command to perform.</returns>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x0004334F File Offset: 0x0004154F
		public string CommandName
		{
			get
			{
				return this.commandName;
			}
		}

		/// <summary>Gets the argument for the command.</summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the argument for the command.</returns>
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00043357 File Offset: 0x00041557
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x04001683 RID: 5763
		private string commandName;

		// Token: 0x04001684 RID: 5764
		private object argument;
	}
}

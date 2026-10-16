using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> event of a <see cref="T:System.Web.UI.WebControls.Repeater" />. This class cannot be inherited.</summary>
	// Token: 0x020002FB RID: 763
	public class RepeaterCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" /> class.</summary>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> that represents an item in the <see cref="T:System.Web.UI.WebControls.Repeater" />. The <see cref="P:System.Web.UI.WebControls.RepeaterCommandEventArgs.Item" /> property is set to this value. </param>
		/// <param name="commandSource">The command source. The <see cref="P:System.Web.UI.WebControls.RepeaterCommandEventArgs.CommandSource" /> property is set to this value. </param>
		/// <param name="originalArgs">The original event arguments. </param>
		// Token: 0x06001B9D RID: 7069 RVA: 0x000437E5 File Offset: 0x000419E5
		public RepeaterCommandEventArgs(RepeaterItem item, object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event.</returns>
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000437FC File Offset: 0x000419FC
		public RepeaterItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>The command source.</returns>
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00043804 File Offset: 0x00041A04
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x04001747 RID: 5959
		private RepeaterItem item;

		// Token: 0x04001748 RID: 5960
		private object commandSource;
	}
}

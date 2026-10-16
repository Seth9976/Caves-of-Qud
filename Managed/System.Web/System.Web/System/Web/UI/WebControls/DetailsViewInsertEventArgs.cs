using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemInserting" /> event.</summary>
	// Token: 0x02000385 RID: 901
	public class DetailsViewInsertEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewInsertEventArgs" /> class.</summary>
		/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
		// Token: 0x06002364 RID: 9060 RVA: 0x0005A350 File Offset: 0x00058550
		public DetailsViewInsertEventArgs(object commandArgument)
		{
			this.argument = commandArgument;
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0005A35F File Offset: 0x0005855F
		internal DetailsViewInsertEventArgs(object argument, IOrderedDictionary values)
		{
			this.argument = argument;
			this.values = values;
		}

		/// <summary>Gets the command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x0005A375 File Offset: 0x00058575
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets a dictionary that contains the field name/value pairs for the record to insert.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the record to insert.</returns>
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x0005A37D File Offset: 0x0005857D
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04001975 RID: 6517
		private object argument;

		// Token: 0x04001976 RID: 6518
		private IOrderedDictionary values;
	}
}

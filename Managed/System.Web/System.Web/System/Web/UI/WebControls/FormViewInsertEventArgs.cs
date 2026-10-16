using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemInserting" /> event.</summary>
	// Token: 0x0200039A RID: 922
	public class FormViewInsertEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewInsertEventArgs" /> class.</summary>
		/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
		// Token: 0x060024D5 RID: 9429 RVA: 0x0005E001 File Offset: 0x0005C201
		public FormViewInsertEventArgs(object commandArgument)
		{
			this.argument = commandArgument;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0005E010 File Offset: 0x0005C210
		internal FormViewInsertEventArgs(object argument, IOrderedDictionary values)
		{
			this.values = values;
			this.argument = argument;
		}

		/// <summary>Gets the command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x0005E026 File Offset: 0x0005C226
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets a dictionary that contains the field name/value pairs for the record to insert.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the record to insert.</returns>
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x0005E02E File Offset: 0x0005C22E
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040019DB RID: 6619
		private object argument;

		// Token: 0x040019DC RID: 6620
		private IOrderedDictionary values;
	}
}

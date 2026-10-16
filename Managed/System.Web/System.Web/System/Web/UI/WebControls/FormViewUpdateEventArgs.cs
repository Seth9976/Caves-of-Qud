using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemUpdating" /> event.</summary>
	// Token: 0x0200039E RID: 926
	public class FormViewUpdateEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewUpdateEventArgs" /> class.</summary>
		/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
		// Token: 0x060024EB RID: 9451 RVA: 0x0005E18C File Offset: 0x0005C38C
		public FormViewUpdateEventArgs(object commandArgument)
		{
			this.argument = commandArgument;
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0005E19B File Offset: 0x0005C39B
		internal FormViewUpdateEventArgs(object argument, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
			: this(argument)
		{
			this.keys = keys;
			this.oldValues = oldValues;
			this.newValues = newValues;
		}

		/// <summary>Gets the command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x0005E1BA File Offset: 0x0005C3BA
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets a dictionary that contains the original key field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original key field name/value pairs for the record to update.</returns>
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x0005E1C2 File Offset: 0x0005C3C2
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary that contains the new field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the record to update.</returns>
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x0005E1CA File Offset: 0x0005C3CA
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary that contains the original non-key field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original non-key field name/value pairs for the record to update.</returns>
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x0005E1D2 File Offset: 0x0005C3D2
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x040019E6 RID: 6630
		private object argument;

		// Token: 0x040019E7 RID: 6631
		private IOrderedDictionary keys;

		// Token: 0x040019E8 RID: 6632
		private IOrderedDictionary oldValues;

		// Token: 0x040019E9 RID: 6633
		private IOrderedDictionary newValues;
	}
}

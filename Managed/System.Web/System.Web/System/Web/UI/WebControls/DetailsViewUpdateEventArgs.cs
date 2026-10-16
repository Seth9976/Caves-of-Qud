using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemUpdating" /> event.</summary>
	// Token: 0x0200038A RID: 906
	public class DetailsViewUpdateEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewUpdateEventArgs" /> class.</summary>
		/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
		// Token: 0x06002382 RID: 9090 RVA: 0x0005A4D7 File Offset: 0x000586D7
		public DetailsViewUpdateEventArgs(object commandArgument)
		{
			this.argument = commandArgument;
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x0005A4E6 File Offset: 0x000586E6
		internal DetailsViewUpdateEventArgs(object argument, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
		{
			this.argument = argument;
			this.keys = keys;
			this.newValues = newValues;
			this.oldValues = oldValues;
		}

		/// <summary>Gets the command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x0005A50B File Offset: 0x0005870B
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets a dictionary that contains the key field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the record to update.</returns>
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x0005A513 File Offset: 0x00058713
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary that contains the new field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the record to update.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x0005A51B File Offset: 0x0005871B
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary that contains the original field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original field name/value pairs for the record to update.</returns>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x0005A523 File Offset: 0x00058723
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x04001981 RID: 6529
		private object argument;

		// Token: 0x04001982 RID: 6530
		private IOrderedDictionary keys;

		// Token: 0x04001983 RID: 6531
		private IOrderedDictionary newValues;

		// Token: 0x04001984 RID: 6532
		private IOrderedDictionary oldValues;
	}
}

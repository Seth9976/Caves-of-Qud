using System;

namespace System.ComponentModel
{
	/// <summary>Provides a description of the sort operation applied to a data source.</summary>
	// Token: 0x020003D3 RID: 979
	public class ListSortDescription
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescription" /> class with the specified property description and direction.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the property by which the data source is sorted.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDescription" />  values.</param>
		// Token: 0x06001F87 RID: 8071 RVA: 0x0006D5B4 File Offset: 0x0006B7B4
		public ListSortDescription(PropertyDescriptor property, ListSortDirection direction)
		{
			this.PropertyDescriptor = property;
			this.SortDirection = direction;
		}

		/// <summary>Gets or sets the abstract description of a class property associated with this <see cref="T:System.ComponentModel.ListSortDescription" /></summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with this <see cref="T:System.ComponentModel.ListSortDescription" />. </returns>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x0006D5CA File Offset: 0x0006B7CA
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x0006D5D2 File Offset: 0x0006B7D2
		public PropertyDescriptor PropertyDescriptor { get; set; }

		/// <summary>Gets or sets the direction of the sort operation associated with this <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x0006D5DB File Offset: 0x0006B7DB
		// (set) Token: 0x06001F8B RID: 8075 RVA: 0x0006D5E3 File Offset: 0x0006B7E3
		public ListSortDirection SortDirection { get; set; }
	}
}

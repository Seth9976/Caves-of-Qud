using System;

namespace System.Web.Compilation
{
	/// <summary>Contains fields that identify an implicit resource key.</summary>
	// Token: 0x02000605 RID: 1541
	public sealed class ImplicitResourceKey
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ImplicitResourceKey" /> class. </summary>
		// Token: 0x06004273 RID: 17011 RVA: 0x00002050 File Offset: 0x00000250
		public ImplicitResourceKey()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ImplicitResourceKey" /> class with the specified values for the <see cref="P:System.Web.Compilation.ImplicitResourceKey.Filter" />, <see cref="P:System.Web.Compilation.ImplicitResourceKey.KeyPrefix" /> and <see cref="P:System.Web.Compilation.ImplicitResourceKey.Property" /> properties.</summary>
		/// <param name="filter">The filter value of an implicit resource key.</param>
		/// <param name="keyPrefix">The prefix for identifying a group of properties.</param>
		/// <param name="property">A property and subproperty, if provided, for an implicit resource key.</param>
		// Token: 0x06004274 RID: 17012 RVA: 0x000AD1FA File Offset: 0x000AB3FA
		public ImplicitResourceKey(string filter, string keyPrefix, string property)
		{
			this._filter = filter;
			this._keyPrefix = keyPrefix;
			this._property = property;
		}

		/// <summary>Gets or sets the filter value of an implicit resource key.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the filter value for the implicit resource expression.</returns>
		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x000AD217 File Offset: 0x000AB417
		// (set) Token: 0x06004276 RID: 17014 RVA: 0x000AD21F File Offset: 0x000AB41F
		public string Filter
		{
			get
			{
				return this._filter;
			}
			set
			{
				this._filter = value;
			}
		}

		/// <summary>Gets or sets the prefix for identifying a group of properties.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the prefix for an implicit resource expression.</returns>
		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x000AD228 File Offset: 0x000AB428
		// (set) Token: 0x06004278 RID: 17016 RVA: 0x000AD230 File Offset: 0x000AB430
		public string KeyPrefix
		{
			get
			{
				return this._keyPrefix;
			}
			set
			{
				this._keyPrefix = value;
			}
		}

		/// <summary>Gets or sets a property and subproperty, if provided, for an implicit resource key.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the property and subproperty for an implicit resource expression.</returns>
		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x06004279 RID: 17017 RVA: 0x000AD239 File Offset: 0x000AB439
		// (set) Token: 0x0600427A RID: 17018 RVA: 0x000AD241 File Offset: 0x000AB441
		public string Property
		{
			get
			{
				return this._property;
			}
			set
			{
				this._property = value;
			}
		}

		// Token: 0x040023AF RID: 9135
		private string _filter;

		// Token: 0x040023B0 RID: 9136
		private string _keyPrefix;

		// Token: 0x040023B1 RID: 9137
		private string _property;
	}
}

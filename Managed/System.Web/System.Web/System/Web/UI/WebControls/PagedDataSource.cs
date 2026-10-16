using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates the paging-related properties of a data-bound control (such as <see cref="T:System.Web.UI.WebControls.DataGrid" />, <see cref="T:System.Web.UI.WebControls.GridView" />, <see cref="T:System.Web.UI.WebControls.DetailsView" />, and <see cref="T:System.Web.UI.WebControls.FormView" />) that allow it to perform paging. This class cannot be inherited.</summary>
	// Token: 0x020003DB RID: 987
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PagedDataSource : ICollection, IEnumerable, ITypedList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PagedDataSource" /> class.</summary>
		// Token: 0x06002B2C RID: 11052 RVA: 0x00070AC7 File Offset: 0x0006ECC7
		public PagedDataSource()
		{
			this.page_size = 10;
		}

		/// <summary>Gets or sets a value indicating whether custom paging is enabled in a data-bound control.</summary>
		/// <returns>true if custom paging is enabled; otherwise, false.</returns>
		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x00070AD7 File Offset: 0x0006ECD7
		// (set) Token: 0x06002B2E RID: 11054 RVA: 0x00070ADF File Offset: 0x0006ECDF
		public bool AllowCustomPaging
		{
			get
			{
				return this.allow_custom_paging;
			}
			set
			{
				this.allow_custom_paging = value;
				if (this.allow_custom_paging)
				{
					this.allow_server_paging = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether paging is enabled in a data-bound control.</summary>
		/// <returns>true if paging is enabled; otherwise, false.</returns>
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x00070AF7 File Offset: 0x0006ECF7
		// (set) Token: 0x06002B30 RID: 11056 RVA: 0x00070AFF File Offset: 0x0006ECFF
		public bool AllowPaging
		{
			get
			{
				return this.allow_paging;
			}
			set
			{
				this.allow_paging = value;
			}
		}

		/// <summary>Gets the number of items to be used from the data source.</summary>
		/// <returns>The number of items to be used from the data source.</returns>
		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x00070B08 File Offset: 0x0006ED08
		public int Count
		{
			get
			{
				if (this.source == null)
				{
					return 0;
				}
				if (!this.IsPagingEnabled)
				{
					return this.DataSourceCount;
				}
				if (this.IsCustomPagingEnabled || !this.IsLastPage)
				{
					return this.page_size;
				}
				return this.DataSourceCount - this.FirstIndexInPage;
			}
		}

		/// <summary>Gets or sets the index of the current page.</summary>
		/// <returns>The index of the current page.</returns>
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x00070B47 File Offset: 0x0006ED47
		// (set) Token: 0x06002B33 RID: 11059 RVA: 0x00070B4F File Offset: 0x0006ED4F
		public int CurrentPageIndex
		{
			get
			{
				return this.current_page_index;
			}
			set
			{
				this.current_page_index = value;
			}
		}

		/// <summary>Gets or sets the data source.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerable" /> implemented object that represents the data source.</returns>
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x00070B58 File Offset: 0x0006ED58
		// (set) Token: 0x06002B35 RID: 11061 RVA: 0x00070B60 File Offset: 0x0006ED60
		public IEnumerable DataSource
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		/// <summary>Gets the number of items in the data source.</summary>
		/// <returns>The number of items in the data source.</returns>
		/// <exception cref="T:System.Web.HttpException">The data source is not an <see cref="T:System.Collections.ICollection" /> implemented object.</exception>
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x00070B6C File Offset: 0x0006ED6C
		public int DataSourceCount
		{
			get
			{
				if (this.source == null)
				{
					return 0;
				}
				if (this.IsCustomPagingEnabled || this.IsServerPagingEnabled)
				{
					return this.virtual_count;
				}
				if (this.source is ICollection)
				{
					return ((ICollection)this.source).Count;
				}
				throw new HttpException("The data source must implement ICollection");
			}
		}

		/// <summary>Gets the index of the first record displayed on the page.</summary>
		/// <returns>The index of the first record displayed on the page.</returns>
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x00070BC2 File Offset: 0x0006EDC2
		public int FirstIndexInPage
		{
			get
			{
				if (!this.IsPagingEnabled || this.IsCustomPagingEnabled || this.IsServerPagingEnabled || this.source == null)
				{
					return 0;
				}
				return this.current_page_index * this.page_size;
			}
		}

		/// <summary>Gets a value indicating whether custom paging is enabled.</summary>
		/// <returns>true if custom paging is enabled; otherwise, false.</returns>
		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x00070BF3 File Offset: 0x0006EDF3
		public bool IsCustomPagingEnabled
		{
			get
			{
				return this.IsPagingEnabled && this.allow_custom_paging;
			}
		}

		/// <summary>Gets a value indicating whether server-side paging support is enabled.</summary>
		/// <returns>true if paging is enabled and server-side paging is indicated using the <see cref="P:System.Web.UI.WebControls.PagedDataSource.AllowServerPaging" /> property; otherwise, false.</returns>
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x00070C05 File Offset: 0x0006EE05
		public bool IsServerPagingEnabled
		{
			get
			{
				return this.IsPagingEnabled && this.allow_server_paging;
			}
		}

		/// <summary>Gets a value indicating whether the current page is the first page.</summary>
		/// <returns>true if the current page is the first page; otherwise, false.</returns>
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x00070C17 File Offset: 0x0006EE17
		public bool IsFirstPage
		{
			get
			{
				return !this.allow_paging || this.current_page_index == 0;
			}
		}

		/// <summary>Gets a value indicating whether the current page is the last page.</summary>
		/// <returns>true if the current page is the last page; otherwise, false.</returns>
		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x00070C2C File Offset: 0x0006EE2C
		public bool IsLastPage
		{
			get
			{
				return !this.allow_paging || this.page_size == 0 || this.current_page_index == this.PageCount - 1;
			}
		}

		/// <summary>Gets a value indicating whether paging is enabled.</summary>
		/// <returns>true if paging is enabled; otherwise, false.</returns>
		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x00070C50 File Offset: 0x0006EE50
		public bool IsPagingEnabled
		{
			get
			{
				return this.allow_paging && this.page_size != 0;
			}
		}

		/// <summary>Gets a value indicating whether the data source is read-only.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the data source is synchronized (thread-safe).</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the total number of pages necessary to display all items in the data source.</summary>
		/// <returns>The number of pages necessary to display all items in the data source.</returns>
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x00070C65 File Offset: 0x0006EE65
		public int PageCount
		{
			get
			{
				if (this.source == null)
				{
					return 0;
				}
				if (!this.IsPagingEnabled || this.DataSourceCount == 0 || this.page_size == 0)
				{
					return 1;
				}
				return (this.DataSourceCount + this.page_size - 1) / this.page_size;
			}
		}

		/// <summary>Gets or sets the number of items to display on a single page.</summary>
		/// <returns>The number of items to display on a single page.</returns>
		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x00070CA1 File Offset: 0x0006EEA1
		// (set) Token: 0x06002B41 RID: 11073 RVA: 0x00070CA9 File Offset: 0x0006EEA9
		public int PageSize
		{
			get
			{
				return this.page_size;
			}
			set
			{
				this.page_size = value;
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets or sets the virtual number of items in the data source when custom paging is used.</summary>
		/// <returns>The virtual number of items in the data source when custom paging is used.</returns>
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x00070CB2 File Offset: 0x0006EEB2
		// (set) Token: 0x06002B44 RID: 11076 RVA: 0x00070CBA File Offset: 0x0006EEBA
		public int VirtualCount
		{
			get
			{
				return this.virtual_count;
			}
			set
			{
				this.virtual_count = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether server-side paging is enabled.</summary>
		/// <returns>true if server-side paging is enabled; otherwise, false.</returns>
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x00070CC3 File Offset: 0x0006EEC3
		// (set) Token: 0x06002B46 RID: 11078 RVA: 0x00070CCB File Offset: 0x0006EECB
		public bool AllowServerPaging
		{
			get
			{
				return this.allow_server_paging;
			}
			set
			{
				this.allow_server_paging = value;
				if (this.allow_server_paging)
				{
					this.allow_custom_paging = false;
				}
			}
		}

		/// <summary>Copies all the items from the data source to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the data source. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive the copied contents. </param>
		// Token: 0x06002B47 RID: 11079 RVA: 0x00070CE4 File Offset: 0x0006EEE4
		public void CopyTo(Array array, int index)
		{
			foreach (object obj in this.source)
			{
				array.SetValue(obj, index++);
			}
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all items in the data source.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all items in the data source.</returns>
		// Token: 0x06002B48 RID: 11080 RVA: 0x00070D40 File Offset: 0x0006EF40
		public IEnumerator GetEnumerator()
		{
			IList list = this.source as IList;
			if (list != null)
			{
				int num = this.FirstIndexInPage;
				int num2 = ((ICollection)this.source).Count;
				int num3 = ((num + this.page_size > num2) ? (num2 - num) : this.page_size);
				return this.GetListEnum(list, num, num + num3);
			}
			ICollection collection = this.source as ICollection;
			if (collection != null)
			{
				int num = this.FirstIndexInPage;
				int num2 = collection.Count;
				int num4 = ((num + this.page_size > num2) ? (num2 - num) : this.page_size);
				return this.GetEnumeratorEnum(collection.GetEnumerator(), num, num + this.page_size);
			}
			return this.source.GetEnumerator();
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that contains the list name returned. This can be null.</param>
		// Token: 0x06002B49 RID: 11081 RVA: 0x00070DF4 File Offset: 0x0006EFF4
		public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			ITypedList typedList = this.source as ITypedList;
			if (typedList == null)
			{
				return null;
			}
			return typedList.GetItemProperties(listAccessors);
		}

		/// <summary>Returns the name of the list. This method does not apply to this class.</summary>
		/// <returns>
		///   <see cref="F:System.String.Empty" /> for all cases.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that contains the list name returned. This can be null. </param>
		// Token: 0x06002B4A RID: 11082 RVA: 0x0000EE63 File Offset: 0x0000D063
		public string GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x00070E19 File Offset: 0x0006F019
		private IEnumerator GetListEnum(IList list, int start, int end)
		{
			if (!this.AllowPaging)
			{
				end = list.Count;
			}
			else if (start >= list.Count)
			{
				yield break;
			}
			int num;
			for (int i = start; i < end; i = num + 1)
			{
				yield return list[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x00070E3D File Offset: 0x0006F03D
		private IEnumerator GetEnumeratorEnum(IEnumerator e, int start, int end)
		{
			for (int j = 0; j < start; j++)
			{
				e.MoveNext();
			}
			int i = start;
			while ((!this.allow_paging || i < end) && e.MoveNext())
			{
				yield return e.Current;
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x04001B0C RID: 6924
		private int page_size;

		// Token: 0x04001B0D RID: 6925
		private int current_page_index;

		// Token: 0x04001B0E RID: 6926
		private int virtual_count;

		// Token: 0x04001B0F RID: 6927
		private bool allow_paging;

		// Token: 0x04001B10 RID: 6928
		private bool allow_custom_paging;

		// Token: 0x04001B11 RID: 6929
		private IEnumerable source;

		// Token: 0x04001B12 RID: 6930
		private bool allow_server_paging;
	}
}

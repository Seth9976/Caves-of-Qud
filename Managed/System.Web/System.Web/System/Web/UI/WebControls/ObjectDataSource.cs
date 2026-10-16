using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a business object that provides data to data-bound controls in multitier Web application architectures.</summary>
	// Token: 0x020003D9 RID: 985
	[ToolboxBitmap("bitmap file goes here")]
	[DefaultEvent("Selecting")]
	[DefaultProperty("TypeName")]
	[Designer("System.Web.UI.Design.WebControls.ObjectDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ObjectDataSource : DataSourceControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> class.</summary>
		// Token: 0x06002A60 RID: 10848 RVA: 0x0006E77C File Offset: 0x0006C97C
		public ObjectDataSource()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> class with the specified type name and data retrieval method name.</summary>
		/// <param name="typeName">The name of the class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> works with. </param>
		/// <param name="selectMethod">The name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> invokes to retrieve data. </param>
		// Token: 0x06002A61 RID: 10849 RVA: 0x0006E784 File Offset: 0x0006C984
		public ObjectDataSource(string typeName, string selectMethod)
		{
			this.SelectMethod = selectMethod;
			this.TypeName = typeName;
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x0006E79A File Offset: 0x0006C99A
		private ObjectDataSourceView DefaultView
		{
			get
			{
				if (this.defaultView == null)
				{
					this.defaultView = new ObjectDataSourceView(this, ObjectDataSource.emptyNames[0], this.Context);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.defaultView).TrackViewState();
					}
				}
				return this.defaultView;
			}
		}

		/// <summary>Occurs when a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation has completed.</summary>
		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06002A63 RID: 10851 RVA: 0x0006E7D6 File Offset: 0x0006C9D6
		// (remove) Token: 0x06002A64 RID: 10852 RVA: 0x0006E7E4 File Offset: 0x0006C9E4
		public event ObjectDataSourceStatusEventHandler Deleted
		{
			add
			{
				this.DefaultView.Deleted += value;
			}
			remove
			{
				this.DefaultView.Deleted -= value;
			}
		}

		/// <summary>Occurs before a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation.</summary>
		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06002A65 RID: 10853 RVA: 0x0006E7F2 File Offset: 0x0006C9F2
		// (remove) Token: 0x06002A66 RID: 10854 RVA: 0x0006E800 File Offset: 0x0006CA00
		public event ObjectDataSourceMethodEventHandler Deleting
		{
			add
			{
				this.DefaultView.Deleting += value;
			}
			remove
			{
				this.DefaultView.Deleting -= value;
			}
		}

		/// <summary>Occurs before a filter operation.</summary>
		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x06002A67 RID: 10855 RVA: 0x0006E80E File Offset: 0x0006CA0E
		// (remove) Token: 0x06002A68 RID: 10856 RVA: 0x0006E81C File Offset: 0x0006CA1C
		public event ObjectDataSourceFilteringEventHandler Filtering
		{
			add
			{
				this.DefaultView.Filtering += value;
			}
			remove
			{
				this.DefaultView.Filtering -= value;
			}
		}

		/// <summary>Occurs when an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" /> operation has completed.</summary>
		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x06002A69 RID: 10857 RVA: 0x0006E82A File Offset: 0x0006CA2A
		// (remove) Token: 0x06002A6A RID: 10858 RVA: 0x0006E838 File Offset: 0x0006CA38
		public event ObjectDataSourceStatusEventHandler Inserted
		{
			add
			{
				this.DefaultView.Inserted += value;
			}
			remove
			{
				this.DefaultView.Inserted -= value;
			}
		}

		/// <summary>Occurs before an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" /> operation.</summary>
		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x06002A6B RID: 10859 RVA: 0x0006E846 File Offset: 0x0006CA46
		// (remove) Token: 0x06002A6C RID: 10860 RVA: 0x0006E854 File Offset: 0x0006CA54
		public event ObjectDataSourceMethodEventHandler Inserting
		{
			add
			{
				this.DefaultView.Inserting += value;
			}
			remove
			{
				this.DefaultView.Inserting -= value;
			}
		}

		/// <summary>Occurs after the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.TypeName" /> property is created.</summary>
		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x06002A6D RID: 10861 RVA: 0x0006E862 File Offset: 0x0006CA62
		// (remove) Token: 0x06002A6E RID: 10862 RVA: 0x0006E870 File Offset: 0x0006CA70
		public event ObjectDataSourceObjectEventHandler ObjectCreated
		{
			add
			{
				this.DefaultView.ObjectCreated += value;
			}
			remove
			{
				this.DefaultView.ObjectCreated -= value;
			}
		}

		/// <summary>Occurs before the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.TypeName" /> property is created.</summary>
		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06002A6F RID: 10863 RVA: 0x0006E87E File Offset: 0x0006CA7E
		// (remove) Token: 0x06002A70 RID: 10864 RVA: 0x0006E88C File Offset: 0x0006CA8C
		public event ObjectDataSourceObjectEventHandler ObjectCreating
		{
			add
			{
				this.DefaultView.ObjectCreating += value;
			}
			remove
			{
				this.DefaultView.ObjectCreating -= value;
			}
		}

		/// <summary>Occurs before the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.TypeName" /> property is discarded.</summary>
		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06002A71 RID: 10865 RVA: 0x0006E89A File Offset: 0x0006CA9A
		// (remove) Token: 0x06002A72 RID: 10866 RVA: 0x0006E8A8 File Offset: 0x0006CAA8
		public event ObjectDataSourceDisposingEventHandler ObjectDisposing
		{
			add
			{
				this.DefaultView.ObjectDisposing += value;
			}
			remove
			{
				this.DefaultView.ObjectDisposing -= value;
			}
		}

		/// <summary>Occurs when a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> operation has completed.</summary>
		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06002A73 RID: 10867 RVA: 0x0006E8B6 File Offset: 0x0006CAB6
		// (remove) Token: 0x06002A74 RID: 10868 RVA: 0x0006E8C4 File Offset: 0x0006CAC4
		public event ObjectDataSourceStatusEventHandler Selected
		{
			add
			{
				this.DefaultView.Selected += value;
			}
			remove
			{
				this.DefaultView.Selected -= value;
			}
		}

		/// <summary>Occurs before a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> operation.</summary>
		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06002A75 RID: 10869 RVA: 0x0006E8D2 File Offset: 0x0006CAD2
		// (remove) Token: 0x06002A76 RID: 10870 RVA: 0x0006E8E0 File Offset: 0x0006CAE0
		public event ObjectDataSourceSelectingEventHandler Selecting
		{
			add
			{
				this.DefaultView.Selecting += value;
			}
			remove
			{
				this.DefaultView.Selecting -= value;
			}
		}

		/// <summary>Occurs when an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" /> operation has completed.</summary>
		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06002A77 RID: 10871 RVA: 0x0006E8EE File Offset: 0x0006CAEE
		// (remove) Token: 0x06002A78 RID: 10872 RVA: 0x0006E8FC File Offset: 0x0006CAFC
		public event ObjectDataSourceStatusEventHandler Updated
		{
			add
			{
				this.DefaultView.Updated += value;
			}
			remove
			{
				this.DefaultView.Updated -= value;
			}
		}

		/// <summary>Occurs before an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" /> operation.</summary>
		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06002A79 RID: 10873 RVA: 0x0006E90A File Offset: 0x0006CB0A
		// (remove) Token: 0x06002A7A RID: 10874 RVA: 0x0006E918 File Offset: 0x0006CB18
		public event ObjectDataSourceMethodEventHandler Updating
		{
			add
			{
				this.DefaultView.Updating += value;
			}
			remove
			{
				this.DefaultView.Updating -= value;
			}
		}

		/// <summary>Gets or sets the length of time, in seconds, that the data source control caches data that is retrieved by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</summary>
		/// <returns>The number of seconds that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> caches the results of a <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property invocation. The default is 0. The value cannot be negative.</returns>
		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x0006E926 File Offset: 0x0006CB26
		// (set) Token: 0x06002A7C RID: 10876 RVA: 0x0006E92E File Offset: 0x0006CB2E
		[DefaultValue(0)]
		[TypeConverter(typeof(DataSourceCacheDurationConverter))]
		public virtual int CacheDuration
		{
			get
			{
				return this.cacheDuration;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "The duration must be non-negative");
				}
				this.cacheDuration = value;
			}
		}

		/// <summary>Gets or sets the cache expiration behavior that, when combined with the duration, describes the behavior of the cache that the data source control uses.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.DataSourceCacheExpiry" /> values. The default is <see cref="F:System.Web.UI.DataSourceCacheExpiry.Absolute" />.</returns>
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x0006E94B File Offset: 0x0006CB4B
		// (set) Token: 0x06002A7E RID: 10878 RVA: 0x0006E953 File Offset: 0x0006CB53
		[DefaultValue(DataSourceCacheExpiry.Absolute)]
		public virtual DataSourceCacheExpiry CacheExpirationPolicy
		{
			get
			{
				return this.cacheExpirationPolicy;
			}
			set
			{
				this.cacheExpirationPolicy = value;
			}
		}

		/// <summary>Gets or sets a user-defined key dependency that is linked to all data cache objects that are created by the data source control.</summary>
		/// <returns>A key that identifies all cache objects created by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />.</returns>
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x0006E95C File Offset: 0x0006CB5C
		// (set) Token: 0x06002A80 RID: 10880 RVA: 0x0006E972 File Offset: 0x0006CB72
		[DefaultValue("")]
		public virtual string CacheKeyDependency
		{
			get
			{
				if (this.cacheKeyDependency == null)
				{
					return string.Empty;
				}
				return this.cacheKeyDependency;
			}
			set
			{
				this.cacheKeyDependency = value;
			}
		}

		/// <summary>Gets or sets a value that determines whether or not just the new values are passed to the Update method or both the old and new values are passed to the Update method.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.ConflictOptions" /> values. The default is <see cref="F:System.Web.UI.ConflictOptions.OverwriteChanges" />.</returns>
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x0006E97B File Offset: 0x0006CB7B
		// (set) Token: 0x06002A82 RID: 10882 RVA: 0x0006E988 File Offset: 0x0006CB88
		[WebCategory("Data")]
		[DefaultValue(ConflictOptions.OverwriteChanges)]
		public ConflictOptions ConflictDetection
		{
			get
			{
				return this.DefaultView.ConflictDetection;
			}
			set
			{
				this.DefaultView.ConflictDetection = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Parameter" /> values that are passed to an update, insert, or delete operation are automatically converted from null to the <see cref="F:System.DBNull.Value" /> value by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
		/// <returns>true, if any null values in <see cref="T:System.Web.UI.WebControls.Parameter" /> objects passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control are automatically converted to <see cref="F:System.DBNull.Value" /> values; otherwise, false. The default is false.</returns>
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x0006E996 File Offset: 0x0006CB96
		// (set) Token: 0x06002A84 RID: 10884 RVA: 0x0006E9A3 File Offset: 0x0006CBA3
		[DefaultValue(false)]
		public bool ConvertNullToDBNull
		{
			get
			{
				return this.DefaultView.ConvertNullToDBNull;
			}
			set
			{
				this.DefaultView.ConvertNullToDBNull = value;
			}
		}

		/// <summary>Gets or sets the name of a class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control uses for a parameter in an update, insert, or delete data operation, instead of passing individual values from the data-bound control.</summary>
		/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> can use as a parameter for an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" />, <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" />, or a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation. The default is an empty string ("").</returns>
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x0006E9B1 File Offset: 0x0006CBB1
		// (set) Token: 0x06002A86 RID: 10886 RVA: 0x0006E9BE File Offset: 0x0006CBBE
		[WebCategory("Data")]
		[DefaultValue("")]
		public string DataObjectTypeName
		{
			get
			{
				return this.DefaultView.DataObjectTypeName;
			}
			set
			{
				this.DefaultView.DataObjectTypeName = value;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to delete data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to delete data. The default is an empty string ("").</returns>
		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x0006E9CC File Offset: 0x0006CBCC
		// (set) Token: 0x06002A88 RID: 10888 RVA: 0x0006E9D9 File Offset: 0x0006CBD9
		[DefaultValue("")]
		[WebCategory("Data")]
		public string DeleteMethod
		{
			get
			{
				return this.DefaultView.DeleteMethod;
			}
			set
			{
				this.DefaultView.DeleteMethod = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteMethod" /> method.</returns>
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x0006E9E7 File Offset: 0x0006CBE7
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[DefaultValue(null)]
		public ParameterCollection DeleteParameters
		{
			get
			{
				return this.DefaultView.DeleteParameters;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control has data caching enabled.</summary>
		/// <returns>true if data caching is enabled for the data source control; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSource.EnableCaching" /> property is set to true when the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property returns a <see cref="T:System.Data.Common.DbDataReader" />.</exception>
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x0006E9F4 File Offset: 0x0006CBF4
		// (set) Token: 0x06002A8B RID: 10891 RVA: 0x0006E9FC File Offset: 0x0006CBFC
		[DefaultValue(false)]
		public virtual bool EnableCaching
		{
			get
			{
				return this.enableCaching;
			}
			set
			{
				this.enableCaching = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the data source control supports paging through the set of data that it retrieves.</summary>
		/// <returns>true if the data source control supports paging through the data it retrieves; otherwise, false.</returns>
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x0006EA05 File Offset: 0x0006CC05
		// (set) Token: 0x06002A8D RID: 10893 RVA: 0x0006EA12 File Offset: 0x0006CC12
		[DefaultValue(false)]
		[WebCategory("Paging")]
		public bool EnablePaging
		{
			get
			{
				return this.DefaultView.EnablePaging;
			}
			set
			{
				this.DefaultView.EnablePaging = value;
			}
		}

		/// <summary>Gets or sets a filtering expression that is applied when the method that is specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property is called.</summary>
		/// <returns>A string that represents a filtering expression that is applied when data is retrieved by using the method or function identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> property was set and the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> method does not return a <see cref="T:System.Data.DataSet" /> or <see cref="T:System.Data.DataTable" />. </exception>
		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x0006EA20 File Offset: 0x0006CC20
		// (set) Token: 0x06002A8F RID: 10895 RVA: 0x0006EA2D File Offset: 0x0006CC2D
		[DefaultValue("")]
		[WebCategory("Data")]
		public string FilterExpression
		{
			get
			{
				return this.DefaultView.FilterExpression;
			}
			set
			{
				this.DefaultView.FilterExpression = value;
			}
		}

		/// <summary>Gets a collection of parameters that are associated with any parameter placeholders in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> string.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> property.</returns>
		/// <exception cref="T:System.NotSupportedException">You set the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> property and the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> method does not return a <see cref="T:System.Data.DataSet" /> or <see cref="T:System.Data.DataTable" />. </exception>
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x0006EA3B File Offset: 0x0006CC3B
		[MergableProperty(false)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[WebCategory("Data")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ParameterCollection FilterParameters
		{
			get
			{
				return this.DefaultView.FilterParameters;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to insert data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to insert data. The default is an empty string ("").</returns>
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x0006EA48 File Offset: 0x0006CC48
		// (set) Token: 0x06002A92 RID: 10898 RVA: 0x0006EA55 File Offset: 0x0006CC55
		[DefaultValue("")]
		[WebCategory("Data")]
		public string InsertMethod
		{
			get
			{
				return this.DefaultView.InsertMethod;
			}
			set
			{
				this.DefaultView.InsertMethod = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertMethod" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertMethod" /> property.</returns>
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x0006EA63 File Offset: 0x0006CC63
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Data")]
		[MergableProperty(false)]
		public ParameterCollection InsertParameters
		{
			get
			{
				return this.DefaultView.InsertParameters;
			}
		}

		/// <summary>Gets or sets the name of the business object data retrieval method parameter that is used to indicate the number of records to retrieve for data source paging support.</summary>
		/// <returns>The name of the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> parameter that is used to indicate the number of records to retrieve. The default is "maximumRows".</returns>
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x0006EA70 File Offset: 0x0006CC70
		// (set) Token: 0x06002A95 RID: 10901 RVA: 0x0006EA7D File Offset: 0x0006CC7D
		[WebCategory("Paging")]
		[DefaultValue("maximumRows")]
		public string MaximumRowsParameterName
		{
			get
			{
				return this.DefaultView.MaximumRowsParameterName;
			}
			set
			{
				this.DefaultView.MaximumRowsParameterName = value;
			}
		}

		/// <summary>Gets or sets a format string to apply to the names of the parameters for original values that are passed to the Delete or Update methods.</summary>
		/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> or key parameters passed to the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> or <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" /> methods. The default is "{0}", which means the parameter name is the field name.</returns>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x0006EA8B File Offset: 0x0006CC8B
		// (set) Token: 0x06002A97 RID: 10903 RVA: 0x0006EA98 File Offset: 0x0006CC98
		[WebCategory("Data")]
		[DefaultValue("{0}")]
		public string OldValuesParameterFormatString
		{
			get
			{
				return this.DefaultView.OldValuesParameterFormatString;
			}
			set
			{
				this.DefaultView.OldValuesParameterFormatString = value;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to retrieve a row count.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to retrieve a row count. The method must return an integer (<see cref="T:System.Int32" />). The default is an empty string ("").</returns>
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x0006EAA6 File Offset: 0x0006CCA6
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x0006EAB3 File Offset: 0x0006CCB3
		[DefaultValue("")]
		[WebCategory("Paging")]
		public string SelectCountMethod
		{
			get
			{
				return this.DefaultView.SelectCountMethod;
			}
			set
			{
				this.DefaultView.SelectCountMethod = value;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to retrieve data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to retrieve data. The default is an empty string ("").</returns>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x0006EAC1 File Offset: 0x0006CCC1
		// (set) Token: 0x06002A9B RID: 10907 RVA: 0x0006EACE File Offset: 0x0006CCCE
		[DefaultValue("")]
		[WebCategory("Data")]
		public string SelectMethod
		{
			get
			{
				return this.DefaultView.SelectMethod;
			}
			set
			{
				this.DefaultView.SelectMethod = value;
			}
		}

		/// <summary>Gets a collection of parameters that are used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</summary>
		/// <returns>A collection of parameters that are used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</returns>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x0006EADC File Offset: 0x0006CCDC
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[WebCategory("Data")]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public ParameterCollection SelectParameters
		{
			get
			{
				return this.DefaultView.SelectParameters;
			}
		}

		/// <summary>Gets or sets the name of the business object that the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> parameter used to specify a sort expression for data source sorting support.</summary>
		/// <returns>The name of the method parameter used to indicate the parameter which is used to sort the data. The default is an empty string.</returns>
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06002A9D RID: 10909 RVA: 0x0006EAE9 File Offset: 0x0006CCE9
		// (set) Token: 0x06002A9E RID: 10910 RVA: 0x0006EAF6 File Offset: 0x0006CCF6
		[DefaultValue("")]
		[WebCategory("Data")]
		public string SortParameterName
		{
			get
			{
				return this.DefaultView.SortParameterName;
			}
			set
			{
				this.DefaultView.SortParameterName = value;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited string that indicates which databases and tables to use for the Microsoft SQL Server cache dependency.</summary>
		/// <returns>A string that indicates which databases and tables to use for the SQL Server cache dependency.</returns>
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x0006EB04 File Offset: 0x0006CD04
		// (set) Token: 0x06002AA0 RID: 10912 RVA: 0x0006EB1A File Offset: 0x0006CD1A
		[global::System.MonoTODO("SQLServer specific")]
		[DefaultValue("")]
		public virtual string SqlCacheDependency
		{
			get
			{
				if (this.sqlCacheDependency == null)
				{
					return string.Empty;
				}
				return this.sqlCacheDependency;
			}
			set
			{
				this.sqlCacheDependency = value;
			}
		}

		/// <summary>Gets or sets the name of the data retrieval method parameter that is used to indicate the value of the identifier of the first record to retrieve for data source paging support.</summary>
		/// <returns>The name of the business object method parameter used to indicate the first record to retrieve. The parameter must return an integer value. The default is "startRowIndex".</returns>
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x0006EB23 File Offset: 0x0006CD23
		// (set) Token: 0x06002AA2 RID: 10914 RVA: 0x0006EB30 File Offset: 0x0006CD30
		[WebCategory("Paging")]
		[DefaultValue("startRowIndex")]
		public string StartRowIndexParameterName
		{
			get
			{
				return this.DefaultView.StartRowIndexParameterName;
			}
			set
			{
				this.DefaultView.StartRowIndexParameterName = value;
			}
		}

		/// <summary>Gets or sets the name of the class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object represents.</summary>
		/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> represents. The default is an empty string ("").</returns>
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x0006EB3E File Offset: 0x0006CD3E
		// (set) Token: 0x06002AA4 RID: 10916 RVA: 0x0006EB4B File Offset: 0x0006CD4B
		[WebCategory("Data")]
		[DefaultValue("")]
		public string TypeName
		{
			get
			{
				return this.DefaultView.TypeName;
			}
			set
			{
				this.DefaultView.TypeName = value;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to update data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to update data. The default is an empty string.</returns>
		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x0006EB59 File Offset: 0x0006CD59
		// (set) Token: 0x06002AA6 RID: 10918 RVA: 0x0006EB66 File Offset: 0x0006CD66
		[WebCategory("Data")]
		[DefaultValue("")]
		public string UpdateMethod
		{
			get
			{
				return this.DefaultView.UpdateMethod;
			}
			set
			{
				this.DefaultView.UpdateMethod = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the method that is specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateMethod" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method that is specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateMethod" /> property.</returns>
		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x0006EB74 File Offset: 0x0006CD74
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public ParameterCollection UpdateParameters
		{
			get
			{
				return this.DefaultView.UpdateParameters;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x0006EB81 File Offset: 0x0006CD81
		internal DataSourceCacheManager Cache
		{
			get
			{
				if (this.cache == null)
				{
					this.cache = new DataSourceCacheManager(this.CacheDuration, this.CacheKeyDependency, this.CacheExpirationPolicy, this, this.Context);
				}
				return this.cache;
			}
		}

		/// <summary>Retrieves the named data source view that is associated with the data source control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> named DefaultView that is associated with the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />.</returns>
		/// <param name="viewName">The name of the view to retrieve. Because the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> supports only one view, <paramref name="viewName" /> is ignored. </param>
		/// <exception cref="T:System.ArgumentException">The specified <paramref name="viewName" /> is null or something other than DefaultView. </exception>
		// Token: 0x06002AA9 RID: 10921 RVA: 0x0006EBB5 File Offset: 0x0006CDB5
		protected override DataSourceView GetView(string viewName)
		{
			if (viewName == null)
			{
				throw new ArgumentException("viewName");
			}
			return this.DefaultView;
		}

		/// <summary>Retrieves a collection of names representing the list of view objects that are associated with the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the views associated with the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />.</returns>
		// Token: 0x06002AAA RID: 10922 RVA: 0x0006EBCB File Offset: 0x0006CDCB
		protected override ICollection GetViewNames()
		{
			return ObjectDataSource.emptyNames;
		}

		/// <summary>Retrieves data from the underlying data storage by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property with the parameters in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectParameters" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
		// Token: 0x06002AAB RID: 10923 RVA: 0x0006EBD2 File Offset: 0x0006CDD2
		public IEnumerable Select()
		{
			return this.DefaultView.Select(DataSourceSelectArguments.Empty);
		}

		/// <summary>Performs an update operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateMethod" /> property and any parameters that are in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows updated in the underlying data storage.</returns>
		// Token: 0x06002AAC RID: 10924 RVA: 0x0006EBE4 File Offset: 0x0006CDE4
		public int Update()
		{
			Hashtable hashtable = new Hashtable();
			return this.DefaultView.Update(hashtable, hashtable, null);
		}

		/// <summary>Performs a delete operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteMethod" /> property with any parameters that are in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows deleted from the underlying data storage, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs.AffectedRows" /> property of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> is set in the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleted" /> event; otherwise, -1.</returns>
		// Token: 0x06002AAD RID: 10925 RVA: 0x0006EC08 File Offset: 0x0006CE08
		public int Delete()
		{
			Hashtable hashtable = new Hashtable();
			return this.DefaultView.Delete(hashtable, null);
		}

		/// <summary>Performs an insert operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertMethod" /> property and any parameters in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows inserted into the underlying data storage.</returns>
		// Token: 0x06002AAE RID: 10926 RVA: 0x0006EC28 File Offset: 0x0006CE28
		public int Insert()
		{
			Hashtable hashtable = new Hashtable();
			return this.DefaultView.Insert(hashtable);
		}

		/// <summary>Adds a <see cref="E:System.Web.UI.Page.LoadComplete" /> event handler to the page that contains the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x06002AAF RID: 10927 RVA: 0x0006EC47 File Offset: 0x0006CE47
		protected internal override void OnInit(EventArgs e)
		{
			this.Page.LoadComplete += this.OnPageLoadComplete;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0006EC60 File Offset: 0x0006CE60
		private void OnPageLoadComplete(object sender, EventArgs e)
		{
			this.FilterParameters.UpdateValues(this.Context, this);
			this.SelectParameters.UpdateValues(this.Context, this);
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control. </summary>
		/// <param name="savedState">An object that contains the saved view state values for the control. </param>
		// Token: 0x06002AB1 RID: 10929 RVA: 0x0006EC88 File Offset: 0x0006CE88
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				((IStateManager)this.DefaultView).LoadViewState(null);
				return;
			}
			Pair pair = (Pair)savedState;
			base.LoadViewState(pair.First);
			((IStateManager)this.DefaultView).LoadViewState(pair.Second);
		}

		/// <summary>Saves the state of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
		/// <returns>Returns the server control's current view state; otherwise, returns null, if there is no view state associated with the control.</returns>
		// Token: 0x06002AB2 RID: 10930 RVA: 0x0006ECD0 File Offset: 0x0006CED0
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = ((IStateManager)this.DefaultView).SaveViewState();
			if (obj != null || obj2 != null)
			{
				return new Pair(obj, obj2);
			}
			return null;
		}

		/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control so that they can be stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x06002AB3 RID: 10931 RVA: 0x0006ECFF File Offset: 0x0006CEFF
		protected override void TrackViewState()
		{
			((IStateManager)this.DefaultView).TrackViewState();
		}

		/// <summary>Gets a or sets a value that indicates what culture information is used when converting string values to actual property types in order to construct an object of the type indicated by <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DataObjectTypeName" />.</summary>
		/// <returns>The culture information. The default value is <see cref="F:System.Web.UI.WebControls.ParsingCulture.Invariant" />.</returns>
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x0006ED24 File Offset: 0x0006CF24
		// (set) Token: 0x06002AB6 RID: 10934 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ParsingCulture ParsingCulture
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ParsingCulture.Invariant;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001ADF RID: 6879
		private static readonly string[] emptyNames = new string[] { "DefaultView" };

		// Token: 0x04001AE0 RID: 6880
		private ObjectDataSourceView defaultView;

		// Token: 0x04001AE1 RID: 6881
		private int cacheDuration;

		// Token: 0x04001AE2 RID: 6882
		private bool enableCaching;

		// Token: 0x04001AE3 RID: 6883
		private string cacheKeyDependency;

		// Token: 0x04001AE4 RID: 6884
		private string sqlCacheDependency;

		// Token: 0x04001AE5 RID: 6885
		private DataSourceCacheManager cache;

		// Token: 0x04001AE6 RID: 6886
		private DataSourceCacheExpiry cacheExpirationPolicy;
	}
}

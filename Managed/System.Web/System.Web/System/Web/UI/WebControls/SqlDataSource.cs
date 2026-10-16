using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an SQL database to data-bound controls.</summary>
	// Token: 0x02000409 RID: 1033
	[ParseChildren(true)]
	[DefaultEvent("Selecting")]
	[ToolboxBitmap("")]
	[Designer("System.Web.UI.Design.WebControls.SqlDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[PersistChildren(false)]
	[DefaultProperty("SelectQuery")]
	public class SqlDataSource : DataSourceControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class.</summary>
		// Token: 0x06002DDB RID: 11739 RVA: 0x00077591 File Offset: 0x00075791
		public SqlDataSource()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class with the specified connection string and Select command.</summary>
		/// <param name="connectionString">The connection string used to connect to the underlying database. </param>
		/// <param name="selectCommand">The SQL query used to retrieve data from the underlying database. If the SQL query is a parameterized SQL string, you might need to add <see cref="T:System.Web.UI.WebControls.Parameter" /> objects to the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection. </param>
		// Token: 0x06002DDC RID: 11740 RVA: 0x000775B6 File Offset: 0x000757B6
		public SqlDataSource(string connectionString, string selectCommand)
		{
			this.ConnectionString = connectionString;
			this.SelectCommand = selectCommand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class with the specified connection string and Select command.</summary>
		/// <param name="providerName">The name of the data provider that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses. If no provider is set, the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses the ADO.NET provider for Microsoft SQL Server, by default. </param>
		/// <param name="connectionString">The connection string used to connect to the underlying database. </param>
		/// <param name="selectCommand">The SQL query used to retrieve data from the underlying database. If the SQL query is a parameterized SQL string, you might need to add <see cref="T:System.Web.UI.WebControls.Parameter" /> objects to the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection. </param>
		// Token: 0x06002DDD RID: 11741 RVA: 0x000775E9 File Offset: 0x000757E9
		public SqlDataSource(string providerName, string connectionString, string selectCommand)
		{
			this.ProviderName = providerName;
			this.ConnectionString = connectionString;
			this.SelectCommand = selectCommand;
		}

		/// <summary>Gets the named data source view that is associated with the data source control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> named "Table" that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
		/// <param name="viewName">The name of the view to retrieve. Because the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> supports only one view, <paramref name="viewName" /> is ignored. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="viewName" /> is null or something other than Table. </exception>
		// Token: 0x06002DDE RID: 11742 RVA: 0x00077623 File Offset: 0x00075823
		protected override DataSourceView GetView(string viewName)
		{
			if (string.IsNullOrEmpty(viewName) || string.Compare(viewName, SqlDataSource.emptyNames[0], StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return this.View;
			}
			throw new ArgumentException("viewName");
		}

		/// <summary>Creates a data source view object that is associated with the data source control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
		/// <param name="viewName">The name of the data source view. </param>
		// Token: 0x06002DDF RID: 11743 RVA: 0x00077650 File Offset: 0x00075850
		protected virtual SqlDataSourceView CreateDataSourceView(string viewName)
		{
			SqlDataSourceView sqlDataSourceView = new SqlDataSourceView(this, viewName, this.Context);
			if (base.IsTrackingViewState)
			{
				((IStateManager)sqlDataSourceView).TrackViewState();
			}
			return sqlDataSourceView;
		}

		/// <summary>Returns the <see cref="T:System.Data.Common.DbProviderFactory" /> object that is associated with the ADO.NET provider that is identified by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.ProviderName" /> property.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbProviderFactory" /> that represents the identified ADO.NET provider; otherwise, and instance of the <see cref="N:System.Data.SqlClient" />, if no provider is set.</returns>
		// Token: 0x06002DE0 RID: 11744 RVA: 0x0007767A File Offset: 0x0007587A
		protected virtual DbProviderFactory GetDbProviderFactory()
		{
			if (!string.IsNullOrEmpty(this.ProviderName))
			{
				return DbProviderFactories.GetFactory(this.ProviderName);
			}
			return SqlClientFactory.Instance;
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x0007769A File Offset: 0x0007589A
		internal DbProviderFactory GetDbProviderFactoryInternal()
		{
			return this.GetDbProviderFactory();
		}

		/// <summary>Gets a collection of names representing the list of view objects that are associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the views associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
		// Token: 0x06002DE2 RID: 11746 RVA: 0x000776A2 File Offset: 0x000758A2
		protected override ICollection GetViewNames()
		{
			return SqlDataSource.emptyNames;
		}

		/// <summary>Performs an insert operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows inserted into the underlying database.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
		// Token: 0x06002DE3 RID: 11747 RVA: 0x000776A9 File Offset: 0x000758A9
		public int Insert()
		{
			return this.View.Insert(null);
		}

		/// <summary>Performs a delete operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows deleted from the underlying database.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
		// Token: 0x06002DE4 RID: 11748 RVA: 0x000776B7 File Offset: 0x000758B7
		public int Delete()
		{
			return this.View.Delete(null, null);
		}

		/// <summary>Retrieves data from the underlying database by using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that is used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> object cannot establish a connection with the underlying data source. </exception>
		// Token: 0x06002DE5 RID: 11749 RVA: 0x000776C6 File Offset: 0x000758C6
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.View.Select(arguments);
		}

		/// <summary>Performs an update operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateParameters" /> collection.</summary>
		/// <returns>A value that represents the number of rows updated in the underlying database.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
		// Token: 0x06002DE6 RID: 11750 RVA: 0x000776D4 File Offset: 0x000758D4
		public int Update()
		{
			return this.View.Update(null, null, null);
		}

		/// <summary>Adds a <see cref="E:System.Web.UI.Page.LoadComplete" /> event handler to the <see cref="T:System.Web.UI.Page" /> control that contains the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002DE7 RID: 11751 RVA: 0x000776E4 File Offset: 0x000758E4
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.LoadComplete += this.OnPageLoadComplete;
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x00077704 File Offset: 0x00075904
		private void OnPageLoadComplete(object sender, EventArgs e)
		{
			this.FilterParameters.UpdateValues(this.Context, this);
			this.SelectParameters.UpdateValues(this.Context, this);
		}

		/// <summary>Loads the state of the properties in the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control that need to be persisted.</summary>
		/// <param name="savedState">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</param>
		// Token: 0x06002DE9 RID: 11753 RVA: 0x0007772C File Offset: 0x0007592C
		protected override void LoadViewState(object savedState)
		{
			Pair pair = savedState as Pair;
			if (pair != null)
			{
				base.LoadViewState(pair.First);
				((IStateManager)this.View).LoadViewState(pair.Second);
			}
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>An object that contains the saved state of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
		// Token: 0x06002DEA RID: 11754 RVA: 0x00077760 File Offset: 0x00075960
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = ((IStateManager)this.View).SaveViewState();
			if (obj != null || obj2 != null)
			{
				return new Pair(obj, obj2);
			}
			return null;
		}

		/// <summary>Tracks view state changes to the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control so that the changes can be stored in the <see cref="T:System.Web.UI.StateBag" /> object for the control.</summary>
		// Token: 0x06002DEB RID: 11755 RVA: 0x0007778F File Offset: 0x0007598F
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.view != null)
			{
				((IStateManager)this.view).TrackViewState();
			}
		}

		/// <summary>Gets or sets a value indicating whether a data retrieval operation is canceled when any parameter that is contained in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection evaluates to null.</summary>
		/// <returns>true if a data retrieval operation is canceled when a parameter contained in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection evaluated to null; otherwise, false. The default is true.</returns>
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000777AA File Offset: 0x000759AA
		// (set) Token: 0x06002DED RID: 11757 RVA: 0x000777B7 File Offset: 0x000759B7
		[DefaultValue(true)]
		public virtual bool CancelSelectOnNullParameter
		{
			get
			{
				return this.View.CancelSelectOnNullParameter;
			}
			set
			{
				this.View.CancelSelectOnNullParameter = value;
			}
		}

		/// <summary>Gets or sets the value indicating how the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control performs updates and deletes when data in a row in the underlying database changes during the time of the operation.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.ConflictOptions" /> values. The default is the <see cref="F:System.Web.UI.ConflictOptions.OverwriteChanges" /> value.</returns>
		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000777C5 File Offset: 0x000759C5
		// (set) Token: 0x06002DEF RID: 11759 RVA: 0x000777D2 File Offset: 0x000759D2
		[DefaultValue(ConflictOptions.OverwriteChanges)]
		public ConflictOptions ConflictDetection
		{
			get
			{
				return this.View.ConflictDetection;
			}
			set
			{
				this.View.ConflictDetection = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> property is an SQL statement or the name of a stored procedure.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x000777E0 File Offset: 0x000759E0
		// (set) Token: 0x06002DF1 RID: 11761 RVA: 0x000777ED File Offset: 0x000759ED
		[DefaultValue(SqlDataSourceCommandType.Text)]
		public SqlDataSourceCommandType DeleteCommandType
		{
			get
			{
				return this.View.DeleteCommandType;
			}
			set
			{
				this.View.DeleteCommandType = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> property is an SQL statement or the name of a stored procedure. </summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000777FB File Offset: 0x000759FB
		// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x00077808 File Offset: 0x00075A08
		[DefaultValue(SqlDataSourceCommandType.Text)]
		public SqlDataSourceCommandType InsertCommandType
		{
			get
			{
				return this.View.InsertCommandType;
			}
			set
			{
				this.View.InsertCommandType = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> property is an SQL query or the name of a stored procedure.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x00077816 File Offset: 0x00075A16
		// (set) Token: 0x06002DF5 RID: 11765 RVA: 0x00077823 File Offset: 0x00075A23
		[DefaultValue(SqlDataSourceCommandType.Text)]
		public SqlDataSourceCommandType SelectCommandType
		{
			get
			{
				return this.View.SelectCommandType;
			}
			set
			{
				this.View.SelectCommandType = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> property is an SQL statement or the name of a stored procedure.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x00077831 File Offset: 0x00075A31
		// (set) Token: 0x06002DF7 RID: 11767 RVA: 0x0007783E File Offset: 0x00075A3E
		[DefaultValue(SqlDataSourceCommandType.Text)]
		public SqlDataSourceCommandType UpdateCommandType
		{
			get
			{
				return this.View.UpdateCommandType;
			}
			set
			{
				this.View.UpdateCommandType = value;
			}
		}

		/// <summary>Gets or sets a format string to apply to the names of any parameters that are passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Delete" /> or <see cref="M:System.Web.UI.WebControls.SqlDataSource.Update" /> method.</summary>
		/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> parameters passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Delete" /> or <see cref="M:System.Web.UI.WebControls.SqlDataSource.Update" /> methods. The default is "{0}".</returns>
		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x0007784C File Offset: 0x00075A4C
		// (set) Token: 0x06002DF9 RID: 11769 RVA: 0x00077859 File Offset: 0x00075A59
		[DefaultValue("{0}")]
		public string OldValuesParameterFormatString
		{
			get
			{
				return this.View.OldValuesParameterFormatString;
			}
			set
			{
				this.View.OldValuesParameterFormatString = value;
			}
		}

		/// <summary>Gets or sets the name of a stored procedure parameter that is used to sort retrieved data when data retrieval is performed using a stored procedure.</summary>
		/// <returns>The name of a stored procedure parameter used to sort retrieved data when data retrieval is performed using a stored procedure.</returns>
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x00077867 File Offset: 0x00075A67
		// (set) Token: 0x06002DFB RID: 11771 RVA: 0x00077874 File Offset: 0x00075A74
		[DefaultValue("")]
		public string SortParameterName
		{
			get
			{
				return this.View.SortParameterName;
			}
			set
			{
				this.View.SortParameterName = value;
			}
		}

		/// <summary>Gets or sets a filtering expression that is applied when the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method is called.</summary>
		/// <returns>A string that represents a filtering expression applied when data is retrieved using the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSource.FilterExpression" /> property was set and the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> is in <see cref="F:System.Web.UI.WebControls.SqlDataSourceMode.DataReader" /> mode. </exception>
		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x00077882 File Offset: 0x00075A82
		// (set) Token: 0x06002DFD RID: 11773 RVA: 0x0007788F File Offset: 0x00075A8F
		[DefaultValue("")]
		public string FilterExpression
		{
			get
			{
				return this.View.FilterExpression;
			}
			set
			{
				this.View.FilterExpression = value;
			}
		}

		/// <summary>Gets or sets the name of the .NET Framework data provider that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to connect to an underlying data source.</summary>
		/// <returns>The name of the data provider that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses; otherwise, the ADO.NET provider for Microsoft SQL Server, if no provider is set. The default is the ADO.NET provider for Microsoft SQL Server.</returns>
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x0007789D File Offset: 0x00075A9D
		// (set) Token: 0x06002DFF RID: 11775 RVA: 0x000778A5 File Offset: 0x00075AA5
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.WebControls.DataProviderNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string ProviderName
		{
			get
			{
				return this.providerName;
			}
			set
			{
				if (this.providerName != value)
				{
					this.providerName = value;
					this.RaiseDataSourceChangedEvent(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the ADO.NET provider–specific connection string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to connect to an underlying database.</summary>
		/// <returns>A .NET Framework data provider–specific string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to connect to the SQL database that it represents. The default is an empty string ("").</returns>
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x000778C7 File Offset: 0x00075AC7
		// (set) Token: 0x06002E01 RID: 11777 RVA: 0x000778CF File Offset: 0x00075ACF
		[MergableProperty(false)]
		[Editor("System.Web.UI.Design.WebControls.SqlDataSourceConnectionStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				if (this.connectionString != value)
				{
					this.connectionString = value;
					this.RaiseDataSourceChangedEvent(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the data retrieval mode that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to fetch data.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceMode" /> values. The default is the  <see cref="F:System.Web.UI.WebControls.SqlDataSourceMode.DataSet" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.SqlDataSource.DataSourceMode" /> property is not one of the values defined in the <see cref="T:System.Web.UI.WebControls.SqlDataSourceMode" />. </exception>
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000778F1 File Offset: 0x00075AF1
		// (set) Token: 0x06002E03 RID: 11779 RVA: 0x000778F9 File Offset: 0x00075AF9
		[DefaultValue(SqlDataSourceMode.DataSet)]
		public SqlDataSourceMode DataSourceMode
		{
			get
			{
				return this.dataSourceMode;
			}
			set
			{
				if (this.dataSourceMode != value)
				{
					this.dataSourceMode = value;
					this.RaiseDataSourceChangedEvent(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to delete data from the underlying database.</summary>
		/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to delete data.</returns>
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x00077916 File Offset: 0x00075B16
		// (set) Token: 0x06002E05 RID: 11781 RVA: 0x00077923 File Offset: 0x00075B23
		[DefaultValue("")]
		public string DeleteCommand
		{
			get
			{
				return this.View.DeleteCommand;
			}
			set
			{
				this.View.DeleteCommand = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> property.</returns>
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x00077931 File Offset: 0x00075B31
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public ParameterCollection DeleteParameters
		{
			get
			{
				return this.View.DeleteParameters;
			}
		}

		/// <summary>Gets a collection of parameters that are associated with any parameter placeholders that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.FilterExpression" /> string.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.FilterExpression" /> property.</returns>
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x0007793E File Offset: 0x00075B3E
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ParameterCollection FilterParameters
		{
			get
			{
				return this.View.FilterParameters;
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to insert data into the underlying database.</summary>
		/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to insert data.</returns>
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x0007794B File Offset: 0x00075B4B
		// (set) Token: 0x06002E09 RID: 11785 RVA: 0x00077958 File Offset: 0x00075B58
		[DefaultValue("")]
		public string InsertCommand
		{
			get
			{
				return this.View.InsertCommand;
			}
			set
			{
				this.View.InsertCommand = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> property.</returns>
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x00077966 File Offset: 0x00075B66
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		public ParameterCollection InsertParameters
		{
			get
			{
				return this.View.InsertParameters;
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to retrieve data from the underlying database.</summary>
		/// <returns>An SQL string or the name of a stored procedure that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to retrieve data.</returns>
		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06002E0B RID: 11787 RVA: 0x00077973 File Offset: 0x00075B73
		// (set) Token: 0x06002E0C RID: 11788 RVA: 0x00077980 File Offset: 0x00075B80
		[DefaultValue("")]
		public string SelectCommand
		{
			get
			{
				return this.View.SelectCommand;
			}
			set
			{
				this.View.SelectCommand = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> property.</returns>
		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x0007798E File Offset: 0x00075B8E
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public ParameterCollection SelectParameters
		{
			get
			{
				return this.View.SelectParameters;
			}
		}

		/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to update data in the underlying database.</summary>
		/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to update data.</returns>
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x0007799B File Offset: 0x00075B9B
		// (set) Token: 0x06002E0F RID: 11791 RVA: 0x000779A8 File Offset: 0x00075BA8
		[DefaultValue("")]
		public string UpdateCommand
		{
			get
			{
				return this.View.UpdateCommand;
			}
			set
			{
				this.View.UpdateCommand = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> control that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> property.</returns>
		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x000779B6 File Offset: 0x00075BB6
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ParameterCollection UpdateParameters
		{
			get
			{
				return this.View.UpdateParameters;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000779C3 File Offset: 0x00075BC3
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

		/// <summary>Gets or sets a user-defined key dependency that is linked to all data cache objects that are created by the data source control. All cache objects are explicitly expired when the key is expired.</summary>
		/// <returns>A key that identifies all cache objects created by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x000779F7 File Offset: 0x00075BF7
		// (set) Token: 0x06002E13 RID: 11795 RVA: 0x00077A0D File Offset: 0x00075C0D
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

		/// <summary>Gets or sets a semicolon-delimited string that indicates which databases and tables to use for the Microsoft SQL Server cache dependency.</summary>
		/// <returns>A string that indicates which databases and tables to use for the SQL Server cache dependency.</returns>
		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x00077A16 File Offset: 0x00075C16
		// (set) Token: 0x06002E15 RID: 11797 RVA: 0x00077A2C File Offset: 0x00075C2C
		[DefaultValue("")]
		[global::System.MonoTODO("SQLServer specific")]
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

		/// <summary>Gets or sets the length of time, in seconds, that the data source control caches data that is retrieved by the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
		/// <returns>The number of seconds that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> caches the results of a <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> operation. The default is 0. The value cannot be negative.</returns>
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x00077A35 File Offset: 0x00075C35
		// (set) Token: 0x06002E17 RID: 11799 RVA: 0x00077A3D File Offset: 0x00075C3D
		[TypeConverter("System.Web.UI.DataSourceCacheDurationConverter")]
		[DefaultValue(0)]
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
		/// <returns>One of the <see cref="T:System.Web.UI.DataSourceCacheExpiry" /> values. The default is the <see cref="F:System.Web.UI.DataSourceCacheExpiry.Absolute" /> value.</returns>
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x00077A5A File Offset: 0x00075C5A
		// (set) Token: 0x06002E19 RID: 11801 RVA: 0x00077A62 File Offset: 0x00075C62
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has data caching enabled.</summary>
		/// <returns>true if data caching is enabled for the data source control; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSource.EnableCaching" /> property is set to true when caching is not supported by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />. </exception>
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x00077A6B File Offset: 0x00075C6B
		// (set) Token: 0x06002E1B RID: 11803 RVA: 0x00077A73 File Offset: 0x00075C73
		[DefaultValue(false)]
		public virtual bool EnableCaching
		{
			get
			{
				return this.enableCaching;
			}
			set
			{
				if (this.DataSourceMode == SqlDataSourceMode.DataReader && value)
				{
					throw new NotSupportedException();
				}
				this.enableCaching = value;
			}
		}

		/// <summary>Occurs when a delete operation has completed.</summary>
		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06002E1C RID: 11804 RVA: 0x00077A8D File Offset: 0x00075C8D
		// (remove) Token: 0x06002E1D RID: 11805 RVA: 0x00077A9B File Offset: 0x00075C9B
		public event SqlDataSourceStatusEventHandler Deleted
		{
			add
			{
				this.View.Deleted += value;
			}
			remove
			{
				this.View.Deleted -= value;
			}
		}

		/// <summary>Occurs before a delete operation.</summary>
		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06002E1E RID: 11806 RVA: 0x00077AA9 File Offset: 0x00075CA9
		// (remove) Token: 0x06002E1F RID: 11807 RVA: 0x00077AB7 File Offset: 0x00075CB7
		public event SqlDataSourceCommandEventHandler Deleting
		{
			add
			{
				this.View.Deleting += value;
			}
			remove
			{
				this.View.Deleting -= value;
			}
		}

		/// <summary>Occurs when an insert operation has completed.</summary>
		// Token: 0x140000DD RID: 221
		// (add) Token: 0x06002E20 RID: 11808 RVA: 0x00077AC5 File Offset: 0x00075CC5
		// (remove) Token: 0x06002E21 RID: 11809 RVA: 0x00077AD3 File Offset: 0x00075CD3
		public event SqlDataSourceStatusEventHandler Inserted
		{
			add
			{
				this.View.Inserted += value;
			}
			remove
			{
				this.View.Inserted -= value;
			}
		}

		/// <summary>Occurs before a filter operation.</summary>
		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06002E22 RID: 11810 RVA: 0x00077AE1 File Offset: 0x00075CE1
		// (remove) Token: 0x06002E23 RID: 11811 RVA: 0x00077AEF File Offset: 0x00075CEF
		public event SqlDataSourceFilteringEventHandler Filtering
		{
			add
			{
				this.View.Filtering += value;
			}
			remove
			{
				this.View.Filtering -= value;
			}
		}

		/// <summary>Occurs before an insert operation.</summary>
		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06002E24 RID: 11812 RVA: 0x00077AFD File Offset: 0x00075CFD
		// (remove) Token: 0x06002E25 RID: 11813 RVA: 0x00077B0B File Offset: 0x00075D0B
		public event SqlDataSourceCommandEventHandler Inserting
		{
			add
			{
				this.View.Inserting += value;
			}
			remove
			{
				this.View.Inserting -= value;
			}
		}

		/// <summary>Occurs when a data retrieval operation has completed.</summary>
		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06002E26 RID: 11814 RVA: 0x00077B19 File Offset: 0x00075D19
		// (remove) Token: 0x06002E27 RID: 11815 RVA: 0x00077B27 File Offset: 0x00075D27
		public event SqlDataSourceStatusEventHandler Selected
		{
			add
			{
				this.View.Selected += value;
			}
			remove
			{
				this.View.Selected -= value;
			}
		}

		/// <summary>Occurs before a data retrieval operation.</summary>
		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06002E28 RID: 11816 RVA: 0x00077B35 File Offset: 0x00075D35
		// (remove) Token: 0x06002E29 RID: 11817 RVA: 0x00077B43 File Offset: 0x00075D43
		public event SqlDataSourceSelectingEventHandler Selecting
		{
			add
			{
				this.View.Selecting += value;
			}
			remove
			{
				this.View.Selecting -= value;
			}
		}

		/// <summary>Occurs when an update operation has completed.</summary>
		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06002E2A RID: 11818 RVA: 0x00077B51 File Offset: 0x00075D51
		// (remove) Token: 0x06002E2B RID: 11819 RVA: 0x00077B5F File Offset: 0x00075D5F
		public event SqlDataSourceStatusEventHandler Updated
		{
			add
			{
				this.View.Updated += value;
			}
			remove
			{
				this.View.Updated -= value;
			}
		}

		/// <summary>Occurs before an update operation.</summary>
		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06002E2C RID: 11820 RVA: 0x00077B6D File Offset: 0x00075D6D
		// (remove) Token: 0x06002E2D RID: 11821 RVA: 0x00077B7B File Offset: 0x00075D7B
		public event SqlDataSourceCommandEventHandler Updating
		{
			add
			{
				this.View.Updating += value;
			}
			remove
			{
				this.View.Updating -= value;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x00077B89 File Offset: 0x00075D89
		private SqlDataSourceView View
		{
			get
			{
				if (this.view == null)
				{
					this.view = this.CreateDataSourceView("DefaultView");
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.view).TrackViewState();
					}
				}
				return this.view;
			}
		}

		// Token: 0x04001B93 RID: 7059
		private static readonly string[] emptyNames = new string[] { "DefaultView" };

		// Token: 0x04001B94 RID: 7060
		private string providerName = string.Empty;

		// Token: 0x04001B95 RID: 7061
		private string connectionString = string.Empty;

		// Token: 0x04001B96 RID: 7062
		private SqlDataSourceMode dataSourceMode = SqlDataSourceMode.DataSet;

		// Token: 0x04001B97 RID: 7063
		private int cacheDuration;

		// Token: 0x04001B98 RID: 7064
		private bool enableCaching;

		// Token: 0x04001B99 RID: 7065
		private string cacheKeyDependency;

		// Token: 0x04001B9A RID: 7066
		private string sqlCacheDependency;

		// Token: 0x04001B9B RID: 7067
		private DataSourceCacheManager cache;

		// Token: 0x04001B9C RID: 7068
		private DataSourceCacheExpiry cacheExpirationPolicy;

		// Token: 0x04001B9D RID: 7069
		private SqlDataSourceView view;
	}
}

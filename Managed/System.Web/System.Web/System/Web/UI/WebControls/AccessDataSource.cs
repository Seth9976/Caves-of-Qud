using System;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a Microsoft Access database for use with data-bound controls.</summary>
	// Token: 0x02000329 RID: 809
	[Designer("System.Web.UI.Design.WebControls.AccessDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxBitmap("")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class AccessDataSource : SqlDataSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> class.</summary>
		// Token: 0x06001BF0 RID: 7152 RVA: 0x00043D9E File Offset: 0x00041F9E
		public AccessDataSource()
		{
			base.ProviderName = "System.Data.OleDb";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> class with the specified data file path and Select command.</summary>
		/// <param name="dataFile">The location of the Access .mdb file. The location can be relative to the current Web form's folder, an absolute physical path, or a virtual path.</param>
		/// <param name="selectCommand">The SQL query used to retrieve data from the Access database. If the SQL query is a parameterized SQL string, add <see cref="T:System.Web.UI.WebControls.Parameter" /> objects to the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataFile" /> is null or an empty string.</exception>
		// Token: 0x06001BF1 RID: 7153 RVA: 0x00043DB1 File Offset: 0x00041FB1
		public AccessDataSource(string dataFile, string selectCommand)
			: base(string.Empty, selectCommand)
		{
			this.ProviderName = "System.Data.OleDb";
		}

		/// <summary>Creates a data source view object that is associated with the data source control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.AccessDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> instance.</returns>
		/// <param name="viewName">The name of the data source view.</param>
		// Token: 0x06001BF2 RID: 7154 RVA: 0x00043DCC File Offset: 0x00041FCC
		protected override SqlDataSourceView CreateDataSourceView(string viewName)
		{
			AccessDataSourceView accessDataSourceView = new AccessDataSourceView(this, viewName, this.Context);
			if (base.IsTrackingViewState)
			{
				((IStateManager)accessDataSourceView).TrackViewState();
			}
			return accessDataSourceView;
		}

		/// <summary>The <see cref="P:System.Web.UI.WebControls.AccessDataSource.SqlCacheDependency" /> property overrides the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SqlCacheDependency" /> property.</summary>
		/// <returns>Throws a <see cref="T:System.NotSupportedException" />, in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to get or set the <see cref="P:System.Web.UI.WebControls.AccessDataSource.SqlCacheDependency" />  property.</exception>
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x00043DF6 File Offset: 0x00041FF6
		// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x00043DF6 File Offset: 0x00041FF6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[global::System.MonoTODO("AccessDataSource does not support SQL Cache Dependencies")]
		[Browsable(false)]
		public override string SqlCacheDependency
		{
			get
			{
				throw new NotSupportedException("AccessDataSource does not supports SQL Cache Dependencies.");
			}
			set
			{
				throw new NotSupportedException("AccessDataSource does not supports SQL Cache Dependencies.");
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Data.Common.DbProviderFactory" /> object that is associated with the .NET data provider that is identified by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.ProviderName" /> property.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbFactory" /> object.</returns>
		// Token: 0x06001BF5 RID: 7157 RVA: 0x00043E02 File Offset: 0x00042002
		[global::System.MonoTODO("why override?  maybe it doesn't call DbProviderFactories.GetFactory?")]
		protected override DbProviderFactory GetDbProviderFactory()
		{
			return DbProviderFactories.GetFactory("System.Data.OleDb");
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00043E0E File Offset: 0x0004200E
		private string GetPhysicalDataFilePath()
		{
			if (string.IsNullOrEmpty(this.DataFile))
			{
				return string.Empty;
			}
			return HttpContext.Current.Request.MapPath(this.DataFile);
		}

		/// <summary>Gets the connection string that is used to connect to the Microsoft Access database.</summary>
		/// <returns>The OLE DB connection string that the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control uses to connect to an Access database, through the <see cref="N:System.Data.OleDb" /> .NET data provider.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.AccessDataSource.ConnectionString" /> property.</exception>
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00043E38 File Offset: 0x00042038
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x00043E68 File Offset: 0x00042068
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ConnectionString
		{
			get
			{
				if (this.connectionString == null)
				{
					this.connectionString = "Provider=" + "Microsoft.Jet.OLEDB.4.0" + "; Data Source=" + this.GetPhysicalDataFilePath();
				}
				return this.connectionString;
			}
			set
			{
				throw new InvalidOperationException("The ConnectionString is automatically generated for AccessDataSource and hence cannot be set.");
			}
		}

		/// <summary>Gets or sets the location of the Microsoft Access .mdb file.</summary>
		/// <returns>The location of the Access .mdb file. Absolute, relative, and virtual paths are supported.</returns>
		/// <exception cref="T:System.ArgumentException">An invalid path was given.</exception>
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00043E74 File Offset: 0x00042074
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00043E8B File Offset: 0x0004208B
		[Editor("System.Web.UI.Design.MdbDataFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("MS Office Access database file name")]
		public string DataFile
		{
			get
			{
				return this.ViewState.GetString("DataFile", string.Empty);
			}
			set
			{
				this.ViewState["DataFile"] = value;
				this.connectionString = null;
			}
		}

		/// <summary>Gets the name of the .NET data provider that the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control uses to connect to a Microsoft Access database.</summary>
		/// <returns>The string "System.Data.OleDb".</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.AccessDataSource.ProviderName" /> property. </exception>
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00043EA5 File Offset: 0x000420A5
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x00043EAD File Offset: 0x000420AD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string ProviderName
		{
			get
			{
				return base.ProviderName;
			}
			set
			{
				throw new InvalidOperationException("Setting ProviderName on an AccessDataSource is not allowed");
			}
		}

		// Token: 0x040017DB RID: 6107
		private const string PROVIDER_NAME = "System.Data.OleDb";

		// Token: 0x040017DC RID: 6108
		private const string PROVIDER_STRING = "Microsoft.Jet.OLEDB.4.0";

		// Token: 0x040017DD RID: 6109
		private string connectionString;
	}
}

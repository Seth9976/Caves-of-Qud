using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200000C RID: 12
	public sealed class SqliteCommandBuilder : DbCommandBuilder
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00007EC6 File Offset: 0x000060C6
		public SqliteCommandBuilder()
			: this(null)
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007ECF File Offset: 0x000060CF
		public SqliteCommandBuilder(SqliteDataAdapter adp)
		{
			this.QuotePrefix = "[";
			this.QuoteSuffix = "]";
			this.DataAdapter = adp;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007EF4 File Offset: 0x000060F4
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
		{
			((SqliteParameter)parameter).DbType = (DbType)row[SchemaTableColumn.ProviderType];
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007F11 File Offset: 0x00006111
		protected override string GetParameterName(string parameterName)
		{
			return string.Format(CultureInfo.InvariantCulture, "@{0}", parameterName);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007F23 File Offset: 0x00006123
		protected override string GetParameterName(int parameterOrdinal)
		{
			return string.Format(CultureInfo.InvariantCulture, "@param{0}", parameterOrdinal);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007F3A File Offset: 0x0000613A
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return this.GetParameterName(parameterOrdinal);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007F43 File Offset: 0x00006143
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((SqliteDataAdapter)adapter).RowUpdating -= this.RowUpdatingEventHandler;
				return;
			}
			((SqliteDataAdapter)adapter).RowUpdating += this.RowUpdatingEventHandler;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007F7D File Offset: 0x0000617D
		private void RowUpdatingEventHandler(object sender, RowUpdatingEventArgs e)
		{
			base.RowUpdatingHandler(e);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007F86 File Offset: 0x00006186
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00007F93 File Offset: 0x00006193
		public new SqliteDataAdapter DataAdapter
		{
			get
			{
				return (SqliteDataAdapter)base.DataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007F9C File Offset: 0x0000619C
		public new SqliteCommand GetDeleteCommand()
		{
			return (SqliteCommand)base.GetDeleteCommand();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007FA9 File Offset: 0x000061A9
		public new SqliteCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (SqliteCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007FB7 File Offset: 0x000061B7
		public new SqliteCommand GetUpdateCommand()
		{
			return (SqliteCommand)base.GetUpdateCommand();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007FC4 File Offset: 0x000061C4
		public new SqliteCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (SqliteCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007FD2 File Offset: 0x000061D2
		public new SqliteCommand GetInsertCommand()
		{
			return (SqliteCommand)base.GetInsertCommand();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007FDF File Offset: 0x000061DF
		public new SqliteCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (SqliteCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00007FED File Offset: 0x000061ED
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00007FF5 File Offset: 0x000061F5
		[Browsable(false)]
		public override CatalogLocation CatalogLocation
		{
			get
			{
				return base.CatalogLocation;
			}
			set
			{
				base.CatalogLocation = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00007FFE File Offset: 0x000061FE
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00008006 File Offset: 0x00006206
		[Browsable(false)]
		public override string CatalogSeparator
		{
			get
			{
				return base.CatalogSeparator;
			}
			set
			{
				base.CatalogSeparator = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000800F File Offset: 0x0000620F
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00008017 File Offset: 0x00006217
		[Browsable(false)]
		[DefaultValue("[")]
		public override string QuotePrefix
		{
			get
			{
				return base.QuotePrefix;
			}
			set
			{
				base.QuotePrefix = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00008020 File Offset: 0x00006220
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00008028 File Offset: 0x00006228
		[Browsable(false)]
		public override string QuoteSuffix
		{
			get
			{
				return base.QuoteSuffix;
			}
			set
			{
				base.QuoteSuffix = value;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00008034 File Offset: 0x00006234
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			if (string.IsNullOrEmpty(this.QuotePrefix) || string.IsNullOrEmpty(this.QuoteSuffix) || string.IsNullOrEmpty(unquotedIdentifier))
			{
				return unquotedIdentifier;
			}
			return this.QuotePrefix + unquotedIdentifier.Replace(this.QuoteSuffix, this.QuoteSuffix + this.QuoteSuffix) + this.QuoteSuffix;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00008094 File Offset: 0x00006294
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			if (string.IsNullOrEmpty(this.QuotePrefix) || string.IsNullOrEmpty(this.QuoteSuffix) || string.IsNullOrEmpty(quotedIdentifier))
			{
				return quotedIdentifier;
			}
			if (!quotedIdentifier.StartsWith(this.QuotePrefix, StringComparison.InvariantCultureIgnoreCase) || !quotedIdentifier.EndsWith(this.QuoteSuffix, StringComparison.InvariantCultureIgnoreCase))
			{
				return quotedIdentifier;
			}
			return quotedIdentifier.Substring(this.QuotePrefix.Length, quotedIdentifier.Length - (this.QuotePrefix.Length + this.QuoteSuffix.Length)).Replace(this.QuoteSuffix + this.QuoteSuffix, this.QuoteSuffix);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00008130 File Offset: 0x00006330
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00008138 File Offset: 0x00006338
		[Browsable(false)]
		public override string SchemaSeparator
		{
			get
			{
				return base.SchemaSeparator;
			}
			set
			{
				base.SchemaSeparator = value;
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00008144 File Offset: 0x00006344
		protected override DataTable GetSchemaTable(DbCommand sourceCommand)
		{
			DataTable dataTable;
			using (IDataReader dataReader = sourceCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
			{
				DataTable schemaTable = dataReader.GetSchemaTable();
				if (this.HasSchemaPrimaryKey(schemaTable))
				{
					this.ResetIsUniqueSchemaColumn(schemaTable);
				}
				dataTable = schemaTable;
			}
			return dataTable;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00008190 File Offset: 0x00006390
		private bool HasSchemaPrimaryKey(DataTable schema)
		{
			DataColumn dataColumn = schema.Columns[SchemaTableColumn.IsKey];
			using (IEnumerator enumerator = schema.Rows.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((bool)((DataRow)enumerator.Current)[dataColumn])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00008208 File Offset: 0x00006408
		private void ResetIsUniqueSchemaColumn(DataTable schema)
		{
			DataColumn dataColumn = schema.Columns[SchemaTableColumn.IsUnique];
			DataColumn dataColumn2 = schema.Columns[SchemaTableColumn.IsKey];
			foreach (object obj in schema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (!(bool)dataRow[dataColumn2])
				{
					dataRow[dataColumn] = false;
				}
			}
			schema.AcceptChanges();
		}
	}
}

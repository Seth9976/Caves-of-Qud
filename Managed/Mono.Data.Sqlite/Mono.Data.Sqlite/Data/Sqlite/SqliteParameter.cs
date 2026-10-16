using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200002F RID: 47
	public sealed class SqliteParameter : DbParameter, ICloneable
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000D48F File Offset: 0x0000B68F
		public SqliteParameter()
			: this(null, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
		public SqliteParameter(string parameterName)
			: this(parameterName, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D4B1 File Offset: 0x0000B6B1
		public SqliteParameter(string parameterName, object value)
			: this(parameterName, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
			this.Value = value;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D4C9 File Offset: 0x0000B6C9
		public SqliteParameter(string parameterName, DbType dbType)
			: this(parameterName, dbType, 0, null, DataRowVersion.Current)
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D4DA File Offset: 0x0000B6DA
		public SqliteParameter(string parameterName, DbType dbType, string sourceColumn)
			: this(parameterName, dbType, 0, sourceColumn, DataRowVersion.Current)
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D4EB File Offset: 0x0000B6EB
		public SqliteParameter(string parameterName, DbType dbType, string sourceColumn, DataRowVersion rowVersion)
			: this(parameterName, dbType, 0, sourceColumn, rowVersion)
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D4F9 File Offset: 0x0000B6F9
		public SqliteParameter(DbType dbType)
			: this(null, dbType, 0, null, DataRowVersion.Current)
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D50A File Offset: 0x0000B70A
		public SqliteParameter(DbType dbType, object value)
			: this(null, dbType, 0, null, DataRowVersion.Current)
		{
			this.Value = value;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D522 File Offset: 0x0000B722
		public SqliteParameter(DbType dbType, string sourceColumn)
			: this(null, dbType, 0, sourceColumn, DataRowVersion.Current)
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D533 File Offset: 0x0000B733
		public SqliteParameter(DbType dbType, string sourceColumn, DataRowVersion rowVersion)
			: this(null, dbType, 0, sourceColumn, rowVersion)
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D540 File Offset: 0x0000B740
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize)
			: this(parameterName, parameterType, parameterSize, null, DataRowVersion.Current)
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D551 File Offset: 0x0000B751
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize, string sourceColumn)
			: this(parameterName, parameterType, parameterSize, sourceColumn, DataRowVersion.Current)
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D564 File Offset: 0x0000B764
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize, string sourceColumn, DataRowVersion rowVersion)
		{
			this._parameterName = parameterName;
			this._dbType = (int)parameterType;
			this._sourceColumn = sourceColumn;
			this._rowVersion = rowVersion;
			this._objValue = null;
			this._dataSize = parameterSize;
			this._nullMapping = false;
			this._nullable = true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		private SqliteParameter(SqliteParameter source)
			: this(source.ParameterName, (DbType)source._dbType, 0, source.Direction, source.IsNullable, 0, 0, source.SourceColumn, source.SourceVersion, source.Value)
		{
			this._nullMapping = source._nullMapping;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D600 File Offset: 0x0000B800
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion rowVersion, object value)
			: this(parameterName, parameterType, parameterSize, sourceColumn, rowVersion)
		{
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.Value = value;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D627 File Offset: 0x0000B827
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion rowVersion, bool sourceColumnNullMapping, object value)
			: this(parameterName, parameterType, parameterSize, sourceColumn, rowVersion)
		{
			this.Direction = direction;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D64E File Offset: 0x0000B84E
		public SqliteParameter(DbType parameterType, int parameterSize)
			: this(null, parameterType, parameterSize, null, DataRowVersion.Current)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D65F File Offset: 0x0000B85F
		public SqliteParameter(DbType parameterType, int parameterSize, string sourceColumn)
			: this(null, parameterType, parameterSize, sourceColumn, DataRowVersion.Current)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D670 File Offset: 0x0000B870
		public SqliteParameter(DbType parameterType, int parameterSize, string sourceColumn, DataRowVersion rowVersion)
			: this(null, parameterType, parameterSize, sourceColumn, rowVersion)
		{
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000D67E File Offset: 0x0000B87E
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000D686 File Offset: 0x0000B886
		public override bool IsNullable
		{
			get
			{
				return this._nullable;
			}
			set
			{
				this._nullable = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000D68F File Offset: 0x0000B88F
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000D6C9 File Offset: 0x0000B8C9
		[DbProviderSpecificTypeProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		public override DbType DbType
		{
			get
			{
				if (this._dbType != -1)
				{
					return (DbType)this._dbType;
				}
				if (this._objValue != null && this._objValue != DBNull.Value)
				{
					return SqliteConvert.TypeToDbType(this._objValue.GetType());
				}
				return DbType.String;
			}
			set
			{
				this._dbType = (int)value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000D6D2 File Offset: 0x0000B8D2
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000D6D5 File Offset: 0x0000B8D5
		public override ParameterDirection Direction
		{
			get
			{
				return ParameterDirection.Input;
			}
			set
			{
				if (value != ParameterDirection.Input)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000D6E1 File Offset: 0x0000B8E1
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000D6E9 File Offset: 0x0000B8E9
		public override string ParameterName
		{
			get
			{
				return this._parameterName;
			}
			set
			{
				this._parameterName = value;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D6F2 File Offset: 0x0000B8F2
		public override void ResetDbType()
		{
			this._dbType = -1;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000D6FB File Offset: 0x0000B8FB
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000D703 File Offset: 0x0000B903
		[DefaultValue(0)]
		public override int Size
		{
			get
			{
				return this._dataSize;
			}
			set
			{
				this._dataSize = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000D70C File Offset: 0x0000B90C
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000D714 File Offset: 0x0000B914
		public override string SourceColumn
		{
			get
			{
				return this._sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000D71D File Offset: 0x0000B91D
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000D725 File Offset: 0x0000B925
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._nullMapping;
			}
			set
			{
				this._nullMapping = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000D72E File Offset: 0x0000B92E
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000D736 File Offset: 0x0000B936
		public override DataRowVersion SourceVersion
		{
			get
			{
				return this._rowVersion;
			}
			set
			{
				this._rowVersion = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000D73F File Offset: 0x0000B93F
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000D747 File Offset: 0x0000B947
		[TypeConverter(typeof(StringConverter))]
		[RefreshProperties(RefreshProperties.All)]
		public override object Value
		{
			get
			{
				return this._objValue;
			}
			set
			{
				this._objValue = value;
				if (this._dbType == -1 && this._objValue != null && this._objValue != DBNull.Value)
				{
					this._dbType = (int)SqliteConvert.TypeToDbType(this._objValue.GetType());
				}
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000D784 File Offset: 0x0000B984
		public object Clone()
		{
			return new SqliteParameter(this);
		}

		// Token: 0x040000F0 RID: 240
		internal int _dbType;

		// Token: 0x040000F1 RID: 241
		private DataRowVersion _rowVersion;

		// Token: 0x040000F2 RID: 242
		private object _objValue;

		// Token: 0x040000F3 RID: 243
		private string _sourceColumn;

		// Token: 0x040000F4 RID: 244
		private string _parameterName;

		// Token: 0x040000F5 RID: 245
		private int _dataSize;

		// Token: 0x040000F6 RID: 246
		private bool _nullable;

		// Token: 0x040000F7 RID: 247
		private bool _nullMapping;
	}
}

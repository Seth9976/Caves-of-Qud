using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000030 RID: 48
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class SqliteParameterCollection : DbParameterCollection
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000D78C File Offset: 0x0000B98C
		internal SqliteParameterCollection(SqliteCommand cmd)
		{
			this._command = cmd;
			this._parameterList = new List<SqliteParameter>();
			this._unboundFlag = true;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000D7AD File Offset: 0x0000B9AD
		public override bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		public override bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000D7B3 File Offset: 0x0000B9B3
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000D7B6 File Offset: 0x0000B9B6
		public override object SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000D7B9 File Offset: 0x0000B9B9
		public override IEnumerator GetEnumerator()
		{
			return this._parameterList.GetEnumerator();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		public SqliteParameter Add(string parameterName, DbType parameterType, int parameterSize, string sourceColumn)
		{
			SqliteParameter sqliteParameter = new SqliteParameter(parameterName, parameterType, parameterSize, sourceColumn);
			this.Add(sqliteParameter);
			return sqliteParameter;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		public SqliteParameter Add(string parameterName, DbType parameterType, int parameterSize)
		{
			SqliteParameter sqliteParameter = new SqliteParameter(parameterName, parameterType, parameterSize);
			this.Add(sqliteParameter);
			return sqliteParameter;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000D810 File Offset: 0x0000BA10
		public SqliteParameter Add(string parameterName, DbType parameterType)
		{
			SqliteParameter sqliteParameter = new SqliteParameter(parameterName, parameterType);
			this.Add(sqliteParameter);
			return sqliteParameter;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D830 File Offset: 0x0000BA30
		public int Add(SqliteParameter parameter)
		{
			int num = -1;
			if (!string.IsNullOrEmpty(parameter.ParameterName))
			{
				num = this.IndexOf(parameter.ParameterName);
			}
			if (num == -1)
			{
				num = this._parameterList.Count;
				this._parameterList.Add(parameter);
			}
			this.SetParameter(num, parameter);
			return num;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000D87E File Offset: 0x0000BA7E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			return this.Add((SqliteParameter)value);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D88C File Offset: 0x0000BA8C
		public SqliteParameter AddWithValue(string parameterName, object value)
		{
			SqliteParameter sqliteParameter = new SqliteParameter(parameterName, value);
			this.Add(sqliteParameter);
			return sqliteParameter;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D8AC File Offset: 0x0000BAAC
		public void AddRange(SqliteParameter[] values)
		{
			int num = values.Length;
			for (int i = 0; i < num; i++)
			{
				this.Add(values[i]);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		public override void AddRange(Array values)
		{
			int length = values.Length;
			for (int i = 0; i < length; i++)
			{
				this.Add((SqliteParameter)values.GetValue(i));
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D907 File Offset: 0x0000BB07
		public override void Clear()
		{
			this._unboundFlag = true;
			this._parameterList.Clear();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D91B File Offset: 0x0000BB1B
		public override bool Contains(string parameterName)
		{
			return this.IndexOf(parameterName) != -1;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D92A File Offset: 0x0000BB2A
		public override bool Contains(object value)
		{
			return this._parameterList.Contains((SqliteParameter)value);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D93D File Offset: 0x0000BB3D
		public override void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000D944 File Offset: 0x0000BB44
		public override int Count
		{
			get
			{
				return this._parameterList.Count;
			}
		}

		// Token: 0x17000052 RID: 82
		public SqliteParameter this[string parameterName]
		{
			get
			{
				return (SqliteParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		// Token: 0x17000053 RID: 83
		public SqliteParameter this[int index]
		{
			get
			{
				return (SqliteParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D981 File Offset: 0x0000BB81
		protected override DbParameter GetParameter(string parameterName)
		{
			return this.GetParameter(this.IndexOf(parameterName));
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D990 File Offset: 0x0000BB90
		protected override DbParameter GetParameter(int index)
		{
			return this._parameterList[index];
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		public override int IndexOf(string parameterName)
		{
			int count = this._parameterList.Count;
			for (int i = 0; i < count; i++)
			{
				if (string.Compare(parameterName, this._parameterList[i].ParameterName, true, CultureInfo.InvariantCulture) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D9E7 File Offset: 0x0000BBE7
		public override int IndexOf(object value)
		{
			return this._parameterList.IndexOf((SqliteParameter)value);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D9FA File Offset: 0x0000BBFA
		public override void Insert(int index, object value)
		{
			this._unboundFlag = true;
			this._parameterList.Insert(index, (SqliteParameter)value);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000DA15 File Offset: 0x0000BC15
		public override void Remove(object value)
		{
			this._unboundFlag = true;
			this._parameterList.Remove((SqliteParameter)value);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000DA30 File Offset: 0x0000BC30
		public override void RemoveAt(string parameterName)
		{
			this.RemoveAt(this.IndexOf(parameterName));
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000DA3F File Offset: 0x0000BC3F
		public override void RemoveAt(int index)
		{
			this._unboundFlag = true;
			this._parameterList.RemoveAt(index);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000DA54 File Offset: 0x0000BC54
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.SetParameter(this.IndexOf(parameterName), value);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000DA64 File Offset: 0x0000BC64
		protected override void SetParameter(int index, DbParameter value)
		{
			this._unboundFlag = true;
			this._parameterList[index] = (SqliteParameter)value;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000DA7F File Offset: 0x0000BC7F
		internal void Unbind()
		{
			this._unboundFlag = true;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000DA88 File Offset: 0x0000BC88
		internal void MapParameters(SqliteStatement activeStatement)
		{
			if (!this._unboundFlag || this._parameterList.Count == 0 || this._command._statementList == null)
			{
				return;
			}
			int num = 0;
			int num2 = -1;
			foreach (SqliteParameter sqliteParameter in this._parameterList)
			{
				num2++;
				string text = sqliteParameter.ParameterName;
				if (text == null)
				{
					text = string.Format(CultureInfo.InvariantCulture, ";{0}", num);
					num++;
				}
				bool flag = false;
				int num3;
				if (activeStatement == null)
				{
					num3 = this._command._statementList.Count;
				}
				else
				{
					num3 = 1;
				}
				SqliteStatement sqliteStatement = activeStatement;
				for (int i = 0; i < num3; i++)
				{
					flag = false;
					if (sqliteStatement == null)
					{
						sqliteStatement = this._command._statementList[i];
					}
					if (sqliteStatement._paramNames != null && sqliteStatement.MapParameter(text, sqliteParameter))
					{
						flag = true;
					}
					sqliteStatement = null;
				}
				if (!flag)
				{
					text = string.Format(CultureInfo.InvariantCulture, ";{0}", num2);
					sqliteStatement = activeStatement;
					for (int i = 0; i < num3; i++)
					{
						if (sqliteStatement == null)
						{
							sqliteStatement = this._command._statementList[i];
						}
						if (sqliteStatement._paramNames == null || sqliteStatement.MapParameter(text, sqliteParameter))
						{
						}
						sqliteStatement = null;
					}
				}
			}
			if (activeStatement == null)
			{
				this._unboundFlag = false;
			}
		}

		// Token: 0x040000F8 RID: 248
		private SqliteCommand _command;

		// Token: 0x040000F9 RID: 249
		private List<SqliteParameter> _parameterList;

		// Token: 0x040000FA RID: 250
		private bool _unboundFlag;
	}
}

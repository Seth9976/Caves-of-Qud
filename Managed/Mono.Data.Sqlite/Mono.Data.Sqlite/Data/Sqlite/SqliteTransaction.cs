using System;
using System.Data;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000032 RID: 50
	public sealed class SqliteTransaction : DbTransaction
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000E030 File Offset: 0x0000C230
		internal SqliteTransaction(SqliteConnection connection, bool deferredLock)
		{
			this._cnn = connection;
			this._version = this._cnn._version;
			this._level = (deferredLock ? IsolationLevel.ReadCommitted : IsolationLevel.Serializable);
			SqliteConnection cnn = this._cnn;
			int transactionLevel = cnn._transactionLevel;
			cnn._transactionLevel = transactionLevel + 1;
			if (transactionLevel == 0)
			{
				try
				{
					using (SqliteCommand sqliteCommand = this._cnn.CreateCommand())
					{
						if (!deferredLock)
						{
							sqliteCommand.CommandText = "BEGIN IMMEDIATE";
						}
						else
						{
							sqliteCommand.CommandText = "BEGIN";
						}
						sqliteCommand.ExecuteNonQuery();
					}
				}
				catch (SqliteException)
				{
					this._cnn._transactionLevel--;
					this._cnn = null;
					throw;
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000E100 File Offset: 0x0000C300
		public override void Commit()
		{
			this.IsValid(true);
			if (this._cnn._transactionLevel - 1 == 0)
			{
				using (SqliteCommand sqliteCommand = this._cnn.CreateCommand())
				{
					sqliteCommand.CommandText = "COMMIT";
					sqliteCommand.ExecuteNonQuery();
				}
			}
			this._cnn._transactionLevel--;
			this._cnn = null;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000E178 File Offset: 0x0000C378
		public new SqliteConnection Connection
		{
			get
			{
				return this._cnn;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000E180 File Offset: 0x0000C380
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000E188 File Offset: 0x0000C388
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (this.IsValid(false))
					{
						this.Rollback();
					}
					this._cnn = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		public override IsolationLevel IsolationLevel
		{
			get
			{
				return this._level;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		public override void Rollback()
		{
			this.IsValid(true);
			SqliteTransaction.IssueRollback(this._cnn);
			this._cnn._transactionLevel = 0;
			this._cnn = null;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000E210 File Offset: 0x0000C410
		internal static void IssueRollback(SqliteConnection cnn)
		{
			using (SqliteCommand sqliteCommand = cnn.CreateCommand())
			{
				sqliteCommand.CommandText = "ROLLBACK";
				sqliteCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000E254 File Offset: 0x0000C454
		internal bool IsValid(bool throwError)
		{
			if (this._cnn == null)
			{
				if (throwError)
				{
					throw new ArgumentNullException("No connection associated with this transaction");
				}
				return false;
			}
			else if (this._cnn._transactionLevel == 0)
			{
				if (throwError)
				{
					throw new SqliteException(21, "No transaction is active on this connection");
				}
				return false;
			}
			else if (this._cnn._version != this._version)
			{
				if (throwError)
				{
					throw new SqliteException(21, "The connection was closed and re-opened, changes were rolled back");
				}
				return false;
			}
			else
			{
				if (this._cnn.State == ConnectionState.Open)
				{
					return true;
				}
				if (throwError)
				{
					throw new SqliteException(21, "Connection was closed");
				}
				return false;
			}
		}

		// Token: 0x04000103 RID: 259
		internal SqliteConnection _cnn;

		// Token: 0x04000104 RID: 260
		internal long _version;

		// Token: 0x04000105 RID: 261
		private IsolationLevel _level;
	}
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Web.Configuration;
using System.Web.Hosting;

namespace System.Web.SessionState
{
	// Token: 0x0200049A RID: 1178
	internal sealed class SessionSQLServerHandler : SessionStateStoreProviderBase
	{
		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x00089D34 File Offset: 0x00087F34
		private DbProviderFactory ProviderFactory
		{
			get
			{
				if (this.providerFactory == null)
				{
					try
					{
						this.providerFactory = Activator.CreateInstance(this.providerFactoryType) as DbProviderFactory;
					}
					catch (Exception ex)
					{
						throw new ProviderException("Failure to create database factory instance.", ex);
					}
				}
				return this.providerFactory;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x00089D84 File Offset: 0x00087F84
		// (set) Token: 0x0600356E RID: 13678 RVA: 0x00089D8C File Offset: 0x00087F8C
		public string ApplicationName { get; private set; }

		// Token: 0x0600356F RID: 13679 RVA: 0x00089D98 File Offset: 0x00087F98
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "SessionSQLServerHandler";
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", "Mono SQL Session Store Provider");
			}
			this.ApplicationName = HostingEnvironment.ApplicationVirtualPath;
			base.Initialize(name, config);
			this.sessionConfig = WebConfigurationManager.GetWebApplicationSection("system.web/sessionState") as SessionStateSection;
			this.connectionString = this.sessionConfig.SqlConnectionString;
			string text;
			if (string.IsNullOrEmpty(this.connectionString) || string.Compare(this.connectionString, SessionStateSection.DefaultSqlConnectionString, StringComparison.Ordinal) == 0)
			{
				this.connectionString = "Data Source=|DataDirectory|/ASPState.sqlite;Version=3";
				text = SessionSQLServerHandler.defaultDbFactoryTypeName;
			}
			else
			{
				string[] array = this.connectionString.Split(';', StringSplitOptions.None);
				List<string> list = new List<string>();
				text = null;
				bool allowCustomSqlDatabase = this.sessionConfig.AllowCustomSqlDatabase;
				foreach (string text2 in array)
				{
					if (text2.Trim().Length != 0)
					{
						if (text2.StartsWith("DbProviderName", StringComparison.OrdinalIgnoreCase))
						{
							int num = text2.IndexOf('=');
							if (num < 0)
							{
								throw new ProviderException("Invalid format for the 'DbProviderName' connection string parameter. Expected 'DbProviderName = value'.");
							}
							text = text2.Substring(num + 1);
						}
						else
						{
							if (!allowCustomSqlDatabase)
							{
								string text3 = text2.Trim();
								if (text3.StartsWith("database", StringComparison.OrdinalIgnoreCase) || text3.StartsWith("initial catalog", StringComparison.OrdinalIgnoreCase))
								{
									throw new ProviderException("Specifying a custom database is not allowed. Set the allowCustomSqlDatabase attribute of the <system.web/sessionState> section to 'true' in order to use a custom database name.");
								}
							}
							list.Add(text2);
						}
					}
				}
				this.connectionString = string.Join(";", list.ToArray());
				if (string.IsNullOrEmpty(text))
				{
					text = SessionSQLServerHandler.defaultDbFactoryTypeName;
				}
			}
			Exception ex = null;
			try
			{
				this.providerFactoryType = Type.GetType(text, true);
			}
			catch (Exception ex)
			{
				this.providerFactoryType = null;
			}
			if (this.providerFactoryType == null)
			{
				throw new ProviderException("Unable to find database provider factory type.", ex);
			}
			this.sqlCommandTimeout = (int)this.sessionConfig.SqlCommandTimeout.TotalSeconds;
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x00003946 File Offset: 0x00001B46
		public override void Dispose()
		{
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
		{
			return false;
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x00089FAC File Offset: 0x000881AC
		public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
		{
			DbCommand dbCommand = null;
			string text = this.Serialize((SessionStateItemCollection)item.Items);
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			string applicationName = this.ApplicationName;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			DateTime now = DateTime.Now;
			DbCommand dbCommand2;
			if (newItem)
			{
				dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "DELETE FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND Expires < @Expires");
				DbParameterCollection parameters = dbCommand.Parameters;
				parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
				parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
				parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", now));
				dbCommand2 = this.CreateCommand(dbProviderFactory, dbConnection, "INSERT INTO Sessions (SessionId, ApplicationName, Created, Expires, LockDate, LockId, Timeout, Locked, SessionItems, Flags) Values (@SessionId, @ApplicationName, @Created, @Expires, @LockDate, @LockId , @Timeout, @Locked, @SessionItems, @Flags)");
				DbParameterCollection parameters2 = dbCommand2.Parameters;
				parameters2.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
				parameters2.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
				parameters2.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Created", now));
				parameters2.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", now.AddMinutes((double)item.Timeout)));
				parameters2.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@LockDate", now));
				parameters2.Add(this.CreateParameter<int>(dbProviderFactory, "@LockId", 0));
				parameters2.Add(this.CreateParameter<int>(dbProviderFactory, "@Timeout", item.Timeout));
				parameters2.Add(this.CreateParameter<bool>(dbProviderFactory, "@Locked", false));
				parameters2.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionItems", text));
				parameters2.Add(this.CreateParameter<int>(dbProviderFactory, "@Flags", 0));
			}
			else
			{
				dbCommand2 = this.CreateCommand(dbProviderFactory, dbConnection, "UPDATE Sessions SET Expires = @Expires, SessionItems = @SessionItems, Locked = @Locked WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND LockId = @LockId");
				DbParameterCollection parameters3 = dbCommand2.Parameters;
				parameters3.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", now.AddMinutes((double)item.Timeout)));
				parameters3.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionItems", text));
				parameters3.Add(this.CreateParameter<bool>(dbProviderFactory, "@Locked", false));
				parameters3.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
				parameters3.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
				parameters3.Add(this.CreateParameter<int>(dbProviderFactory, "@Lockid", (int)lockId));
			}
			try
			{
				dbConnection.Open();
				if (dbCommand != null)
				{
					dbCommand.ExecuteNonQuery();
				}
				dbCommand2.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Failure storing session item in database.", ex);
			}
			finally
			{
				dbConnection.Close();
			}
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x0008A240 File Offset: 0x00088440
		public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			return this.GetSessionStoreItem(false, context, id, out locked, out lockAge, out lockId, out actionFlags);
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x0008A252 File Offset: 0x00088452
		public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			return this.GetSessionStoreItem(true, context, id, out locked, out lockAge, out lockId, out actionFlags);
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x0008A264 File Offset: 0x00088464
		private SessionStateStoreData GetSessionStoreItem(bool lockRecord, HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			SessionStateStoreData sessionStateStoreData = null;
			lockAge = TimeSpan.Zero;
			lockId = null;
			locked = false;
			actionFlags = SessionStateActions.None;
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			string applicationName = this.ApplicationName;
			DbDataReader dbDataReader = null;
			string text = string.Empty;
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			DateTime now = DateTime.Now;
			try
			{
				dbConnection.Open();
				if (lockRecord)
				{
					DbCommand dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "UPDATE Sessions SET Locked = @Locked, LockDate = @LockDate WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND Expires > @Expires");
					DbParameterCollection parameters = dbCommand.Parameters;
					parameters.Add(this.CreateParameter<bool>(dbProviderFactory, "@Locked", true));
					parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@LockDate", now));
					parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
					parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
					parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", now));
					if (dbCommand.ExecuteNonQuery() == 0)
					{
						locked = true;
					}
					else
					{
						locked = false;
					}
				}
				DbCommand dbCommand2 = this.CreateCommand(dbProviderFactory, dbConnection, "SELECT Expires, SessionItems, LockId, LockDate, Flags, Timeout FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
				DbParameterCollection parameters2 = dbCommand2.Parameters;
				parameters2.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
				parameters2.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
				dbDataReader = dbCommand2.ExecuteReader(CommandBehavior.SingleRow);
				while (dbDataReader.Read())
				{
					if (dbDataReader.GetDateTime(dbDataReader.GetOrdinal("Expires")) < now)
					{
						locked = false;
						flag2 = true;
					}
					else
					{
						flag = true;
					}
					text = dbDataReader.GetString(dbDataReader.GetOrdinal("SessionItems"));
					lockId = dbDataReader.GetInt32(dbDataReader.GetOrdinal("LockId"));
					lockAge = now.Subtract(dbDataReader.GetDateTime(dbDataReader.GetOrdinal("LockDate")));
					actionFlags = (SessionStateActions)dbDataReader.GetInt32(dbDataReader.GetOrdinal("Flags"));
					num = dbDataReader.GetInt32(dbDataReader.GetOrdinal("Timeout"));
				}
				dbDataReader.Close();
				if (flag2)
				{
					DbCommand dbCommand3 = this.CreateCommand(dbProviderFactory, dbConnection, "DELETE FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
					DbParameterCollection parameters3 = dbCommand3.Parameters;
					parameters3.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
					parameters3.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
					dbCommand3.ExecuteNonQuery();
				}
				if (!flag)
				{
					locked = false;
				}
				if (flag && !locked)
				{
					lockId = (int)lockId + 1;
					DbCommand dbCommand4 = this.CreateCommand(dbProviderFactory, dbConnection, "UPDATE Sessions SET LockId = @LockId, Flags = 0 WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
					DbParameterCollection parameters4 = dbCommand4.Parameters;
					parameters4.Add(this.CreateParameter<int>(dbProviderFactory, "@LockId", (int)lockId));
					parameters4.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
					parameters4.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
					dbCommand4.ExecuteNonQuery();
					if (actionFlags == SessionStateActions.InitializeItem)
					{
						sessionStateStoreData = this.CreateNewStoreData(context, (int)this.sessionConfig.Timeout.TotalMinutes);
					}
					else
					{
						sessionStateStoreData = this.Deserialize(context, text, num);
					}
				}
			}
			catch (Exception ex)
			{
				throw new ProviderException("Unable to retrieve session item from database.", ex);
			}
			finally
			{
				if (dbDataReader != null)
				{
					dbDataReader.Close();
				}
				dbConnection.Close();
			}
			return sessionStateStoreData;
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x0008A5BC File Offset: 0x000887BC
		private string Serialize(SessionStateItemCollection items)
		{
			GZipStream gzipStream = null;
			MemoryStream memoryStream = null;
			BinaryWriter binaryWriter = null;
			string text;
			try
			{
				memoryStream = new MemoryStream();
				Stream stream;
				if (this.sessionConfig.CompressionEnabled)
				{
					gzipStream = (stream = new GZipStream(memoryStream, CompressionMode.Compress, true));
				}
				else
				{
					stream = memoryStream;
				}
				binaryWriter = new BinaryWriter(stream);
				if (items != null)
				{
					items.Serialize(binaryWriter);
				}
				if (gzipStream != null)
				{
					gzipStream.Close();
				}
				binaryWriter.Close();
				text = Convert.ToBase64String(memoryStream.ToArray());
			}
			finally
			{
				if (binaryWriter != null)
				{
					binaryWriter.Dispose();
				}
				if (gzipStream != null)
				{
					gzipStream.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
			return text;
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x0008A650 File Offset: 0x00088850
		private SessionStateStoreData Deserialize(HttpContext context, string serializedItems, int timeout)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			GZipStream gzipStream = null;
			SessionStateStoreData sessionStateStoreData;
			try
			{
				memoryStream = new MemoryStream(Convert.FromBase64String(serializedItems));
				SessionStateItemCollection sessionStateItemCollection = new SessionStateItemCollection();
				if (memoryStream.Length > 0L)
				{
					Stream stream;
					if (this.sessionConfig.CompressionEnabled)
					{
						gzipStream = (stream = new GZipStream(memoryStream, CompressionMode.Decompress, true));
					}
					else
					{
						stream = memoryStream;
					}
					binaryReader = new BinaryReader(stream);
					sessionStateItemCollection = SessionStateItemCollection.Deserialize(binaryReader);
					if (gzipStream != null)
					{
						gzipStream.Close();
					}
					binaryReader.Close();
				}
				sessionStateStoreData = new SessionStateStoreData(sessionStateItemCollection, SessionStateUtility.GetSessionStaticObjects(context), timeout);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Dispose();
				}
				if (gzipStream != null)
				{
					gzipStream.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
			return sessionStateStoreData;
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x0008A6FC File Offset: 0x000888FC
		public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
		{
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			DbCommand dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "UPDATE Sessions SET Locked = 0, Expires = @Expires WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND LockId = @LockId");
			DbParameterCollection parameters = dbCommand.Parameters;
			parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", DateTime.Now.AddMinutes(this.sessionConfig.Timeout.TotalMinutes)));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", this.ApplicationName, 255));
			parameters.Add(this.CreateParameter<int>(dbProviderFactory, "@LockId", (int)lockId));
			try
			{
				dbConnection.Open();
				dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Error releasing item in database.", ex);
			}
			finally
			{
				dbConnection.Close();
			}
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x0008A7F0 File Offset: 0x000889F0
		public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
		{
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			DbCommand dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "DELETE FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND LockId = @LockId");
			DbParameterCollection parameters = dbCommand.Parameters;
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", this.ApplicationName, 255));
			parameters.Add(this.CreateParameter<int>(dbProviderFactory, "@LockId", (int)lockId));
			try
			{
				dbConnection.Open();
				dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Error removing item from database.", ex);
			}
			finally
			{
				dbConnection.Close();
			}
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x0008A8AC File Offset: 0x00088AAC
		public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
		{
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			DbCommand dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "INSERT INTO Sessions (SessionId, ApplicationName, Created, Expires, LockDate, LockId, Timeout, Locked, SessionItems, Flags) Values (@SessionId, @ApplicationName, @Created, @Expires, @LockDate, @LockId , @Timeout, @Locked, @SessionItems, @Flags)");
			DateTime now = DateTime.Now;
			DbParameterCollection parameters = dbCommand.Parameters;
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", this.ApplicationName, 255));
			parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Created", now));
			parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", now.AddMinutes((double)timeout)));
			parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@LockDate", now));
			parameters.Add(this.CreateParameter<int>(dbProviderFactory, "@LockId", 0));
			parameters.Add(this.CreateParameter<int>(dbProviderFactory, "@Timeout", timeout));
			parameters.Add(this.CreateParameter<bool>(dbProviderFactory, "@Locked", false));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionItems", string.Empty));
			parameters.Add(this.CreateParameter<int>(dbProviderFactory, "@Flags", 1));
			try
			{
				dbConnection.Open();
				dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Error creating uninitialized session item in the database.", ex);
			}
			finally
			{
				dbConnection.Close();
			}
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x0008AA04 File Offset: 0x00088C04
		public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
		{
			return new SessionStateStoreData(new SessionStateItemCollection(), SessionStateUtility.GetSessionStaticObjects(context), timeout);
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x0008AA18 File Offset: 0x00088C18
		public override void ResetItemTimeout(HttpContext context, string id)
		{
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			DbCommand dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "UPDATE Sessions SET Expires = @Expires WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
			DbParameterCollection parameters = dbCommand.Parameters;
			parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", DateTime.Now.AddMinutes(this.sessionConfig.Timeout.TotalMinutes)));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", this.ApplicationName, 255));
			try
			{
				dbConnection.Open();
				dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Error resetting session item timeout in the database.", ex);
			}
			finally
			{
				dbConnection.Close();
			}
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x00003946 File Offset: 0x00001B46
		public override void InitializeRequest(HttpContext context)
		{
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x00003946 File Offset: 0x00001B46
		public override void EndRequest(HttpContext context)
		{
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x0008AAF0 File Offset: 0x00088CF0
		private DbConnection CreateConnection(DbProviderFactory factory)
		{
			DbConnection dbConnection = factory.CreateConnection();
			dbConnection.ConnectionString = this.connectionString;
			return dbConnection;
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x0008AB04 File Offset: 0x00088D04
		private DbCommand CreateCommand(DbProviderFactory factory, DbConnection conn, string commandText)
		{
			DbCommand dbCommand = factory.CreateCommand();
			dbCommand.CommandTimeout = this.sqlCommandTimeout;
			dbCommand.Connection = conn;
			dbCommand.CommandText = commandText;
			return dbCommand;
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x0008AB26 File Offset: 0x00088D26
		private DbParameter CreateParameter<ValueType>(DbProviderFactory factory, string name, ValueType value)
		{
			return this.CreateParameter<ValueType>(factory, name, value, -1);
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x0008AB34 File Offset: 0x00088D34
		private DbParameter CreateParameter<ValueType>(DbProviderFactory factory, string name, ValueType value, int size)
		{
			DbParameter dbParameter = factory.CreateParameter();
			dbParameter.ParameterName = name;
			Type typeFromHandle = typeof(ValueType);
			if (typeFromHandle == typeof(string))
			{
				dbParameter.DbType = DbType.String;
			}
			else if (typeFromHandle == typeof(int))
			{
				dbParameter.DbType = DbType.Int32;
			}
			else if (typeFromHandle == typeof(bool))
			{
				dbParameter.DbType = DbType.Boolean;
			}
			else if (typeFromHandle == typeof(DateTime))
			{
				dbParameter.DbType = DbType.DateTime;
			}
			if (size > -1)
			{
				dbParameter.Size = size;
			}
			dbParameter.Value = value;
			return dbParameter;
		}

		// Token: 0x04001D65 RID: 7525
		private static readonly string defaultDbFactoryTypeName = "Mono.Data.Sqlite.SqliteFactory, Mono.Data.Sqlite, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

		// Token: 0x04001D66 RID: 7526
		private SessionStateSection sessionConfig;

		// Token: 0x04001D67 RID: 7527
		private string connectionString;

		// Token: 0x04001D68 RID: 7528
		private Type providerFactoryType;

		// Token: 0x04001D69 RID: 7529
		private DbProviderFactory providerFactory;

		// Token: 0x04001D6A RID: 7530
		private int sqlCommandTimeout;
	}
}

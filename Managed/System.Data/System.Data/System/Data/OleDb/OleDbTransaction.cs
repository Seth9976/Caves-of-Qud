using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents an SQL transaction to be made at a data source. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000174 RID: 372
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbTransaction : DbTransaction
	{
		// Token: 0x060013D5 RID: 5077 RVA: 0x0005AE3A File Offset: 0x0005903A
		internal OleDbTransaction()
		{
		}

		/// <summary>Gets the <see cref="T:System.Data.OleDb.OleDbConnection" /> object associated with the transaction, or null if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbConnection Connection
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override DbConnection DbConnection
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default is ReadCommitted.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override IsolationLevel IsolationLevel
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Initiates a nested database transaction.</summary>
		/// <returns>A nested database transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">Nested transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013D9 RID: 5081 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public OleDbTransaction Begin()
		{
			throw ADP.OleDb();
		}

		/// <summary>Initiates a nested database transaction and specifies the isolation level to use for the new transaction.</summary>
		/// <returns>A nested database transaction.</returns>
		/// <param name="isolevel">The isolation level to use for the transaction. </param>
		/// <exception cref="T:System.InvalidOperationException">Nested transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013DA RID: 5082 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public OleDbTransaction Begin(IsolationLevel isolevel)
		{
			throw ADP.OleDb();
		}

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013DB RID: 5083 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void Commit()
		{
			throw ADP.OleDb();
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override void Dispose(bool disposing)
		{
			throw ADP.OleDb();
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013DD RID: 5085 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void Rollback()
		{
			throw ADP.OleDb();
		}
	}
}

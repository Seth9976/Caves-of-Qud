using System;
using System.EnterpriseServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	/// <summary>Provides a way to wrap a callback method within a transaction boundary.</summary>
	// Token: 0x02000149 RID: 329
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Transactions
	{
		/// <summary>Wraps a specified transaction support around a callback method.</summary>
		/// <param name="callback">The <see cref="T:System.Web.Util.TransactedCallback" /> to be run under the specified transaction support.</param>
		/// <param name="mode">The <see cref="T:System.EnterpriseServices.TransactionOption" /> that specifies the transaction support for the delegate.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.Web.HttpException">The transacted code cannot be executed.</exception>
		// Token: 0x06000EB8 RID: 3768 RVA: 0x00027E28 File Offset: 0x00026028
		public static void InvokeTransacted(TransactedCallback callback, TransactionOption mode)
		{
			bool flag = false;
			Transactions.InvokeTransacted(callback, mode, ref flag);
		}

		/// <summary>Wraps a specified transaction support around a callback method and indicates whether the transaction aborted.</summary>
		/// <param name="callback">The <see cref="T:System.Web.Util.TransactedCallback" /> to be run under the specified transaction support.</param>
		/// <param name="mode">The <see cref="T:System.EnterpriseServices.TransactionOption" /> that specifies the transaction support for the delegate.</param>
		/// <param name="transactionAborted">The reference parameter that returns true if the transaction was aborted during the callback method; otherwise, false. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.Web.HttpException">The transacted code cannot be executed.</exception>
		// Token: 0x06000EB9 RID: 3769 RVA: 0x00027E40 File Offset: 0x00026040
		[global::System.MonoTODO("Not implemented, not supported by Mono")]
		public static void InvokeTransacted(TransactedCallback callback, TransactionOption mode, ref bool transactionAborted)
		{
			throw new PlatformNotSupportedException("Not supported on mono");
		}
	}
}

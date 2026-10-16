using System;
using System.Security.Permissions;

namespace System.Web.Caching
{
	/// <summary>The exception that is thrown when a SQL Server database is not enabled to support dependencies associated with the <see cref="T:System.Web.Caching.SqlCacheDependency" /> class. This class cannot be inherited. </summary>
	// Token: 0x02000687 RID: 1671
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class DatabaseNotEnabledForNotificationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException" /> class.</summary>
		// Token: 0x0600473A RID: 18234 RVA: 0x000C661C File Offset: 0x000C481C
		public DatabaseNotEnabledForNotificationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException" /> class.</summary>
		/// <param name="message">A string that describes the error. </param>
		// Token: 0x0600473B RID: 18235 RVA: 0x000C6624 File Offset: 0x000C4824
		public DatabaseNotEnabledForNotificationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x0600473C RID: 18236 RVA: 0x000C662D File Offset: 0x000C482D
		public DatabaseNotEnabledForNotificationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}

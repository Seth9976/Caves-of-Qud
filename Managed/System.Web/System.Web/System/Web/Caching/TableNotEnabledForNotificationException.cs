using System;
using System.Security.Permissions;

namespace System.Web.Caching
{
	/// <summary>The exception that is thrown when a <see cref="T:System.Web.Caching.SqlCacheDependency" /> class is used against a database table that is not enabled for change notifications.</summary>
	// Token: 0x02000692 RID: 1682
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class TableNotEnabledForNotificationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.TableNotEnabledForNotificationException" /> class.</summary>
		// Token: 0x0600477E RID: 18302 RVA: 0x000C661C File Offset: 0x000C481C
		public TableNotEnabledForNotificationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.TableNotEnabledForNotificationException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600477F RID: 18303 RVA: 0x000C6624 File Offset: 0x000C4824
		public TableNotEnabledForNotificationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.TableNotEnabledForNotificationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06004780 RID: 18304 RVA: 0x000C662D File Offset: 0x000C482D
		public TableNotEnabledForNotificationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}

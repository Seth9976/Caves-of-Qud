using System;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>The exception that is thrown when a generic exception occurs.</summary>
	// Token: 0x020000B9 RID: 185
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class HttpUnhandledException : HttpException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpUnhandledException" /> class.</summary>
		// Token: 0x06000A27 RID: 2599 RVA: 0x0000955C File Offset: 0x0000775C
		public HttpUnhandledException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpUnhandledException" /> class with the specified error messages.</summary>
		/// <param name="message">The message displayed to the client when the exception is thrown. </param>
		// Token: 0x06000A28 RID: 2600 RVA: 0x00009564 File Offset: 0x00007764
		public HttpUnhandledException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpUnhandledException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">The message displayed to the client when the exception is thrown. </param>
		/// <param name="innerException">The <see cref="P:System.Exception.InnerException" />, if any, that threw the current exception. </param>
		// Token: 0x06000A29 RID: 2601 RVA: 0x0000956D File Offset: 0x0000776D
		public HttpUnhandledException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}

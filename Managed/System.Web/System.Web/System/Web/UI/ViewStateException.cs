using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Represents the exception that is thrown when the view state cannot be loaded or validated. This class cannot be inherited.</summary>
	// Token: 0x02000246 RID: 582
	[Serializable]
	public sealed class ViewStateException : Exception, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ViewStateException" /> class. </summary>
		// Token: 0x060017CC RID: 6092 RVA: 0x0003E3A1 File Offset: 0x0003C5A1
		public ViewStateException()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the client is currently connected to the server.</summary>
		/// <returns>true if the client is still connected to the server; otherwise, false.</returns>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x00003A2B File Offset: 0x00001C2B
		public bool IsConnected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets debugging information about the HTTP request that resulted in a view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the formatted message with information about the exception.</returns>
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x00003A2B File Offset: 0x00001C2B
		public override string Message
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the path of the HTTP request that resulted in a view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the path from the request.</returns>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x00003A2B File Offset: 0x00001C2B
		public string Path
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the contents of the view-state string that, when read, caused the view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the view-state values that caused the view-state exception.</returns>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00003A2B File Offset: 0x00001C2B
		public string PersistedState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the URL of the page that linked to the page where the view-state exception occurred.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the HTTP referrer.</returns>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00003A2B File Offset: 0x00001C2B
		public string Referer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the IP address of the HTTP request that resulted in a view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the remote IP address of the client.</returns>
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x00003A2B File Offset: 0x00001C2B
		public string RemoteAddress
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the port number of the HTTP request that resulted in a view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the remote port number.</returns>
		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00003A2B File Offset: 0x00001C2B
		public string RemotePort
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the browser type of the HTTP request that resulted in a view-state exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the user agent, which is typically the browser type.</returns>
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x00003A2B File Offset: 0x00001C2B
		public string UserAgent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ViewStateException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060017D5 RID: 6101 RVA: 0x00003A2B File Offset: 0x00001C2B
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
	}
}

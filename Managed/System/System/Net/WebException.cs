using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error occurs while accessing the network through a pluggable protocol.</summary>
	// Token: 0x02000608 RID: 1544
	[Serializable]
	public class WebException : InvalidOperationException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class.</summary>
		// Token: 0x060030B4 RID: 12468 RVA: 0x000A76DF File Offset: 0x000A58DF
		public WebException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message.</summary>
		/// <param name="message">The text of the error message. </param>
		// Token: 0x060030B5 RID: 12469 RVA: 0x000A76EF File Offset: 0x000A58EF
		public WebException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message and nested exception.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="innerException">A nested exception. </param>
		// Token: 0x060030B6 RID: 12470 RVA: 0x000A76F9 File Offset: 0x000A58F9
		public WebException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message and status.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="status">One of the <see cref="T:System.Net.WebExceptionStatus" /> values. </param>
		// Token: 0x060030B7 RID: 12471 RVA: 0x000A770B File Offset: 0x000A590B
		public WebException(string message, WebExceptionStatus status)
			: this(message, null, status, null)
		{
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000A7717 File Offset: 0x000A5917
		internal WebException(string message, WebExceptionStatus status, WebExceptionInternalStatus internalStatus, Exception innerException)
			: this(message, innerException, status, null, internalStatus)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message, nested exception, status, and response.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="innerException">A nested exception. </param>
		/// <param name="status">One of the <see cref="T:System.Net.WebExceptionStatus" /> values. </param>
		/// <param name="response">A <see cref="T:System.Net.WebResponse" /> instance that contains the response from the remote host. </param>
		// Token: 0x060030B9 RID: 12473 RVA: 0x000A7725 File Offset: 0x000A5925
		public WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response)
			: this(message, null, innerException, status, response)
		{
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000A7734 File Offset: 0x000A5934
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response)
			: base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000A7780 File Offset: 0x000A5980
		internal WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus)
			: this(message, null, innerException, status, response, internalStatus)
		{
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000A7790 File Offset: 0x000A5990
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus)
			: base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
			this.m_InternalStatus = internalStatus;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.WebException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.WebException" />. </param>
		// Token: 0x060030BD RID: 12477 RVA: 0x000A77E4 File Offset: 0x000A59E4
		protected WebException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebException" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x060030BE RID: 12478 RVA: 0x000A77F6 File Offset: 0x000A59F6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.WebException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x060030BF RID: 12479 RVA: 0x0002973E File Offset: 0x0002793E
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.WebExceptionStatus" /> values.</returns>
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x000A7800 File Offset: 0x000A5A00
		public WebExceptionStatus Status
		{
			get
			{
				return this.m_Status;
			}
		}

		/// <summary>Gets the response that the remote host returned.</summary>
		/// <returns>If a response is available from the Internet resource, a <see cref="T:System.Net.WebResponse" /> instance that contains the error response from an Internet resource; otherwise, null.</returns>
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000A7808 File Offset: 0x000A5A08
		public WebResponse Response
		{
			get
			{
				return this.m_Response;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x000A7810 File Offset: 0x000A5A10
		internal WebExceptionInternalStatus InternalStatus
		{
			get
			{
				return this.m_InternalStatus;
			}
		}

		// Token: 0x04001C62 RID: 7266
		private WebExceptionStatus m_Status = WebExceptionStatus.UnknownError;

		// Token: 0x04001C63 RID: 7267
		private WebResponse m_Response;

		// Token: 0x04001C64 RID: 7268
		[NonSerialized]
		private WebExceptionInternalStatus m_InternalStatus;
	}
}

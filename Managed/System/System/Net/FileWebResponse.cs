using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Provides a file system implementation of the <see cref="T:System.Net.WebResponse" /> class.</summary>
	// Token: 0x0200065D RID: 1629
	[Serializable]
	public class FileWebResponse : WebResponse, ISerializable, ICloseEx
	{
		// Token: 0x06003358 RID: 13144 RVA: 0x000B30F4 File Offset: 0x000B12F4
		internal FileWebResponse(FileWebRequest request, Uri uri, FileAccess access, bool asyncHint)
		{
			try
			{
				this.m_fileAccess = access;
				if (access == FileAccess.Write)
				{
					this.m_stream = Stream.Null;
				}
				else
				{
					this.m_stream = new FileWebStream(request, uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, asyncHint);
					this.m_contentLength = this.m_stream.Length;
				}
				this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.FileWebResponse);
				this.m_headers.AddInternal("Content-Length", this.m_contentLength.ToString(NumberFormatInfo.InvariantInfo));
				this.m_headers.AddInternal("Content-Type", "application/octet-stream");
				this.m_uri = uri;
			}
			catch (Exception ex)
			{
				throw new WebException(ex.Message, ex, WebExceptionStatus.ConnectFailure, null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.FileWebResponse" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to serialize the new <see cref="T:System.Net.FileWebResponse" /> instance. </param>
		/// <param name="streamingContext">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that contains the source of the serialized stream associated with the new <see cref="T:System.Net.FileWebResponse" /> instance. </param>
		// Token: 0x06003359 RID: 13145 RVA: 0x000B31B8 File Offset: 0x000B13B8
		[Obsolete("Serialization is obsoleted for this type. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected FileWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			this.m_headers = (WebHeaderCollection)serializationInfo.GetValue("headers", typeof(WebHeaderCollection));
			this.m_uri = (Uri)serializationInfo.GetValue("uri", typeof(Uri));
			this.m_contentLength = serializationInfo.GetInt64("contentLength");
			this.m_fileAccess = (FileAccess)serializationInfo.GetInt32("fileAccess");
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.FileWebResponse" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> , which will hold the serialized data for the <see cref="T:System.Net.FileWebResponse" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the destination of the serialized stream associated with the new <see cref="T:System.Net.FileWebResponse" />. </param>
		// Token: 0x0600335A RID: 13146 RVA: 0x000ABB64 File Offset: 0x000A9D64
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x0600335B RID: 13147 RVA: 0x000B3230 File Offset: 0x000B1430
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("headers", this.m_headers, typeof(WebHeaderCollection));
			serializationInfo.AddValue("uri", this.m_uri, typeof(Uri));
			serializationInfo.AddValue("contentLength", this.m_contentLength);
			serializationInfo.AddValue("fileAccess", this.m_fileAccess);
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets the length of the content in the file system resource.</summary>
		/// <returns>The number of bytes returned from the file system resource.</returns>
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000B32A2 File Offset: 0x000B14A2
		public override long ContentLength
		{
			get
			{
				this.CheckDisposed();
				return this.m_contentLength;
			}
		}

		/// <summary>Gets the content type of the file system resource.</summary>
		/// <returns>The value "binary/octet-stream".</returns>
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x000B32B0 File Offset: 0x000B14B0
		public override string ContentType
		{
			get
			{
				this.CheckDisposed();
				return "application/octet-stream";
			}
		}

		/// <summary>Gets a collection of header name/value pairs associated with the response.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the header name/value pairs associated with the response.</returns>
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000B32BD File Offset: 0x000B14BD
		public override WebHeaderCollection Headers
		{
			get
			{
				this.CheckDisposed();
				return this.m_headers;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Net.FileWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FileWebResponse" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <see cref="P:System.Net.FileWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FileWebResponse" /> instance; otherwise, false.</returns>
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool SupportsHeaders
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the URI of the file system resource that provided the response.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the file system resource that provided the response.</returns>
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x000B32CB File Offset: 0x000B14CB
		public override Uri ResponseUri
		{
			get
			{
				this.CheckDisposed();
				return this.m_uri;
			}
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000B32D9 File Offset: 0x000B14D9
		private void CheckDisposed()
		{
			if (this.m_closed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		/// <summary>Closes the response stream.</summary>
		// Token: 0x06003362 RID: 13154 RVA: 0x000B32F4 File Offset: 0x000B14F4
		public override void Close()
		{
			((ICloseEx)this).CloseEx(CloseExState.Normal);
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000B3300 File Offset: 0x000B1500
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			try
			{
				if (!this.m_closed)
				{
					this.m_closed = true;
					Stream stream = this.m_stream;
					if (stream != null)
					{
						if (stream is ICloseEx)
						{
							((ICloseEx)stream).CloseEx(closeState);
						}
						else
						{
							stream.Close();
						}
						this.m_stream = null;
					}
				}
			}
			finally
			{
			}
		}

		/// <summary>Returns the data stream from the file system resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> for reading data from the file system resource.</returns>
		// Token: 0x06003364 RID: 13156 RVA: 0x000B335C File Offset: 0x000B155C
		public override Stream GetResponseStream()
		{
			try
			{
				this.CheckDisposed();
			}
			finally
			{
			}
			return this.m_stream;
		}

		// Token: 0x04001E34 RID: 7732
		private const int DefaultFileStreamBufferSize = 8192;

		// Token: 0x04001E35 RID: 7733
		private const string DefaultFileContentType = "application/octet-stream";

		// Token: 0x04001E36 RID: 7734
		private bool m_closed;

		// Token: 0x04001E37 RID: 7735
		private long m_contentLength;

		// Token: 0x04001E38 RID: 7736
		private FileAccess m_fileAccess;

		// Token: 0x04001E39 RID: 7737
		private WebHeaderCollection m_headers;

		// Token: 0x04001E3A RID: 7738
		private Stream m_stream;

		// Token: 0x04001E3B RID: 7739
		private Uri m_uri;
	}
}

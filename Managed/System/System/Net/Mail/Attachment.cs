using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an attachment to an e-mail.</summary>
	// Token: 0x020007FC RID: 2044
	public class Attachment : AttachmentBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string. </summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains a file path to use to create this attachment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileName" /> is empty.</exception>
		// Token: 0x06004148 RID: 16712 RVA: 0x000DFB51 File Offset: 0x000DDD51
		public Attachment(string fileName)
			: base(fileName)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string and MIME type information. </summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x06004149 RID: 16713 RVA: 0x000DFB6C File Offset: 0x000DDD6C
		public Attachment(string fileName, string mediaType)
			: base(fileName, mediaType)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains a file path to use to create this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="string" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x0600414A RID: 16714 RVA: 0x000DFB88 File Offset: 0x000DDD88
		public Attachment(string fileName, ContentType contentType)
			: base(fileName, contentType)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream and content type. </summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="stream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentType" /> is null.-or-<paramref name="contentStream" /> is null.</exception>
		// Token: 0x0600414B RID: 16715 RVA: 0x000DFBA4 File Offset: 0x000DDDA4
		public Attachment(Stream contentStream, ContentType contentType)
			: base(contentStream, contentType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream and name.</summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the value for the <see cref="P:System.Net.Mime.ContentType.Name" /> property of the <see cref="T:System.Net.Mime.ContentType" /> associated with this attachment. This value can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		// Token: 0x0600414C RID: 16716 RVA: 0x000DFBB9 File Offset: 0x000DDDB9
		public Attachment(Stream contentStream, string name)
			: base(contentStream)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream, name, and MIME type information. </summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the value for the <see cref="P:System.Net.Mime.ContentType.Name" /> property of the <see cref="T:System.Net.Mime.ContentType" /> associated with this attachment. This value can be null.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x0600414D RID: 16717 RVA: 0x000DFBD4 File Offset: 0x000DDDD4
		public Attachment(Stream contentStream, string name, string mediaType)
			: base(contentStream, mediaType)
		{
			this.Name = name;
		}

		/// <summary>Gets the MIME content disposition for this attachment.</summary>
		/// <returns>A <see cref="T:System.Net.Mime.ContentDisposition" /> that provides the presentation information for this attachment. </returns>
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x0600414E RID: 16718 RVA: 0x000DFBF0 File Offset: 0x000DDDF0
		public ContentDisposition ContentDisposition
		{
			get
			{
				return this.contentDisposition;
			}
		}

		/// <summary>Gets or sets the MIME content type name value in the content type associated with this attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value for the content type <paramref name="name" /> represented by the <see cref="P:System.Net.Mime.ContentType.Name" /> property.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x000DFBF8 File Offset: 0x000DDDF8
		// (set) Token: 0x06004150 RID: 16720 RVA: 0x000DFC05 File Offset: 0x000DDE05
		public string Name
		{
			get
			{
				return base.ContentType.Name;
			}
			set
			{
				base.ContentType.Name = value;
			}
		}

		/// <summary>Specifies the encoding for the <see cref="T:System.Net.Mail.Attachment" /><see cref="P:System.Net.Mail.Attachment.Name" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> value that specifies the type of name encoding. The default value is determined from the name of the attachment.</returns>
		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x000DFC13 File Offset: 0x000DDE13
		// (set) Token: 0x06004152 RID: 16722 RVA: 0x000DFC1B File Offset: 0x000DDE1B
		public Encoding NameEncoding
		{
			get
			{
				return this.nameEncoding;
			}
			set
			{
				this.nameEncoding = value;
			}
		}

		/// <summary>Creates a mail attachment using the content from the specified string, and the specified <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> object that represents the Multipurpose Internet Mail Exchange (MIME) protocol Content-Type header to be used.</param>
		// Token: 0x06004153 RID: 16723 RVA: 0x000DFC24 File Offset: 0x000DDE24
		public static Attachment CreateAttachmentFromString(string content, ContentType contentType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, contentType)
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates a mail attachment using the content from the specified string, and the specified MIME content type name.</summary>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="name">The MIME content type name value in the content type associated with this attachment.</param>
		// Token: 0x06004154 RID: 16724 RVA: 0x000DFC60 File Offset: 0x000DDE60
		public static Attachment CreateAttachmentFromString(string content, string name)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, new ContentType("text/plain"))
			{
				TransferEncoding = TransferEncoding.QuotedPrintable,
				Name = name
			};
		}

		/// <summary>Creates a mail attachment using the content from the specified string, the specified MIME content type name, character encoding, and MIME header information for the attachment.</summary>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="name">The MIME content type name value in the content type associated with this attachment.</param>
		/// <param name="contentEncoding">An <see cref="T:System.Text.Encoding" />. This value can be null.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be null.</param>
		// Token: 0x06004155 RID: 16725 RVA: 0x000DFCB8 File Offset: 0x000DDEB8
		public static Attachment CreateAttachmentFromString(string content, string name, Encoding contentEncoding, string mediaType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, contentEncoding);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, name, mediaType)
			{
				TransferEncoding = MailMessage.GuessTransferEncoding(contentEncoding),
				ContentType = 
				{
					CharSet = streamWriter.Encoding.BodyName
				}
			};
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x000DFD1E File Offset: 0x000DDF1E
		private void InitName(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.Name = Path.GetFileName(fileName);
		}

		// Token: 0x04002740 RID: 10048
		private ContentDisposition contentDisposition = new ContentDisposition();

		// Token: 0x04002741 RID: 10049
		private Encoding nameEncoding;
	}
}

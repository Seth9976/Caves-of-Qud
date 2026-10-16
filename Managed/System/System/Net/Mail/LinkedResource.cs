using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an embedded external resource in an email attachment, such as an image in an HTML attachment.</summary>
	// Token: 0x02000801 RID: 2049
	public class LinkedResource : AttachmentBase
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> using the specified file name.</summary>
		/// <param name="fileName">The file name holding the content for this embedded resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		// Token: 0x0600416F RID: 16751 RVA: 0x000E2187 File Offset: 0x000E0387
		public LinkedResource(string fileName)
			: base(fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified file name and content type.</summary>
		/// <param name="fileName">The file name that holds the content for this embedded resource.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x06004170 RID: 16752 RVA: 0x000E2199 File Offset: 0x000E0399
		public LinkedResource(string fileName, ContentType contentType)
			: base(fileName, contentType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified file name and media type.</summary>
		/// <param name="fileName">The file name that holds the content for this embedded resource.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x06004171 RID: 16753 RVA: 0x000E21AC File Offset: 0x000E03AC
		public LinkedResource(string fileName, string mediaType)
			: base(fileName, mediaType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> using the supplied <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		// Token: 0x06004172 RID: 16754 RVA: 0x000E21BF File Offset: 0x000E03BF
		public LinkedResource(Stream contentStream)
			: base(contentStream)
		{
			if (contentStream == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the values supplied by <see cref="T:System.IO.Stream" /> and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x06004173 RID: 16755 RVA: 0x000E21D1 File Offset: 0x000E03D1
		public LinkedResource(Stream contentStream, ContentType contentType)
			: base(contentStream, contentType)
		{
			if (contentStream == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified <see cref="T:System.IO.Stream" /> and media type.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x06004174 RID: 16756 RVA: 0x000E21E4 File Offset: 0x000E03E4
		public LinkedResource(Stream contentStream, string mediaType)
			: base(contentStream, mediaType)
		{
			if (contentStream == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Gets or sets a URI that the resource must match.</summary>
		/// <returns>If <see cref="P:System.Net.Mail.LinkedResource.ContentLink" /> is a relative URI, the recipient of the message must resolve it.</returns>
		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x000E21F7 File Offset: 0x000E03F7
		// (set) Token: 0x06004176 RID: 16758 RVA: 0x000E21FF File Offset: 0x000E03FF
		public Uri ContentLink
		{
			get
			{
				return this.contentLink;
			}
			set
			{
				this.contentLink = value;
			}
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource. The default media type is plain text, and the default content type is ASCII.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x06004177 RID: 16759 RVA: 0x000E2208 File Offset: 0x000E0408
		public static LinkedResource CreateLinkedResourceFromString(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			return new LinkedResource(new MemoryStream(Encoding.Default.GetBytes(content)))
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource, with the specified content type, and media type as plain text.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x06004178 RID: 16760 RVA: 0x000E222F File Offset: 0x000E042F
		public static LinkedResource CreateLinkedResourceFromString(string content, ContentType contentType)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			return new LinkedResource(new MemoryStream(Encoding.Default.GetBytes(content)), contentType)
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource, with the specified content type, and media type.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <param name="contentEncoding">The type of the content.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x06004179 RID: 16761 RVA: 0x000E2257 File Offset: 0x000E0457
		public static LinkedResource CreateLinkedResourceFromString(string content, Encoding contentEncoding, string mediaType)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			return new LinkedResource(new MemoryStream(contentEncoding.GetBytes(content)), mediaType)
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		// Token: 0x0400274D RID: 10061
		private Uri contentLink;
	}
}

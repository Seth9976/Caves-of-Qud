using System;

namespace System.Net.Mime
{
	/// <summary>Specifies the media type information for an e-mail message attachment.</summary>
	// Token: 0x020007D8 RID: 2008
	public static class MediaTypeNames
	{
		/// <summary>Specifies the type of text data in an e-mail message attachment.</summary>
		// Token: 0x020007D9 RID: 2009
		public static class Text
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in plain text format.</summary>
			// Token: 0x04002699 RID: 9881
			public const string Plain = "text/plain";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in HTML format.</summary>
			// Token: 0x0400269A RID: 9882
			public const string Html = "text/html";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in XML format.</summary>
			// Token: 0x0400269B RID: 9883
			public const string Xml = "text/xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x0400269C RID: 9884
			public const string RichText = "text/richtext";
		}

		/// <summary>Specifies the kind of application data in an e-mail message attachment.</summary>
		// Token: 0x020007DA RID: 2010
		public static class Application
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is a SOAP document.</summary>
			// Token: 0x0400269D RID: 9885
			public const string Soap = "application/soap+xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is not interpreted.</summary>
			// Token: 0x0400269E RID: 9886
			public const string Octet = "application/octet-stream";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x0400269F RID: 9887
			public const string Rtf = "application/rtf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Portable Document Format (PDF).</summary>
			// Token: 0x040026A0 RID: 9888
			public const string Pdf = "application/pdf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is compressed.</summary>
			// Token: 0x040026A1 RID: 9889
			public const string Zip = "application/zip";

			// Token: 0x040026A2 RID: 9890
			public const string Json = "application/json";

			// Token: 0x040026A3 RID: 9891
			public const string Xml = "application/xml";
		}

		/// <summary>Specifies the type of image data in an e-mail message attachment.</summary>
		// Token: 0x020007DB RID: 2011
		public static class Image
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Graphics Interchange Format (GIF).</summary>
			// Token: 0x040026A4 RID: 9892
			public const string Gif = "image/gif";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Tagged Image File Format (TIFF).</summary>
			// Token: 0x040026A5 RID: 9893
			public const string Tiff = "image/tiff";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Joint Photographic Experts Group (JPEG) format.</summary>
			// Token: 0x040026A6 RID: 9894
			public const string Jpeg = "image/jpeg";
		}
	}
}

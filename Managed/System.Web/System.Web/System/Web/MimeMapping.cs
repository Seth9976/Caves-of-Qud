using System;

namespace System.Web
{
	/// <summary>Maps document extensions to content MIME types.</summary>
	// Token: 0x020000C3 RID: 195
	public static class MimeMapping
	{
		/// <summary>Returns the MIME mapping for the specified file name.</summary>
		/// <param name="fileName">The file name that is used to determine the MIME type.</param>
		// Token: 0x06000ABF RID: 2751 RVA: 0x00019764 File Offset: 0x00017964
		public static string GetMimeMapping(string fileName)
		{
			return MimeTypes.GetMimeType(fileName);
		}
	}
}

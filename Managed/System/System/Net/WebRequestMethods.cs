using System;

namespace System.Net
{
	/// <summary>Container class for <see cref="T:System.Net.WebRequestMethods.Ftp" />, <see cref="T:System.Net.WebRequestMethods.File" />, and <see cref="T:System.Net.WebRequestMethods.Http" /> classes. This class cannot be inherited</summary>
	// Token: 0x0200061B RID: 1563
	public static class WebRequestMethods
	{
		/// <summary>Represents the types of FTP protocol methods that can be used with an FTP request. This class cannot be inherited.</summary>
		// Token: 0x0200061C RID: 1564
		public static class Ftp
		{
			/// <summary>Represents the FTP RETR protocol method that is used to download a file from an FTP server.</summary>
			// Token: 0x04001CD4 RID: 7380
			public const string DownloadFile = "RETR";

			/// <summary>Represents the FTP NLIST protocol method that gets a short listing of the files on an FTP server.</summary>
			// Token: 0x04001CD5 RID: 7381
			public const string ListDirectory = "NLST";

			/// <summary>Represents the FTP STOR protocol method that uploads a file to an FTP server.</summary>
			// Token: 0x04001CD6 RID: 7382
			public const string UploadFile = "STOR";

			/// <summary>Represents the FTP DELE protocol method that is used to delete a file on an FTP server.</summary>
			// Token: 0x04001CD7 RID: 7383
			public const string DeleteFile = "DELE";

			/// <summary>Represents the FTP APPE protocol method that is used to append a file to an existing file on an FTP server.</summary>
			// Token: 0x04001CD8 RID: 7384
			public const string AppendFile = "APPE";

			/// <summary>Represents the FTP SIZE protocol method that is used to retrieve the size of a file on an FTP server.</summary>
			// Token: 0x04001CD9 RID: 7385
			public const string GetFileSize = "SIZE";

			/// <summary>Represents the FTP STOU protocol that uploads a file with a unique name to an FTP server.</summary>
			// Token: 0x04001CDA RID: 7386
			public const string UploadFileWithUniqueName = "STOU";

			/// <summary>Represents the FTP MKD protocol method creates a directory on an FTP server.</summary>
			// Token: 0x04001CDB RID: 7387
			public const string MakeDirectory = "MKD";

			/// <summary>Represents the FTP RMD protocol method that removes a directory.</summary>
			// Token: 0x04001CDC RID: 7388
			public const string RemoveDirectory = "RMD";

			/// <summary>Represents the FTP LIST protocol method that gets a detailed listing of the files on an FTP server.</summary>
			// Token: 0x04001CDD RID: 7389
			public const string ListDirectoryDetails = "LIST";

			/// <summary>Represents the FTP MDTM protocol method that is used to retrieve the date-time stamp from a file on an FTP server.</summary>
			// Token: 0x04001CDE RID: 7390
			public const string GetDateTimestamp = "MDTM";

			/// <summary>Represents the FTP PWD protocol method that prints the name of the current working directory.</summary>
			// Token: 0x04001CDF RID: 7391
			public const string PrintWorkingDirectory = "PWD";

			/// <summary>Represents the FTP RENAME protocol method that renames a directory.</summary>
			// Token: 0x04001CE0 RID: 7392
			public const string Rename = "RENAME";
		}

		/// <summary>Represents the types of HTTP protocol methods that can be used with an HTTP request.</summary>
		// Token: 0x0200061D RID: 1565
		public static class Http
		{
			/// <summary>Represents an HTTP GET protocol method. </summary>
			// Token: 0x04001CE1 RID: 7393
			public const string Get = "GET";

			/// <summary>Represents the HTTP CONNECT protocol method that is used with a proxy that can dynamically switch to tunneling, as in the case of SSL tunneling.</summary>
			// Token: 0x04001CE2 RID: 7394
			public const string Connect = "CONNECT";

			/// <summary>Represents an HTTP HEAD protocol method. The HEAD method is identical to GET except that the server only returns message-headers in the response, without a message-body.</summary>
			// Token: 0x04001CE3 RID: 7395
			public const string Head = "HEAD";

			/// <summary>Represents an HTTP PUT protocol method that is used to replace an entity identified by a URI.</summary>
			// Token: 0x04001CE4 RID: 7396
			public const string Put = "PUT";

			/// <summary>Represents an HTTP POST protocol method that is used to post a new entity as an addition to a URI.</summary>
			// Token: 0x04001CE5 RID: 7397
			public const string Post = "POST";

			/// <summary>Represents an HTTP MKCOL request that creates a new collection (such as a collection of pages) at the location specified by the request-Uniform Resource Identifier (URI).</summary>
			// Token: 0x04001CE6 RID: 7398
			public const string MkCol = "MKCOL";
		}

		/// <summary>Represents the types of file protocol methods that can be used with a FILE request. This class cannot be inherited.</summary>
		// Token: 0x0200061E RID: 1566
		public static class File
		{
			/// <summary>Represents the FILE GET protocol method that is used to retrieve a file from a specified location.</summary>
			// Token: 0x04001CE7 RID: 7399
			public const string DownloadFile = "GET";

			/// <summary>Represents the FILE PUT protocol method that is used to copy a file to a specified location.</summary>
			// Token: 0x04001CE8 RID: 7400
			public const string UploadFile = "PUT";
		}
	}
}

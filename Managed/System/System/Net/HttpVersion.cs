using System;

namespace System.Net
{
	/// <summary>Defines the HTTP version numbers that are supported by the <see cref="T:System.Net.HttpWebRequest" /> and <see cref="T:System.Net.HttpWebResponse" /> classes.</summary>
	// Token: 0x0200057E RID: 1406
	public class HttpVersion
	{
		// Token: 0x04001902 RID: 6402
		public static readonly Version Unknown = new Version(0, 0);

		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.0.</summary>
		// Token: 0x04001903 RID: 6403
		public static readonly Version Version10 = new Version(1, 0);

		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.1.</summary>
		// Token: 0x04001904 RID: 6404
		public static readonly Version Version11 = new Version(1, 1);

		// Token: 0x04001905 RID: 6405
		public static readonly Version Version20 = new Version(2, 0);
	}
}

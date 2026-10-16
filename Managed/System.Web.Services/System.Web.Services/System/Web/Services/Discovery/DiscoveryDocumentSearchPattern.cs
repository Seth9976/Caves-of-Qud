using System;

namespace System.Web.Services.Discovery
{
	/// <summary>Obtains the file locations of Web services discovery documents for use in populating another Web services discovery document. This class cannot be inherited.</summary>
	// Token: 0x020000A7 RID: 167
	public sealed class DiscoveryDocumentSearchPattern : DiscoverySearchPattern
	{
		/// <summary>Gets the file name pattern to use as a search target.</summary>
		/// <returns>The literal string "*.vsdisco".</returns>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00014204 File Offset: 0x00012404
		public override string Pattern
		{
			get
			{
				return "*.vsdisco";
			}
		}

		/// <summary>Returns the <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentReference" /> object for a given discovery document.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentReference" /> object that specifies the location of a .vsdisco file.</returns>
		/// <param name="filename">The file system path of the discovery document.</param>
		// Token: 0x06000457 RID: 1111 RVA: 0x00013BD7 File Offset: 0x00011DD7
		public override DiscoveryReference GetDiscoveryReference(string filename)
		{
			return new DiscoveryDocumentReference(filename);
		}
	}
}

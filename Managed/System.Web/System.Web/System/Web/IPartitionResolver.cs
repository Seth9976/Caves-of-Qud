using System;

namespace System.Web
{
	/// <summary>Defines methods that must be implemented for custom session-state partition resolution.</summary>
	// Token: 0x020000BF RID: 191
	public interface IPartitionResolver
	{
		/// <summary>Initializes the custom partition resolver. </summary>
		// Token: 0x06000A91 RID: 2705
		void Initialize();

		/// <summary>Resolves the partition based on a key parameter.</summary>
		/// <returns>A string with connection information.</returns>
		/// <param name="key">An identifier used to determine which partition to use for the current session state.</param>
		// Token: 0x06000A92 RID: 2706
		string ResolvePartition(object key);
	}
}

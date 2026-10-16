using System;

namespace System
{
	/// <summary>A parser based on the NetPipe scheme for the "Indigo" system.</summary>
	// Token: 0x02000167 RID: 359
	public class NetPipeStyleUriParser : UriParser
	{
		/// <summary>Create a parser based on the NetPipe scheme for the "Indigo" system.</summary>
		// Token: 0x06000999 RID: 2457 RVA: 0x0002A2FD File Offset: 0x000284FD
		public NetPipeStyleUriParser()
			: base(UriParser.NetPipeUri.Flags)
		{
		}
	}
}

using System;

namespace System
{
	/// <summary>A parser based on the NetTcp scheme for the "Indigo" system.</summary>
	// Token: 0x02000168 RID: 360
	public class NetTcpStyleUriParser : UriParser
	{
		/// <summary>Create a parser based on the NetTcp scheme for the "Indigo" system.</summary>
		// Token: 0x0600099A RID: 2458 RVA: 0x0002A30F File Offset: 0x0002850F
		public NetTcpStyleUriParser()
			: base(UriParser.NetTcpUri.Flags)
		{
		}
	}
}

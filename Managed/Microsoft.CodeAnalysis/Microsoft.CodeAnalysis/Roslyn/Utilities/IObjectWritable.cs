using System;

namespace Roslyn.Utilities
{
	// Token: 0x02000070 RID: 112
	internal interface IObjectWritable
	{
		// Token: 0x06000299 RID: 665
		void WriteTo(ObjectWriter writer);

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600029A RID: 666
		bool ShouldReuseInSerialization { get; }
	}
}

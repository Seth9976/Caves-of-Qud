using System;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Defines method body attributes.</summary>
	// Token: 0x020000DF RID: 223
	[Flags]
	public enum MethodBodyAttributes
	{
		/// <summary>Performs no local memory initialization.</summary>
		// Token: 0x040004EF RID: 1263
		None = 0,
		/// <summary>Initializes any locals the method defines to zero and dynamically allocates local memory.</summary>
		// Token: 0x040004F0 RID: 1264
		InitLocals = 1
	}
}

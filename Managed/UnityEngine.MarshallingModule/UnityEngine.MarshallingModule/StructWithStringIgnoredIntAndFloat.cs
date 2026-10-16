using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	[ExcludeFromDocs]
	internal struct StructWithStringIgnoredIntAndFloat
	{
		// Token: 0x04000025 RID: 37
		public string a;

		// Token: 0x04000026 RID: 38
		[Ignore]
		public int b;

		// Token: 0x04000027 RID: 39
		public float c;
	}
}

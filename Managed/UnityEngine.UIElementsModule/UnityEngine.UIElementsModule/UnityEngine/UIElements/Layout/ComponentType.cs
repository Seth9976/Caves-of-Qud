using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006A5 RID: 1701
	internal struct ComponentType
	{
		// Token: 0x060031D6 RID: 12758 RVA: 0x000BBE4C File Offset: 0x000BA04C
		public static ComponentType Create<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
		{
			return new ComponentType
			{
				Size = UnsafeUtility.SizeOf<T>()
			};
		}

		// Token: 0x04001861 RID: 6241
		public int Size;
	}
}

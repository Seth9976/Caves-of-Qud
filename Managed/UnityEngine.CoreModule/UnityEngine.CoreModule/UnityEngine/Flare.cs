using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000195 RID: 405
	[NativeHeader("Runtime/Camera/Flare.h")]
	public sealed class Flare : Object
	{
		// Token: 0x060013AE RID: 5038 RVA: 0x00022098 File Offset: 0x00020298
		public Flare()
		{
			Flare.Internal_Create(this);
		}

		// Token: 0x060013AF RID: 5039
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Flare self);
	}
}

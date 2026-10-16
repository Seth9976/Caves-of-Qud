using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal struct StructUnityObject
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00002B40 File Offset: 0x00000D40
		public int InstanceMethod([NotNull] object o)
		{
			if (o == null)
			{
				ThrowHelper.ThrowArgumentNullException(o, "o");
			}
			return StructUnityObject.InstanceMethod_Injected(ref this, o);
		}

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InstanceMethod_Injected(ref StructUnityObject _unity_self, object o);

		// Token: 0x0400000D RID: 13
		public MarshallingTestObject field;
	}
}

using System;
using UnityEngine.Scripting;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000BC RID: 188
	[Obsolete("Use NativeSetThreadIndexAttribute instead")]
	[AttributeUsage(AttributeTargets.Struct)]
	[RequiredByNativeCode]
	public sealed class NativeContainerNeedsThreadIndexAttribute : Attribute
	{
	}
}

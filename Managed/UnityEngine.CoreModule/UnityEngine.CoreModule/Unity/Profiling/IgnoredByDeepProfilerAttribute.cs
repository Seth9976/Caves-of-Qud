using System;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x02000062 RID: 98
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method, AllowMultiple = false)]
	public sealed class IgnoredByDeepProfilerAttribute : Attribute
	{
	}
}

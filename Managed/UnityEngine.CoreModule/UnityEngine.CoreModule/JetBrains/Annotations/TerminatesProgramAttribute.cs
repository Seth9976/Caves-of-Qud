using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E9 RID: 233
	[AttributeUsage(AttributeTargets.Method)]
	[Obsolete("Use [ContractAnnotation('=> halt')] instead")]
	public sealed class TerminatesProgramAttribute : Attribute
	{
	}
}

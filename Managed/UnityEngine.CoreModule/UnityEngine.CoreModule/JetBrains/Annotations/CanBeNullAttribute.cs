using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000CB RID: 203
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate | AttributeTargets.GenericParameter)]
	public sealed class CanBeNullAttribute : Attribute
	{
	}
}

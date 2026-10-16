using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007DB RID: 2011
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncMethodBuilderAttribute : Attribute
	{
		// Token: 0x060045C4 RID: 17860 RVA: 0x000E5223 File Offset: 0x000E3423
		public AsyncMethodBuilderAttribute(Type builderType)
		{
			this.BuilderType = builderType;
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x000E5232 File Offset: 0x000E3432
		public Type BuilderType { get; }
	}
}

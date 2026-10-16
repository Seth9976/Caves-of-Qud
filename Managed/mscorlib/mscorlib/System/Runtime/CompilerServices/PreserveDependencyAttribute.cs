using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000851 RID: 2129
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class PreserveDependencyAttribute : Attribute
	{
		// Token: 0x060046EF RID: 18159 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveDependencyAttribute(string memberSignature)
		{
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveDependencyAttribute(string memberSignature, string typeName)
		{
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveDependencyAttribute(string memberSignature, string typeName, string assembly)
		{
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060046F2 RID: 18162 RVA: 0x000E7DFA File Offset: 0x000E5FFA
		// (set) Token: 0x060046F3 RID: 18163 RVA: 0x000E7E02 File Offset: 0x000E6002
		public string Condition { get; set; }
	}
}

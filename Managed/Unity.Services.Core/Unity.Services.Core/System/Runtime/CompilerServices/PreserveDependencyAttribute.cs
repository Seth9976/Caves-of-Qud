using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class PreserveDependencyAttribute : Attribute
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public PreserveDependencyAttribute(string memberSignature)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public PreserveDependencyAttribute(string memberSignature, string typeName)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public PreserveDependencyAttribute(string memberSignature, string typeName, string assembly)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		public string Condition { get; set; }
	}
}

using System;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity
{
	// Token: 0x0200000A RID: 10
	public class IllegalAssemblyReference
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D96 File Offset: 0x00000F96
		public AssemblyNameReference ReferencedAssemebly
		{
			get
			{
				return this.assemblyName;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D9E File Offset: 0x00000F9E
		public IllegalReferenceUsage IllegalUsage
		{
			get
			{
				return this.illegalUsage;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public IllegalAssemblyReference(AssemblyNameReference illegalAssemblyName, ModuleDefinition module)
		{
			this.assemblyName = illegalAssemblyName;
			this.illegalUsage = new IllegalReferenceUsage(module);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DC1 File Offset: 0x00000FC1
		public override string ToString()
		{
			return string.Format("Illegal reference to disallowed reference assembly: {0}", this.assemblyName);
		}

		// Token: 0x04000022 RID: 34
		private AssemblyNameReference assemblyName;

		// Token: 0x04000023 RID: 35
		private IllegalReferenceUsage illegalUsage;
	}
}

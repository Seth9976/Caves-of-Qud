using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x0200001F RID: 31
	internal class InMemoryHandler : ResXDataNodeHandler
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public InMemoryHandler(object valueObject)
		{
			this.value = valueObject;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003BC3 File Offset: 0x00001DC3
		public override object GetValue(ITypeResolutionService typeResolver)
		{
			return this.value;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003BC3 File Offset: 0x00001DC3
		public override object GetValue(AssemblyName[] assemblyNames)
		{
			return this.value;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003BCB File Offset: 0x00001DCB
		public override string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			if (this.value == null)
			{
				return null;
			}
			return this.value.GetType().AssemblyQualifiedName;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003BCB File Offset: 0x00001DCB
		public override string GetValueTypeName(AssemblyName[] assemblyNames)
		{
			if (this.value == null)
			{
				return null;
			}
			return this.value.GetType().AssemblyQualifiedName;
		}

		// Token: 0x04000D67 RID: 3431
		private object value;
	}
}

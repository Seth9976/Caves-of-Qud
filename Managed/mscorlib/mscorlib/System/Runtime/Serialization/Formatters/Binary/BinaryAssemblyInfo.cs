using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000694 RID: 1684
	internal sealed class BinaryAssemblyInfo
	{
		// Token: 0x06003E12 RID: 15890 RVA: 0x000D6192 File Offset: 0x000D4392
		internal BinaryAssemblyInfo(string assemblyString)
		{
			this.assemblyString = assemblyString;
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x000D61A1 File Offset: 0x000D43A1
		internal BinaryAssemblyInfo(string assemblyString, Assembly assembly)
		{
			this.assemblyString = assemblyString;
			this.assembly = assembly;
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x000D61B8 File Offset: 0x000D43B8
		internal Assembly GetAssembly()
		{
			if (this.assembly == null)
			{
				this.assembly = FormatterServices.LoadAssemblyFromStringNoThrow(this.assemblyString);
				if (this.assembly == null)
				{
					throw new SerializationException(Environment.GetResourceString("Unable to find assembly '{0}'.", new object[] { this.assemblyString }));
				}
			}
			return this.assembly;
		}

		// Token: 0x04002834 RID: 10292
		internal string assemblyString;

		// Token: 0x04002835 RID: 10293
		private Assembly assembly;
	}
}

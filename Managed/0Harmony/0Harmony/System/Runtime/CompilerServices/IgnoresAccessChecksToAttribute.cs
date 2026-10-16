using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200042B RID: 1067
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal class IgnoresAccessChecksToAttribute : Attribute
	{
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00048936 File Offset: 0x00046B36
		public string AssemblyName { get; }

		// Token: 0x06001678 RID: 5752 RVA: 0x0004893E File Offset: 0x00046B3E
		public IgnoresAccessChecksToAttribute(string assemblyName)
		{
			this.AssemblyName = assemblyName;
		}
	}
}

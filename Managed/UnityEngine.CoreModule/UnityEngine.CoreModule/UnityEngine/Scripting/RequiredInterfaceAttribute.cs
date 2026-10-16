using System;

namespace UnityEngine.Scripting
{
	// Token: 0x02000376 RID: 886
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true)]
	public class RequiredInterfaceAttribute : Attribute
	{
		// Token: 0x060028DF RID: 10463 RVA: 0x00002082 File Offset: 0x00000282
		public RequiredInterfaceAttribute(Type interfaceType)
		{
		}
	}
}

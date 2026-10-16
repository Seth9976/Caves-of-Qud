using System;

namespace NUnit.Framework
{
	// Token: 0x02000042 RID: 66
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class TestOfAttribute : PropertyAttribute
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00005D95 File Offset: 0x00003F95
		public TestOfAttribute(Type type)
			: base("TestOf", type.FullName)
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public TestOfAttribute(string typeName)
			: base("TestOf", typeName)
		{
		}
	}
}

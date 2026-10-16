using System;
using System.Diagnostics;

namespace Occult.Engine.CodeGeneration
{
	// Token: 0x020004AC RID: 1196
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[Conditional("UNITY_EDITOR")]
	public class GeneratedPartial : Attribute
	{
		// Token: 0x04001A0F RID: 6671
		public ulong Hash;
	}
}

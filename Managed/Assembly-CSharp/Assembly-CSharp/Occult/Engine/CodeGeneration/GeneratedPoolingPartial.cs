using System;
using System.Reflection;

namespace Occult.Engine.CodeGeneration
{
	// Token: 0x020004B2 RID: 1202
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class GeneratedPoolingPartial : GeneratedPartial
	{
		// Token: 0x06002DE7 RID: 11751 RVA: 0x000FAF1F File Offset: 0x000F911F
		public static ulong GetHashCode(Type Type)
		{
			return Type.GetStableHashCode64(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, FieldAttributes.Static | FieldAttributes.Literal);
		}
	}
}

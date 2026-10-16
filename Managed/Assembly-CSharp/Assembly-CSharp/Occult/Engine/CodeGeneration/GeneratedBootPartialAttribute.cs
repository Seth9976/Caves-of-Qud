using System;
using System.Reflection;
using XRL.Serialization;

namespace Occult.Engine.CodeGeneration
{
	// Token: 0x020004B0 RID: 1200
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class GeneratedBootPartialAttribute : GeneratedPartial
	{
		// Token: 0x06002DE4 RID: 11748 RVA: 0x000FAF00 File Offset: 0x000F9100
		public static ulong GetHashCode(Type Type)
		{
			return FastSerialization.GetFieldSaveHash(Type, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}
	}
}

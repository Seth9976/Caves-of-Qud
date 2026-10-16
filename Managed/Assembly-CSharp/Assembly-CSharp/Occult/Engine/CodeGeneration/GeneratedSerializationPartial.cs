using System;
using System.Reflection;
using XRL.Serialization;

namespace Occult.Engine.CodeGeneration
{
	// Token: 0x020004AF RID: 1199
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class GeneratedSerializationPartial : GeneratedPartial
	{
		// Token: 0x06002DE2 RID: 11746 RVA: 0x000FAEE9 File Offset: 0x000F90E9
		public static ulong GetHashCode(Type Type)
		{
			return FastSerialization.GetFieldSaveHash(Type, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}
	}
}

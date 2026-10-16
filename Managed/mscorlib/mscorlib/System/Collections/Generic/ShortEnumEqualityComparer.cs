using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000ACC RID: 2764
	[Serializable]
	internal sealed class ShortEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060062BA RID: 25274 RVA: 0x0014A4D8 File Offset: 0x001486D8
		public ShortEnumEqualityComparer()
		{
		}

		// Token: 0x060062BB RID: 25275 RVA: 0x0014A4D8 File Offset: 0x001486D8
		public ShortEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x0014A4FC File Offset: 0x001486FC
		public override int GetHashCode(T obj)
		{
			return ((short)JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
		}
	}
}

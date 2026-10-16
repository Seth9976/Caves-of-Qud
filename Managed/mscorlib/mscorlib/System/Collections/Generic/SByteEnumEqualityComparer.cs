using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000ACB RID: 2763
	[Serializable]
	internal sealed class SByteEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060062B7 RID: 25271 RVA: 0x0014A4D8 File Offset: 0x001486D8
		public SByteEnumEqualityComparer()
		{
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x0014A4D8 File Offset: 0x001486D8
		public SByteEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060062B9 RID: 25273 RVA: 0x0014A4E0 File Offset: 0x001486E0
		public override int GetHashCode(T obj)
		{
			return ((sbyte)JitHelpers.UnsafeEnumCast<T>(obj)).GetHashCode();
		}
	}
}

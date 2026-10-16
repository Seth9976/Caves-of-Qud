using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000ACD RID: 2765
	[Serializable]
	internal sealed class LongEnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x060062BD RID: 25277 RVA: 0x0014A518 File Offset: 0x00148718
		public override bool Equals(T x, T y)
		{
			long num = JitHelpers.UnsafeEnumCastLong<T>(x);
			long num2 = JitHelpers.UnsafeEnumCastLong<T>(y);
			return num == num2;
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x0014A538 File Offset: 0x00148738
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCastLong<T>(obj).GetHashCode();
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x0014A553 File Offset: 0x00148753
		public override bool Equals(object obj)
		{
			return obj is LongEnumEqualityComparer<T>;
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x00149CE6 File Offset: 0x00147EE6
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x0014A10D File Offset: 0x0014830D
		public LongEnumEqualityComparer()
		{
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x0014A10D File Offset: 0x0014830D
		public LongEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x060062C3 RID: 25283 RVA: 0x0014A55E File Offset: 0x0014875E
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(ObjectEqualityComparer<T>));
		}
	}
}

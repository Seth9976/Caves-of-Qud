using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000166 RID: 358
	[NativeType("Runtime/Graphics/RefreshRate.h")]
	public struct RefreshRate : IEquatable<RefreshRate>, IComparable<RefreshRate>
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x000155D8 File Offset: 0x000137D8
		public double value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.numerator / this.denominator;
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000155EC File Offset: 0x000137EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(RefreshRate other)
		{
			bool flag = this.denominator == 0U;
			bool flag2;
			if (flag)
			{
				flag2 = other.denominator == 0U;
			}
			else
			{
				bool flag3 = other.denominator == 0U;
				flag2 = !flag3 && (ulong)this.numerator * (ulong)other.denominator == (ulong)this.denominator * (ulong)other.numerator;
			}
			return flag2;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00015648 File Offset: 0x00013848
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int CompareTo(RefreshRate other)
		{
			bool flag = this.denominator == 0U;
			int num;
			if (flag)
			{
				num = ((other.denominator == 0U) ? 0 : 1);
			}
			else
			{
				bool flag2 = other.denominator == 0U;
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = ((ulong)this.numerator * (ulong)other.denominator).CompareTo((ulong)this.denominator * (ulong)other.numerator);
				}
			}
			return num;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000156B0 File Offset: 0x000138B0
		public override string ToString()
		{
			return this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x04000499 RID: 1177
		[RequiredMember]
		public uint numerator;

		// Token: 0x0400049A RID: 1178
		[RequiredMember]
		public uint denominator;
	}
}

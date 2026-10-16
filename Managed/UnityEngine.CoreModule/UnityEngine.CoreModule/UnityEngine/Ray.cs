using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200014F RID: 335
	public struct Ray : IFormattable
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x000125F3 File Offset: 0x000107F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Ray(Vector3 origin, Vector3 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0001260C File Offset: 0x0001080C
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x00012624 File Offset: 0x00010824
		public Vector3 origin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Origin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Origin = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00012630 File Offset: 0x00010830
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00012648 File Offset: 0x00010848
		public Vector3 direction
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Direction;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Direction = value.normalized;
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00012658 File Offset: 0x00010858
		public Vector3 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00012684 File Offset: 0x00010884
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000126A0 File Offset: 0x000108A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000126BC File Offset: 0x000108BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format, formatProvider),
				this.m_Direction.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400046D RID: 1133
		private Vector3 m_Origin;

		// Token: 0x0400046E RID: 1134
		private Vector3 m_Direction;
	}
}

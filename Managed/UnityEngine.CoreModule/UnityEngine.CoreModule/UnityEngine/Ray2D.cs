using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000150 RID: 336
	public struct Ray2D : IFormattable
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x00012723 File Offset: 0x00010923
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Ray2D(Vector2 origin, Vector2 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0001273C File Offset: 0x0001093C
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00012754 File Offset: 0x00010954
		public Vector2 origin
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

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00012760 File Offset: 0x00010960
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x00012778 File Offset: 0x00010978
		public Vector2 direction
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

		// Token: 0x06000A10 RID: 2576 RVA: 0x00012788 File Offset: 0x00010988
		public Vector2 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000127B4 File Offset: 0x000109B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000127D0 File Offset: 0x000109D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x000127EC File Offset: 0x000109EC
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

		// Token: 0x0400046F RID: 1135
		private Vector2 m_Origin;

		// Token: 0x04000470 RID: 1136
		private Vector2 m_Direction;
	}
}

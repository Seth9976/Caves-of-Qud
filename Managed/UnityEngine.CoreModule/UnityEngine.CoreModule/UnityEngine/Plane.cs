using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014E RID: 334
	[UsedByNativeCode]
	public struct Plane : IFormattable
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x000122BC File Offset: 0x000104BC
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x000122D4 File Offset: 0x000104D4
		public Vector3 normal
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Normal;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x000122E0 File Offset: 0x000104E0
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x000122F8 File Offset: 0x000104F8
		public float distance
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Distance;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00012302 File Offset: 0x00010502
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(this.m_Normal, inPoint);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00012324 File Offset: 0x00010524
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(Vector3 inNormal, float d)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = d;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0001233A File Offset: 0x0001053A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00012302 File Offset: 0x00010502
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetNormalAndPosition(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(this.m_Normal, inPoint);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001233A File Offset: 0x0001053A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set3Points(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001236E File Offset: 0x0001056E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Flip()
		{
			this.m_Normal = -this.m_Normal;
			this.m_Distance = -this.m_Distance;
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00012390 File Offset: 0x00010590
		public Plane flipped
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Plane(-this.m_Normal, -this.m_Distance);
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000123B9 File Offset: 0x000105B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Translate(Vector3 translation)
		{
			this.m_Distance += Vector3.Dot(this.m_Normal, translation);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000123D8 File Offset: 0x000105D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Translate(Plane plane, Vector3 translation)
		{
			return new Plane(plane.m_Normal, plane.m_Distance += Vector3.Dot(plane.m_Normal, translation));
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00012410 File Offset: 0x00010610
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 ClosestPointOnPlane(Vector3 point)
		{
			float num = Vector3.Dot(this.m_Normal, point) + this.m_Distance;
			return point - this.m_Normal * num;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00012448 File Offset: 0x00010648
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetDistanceToPoint(Vector3 point)
		{
			return Vector3.Dot(this.m_Normal, point) + this.m_Distance;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00012470 File Offset: 0x00010670
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool GetSide(Vector3 point)
		{
			return Vector3.Dot(this.m_Normal, point) + this.m_Distance > 0f;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001249C File Offset: 0x0001069C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool SameSide(Vector3 inPt0, Vector3 inPt1)
		{
			float distanceToPoint = this.GetDistanceToPoint(inPt0);
			float distanceToPoint2 = this.GetDistanceToPoint(inPt1);
			return (distanceToPoint > 0f && distanceToPoint2 > 0f) || (distanceToPoint <= 0f && distanceToPoint2 <= 0f);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000124E8 File Offset: 0x000106E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Raycast(Ray ray, out float enter)
		{
			float num = Vector3.Dot(ray.direction, this.m_Normal);
			float num2 = -Vector3.Dot(ray.origin, this.m_Normal) - this.m_Distance;
			bool flag = Mathf.Approximately(num, 0f);
			bool flag2;
			if (flag)
			{
				enter = 0f;
				flag2 = false;
			}
			else
			{
				enter = num2 / num;
				flag2 = enter > 0f;
			}
			return flag2;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00012554 File Offset: 0x00010754
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00012570 File Offset: 0x00010770
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0001258C File Offset: 0x0001078C
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
			return UnityString.Format("(normal:{0}, distance:{1})", new object[]
			{
				this.m_Normal.ToString(format, formatProvider),
				this.m_Distance.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400046A RID: 1130
		internal const int size = 16;

		// Token: 0x0400046B RID: 1131
		private Vector3 m_Normal;

		// Token: 0x0400046C RID: 1132
		private float m_Distance;
	}
}

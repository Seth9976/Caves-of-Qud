using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014A RID: 330
	[NativeHeader("Runtime/Geometry/AABB.h")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[NativeHeader("Runtime/Geometry/Intersection.h")]
	[NativeClass("AABB")]
	[NativeType(Header = "Runtime/Geometry/AABB.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Runtime/Geometry/Ray.h")]
	public struct Bounds : IEquatable<Bounds>, IFormattable
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x0001105F File Offset: 0x0000F25F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Bounds(Vector3 center, Vector3 size)
		{
			this.m_Center = center;
			this.m_Extents = size * 0.5f;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001107C File Offset: 0x0000F27C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.center.GetHashCode() ^ (this.extents.GetHashCode() << 2);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000110BC File Offset: 0x0000F2BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Bounds);
			return !flag && this.Equals((Bounds)other);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x000110F0 File Offset: 0x0000F2F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Bounds other)
		{
			return this.center.Equals(other.center) && this.extents.Equals(other.extents);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00011134 File Offset: 0x0000F334
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x0001114C File Offset: 0x0000F34C
		public Vector3 center
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Center;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00011158 File Offset: 0x0000F358
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x0001117A File Offset: 0x0000F37A
		public Vector3 size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Extents * 2f;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Extents = value * 0.5f;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00011190 File Offset: 0x0000F390
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x000111A8 File Offset: 0x0000F3A8
		public Vector3 extents
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Extents;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Extents = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x000111B4 File Offset: 0x0000F3B4
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x000111D7 File Offset: 0x0000F3D7
		public Vector3 min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.center - this.extents;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.SetMinMax(value, this.max);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x000111E8 File Offset: 0x0000F3E8
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0001120B File Offset: 0x0000F40B
		public Vector3 max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.center + this.extents;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.SetMinMax(this.min, value);
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001121C File Offset: 0x0000F41C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Bounds lhs, Bounds rhs)
		{
			return lhs.center == rhs.center && lhs.extents == rhs.extents;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001125C File Offset: 0x0000F45C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Bounds lhs, Bounds rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00011278 File Offset: 0x0000F478
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this.extents = (max - min) * 0.5f;
			this.center = min + this.extents;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000112A6 File Offset: 0x0000F4A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(Vector3 point)
		{
			this.SetMinMax(Vector3.Min(this.min, point), Vector3.Max(this.max, point));
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x000112C8 File Offset: 0x0000F4C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(Bounds bounds)
		{
			this.Encapsulate(bounds.center - bounds.extents);
			this.Encapsulate(bounds.center + bounds.extents);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000112FF File Offset: 0x0000F4FF
		public void Expand(float amount)
		{
			amount *= 0.5f;
			this.extents += new Vector3(amount, amount, amount);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00011325 File Offset: 0x0000F525
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Expand(Vector3 amount)
		{
			this.extents += amount * 0.5f;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00011348 File Offset: 0x0000F548
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Intersects(Bounds bounds)
		{
			return this.min.x <= bounds.max.x && this.max.x >= bounds.min.x && this.min.y <= bounds.max.y && this.max.y >= bounds.min.y && this.min.z <= bounds.max.z && this.max.z >= bounds.min.z;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000113FC File Offset: 0x0000F5FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IntersectRay(Ray ray)
		{
			float num;
			return Bounds.IntersectRayAABB(ray, this, out num);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001141C File Offset: 0x0000F61C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IntersectRay(Ray ray, out float distance)
		{
			return Bounds.IntersectRayAABB(ray, this, out distance);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001143C File Offset: 0x0000F63C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00011458 File Offset: 0x0000F658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00011474 File Offset: 0x0000F674
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
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center.ToString(format, formatProvider),
				this.m_Extents.ToString(format, formatProvider)
			});
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000114DC File Offset: 0x0000F6DC
		[NativeMethod("IsInside", IsThreadSafe = true)]
		public bool Contains(Vector3 point)
		{
			return Bounds.Contains_Injected(ref this, ref point);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000114F4 File Offset: 0x0000F6F4
		[FreeFunction("BoundsScripting::SqrDistance", HasExplicitThis = true, IsThreadSafe = true)]
		public float SqrDistance(Vector3 point)
		{
			return Bounds.SqrDistance_Injected(ref this, ref point);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001150C File Offset: 0x0000F70C
		[FreeFunction("IntersectRayAABB", IsThreadSafe = true)]
		private static bool IntersectRayAABB(Ray ray, Bounds bounds, out float dist)
		{
			return Bounds.IntersectRayAABB_Injected(ref ray, ref bounds, out dist);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00011524 File Offset: 0x0000F724
		[FreeFunction("BoundsScripting::ClosestPoint", HasExplicitThis = true, IsThreadSafe = true)]
		public Vector3 ClosestPoint(Vector3 point)
		{
			Vector3 vector;
			Bounds.ClosestPoint_Injected(ref this, ref point, out vector);
			return vector;
		}

		// Token: 0x060009AC RID: 2476
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Contains_Injected(ref Bounds _unity_self, [In] ref Vector3 point);

		// Token: 0x060009AD RID: 2477
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float SqrDistance_Injected(ref Bounds _unity_self, [In] ref Vector3 point);

		// Token: 0x060009AE RID: 2478
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IntersectRayAABB_Injected([In] ref Ray ray, [In] ref Bounds bounds, out float dist);

		// Token: 0x060009AF RID: 2479
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClosestPoint_Injected(ref Bounds _unity_self, [In] ref Vector3 point, out Vector3 ret);

		// Token: 0x04000463 RID: 1123
		private Vector3 m_Center;

		// Token: 0x04000464 RID: 1124
		[NativeName("m_Extent")]
		private Vector3 m_Extents;
	}
}

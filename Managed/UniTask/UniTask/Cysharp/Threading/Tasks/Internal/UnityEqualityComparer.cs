using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000118 RID: 280
	internal static class UnityEqualityComparer
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0000EB65 File Offset: 0x0000CD65
		public static IEqualityComparer<T> GetDefault<T>()
		{
			return UnityEqualityComparer.Cache<T>.Comparer;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		private static object GetDefaultHelper(Type type)
		{
			RuntimeTypeHandle typeHandle = type.TypeHandle;
			if (typeHandle.Equals(UnityEqualityComparer.vector2Type))
			{
				return UnityEqualityComparer.Vector2;
			}
			if (typeHandle.Equals(UnityEqualityComparer.vector3Type))
			{
				return UnityEqualityComparer.Vector3;
			}
			if (typeHandle.Equals(UnityEqualityComparer.vector4Type))
			{
				return UnityEqualityComparer.Vector4;
			}
			if (typeHandle.Equals(UnityEqualityComparer.colorType))
			{
				return UnityEqualityComparer.Color;
			}
			if (typeHandle.Equals(UnityEqualityComparer.color32Type))
			{
				return UnityEqualityComparer.Color32;
			}
			if (typeHandle.Equals(UnityEqualityComparer.rectType))
			{
				return UnityEqualityComparer.Rect;
			}
			if (typeHandle.Equals(UnityEqualityComparer.boundsType))
			{
				return UnityEqualityComparer.Bounds;
			}
			if (typeHandle.Equals(UnityEqualityComparer.quaternionType))
			{
				return UnityEqualityComparer.Quaternion;
			}
			if (typeHandle.Equals(UnityEqualityComparer.vector2IntType))
			{
				return UnityEqualityComparer.Vector2Int;
			}
			if (typeHandle.Equals(UnityEqualityComparer.vector3IntType))
			{
				return UnityEqualityComparer.Vector3Int;
			}
			if (typeHandle.Equals(UnityEqualityComparer.rangeIntType))
			{
				return UnityEqualityComparer.RangeInt;
			}
			if (typeHandle.Equals(UnityEqualityComparer.rectIntType))
			{
				return UnityEqualityComparer.RectInt;
			}
			if (typeHandle.Equals(UnityEqualityComparer.boundsIntType))
			{
				return UnityEqualityComparer.BoundsInt;
			}
			return null;
		}

		// Token: 0x04000129 RID: 297
		public static readonly IEqualityComparer<Vector2> Vector2 = new UnityEqualityComparer.Vector2EqualityComparer();

		// Token: 0x0400012A RID: 298
		public static readonly IEqualityComparer<Vector3> Vector3 = new UnityEqualityComparer.Vector3EqualityComparer();

		// Token: 0x0400012B RID: 299
		public static readonly IEqualityComparer<Vector4> Vector4 = new UnityEqualityComparer.Vector4EqualityComparer();

		// Token: 0x0400012C RID: 300
		public static readonly IEqualityComparer<Color> Color = new UnityEqualityComparer.ColorEqualityComparer();

		// Token: 0x0400012D RID: 301
		public static readonly IEqualityComparer<Color32> Color32 = new UnityEqualityComparer.Color32EqualityComparer();

		// Token: 0x0400012E RID: 302
		public static readonly IEqualityComparer<Rect> Rect = new UnityEqualityComparer.RectEqualityComparer();

		// Token: 0x0400012F RID: 303
		public static readonly IEqualityComparer<Bounds> Bounds = new UnityEqualityComparer.BoundsEqualityComparer();

		// Token: 0x04000130 RID: 304
		public static readonly IEqualityComparer<Quaternion> Quaternion = new UnityEqualityComparer.QuaternionEqualityComparer();

		// Token: 0x04000131 RID: 305
		private static readonly RuntimeTypeHandle vector2Type = typeof(Vector2).TypeHandle;

		// Token: 0x04000132 RID: 306
		private static readonly RuntimeTypeHandle vector3Type = typeof(Vector3).TypeHandle;

		// Token: 0x04000133 RID: 307
		private static readonly RuntimeTypeHandle vector4Type = typeof(Vector4).TypeHandle;

		// Token: 0x04000134 RID: 308
		private static readonly RuntimeTypeHandle colorType = typeof(Color).TypeHandle;

		// Token: 0x04000135 RID: 309
		private static readonly RuntimeTypeHandle color32Type = typeof(Color32).TypeHandle;

		// Token: 0x04000136 RID: 310
		private static readonly RuntimeTypeHandle rectType = typeof(Rect).TypeHandle;

		// Token: 0x04000137 RID: 311
		private static readonly RuntimeTypeHandle boundsType = typeof(Bounds).TypeHandle;

		// Token: 0x04000138 RID: 312
		private static readonly RuntimeTypeHandle quaternionType = typeof(Quaternion).TypeHandle;

		// Token: 0x04000139 RID: 313
		public static readonly IEqualityComparer<Vector2Int> Vector2Int = new UnityEqualityComparer.Vector2IntEqualityComparer();

		// Token: 0x0400013A RID: 314
		public static readonly IEqualityComparer<Vector3Int> Vector3Int = new UnityEqualityComparer.Vector3IntEqualityComparer();

		// Token: 0x0400013B RID: 315
		public static readonly IEqualityComparer<RangeInt> RangeInt = new UnityEqualityComparer.RangeIntEqualityComparer();

		// Token: 0x0400013C RID: 316
		public static readonly IEqualityComparer<RectInt> RectInt = new UnityEqualityComparer.RectIntEqualityComparer();

		// Token: 0x0400013D RID: 317
		public static readonly IEqualityComparer<BoundsInt> BoundsInt = new UnityEqualityComparer.BoundsIntEqualityComparer();

		// Token: 0x0400013E RID: 318
		private static readonly RuntimeTypeHandle vector2IntType = typeof(Vector2Int).TypeHandle;

		// Token: 0x0400013F RID: 319
		private static readonly RuntimeTypeHandle vector3IntType = typeof(Vector3Int).TypeHandle;

		// Token: 0x04000140 RID: 320
		private static readonly RuntimeTypeHandle rangeIntType = typeof(RangeInt).TypeHandle;

		// Token: 0x04000141 RID: 321
		private static readonly RuntimeTypeHandle rectIntType = typeof(RectInt).TypeHandle;

		// Token: 0x04000142 RID: 322
		private static readonly RuntimeTypeHandle boundsIntType = typeof(BoundsInt).TypeHandle;

		// Token: 0x02000212 RID: 530
		private static class Cache<T>
		{
			// Token: 0x06000BDA RID: 3034 RVA: 0x0002AD64 File Offset: 0x00028F64
			static Cache()
			{
				object defaultHelper = UnityEqualityComparer.GetDefaultHelper(typeof(T));
				if (defaultHelper == null)
				{
					UnityEqualityComparer.Cache<T>.Comparer = EqualityComparer<T>.Default;
					return;
				}
				UnityEqualityComparer.Cache<T>.Comparer = (IEqualityComparer<T>)defaultHelper;
			}

			// Token: 0x0400055A RID: 1370
			public static readonly IEqualityComparer<T> Comparer;
		}

		// Token: 0x02000213 RID: 531
		private sealed class Vector2EqualityComparer : IEqualityComparer<Vector2>
		{
			// Token: 0x06000BDB RID: 3035 RVA: 0x0002AD9A File Offset: 0x00028F9A
			public bool Equals(Vector2 self, Vector2 vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y);
			}

			// Token: 0x06000BDC RID: 3036 RVA: 0x0002ADC4 File Offset: 0x00028FC4
			public int GetHashCode(Vector2 obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2);
			}
		}

		// Token: 0x02000214 RID: 532
		private sealed class Vector3EqualityComparer : IEqualityComparer<Vector3>
		{
			// Token: 0x06000BDE RID: 3038 RVA: 0x0002ADE9 File Offset: 0x00028FE9
			public bool Equals(Vector3 self, Vector3 vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z);
			}

			// Token: 0x06000BDF RID: 3039 RVA: 0x0002AE27 File Offset: 0x00029027
			public int GetHashCode(Vector3 obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2) ^ (obj.z.GetHashCode() >> 2);
			}
		}

		// Token: 0x02000215 RID: 533
		private sealed class Vector4EqualityComparer : IEqualityComparer<Vector4>
		{
			// Token: 0x06000BE1 RID: 3041 RVA: 0x0002AE5C File Offset: 0x0002905C
			public bool Equals(Vector4 self, Vector4 vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z) && self.w.Equals(vector.w);
			}

			// Token: 0x06000BE2 RID: 3042 RVA: 0x0002AEB9 File Offset: 0x000290B9
			public int GetHashCode(Vector4 obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2) ^ (obj.z.GetHashCode() >> 2) ^ (obj.w.GetHashCode() >> 1);
			}
		}

		// Token: 0x02000216 RID: 534
		private sealed class ColorEqualityComparer : IEqualityComparer<Color>
		{
			// Token: 0x06000BE4 RID: 3044 RVA: 0x0002AEFC File Offset: 0x000290FC
			public bool Equals(Color self, Color other)
			{
				return self.r.Equals(other.r) && self.g.Equals(other.g) && self.b.Equals(other.b) && self.a.Equals(other.a);
			}

			// Token: 0x06000BE5 RID: 3045 RVA: 0x0002AF59 File Offset: 0x00029159
			public int GetHashCode(Color obj)
			{
				return obj.r.GetHashCode() ^ (obj.g.GetHashCode() << 2) ^ (obj.b.GetHashCode() >> 2) ^ (obj.a.GetHashCode() >> 1);
			}
		}

		// Token: 0x02000217 RID: 535
		private sealed class RectEqualityComparer : IEqualityComparer<Rect>
		{
			// Token: 0x06000BE7 RID: 3047 RVA: 0x0002AF9C File Offset: 0x0002919C
			public bool Equals(Rect self, Rect other)
			{
				return self.x.Equals(other.x) && self.width.Equals(other.width) && self.y.Equals(other.y) && self.height.Equals(other.height);
			}

			// Token: 0x06000BE8 RID: 3048 RVA: 0x0002B00C File Offset: 0x0002920C
			public int GetHashCode(Rect obj)
			{
				return obj.x.GetHashCode() ^ (obj.width.GetHashCode() << 2) ^ (obj.y.GetHashCode() >> 2) ^ (obj.height.GetHashCode() >> 1);
			}
		}

		// Token: 0x02000218 RID: 536
		private sealed class BoundsEqualityComparer : IEqualityComparer<Bounds>
		{
			// Token: 0x06000BEA RID: 3050 RVA: 0x0002B068 File Offset: 0x00029268
			public bool Equals(Bounds self, Bounds vector)
			{
				return self.center.Equals(vector.center) && self.extents.Equals(vector.extents);
			}

			// Token: 0x06000BEB RID: 3051 RVA: 0x0002B0A8 File Offset: 0x000292A8
			public int GetHashCode(Bounds obj)
			{
				return obj.center.GetHashCode() ^ (obj.extents.GetHashCode() << 2);
			}
		}

		// Token: 0x02000219 RID: 537
		private sealed class QuaternionEqualityComparer : IEqualityComparer<Quaternion>
		{
			// Token: 0x06000BED RID: 3053 RVA: 0x0002B0EC File Offset: 0x000292EC
			public bool Equals(Quaternion self, Quaternion vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z) && self.w.Equals(vector.w);
			}

			// Token: 0x06000BEE RID: 3054 RVA: 0x0002B149 File Offset: 0x00029349
			public int GetHashCode(Quaternion obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2) ^ (obj.z.GetHashCode() >> 2) ^ (obj.w.GetHashCode() >> 1);
			}
		}

		// Token: 0x0200021A RID: 538
		private sealed class Color32EqualityComparer : IEqualityComparer<Color32>
		{
			// Token: 0x06000BF0 RID: 3056 RVA: 0x0002B18C File Offset: 0x0002938C
			public bool Equals(Color32 self, Color32 vector)
			{
				return self.a.Equals(vector.a) && self.r.Equals(vector.r) && self.g.Equals(vector.g) && self.b.Equals(vector.b);
			}

			// Token: 0x06000BF1 RID: 3057 RVA: 0x0002B1E9 File Offset: 0x000293E9
			public int GetHashCode(Color32 obj)
			{
				return obj.a.GetHashCode() ^ (obj.r.GetHashCode() << 2) ^ (obj.g.GetHashCode() >> 2) ^ (obj.b.GetHashCode() >> 1);
			}
		}

		// Token: 0x0200021B RID: 539
		private sealed class Vector2IntEqualityComparer : IEqualityComparer<Vector2Int>
		{
			// Token: 0x06000BF3 RID: 3059 RVA: 0x0002B22C File Offset: 0x0002942C
			public bool Equals(Vector2Int self, Vector2Int vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y);
			}

			// Token: 0x06000BF4 RID: 3060 RVA: 0x0002B26C File Offset: 0x0002946C
			public int GetHashCode(Vector2Int obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2);
			}
		}

		// Token: 0x0200021C RID: 540
		private sealed class Vector3IntEqualityComparer : IEqualityComparer<Vector3Int>
		{
			// Token: 0x06000BF6 RID: 3062 RVA: 0x0002B2A4 File Offset: 0x000294A4
			public bool Equals(Vector3Int self, Vector3Int vector)
			{
				return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z);
			}

			// Token: 0x06000BF7 RID: 3063 RVA: 0x0002B2FC File Offset: 0x000294FC
			public int GetHashCode(Vector3Int obj)
			{
				return obj.x.GetHashCode() ^ (obj.y.GetHashCode() << 2) ^ (obj.z.GetHashCode() >> 2);
			}

			// Token: 0x0400055B RID: 1371
			public static readonly UnityEqualityComparer.Vector3IntEqualityComparer Default = new UnityEqualityComparer.Vector3IntEqualityComparer();
		}

		// Token: 0x0200021D RID: 541
		private sealed class RangeIntEqualityComparer : IEqualityComparer<RangeInt>
		{
			// Token: 0x06000BFA RID: 3066 RVA: 0x0002B350 File Offset: 0x00029550
			public bool Equals(RangeInt self, RangeInt vector)
			{
				return self.start.Equals(vector.start) && self.length.Equals(vector.length);
			}

			// Token: 0x06000BFB RID: 3067 RVA: 0x0002B37A File Offset: 0x0002957A
			public int GetHashCode(RangeInt obj)
			{
				return obj.start.GetHashCode() ^ (obj.length.GetHashCode() << 2);
			}
		}

		// Token: 0x0200021E RID: 542
		private sealed class RectIntEqualityComparer : IEqualityComparer<RectInt>
		{
			// Token: 0x06000BFD RID: 3069 RVA: 0x0002B3A0 File Offset: 0x000295A0
			public bool Equals(RectInt self, RectInt other)
			{
				return self.x.Equals(other.x) && self.width.Equals(other.width) && self.y.Equals(other.y) && self.height.Equals(other.height);
			}

			// Token: 0x06000BFE RID: 3070 RVA: 0x0002B410 File Offset: 0x00029610
			public int GetHashCode(RectInt obj)
			{
				return obj.x.GetHashCode() ^ (obj.width.GetHashCode() << 2) ^ (obj.y.GetHashCode() >> 2) ^ (obj.height.GetHashCode() >> 1);
			}
		}

		// Token: 0x0200021F RID: 543
		private sealed class BoundsIntEqualityComparer : IEqualityComparer<BoundsInt>
		{
			// Token: 0x06000C00 RID: 3072 RVA: 0x0002B46A File Offset: 0x0002966A
			public bool Equals(BoundsInt self, BoundsInt vector)
			{
				return UnityEqualityComparer.Vector3IntEqualityComparer.Default.Equals(self.position, vector.position) && UnityEqualityComparer.Vector3IntEqualityComparer.Default.Equals(self.size, vector.size);
			}

			// Token: 0x06000C01 RID: 3073 RVA: 0x0002B4A0 File Offset: 0x000296A0
			public int GetHashCode(BoundsInt obj)
			{
				return UnityEqualityComparer.Vector3IntEqualityComparer.Default.GetHashCode(obj.position) ^ (UnityEqualityComparer.Vector3IntEqualityComparer.Default.GetHashCode(obj.size) << 2);
			}
		}
	}
}

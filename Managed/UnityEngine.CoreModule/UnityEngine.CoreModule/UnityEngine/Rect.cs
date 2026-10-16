using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000151 RID: 337
	[NativeClass("Rectf", "template<typename T> class RectT; typedef RectT<float> Rectf;")]
	[NativeHeader("Runtime/Math/Rect.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Rect : IEquatable<Rect>, IFormattable
	{
		// Token: 0x06000A14 RID: 2580 RVA: 0x00012853 File Offset: 0x00010A53
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rect(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00012873 File Offset: 0x00010A73
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rect(Vector2 position, Vector2 size)
		{
			this.m_XMin = position.x;
			this.m_YMin = position.y;
			this.m_Width = size.x;
			this.m_Height = size.y;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000128A6 File Offset: 0x00010AA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Rect(Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x000128D9 File Offset: 0x00010AD9
		public static Rect zero
		{
			get
			{
				return new Rect(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000128F4 File Offset: 0x00010AF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rect MinMaxRect(float xmin, float ymin, float xmax, float ymax)
		{
			return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00012853 File Offset: 0x00010A53
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00012914 File Offset: 0x00010B14
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0001292C File Offset: 0x00010B2C
		public float x
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_XMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_XMin = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00012938 File Offset: 0x00010B38
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x00012950 File Offset: 0x00010B50
		public float y
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_YMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_YMin = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0001295C File Offset: 0x00010B5C
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0001297F File Offset: 0x00010B7F
		public Vector2 position
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2(this.m_XMin, this.m_YMin);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0001299C File Offset: 0x00010B9C
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x000129D9 File Offset: 0x00010BD9
		public Vector2 center
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2(this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_XMin = value.x - this.m_Width / 2f;
				this.m_YMin = value.y - this.m_Height / 2f;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00012A10 File Offset: 0x00010C10
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00012A33 File Offset: 0x00010C33
		public Vector2 min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2(this.xMin, this.yMin);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00012A50 File Offset: 0x00010C50
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00012A73 File Offset: 0x00010C73
		public Vector2 max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2(this.xMax, this.yMax);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00012A90 File Offset: 0x00010C90
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00012AA8 File Offset: 0x00010CA8
		public float width
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Width;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00012AB4 File Offset: 0x00010CB4
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x00012ACC File Offset: 0x00010CCC
		public float height
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Height;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00012AD8 File Offset: 0x00010CD8
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x00012AFB File Offset: 0x00010CFB
		public Vector2 size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2(this.m_Width, this.m_Height);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00012B18 File Offset: 0x00010D18
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x00012B30 File Offset: 0x00010D30
		public float xMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_XMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				float xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00012B5C File Offset: 0x00010D5C
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x00012B74 File Offset: 0x00010D74
		public float yMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_YMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				float yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00012BA0 File Offset: 0x00010DA0
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x00012BBF File Offset: 0x00010DBF
		public float xMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Width + this.m_XMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00012BD0 File Offset: 0x00010DD0
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x00012BEF File Offset: 0x00010DEF
		public float yMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Height + this.m_YMin;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00012C00 File Offset: 0x00010E00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00012C50 File Offset: 0x00010E50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00012CA0 File Offset: 0x00010EA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(Vector3 point, bool allowInverse)
		{
			bool flag = !allowInverse;
			bool flag2;
			if (flag)
			{
				flag2 = this.Contains(point);
			}
			else
			{
				bool flag3 = (this.width < 0f && point.x <= this.xMin && point.x > this.xMax) || (this.width >= 0f && point.x >= this.xMin && point.x < this.xMax);
				bool flag4 = (this.height < 0f && point.y <= this.yMin && point.y > this.yMax) || (this.height >= 0f && point.y >= this.yMin && point.y < this.yMax);
				flag2 = flag3 && flag4;
			}
			return flag2;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00012D80 File Offset: 0x00010F80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Rect OrderMinMax(Rect rect)
		{
			bool flag = rect.xMin > rect.xMax;
			if (flag)
			{
				float xMin = rect.xMin;
				rect.xMin = rect.xMax;
				rect.xMax = xMin;
			}
			bool flag2 = rect.yMin > rect.yMax;
			if (flag2)
			{
				float yMin = rect.yMin;
				rect.yMin = rect.yMax;
				rect.yMax = yMin;
			}
			return rect;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00012E04 File Offset: 0x00011004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(Rect other)
		{
			return other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00012E58 File Offset: 0x00011058
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(Rect other, bool allowInverse)
		{
			Rect rect = this;
			if (allowInverse)
			{
				rect = Rect.OrderMinMax(rect);
				other = Rect.OrderMinMax(other);
			}
			return rect.Overlaps(other);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00012E90 File Offset: 0x00011090
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 NormalizedToPoint(Rect rectangle, Vector2 normalizedRectCoordinates)
		{
			return new Vector2(Mathf.Lerp(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x), Mathf.Lerp(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y));
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00012EDC File Offset: 0x000110DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 PointToNormalized(Rect rectangle, Vector2 point)
		{
			return new Vector2(Mathf.InverseLerp(rectangle.x, rectangle.xMax, point.x), Mathf.InverseLerp(rectangle.y, rectangle.yMax, point.y));
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00012F28 File Offset: 0x00011128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00012F44 File Offset: 0x00011144
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00012F9C File Offset: 0x0001119C
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.width.GetHashCode() << 2) ^ (this.y.GetHashCode() >> 2) ^ (this.height.GetHashCode() >> 1);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00012FF0 File Offset: 0x000111F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Rect rect;
			bool flag;
			if (other is Rect)
			{
				rect = (Rect)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(rect);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00013024 File Offset: 0x00011224
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Rect other)
		{
			return this.x.Equals(other.x) && this.y.Equals(other.y) && this.width.Equals(other.width) && this.height.Equals(other.height);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00013094 File Offset: 0x00011294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000130B0 File Offset: 0x000112B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000130CC File Offset: 0x000112CC
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
			return UnityString.Format("(x:{0}, y:{1}, width:{2}, height:{3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.width.ToString(format, formatProvider),
				this.height.ToString(format, formatProvider)
			});
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00013160 File Offset: 0x00011360
		[Obsolete("use xMin")]
		public float left
		{
			get
			{
				return this.m_XMin;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00013178 File Offset: 0x00011378
		[Obsolete("use xMax")]
		public float right
		{
			get
			{
				return this.m_XMin + this.m_Width;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00013198 File Offset: 0x00011398
		[Obsolete("use yMin")]
		public float top
		{
			get
			{
				return this.m_YMin;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000131B0 File Offset: 0x000113B0
		[Obsolete("use yMax")]
		public float bottom
		{
			get
			{
				return this.m_YMin + this.m_Height;
			}
		}

		// Token: 0x04000471 RID: 1137
		[NativeName("x")]
		private float m_XMin;

		// Token: 0x04000472 RID: 1138
		[NativeName("y")]
		private float m_YMin;

		// Token: 0x04000473 RID: 1139
		[NativeName("width")]
		private float m_Width;

		// Token: 0x04000474 RID: 1140
		[NativeName("height")]
		private float m_Height;
	}
}

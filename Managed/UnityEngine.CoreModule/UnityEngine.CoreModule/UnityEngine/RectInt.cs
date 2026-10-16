using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000152 RID: 338
	[UsedByNativeCode]
	public struct RectInt : IEquatable<RectInt>, IFormattable
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000131D0 File Offset: 0x000113D0
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x000131E8 File Offset: 0x000113E8
		public int x
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x000131F4 File Offset: 0x000113F4
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0001320C File Offset: 0x0001140C
		public int y
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

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00013218 File Offset: 0x00011418
		public Vector2 center
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2((float)this.x + (float)this.m_Width / 2f, (float)this.y + (float)this.m_Height / 2f);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0001325C File Offset: 0x0001145C
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0001327F File Offset: 0x0001147F
		public Vector2Int min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2Int(this.xMin, this.yMin);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x000132A0 File Offset: 0x000114A0
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x000132C3 File Offset: 0x000114C3
		public Vector2Int max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2Int(this.xMax, this.yMax);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x000132E4 File Offset: 0x000114E4
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x000132FC File Offset: 0x000114FC
		public int width
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

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00013308 File Offset: 0x00011508
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x00013320 File Offset: 0x00011520
		public int height
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

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0001332C File Offset: 0x0001152C
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00013358 File Offset: 0x00011558
		public int xMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Min(this.m_XMin, this.m_XMin + this.m_Width);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				int xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00013384 File Offset: 0x00011584
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x000133B0 File Offset: 0x000115B0
		public int yMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Min(this.m_YMin, this.m_YMin + this.m_Height);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				int yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x000133DC File Offset: 0x000115DC
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x00013406 File Offset: 0x00011606
		public int xMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Max(this.m_XMin, this.m_XMin + this.m_Width);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00013418 File Offset: 0x00011618
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00013442 File Offset: 0x00011642
		public int yMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Max(this.m_YMin, this.m_YMin + this.m_Height);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00013454 File Offset: 0x00011654
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00013477 File Offset: 0x00011677
		public Vector2Int position
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2Int(this.m_XMin, this.m_YMin);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00013494 File Offset: 0x00011694
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x000134B7 File Offset: 0x000116B7
		public Vector2Int size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector2Int(this.m_Width, this.m_Height);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000134D4 File Offset: 0x000116D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetMinMax(Vector2Int minPosition, Vector2Int maxPosition)
		{
			this.min = minPosition;
			this.max = maxPosition;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000134E7 File Offset: 0x000116E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RectInt(int xMin, int yMin, int width, int height)
		{
			this.m_XMin = xMin;
			this.m_YMin = yMin;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00013507 File Offset: 0x00011707
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RectInt(Vector2Int position, Vector2Int size)
		{
			this.m_XMin = position.x;
			this.m_YMin = position.y;
			this.m_Width = size.x;
			this.m_Height = size.y;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001353E File Offset: 0x0001173E
		public static RectInt zero
		{
			get
			{
				return new RectInt(0, 0, 0, 0);
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001354C File Offset: 0x0001174C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClampToBounds(RectInt bounds)
		{
			this.position = new Vector2Int(Math.Max(Math.Min(bounds.xMax, this.position.x), bounds.xMin), Math.Max(Math.Min(bounds.yMax, this.position.y), bounds.yMin));
			this.size = new Vector2Int(Math.Min(bounds.xMax - this.position.x, this.size.x), Math.Min(bounds.yMax - this.position.y, this.size.y));
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00013610 File Offset: 0x00011810
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(Vector2Int position)
		{
			return position.x >= this.xMin && position.y >= this.yMin && position.x < this.xMax && position.y < this.yMax;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00013664 File Offset: 0x00011864
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(RectInt other)
		{
			return other.xMin < this.xMax && other.xMax > this.xMin && other.yMin < this.yMax && other.yMax > this.yMin;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000136B8 File Offset: 0x000118B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000136D4 File Offset: 0x000118D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000136F0 File Offset: 0x000118F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
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

		// Token: 0x06000A6B RID: 2667 RVA: 0x00013774 File Offset: 0x00011974
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(RectInt lhs, RectInt rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00013790 File Offset: 0x00011990
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(RectInt lhs, RectInt rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000137E8 File Offset: 0x000119E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			int hashCode = this.x.GetHashCode();
			int hashCode2 = this.y.GetHashCode();
			int hashCode3 = this.width.GetHashCode();
			int hashCode4 = this.height.GetHashCode();
			return hashCode ^ (hashCode2 << 4) ^ (hashCode2 >> 28) ^ (hashCode3 >> 4) ^ (hashCode3 << 28) ^ (hashCode4 >> 4) ^ (hashCode4 << 28);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00013858 File Offset: 0x00011A58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is RectInt);
			return !flag && this.Equals((RectInt)other);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001388C File Offset: 0x00011A8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(RectInt other)
		{
			return this.m_XMin == other.m_XMin && this.m_YMin == other.m_YMin && this.m_Width == other.m_Width && this.m_Height == other.m_Height;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x000138DC File Offset: 0x00011ADC
		public RectInt.PositionEnumerator allPositionsWithin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new RectInt.PositionEnumerator(this.min, this.max);
			}
		}

		// Token: 0x04000475 RID: 1141
		private int m_XMin;

		// Token: 0x04000476 RID: 1142
		private int m_YMin;

		// Token: 0x04000477 RID: 1143
		private int m_Width;

		// Token: 0x04000478 RID: 1144
		private int m_Height;

		// Token: 0x02000153 RID: 339
		public struct PositionEnumerator : IEnumerator<Vector2Int>, IEnumerator, IDisposable
		{
			// Token: 0x06000A71 RID: 2673 RVA: 0x00013900 File Offset: 0x00011B00
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public PositionEnumerator(Vector2Int min, Vector2Int max)
			{
				this._current = min;
				this._min = min;
				this._max = max;
				this.Reset();
			}

			// Token: 0x06000A72 RID: 2674 RVA: 0x0001392C File Offset: 0x00011B2C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public RectInt.PositionEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x06000A73 RID: 2675 RVA: 0x00013944 File Offset: 0x00011B44
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				bool flag = this._current.y >= this._max.y;
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					int num = this._current.x;
					this._current.x = num + 1;
					bool flag3 = this._current.x >= this._max.x;
					if (flag3)
					{
						this._current.x = this._min.x;
						bool flag4 = this._current.x >= this._max.x;
						if (flag4)
						{
							return false;
						}
						num = this._current.y;
						this._current.y = num + 1;
						bool flag5 = this._current.y >= this._max.y;
						if (flag5)
						{
							return false;
						}
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06000A74 RID: 2676 RVA: 0x00013A40 File Offset: 0x00011C40
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Reset()
			{
				this._current = this._min;
				int x = this._current.x;
				this._current.x = x - 1;
			}

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00013A70 File Offset: 0x00011C70
			public Vector2Int Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._current;
				}
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00013A88 File Offset: 0x00011C88
			object IEnumerator.Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x00002925 File Offset: 0x00000B25
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000479 RID: 1145
			private readonly Vector2Int _min;

			// Token: 0x0400047A RID: 1146
			private readonly Vector2Int _max;

			// Token: 0x0400047B RID: 1147
			private Vector2Int _current;
		}
	}
}

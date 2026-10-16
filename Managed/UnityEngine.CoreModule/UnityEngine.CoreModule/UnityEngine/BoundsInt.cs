using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014B RID: 331
	[UsedByNativeCode]
	public struct BoundsInt : IEquatable<BoundsInt>, IFormattable
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001153C File Offset: 0x0000F73C
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x00011559 File Offset: 0x0000F759
		public int x
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Position.x;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Position.x = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0001156C File Offset: 0x0000F76C
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x00011589 File Offset: 0x0000F789
		public int y
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Position.y;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Position.y = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0001159C File Offset: 0x0000F79C
		// (set) Token: 0x060009B5 RID: 2485 RVA: 0x000115B9 File Offset: 0x0000F7B9
		public int z
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Position.z;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Position.z = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x000115CC File Offset: 0x0000F7CC
		public Vector3 center
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector3((float)this.x + (float)this.m_Size.x / 2f, (float)this.y + (float)this.m_Size.y / 2f, (float)this.z + (float)this.m_Size.z / 2f);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00011634 File Offset: 0x0000F834
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x0001165D File Offset: 0x0000F85D
		public Vector3Int min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector3Int(this.xMin, this.yMin, this.zMin);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
				this.zMin = value.z;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001168C File Offset: 0x0000F88C
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x000116B5 File Offset: 0x0000F8B5
		public Vector3Int max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Vector3Int(this.xMax, this.yMax, this.zMax);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
				this.zMax = value.z;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x000116E4 File Offset: 0x0000F8E4
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x00011720 File Offset: 0x0000F920
		public int xMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Min(this.m_Position.x, this.m_Position.x + this.m_Size.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				int xMax = this.xMax;
				this.m_Position.x = value;
				this.m_Size.x = xMax - this.m_Position.x;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0001175C File Offset: 0x0000F95C
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x00011798 File Offset: 0x0000F998
		public int yMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Min(this.m_Position.y, this.m_Position.y + this.m_Size.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				int yMax = this.yMax;
				this.m_Position.y = value;
				this.m_Size.y = yMax - this.m_Position.y;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x000117D4 File Offset: 0x0000F9D4
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00011810 File Offset: 0x0000FA10
		public int zMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Min(this.m_Position.z, this.m_Position.z + this.m_Size.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				int zMax = this.zMax;
				this.m_Position.z = value;
				this.m_Size.z = zMax - this.m_Position.z;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0001184C File Offset: 0x0000FA4C
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00011885 File Offset: 0x0000FA85
		public int xMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Max(this.m_Position.x, this.m_Position.x + this.m_Size.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Size.x = value - this.m_Position.x;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x000118A4 File Offset: 0x0000FAA4
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x000118DD File Offset: 0x0000FADD
		public int yMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Max(this.m_Position.y, this.m_Position.y + this.m_Size.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Size.y = value - this.m_Position.y;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x000118FC File Offset: 0x0000FAFC
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00011935 File Offset: 0x0000FB35
		public int zMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Math.Max(this.m_Position.z, this.m_Position.z + this.m_Size.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Size.z = value - this.m_Position.z;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00011954 File Offset: 0x0000FB54
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x0001196C File Offset: 0x0000FB6C
		public Vector3Int position
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Position;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00011978 File Offset: 0x0000FB78
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00011990 File Offset: 0x0000FB90
		public Vector3Int size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Size;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0001199A File Offset: 0x0000FB9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public BoundsInt(int xMin, int yMin, int zMin, int sizeX, int sizeY, int sizeZ)
		{
			this.m_Position = new Vector3Int(xMin, yMin, zMin);
			this.m_Size = new Vector3Int(sizeX, sizeY, sizeZ);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000119BC File Offset: 0x0000FBBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public BoundsInt(Vector3Int position, Vector3Int size)
		{
			this.m_Position = position;
			this.m_Size = size;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000119CD File Offset: 0x0000FBCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetMinMax(Vector3Int minPosition, Vector3Int maxPosition)
		{
			this.min = minPosition;
			this.max = maxPosition;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000119E0 File Offset: 0x0000FBE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClampToBounds(BoundsInt bounds)
		{
			this.position = new Vector3Int(Math.Max(Math.Min(bounds.xMax, this.position.x), bounds.xMin), Math.Max(Math.Min(bounds.yMax, this.position.y), bounds.yMin), Math.Max(Math.Min(bounds.zMax, this.position.z), bounds.zMin));
			this.size = new Vector3Int(Math.Min(bounds.xMax - this.position.x, this.size.x), Math.Min(bounds.yMax - this.position.y, this.size.y), Math.Min(bounds.zMax - this.position.z, this.size.z));
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00011AF4 File Offset: 0x0000FCF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(Vector3Int position)
		{
			return position.x >= this.xMin && position.y >= this.yMin && position.z >= this.zMin && position.x < this.xMax && position.y < this.yMax && position.z < this.zMax;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00011B64 File Offset: 0x0000FD64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00011B80 File Offset: 0x0000FD80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00011B9C File Offset: 0x0000FD9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("Position: {0}, Size: {1}", new object[]
			{
				this.m_Position.ToString(format, formatProvider),
				this.m_Size.ToString(format, formatProvider)
			});
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(BoundsInt lhs, BoundsInt rhs)
		{
			return lhs.m_Position == rhs.m_Position && lhs.m_Size == rhs.m_Size;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00011C30 File Offset: 0x0000FE30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(BoundsInt lhs, BoundsInt rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00011C4C File Offset: 0x0000FE4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is BoundsInt);
			return !flag && this.Equals((BoundsInt)other);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00011C80 File Offset: 0x0000FE80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(BoundsInt other)
		{
			return this.m_Position.Equals(other.m_Position) && this.m_Size.Equals(other.m_Size);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00011CBC File Offset: 0x0000FEBC
		public override int GetHashCode()
		{
			return this.m_Position.GetHashCode() ^ (this.m_Size.GetHashCode() << 2);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		public BoundsInt.PositionEnumerator allPositionsWithin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new BoundsInt.PositionEnumerator(this.min, this.max);
			}
		}

		// Token: 0x04000465 RID: 1125
		private Vector3Int m_Position;

		// Token: 0x04000466 RID: 1126
		private Vector3Int m_Size;

		// Token: 0x0200014C RID: 332
		public struct PositionEnumerator : IEnumerator<Vector3Int>, IEnumerator, IDisposable
		{
			// Token: 0x060009D9 RID: 2521 RVA: 0x00011D18 File Offset: 0x0000FF18
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public PositionEnumerator(Vector3Int min, Vector3Int max)
			{
				this._current = min;
				this._min = min;
				this._max = max;
				this.Reset();
			}

			// Token: 0x060009DA RID: 2522 RVA: 0x00011D44 File Offset: 0x0000FF44
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public BoundsInt.PositionEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x060009DB RID: 2523 RVA: 0x00011D5C File Offset: 0x0000FF5C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				bool flag = this._current.z >= this._max.z || this._current.y >= this._max.y;
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
							this._current.y = this._min.y;
							num = this._current.z;
							this._current.z = num + 1;
							bool flag6 = this._current.z >= this._max.z;
							if (flag6)
							{
								return false;
							}
						}
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x00011ED0 File Offset: 0x000100D0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Reset()
			{
				this._current = this._min;
				int x = this._current.x;
				this._current.x = x - 1;
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x060009DD RID: 2525 RVA: 0x00011F00 File Offset: 0x00010100
			public Vector3Int Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._current;
				}
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x060009DE RID: 2526 RVA: 0x00011F18 File Offset: 0x00010118
			object IEnumerator.Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x00002925 File Offset: 0x00000B25
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000467 RID: 1127
			private readonly Vector3Int _min;

			// Token: 0x04000468 RID: 1128
			private readonly Vector3Int _max;

			// Token: 0x04000469 RID: 1129
			private Vector3Int _current;
		}
	}
}

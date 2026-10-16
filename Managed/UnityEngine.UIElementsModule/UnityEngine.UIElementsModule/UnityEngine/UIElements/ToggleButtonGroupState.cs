using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B5 RID: 437
	[Serializable]
	public struct ToggleButtonGroupState : IEquatable<ToggleButtonGroupState>, IComparable<ToggleButtonGroupState>
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x0003D084 File Offset: 0x0003B284
		public ToggleButtonGroupState(ulong optionsBitMask, int length)
		{
			bool flag = length < 0 || length > 64;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length", string.Format("length of {0} should be greater than or equal to 0 and less than or equal to {1}.", length, 64));
			}
			this.m_Data = optionsBitMask;
			this.m_Length = length;
			this.ResetOptions(this.m_Length);
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0003D0DF File Offset: 0x0003B2DF
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x0003D0E7 File Offset: 0x0003B2E7
		public int length
		{
			get
			{
				return this.m_Length;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Length = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0003D0F0 File Offset: 0x0003B2F0
		internal ulong data
		{
			get
			{
				return this.m_Data;
			}
		}

		// Token: 0x17000242 RID: 578
		public bool this[int index]
		{
			get
			{
				bool flag = index < 0 || index >= this.m_Length;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index", string.Format("index of {0} should be in the range of 0 and {1} inclusively.", index, this.m_Length - 1));
				}
				ulong num = 1UL << index;
				return (this.m_Data & num) == num;
			}
			set
			{
				bool flag = index < 0 || index >= this.m_Length;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index", string.Format("index of {0} should be in the range of 0 and {1} inclusively.", index, this.m_Length - 1));
				}
				ulong num = 1UL << index;
				if (value)
				{
					this.m_Data |= num;
				}
				else
				{
					this.m_Data &= ~num;
				}
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003D1D8 File Offset: 0x0003B3D8
		public unsafe Span<int> GetActiveOptions(Span<int> activeOptionsIndices)
		{
			bool flag = activeOptionsIndices.Length < this.m_Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("indices' length ({0}) should be equal to or greater than the ToggleButtonGroupState's length ({1}).", activeOptionsIndices.Length, this.m_Length));
			}
			int num = 0;
			for (int i = 0; i < this.m_Length; i++)
			{
				bool flag2 = !this[i];
				if (!flag2)
				{
					*activeOptionsIndices[num] = i;
					num++;
				}
			}
			return activeOptionsIndices.Slice(0, num);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0003D268 File Offset: 0x0003B468
		public unsafe Span<int> GetInactiveOptions(Span<int> inactiveOptionsIndices)
		{
			bool flag = inactiveOptionsIndices.Length < this.m_Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("indices' length ({0}) should be equal to or greater than the ToggleButtonGroupState's length ({1}).", inactiveOptionsIndices.Length, this.m_Length));
			}
			int num = 0;
			for (int i = 0; i < this.m_Length; i++)
			{
				bool flag2 = this[i];
				if (!flag2)
				{
					*inactiveOptionsIndices[num] = i;
					num++;
				}
			}
			return inactiveOptionsIndices.Slice(0, num);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0003D2F5 File Offset: 0x0003B4F5
		public void SetAllOptions()
		{
			this.m_Data = ulong.MaxValue;
			this.ResetOptions(this.m_Length);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0003D30D File Offset: 0x0003B50D
		public void ResetAllOptions()
		{
			this.m_Data = 0UL;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0003D318 File Offset: 0x0003B518
		public void ToggleAllOptions()
		{
			this.m_Data = ~this.m_Data;
			this.ResetOptions(this.m_Length);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003D338 File Offset: 0x0003B538
		public static ToggleButtonGroupState CreateFromOptions(IList<bool> options)
		{
			int count = options.Count;
			ToggleButtonGroupState toggleButtonGroupState = new ToggleButtonGroupState(0UL, count);
			for (int i = 0; i < count; i++)
			{
				toggleButtonGroupState[i] = options[i];
			}
			return toggleButtonGroupState;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003D380 File Offset: 0x0003B580
		public unsafe static ToggleButtonGroupState FromEnumFlags<T>(T options, int length = -1) where T : Enum
		{
			bool flag = !TypeTraits<T>.IsEnumFlags;
			if (flag)
			{
				throw new ArgumentException("Enum type T is not a flag enum type.");
			}
			Type underlyingType = Enum.GetUnderlyingType(typeof(T));
			bool flag2 = length == -1;
			if (flag2)
			{
				TypeCode typeCode = Type.GetTypeCode(underlyingType);
				if (!true)
				{
				}
				int num;
				switch (typeCode)
				{
				case TypeCode.SByte:
					num = 8;
					break;
				case TypeCode.Byte:
					num = 8;
					break;
				case TypeCode.Int16:
					num = 16;
					break;
				case TypeCode.UInt16:
					num = 16;
					break;
				case TypeCode.Int32:
					num = 32;
					break;
				case TypeCode.UInt32:
					num = 32;
					break;
				case TypeCode.Int64:
					num = 64;
					break;
				case TypeCode.UInt64:
					num = 64;
					break;
				default:
					num = 0;
					break;
				}
				if (!true)
				{
				}
				length = num;
			}
			return new ToggleButtonGroupState((ulong)((long)(*UnsafeUtility.As<T, int>(ref options))), length);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0003D43C File Offset: 0x0003B63C
		public static T ToEnumFlags<T>(ToggleButtonGroupState options, bool acceptsLengthMismatch = true) where T : Enum
		{
			bool flag = !TypeTraits<T>.IsEnumFlags;
			if (flag)
			{
				throw new ArgumentException("Enum type T is not a flag enum type.");
			}
			Type underlyingType = Enum.GetUnderlyingType(typeof(T));
			TypeCode typeCode = Type.GetTypeCode(underlyingType);
			if (!true)
			{
			}
			int num;
			switch (typeCode)
			{
			case TypeCode.SByte:
				num = 8;
				break;
			case TypeCode.Byte:
				num = 8;
				break;
			case TypeCode.Int16:
				num = 16;
				break;
			case TypeCode.UInt16:
				num = 16;
				break;
			case TypeCode.Int32:
				num = 32;
				break;
			case TypeCode.UInt32:
				num = 32;
				break;
			case TypeCode.Int64:
				num = 64;
				break;
			case TypeCode.UInt64:
				num = 64;
				break;
			default:
				num = -1;
				break;
			}
			if (!true)
			{
			}
			int num2 = num;
			bool flag2 = !acceptsLengthMismatch && options.m_Length != num2;
			if (flag2)
			{
				throw new ArgumentException("Cannot sync to enum flag since the ToggleButtonGroupState has a different amount of options.");
			}
			return (T)((object)Enum.Parse(typeof(T), options.m_Data.ToString()));
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003D524 File Offset: 0x0003B724
		public int CompareTo(ToggleButtonGroupState other)
		{
			return (other == this) ? 1 : (-1);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0003D548 File Offset: 0x0003B748
		public unsafe static bool Compare<T>(ToggleButtonGroupState options, T value) where T : Enum
		{
			bool flag = !TypeTraits<T>.IsEnumFlags;
			if (flag)
			{
				throw new ArgumentException("Enum type T is not a flag enum type.");
			}
			ulong num = (ulong)((long)(*UnsafeUtility.As<T, int>(ref value)));
			return options.m_Data == num;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003D584 File Offset: 0x0003B784
		private void ResetOptions(int startingIndex)
		{
			for (int i = startingIndex; i < 64; i++)
			{
				ulong num = 1UL << i;
				this.m_Data &= ~num;
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003D5BC File Offset: 0x0003B7BC
		public static bool operator ==(ToggleButtonGroupState lhs, ToggleButtonGroupState rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003D5D8 File Offset: 0x0003B7D8
		public static bool operator !=(ToggleButtonGroupState lhs, ToggleButtonGroupState rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003D5F4 File Offset: 0x0003B7F4
		public bool Equals(ToggleButtonGroupState other)
		{
			return this.m_Data == other.m_Data && this.m_Length == other.m_Length;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003D628 File Offset: 0x0003B828
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is ToggleButtonGroupState)
			{
				ToggleButtonGroupState toggleButtonGroupState = (ToggleButtonGroupState)obj;
				flag = this.Equals(toggleButtonGroupState);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003D654 File Offset: 0x0003B854
		public override int GetHashCode()
		{
			return HashCode.Combine<ulong, int>(this.m_Data, this.m_Length);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003D678 File Offset: 0x0003B878
		public override string ToString()
		{
			return Convert.ToString((long)this.m_Data, 2).PadLeft(this.length, '0');
		}

		// Token: 0x04000889 RID: 2185
		internal const int maxLength = 64;

		// Token: 0x0400088A RID: 2186
		[SerializeField]
		private ulong m_Data;

		// Token: 0x0400088B RID: 2187
		[SerializeField]
		private int m_Length;
	}
}

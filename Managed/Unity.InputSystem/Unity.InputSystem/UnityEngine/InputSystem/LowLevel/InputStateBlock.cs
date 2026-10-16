using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000FB RID: 251
	public struct InputStateBlock
	{
		// Token: 0x06000EC9 RID: 3785 RVA: 0x00048488 File Offset: 0x00046688
		public static int GetSizeOfPrimitiveFormatInBits(FourCC type)
		{
			if (type == InputStateBlock.FormatBit || type == InputStateBlock.FormatSBit)
			{
				return 1;
			}
			if (type == InputStateBlock.FormatInt || type == InputStateBlock.FormatUInt)
			{
				return 32;
			}
			if (type == InputStateBlock.FormatShort || type == InputStateBlock.FormatUShort)
			{
				return 16;
			}
			if (type == InputStateBlock.FormatByte || type == InputStateBlock.FormatSByte)
			{
				return 8;
			}
			if (type == InputStateBlock.FormatLong || type == InputStateBlock.FormatULong)
			{
				return 64;
			}
			if (type == InputStateBlock.FormatFloat)
			{
				return 32;
			}
			if (type == InputStateBlock.FormatDouble)
			{
				return 64;
			}
			if (type == InputStateBlock.FormatVector2)
			{
				return 64;
			}
			if (type == InputStateBlock.FormatVector3)
			{
				return 96;
			}
			if (type == InputStateBlock.FormatQuaternion)
			{
				return 128;
			}
			if (type == InputStateBlock.FormatVector2Short)
			{
				return 32;
			}
			if (type == InputStateBlock.FormatVector3Short)
			{
				return 48;
			}
			if (type == InputStateBlock.FormatVector2Byte)
			{
				return 16;
			}
			if (type == InputStateBlock.FormatVector3Byte)
			{
				return 24;
			}
			return -1;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000485B8 File Offset: 0x000467B8
		public static FourCC GetPrimitiveFormatFromType(Type type)
		{
			if (type == typeof(int))
			{
				return InputStateBlock.FormatInt;
			}
			if (type == typeof(uint))
			{
				return InputStateBlock.FormatUInt;
			}
			if (type == typeof(short))
			{
				return InputStateBlock.FormatShort;
			}
			if (type == typeof(ushort))
			{
				return InputStateBlock.FormatUShort;
			}
			if (type == typeof(byte))
			{
				return InputStateBlock.FormatByte;
			}
			if (type == typeof(sbyte))
			{
				return InputStateBlock.FormatSByte;
			}
			if (type == typeof(long))
			{
				return InputStateBlock.FormatLong;
			}
			if (type == typeof(ulong))
			{
				return InputStateBlock.FormatULong;
			}
			if (type == typeof(float))
			{
				return InputStateBlock.FormatFloat;
			}
			if (type == typeof(double))
			{
				return InputStateBlock.FormatDouble;
			}
			if (type == typeof(Vector2))
			{
				return InputStateBlock.FormatVector2;
			}
			if (type == typeof(Vector3))
			{
				return InputStateBlock.FormatVector3;
			}
			if (type == typeof(Quaternion))
			{
				return InputStateBlock.FormatQuaternion;
			}
			return default(FourCC);
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x000486C5 File Offset: 0x000468C5
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x000486CD File Offset: 0x000468CD
		public FourCC format { readonly get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x000486D6 File Offset: 0x000468D6
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x000486DE File Offset: 0x000468DE
		public uint byteOffset
		{
			get
			{
				return this.m_ByteOffset;
			}
			set
			{
				this.m_ByteOffset = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x000486E7 File Offset: 0x000468E7
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x000486EF File Offset: 0x000468EF
		public uint bitOffset { readonly get; set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x000486F8 File Offset: 0x000468F8
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x00048700 File Offset: 0x00046900
		public uint sizeInBits { readonly get; set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00048709 File Offset: 0x00046909
		internal uint alignedSizeInBytes
		{
			get
			{
				return this.sizeInBits + 7U >> 3;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00048715 File Offset: 0x00046915
		internal uint effectiveByteOffset
		{
			get
			{
				return this.byteOffset + (this.bitOffset >> 3);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00048726 File Offset: 0x00046926
		internal uint effectiveBitOffset
		{
			get
			{
				return this.byteOffset * 8U + this.bitOffset;
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00048738 File Offset: 0x00046938
		public unsafe int ReadInt(void* statePtr)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1396853076)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num != 1113150533)
						{
							goto IL_00FA;
						}
						return (int)(*ptr);
					}
					else
					{
						if (this.sizeInBits != 1U)
						{
							return (int)MemoryHelpers.ReadMultipleBitsAsUInt((void*)ptr, this.bitOffset, this.sizeInBits);
						}
						if (!MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset))
						{
							return 0;
						}
						return 1;
					}
				}
				else if (num != 1229870112)
				{
					if (num != 1396853076)
					{
						goto IL_00FA;
					}
					if (this.sizeInBits != 1U)
					{
						return MemoryHelpers.ReadExcessKMultipleBitsAsInt((void*)ptr, this.bitOffset, this.sizeInBits);
					}
					if (!MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset))
					{
						return -1;
					}
					return 1;
				}
			}
			else if (num <= 1397248596)
			{
				if (num == 1396857172)
				{
					return (int)(*(sbyte*)ptr);
				}
				if (num != 1397248596)
				{
					goto IL_00FA;
				}
				return (int)(*(short*)ptr);
			}
			else if (num != 1430867540)
			{
				if (num != 1431521364)
				{
					goto IL_00FA;
				}
				return (int)(*(ushort*)ptr);
			}
			return *(int*)ptr;
			IL_00FA:
			throw new InvalidOperationException(string.Format("State format '{0}' is not supported as integer format", this.format));
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0004885C File Offset: 0x00046A5C
		public unsafe void WriteInt(void* statePtr, int value)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1396853076)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num != 1113150533)
						{
							goto IL_00FB;
						}
						*ptr = (byte)value;
						return;
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value != 0);
							return;
						}
						MemoryHelpers.WriteUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, (uint)value);
						return;
					}
				}
				else if (num != 1229870112)
				{
					if (num != 1396853076)
					{
						goto IL_00FB;
					}
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value > 0);
						return;
					}
					MemoryHelpers.WriteIntAsExcessKMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, value);
					return;
				}
			}
			else if (num <= 1397248596)
			{
				if (num == 1396857172)
				{
					*ptr = (byte)((sbyte)value);
					return;
				}
				if (num != 1397248596)
				{
					goto IL_00FB;
				}
				*(short*)ptr = (short)value;
				return;
			}
			else if (num != 1430867540)
			{
				if (num != 1431521364)
				{
					goto IL_00FB;
				}
				*(short*)ptr = (short)((ushort)value);
				return;
			}
			*(int*)ptr = value;
			return;
			IL_00FB:
			throw new Exception(string.Format("State format '{0}' is not supported as integer format", this.format));
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00048980 File Offset: 0x00046B80
		public unsafe float ReadFloat(void* statePtr)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							return NumberHelpers.UIntToNormalizedFloat((uint)(*ptr), 0U, 255U);
						}
					}
					else
					{
						if (this.sizeInBits != 1U)
						{
							return MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)ptr, this.bitOffset, this.sizeInBits);
						}
						if (!MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset))
						{
							return 0f;
						}
						return 1f;
					}
				}
				else
				{
					if (num == 1145195552)
					{
						return (float)(*(double*)ptr);
					}
					if (num == 1179407392)
					{
						return *(float*)ptr;
					}
					if (num == 1229870112)
					{
						return NumberHelpers.IntToNormalizedFloat(*(int*)ptr, int.MinValue, int.MaxValue) * 2f - 1f;
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						return NumberHelpers.IntToNormalizedFloat((int)(*(sbyte*)ptr), -128, 127) * 2f - 1f;
					}
				}
				else
				{
					if (this.sizeInBits != 1U)
					{
						return MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)ptr, this.bitOffset, this.sizeInBits) * 2f - 1f;
					}
					if (!MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset))
					{
						return -1f;
					}
					return 1f;
				}
			}
			else
			{
				if (num == 1397248596)
				{
					return NumberHelpers.IntToNormalizedFloat((int)(*(short*)ptr), -32768, 32767) * 2f - 1f;
				}
				if (num == 1430867540)
				{
					return NumberHelpers.UIntToNormalizedFloat(*(uint*)ptr, 0U, uint.MaxValue);
				}
				if (num == 1431521364)
				{
					return NumberHelpers.UIntToNormalizedFloat((uint)(*(ushort*)ptr), 0U, 65535U);
				}
			}
			throw new InvalidOperationException(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00048B4C File Offset: 0x00046D4C
		public unsafe void WriteFloat(void* statePtr, float value)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							*ptr = (byte)NumberHelpers.NormalizedFloatToUInt(value, 0U, 255U);
							return;
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value >= 0.5f);
							return;
						}
						MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, value);
						return;
					}
				}
				else
				{
					if (num == 1145195552)
					{
						*(double*)ptr = (double)value;
						return;
					}
					if (num == 1179407392)
					{
						*(float*)ptr = value;
						return;
					}
					if (num == 1229870112)
					{
						*(int*)ptr = NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
						return;
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						*ptr = (byte)((sbyte)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -128, 127));
						return;
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value >= 0f);
						return;
					}
					MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, value * 0.5f + 0.5f);
					return;
				}
			}
			else
			{
				if (num == 1397248596)
				{
					*(short*)ptr = (short)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -32768, 32767);
					return;
				}
				if (num == 1430867540)
				{
					*(int*)ptr = (int)NumberHelpers.NormalizedFloatToUInt(value, 0U, uint.MaxValue);
					return;
				}
				if (num == 1431521364)
				{
					*(short*)ptr = (short)((ushort)NumberHelpers.NormalizedFloatToUInt(value, 0U, 65535U));
					return;
				}
			}
			throw new Exception(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00048D20 File Offset: 0x00046F20
		internal PrimitiveValue FloatToPrimitiveValue(float value)
		{
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							return (byte)NumberHelpers.NormalizedFloatToUInt(value, 0U, 255U);
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							return value >= 0.5f;
						}
						return (int)NumberHelpers.NormalizedFloatToUInt(value, 0U, (uint)((1L << (int)this.sizeInBits) - 1L));
					}
				}
				else
				{
					if (num == 1145195552)
					{
						return value;
					}
					if (num == 1179407392)
					{
						return value;
					}
					if (num == 1229870112)
					{
						return NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						return (sbyte)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -128, 127);
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						return value >= 0f;
					}
					int num2 = (int)(-(int)((int)1L << (int)(this.sizeInBits - 1U)));
					int num3 = (int)((1L << (int)(this.sizeInBits - 1U)) - 1L);
					return NumberHelpers.NormalizedFloatToInt(value, num2, num3);
				}
			}
			else
			{
				if (num == 1397248596)
				{
					return (short)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -32768, 32767);
				}
				if (num == 1430867540)
				{
					return NumberHelpers.NormalizedFloatToUInt(value, 0U, uint.MaxValue);
				}
				if (num == 1431521364)
				{
					return (ushort)NumberHelpers.NormalizedFloatToUInt(value, 0U, 65535U);
				}
			}
			throw new Exception(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00048F10 File Offset: 0x00047110
		public unsafe double ReadDouble(void* statePtr)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							return (double)NumberHelpers.UIntToNormalizedFloat((uint)(*ptr), 0U, 255U);
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							return (double)(MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset) ? 1f : 0f);
						}
						return (double)MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)ptr, this.bitOffset, this.sizeInBits);
					}
				}
				else
				{
					if (num == 1145195552)
					{
						return *(double*)ptr;
					}
					if (num == 1179407392)
					{
						return (double)(*(float*)ptr);
					}
					if (num == 1229870112)
					{
						return (double)(NumberHelpers.IntToNormalizedFloat(*(int*)ptr, int.MinValue, int.MaxValue) * 2f - 1f);
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						return (double)(NumberHelpers.IntToNormalizedFloat((int)(*(sbyte*)ptr), -128, 127) * 2f - 1f);
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						return (double)(MemoryHelpers.ReadSingleBit((void*)ptr, this.bitOffset) ? 1f : (-1f));
					}
					return (double)(MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)ptr, this.bitOffset, this.sizeInBits) * 2f - 1f);
				}
			}
			else
			{
				if (num == 1397248596)
				{
					return (double)(NumberHelpers.IntToNormalizedFloat((int)(*(short*)ptr), -32768, 32767) * 2f - 1f);
				}
				if (num == 1430867540)
				{
					return (double)NumberHelpers.UIntToNormalizedFloat(*(uint*)ptr, 0U, uint.MaxValue);
				}
				if (num == 1431521364)
				{
					return (double)NumberHelpers.UIntToNormalizedFloat((uint)(*(ushort*)ptr), 0U, 65535U);
				}
			}
			throw new Exception(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000490E8 File Offset: 0x000472E8
		public unsafe void WriteDouble(void* statePtr, double value)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							*ptr = (byte)NumberHelpers.NormalizedFloatToUInt((float)value, 0U, 255U);
							return;
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value >= 0.5);
							return;
						}
						MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, (float)value);
						return;
					}
				}
				else
				{
					if (num == 1145195552)
					{
						*(double*)ptr = value;
						return;
					}
					if (num == 1179407392)
					{
						*(float*)ptr = (float)value;
						return;
					}
					if (num == 1229870112)
					{
						*(int*)ptr = NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
						return;
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						*ptr = (byte)((sbyte)NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, -128, 127));
						return;
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value >= 0.0);
						return;
					}
					MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, (float)value * 0.5f + 0.5f);
					return;
				}
			}
			else
			{
				if (num == 1397248596)
				{
					*(short*)ptr = (short)NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, -32768, 32767);
					return;
				}
				if (num == 1430867540)
				{
					*(int*)ptr = (int)NumberHelpers.NormalizedFloatToUInt((float)value, 0U, uint.MaxValue);
					return;
				}
				if (num == 1431521364)
				{
					*(short*)ptr = (short)((ushort)NumberHelpers.NormalizedFloatToUInt((float)value, 0U, 65535U));
					return;
				}
			}
			throw new InvalidOperationException(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000492CC File Offset: 0x000474CC
		public unsafe void Write(void* statePtr, PrimitiveValue value)
		{
			byte* ptr = (byte*)statePtr + this.byteOffset;
			int num = this.format;
			if (num <= 1229870112)
			{
				if (num <= 1113150533)
				{
					if (num != 1112101920)
					{
						if (num == 1113150533)
						{
							*ptr = value.ToByte(null);
							return;
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value.ToBoolean(null));
							return;
						}
						MemoryHelpers.WriteUIntAsMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, value.ToUInt32(null));
						return;
					}
				}
				else
				{
					if (num == 1179407392)
					{
						*(float*)ptr = value.ToSingle(null);
						return;
					}
					if (num == 1229870112)
					{
						*(int*)ptr = value.ToInt32(null);
						return;
					}
				}
			}
			else if (num <= 1396857172)
			{
				if (num != 1396853076)
				{
					if (num == 1396857172)
					{
						*ptr = (byte)value.ToSByte(null);
						return;
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)ptr, this.bitOffset, value.ToBoolean(null));
						return;
					}
					MemoryHelpers.WriteIntAsExcessKMultipleBits((void*)ptr, this.bitOffset, this.sizeInBits, value.ToInt32(null));
					return;
				}
			}
			else
			{
				if (num == 1397248596)
				{
					*(short*)ptr = value.ToInt16(null);
					return;
				}
				if (num == 1430867540)
				{
					*(int*)ptr = (int)value.ToUInt32(null);
					return;
				}
				if (num == 1431521364)
				{
					*(short*)ptr = (short)value.ToUInt16(null);
					return;
				}
			}
			throw new NotImplementedException(string.Format("Writing primitive value of type '{0}' into state block with format '{1}'", value.type, this.format));
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0004945C File Offset: 0x0004765C
		public unsafe void CopyToFrom(void* toStatePtr, void* fromStatePtr)
		{
			if (this.bitOffset != 0U || this.sizeInBits % 8U != 0U)
			{
				throw new NotImplementedException("Copying bitfields");
			}
			byte* ptr = (byte*)fromStatePtr + this.byteOffset;
			UnsafeUtility.MemCpy((void*)((byte*)toStatePtr + this.byteOffset), (void*)ptr, (long)((ulong)this.alignedSizeInBytes));
		}

		// Token: 0x0400060D RID: 1549
		public const uint InvalidOffset = 4294967295U;

		// Token: 0x0400060E RID: 1550
		public const uint AutomaticOffset = 4294967294U;

		// Token: 0x0400060F RID: 1551
		public static readonly FourCC FormatInvalid = new FourCC(0);

		// Token: 0x04000610 RID: 1552
		internal const int kFormatInvalid = 0;

		// Token: 0x04000611 RID: 1553
		public static readonly FourCC FormatBit = new FourCC('B', 'I', 'T', ' ');

		// Token: 0x04000612 RID: 1554
		internal const int kFormatBit = 1112101920;

		// Token: 0x04000613 RID: 1555
		public static readonly FourCC FormatSBit = new FourCC('S', 'B', 'I', 'T');

		// Token: 0x04000614 RID: 1556
		internal const int kFormatSBit = 1396853076;

		// Token: 0x04000615 RID: 1557
		public static readonly FourCC FormatInt = new FourCC('I', 'N', 'T', ' ');

		// Token: 0x04000616 RID: 1558
		internal const int kFormatInt = 1229870112;

		// Token: 0x04000617 RID: 1559
		public static readonly FourCC FormatUInt = new FourCC('U', 'I', 'N', 'T');

		// Token: 0x04000618 RID: 1560
		internal const int kFormatUInt = 1430867540;

		// Token: 0x04000619 RID: 1561
		public static readonly FourCC FormatShort = new FourCC('S', 'H', 'R', 'T');

		// Token: 0x0400061A RID: 1562
		internal const int kFormatShort = 1397248596;

		// Token: 0x0400061B RID: 1563
		public static readonly FourCC FormatUShort = new FourCC('U', 'S', 'H', 'T');

		// Token: 0x0400061C RID: 1564
		internal const int kFormatUShort = 1431521364;

		// Token: 0x0400061D RID: 1565
		public static readonly FourCC FormatByte = new FourCC('B', 'Y', 'T', 'E');

		// Token: 0x0400061E RID: 1566
		internal const int kFormatByte = 1113150533;

		// Token: 0x0400061F RID: 1567
		public static readonly FourCC FormatSByte = new FourCC('S', 'B', 'Y', 'T');

		// Token: 0x04000620 RID: 1568
		internal const int kFormatSByte = 1396857172;

		// Token: 0x04000621 RID: 1569
		public static readonly FourCC FormatLong = new FourCC('L', 'N', 'G', ' ');

		// Token: 0x04000622 RID: 1570
		internal const int kFormatLong = 1280198432;

		// Token: 0x04000623 RID: 1571
		public static readonly FourCC FormatULong = new FourCC('U', 'L', 'N', 'G');

		// Token: 0x04000624 RID: 1572
		internal const int kFormatULong = 1431064135;

		// Token: 0x04000625 RID: 1573
		public static readonly FourCC FormatFloat = new FourCC('F', 'L', 'T', ' ');

		// Token: 0x04000626 RID: 1574
		internal const int kFormatFloat = 1179407392;

		// Token: 0x04000627 RID: 1575
		public static readonly FourCC FormatDouble = new FourCC('D', 'B', 'L', ' ');

		// Token: 0x04000628 RID: 1576
		internal const int kFormatDouble = 1145195552;

		// Token: 0x04000629 RID: 1577
		public static readonly FourCC FormatVector2 = new FourCC('V', 'E', 'C', '2');

		// Token: 0x0400062A RID: 1578
		internal const int kFormatVector2 = 1447379762;

		// Token: 0x0400062B RID: 1579
		public static readonly FourCC FormatVector3 = new FourCC('V', 'E', 'C', '3');

		// Token: 0x0400062C RID: 1580
		internal const int kFormatVector3 = 1447379763;

		// Token: 0x0400062D RID: 1581
		public static readonly FourCC FormatQuaternion = new FourCC('Q', 'U', 'A', 'T');

		// Token: 0x0400062E RID: 1582
		internal const int kFormatQuaternion = 1364541780;

		// Token: 0x0400062F RID: 1583
		public static readonly FourCC FormatVector2Short = new FourCC('V', 'C', '2', 'S');

		// Token: 0x04000630 RID: 1584
		public static readonly FourCC FormatVector3Short = new FourCC('V', 'C', '3', 'S');

		// Token: 0x04000631 RID: 1585
		public static readonly FourCC FormatVector2Byte = new FourCC('V', 'C', '2', 'B');

		// Token: 0x04000632 RID: 1586
		public static readonly FourCC FormatVector3Byte = new FourCC('V', 'C', '3', 'B');

		// Token: 0x04000633 RID: 1587
		public static readonly FourCC FormatPose = new FourCC('P', 'o', 's', 'e');

		// Token: 0x04000634 RID: 1588
		internal const int kFormatPose = 1349481317;

		// Token: 0x04000636 RID: 1590
		internal uint m_ByteOffset;
	}
}

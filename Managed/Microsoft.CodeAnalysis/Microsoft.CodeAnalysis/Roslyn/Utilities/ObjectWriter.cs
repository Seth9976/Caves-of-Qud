using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x02000074 RID: 116
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ObjectWriter : IDisposable
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000A40C File Offset: 0x0000860C
		public ObjectWriter(Stream stream, bool leaveOpen = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			this._writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen);
			this._objectReferenceMap = new ObjectWriter.WriterReferenceMap(false);
			this._stringReferenceMap = new ObjectWriter.WriterReferenceMap(true);
			this._cancellationToken = cancellationToken;
			this._binderSnapshot = ObjectBinder.GetSnapshot();
			this.WriteVersion();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A461 File Offset: 0x00008661
		private void WriteVersion()
		{
			this._writer.Write(170);
			this._writer.Write(11);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000A480 File Offset: 0x00008680
		public void Dispose()
		{
			this._writer.Dispose();
			this._objectReferenceMap.Dispose();
			this._stringReferenceMap.Dispose();
			this._recursionDepth = 0;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000A4AA File Offset: 0x000086AA
		public void WriteBoolean(bool value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public void WriteByte(byte value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000A4C6 File Offset: 0x000086C6
		public void WriteChar(char ch)
		{
			this._writer.Write((ushort)ch);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A4D4 File Offset: 0x000086D4
		public void WriteDecimal(decimal value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A4E2 File Offset: 0x000086E2
		public void WriteDouble(double value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public void WriteSingle(float value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A4FE File Offset: 0x000086FE
		public void WriteInt32(int value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000A50C File Offset: 0x0000870C
		public void WriteInt64(long value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000A51A File Offset: 0x0000871A
		public void WriteSByte(sbyte value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000A528 File Offset: 0x00008728
		public void WriteInt16(short value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000A536 File Offset: 0x00008736
		public void WriteUInt32(uint value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000A544 File Offset: 0x00008744
		public void WriteUInt64(ulong value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A552 File Offset: 0x00008752
		public void WriteUInt16(ushort value)
		{
			this._writer.Write(value);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A560 File Offset: 0x00008760
		[NullableContext(2)]
		public void WriteString(string value)
		{
			this.WriteStringValue(value);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000A56C File Offset: 0x0000876C
		public void WriteGuid(Guid guid)
		{
			ObjectWriter.GuidAccessor guidAccessor = new ObjectWriter.GuidAccessor
			{
				Guid = guid
			};
			this.WriteInt64(guidAccessor.Low64);
			this.WriteInt64(guidAccessor.High64);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000A5A4 File Offset: 0x000087A4
		[NullableContext(2)]
		public void WriteValue(object value)
		{
			if (value == null)
			{
				this._writer.Write(0);
				return;
			}
			Type type = value.GetType();
			if (IntrospectionExtensions.GetTypeInfo(type).IsPrimitive)
			{
				if (value.GetType() == typeof(int))
				{
					this.WriteEncodedInt32((int)value);
					return;
				}
				if (value.GetType() == typeof(double))
				{
					this._writer.Write(49);
					this._writer.Write((double)value);
					return;
				}
				if (value.GetType() == typeof(bool))
				{
					this._writer.Write(((bool)value) ? 11 : 12);
					return;
				}
				if (value.GetType() == typeof(char))
				{
					this._writer.Write(13);
					this._writer.Write((ushort)((char)value));
					return;
				}
				if (value.GetType() == typeof(byte))
				{
					this._writer.Write(31);
					this._writer.Write((byte)value);
					return;
				}
				if (value.GetType() == typeof(short))
				{
					this._writer.Write(15);
					this._writer.Write((short)value);
					return;
				}
				if (value.GetType() == typeof(long))
				{
					this._writer.Write(30);
					this._writer.Write((long)value);
					return;
				}
				if (value.GetType() == typeof(sbyte))
				{
					this._writer.Write(14);
					this._writer.Write((sbyte)value);
					return;
				}
				if (value.GetType() == typeof(float))
				{
					this._writer.Write(48);
					this._writer.Write((float)value);
					return;
				}
				if (value.GetType() == typeof(ushort))
				{
					this._writer.Write(32);
					this._writer.Write((ushort)value);
					return;
				}
				if (value.GetType() == typeof(uint))
				{
					this.WriteEncodedUInt32((uint)value);
					return;
				}
				if (value.GetType() == typeof(ulong))
				{
					this._writer.Write(47);
					this._writer.Write((ulong)value);
					return;
				}
				throw ExceptionUtilities.UnexpectedValue(value.GetType());
			}
			else
			{
				if (value.GetType() == typeof(decimal))
				{
					this._writer.Write(50);
					this._writer.Write((decimal)value);
					return;
				}
				if (value.GetType() == typeof(DateTime))
				{
					this._writer.Write(51);
					this._writer.Write(((DateTime)value).ToBinary());
					return;
				}
				if (value.GetType() == typeof(string))
				{
					this.WriteStringValue((string)value);
					return;
				}
				if (type.IsArray)
				{
					Array array = (Array)value;
					if (array.Rank > 1)
					{
						throw new InvalidOperationException(CodeAnalysisResources.Arrays_with_more_than_one_dimension_cannot_be_serialized);
					}
					this.WriteArray(array);
					return;
				}
				else
				{
					Encoding encoding = value as Encoding;
					if (encoding != null)
					{
						this.WriteEncoding(encoding);
						return;
					}
					this.WriteObject(value, null);
					return;
				}
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000A920 File Offset: 0x00008B20
		[NullableContext(0)]
		public void WriteValue(ReadOnlySpan<byte> span)
		{
			int length = span.Length;
			switch (length)
			{
			case 0:
				this._writer.Write(53);
				break;
			case 1:
				this._writer.Write(54);
				break;
			case 2:
				this._writer.Write(55);
				break;
			case 3:
				this._writer.Write(56);
				break;
			default:
				this._writer.Write(52);
				this.WriteCompressedUInt((uint)length);
				break;
			}
			Type typeFromHandle = typeof(byte);
			this.WritePrimitiveType(typeFromHandle, ObjectWriter.EncodingKind.UInt8);
			byte[] array = new byte[Math.Min(length, 8192)];
			for (int i = 0; i < length; i += array.Length)
			{
				int num = Math.Min(array.Length, length - i);
				span.Slice(i, num).CopyTo(MemoryExtensions.AsSpan<byte>(array));
				this._writer.Write(array, 0, num);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000AA05 File Offset: 0x00008C05
		[NullableContext(2)]
		public void WriteValue(IObjectWritable value)
		{
			if (value == null)
			{
				this._writer.Write(0);
				return;
			}
			this.WriteObject(value, value);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000AA20 File Offset: 0x00008C20
		private void WriteEncodedInt32(int v)
		{
			if (v >= 0 && v <= 10)
			{
				this._writer.Write((byte)(19 + v));
				return;
			}
			if (v >= 0 && v < 255)
			{
				this._writer.Write(17);
				this._writer.Write((byte)v);
				return;
			}
			if (v >= 0 && v < 65535)
			{
				this._writer.Write(18);
				this._writer.Write((ushort)v);
				return;
			}
			this._writer.Write(16);
			this._writer.Write(v);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		private void WriteEncodedUInt32(uint v)
		{
			if (v >= 0U && v <= 10U)
			{
				this._writer.Write((byte)(36U + v));
				return;
			}
			if (v >= 0U && v < 255U)
			{
				this._writer.Write(34);
				this._writer.Write((byte)v);
				return;
			}
			if (v >= 0U && v < 65535U)
			{
				this._writer.Write(35);
				this._writer.Write((ushort)v);
				return;
			}
			this._writer.Write(33);
			this._writer.Write(v);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000AB40 File Offset: 0x00008D40
		internal void WriteCompressedUInt(uint value)
		{
			if (value <= 63U)
			{
				this._writer.Write((byte)value);
				return;
			}
			if (value <= 16383U)
			{
				byte b = (byte)(((value >> 8) & 255U) | 64U);
				byte b2 = (byte)(value & 255U);
				this._writer.Write(b);
				this._writer.Write(b2);
				return;
			}
			if (value <= 1073741823U)
			{
				byte b3 = (byte)(((value >> 24) & 255U) | 128U);
				byte b4 = (byte)((value >> 16) & 255U);
				byte b5 = (byte)((value >> 8) & 255U);
				byte b6 = (byte)(value & 255U);
				this._writer.Write(b3);
				this._writer.Write(b4);
				this._writer.Write(b5);
				this._writer.Write(b6);
				return;
			}
			throw new ArgumentException(CodeAnalysisResources.Value_too_large_to_be_represented_as_a_30_bit_unsigned_integer);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000AC14 File Offset: 0x00008E14
		[NullableContext(2)]
		private unsafe void WriteStringValue(string value)
		{
			if (value == null)
			{
				this._writer.Write(0);
				return;
			}
			int num;
			if (this._stringReferenceMap.TryGetReferenceId(value, out num))
			{
				if (num <= 255)
				{
					this._writer.Write(8);
					this._writer.Write((byte)num);
					return;
				}
				if (num <= 65535)
				{
					this._writer.Write(9);
					this._writer.Write((ushort)num);
					return;
				}
				this._writer.Write(10);
				this._writer.Write(num);
				return;
			}
			else
			{
				this._stringReferenceMap.Add(value, true);
				if (value.IsValidUnicodeString())
				{
					this._writer.Write(6);
					this._writer.Write(value);
					return;
				}
				this._writer.Write(7);
				byte[] array = new byte[value.Length * 2];
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					Marshal.Copy((IntPtr)((void*)ptr), array, 0, array.Length);
				}
				this.WriteCompressedUInt((uint)value.Length);
				this._writer.Write(array);
				return;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000AD24 File Offset: 0x00008F24
		private void WriteArray(Array array)
		{
			int length = array.GetLength(0);
			switch (length)
			{
			case 0:
				this._writer.Write(53);
				break;
			case 1:
				this._writer.Write(54);
				break;
			case 2:
				this._writer.Write(55);
				break;
			case 3:
				this._writer.Write(56);
				break;
			default:
				this._writer.Write(52);
				this.WriteCompressedUInt((uint)length);
				break;
			}
			Type elementType = array.GetType().GetElementType();
			ObjectWriter.EncodingKind encodingKind;
			if (ObjectWriter.s_typeMap.TryGetValue(elementType, ref encodingKind))
			{
				this.WritePrimitiveType(elementType, encodingKind);
				this.WritePrimitiveTypeArrayElements(elementType, encodingKind, array);
				return;
			}
			this.WriteKnownType(elementType);
			int recursionDepth = this._recursionDepth;
			this._recursionDepth++;
			if (this._recursionDepth % 50 == 0)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				SerializationThreadPool.RunOnBackgroundThreadAsync(delegate(object a)
				{
					this.WriteArrayValues((Array)a);
					return null;
				}, array).GetAwaiter().GetResult();
			}
			else
			{
				this.WriteArrayValues(array);
			}
			this._recursionDepth--;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000AE3C File Offset: 0x0000903C
		private void WriteArrayValues(Array array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this.WriteValue(array.GetValue(i));
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000AE68 File Offset: 0x00009068
		private void WritePrimitiveTypeArrayElements(Type type, ObjectWriter.EncodingKind kind, Array instance)
		{
			if (type == typeof(byte))
			{
				this._writer.Write((byte[])instance);
				return;
			}
			if (type == typeof(char))
			{
				this._writer.Write((char[])instance);
				return;
			}
			if (type == typeof(string))
			{
				this.WriteStringArrayElements((string[])instance);
				return;
			}
			if (type == typeof(bool))
			{
				this.WriteBooleanArrayElements((bool[])instance);
				return;
			}
			switch (kind)
			{
			case ObjectWriter.EncodingKind.Int8:
				this.WriteInt8ArrayElements((sbyte[])instance);
				return;
			case ObjectWriter.EncodingKind.Int16:
				this.WriteInt16ArrayElements((short[])instance);
				return;
			case ObjectWriter.EncodingKind.Int32:
				this.WriteInt32ArrayElements((int[])instance);
				return;
			default:
				switch (kind)
				{
				case ObjectWriter.EncodingKind.Int64:
					this.WriteInt64ArrayElements((long[])instance);
					return;
				case ObjectWriter.EncodingKind.UInt8:
					break;
				case ObjectWriter.EncodingKind.UInt16:
					this.WriteUInt16ArrayElements((ushort[])instance);
					return;
				case ObjectWriter.EncodingKind.UInt32:
					this.WriteUInt32ArrayElements((uint[])instance);
					return;
				default:
					switch (kind)
					{
					case ObjectWriter.EncodingKind.UInt64:
						this.WriteUInt64ArrayElements((ulong[])instance);
						return;
					case ObjectWriter.EncodingKind.Float4:
						this.WriteFloat4ArrayElements((float[])instance);
						return;
					case ObjectWriter.EncodingKind.Float8:
						this.WriteFloat8ArrayElements((double[])instance);
						return;
					case ObjectWriter.EncodingKind.Decimal:
						this.WriteDecimalArrayElements((decimal[])instance);
						return;
					}
					break;
				}
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000AFD4 File Offset: 0x000091D4
		private void WriteBooleanArrayElements(bool[] array)
		{
			BitVector bitVector = BitVector.Create(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				bitVector[i] = array[i];
			}
			foreach (ulong num in bitVector.Words())
			{
				this._writer.Write(num);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B04C File Offset: 0x0000924C
		private void WriteStringArrayElements(string[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this.WriteStringValue(array[i]);
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B070 File Offset: 0x00009270
		private void WriteInt8ArrayElements(sbyte[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._writer.Write(array[i]);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B09C File Offset: 0x0000929C
		private void WriteInt16ArrayElements(short[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._writer.Write(array[i]);
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B0C8 File Offset: 0x000092C8
		private void WriteInt32ArrayElements(int[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._writer.Write(array[i]);
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B0F4 File Offset: 0x000092F4
		private void WriteInt64ArrayElements(long[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._writer.Write(array[i]);
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B120 File Offset: 0x00009320
		private void WriteUInt16ArrayElements(ushort[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._writer.Write(array[i]);
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B14C File Offset: 0x0000934C
		private void WriteUInt32ArrayElements(uint[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._writer.Write(array[i]);
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B178 File Offset: 0x00009378
		private void WriteUInt64ArrayElements(ulong[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._writer.Write(array[i]);
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B1A4 File Offset: 0x000093A4
		private void WriteDecimalArrayElements(decimal[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._writer.Write(array[i]);
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B1D4 File Offset: 0x000093D4
		private void WriteFloat4ArrayElements(float[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._writer.Write(array[i]);
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B200 File Offset: 0x00009400
		private void WriteFloat8ArrayElements(double[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._writer.Write(array[i]);
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B229 File Offset: 0x00009429
		private void WritePrimitiveType(Type type, ObjectWriter.EncodingKind kind)
		{
			this._writer.Write((byte)kind);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B237 File Offset: 0x00009437
		public void WriteType(Type type)
		{
			this._writer.Write(1);
			this.WriteString(type.AssemblyQualifiedName);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B251 File Offset: 0x00009451
		private void WriteKnownType(Type type)
		{
			this._writer.Write(1);
			this.WriteInt32(this._binderSnapshot.GetTypeId(type));
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B274 File Offset: 0x00009474
		[NullableContext(2)]
		public void WriteEncoding(Encoding encoding)
		{
			ObjectWriter.EncodingKind encodingKind = ObjectWriter.GetEncodingKind(encoding);
			this.WriteByte((byte)encodingKind);
			if (encodingKind == ObjectWriter.EncodingKind.EncodingName)
			{
				this.WriteString(encoding.WebName);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B2A0 File Offset: 0x000094A0
		[NullableContext(2)]
		private static ObjectWriter.EncodingKind GetEncodingKind(Encoding encoding)
		{
			if (encoding == null)
			{
				return ObjectWriter.EncodingKind.Null;
			}
			int codePage = encoding.CodePage;
			if (codePage <= 1201)
			{
				if (codePage != 1200)
				{
					if (codePage == 1201)
					{
						if (!encoding.Equals(Encoding.BigEndianUnicode) && !ObjectWriter.<GetEncodingKind>g__HasPreamble|53_0(encoding))
						{
							return ObjectWriter.EncodingKind.EncodingUnicode_BE;
						}
						return ObjectWriter.EncodingKind.EncodingUnicode_BE_BOM;
					}
				}
				else
				{
					if (!encoding.Equals(Encoding.Unicode) && !ObjectWriter.<GetEncodingKind>g__HasPreamble|53_0(encoding))
					{
						return ObjectWriter.EncodingKind.EncodingUnicode_LE;
					}
					return ObjectWriter.EncodingKind.EncodingUnicode_LE_BOM;
				}
			}
			else if (codePage != 12000)
			{
				if (codePage != 12001)
				{
					if (codePage == 65001)
					{
						if (!encoding.Equals(Encoding.UTF8) && !ObjectWriter.<GetEncodingKind>g__HasPreamble|53_0(encoding))
						{
							return ObjectWriter.EncodingKind.EncodingUTF8;
						}
						return ObjectWriter.EncodingKind.EncodingUTF8_BOM;
					}
				}
				else
				{
					if (!encoding.Equals(Encoding.UTF32) && !ObjectWriter.<GetEncodingKind>g__HasPreamble|53_0(encoding))
					{
						return ObjectWriter.EncodingKind.EncodingUTF32_BE;
					}
					return ObjectWriter.EncodingKind.EncodingUTF32_BE_BOM;
				}
			}
			else
			{
				if (!encoding.Equals(Encoding.UTF32) && !ObjectWriter.<GetEncodingKind>g__HasPreamble|53_0(encoding))
				{
					return ObjectWriter.EncodingKind.EncodingUTF32_LE;
				}
				return ObjectWriter.EncodingKind.EncodingUTF32_LE_BOM;
			}
			return ObjectWriter.EncodingKind.EncodingName;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000B37C File Offset: 0x0000957C
		private void WriteObject(object instance, [Nullable(2)] IObjectWritable instanceAsWritable)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			int num;
			if (!this._objectReferenceMap.TryGetReferenceId(instance, out num))
			{
				IObjectWritable objectWritable = instanceAsWritable;
				if (objectWritable == null)
				{
					objectWritable = instance as IObjectWritable;
					if (objectWritable == null)
					{
						throw ObjectWriter.NoSerializationWriterException(string.Format("{0} must implement {1}", instance.GetType(), "IObjectWritable"));
					}
				}
				int recursionDepth = this._recursionDepth;
				this._recursionDepth++;
				if (this._recursionDepth % 50 == 0)
				{
					this._cancellationToken.ThrowIfCancellationRequested();
					SerializationThreadPool.RunOnBackgroundThreadAsync(delegate(object obj)
					{
						this.WriteObjectWorker((IObjectWritable)obj);
						return null;
					}, objectWritable).GetAwaiter().GetResult();
				}
				else
				{
					this.WriteObjectWorker(objectWritable);
				}
				this._recursionDepth--;
				return;
			}
			if (num <= 255)
			{
				this._writer.Write(3);
				this._writer.Write((byte)num);
				return;
			}
			if (num <= 65535)
			{
				this._writer.Write(4);
				this._writer.Write((ushort)num);
				return;
			}
			this._writer.Write(5);
			this._writer.Write(num);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000B493 File Offset: 0x00009693
		private void WriteObjectWorker(IObjectWritable writable)
		{
			this._objectReferenceMap.Add(writable, writable.ShouldReuseInSerialization);
			this._writer.Write(2);
			this.WriteInt32(this._binderSnapshot.GetTypeId(writable.GetType()));
			writable.WriteTo(this);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000B4D1 File Offset: 0x000096D1
		private static Exception NoSerializationTypeException(string typeName)
		{
			return new InvalidOperationException(string.Format(CodeAnalysisResources.The_type_0_is_not_understood_by_the_serialization_binder, typeName));
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000B4E3 File Offset: 0x000096E3
		private static Exception NoSerializationWriterException(string typeName)
		{
			return new InvalidOperationException(string.Format(CodeAnalysisResources.Cannot_serialize_type_0, typeName));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000B4F8 File Offset: 0x000096F8
		static ObjectWriter()
		{
			Dictionary<Type, ObjectWriter.EncodingKind> dictionary = new Dictionary<Type, ObjectWriter.EncodingKind>();
			dictionary.Add(typeof(bool), ObjectWriter.EncodingKind.BooleanType);
			dictionary.Add(typeof(char), ObjectWriter.EncodingKind.Char);
			dictionary.Add(typeof(string), ObjectWriter.EncodingKind.StringType);
			dictionary.Add(typeof(sbyte), ObjectWriter.EncodingKind.Int8);
			dictionary.Add(typeof(short), ObjectWriter.EncodingKind.Int16);
			dictionary.Add(typeof(int), ObjectWriter.EncodingKind.Int32);
			dictionary.Add(typeof(long), ObjectWriter.EncodingKind.Int64);
			dictionary.Add(typeof(byte), ObjectWriter.EncodingKind.UInt8);
			dictionary.Add(typeof(ushort), ObjectWriter.EncodingKind.UInt16);
			dictionary.Add(typeof(uint), ObjectWriter.EncodingKind.UInt32);
			dictionary.Add(typeof(ulong), ObjectWriter.EncodingKind.UInt64);
			dictionary.Add(typeof(float), ObjectWriter.EncodingKind.Float4);
			dictionary.Add(typeof(double), ObjectWriter.EncodingKind.Float8);
			dictionary.Add(typeof(decimal), ObjectWriter.EncodingKind.Decimal);
			ObjectWriter.s_typeMap = dictionary;
			Type[] array = new Type[70];
			foreach (KeyValuePair<Type, ObjectWriter.EncodingKind> keyValuePair in ObjectWriter.s_typeMap)
			{
				array[(int)keyValuePair.Value] = keyValuePair.Key;
			}
			ObjectWriter.s_reverseTypeMap = ImmutableArray.Create<Type>(array);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000B67B File Offset: 0x0000987B
		[CompilerGenerated]
		internal static bool <GetEncodingKind>g__HasPreamble|53_0(Encoding encoding)
		{
			return !encoding.GetPreamble().IsEmpty<byte>();
		}

		// Token: 0x040000CA RID: 202
		private readonly BinaryWriter _writer;

		// Token: 0x040000CB RID: 203
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040000CC RID: 204
		private ObjectWriter.WriterReferenceMap _objectReferenceMap;

		// Token: 0x040000CD RID: 205
		private ObjectWriter.WriterReferenceMap _stringReferenceMap;

		// Token: 0x040000CE RID: 206
		private readonly ObjectBinderSnapshot _binderSnapshot;

		// Token: 0x040000CF RID: 207
		private int _recursionDepth;

		// Token: 0x040000D0 RID: 208
		internal const int MaxRecursionDepth = 50;

		// Token: 0x040000D1 RID: 209
		internal static readonly Dictionary<Type, ObjectWriter.EncodingKind> s_typeMap;

		// Token: 0x040000D2 RID: 210
		[Nullable(new byte[] { 0, 1 })]
		internal static readonly ImmutableArray<Type> s_reverseTypeMap;

		// Token: 0x040000D3 RID: 211
		internal const byte ByteMarkerMask = 192;

		// Token: 0x040000D4 RID: 212
		internal const byte Byte1Marker = 0;

		// Token: 0x040000D5 RID: 213
		internal const byte Byte2Marker = 64;

		// Token: 0x040000D6 RID: 214
		internal const byte Byte4Marker = 128;

		// Token: 0x02000519 RID: 1305
		[NullableContext(0)]
		[StructLayout(2)]
		internal struct GuidAccessor
		{
			// Token: 0x0400143D RID: 5181
			[FieldOffset(0)]
			public Guid Guid;

			// Token: 0x0400143E RID: 5182
			[FieldOffset(0)]
			public long Low64;

			// Token: 0x0400143F RID: 5183
			[FieldOffset(8)]
			public long High64;
		}

		// Token: 0x0200051A RID: 1306
		[Nullable(0)]
		private struct WriterReferenceMap
		{
			// Token: 0x06003332 RID: 13106 RVA: 0x0009112F File Offset: 0x0008F32F
			public WriterReferenceMap(bool valueEquality)
			{
				this._valueEquality = valueEquality;
				this._valueToIdMap = ObjectWriter.WriterReferenceMap.GetDictionaryPool(valueEquality).Allocate();
				this._nextId = 0;
			}

			// Token: 0x06003333 RID: 13107 RVA: 0x00091150 File Offset: 0x0008F350
			private static ObjectPool<SegmentedDictionary<object, int>> GetDictionaryPool(bool valueEquality)
			{
				if (!valueEquality)
				{
					return ObjectWriter.WriterReferenceMap.s_referenceDictionaryPool;
				}
				return ObjectWriter.WriterReferenceMap.s_valueDictionaryPool;
			}

			// Token: 0x06003334 RID: 13108 RVA: 0x00091160 File Offset: 0x0008F360
			public void Dispose()
			{
				ObjectPool<SegmentedDictionary<object, int>> dictionaryPool = ObjectWriter.WriterReferenceMap.GetDictionaryPool(this._valueEquality);
				if (this._valueToIdMap.Count <= 1024)
				{
					this._valueToIdMap.Clear();
					dictionaryPool.Free(this._valueToIdMap);
				}
			}

			// Token: 0x06003335 RID: 13109 RVA: 0x000911A2 File Offset: 0x0008F3A2
			public bool TryGetReferenceId(object value, out int referenceId)
			{
				return this._valueToIdMap.TryGetValue(value, out referenceId);
			}

			// Token: 0x06003336 RID: 13110 RVA: 0x000911B4 File Offset: 0x0008F3B4
			public void Add(object value, bool isReusable)
			{
				int nextId = this._nextId;
				this._nextId = nextId + 1;
				int num = nextId;
				if (isReusable)
				{
					this._valueToIdMap.Add(value, num);
				}
			}

			// Token: 0x04001440 RID: 5184
			private readonly SegmentedDictionary<object, int> _valueToIdMap;

			// Token: 0x04001441 RID: 5185
			private readonly bool _valueEquality;

			// Token: 0x04001442 RID: 5186
			private int _nextId;

			// Token: 0x04001443 RID: 5187
			private static readonly ObjectPool<SegmentedDictionary<object, int>> s_referenceDictionaryPool = new ObjectPool<SegmentedDictionary<object, int>>(() => new SegmentedDictionary<object, int>(128, ReferenceEqualityComparer.Instance));

			// Token: 0x04001444 RID: 5188
			private static readonly ObjectPool<SegmentedDictionary<object, int>> s_valueDictionaryPool = new ObjectPool<SegmentedDictionary<object, int>>(() => new SegmentedDictionary<object, int>(128));
		}

		// Token: 0x0200051B RID: 1307
		[NullableContext(0)]
		internal enum EncodingKind : byte
		{
			// Token: 0x04001446 RID: 5190
			Null,
			// Token: 0x04001447 RID: 5191
			Type,
			// Token: 0x04001448 RID: 5192
			Object,
			// Token: 0x04001449 RID: 5193
			ObjectRef_1Byte,
			// Token: 0x0400144A RID: 5194
			ObjectRef_2Bytes,
			// Token: 0x0400144B RID: 5195
			ObjectRef_4Bytes,
			// Token: 0x0400144C RID: 5196
			StringUtf8,
			// Token: 0x0400144D RID: 5197
			StringUtf16,
			// Token: 0x0400144E RID: 5198
			StringRef_1Byte,
			// Token: 0x0400144F RID: 5199
			StringRef_2Bytes,
			// Token: 0x04001450 RID: 5200
			StringRef_4Bytes,
			// Token: 0x04001451 RID: 5201
			Boolean_True,
			// Token: 0x04001452 RID: 5202
			Boolean_False,
			// Token: 0x04001453 RID: 5203
			Char,
			// Token: 0x04001454 RID: 5204
			Int8,
			// Token: 0x04001455 RID: 5205
			Int16,
			// Token: 0x04001456 RID: 5206
			Int32,
			// Token: 0x04001457 RID: 5207
			Int32_1Byte,
			// Token: 0x04001458 RID: 5208
			Int32_2Bytes,
			// Token: 0x04001459 RID: 5209
			Int32_0,
			// Token: 0x0400145A RID: 5210
			Int32_1,
			// Token: 0x0400145B RID: 5211
			Int32_2,
			// Token: 0x0400145C RID: 5212
			Int32_3,
			// Token: 0x0400145D RID: 5213
			Int32_4,
			// Token: 0x0400145E RID: 5214
			Int32_5,
			// Token: 0x0400145F RID: 5215
			Int32_6,
			// Token: 0x04001460 RID: 5216
			Int32_7,
			// Token: 0x04001461 RID: 5217
			Int32_8,
			// Token: 0x04001462 RID: 5218
			Int32_9,
			// Token: 0x04001463 RID: 5219
			Int32_10,
			// Token: 0x04001464 RID: 5220
			Int64,
			// Token: 0x04001465 RID: 5221
			UInt8,
			// Token: 0x04001466 RID: 5222
			UInt16,
			// Token: 0x04001467 RID: 5223
			UInt32,
			// Token: 0x04001468 RID: 5224
			UInt32_1Byte,
			// Token: 0x04001469 RID: 5225
			UInt32_2Bytes,
			// Token: 0x0400146A RID: 5226
			UInt32_0,
			// Token: 0x0400146B RID: 5227
			UInt32_1,
			// Token: 0x0400146C RID: 5228
			UInt32_2,
			// Token: 0x0400146D RID: 5229
			UInt32_3,
			// Token: 0x0400146E RID: 5230
			UInt32_4,
			// Token: 0x0400146F RID: 5231
			UInt32_5,
			// Token: 0x04001470 RID: 5232
			UInt32_6,
			// Token: 0x04001471 RID: 5233
			UInt32_7,
			// Token: 0x04001472 RID: 5234
			UInt32_8,
			// Token: 0x04001473 RID: 5235
			UInt32_9,
			// Token: 0x04001474 RID: 5236
			UInt32_10,
			// Token: 0x04001475 RID: 5237
			UInt64,
			// Token: 0x04001476 RID: 5238
			Float4,
			// Token: 0x04001477 RID: 5239
			Float8,
			// Token: 0x04001478 RID: 5240
			Decimal,
			// Token: 0x04001479 RID: 5241
			DateTime,
			// Token: 0x0400147A RID: 5242
			Array,
			// Token: 0x0400147B RID: 5243
			Array_0,
			// Token: 0x0400147C RID: 5244
			Array_1,
			// Token: 0x0400147D RID: 5245
			Array_2,
			// Token: 0x0400147E RID: 5246
			Array_3,
			// Token: 0x0400147F RID: 5247
			BooleanType,
			// Token: 0x04001480 RID: 5248
			StringType,
			// Token: 0x04001481 RID: 5249
			EncodingName,
			// Token: 0x04001482 RID: 5250
			EncodingUTF8,
			// Token: 0x04001483 RID: 5251
			EncodingUTF8_BOM,
			// Token: 0x04001484 RID: 5252
			EncodingUTF32_BE,
			// Token: 0x04001485 RID: 5253
			EncodingUTF32_BE_BOM,
			// Token: 0x04001486 RID: 5254
			EncodingUTF32_LE,
			// Token: 0x04001487 RID: 5255
			EncodingUTF32_LE_BOM,
			// Token: 0x04001488 RID: 5256
			EncodingUnicode_BE,
			// Token: 0x04001489 RID: 5257
			EncodingUnicode_BE_BOM,
			// Token: 0x0400148A RID: 5258
			EncodingUnicode_LE,
			// Token: 0x0400148B RID: 5259
			EncodingUnicode_LE_BOM,
			// Token: 0x0400148C RID: 5260
			Last
		}
	}
}

using System;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x02000073 RID: 115
	internal sealed class ObjectReader : IDisposable
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00009758 File Offset: 0x00007958
		private ObjectReader(Stream stream, bool leaveOpen, CancellationToken cancellationToken)
		{
			this._reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen);
			this._objectReferenceMap = ObjectReader.ReaderReferenceMap<object>.Create();
			this._stringReferenceMap = ObjectReader.ReaderReferenceMap<string>.Create();
			this._binderSnapshot = ObjectBinder.GetSnapshot();
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000097A5 File Offset: 0x000079A5
		public static ObjectReader TryGetReader(Stream stream, bool leaveOpen = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				return null;
			}
			if (stream.ReadByte() != 170 || stream.ReadByte() != 11)
			{
				return null;
			}
			return new ObjectReader(stream, leaveOpen, cancellationToken);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000097D0 File Offset: 0x000079D0
		public static ObjectReader GetReader(Stream stream, bool leaveOpen, CancellationToken cancellationToken)
		{
			int num = stream.ReadByte();
			if (num == -1)
			{
				throw new EndOfStreamException();
			}
			if (num != 170)
			{
				throw ExceptionUtilities.UnexpectedValue(num);
			}
			num = stream.ReadByte();
			if (num == -1)
			{
				throw new EndOfStreamException();
			}
			if (num != 11)
			{
				throw ExceptionUtilities.UnexpectedValue(num);
			}
			return new ObjectReader(stream, leaveOpen, cancellationToken);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000982C File Offset: 0x00007A2C
		public void Dispose()
		{
			this._objectReferenceMap.Dispose();
			this._stringReferenceMap.Dispose();
			this._recursionDepth = 0;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000985C File Offset: 0x00007A5C
		public bool ReadBoolean()
		{
			return this._reader.ReadBoolean();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00009869 File Offset: 0x00007A69
		public byte ReadByte()
		{
			return this._reader.ReadByte();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009876 File Offset: 0x00007A76
		public char ReadChar()
		{
			return (char)this._reader.ReadUInt16();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009883 File Offset: 0x00007A83
		public decimal ReadDecimal()
		{
			return this._reader.ReadDecimal();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00009890 File Offset: 0x00007A90
		public double ReadDouble()
		{
			return this._reader.ReadDouble();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000989D File Offset: 0x00007A9D
		public float ReadSingle()
		{
			return this._reader.ReadSingle();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000098AA File Offset: 0x00007AAA
		public int ReadInt32()
		{
			return this._reader.ReadInt32();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000098B7 File Offset: 0x00007AB7
		public long ReadInt64()
		{
			return this._reader.ReadInt64();
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000098C4 File Offset: 0x00007AC4
		public sbyte ReadSByte()
		{
			return this._reader.ReadSByte();
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000098D1 File Offset: 0x00007AD1
		public short ReadInt16()
		{
			return this._reader.ReadInt16();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000098DE File Offset: 0x00007ADE
		public uint ReadUInt32()
		{
			return this._reader.ReadUInt32();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000098EB File Offset: 0x00007AEB
		public ulong ReadUInt64()
		{
			return this._reader.ReadUInt64();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000098F8 File Offset: 0x00007AF8
		public ushort ReadUInt16()
		{
			return this._reader.ReadUInt16();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00009905 File Offset: 0x00007B05
		public string ReadString()
		{
			return this.ReadStringValue();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00009910 File Offset: 0x00007B10
		public Guid ReadGuid()
		{
			return new ObjectWriter.GuidAccessor
			{
				Low64 = this.ReadInt64(),
				High64 = this.ReadInt64()
			}.Guid;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00009948 File Offset: 0x00007B48
		public object ReadValue()
		{
			int recursionDepth = this._recursionDepth;
			this._recursionDepth++;
			object obj;
			if (this._recursionDepth % 50 == 0)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				obj = SerializationThreadPool.RunOnBackgroundThreadAsync(() => this.ReadValueWorker()).GetAwaiter().GetResult();
			}
			else
			{
				obj = this.ReadValueWorker();
			}
			this._recursionDepth--;
			return obj;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000099BC File Offset: 0x00007BBC
		private object ReadValueWorker()
		{
			ObjectWriter.EncodingKind encodingKind = (ObjectWriter.EncodingKind)this._reader.ReadByte();
			switch (encodingKind)
			{
			case ObjectWriter.EncodingKind.Null:
				return null;
			case ObjectWriter.EncodingKind.Object:
				return this.ReadObject();
			case ObjectWriter.EncodingKind.ObjectRef_1Byte:
				return this._objectReferenceMap.GetValue((int)this._reader.ReadByte());
			case ObjectWriter.EncodingKind.ObjectRef_2Bytes:
				return this._objectReferenceMap.GetValue((int)this._reader.ReadUInt16());
			case ObjectWriter.EncodingKind.ObjectRef_4Bytes:
				return this._objectReferenceMap.GetValue(this._reader.ReadInt32());
			case ObjectWriter.EncodingKind.StringUtf8:
			case ObjectWriter.EncodingKind.StringUtf16:
			case ObjectWriter.EncodingKind.StringRef_1Byte:
			case ObjectWriter.EncodingKind.StringRef_2Bytes:
			case ObjectWriter.EncodingKind.StringRef_4Bytes:
				return this.ReadStringValue(encodingKind);
			case ObjectWriter.EncodingKind.Boolean_True:
				return true;
			case ObjectWriter.EncodingKind.Boolean_False:
				return false;
			case ObjectWriter.EncodingKind.Char:
				return (char)this._reader.ReadUInt16();
			case ObjectWriter.EncodingKind.Int8:
				return this._reader.ReadSByte();
			case ObjectWriter.EncodingKind.Int16:
				return this._reader.ReadInt16();
			case ObjectWriter.EncodingKind.Int32:
				return this._reader.ReadInt32();
			case ObjectWriter.EncodingKind.Int32_1Byte:
				return (int)this._reader.ReadByte();
			case ObjectWriter.EncodingKind.Int32_2Bytes:
				return (int)this._reader.ReadUInt16();
			case ObjectWriter.EncodingKind.Int32_0:
			case ObjectWriter.EncodingKind.Int32_1:
			case ObjectWriter.EncodingKind.Int32_2:
			case ObjectWriter.EncodingKind.Int32_3:
			case ObjectWriter.EncodingKind.Int32_4:
			case ObjectWriter.EncodingKind.Int32_5:
			case ObjectWriter.EncodingKind.Int32_6:
			case ObjectWriter.EncodingKind.Int32_7:
			case ObjectWriter.EncodingKind.Int32_8:
			case ObjectWriter.EncodingKind.Int32_9:
			case ObjectWriter.EncodingKind.Int32_10:
				return (int)(encodingKind - ObjectWriter.EncodingKind.Int32_0);
			case ObjectWriter.EncodingKind.Int64:
				return this._reader.ReadInt64();
			case ObjectWriter.EncodingKind.UInt8:
				return this._reader.ReadByte();
			case ObjectWriter.EncodingKind.UInt16:
				return this._reader.ReadUInt16();
			case ObjectWriter.EncodingKind.UInt32:
				return this._reader.ReadUInt32();
			case ObjectWriter.EncodingKind.UInt32_1Byte:
				return (uint)this._reader.ReadByte();
			case ObjectWriter.EncodingKind.UInt32_2Bytes:
				return (uint)this._reader.ReadUInt16();
			case ObjectWriter.EncodingKind.UInt32_0:
			case ObjectWriter.EncodingKind.UInt32_1:
			case ObjectWriter.EncodingKind.UInt32_2:
			case ObjectWriter.EncodingKind.UInt32_3:
			case ObjectWriter.EncodingKind.UInt32_4:
			case ObjectWriter.EncodingKind.UInt32_5:
			case ObjectWriter.EncodingKind.UInt32_6:
			case ObjectWriter.EncodingKind.UInt32_7:
			case ObjectWriter.EncodingKind.UInt32_8:
			case ObjectWriter.EncodingKind.UInt32_9:
			case ObjectWriter.EncodingKind.UInt32_10:
				return (uint)(encodingKind - ObjectWriter.EncodingKind.UInt32_0);
			case ObjectWriter.EncodingKind.UInt64:
				return this._reader.ReadUInt64();
			case ObjectWriter.EncodingKind.Float4:
				return this._reader.ReadSingle();
			case ObjectWriter.EncodingKind.Float8:
				return this._reader.ReadDouble();
			case ObjectWriter.EncodingKind.Decimal:
				return this._reader.ReadDecimal();
			case ObjectWriter.EncodingKind.DateTime:
				return DateTime.FromBinary(this._reader.ReadInt64());
			case ObjectWriter.EncodingKind.Array:
			case ObjectWriter.EncodingKind.Array_0:
			case ObjectWriter.EncodingKind.Array_1:
			case ObjectWriter.EncodingKind.Array_2:
			case ObjectWriter.EncodingKind.Array_3:
				return this.ReadArray(encodingKind);
			case ObjectWriter.EncodingKind.EncodingName:
				return Encoding.GetEncoding(this.ReadString());
			case ObjectWriter.EncodingKind.EncodingUTF8:
				return ObjectReader.s_encodingUTF8;
			case ObjectWriter.EncodingKind.EncodingUTF8_BOM:
				return Encoding.UTF8;
			case ObjectWriter.EncodingKind.EncodingUTF32_BE:
				return ObjectReader.s_encodingUTF32_BE;
			case ObjectWriter.EncodingKind.EncodingUTF32_BE_BOM:
				return ObjectReader.s_encodingUTF32_BE_BOM;
			case ObjectWriter.EncodingKind.EncodingUTF32_LE:
				return ObjectReader.s_encodingUTF32_LE;
			case ObjectWriter.EncodingKind.EncodingUTF32_LE_BOM:
				return Encoding.UTF32;
			case ObjectWriter.EncodingKind.EncodingUnicode_BE:
				return ObjectReader.s_encodingUnicode_BE;
			case ObjectWriter.EncodingKind.EncodingUnicode_BE_BOM:
				return Encoding.BigEndianUnicode;
			case ObjectWriter.EncodingKind.EncodingUnicode_LE:
				return ObjectReader.s_encodingUnicode_LE;
			case ObjectWriter.EncodingKind.EncodingUnicode_LE_BOM:
				return Encoding.Unicode;
			}
			throw ExceptionUtilities.UnexpectedValue(encodingKind);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00009CFC File Offset: 0x00007EFC
		internal uint ReadCompressedUInt()
		{
			byte b = this._reader.ReadByte();
			byte b2 = b & 192;
			byte b3 = (byte)((int)b & -193);
			if (b2 == 0)
			{
				return (uint)b3;
			}
			if (b2 == 64)
			{
				byte b4 = this._reader.ReadByte();
				return (uint)(((int)b3 << 8) | (int)b4);
			}
			if (b2 == 128)
			{
				byte b5 = this._reader.ReadByte();
				byte b6 = this._reader.ReadByte();
				byte b7 = this._reader.ReadByte();
				return (uint)(((int)b3 << 24) | ((int)b5 << 16) | ((int)b6 << 8) | (int)b7);
			}
			throw ExceptionUtilities.UnexpectedValue(b2);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00009D8C File Offset: 0x00007F8C
		private string ReadStringValue()
		{
			ObjectWriter.EncodingKind encodingKind = (ObjectWriter.EncodingKind)this._reader.ReadByte();
			if (encodingKind != ObjectWriter.EncodingKind.Null)
			{
				return this.ReadStringValue(encodingKind);
			}
			return null;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00009DB4 File Offset: 0x00007FB4
		private string ReadStringValue(ObjectWriter.EncodingKind kind)
		{
			switch (kind)
			{
			case ObjectWriter.EncodingKind.StringUtf8:
			case ObjectWriter.EncodingKind.StringUtf16:
				return this.ReadStringLiteral(kind);
			case ObjectWriter.EncodingKind.StringRef_1Byte:
				return this._stringReferenceMap.GetValue((int)this._reader.ReadByte());
			case ObjectWriter.EncodingKind.StringRef_2Bytes:
				return this._stringReferenceMap.GetValue((int)this._reader.ReadUInt16());
			case ObjectWriter.EncodingKind.StringRef_4Bytes:
				return this._stringReferenceMap.GetValue(this._reader.ReadInt32());
			default:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00009E40 File Offset: 0x00008040
		private unsafe string ReadStringLiteral(ObjectWriter.EncodingKind kind)
		{
			string text;
			if (kind == ObjectWriter.EncodingKind.StringUtf8)
			{
				text = this._reader.ReadString();
			}
			else
			{
				int num = (int)this.ReadCompressedUInt();
				byte[] array;
				byte* ptr;
				if ((array = this._reader.ReadBytes(num * 2)) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				text = new string((char*)ptr, 0, num);
				array = null;
			}
			this._stringReferenceMap.AddValue(text);
			return text;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00009EA8 File Offset: 0x000080A8
		private Array ReadArray(ObjectWriter.EncodingKind kind)
		{
			int num;
			switch (kind)
			{
			case ObjectWriter.EncodingKind.Array_0:
				num = 0;
				break;
			case ObjectWriter.EncodingKind.Array_1:
				num = 1;
				break;
			case ObjectWriter.EncodingKind.Array_2:
				num = 2;
				break;
			case ObjectWriter.EncodingKind.Array_3:
				num = 3;
				break;
			default:
				num = (int)this.ReadCompressedUInt();
				break;
			}
			ObjectWriter.EncodingKind encodingKind = (ObjectWriter.EncodingKind)this._reader.ReadByte();
			Type type = ObjectWriter.s_reverseTypeMap[(int)encodingKind];
			if (type != null)
			{
				return this.ReadPrimitiveTypeArrayElements(type, encodingKind, num);
			}
			type = this.ReadTypeAfterTag();
			Array array = Array.CreateInstance(type, num);
			for (int i = 0; i < num; i++)
			{
				object obj = this.ReadValue();
				array.SetValue(obj, i);
			}
			return array;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00009F4C File Offset: 0x0000814C
		private Array ReadPrimitiveTypeArrayElements(Type type, ObjectWriter.EncodingKind kind, int length)
		{
			if (type == typeof(byte))
			{
				return this._reader.ReadBytes(length);
			}
			if (type == typeof(char))
			{
				return this._reader.ReadChars(length);
			}
			if (type == typeof(string))
			{
				return this.ReadStringArrayElements(ObjectReader.CreateArray<string>(length));
			}
			if (type == typeof(bool))
			{
				return this.ReadBooleanArrayElements(ObjectReader.CreateArray<bool>(length));
			}
			switch (kind)
			{
			case ObjectWriter.EncodingKind.Int8:
				return this.ReadInt8ArrayElements(ObjectReader.CreateArray<sbyte>(length));
			case ObjectWriter.EncodingKind.Int16:
				return this.ReadInt16ArrayElements(ObjectReader.CreateArray<short>(length));
			case ObjectWriter.EncodingKind.Int32:
				return this.ReadInt32ArrayElements(ObjectReader.CreateArray<int>(length));
			default:
				switch (kind)
				{
				case ObjectWriter.EncodingKind.Int64:
					return this.ReadInt64ArrayElements(ObjectReader.CreateArray<long>(length));
				case ObjectWriter.EncodingKind.UInt8:
					break;
				case ObjectWriter.EncodingKind.UInt16:
					return this.ReadUInt16ArrayElements(ObjectReader.CreateArray<ushort>(length));
				case ObjectWriter.EncodingKind.UInt32:
					return this.ReadUInt32ArrayElements(ObjectReader.CreateArray<uint>(length));
				default:
					switch (kind)
					{
					case ObjectWriter.EncodingKind.UInt64:
						return this.ReadUInt64ArrayElements(ObjectReader.CreateArray<ulong>(length));
					case ObjectWriter.EncodingKind.Float4:
						return this.ReadFloat4ArrayElements(ObjectReader.CreateArray<float>(length));
					case ObjectWriter.EncodingKind.Float8:
						return this.ReadFloat8ArrayElements(ObjectReader.CreateArray<double>(length));
					case ObjectWriter.EncodingKind.Decimal:
						return this.ReadDecimalArrayElements(ObjectReader.CreateArray<decimal>(length));
					}
					break;
				}
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A0B0 File Offset: 0x000082B0
		private bool[] ReadBooleanArrayElements(bool[] array)
		{
			int num = BitVector.WordsRequired(array.Length);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				ulong num3 = this._reader.ReadUInt64();
				for (int j = 0; j < 64; j++)
				{
					if (num2 >= array.Length)
					{
						return array;
					}
					array[num2++] = BitVector.IsTrue(num3, j);
				}
			}
			return array;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000A109 File Offset: 0x00008309
		private static T[] CreateArray<T>(int length)
		{
			if (length == 0)
			{
				return Array.Empty<T>();
			}
			return new T[length];
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000A11C File Offset: 0x0000831C
		private string[] ReadStringArrayElements(string[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.ReadStringValue();
			}
			return array;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000A144 File Offset: 0x00008344
		private sbyte[] ReadInt8ArrayElements(sbyte[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._reader.ReadSByte();
			}
			return array;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A170 File Offset: 0x00008370
		private short[] ReadInt16ArrayElements(short[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._reader.ReadInt16();
			}
			return array;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A19C File Offset: 0x0000839C
		private int[] ReadInt32ArrayElements(int[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._reader.ReadInt32();
			}
			return array;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A1C8 File Offset: 0x000083C8
		private long[] ReadInt64ArrayElements(long[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._reader.ReadInt64();
			}
			return array;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A1F4 File Offset: 0x000083F4
		private ushort[] ReadUInt16ArrayElements(ushort[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._reader.ReadUInt16();
			}
			return array;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000A220 File Offset: 0x00008420
		private uint[] ReadUInt32ArrayElements(uint[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._reader.ReadUInt32();
			}
			return array;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A24C File Offset: 0x0000844C
		private ulong[] ReadUInt64ArrayElements(ulong[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._reader.ReadUInt64();
			}
			return array;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A278 File Offset: 0x00008478
		private decimal[] ReadDecimalArrayElements(decimal[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._reader.ReadDecimal();
			}
			return array;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A2A8 File Offset: 0x000084A8
		private float[] ReadFloat4ArrayElements(float[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._reader.ReadSingle();
			}
			return array;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A2D4 File Offset: 0x000084D4
		private double[] ReadFloat8ArrayElements(double[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._reader.ReadDouble();
			}
			return array;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A2FE File Offset: 0x000084FE
		public Type ReadType()
		{
			this._reader.ReadByte();
			return Type.GetType(this.ReadString());
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A317 File Offset: 0x00008517
		private Type ReadTypeAfterTag()
		{
			return this._binderSnapshot.GetTypeFromId(this.ReadInt32());
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A32C File Offset: 0x0000852C
		private object ReadObject()
		{
			int nextObjectId = this._objectReferenceMap.GetNextObjectId();
			IObjectWritable objectWritable = this._binderSnapshot.GetTypeReaderFromId(this.ReadInt32()).Invoke(this);
			if (objectWritable.ShouldReuseInSerialization)
			{
				this._objectReferenceMap.AddValue(nextObjectId, objectWritable);
			}
			return objectWritable;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A379 File Offset: 0x00008579
		private static Exception DeserializationReadIncorrectNumberOfValuesException(string typeName)
		{
			throw new InvalidOperationException(string.Format(CodeAnalysisResources.Deserialization_reader_for_0_read_incorrect_number_of_values, typeName));
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A38B File Offset: 0x0000858B
		private static Exception NoSerializationTypeException(string typeName)
		{
			return new InvalidOperationException(string.Format(CodeAnalysisResources.The_type_0_is_not_understood_by_the_serialization_binder, typeName));
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A39D File Offset: 0x0000859D
		private static Exception NoSerializationReaderException(string typeName)
		{
			return new InvalidOperationException(string.Format(CodeAnalysisResources.Cannot_serialize_type_0, typeName));
		}

		// Token: 0x040000BC RID: 188
		internal const byte VersionByte1 = 170;

		// Token: 0x040000BD RID: 189
		internal const byte VersionByte2 = 11;

		// Token: 0x040000BE RID: 190
		private readonly BinaryReader _reader;

		// Token: 0x040000BF RID: 191
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040000C0 RID: 192
		private readonly ObjectReader.ReaderReferenceMap<object> _objectReferenceMap;

		// Token: 0x040000C1 RID: 193
		private readonly ObjectReader.ReaderReferenceMap<string> _stringReferenceMap;

		// Token: 0x040000C2 RID: 194
		private readonly ObjectBinderSnapshot _binderSnapshot;

		// Token: 0x040000C3 RID: 195
		private int _recursionDepth;

		// Token: 0x040000C4 RID: 196
		private static readonly Encoding s_encodingUTF8 = new UTF8Encoding(false);

		// Token: 0x040000C5 RID: 197
		private static readonly Encoding s_encodingUTF32_BE = new UTF32Encoding(true, false);

		// Token: 0x040000C6 RID: 198
		private static readonly Encoding s_encodingUTF32_BE_BOM = new UTF32Encoding(true, true);

		// Token: 0x040000C7 RID: 199
		private static readonly Encoding s_encodingUTF32_LE = new UTF32Encoding(false, false);

		// Token: 0x040000C8 RID: 200
		private static readonly Encoding s_encodingUnicode_BE = new UnicodeEncoding(true, false);

		// Token: 0x040000C9 RID: 201
		private static readonly Encoding s_encodingUnicode_LE = new UnicodeEncoding(false, false);

		// Token: 0x02000518 RID: 1304
		private struct ReaderReferenceMap<T> : IDisposable where T : class
		{
			// Token: 0x0600332A RID: 13098 RVA: 0x00091082 File Offset: 0x0008F282
			private ReaderReferenceMap(SegmentedList<T> values)
			{
				this._values = values;
			}

			// Token: 0x0600332B RID: 13099 RVA: 0x0009108B File Offset: 0x0008F28B
			public static ObjectReader.ReaderReferenceMap<T> Create()
			{
				return new ObjectReader.ReaderReferenceMap<T>(ObjectReader.ReaderReferenceMap<T>.s_objectListPool.Allocate());
			}

			// Token: 0x0600332C RID: 13100 RVA: 0x0009109C File Offset: 0x0008F29C
			public void Dispose()
			{
				this._values.Clear();
				ObjectReader.ReaderReferenceMap<T>.s_objectListPool.Free(this._values);
			}

			// Token: 0x0600332D RID: 13101 RVA: 0x000910BC File Offset: 0x0008F2BC
			public int GetNextObjectId()
			{
				int count = this._values.Count;
				this._values.Add(default(T));
				return count;
			}

			// Token: 0x0600332E RID: 13102 RVA: 0x000910E8 File Offset: 0x0008F2E8
			public void AddValue(T value)
			{
				this._values.Add(value);
			}

			// Token: 0x0600332F RID: 13103 RVA: 0x000910F6 File Offset: 0x0008F2F6
			public void AddValue(int index, T value)
			{
				this._values[index] = value;
			}

			// Token: 0x06003330 RID: 13104 RVA: 0x00091105 File Offset: 0x0008F305
			public T GetValue(int referenceId)
			{
				return this._values[referenceId];
			}

			// Token: 0x0400143B RID: 5179
			private readonly SegmentedList<T> _values;

			// Token: 0x0400143C RID: 5180
			private static readonly ObjectPool<SegmentedList<T>> s_objectListPool = new ObjectPool<SegmentedList<T>>(() => new SegmentedList<T>(20));
		}
	}
}

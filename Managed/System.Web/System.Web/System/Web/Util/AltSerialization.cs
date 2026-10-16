using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Web.Util
{
	// Token: 0x02000136 RID: 310
	internal sealed class AltSerialization
	{
		// Token: 0x06000E4C RID: 3660 RVA: 0x00002050 File Offset: 0x00000250
		private AltSerialization()
		{
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00026928 File Offset: 0x00024B28
		internal static void Serialize(BinaryWriter w, object value)
		{
			TypeCode typeCode = ((value != null) ? Type.GetTypeCode(value.GetType()) : TypeCode.Empty);
			w.Write((byte)typeCode);
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
			case (TypeCode)17:
				break;
			case TypeCode.Object:
				new BinaryFormatter().Serialize(w.BaseStream, value);
				return;
			case TypeCode.Boolean:
				w.Write((bool)value);
				return;
			case TypeCode.Char:
				w.Write((char)value);
				return;
			case TypeCode.SByte:
				w.Write((sbyte)value);
				return;
			case TypeCode.Byte:
				w.Write((byte)value);
				return;
			case TypeCode.Int16:
				w.Write((short)value);
				return;
			case TypeCode.UInt16:
				w.Write((ushort)value);
				return;
			case TypeCode.Int32:
				w.Write((int)value);
				return;
			case TypeCode.UInt32:
				w.Write((uint)value);
				return;
			case TypeCode.Int64:
				w.Write((long)value);
				return;
			case TypeCode.UInt64:
				w.Write((ulong)value);
				break;
			case TypeCode.Single:
				w.Write((float)value);
				return;
			case TypeCode.Double:
				w.Write((double)value);
				return;
			case TypeCode.Decimal:
				w.Write((decimal)value);
				return;
			case TypeCode.DateTime:
				w.Write(((DateTime)value).Ticks);
				return;
			case TypeCode.String:
				w.Write((string)value);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00026A80 File Offset: 0x00024C80
		internal static object Deserialize(BinaryReader r)
		{
			TypeCode typeCode = (TypeCode)r.ReadByte();
			switch (typeCode)
			{
			case TypeCode.Empty:
				return null;
			case TypeCode.Object:
				return new BinaryFormatter().Deserialize(r.BaseStream);
			case TypeCode.DBNull:
				return DBNull.Value;
			case TypeCode.Boolean:
				return r.ReadBoolean();
			case TypeCode.Char:
				return r.ReadChar();
			case TypeCode.SByte:
				return r.ReadSByte();
			case TypeCode.Byte:
				return r.ReadByte();
			case TypeCode.Int16:
				return r.ReadInt16();
			case TypeCode.UInt16:
				return r.ReadUInt16();
			case TypeCode.Int32:
				return r.ReadInt32();
			case TypeCode.UInt32:
				return r.ReadUInt32();
			case TypeCode.Int64:
				return r.ReadInt64();
			case TypeCode.UInt64:
				return r.ReadUInt64();
			case TypeCode.Single:
				return r.ReadSingle();
			case TypeCode.Double:
				return r.ReadDouble();
			case TypeCode.Decimal:
				return r.ReadDecimal();
			case TypeCode.DateTime:
				return new DateTime(r.ReadInt64());
			case TypeCode.String:
				return r.ReadString();
			}
			throw new ArgumentOutOfRangeException("TypeCode:" + typeCode.ToString());
		}
	}
}

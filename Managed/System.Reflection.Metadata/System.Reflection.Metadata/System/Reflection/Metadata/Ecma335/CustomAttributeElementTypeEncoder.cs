using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FF RID: 255
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct CustomAttributeElementTypeEncoder
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001ACD1 File Offset: 0x00018ED1
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x0600097E RID: 2430 RVA: 0x0001ACD9 File Offset: 0x00018ED9
		public CustomAttributeElementTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001ACE2 File Offset: 0x00018EE2
		private void WriteTypeCode(SerializationTypeCode value)
		{
			this.Builder.WriteByte((byte)value);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		public void Boolean()
		{
			this.WriteTypeCode(SerializationTypeCode.Boolean);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001ACF9 File Offset: 0x00018EF9
		public void Char()
		{
			this.WriteTypeCode(SerializationTypeCode.Char);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001AD02 File Offset: 0x00018F02
		public void SByte()
		{
			this.WriteTypeCode(SerializationTypeCode.SByte);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001AD0B File Offset: 0x00018F0B
		public void Byte()
		{
			this.WriteTypeCode(SerializationTypeCode.Byte);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001AD14 File Offset: 0x00018F14
		public void Int16()
		{
			this.WriteTypeCode(SerializationTypeCode.Int16);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001AD1D File Offset: 0x00018F1D
		public void UInt16()
		{
			this.WriteTypeCode(SerializationTypeCode.UInt16);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001AD26 File Offset: 0x00018F26
		public void Int32()
		{
			this.WriteTypeCode(SerializationTypeCode.Int32);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001AD2F File Offset: 0x00018F2F
		public void UInt32()
		{
			this.WriteTypeCode(SerializationTypeCode.UInt32);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001AD39 File Offset: 0x00018F39
		public void Int64()
		{
			this.WriteTypeCode(SerializationTypeCode.Int64);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001AD43 File Offset: 0x00018F43
		public void UInt64()
		{
			this.WriteTypeCode(SerializationTypeCode.UInt64);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001AD4D File Offset: 0x00018F4D
		public void Single()
		{
			this.WriteTypeCode(SerializationTypeCode.Single);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001AD57 File Offset: 0x00018F57
		public void Double()
		{
			this.WriteTypeCode(SerializationTypeCode.Double);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001AD61 File Offset: 0x00018F61
		public void String()
		{
			this.WriteTypeCode(SerializationTypeCode.String);
		}

		/// <param name="type" />
		// Token: 0x0600098D RID: 2445 RVA: 0x0001AD6B File Offset: 0x00018F6B
		public void PrimitiveType(PrimitiveSerializationTypeCode type)
		{
			if (type - PrimitiveSerializationTypeCode.Boolean <= 12)
			{
				this.WriteTypeCode((SerializationTypeCode)type);
				return;
			}
			Throw.ArgumentOutOfRange("type");
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001AD86 File Offset: 0x00018F86
		public void SystemType()
		{
			this.WriteTypeCode(SerializationTypeCode.Type);
		}

		/// <param name="enumTypeName" />
		// Token: 0x0600098F RID: 2447 RVA: 0x0001AD90 File Offset: 0x00018F90
		public void Enum(string enumTypeName)
		{
			if (enumTypeName == null)
			{
				Throw.ArgumentNull("enumTypeName");
			}
			if (enumTypeName.Length == 0)
			{
				Throw.ArgumentEmptyString("enumTypeName");
			}
			this.WriteTypeCode(SerializationTypeCode.Enum);
			this.Builder.WriteSerializedString(enumTypeName);
		}
	}
}

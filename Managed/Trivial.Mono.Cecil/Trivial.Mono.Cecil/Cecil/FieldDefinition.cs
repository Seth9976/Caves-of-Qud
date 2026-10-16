using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000034 RID: 52
	public sealed class FieldDefinition : FieldReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, IConstantProvider, IMarshalInfoProvider
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00005646 File Offset: 0x00003846
		public bool HasLayoutInfo
		{
			get
			{
				if (this.offset >= 0)
				{
					return true;
				}
				this.ResolveLayout();
				return this.offset >= 0;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00005665 File Offset: 0x00003865
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000568E File Offset: 0x0000388E
		public int Offset
		{
			get
			{
				if (this.offset >= 0)
				{
					return this.offset;
				}
				this.ResolveLayout();
				if (this.offset < 0)
				{
					return -1;
				}
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00005697 File Offset: 0x00003897
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x000056A4 File Offset: 0x000038A4
		internal FieldDefinitionProjection WindowsRuntimeProjection
		{
			get
			{
				return (FieldDefinitionProjection)this.projection;
			}
			set
			{
				this.projection = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000056AD File Offset: 0x000038AD
		public int RVA
		{
			get
			{
				if (this.rva > 0)
				{
					return this.rva;
				}
				this.ResolveRVA();
				if (this.rva <= 0)
				{
					return 0;
				}
				return this.rva;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000056D6 File Offset: 0x000038D6
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00005706 File Offset: 0x00003906
		public byte[] InitialValue
		{
			get
			{
				if (this.initial_value != null)
				{
					return this.initial_value;
				}
				this.ResolveRVA();
				if (this.initial_value == null)
				{
					this.initial_value = Empty<byte>.Array;
				}
				return this.initial_value;
			}
			set
			{
				this.initial_value = value;
				this.rva = 0;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005716 File Offset: 0x00003916
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000571E File Offset: 0x0000391E
		public FieldAttributes Attributes
		{
			get
			{
				return (FieldAttributes)this.attributes;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != (FieldAttributes)this.attributes)
				{
					throw new InvalidOperationException();
				}
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000573E File Offset: 0x0000393E
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005762 File Offset: 0x00003962
		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref this.constant, this.Module);
				return this.constant != Mixin.NoValue;
			}
			set
			{
				if (!value)
				{
					this.constant = Mixin.NoValue;
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005772 File Offset: 0x00003972
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005784 File Offset: 0x00003984
		public object Constant
		{
			get
			{
				if (!this.HasConstant)
				{
					return null;
				}
				return this.constant;
			}
			set
			{
				this.constant = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000578D File Offset: 0x0000398D
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.Module);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000057B2 File Offset: 0x000039B2
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000057D0 File Offset: 0x000039D0
		public bool HasMarshalInfo
		{
			get
			{
				return this.marshal_info != null || this.GetHasMarshalInfo(this.Module);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000057E8 File Offset: 0x000039E8
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00005806 File Offset: 0x00003A06
		public MarshalInfo MarshalInfo
		{
			get
			{
				return this.marshal_info ?? this.GetMarshalInfo(ref this.marshal_info, this.Module);
			}
			set
			{
				this.marshal_info = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000580F File Offset: 0x00003A0F
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000581E File Offset: 0x00003A1E
		public bool IsCompilerControlled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 0U, value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005834 File Offset: 0x00003A34
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00005843 File Offset: 0x00003A43
		public bool IsPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 1U, value);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005859 File Offset: 0x00003A59
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00005868 File Offset: 0x00003A68
		public bool IsFamilyAndAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 2U, value);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000587E File Offset: 0x00003A7E
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000588D File Offset: 0x00003A8D
		public bool IsAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 3U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 3U, value);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000058A3 File Offset: 0x00003AA3
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000058B2 File Offset: 0x00003AB2
		public bool IsFamily
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 4U, value);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000058C8 File Offset: 0x00003AC8
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000058D7 File Offset: 0x00003AD7
		public bool IsFamilyOrAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 5U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 5U, value);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000058ED File Offset: 0x00003AED
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000058FC File Offset: 0x00003AFC
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 6U, value);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005912 File Offset: 0x00003B12
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00005921 File Offset: 0x00003B21
		public bool IsStatic
		{
			get
			{
				return this.attributes.GetAttributes(16);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16, value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005937 File Offset: 0x00003B37
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00005946 File Offset: 0x00003B46
		public bool IsInitOnly
		{
			get
			{
				return this.attributes.GetAttributes(32);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(32, value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000595C File Offset: 0x00003B5C
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000596B File Offset: 0x00003B6B
		public bool IsLiteral
		{
			get
			{
				return this.attributes.GetAttributes(64);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(64, value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00005981 File Offset: 0x00003B81
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00005993 File Offset: 0x00003B93
		public bool IsNotSerialized
		{
			get
			{
				return this.attributes.GetAttributes(128);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(128, value);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000059AC File Offset: 0x00003BAC
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000059BE File Offset: 0x00003BBE
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(512);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(512, value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000059D7 File Offset: 0x00003BD7
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000059E9 File Offset: 0x00003BE9
		public bool IsPInvokeImpl
		{
			get
			{
				return this.attributes.GetAttributes(8192);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8192, value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00005A02 File Offset: 0x00003C02
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00005A14 File Offset: 0x00003C14
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(1024);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024, value);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00005A2D File Offset: 0x00003C2D
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00005A3F File Offset: 0x00003C3F
		public bool HasDefault
		{
			get
			{
				return this.attributes.GetAttributes(32768);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(32768, value);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00004E58 File Offset: 0x00003058
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00004E65 File Offset: 0x00003065
		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005A58 File Offset: 0x00003C58
		private void ResolveLayout()
		{
			if (this.offset == -2)
			{
				if (!base.HasImage)
				{
					this.offset = -1;
					return;
				}
				this.offset = this.Module.Read<FieldDefinition, int>(this, (FieldDefinition field, MetadataReader reader) => reader.ReadFieldLayout(field));
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005AB0 File Offset: 0x00003CB0
		private void ResolveRVA()
		{
			if (this.rva == -2 && base.HasImage)
			{
				this.rva = this.Module.Read<FieldDefinition, int>(this, (FieldDefinition field, MetadataReader reader) => reader.ReadFieldRVA(field));
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005B00 File Offset: 0x00003D00
		public FieldDefinition(string name, FieldAttributes attributes, TypeReference fieldType)
			: base(name, fieldType)
		{
			this.attributes = (ushort)attributes;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00002654 File Offset: 0x00000854
		public override FieldDefinition Resolve()
		{
			return this;
		}

		// Token: 0x04000076 RID: 118
		private ushort attributes;

		// Token: 0x04000077 RID: 119
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000078 RID: 120
		private int offset = -2;

		// Token: 0x04000079 RID: 121
		internal int rva = -2;

		// Token: 0x0400007A RID: 122
		private byte[] initial_value;

		// Token: 0x0400007B RID: 123
		private object constant = Mixin.NotResolved;

		// Token: 0x0400007C RID: 124
		private MarshalInfo marshal_info;
	}
}

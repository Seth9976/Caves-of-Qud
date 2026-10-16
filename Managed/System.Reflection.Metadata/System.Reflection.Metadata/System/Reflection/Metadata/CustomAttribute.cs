using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Provides information about a custom attribute.</summary>
	// Token: 0x02000076 RID: 118
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct CustomAttribute
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x0000BFA6 File Offset: 0x0000A1A6
		internal CustomAttribute(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000BFB6 File Offset: 0x0000A1B6
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000BFC4 File Offset: 0x0000A1C4
		private CustomAttributeHandle Handle
		{
			get
			{
				return CustomAttributeHandle.FromRowId(this.RowId);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000BFD1 File Offset: 0x0000A1D1
		private MethodDefTreatment Treatment
		{
			get
			{
				return (MethodDefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		/// <summary>Gets the constructor (the <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />) of the custom attribute type.</summary>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000BFDD File Offset: 0x0000A1DD
		public EntityHandle Constructor
		{
			get
			{
				return this._reader.CustomAttributeTable.GetConstructor(this.Handle);
			}
		}

		/// <summary>Gets the handle of the metadata entity the attribute is applied to.</summary>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000BFF5 File Offset: 0x0000A1F5
		public EntityHandle Parent
		{
			get
			{
				return this._reader.CustomAttributeTable.GetParent(this.Handle);
			}
		}

		/// <summary>Gets the value of the attribute.</summary>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000C00D File Offset: 0x0000A20D
		public BlobHandle Value
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.CustomAttributeTable.GetValue(this.Handle);
				}
				return this.GetProjectedValue();
			}
		}

		/// <summary>Decodes the arguments encoded in the value blob.</summary>
		/// <param name="provider" />
		/// <typeparam name="TType" />
		// Token: 0x060004F8 RID: 1272 RVA: 0x0000C034 File Offset: 0x0000A234
		[return: Nullable(new byte[] { 0, 1 })]
		public CustomAttributeValue<TType> DecodeValue<[Nullable(2)] TType>(ICustomAttributeTypeProvider<TType> provider)
		{
			CustomAttributeDecoder<TType> customAttributeDecoder = new CustomAttributeDecoder<TType>(provider, this._reader);
			return customAttributeDecoder.DecodeValue(this.Constructor, this.Value);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000C064 File Offset: 0x0000A264
		private BlobHandle GetProjectedValue()
		{
			CustomAttributeValueTreatment customAttributeValueTreatment = this._reader.CalculateCustomAttributeValueTreatment(this.Handle);
			if (customAttributeValueTreatment == CustomAttributeValueTreatment.None)
			{
				return this._reader.CustomAttributeTable.GetValue(this.Handle);
			}
			return this.GetProjectedValue(customAttributeValueTreatment);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		private BlobHandle GetProjectedValue(CustomAttributeValueTreatment treatment)
		{
			BlobHandle.VirtualIndex virtualIndex;
			bool flag;
			switch (treatment)
			{
			case CustomAttributeValueTreatment.AttributeUsageAllowSingle:
				virtualIndex = BlobHandle.VirtualIndex.AttributeUsage_AllowSingle;
				flag = false;
				break;
			case CustomAttributeValueTreatment.AttributeUsageAllowMultiple:
				virtualIndex = BlobHandle.VirtualIndex.AttributeUsage_AllowMultiple;
				flag = false;
				break;
			case CustomAttributeValueTreatment.AttributeUsageVersionAttribute:
			case CustomAttributeValueTreatment.AttributeUsageDeprecatedAttribute:
				virtualIndex = BlobHandle.VirtualIndex.AttributeUsage_AllowMultiple;
				flag = true;
				break;
			default:
				return default(BlobHandle);
			}
			BlobHandle value = this._reader.CustomAttributeTable.GetValue(this.Handle);
			BlobReader blobReader = this._reader.GetBlobReader(value);
			if (blobReader.Length != 8)
			{
				return value;
			}
			if (blobReader.ReadInt16() != 1)
			{
				return value;
			}
			AttributeTargets attributeTargets = CustomAttribute.ProjectAttributeTargetValue(blobReader.ReadUInt32());
			if (flag)
			{
				attributeTargets |= 160;
			}
			return BlobHandle.FromVirtualIndex(virtualIndex, attributeTargets);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000C148 File Offset: 0x0000A348
		private static AttributeTargets ProjectAttributeTargetValue(uint rawValue)
		{
			if (rawValue == 4294967295U)
			{
				return 32767;
			}
			AttributeTargets attributeTargets = 0;
			if ((rawValue & 1U) != 0U)
			{
				attributeTargets |= 4096;
			}
			if ((rawValue & 2U) != 0U)
			{
				attributeTargets |= 16;
			}
			if ((rawValue & 4U) != 0U)
			{
				attributeTargets |= 512;
			}
			if ((rawValue & 8U) != 0U)
			{
				attributeTargets |= 256;
			}
			if ((rawValue & 16U) != 0U)
			{
				attributeTargets |= 1024;
			}
			if ((rawValue & 64U) != 0U)
			{
				attributeTargets |= 64;
			}
			if ((rawValue & 128U) != 0U)
			{
				attributeTargets |= 2048;
			}
			if ((rawValue & 256U) != 0U)
			{
				attributeTargets |= 128;
			}
			if ((rawValue & 512U) != 0U)
			{
				attributeTargets |= 4;
			}
			if ((rawValue & 1024U) != 0U)
			{
				attributeTargets |= 8;
			}
			return attributeTargets;
		}

		// Token: 0x04000327 RID: 807
		private readonly MetadataReader _reader;

		// Token: 0x04000328 RID: 808
		private readonly uint _treatmentAndRowId;
	}
}

using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AB RID: 171
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct MethodDefinition
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x00010F21 File Offset: 0x0000F121
		internal MethodDefinition(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00010F31 File Offset: 0x0000F131
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00010F3F File Offset: 0x0000F13F
		private MethodDefTreatment Treatment
		{
			get
			{
				return (MethodDefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00010F4B File Offset: 0x0000F14B
		private MethodDefinitionHandle Handle
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this.RowId);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00010F58 File Offset: 0x0000F158
		public StringHandle Name
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.MethodDefTable.GetName(this.Handle);
				}
				return this.GetProjectedName();
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00010F7F File Offset: 0x0000F17F
		public BlobHandle Signature
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.MethodDefTable.GetSignature(this.Handle);
				}
				return this.GetProjectedSignature();
			}
		}

		/// <param name="provider" />
		/// <param name="genericContext" />
		/// <typeparam name="TType" />
		/// <typeparam name="TGenericContext" />
		// Token: 0x060006A4 RID: 1700 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		[return: Nullable(new byte[] { 0, 1 })]
		public MethodSignature<TType> DecodeSignature<[Nullable(2)] TType, [Nullable(2)] TGenericContext>(ISignatureTypeProvider<TType, TGenericContext> provider, TGenericContext genericContext)
		{
			SignatureDecoder<TType, TGenericContext> signatureDecoder = new SignatureDecoder<TType, TGenericContext>(provider, this._reader, genericContext);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeMethodSignature(ref blobReader);
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00010FDF File Offset: 0x0000F1DF
		public int RelativeVirtualAddress
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.MethodDefTable.GetRva(this.Handle);
				}
				return this.GetProjectedRelativeVirtualAddress();
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00011006 File Offset: 0x0000F206
		public MethodAttributes Attributes
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.MethodDefTable.GetFlags(this.Handle);
				}
				return this.GetProjectedFlags();
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001102D File Offset: 0x0000F22D
		public MethodImplAttributes ImplAttributes
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.MethodDefTable.GetImplFlags(this.Handle);
				}
				return this.GetProjectedImplFlags();
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00011054 File Offset: 0x0000F254
		public TypeDefinitionHandle GetDeclaringType()
		{
			return this._reader.GetDeclaringType(this.Handle);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00011067 File Offset: 0x0000F267
		public ParameterHandleCollection GetParameters()
		{
			return new ParameterHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001107A File Offset: 0x0000F27A
		public GenericParameterHandleCollection GetGenericParameters()
		{
			return this._reader.GenericParamTable.FindGenericParametersForMethod(this.Handle);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00011094 File Offset: 0x0000F294
		public MethodImport GetImport()
		{
			int num = this._reader.ImplMapTable.FindImplForMethod(this.Handle);
			if (num == 0)
			{
				return default(MethodImport);
			}
			return this._reader.ImplMapTable.GetImport(num);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000110D6 File Offset: 0x0000F2D6
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000110EE File Offset: 0x0000F2EE
		public DeclarativeSecurityAttributeHandleCollection GetDeclarativeSecurityAttributes()
		{
			return new DeclarativeSecurityAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00011106 File Offset: 0x0000F306
		private StringHandle GetProjectedName()
		{
			if ((this.Treatment & MethodDefTreatment.KindMask) == MethodDefTreatment.DisposeMethod)
			{
				return StringHandle.FromVirtualIndex(StringHandle.VirtualIndex.Dispose);
			}
			return this._reader.MethodDefTable.GetName(this.Handle);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00011134 File Offset: 0x0000F334
		private MethodAttributes GetProjectedFlags()
		{
			MethodAttributes methodAttributes = this._reader.MethodDefTable.GetFlags(this.Handle);
			MethodDefTreatment treatment = this.Treatment;
			if ((treatment & MethodDefTreatment.KindMask) == MethodDefTreatment.HiddenInterfaceImplementation)
			{
				methodAttributes = (methodAttributes & -8) | 1;
			}
			if ((treatment & MethodDefTreatment.MarkAbstractFlag) != MethodDefTreatment.None)
			{
				methodAttributes |= 1024;
			}
			if ((treatment & MethodDefTreatment.MarkPublicFlag) != MethodDefTreatment.None)
			{
				methodAttributes = (methodAttributes & -8) | 6;
			}
			return methodAttributes | 128;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00011190 File Offset: 0x0000F390
		private MethodImplAttributes GetProjectedImplFlags()
		{
			MethodImplAttributes methodImplAttributes = this._reader.MethodDefTable.GetImplFlags(this.Handle);
			switch (this.Treatment & MethodDefTreatment.KindMask)
			{
			case MethodDefTreatment.Other:
			case MethodDefTreatment.AttributeMethod:
			case MethodDefTreatment.InterfaceMethod:
			case MethodDefTreatment.HiddenInterfaceImplementation:
			case MethodDefTreatment.DisposeMethod:
				methodImplAttributes |= 4099;
				break;
			case MethodDefTreatment.DelegateMethod:
				methodImplAttributes |= 3;
				break;
			}
			return methodImplAttributes;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000111F3 File Offset: 0x0000F3F3
		private BlobHandle GetProjectedSignature()
		{
			return this._reader.MethodDefTable.GetSignature(this.Handle);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001120B File Offset: 0x0000F40B
		private int GetProjectedRelativeVirtualAddress()
		{
			return 0;
		}

		// Token: 0x0400042D RID: 1069
		private readonly MetadataReader _reader;

		// Token: 0x0400042E RID: 1070
		private readonly uint _treatmentAndRowId;
	}
}

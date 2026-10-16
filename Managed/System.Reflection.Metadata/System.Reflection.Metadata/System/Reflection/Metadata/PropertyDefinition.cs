using System;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000D1 RID: 209
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct PropertyDefinition
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x00012740 File Offset: 0x00010940
		internal PropertyDefinition(MetadataReader reader, PropertyDefinitionHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00012756 File Offset: 0x00010956
		private PropertyDefinitionHandle Handle
		{
			get
			{
				return PropertyDefinitionHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00012763 File Offset: 0x00010963
		public StringHandle Name
		{
			get
			{
				return this._reader.PropertyTable.GetName(this.Handle);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001277B File Offset: 0x0001097B
		public PropertyAttributes Attributes
		{
			get
			{
				return this._reader.PropertyTable.GetFlags(this.Handle);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00012793 File Offset: 0x00010993
		public BlobHandle Signature
		{
			get
			{
				return this._reader.PropertyTable.GetSignature(this.Handle);
			}
		}

		/// <param name="provider" />
		/// <param name="genericContext" />
		/// <typeparam name="TType" />
		/// <typeparam name="TGenericContext" />
		// Token: 0x060007B8 RID: 1976 RVA: 0x000127AC File Offset: 0x000109AC
		[return: Nullable(new byte[] { 0, 1 })]
		public MethodSignature<TType> DecodeSignature<[Nullable(2)] TType, [Nullable(2)] TGenericContext>(ISignatureTypeProvider<TType, TGenericContext> provider, TGenericContext genericContext)
		{
			SignatureDecoder<TType, TGenericContext> signatureDecoder = new SignatureDecoder<TType, TGenericContext>(provider, this._reader, genericContext);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeMethodSignature(ref blobReader);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000127E3 File Offset: 0x000109E3
		public ConstantHandle GetDefaultValue()
		{
			return this._reader.ConstantTable.FindConstant(this.Handle);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00012800 File Offset: 0x00010A00
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00012818 File Offset: 0x00010A18
		public PropertyAccessors GetAccessors()
		{
			int num = 0;
			int num2 = 0;
			ImmutableArray<MethodDefinitionHandle>.Builder builder = null;
			ushort num4;
			int num3 = this._reader.MethodSemanticsTable.FindSemanticMethodsForProperty(this.Handle, out num4);
			for (ushort num5 = 0; num5 < num4; num5 += 1)
			{
				int num6 = num3 + (int)num5;
				switch (this._reader.MethodSemanticsTable.GetSemantics(num6))
				{
				case MethodSemanticsAttributes.Setter:
					num2 = this._reader.MethodSemanticsTable.GetMethod(num6).RowId;
					break;
				case MethodSemanticsAttributes.Getter:
					num = this._reader.MethodSemanticsTable.GetMethod(num6).RowId;
					break;
				case MethodSemanticsAttributes.Other:
					if (builder == null)
					{
						builder = ImmutableArray.CreateBuilder<MethodDefinitionHandle>();
					}
					builder.Add(this._reader.MethodSemanticsTable.GetMethod(num6));
					break;
				}
			}
			ImmutableArray<MethodDefinitionHandle> immutableArray = ((builder != null) ? builder.ToImmutable() : ImmutableArray<MethodDefinitionHandle>.Empty);
			return new PropertyAccessors(num, num2, immutableArray);
		}

		// Token: 0x04000493 RID: 1171
		private readonly MetadataReader _reader;

		// Token: 0x04000494 RID: 1172
		private readonly int _rowId;
	}
}

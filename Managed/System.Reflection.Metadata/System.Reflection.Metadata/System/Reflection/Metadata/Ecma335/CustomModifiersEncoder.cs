using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000101 RID: 257
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct CustomModifiersEncoder
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001B157 File Offset: 0x00019357
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x060009B1 RID: 2481 RVA: 0x0001B15F File Offset: 0x0001935F
		public CustomModifiersEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		/// <summary>Encodes a custom modifier.</summary>
		/// <param name="type">
		///   <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" /> or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />.</param>
		/// <param name="isOptional">Is optional modifier.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is <see langword="null" /> or of an unexpected kind.</exception>
		/// <returns>Encoder of subsequent modifiers.</returns>
		// Token: 0x060009B2 RID: 2482 RVA: 0x0001B168 File Offset: 0x00019368
		public CustomModifiersEncoder AddModifier(EntityHandle type, bool isOptional)
		{
			if (type.IsNil)
			{
				Throw.InvalidArgument_Handle("type");
			}
			if (isOptional)
			{
				this.Builder.WriteByte(32);
			}
			else
			{
				this.Builder.WriteByte(31);
			}
			this.Builder.WriteCompressedInteger(CodedIndex.TypeDefOrRefOrSpec(type));
			return this;
		}
	}
}

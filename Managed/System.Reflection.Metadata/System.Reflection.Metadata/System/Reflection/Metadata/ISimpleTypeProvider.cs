using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <typeparam name="TType" />
	// Token: 0x0200007C RID: 124
	[NullableContext(1)]
	public interface ISimpleTypeProvider<[Nullable(2)] TType>
	{
		/// <summary>Gets the type symbol for a primitive type.</summary>
		/// <param name="typeCode" />
		/// <returns>The type symbol for <paramref name="typeCode" />.</returns>
		// Token: 0x0600050B RID: 1291
		TType GetPrimitiveType(PrimitiveTypeCode typeCode);

		/// <summary>Gets the type symbol for a type definition.</summary>
		/// <param name="reader">The metadata reader that was passed to the signature decoder. It may be <see langword="null" />.</param>
		/// <param name="handle">The type definition handle.</param>
		/// <param name="rawTypeKind">The kind of the type, as specified in the signature. To interpret this value use <see cref="M:System.Reflection.Metadata.Ecma335.MetadataReaderExtensions.ResolveSignatureTypeKind(System.Reflection.Metadata.MetadataReader,System.Reflection.Metadata.EntityHandle,System.Byte)" />.</param>
		/// <returns>The type symbol.</returns>
		// Token: 0x0600050C RID: 1292
		TType GetTypeFromDefinition(MetadataReader reader, TypeDefinitionHandle handle, byte rawTypeKind);

		/// <summary>Gets the type symbol for a type reference.</summary>
		/// <param name="reader">The metadata reader that was passed to the signature decoder. It may be <see langword="null" />.</param>
		/// <param name="handle">The type definition handle.</param>
		/// <param name="rawTypeKind">The kind of the type as specified in the signature. To interpret this value, use <see cref="M:System.Reflection.Metadata.Ecma335.MetadataReaderExtensions.ResolveSignatureTypeKind(System.Reflection.Metadata.MetadataReader,System.Reflection.Metadata.EntityHandle,System.Byte)" />.</param>
		/// <returns>The type symbol.</returns>
		// Token: 0x0600050D RID: 1293
		TType GetTypeFromReference(MetadataReader reader, TypeReferenceHandle handle, byte rawTypeKind);
	}
}

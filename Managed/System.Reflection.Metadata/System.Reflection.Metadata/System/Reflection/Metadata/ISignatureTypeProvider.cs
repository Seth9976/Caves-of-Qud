using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <typeparam name="TType" />
	/// <typeparam name="TGenericContext" />
	// Token: 0x0200007B RID: 123
	[NullableContext(1)]
	public interface ISignatureTypeProvider<[Nullable(2)] TType, [Nullable(2)] TGenericContext> : ISimpleTypeProvider<TType>, IConstructedTypeProvider<TType>, ISZArrayTypeProvider<TType>
	{
		/// <summary>Gets the type symbol for the function pointer type of the given method <paramref name="signature" />.</summary>
		/// <param name="signature" />
		/// <returns>The type symbol for the function pointer type.</returns>
		// Token: 0x06000505 RID: 1285
		TType GetFunctionPointerType([Nullable(new byte[] { 0, 1 })] MethodSignature<TType> signature);

		/// <summary>Gets the type symbol for the generic method parameter at the given zero-based <paramref name="index" />.</summary>
		/// <param name="genericContext" />
		/// <param name="index" />
		/// <returns>The type symbol for the generic method parameter at <paramref name="index" />.</returns>
		// Token: 0x06000506 RID: 1286
		TType GetGenericMethodParameter(TGenericContext genericContext, int index);

		/// <summary>Gets the type symbol for the generic type parameter at the given zero-based <paramref name="index" />.</summary>
		/// <param name="genericContext" />
		/// <param name="index" />
		/// <returns>The type symbol for the generic type parameter at the given zero-based <paramref name="index" />.</returns>
		// Token: 0x06000507 RID: 1287
		TType GetGenericTypeParameter(TGenericContext genericContext, int index);

		/// <summary>Gets the type symbol for a type with a custom modifier applied.</summary>
		/// <param name="modifier">The modifier type applied.</param>
		/// <param name="unmodifiedType">The type symbol of the underlying type without modifiers applied.</param>
		/// <param name="isRequired">
		///   <see langword="true" /> if the modifier is required, <see langword="false" /> if it's optional.</param>
		/// <returns>The type symbol.</returns>
		// Token: 0x06000508 RID: 1288
		TType GetModifiedType(TType modifier, TType unmodifiedType, bool isRequired);

		/// <summary>Gets the type symbol for a local variable type that is marked as pinned.</summary>
		/// <param name="elementType" />
		/// <returns>The type symbol for the local variable type.</returns>
		// Token: 0x06000509 RID: 1289
		TType GetPinnedType(TType elementType);

		/// <summary>Gets the type symbol for a type specification.</summary>
		/// <param name="reader">The metadata reader that was passed to the signature decoder. It may be <see langword="null" />.</param>
		/// <param name="genericContext">The context that was passed to the signature decoder.</param>
		/// <param name="handle">The type specification handle.</param>
		/// <param name="rawTypeKind">The kind of the type, as specified in the signature. To interpret this value, use <see cref="M:System.Reflection.Metadata.Ecma335.MetadataReaderExtensions.ResolveSignatureTypeKind(System.Reflection.Metadata.MetadataReader,System.Reflection.Metadata.EntityHandle,System.Byte)" />.</param>
		/// <returns>The type symbol for the type specification.</returns>
		// Token: 0x0600050A RID: 1290
		TType GetTypeFromSpecification(MetadataReader reader, TGenericContext genericContext, TypeSpecificationHandle handle, byte rawTypeKind);
	}
}

using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a custom attribute of the type specified by <paramref name="TType" />.</summary>
	/// <typeparam name="TType">The attribute type.</typeparam>
	// Token: 0x02000047 RID: 71
	public readonly struct CustomAttributeValue<[Nullable(2)] TType>
	{
		/// <summary>Gets the fixed arguments for the custom attribute.</summary>
		/// <returns>An immutable array of arguments.</returns>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00008D2D File Offset: 0x00006F2D
		[Nullable(new byte[] { 0, 0, 1 })]
		public ImmutableArray<CustomAttributeTypedArgument<TType>> FixedArguments
		{
			[return: Nullable(new byte[] { 0, 0, 1 })]
			get;
		}

		/// <summary>Gets the named arguments for the custom attribute value.</summary>
		/// <returns>An immutable array of arguments.</returns>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00008D35 File Offset: 0x00006F35
		[Nullable(new byte[] { 0, 0, 1 })]
		public ImmutableArray<CustomAttributeNamedArgument<TType>> NamedArguments
		{
			[return: Nullable(new byte[] { 0, 0, 1 })]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.CustomAttributeValue`1" /> structure using the specified fixed arguments and named arguments.</summary>
		/// <param name="fixedArguments">The fixed arguments.</param>
		/// <param name="namedArguments">The named arguments.</param>
		// Token: 0x060002D6 RID: 726 RVA: 0x00008D3D File Offset: 0x00006F3D
		public CustomAttributeValue([Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<CustomAttributeTypedArgument<TType>> fixedArguments, [Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<CustomAttributeNamedArgument<TType>> namedArguments)
		{
			this.FixedArguments = fixedArguments;
			this.NamedArguments = namedArguments;
		}
	}
}

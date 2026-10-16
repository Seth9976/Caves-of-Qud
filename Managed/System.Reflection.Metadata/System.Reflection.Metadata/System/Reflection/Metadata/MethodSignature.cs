using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a method (definition, reference, or standalone) or property signature. In the case of properties, the signature matches that of a getter with a distinguishing <see cref="T:System.Reflection.Metadata.SignatureHeader" />.</summary>
	/// <typeparam name="TType">The method type.</typeparam>
	// Token: 0x0200007F RID: 127
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct MethodSignature<[Nullable(2)] TType>
	{
		/// <summary>Gets the information in the leading byte of the signature (kind, calling convention, flags).</summary>
		/// <returns>The header signature.</returns>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000C26F File Offset: 0x0000A46F
		public SignatureHeader Header { get; }

		/// <summary>Gets the return type of the method.</summary>
		/// <returns>The return type.</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000C277 File Offset: 0x0000A477
		public TType ReturnType { get; }

		/// <summary>Gets the number of parameters that are required for the method.</summary>
		/// <returns>The number of required parameters.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000C27F File Offset: 0x0000A47F
		public int RequiredParameterCount { get; }

		/// <summary>Gets the number of generic type parameters for the method.</summary>
		/// <returns>The number of generic type parameters, or 0 for non-generic methods.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000C287 File Offset: 0x0000A487
		public int GenericParameterCount { get; }

		/// <summary>Gets the method's parameter types.</summary>
		/// <returns>An immutable collection of parameter types.</returns>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000C28F File Offset: 0x0000A48F
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<TType> ParameterTypes
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.MethodSignature`1" /> structure using the specified header, return type, and parameter information.</summary>
		/// <param name="header">The information in the leading byte of the signature (kind, calling convention, flags).</param>
		/// <param name="returnType">The return type of the method.</param>
		/// <param name="requiredParameterCount">The number of required parameters.</param>
		/// <param name="genericParameterCount">The number of generic type parameters.</param>
		/// <param name="parameterTypes">The parameter types.</param>
		// Token: 0x06000517 RID: 1303 RVA: 0x0000C297 File Offset: 0x0000A497
		public MethodSignature(SignatureHeader header, TType returnType, int requiredParameterCount, int genericParameterCount, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TType> parameterTypes)
		{
			this.Header = header;
			this.ReturnType = returnType;
			this.GenericParameterCount = genericParameterCount;
			this.RequiredParameterCount = requiredParameterCount;
			this.ParameterTypes = parameterTypes;
		}
	}
}

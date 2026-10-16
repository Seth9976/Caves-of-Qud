using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a typed argument for a custom metadata attribute.</summary>
	/// <typeparam name="TType">The type of the argument.</typeparam>
	// Token: 0x02000046 RID: 70
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct CustomAttributeTypedArgument<[Nullable(2)] TType>
	{
		/// <summary>Gets the type of the argument.</summary>
		/// <returns>The argument type.</returns>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00008D0D File Offset: 0x00006F0D
		public TType Type { get; }

		/// <summary>Gets the value of the argument.</summary>
		/// <returns>The argument value.</returns>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00008D15 File Offset: 0x00006F15
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.CustomAttributeTypedArgument`1" /> structure using the specified argument type and value.</summary>
		/// <param name="type">The type of the argument.</param>
		/// <param name="value">The argument value.</param>
		// Token: 0x060002D3 RID: 723 RVA: 0x00008D1D File Offset: 0x00006F1D
		public CustomAttributeTypedArgument(TType type, [Nullable(2)] object value)
		{
			this.Type = type;
			this.Value = value;
		}
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a named argument decoded from a custom attribute signature.</summary>
	/// <typeparam name="TType">The type used to represent types of values decoded from the custom attribute signature.</typeparam>
	// Token: 0x02000045 RID: 69
	[NullableContext(2)]
	[Nullable(0)]
	public readonly struct CustomAttributeNamedArgument<TType>
	{
		/// <summary>Gets the name of the argument.</summary>
		/// <returns>The argument name.</returns>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00008CCE File Offset: 0x00006ECE
		public string Name { get; }

		/// <summary>Gets the kind of argument.</summary>
		/// <returns>The argument kind.</returns>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00008CD6 File Offset: 0x00006ED6
		public CustomAttributeNamedArgumentKind Kind { get; }

		/// <summary>Gets the type of the argument.</summary>
		/// <returns>The argument type.</returns>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00008CDE File Offset: 0x00006EDE
		[Nullable(1)]
		public TType Type
		{
			[NullableContext(1)]
			get;
		}

		/// <summary>Gets the value of the argument.</summary>
		/// <returns>An object containing the argument value.</returns>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00008CE6 File Offset: 0x00006EE6
		public object Value { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.CustomAttributeNamedArgument`1" /> structure using the specified name, kind, type, and value.</summary>
		/// <param name="name">The name of the argument.</param>
		/// <param name="kind">The kind of the argument.</param>
		/// <param name="type">The type of the argument.</param>
		/// <param name="value">The value of the argument.</param>
		// Token: 0x060002D0 RID: 720 RVA: 0x00008CEE File Offset: 0x00006EEE
		public CustomAttributeNamedArgument(string name, CustomAttributeNamedArgumentKind kind, [Nullable(1)] TType type, object value)
		{
			this.Name = name;
			this.Kind = kind;
			this.Type = type;
			this.Value = value;
		}
	}
}

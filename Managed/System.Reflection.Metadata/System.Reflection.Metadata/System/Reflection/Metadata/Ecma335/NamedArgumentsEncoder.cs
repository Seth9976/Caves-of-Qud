using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FC RID: 252
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct NamedArgumentsEncoder
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0001AB8F File Offset: 0x00018D8F
		public BlobBuilder Builder { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.Ecma335.NamedArgumentsEncoder" /> structure.</summary>
		/// <param name="builder">A builder for encoding the named argument.</param>
		// Token: 0x06000971 RID: 2417 RVA: 0x0001AB97 File Offset: 0x00018D97
		public NamedArgumentsEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		/// <summary>Encodes a named argument (a field or property) and returns three encoders that must be used in the order they appear in the parameter list.</summary>
		/// <param name="isField">
		///   <see langword="true" /> to encode a field, <see langword="false" /> to encode a property.</param>
		/// <param name="type">The method to call first to encode the type of the argument.</param>
		/// <param name="name">The method to call second to encode the name of the field or property.</param>
		/// <param name="literal">The method to call third to encode the literal value of the argument.</param>
		// Token: 0x06000972 RID: 2418 RVA: 0x0001ABA0 File Offset: 0x00018DA0
		public void AddArgument(bool isField, out NamedArgumentTypeEncoder type, out NameEncoder name, out LiteralEncoder literal)
		{
			this.Builder.WriteByte(isField ? 83 : 84);
			type = new NamedArgumentTypeEncoder(this.Builder);
			name = new NameEncoder(this.Builder);
			literal = new LiteralEncoder(this.Builder);
		}

		/// <summary>Encodes a named argument (a field or property).</summary>
		/// <param name="isField">
		///   <see langword="true" /> to encode a field, <see langword="false" /> to encode a property.</param>
		/// <param name="type">The method to call first to encode the type of the argument.</param>
		/// <param name="name">The method to call second to encode the name of the field or property.</param>
		/// <param name="literal">The method to call third to encode the literal value of the argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" />, <paramref name="name" /> or <paramref name="literal" /> is <see langword="null" />.</exception>
		// Token: 0x06000973 RID: 2419 RVA: 0x0001ABF8 File Offset: 0x00018DF8
		public void AddArgument(bool isField, Action<NamedArgumentTypeEncoder> type, Action<NameEncoder> name, Action<LiteralEncoder> literal)
		{
			if (type == null)
			{
				Throw.ArgumentNull("type");
			}
			if (name == null)
			{
				Throw.ArgumentNull("name");
			}
			if (literal == null)
			{
				Throw.ArgumentNull("literal");
			}
			NamedArgumentTypeEncoder namedArgumentTypeEncoder;
			NameEncoder nameEncoder;
			LiteralEncoder literalEncoder;
			this.AddArgument(isField, out namedArgumentTypeEncoder, out nameEncoder, out literalEncoder);
			type.Invoke(namedArgumentTypeEncoder);
			name.Invoke(nameEncoder);
			literal.Invoke(literalEncoder);
		}
	}
}

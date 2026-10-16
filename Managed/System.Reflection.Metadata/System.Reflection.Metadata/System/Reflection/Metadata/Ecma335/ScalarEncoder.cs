using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F7 RID: 247
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ScalarEncoder
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0001AA43 File Offset: 0x00018C43
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x0600095F RID: 2399 RVA: 0x0001AA4B File Offset: 0x00018C4B
		public ScalarEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		/// <summary>Encodes a <see langword="null" /> literal of type <see cref="T:System.Array" />.</summary>
		// Token: 0x06000960 RID: 2400 RVA: 0x0001AA54 File Offset: 0x00018C54
		public void NullArray()
		{
			this.Builder.WriteInt32(-1);
		}

		/// <summary>Encodes a constant literal.</summary>
		/// <param name="value">A constant of type <see cref="T:System.Boolean" />, <see cref="T:System.Byte" />, <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.UInt16" />, <see cref="T:System.Int32" />, <see cref="T:System.UInt32" />, <see cref="T:System.Int64" />, <see cref="T:System.UInt64" />, <see cref="T:System.Single" />, <see cref="T:System.Double" />, <see cref="T:System.Char" /> (encoded as a two-byte Unicode character), <see cref="T:System.String" /> (encoded as SerString), or <see cref="T:System.Enum" /> (encoded as the underlying integer value).</param>
		/// <exception cref="T:System.ArgumentException">Unexpected constant type.</exception>
		// Token: 0x06000961 RID: 2401 RVA: 0x0001AA64 File Offset: 0x00018C64
		[NullableContext(2)]
		public void Constant(object value)
		{
			string text = value as string;
			if (text != null || value == null)
			{
				this.String(text);
				return;
			}
			this.Builder.WriteConstant(value);
		}

		/// <summary>Encodes a literal of type <see cref="T:System.Type" /> (which can possibly be <see langword="null" />).</summary>
		/// <param name="serializedTypeName">The name of the type, or <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="serializedTypeName" /> is empty.</exception>
		// Token: 0x06000962 RID: 2402 RVA: 0x0001AA92 File Offset: 0x00018C92
		[NullableContext(2)]
		public void SystemType(string serializedTypeName)
		{
			if (serializedTypeName != null && serializedTypeName.Length == 0)
			{
				Throw.ArgumentEmptyString("serializedTypeName");
			}
			this.String(serializedTypeName);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		private void String(string value)
		{
			this.Builder.WriteSerializedString(value);
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection.Metadata
{
	/// <summary>Provides the <see cref="T:System.Reflection.Metadata.MetadataReader" /> with a custom mechanism for decoding byte sequences in metadata that represent text.</summary>
	// Token: 0x020000A9 RID: 169
	[NullableContext(1)]
	[Nullable(0)]
	public class MetadataStringDecoder
	{
		/// <summary>Gets the encoding used by this instance.</summary>
		/// <returns>The encoding used by this instance.</returns>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00010BA1 File Offset: 0x0000EDA1
		public Encoding Encoding { get; }

		/// <summary>Gets the default decoder used by <see cref="T:System.Reflection.Metadata.MetadataReader" /> to decode UTF-8 when no decoder is provided to the constructor.</summary>
		/// <returns>The default decoder used by <see cref="T:System.Reflection.Metadata.MetadataReader" /> to decode UTF-8.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00010BA9 File Offset: 0x0000EDA9
		public static MetadataStringDecoder DefaultUTF8 { get; } = new MetadataStringDecoder(Encoding.UTF8);

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.MetadataStringDecoder" /> class using the given encoding.</summary>
		/// <param name="encoding">The encoding to use.</param>
		// Token: 0x0600068F RID: 1679 RVA: 0x00010BB0 File Offset: 0x0000EDB0
		public MetadataStringDecoder(Encoding encoding)
		{
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this.Encoding = encoding;
		}

		/// <summary>Obtains strings for byte sequences in metadata. Override this to cache strings if required. Otherwise, it is implemented by forwarding straight to <see cref="P:System.Reflection.Metadata.MetadataStringDecoder.Encoding" /> and every call will allocate a new string.</summary>
		/// <param name="bytes">Pointer to bytes to decode.</param>
		/// <param name="byteCount">Number of bytes to decode.</param>
		/// <returns>The decoded string.</returns>
		// Token: 0x06000690 RID: 1680 RVA: 0x00010BCD File Offset: 0x0000EDCD
		[NullableContext(0)]
		[return: Nullable(1)]
		public unsafe virtual string GetString(byte* bytes, int byteCount)
		{
			return this.Encoding.GetString(bytes, byteCount);
		}
	}
}

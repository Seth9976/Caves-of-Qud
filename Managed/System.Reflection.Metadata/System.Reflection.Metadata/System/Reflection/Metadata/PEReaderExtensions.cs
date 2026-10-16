using System;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000D0 RID: 208
	[NullableContext(1)]
	[Nullable(0)]
	[EditorBrowsable(1)]
	public static class PEReaderExtensions
	{
		/// <summary>Returns a body block of a method with the specified Relative Virtual Address (RVA).</summary>
		/// <param name="peReader">The current <see cref="T:System.Reflection.PortableExecutable.PEReader" /> instance.</param>
		/// <param name="relativeVirtualAddress">The Relative Virtual Address (RVA).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peReader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">The body is not found in the metadata or is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The section where the method is stored is not available.</exception>
		/// <exception cref="T:System.IO.IOException">IO error occurred while reading from the underlying stream.</exception>
		/// <returns>A method block body instance.</returns>
		// Token: 0x060007AF RID: 1967 RVA: 0x000126A4 File Offset: 0x000108A4
		public static MethodBodyBlock GetMethodBody(this PEReader peReader, int relativeVirtualAddress)
		{
			if (peReader == null)
			{
				throw new ArgumentNullException("peReader");
			}
			PEMemoryBlock sectionData = peReader.GetSectionData(relativeVirtualAddress);
			if (sectionData.Length == 0)
			{
				throw new BadImageFormatException(SR.Format(SR.InvalidMethodRva, relativeVirtualAddress));
			}
			return MethodBodyBlock.Create(sectionData.GetReader());
		}

		/// <summary>Gets a <see cref="T:System.Reflection.Metadata.MetadataReader" /> from a <see cref="T:System.Reflection.PortableExecutable.PEReader" />.</summary>
		/// <param name="peReader">The current <see cref="T:System.Reflection.PortableExecutable.PEReader" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peReader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is big-endian.</exception>
		/// <exception cref="T:System.IO.IOException">IO error while reading from the underlying stream.</exception>
		/// <returns>A metadata reader.</returns>
		// Token: 0x060007B0 RID: 1968 RVA: 0x000126F2 File Offset: 0x000108F2
		public static MetadataReader GetMetadataReader(this PEReader peReader)
		{
			return peReader.GetMetadataReader(MetadataReaderOptions.Default, null);
		}

		/// <summary>Gets a metadata reader with the specified metadata reading configuration from a <see cref="T:System.Reflection.PortableExecutable.PEReader" />.</summary>
		/// <param name="peReader">The current <see cref="T:System.Reflection.PortableExecutable.PEReader" /> instance.</param>
		/// <param name="options">An enumeration value indicating the metadata reading configuration.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peReader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is big-endian.</exception>
		/// <exception cref="T:System.IO.IOException">IO error while reading from the underlying stream.</exception>
		/// <returns>A metadata reader with the specified metadata reading configuration.</returns>
		// Token: 0x060007B1 RID: 1969 RVA: 0x000126FC File Offset: 0x000108FC
		public static MetadataReader GetMetadataReader(this PEReader peReader, MetadataReaderOptions options)
		{
			return peReader.GetMetadataReader(options, null);
		}

		/// <summary>Gets a metadata reader with the specified metadata reading configuration and encoding configuration from a <see cref="T:System.Reflection.PortableExecutable.PEReader" />.</summary>
		/// <param name="peReader">The current <see cref="T:System.Reflection.PortableExecutable.PEReader" /> instance.</param>
		/// <param name="options">An enumeration value indicating the metadata reading configuration.</param>
		/// <param name="utf8Decoder">A metadata string decoder with the encoding configuration.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peReader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The encoding of <paramref name="utf8Decoder" /> is not <see cref="T:System.Text.UTF8Encoding" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is big-endian.</exception>
		/// <exception cref="T:System.IO.IOException">IO error while reading from the underlying stream.</exception>
		/// <returns>&gt;A metadata reader with the specified metadata reading configuration and encoding configuration.</returns>
		// Token: 0x060007B2 RID: 1970 RVA: 0x00012708 File Offset: 0x00010908
		public static MetadataReader GetMetadataReader(this PEReader peReader, MetadataReaderOptions options, [Nullable(2)] MetadataStringDecoder utf8Decoder)
		{
			if (peReader == null)
			{
				throw new ArgumentNullException("peReader");
			}
			PEMemoryBlock metadata = peReader.GetMetadata();
			return new MetadataReader(metadata.Pointer, metadata.Length, options, utf8Decoder, peReader);
		}
	}
}

using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Reflection.Metadata
{
	/// <summary>Provides a <see cref="T:System.Reflection.Metadata.MetadataReader" /> for metadata stored in an array of bytes, a memory block, or a stream.</summary>
	// Token: 0x0200004E RID: 78
	public sealed class MetadataReaderProvider : IDisposable
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x00008F97 File Offset: 0x00007197
		private MetadataReaderProvider(AbstractMemoryBlock metadataBlock)
		{
			this._lazyMetadataBlock = metadataBlock;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00008FB1 File Offset: 0x000071B1
		private MetadataReaderProvider(MemoryBlockProvider blockProvider)
		{
			this._blockProviderOpt = blockProvider;
		}

		/// <summary>Creates a portable PDB metadata provider over a blob stored in memory.</summary>
		/// <param name="start">Pointer to the start of the portable PDB blob.</param>
		/// <param name="size">The size of the portable PDB blob.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is <see cref="F:System.IntPtr.Zero" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is negative.</exception>
		/// <returns>The new portable PDB metadata provider.</returns>
		// Token: 0x060002E6 RID: 742 RVA: 0x00008FCB File Offset: 0x000071CB
		[return: Nullable(1)]
		public unsafe static MetadataReaderProvider FromPortablePdbImage(byte* start, int size)
		{
			return MetadataReaderProvider.FromMetadataImage(start, size);
		}

		/// <summary>Creates a metadata provider over an image stored in memory.</summary>
		/// <param name="start">Pointer to the start of the metadata blob.</param>
		/// <param name="size">The size of the metadata blob.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is <see cref="F:System.IntPtr.Zero" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is negative.</exception>
		/// <returns>The new metadata provider.</returns>
		// Token: 0x060002E7 RID: 743 RVA: 0x00008FD4 File Offset: 0x000071D4
		[return: Nullable(1)]
		public unsafe static MetadataReaderProvider FromMetadataImage(byte* start, int size)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return new MetadataReaderProvider(new ExternalMemoryBlockProvider(start, size));
		}

		/// <summary>Creates a portable PDB metadata provider over a byte array.</summary>
		/// <param name="image">A portable PDB image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		/// <returns>The new portable PDB metadata provider .</returns>
		// Token: 0x060002E8 RID: 744 RVA: 0x00009001 File Offset: 0x00007201
		[return: Nullable(1)]
		public static MetadataReaderProvider FromPortablePdbImage(ImmutableArray<byte> image)
		{
			return MetadataReaderProvider.FromMetadataImage(image);
		}

		/// <summary>Creates a provider over a byte array.</summary>
		/// <param name="image">Metadata image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		/// <returns>The new provider.</returns>
		// Token: 0x060002E9 RID: 745 RVA: 0x00009009 File Offset: 0x00007209
		[return: Nullable(1)]
		public static MetadataReaderProvider FromMetadataImage(ImmutableArray<byte> image)
		{
			if (image.IsDefault)
			{
				throw new ArgumentNullException("image");
			}
			return new MetadataReaderProvider(new ByteArrayMemoryProvider(image));
		}

		/// <summary>Creates a provider for a stream of the specified size beginning at its current position.</summary>
		/// <param name="stream">The stream.</param>
		/// <param name="options">Options specifying how sections of the image are read from the stream.</param>
		/// <param name="size">Size of the metadata blob in the stream. If not specified, the metadata blob is assumed to span to the end of the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> doesn't support read and seek operations.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Size is negative or extends past the end of the stream.</exception>
		/// <returns>A <see cref="T:System.Reflection.Metadata.MetadataReaderProvider" /> instance.</returns>
		// Token: 0x060002EA RID: 746 RVA: 0x0000902A File Offset: 0x0000722A
		[NullableContext(1)]
		public static MetadataReaderProvider FromPortablePdbStream(Stream stream, MetadataStreamOptions options = MetadataStreamOptions.Default, int size = 0)
		{
			return MetadataReaderProvider.FromMetadataStream(stream, options, size);
		}

		/// <summary>Creates a provider for a stream of the specified size beginning at its current position.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> instance.</param>
		/// <param name="options">Options specifying how sections of the image are read from the stream.</param>
		/// <param name="size">Size of the metadata blob in the stream. If not specified, the metadata blob is assumed to span to the end of the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> doesn't support read and seek operations.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Size is negative or extends past the end of the stream.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading from the stream (only when <see cref="F:System.Reflection.Metadata.MetadataStreamOptions.PrefetchMetadata" /> is specified).</exception>
		/// <returns>The new provider.</returns>
		// Token: 0x060002EB RID: 747 RVA: 0x00009034 File Offset: 0x00007234
		[NullableContext(1)]
		public static MetadataReaderProvider FromMetadataStream(Stream stream, MetadataStreamOptions options = MetadataStreamOptions.Default, int size = 0)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead || !stream.CanSeek)
			{
				throw new ArgumentException(SR.StreamMustSupportReadAndSeek, "stream");
			}
			if (!options.IsValid())
			{
				throw new ArgumentOutOfRangeException("options");
			}
			long position = stream.Position;
			int andValidateSize = StreamExtensions.GetAndValidateSize(stream, size, "stream");
			bool flag = true;
			MetadataReaderProvider metadataReaderProvider;
			try
			{
				bool flag2 = FileStreamReadLightUp.IsFileStream(stream);
				if ((options & MetadataStreamOptions.PrefetchMetadata) == MetadataStreamOptions.Default)
				{
					metadataReaderProvider = new MetadataReaderProvider(new StreamMemoryBlockProvider(stream, position, andValidateSize, flag2, (options & MetadataStreamOptions.LeaveOpen) > MetadataStreamOptions.Default));
					flag = false;
				}
				else
				{
					metadataReaderProvider = new MetadataReaderProvider(StreamMemoryBlockProvider.ReadMemoryBlockNoLock(stream, flag2, position, andValidateSize));
				}
			}
			finally
			{
				if (flag && (options & MetadataStreamOptions.LeaveOpen) == MetadataStreamOptions.Default)
				{
					stream.Dispose();
				}
			}
			return metadataReaderProvider;
		}

		/// <summary>Disposes all memory allocated by the reader.</summary>
		// Token: 0x060002EC RID: 748 RVA: 0x000090F4 File Offset: 0x000072F4
		public void Dispose()
		{
			MemoryBlockProvider blockProviderOpt = this._blockProviderOpt;
			if (blockProviderOpt != null)
			{
				blockProviderOpt.Dispose();
			}
			this._blockProviderOpt = null;
			AbstractMemoryBlock lazyMetadataBlock = this._lazyMetadataBlock;
			if (lazyMetadataBlock != null)
			{
				lazyMetadataBlock.Dispose();
			}
			this._lazyMetadataBlock = null;
			this._lazyMetadataReader = null;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.Metadata.MetadataReader" /> from a <see cref="T:System.Reflection.Metadata.MetadataReaderProvider" />.</summary>
		/// <param name="options">A bitwise combination of the enumeration values that represent the configuration when reading the metadata.</param>
		/// <param name="utf8Decoder">The encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">The encoding of <paramref name="utf8Decoder" /> is not <see cref="T:System.Text.UTF8Encoding" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is big-endian.</exception>
		/// <exception cref="T:System.IO.IOException">IO error while reading from the underlying stream.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Provider has been disposed.</exception>
		/// <returns>A <see cref="T:System.Reflection.Metadata.MetadataReader" /> instance.</returns>
		// Token: 0x060002ED RID: 749 RVA: 0x00009130 File Offset: 0x00007330
		[NullableContext(1)]
		public MetadataReader GetMetadataReader(MetadataReaderOptions options = MetadataReaderOptions.Default, [Nullable(2)] MetadataStringDecoder utf8Decoder = null)
		{
			MetadataReader metadataReader = this._lazyMetadataReader;
			if (MetadataReaderProvider.CanReuseReader(metadataReader, options, utf8Decoder))
			{
				return metadataReader;
			}
			object metadataReaderGuard = this._metadataReaderGuard;
			MetadataReader metadataReader2;
			lock (metadataReaderGuard)
			{
				metadataReader = this._lazyMetadataReader;
				if (MetadataReaderProvider.CanReuseReader(metadataReader, options, utf8Decoder))
				{
					metadataReader2 = metadataReader;
				}
				else
				{
					AbstractMemoryBlock metadataBlock = this.GetMetadataBlock();
					MetadataReader metadataReader3 = new MetadataReader(metadataBlock.Pointer, metadataBlock.Size, options, utf8Decoder, this);
					this._lazyMetadataReader = metadataReader3;
					metadataReader2 = metadataReader3;
				}
			}
			return metadataReader2;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000091C0 File Offset: 0x000073C0
		private static bool CanReuseReader(MetadataReader reader, MetadataReaderOptions options, MetadataStringDecoder utf8DecoderOpt)
		{
			return reader != null && reader.Options == options && reader.UTF8Decoder == (utf8DecoderOpt ?? MetadataStringDecoder.DefaultUTF8);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000091E4 File Offset: 0x000073E4
		[NullableContext(1)]
		internal AbstractMemoryBlock GetMetadataBlock()
		{
			if (this._lazyMetadataBlock == null)
			{
				if (this._blockProviderOpt == null)
				{
					throw new ObjectDisposedException("MetadataReaderProvider");
				}
				AbstractMemoryBlock memoryBlock = this._blockProviderOpt.GetMemoryBlock(0, this._blockProviderOpt.Size);
				if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyMetadataBlock, memoryBlock, null) != null)
				{
					memoryBlock.Dispose();
				}
			}
			return this._lazyMetadataBlock;
		}

		// Token: 0x040002A3 RID: 675
		private MemoryBlockProvider _blockProviderOpt;

		// Token: 0x040002A4 RID: 676
		private AbstractMemoryBlock _lazyMetadataBlock;

		// Token: 0x040002A5 RID: 677
		private MetadataReader _lazyMetadataReader;

		// Token: 0x040002A6 RID: 678
		private readonly object _metadataReaderGuard = new object();
	}
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x0200017B RID: 379
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class StreamMemoryBlockProvider : MemoryBlockProvider
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x00022EB8 File Offset: 0x000210B8
		public StreamMemoryBlockProvider(Stream stream, long imageStart, int imageSize, bool isFileStream, bool leaveOpen)
		{
			this._stream = stream;
			this._streamGuard = new object();
			this._imageStart = imageStart;
			this._imageSize = imageSize;
			this._leaveOpen = leaveOpen;
			this._isFileStream = isFileStream;
			this._useMemoryMap = isFileStream && MemoryMapLightUp.IsAvailable;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00022F0D File Offset: 0x0002110D
		protected override void Dispose(bool disposing)
		{
			if (!this._leaveOpen)
			{
				Stream stream = Interlocked.Exchange<Stream>(ref this._stream, null);
				if (stream != null)
				{
					stream.Dispose();
				}
			}
			IDisposable disposable = Interlocked.Exchange<IDisposable>(ref this._lazyMemoryMap, null);
			if (disposable == null)
			{
				return;
			}
			disposable.Dispose();
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00022F44 File Offset: 0x00021144
		public override int Size
		{
			get
			{
				return this._imageSize;
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00022F4C File Offset: 0x0002114C
		internal static NativeHeapMemoryBlock ReadMemoryBlockNoLock(Stream stream, bool isFileStream, long start, int size)
		{
			NativeHeapMemoryBlock nativeHeapMemoryBlock = new NativeHeapMemoryBlock(size);
			bool flag = true;
			try
			{
				stream.Seek(start, 0);
				if (!isFileStream || !FileStreamReadLightUp.TryReadFile(stream, nativeHeapMemoryBlock.Pointer, start, size))
				{
					stream.CopyTo(nativeHeapMemoryBlock.Pointer, size);
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					nativeHeapMemoryBlock.Dispose();
				}
			}
			return nativeHeapMemoryBlock;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00022FAC File Offset: 0x000211AC
		protected override AbstractMemoryBlock GetMemoryBlockImpl(int start, int size)
		{
			long num = this._imageStart + (long)start;
			if (this._useMemoryMap && size > 16384)
			{
				MemoryMappedFileBlock memoryMappedFileBlock;
				if (this.TryCreateMemoryMappedFileBlock(num, size, out memoryMappedFileBlock))
				{
					return memoryMappedFileBlock;
				}
				this._useMemoryMap = false;
			}
			object streamGuard = this._streamGuard;
			AbstractMemoryBlock abstractMemoryBlock;
			lock (streamGuard)
			{
				abstractMemoryBlock = StreamMemoryBlockProvider.ReadMemoryBlockNoLock(this._stream, this._isFileStream, num, size);
			}
			return abstractMemoryBlock;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002302C File Offset: 0x0002122C
		public override Stream GetStream(out StreamConstraints constraints)
		{
			constraints = new StreamConstraints(this._streamGuard, this._imageStart, this._imageSize);
			return this._stream;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00023054 File Offset: 0x00021254
		private bool TryCreateMemoryMappedFileBlock(long start, int size, [NotNullWhen(true)] out MemoryMappedFileBlock block)
		{
			if (this._lazyMemoryMap == null)
			{
				object streamGuard = this._streamGuard;
				IDisposable disposable;
				lock (streamGuard)
				{
					disposable = MemoryMapLightUp.CreateMemoryMap(this._stream);
				}
				if (disposable == null)
				{
					block = null;
					return false;
				}
				if (Interlocked.CompareExchange<IDisposable>(ref this._lazyMemoryMap, disposable, null) != null)
				{
					disposable.Dispose();
				}
			}
			IDisposable disposable2 = MemoryMapLightUp.CreateViewAccessor(this._lazyMemoryMap, start, size);
			if (disposable2 == null)
			{
				block = null;
				return false;
			}
			SafeBuffer safeBuffer;
			long num;
			if (!MemoryMapLightUp.TryGetSafeBufferAndPointerOffset(disposable2, out safeBuffer, out num))
			{
				block = null;
				return false;
			}
			block = new MemoryMappedFileBlock(disposable2, safeBuffer, num, size);
			return true;
		}

		// Token: 0x04000954 RID: 2388
		internal const int MemoryMapThreshold = 16384;

		// Token: 0x04000955 RID: 2389
		private Stream _stream;

		// Token: 0x04000956 RID: 2390
		private readonly object _streamGuard;

		// Token: 0x04000957 RID: 2391
		private readonly bool _leaveOpen;

		// Token: 0x04000958 RID: 2392
		private bool _useMemoryMap;

		// Token: 0x04000959 RID: 2393
		private readonly bool _isFileStream;

		// Token: 0x0400095A RID: 2394
		private readonly long _imageStart;

		// Token: 0x0400095B RID: 2395
		private readonly int _imageSize;

		// Token: 0x0400095C RID: 2396
		private IDisposable _lazyMemoryMap;
	}
}

using System;
using System.Buffers;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Compression
{
	// Token: 0x02000010 RID: 16
	public struct BrotliDecoder : IDisposable
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002FC0 File Offset: 0x000011C0
		internal void InitializeDecoder()
		{
			this._state = Interop.Brotli.BrotliDecoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (this._state.IsInvalid)
			{
				throw new IOException("Failed to create BrotliDecoder instance");
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002FF4 File Offset: 0x000011F4
		internal void EnsureInitialized()
		{
			this.EnsureNotDisposed();
			if (this._state == null)
			{
				this.InitializeDecoder();
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000300A File Offset: 0x0000120A
		public void Dispose()
		{
			this._disposed = true;
			SafeBrotliDecoderHandle state = this._state;
			if (state == null)
			{
				return;
			}
			state.Dispose();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003023 File Offset: 0x00001223
		private void EnsureNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("BrotliDecoder", "Can not access a closed Decoder.");
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003040 File Offset: 0x00001240
		public unsafe OperationStatus Decompress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten)
		{
			this.EnsureInitialized();
			bytesConsumed = 0;
			bytesWritten = 0;
			if (Interop.Brotli.BrotliDecoderIsFinished(this._state))
			{
				return OperationStatus.Done;
			}
			IntPtr intPtr = (IntPtr)destination.Length;
			IntPtr intPtr2 = (IntPtr)source.Length;
			while ((int)intPtr > 0)
			{
				fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
				{
					byte* ptr = reference;
					fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(destination))
					{
						byte* ptr2 = reference2;
						byte* ptr3 = ptr;
						byte* ptr4 = ptr2;
						IntPtr intPtr3;
						int num = Interop.Brotli.BrotliDecoderDecompressStream(this._state, ref intPtr2, &ptr3, ref intPtr, &ptr4, out intPtr3);
						if (num == 0)
						{
							return OperationStatus.InvalidData;
						}
						bytesConsumed += source.Length - (int)intPtr2;
						bytesWritten += destination.Length - (int)intPtr;
						switch (num)
						{
						case 1:
							return OperationStatus.Done;
						case 3:
							return OperationStatus.DestinationTooSmall;
						}
						source = source.Slice(source.Length - (int)intPtr2);
						destination = destination.Slice(destination.Length - (int)intPtr);
						if (num == 2 && source.Length == 0)
						{
							return OperationStatus.NeedMoreData;
						}
					}
				}
			}
			return OperationStatus.DestinationTooSmall;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003158 File Offset: 0x00001358
		public unsafe static bool TryDecompress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
		{
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
			{
				byte* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(destination))
				{
					byte* ptr2 = reference2;
					IntPtr intPtr = (IntPtr)destination.Length;
					bool flag = Interop.Brotli.BrotliDecoderDecompress((IntPtr)source.Length, ptr, ref intPtr, ptr2);
					bytesWritten = (int)intPtr;
					return flag;
				}
			}
		}

		// Token: 0x040000A5 RID: 165
		private SafeBrotliDecoderHandle _state;

		// Token: 0x040000A6 RID: 166
		private bool _disposed;
	}
}

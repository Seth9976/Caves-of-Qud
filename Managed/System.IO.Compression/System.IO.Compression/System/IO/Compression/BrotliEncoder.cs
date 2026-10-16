using System;
using System.Buffers;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Compression
{
	// Token: 0x02000011 RID: 17
	public struct BrotliEncoder : IDisposable
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000031A8 File Offset: 0x000013A8
		public BrotliEncoder(int quality, int window)
		{
			this._disposed = false;
			this._state = Interop.Brotli.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (this._state.IsInvalid)
			{
				throw new IOException("Failed to create BrotliEncoder instance");
			}
			this.SetQuality(quality);
			this.SetWindow(window);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000031FC File Offset: 0x000013FC
		internal void InitializeEncoder()
		{
			this.EnsureNotDisposed();
			this._state = Interop.Brotli.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (this._state.IsInvalid)
			{
				throw new IOException("Failed to create BrotliEncoder instance");
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003236 File Offset: 0x00001436
		internal void EnsureInitialized()
		{
			this.EnsureNotDisposed();
			if (this._state == null)
			{
				this.InitializeEncoder();
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000324C File Offset: 0x0000144C
		public void Dispose()
		{
			this._disposed = true;
			SafeBrotliEncoderHandle state = this._state;
			if (state == null)
			{
				return;
			}
			state.Dispose();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003265 File Offset: 0x00001465
		private void EnsureNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("BrotliEncoder", "Can not access a closed Encoder.");
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003280 File Offset: 0x00001480
		internal void SetQuality(int quality)
		{
			this.EnsureNotDisposed();
			if (this._state == null || this._state.IsInvalid || this._state.IsClosed)
			{
				this.InitializeEncoder();
			}
			if (quality < 0 || quality > 11)
			{
				throw new ArgumentOutOfRangeException("quality", SR.Format("Provided BrotliEncoder Quality of {0} is not between the minimum value of {1} and the maximum value of {2}", quality, 0, 11));
			}
			if (!Interop.Brotli.BrotliEncoderSetParameter(this._state, BrotliEncoderParameter.Quality, (uint)quality))
			{
				throw new InvalidOperationException(SR.Format("The BrotliEncoder {0} can not be changed at current encoder state.", "Quality"));
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003310 File Offset: 0x00001510
		internal void SetWindow(int window)
		{
			this.EnsureNotDisposed();
			if (this._state == null || this._state.IsInvalid || this._state.IsClosed)
			{
				this.InitializeEncoder();
			}
			if (window < 10 || window > 24)
			{
				throw new ArgumentOutOfRangeException("window", SR.Format("Provided BrotliEncoder Window of {0} is not between the minimum value of {1} and the maximum value of {2}", window, 10, 24));
			}
			if (!Interop.Brotli.BrotliEncoderSetParameter(this._state, BrotliEncoderParameter.LGWin, (uint)window))
			{
				throw new InvalidOperationException(SR.Format("The BrotliEncoder {0} can not be changed at current encoder state.", "Window"));
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000033A4 File Offset: 0x000015A4
		public static int GetMaxCompressedLength(int length)
		{
			if (length < 0 || length > 2147483132)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (length == 0)
			{
				return 1;
			}
			int num = length >> 24;
			int num2 = (((length & 16777215) > 1048576) ? 4 : 3);
			int num3 = 2 + 4 * num + num2 + 1;
			return length + num3;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000033F2 File Offset: 0x000015F2
		internal OperationStatus Flush(Memory<byte> destination, out int bytesWritten)
		{
			return this.Flush(destination.Span, out bytesWritten);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003404 File Offset: 0x00001604
		public OperationStatus Flush(Span<byte> destination, out int bytesWritten)
		{
			int num;
			return this.Compress(ReadOnlySpan<byte>.Empty, destination, out num, out bytesWritten, BrotliEncoderOperation.Flush);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003421 File Offset: 0x00001621
		internal OperationStatus Compress(ReadOnlyMemory<byte> source, Memory<byte> destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			return this.Compress(source.Span, destination.Span, out bytesConsumed, out bytesWritten, isFinalBlock);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000343C File Offset: 0x0000163C
		public OperationStatus Compress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			return this.Compress(source, destination, out bytesConsumed, out bytesWritten, isFinalBlock ? BrotliEncoderOperation.Finish : BrotliEncoderOperation.Process);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003454 File Offset: 0x00001654
		internal unsafe OperationStatus Compress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten, BrotliEncoderOperation operation)
		{
			this.EnsureInitialized();
			bytesWritten = 0;
			bytesConsumed = 0;
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
						if (!Interop.Brotli.BrotliEncoderCompressStream(this._state, operation, ref intPtr2, &ptr3, ref intPtr, &ptr4, out intPtr3))
						{
							return OperationStatus.InvalidData;
						}
						bytesConsumed += source.Length - (int)intPtr2;
						bytesWritten += destination.Length - (int)intPtr;
						if ((int)intPtr == destination.Length && !Interop.Brotli.BrotliEncoderHasMoreOutput(this._state) && (int)intPtr2 == 0)
						{
							return OperationStatus.Done;
						}
						source = source.Slice(source.Length - (int)intPtr2);
						destination = destination.Slice(destination.Length - (int)intPtr);
					}
				}
			}
			return OperationStatus.DestinationTooSmall;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003554 File Offset: 0x00001754
		public static bool TryCompress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
		{
			return BrotliEncoder.TryCompress(source, destination, out bytesWritten, 11, 22);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003564 File Offset: 0x00001764
		public unsafe static bool TryCompress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten, int quality, int window)
		{
			if (quality < 0 || quality > 11)
			{
				throw new ArgumentOutOfRangeException("quality", SR.Format("Provided BrotliEncoder Quality of {0} is not between the minimum value of {1} and the maximum value of {2}", quality, 0, 11));
			}
			if (window < 10 || window > 24)
			{
				throw new ArgumentOutOfRangeException("window", SR.Format("Provided BrotliEncoder Window of {0} is not between the minimum value of {1} and the maximum value of {2}", window, 10, 24));
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
			{
				byte* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(destination))
				{
					byte* ptr2 = reference2;
					IntPtr intPtr = (IntPtr)destination.Length;
					bool flag = Interop.Brotli.BrotliEncoderCompress(quality, window, 0, (IntPtr)source.Length, ptr, ref intPtr, ptr2);
					bytesWritten = (int)intPtr;
					return flag;
				}
			}
		}

		// Token: 0x040000A7 RID: 167
		internal SafeBrotliEncoderHandle _state;

		// Token: 0x040000A8 RID: 168
		private bool _disposed;
	}
}

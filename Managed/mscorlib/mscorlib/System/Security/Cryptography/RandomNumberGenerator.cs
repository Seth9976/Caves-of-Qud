using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of cryptographic random number generators derive.</summary>
	// Token: 0x020004A0 RID: 1184
	[ComVisible(true)]
	public abstract class RandomNumberGenerator : IDisposable
	{
		/// <summary>When overridden in a derived class, creates an instance of the default implementation of a cryptographic random number generator that can be used to generate random data.</summary>
		/// <returns>A new instance of a cryptographic random number generator.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002F61 RID: 12129 RVA: 0x000A8C9E File Offset: 0x000A6E9E
		public static RandomNumberGenerator Create()
		{
			return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
		}

		/// <summary>When overridden in a derived class, creates an instance of the specified implementation of a cryptographic random number generator.</summary>
		/// <returns>A new instance of a cryptographic random number generator.</returns>
		/// <param name="rngName">The name of the random number generator implementation to use. </param>
		// Token: 0x06002F62 RID: 12130 RVA: 0x000A8CAA File Offset: 0x000A6EAA
		public static RandomNumberGenerator Create(string rngName)
		{
			return (RandomNumberGenerator)CryptoConfig.CreateFromName(rngName);
		}

		/// <summary>When overridden in a derived class, releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.RandomNumberGenerator" /> class.</summary>
		// Token: 0x06002F63 RID: 12131 RVA: 0x000A8CB7 File Offset: 0x000A6EB7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.RandomNumberGenerator" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06002F64 RID: 12132 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>When overridden in a derived class, fills an array of bytes with a cryptographically strong random sequence of values.</summary>
		/// <param name="data">The array to fill with cryptographically strong random bytes. </param>
		// Token: 0x06002F65 RID: 12133
		public abstract void GetBytes(byte[] data);

		// Token: 0x06002F66 RID: 12134 RVA: 0x000A8CC8 File Offset: 0x000A6EC8
		public virtual void GetBytes(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (offset + count > data.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (count > 0)
			{
				byte[] array = new byte[count];
				this.GetBytes(array);
				Array.Copy(array, 0, data, offset, count);
			}
		}

		/// <summary>When overridden in a derived class, fills an array of bytes with a cryptographically strong random sequence of nonzero values.</summary>
		/// <param name="data">The array to fill with cryptographically strong random nonzero bytes. </param>
		// Token: 0x06002F67 RID: 12135 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual void GetNonZeroBytes(byte[] data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000A8D49 File Offset: 0x000A6F49
		public static void Fill(Span<byte> data)
		{
			RandomNumberGenerator.FillSpan(data);
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000A8D54 File Offset: 0x000A6F54
		internal unsafe static void FillSpan(Span<byte> data)
		{
			if (data.Length > 0)
			{
				fixed (byte* pinnableReference = data.GetPinnableReference())
				{
					Interop.GetRandomBytes(pinnableReference, data.Length);
				}
			}
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000A8D84 File Offset: 0x000A6F84
		public virtual void GetBytes(Span<byte> data)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(data.Length);
			try
			{
				this.GetBytes(array, 0, data.Length);
				new ReadOnlySpan<byte>(array, 0, data.Length).CopyTo(data);
			}
			finally
			{
				Array.Clear(array, 0, data.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x000A8DF8 File Offset: 0x000A6FF8
		public virtual void GetNonZeroBytes(Span<byte> data)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(data.Length);
			try
			{
				this.GetNonZeroBytes(array);
				new ReadOnlySpan<byte>(array, 0, data.Length).CopyTo(data);
			}
			finally
			{
				Array.Clear(array, 0, data.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x000A8E64 File Offset: 0x000A7064
		public unsafe static int GetInt32(int fromInclusive, int toExclusive)
		{
			if (fromInclusive >= toExclusive)
			{
				throw new ArgumentException("Range of random number does not contain at least one possibility.");
			}
			uint num = (uint)(toExclusive - fromInclusive - 1);
			if (num == 0U)
			{
				return fromInclusive;
			}
			uint num2 = num;
			num2 |= num2 >> 1;
			num2 |= num2 >> 2;
			num2 |= num2 >> 4;
			num2 |= num2 >> 8;
			num2 |= num2 >> 16;
			Span<uint> span = new Span<uint>(stackalloc byte[(UIntPtr)4], 1);
			uint num3;
			do
			{
				RandomNumberGenerator.FillSpan(MemoryMarshal.AsBytes<uint>(span));
				num3 = num2 & *span[0];
			}
			while (num3 > num);
			return (int)(num3 + (uint)fromInclusive);
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x000A8ED5 File Offset: 0x000A70D5
		public static int GetInt32(int toExclusive)
		{
			if (toExclusive <= 0)
			{
				throw new ArgumentOutOfRangeException("toExclusive", "Positive number required.");
			}
			return RandomNumberGenerator.GetInt32(0, toExclusive);
		}
	}
}

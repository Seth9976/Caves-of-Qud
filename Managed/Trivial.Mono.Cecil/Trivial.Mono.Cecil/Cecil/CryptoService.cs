using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200001E RID: 30
	internal static class CryptoService
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x000042DC File Offset: 0x000024DC
		public static void StrongName(Stream stream, ImageWriter writer, StrongNameKeyPair key_pair)
		{
			int num;
			byte[] array = CryptoService.CreateStrongName(key_pair, CryptoService.HashStream(stream, writer, out num));
			CryptoService.PatchStrongName(stream, num, array);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004301 File Offset: 0x00002501
		private static void PatchStrongName(Stream stream, int strong_name_pointer, byte[] strong_name)
		{
			stream.Seek((long)strong_name_pointer, 0);
			stream.Write(strong_name, 0, strong_name.Length);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004318 File Offset: 0x00002518
		private static byte[] CreateStrongName(StrongNameKeyPair key_pair, byte[] hash)
		{
			byte[] array2;
			using (RSA rsa = key_pair.CreateRSA())
			{
				RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(rsa);
				rsapkcs1SignatureFormatter.SetHashAlgorithm("SHA1");
				byte[] array = rsapkcs1SignatureFormatter.CreateSignature(hash);
				Array.Reverse(array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000436C File Offset: 0x0000256C
		private static byte[] HashStream(Stream stream, ImageWriter writer, out int strong_name_pointer)
		{
			Section text = writer.text;
			int headerSize = (int)writer.GetHeaderSize();
			int pointerToRawData = (int)text.PointerToRawData;
			DataDirectory strongNameSignatureDirectory = writer.GetStrongNameSignatureDirectory();
			if (strongNameSignatureDirectory.Size == 0U)
			{
				throw new InvalidOperationException();
			}
			strong_name_pointer = (int)((long)pointerToRawData + (long)((ulong)(strongNameSignatureDirectory.VirtualAddress - text.VirtualAddress)));
			int size = (int)strongNameSignatureDirectory.Size;
			SHA1Managed sha1Managed = new SHA1Managed();
			byte[] array = new byte[8192];
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, sha1Managed, 1))
			{
				stream.Seek(0L, 0);
				CryptoService.CopyStreamChunk(stream, cryptoStream, array, headerSize);
				stream.Seek((long)pointerToRawData, 0);
				CryptoService.CopyStreamChunk(stream, cryptoStream, array, strong_name_pointer - pointerToRawData);
				stream.Seek((long)size, 1);
				CryptoService.CopyStreamChunk(stream, cryptoStream, array, (int)(stream.Length - (long)(strong_name_pointer + size)));
			}
			return sha1Managed.Hash;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004458 File Offset: 0x00002658
		private static void CopyStreamChunk(Stream stream, Stream dest_stream, byte[] buffer, int length)
		{
			while (length > 0)
			{
				int num = stream.Read(buffer, 0, Math.Min(buffer.Length, length));
				dest_stream.Write(buffer, 0, num);
				length -= num;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000448C File Offset: 0x0000268C
		public static byte[] ComputeHash(string file)
		{
			if (!File.Exists(file))
			{
				return Empty<byte>.Array;
			}
			SHA1Managed sha1Managed = new SHA1Managed();
			using (FileStream fileStream = new FileStream(file, 3, 1, 1))
			{
				byte[] array = new byte[8192];
				using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, sha1Managed, 1))
				{
					CryptoService.CopyStreamChunk(fileStream, cryptoStream, array, (int)fileStream.Length);
				}
			}
			return sha1Managed.Hash;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Internal
{
	// Token: 0x0200017E RID: 382
	internal static class EncodingHelper
	{
		// Token: 0x06000BA8 RID: 2984 RVA: 0x000231DA File Offset: 0x000213DA
		[NullableContext(1)]
		public unsafe static string DecodeUtf8([Nullable(0)] byte* bytes, int byteCount, byte[] prefix, MetadataStringDecoder utf8Decoder)
		{
			if (prefix != null)
			{
				return EncodingHelper.DecodeUtf8Prefixed(bytes, byteCount, prefix, utf8Decoder);
			}
			if (byteCount == 0)
			{
				return string.Empty;
			}
			return utf8Decoder.GetString(bytes, byteCount);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000231FC File Offset: 0x000213FC
		private unsafe static string DecodeUtf8Prefixed(byte* bytes, int byteCount, byte[] prefix, MetadataStringDecoder utf8Decoder)
		{
			int num = byteCount + prefix.Length;
			if (num == 0)
			{
				return string.Empty;
			}
			byte[] array = EncodingHelper.AcquireBuffer(num);
			prefix.CopyTo(array, 0);
			Marshal.Copy((IntPtr)((void*)bytes), array, prefix.Length, byteCount);
			string @string;
			fixed (byte* ptr = &array[0])
			{
				byte* ptr2 = ptr;
				@string = utf8Decoder.GetString(ptr2, num);
			}
			EncodingHelper.ReleaseBuffer(array);
			return @string;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00023258 File Offset: 0x00021458
		private static byte[] AcquireBuffer(int byteCount)
		{
			if (byteCount > 200)
			{
				return new byte[byteCount];
			}
			return EncodingHelper.s_pool.Allocate();
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00023273 File Offset: 0x00021473
		private static void ReleaseBuffer(byte[] buffer)
		{
			if (buffer.Length == 200)
			{
				EncodingHelper.s_pool.Free(buffer);
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002328A File Offset: 0x0002148A
		[NullableContext(1)]
		public unsafe static string GetString(this Encoding encoding, [Nullable(0)] byte* bytes, int byteCount)
		{
			if (EncodingHelper.s_getStringPlatform == null)
			{
				return EncodingHelper.GetStringPortable(encoding, bytes, byteCount);
			}
			return EncodingHelper.s_getStringPlatform(encoding, bytes, byteCount);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x000232AC File Offset: 0x000214AC
		private unsafe static string GetStringPortable(Encoding encoding, byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			byte[] array = EncodingHelper.AcquireBuffer(byteCount);
			Marshal.Copy((IntPtr)((void*)bytes), array, 0, byteCount);
			string @string = encoding.GetString(array, 0, byteCount);
			EncodingHelper.ReleaseBuffer(array);
			return @string;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00023300 File Offset: 0x00021500
		private unsafe static EncodingHelper.Encoding_GetString LoadGetStringPlatform()
		{
			MethodInfo method = LightUpHelper.GetMethod(typeof(Encoding), "GetString", new Type[]
			{
				typeof(byte*),
				typeof(int)
			});
			if (method != null && method.ReturnType == typeof(string))
			{
				try
				{
					return (EncodingHelper.Encoding_GetString)method.CreateDelegate(typeof(EncodingHelper.Encoding_GetString), null);
				}
				catch (MemberAccessException)
				{
				}
				catch (InvalidOperationException)
				{
				}
			}
			IEnumerable<MethodInfo> declaredMethods = IntrospectionExtensions.GetTypeInfo(typeof(string)).GetDeclaredMethods("CreateStringFromEncoding");
			foreach (MethodInfo methodInfo in declaredMethods)
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length == 3 && parameters[0].ParameterType == typeof(byte*) && parameters[1].ParameterType == typeof(int) && parameters[2].ParameterType == typeof(Encoding) && methodInfo.ReturnType == typeof(string))
				{
					try
					{
						EncodingHelper.String_CreateStringFromEncoding createStringFromEncoding = (EncodingHelper.String_CreateStringFromEncoding)methodInfo.CreateDelegate(typeof(EncodingHelper.String_CreateStringFromEncoding), null);
						return (Encoding encoding, byte* bytes, int byteCount) => EncodingHelper.GetStringUsingCreateStringFromEncoding(createStringFromEncoding, bytes, byteCount, encoding);
					}
					catch (MemberAccessException)
					{
					}
					catch (InvalidOperationException)
					{
					}
				}
			}
			return null;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000234C4 File Offset: 0x000216C4
		private unsafe static string GetStringUsingCreateStringFromEncoding(EncodingHelper.String_CreateStringFromEncoding createStringFromEncoding, byte* bytes, int byteCount, Encoding encoding)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			return createStringFromEncoding(bytes, byteCount, encoding);
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x000234EE File Offset: 0x000216EE
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x000234F8 File Offset: 0x000216F8
		internal static bool TestOnly_LightUpEnabled
		{
			get
			{
				return EncodingHelper.s_getStringPlatform != null;
			}
			set
			{
				EncodingHelper.s_getStringPlatform = (value ? EncodingHelper.LoadGetStringPlatform() : null);
			}
		}

		// Token: 0x0400095E RID: 2398
		public const int PooledBufferSize = 200;

		// Token: 0x0400095F RID: 2399
		private static readonly ObjectPool<byte[]> s_pool = new ObjectPool<byte[]>(() => new byte[200]);

		// Token: 0x04000960 RID: 2400
		private static EncodingHelper.Encoding_GetString s_getStringPlatform = EncodingHelper.LoadGetStringPlatform();

		// Token: 0x02000206 RID: 518
		// (Invoke) Token: 0x06000D59 RID: 3417
		internal unsafe delegate string Encoding_GetString(Encoding encoding, [Nullable(0)] byte* bytes, int byteCount);

		// Token: 0x02000207 RID: 519
		// (Invoke) Token: 0x06000D5D RID: 3421
		private unsafe delegate string String_CreateStringFromEncoding(byte* bytes, int byteCount, Encoding encoding);
	}
}

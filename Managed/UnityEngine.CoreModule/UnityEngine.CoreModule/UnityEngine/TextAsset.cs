using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020002B2 RID: 690
	[NativeHeader("Runtime/Scripting/TextAsset.h")]
	public class TextAsset : Object
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x00041BFC File Offset: 0x0003FDFC
		public byte[] bytes
		{
			[return: Unmarshalled]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TextAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TextAsset.get_bytes_Injected(intPtr);
			}
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x00041C20 File Offset: 0x0003FE20
		[return: Unmarshalled]
		private byte[] GetPreviewBytes(int maxByteCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TextAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TextAsset.GetPreviewBytes_Injected(intPtr, maxByteCount);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x00041C44 File Offset: 0x0003FE44
		private unsafe static void Internal_CreateInstance([Writable] TextAsset self, string text)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(text, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = text.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				TextAsset.Internal_CreateInstance_Injected(self, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x00041C9C File Offset: 0x0003FE9C
		private unsafe static void Internal_CreateInstanceFromBytes([Writable] TextAsset self, ReadOnlySpan<byte> bytes)
		{
			ReadOnlySpan<byte> readOnlySpan = bytes;
			fixed (byte* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				TextAsset.Internal_CreateInstanceFromBytes_Injected(self, ref managedSpanWrapper);
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x00041CD0 File Offset: 0x0003FED0
		private IntPtr GetDataPtr()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TextAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TextAsset.GetDataPtr_Injected(intPtr);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x00041CF4 File Offset: 0x0003FEF4
		private long GetDataSize()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TextAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TextAsset.GetDataSize_Injected(intPtr);
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x00041D18 File Offset: 0x0003FF18
		public string text
		{
			get
			{
				byte[] bytes = this.bytes;
				return (bytes.Length == 0) ? string.Empty : TextAsset.DecodeString(bytes);
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x00041D42 File Offset: 0x0003FF42
		public long dataSize
		{
			get
			{
				return this.GetDataSize();
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x00041D4C File Offset: 0x0003FF4C
		public override string ToString()
		{
			return this.text;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x00041D64 File Offset: 0x0003FF64
		public TextAsset()
			: this(TextAsset.CreateOptions.CreateNativeObject, null)
		{
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x00041D70 File Offset: 0x0003FF70
		public TextAsset(string text)
			: this(TextAsset.CreateOptions.CreateNativeObject, text)
		{
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x00041D7C File Offset: 0x0003FF7C
		public TextAsset(ReadOnlySpan<byte> bytes)
			: this(TextAsset.CreateOptions.CreateNativeObject, bytes)
		{
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x00041D88 File Offset: 0x0003FF88
		internal TextAsset(TextAsset.CreateOptions options, string text)
		{
			bool flag = options == TextAsset.CreateOptions.CreateNativeObject;
			if (flag)
			{
				TextAsset.Internal_CreateInstance(this, text);
			}
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00041DB0 File Offset: 0x0003FFB0
		internal TextAsset(TextAsset.CreateOptions options, ReadOnlySpan<byte> bytes)
		{
			bool flag = options == TextAsset.CreateOptions.CreateNativeObject;
			if (flag)
			{
				TextAsset.Internal_CreateInstanceFromBytes(this, bytes);
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x00041DD8 File Offset: 0x0003FFD8
		public unsafe NativeArray<T> GetData<T>() where T : struct
		{
			long dataSize = this.GetDataSize();
			long num = (long)UnsafeUtility.SizeOf<T>();
			bool flag = dataSize % num != 0L;
			if (flag)
			{
				throw new ArgumentException(string.Format("Type passed to {0} can't capture the asset data. Data size is {1} which is not a multiple of type size {2}", "GetData", dataSize, num));
			}
			long num2 = dataSize / num;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetDataPtr(), (int)num2, Allocator.None);
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x00041E44 File Offset: 0x00040044
		internal string GetPreview(int maxChars)
		{
			return TextAsset.DecodeString(this.GetPreviewBytes(maxChars * 4));
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x00041E64 File Offset: 0x00040064
		internal static string DecodeString(byte[] bytes)
		{
			int num = TextAsset.EncodingUtility.encodingLookup.Length;
			int i = 0;
			int num2;
			while (i < num)
			{
				byte[] key = TextAsset.EncodingUtility.encodingLookup[i].Key;
				num2 = key.Length;
				bool flag = bytes.Length >= num2;
				if (flag)
				{
					for (int j = 0; j < num2; j++)
					{
						bool flag2 = key[j] != bytes[j];
						if (flag2)
						{
							num2 = -1;
						}
					}
					bool flag3 = num2 < 0;
					if (!flag3)
					{
						try
						{
							Encoding value = TextAsset.EncodingUtility.encodingLookup[i].Value;
							return value.GetString(bytes, num2, bytes.Length - num2);
						}
						catch
						{
						}
					}
				}
				IL_00A2:
				i++;
				continue;
				goto IL_00A2;
			}
			num2 = 0;
			Encoding targetEncoding = TextAsset.EncodingUtility.targetEncoding;
			return targetEncoding.GetString(bytes, num2, bytes.Length - num2);
		}

		// Token: 0x060021CF RID: 8655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] get_bytes_Injected(IntPtr _unity_self);

		// Token: 0x060021D0 RID: 8656
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] GetPreviewBytes_Injected(IntPtr _unity_self, int maxByteCount);

		// Token: 0x060021D1 RID: 8657
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateInstance_Injected([Writable] TextAsset self, ref ManagedSpanWrapper text);

		// Token: 0x060021D2 RID: 8658
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateInstanceFromBytes_Injected([Writable] TextAsset self, ref ManagedSpanWrapper bytes);

		// Token: 0x060021D3 RID: 8659
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDataPtr_Injected(IntPtr _unity_self);

		// Token: 0x060021D4 RID: 8660
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetDataSize_Injected(IntPtr _unity_self);

		// Token: 0x020002B3 RID: 691
		internal enum CreateOptions
		{
			// Token: 0x040009DE RID: 2526
			None,
			// Token: 0x040009DF RID: 2527
			CreateNativeObject
		}

		// Token: 0x020002B4 RID: 692
		private static class EncodingUtility
		{
			// Token: 0x060021D5 RID: 8661 RVA: 0x00041F50 File Offset: 0x00040150
			static EncodingUtility()
			{
				Encoding encoding = new UTF32Encoding(true, true, true);
				Encoding encoding2 = new UTF32Encoding(false, true, true);
				Encoding encoding3 = new UnicodeEncoding(true, true, true);
				Encoding encoding4 = new UnicodeEncoding(false, true, true);
				Encoding encoding5 = new UTF8Encoding(true, true);
				TextAsset.EncodingUtility.encodingLookup = new KeyValuePair<byte[], Encoding>[]
				{
					new KeyValuePair<byte[], Encoding>(encoding.GetPreamble(), encoding),
					new KeyValuePair<byte[], Encoding>(encoding2.GetPreamble(), encoding2),
					new KeyValuePair<byte[], Encoding>(encoding3.GetPreamble(), encoding3),
					new KeyValuePair<byte[], Encoding>(encoding4.GetPreamble(), encoding4),
					new KeyValuePair<byte[], Encoding>(encoding5.GetPreamble(), encoding5)
				};
			}

			// Token: 0x040009E0 RID: 2528
			internal static readonly KeyValuePair<byte[], Encoding>[] encodingLookup;

			// Token: 0x040009E1 RID: 2529
			internal static readonly Encoding targetEncoding = Encoding.GetEncoding(Encoding.UTF8.CodePage, new EncoderReplacementFallback("\ufffd"), new DecoderReplacementFallback("\ufffd"));
		}
	}
}

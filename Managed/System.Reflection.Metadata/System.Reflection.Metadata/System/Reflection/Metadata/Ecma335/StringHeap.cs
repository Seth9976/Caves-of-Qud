using System;
using System.Diagnostics;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E6 RID: 230
	[NullableContext(1)]
	[Nullable(0)]
	internal struct StringHeap
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x00018084 File Offset: 0x00016284
		internal StringHeap(MemoryBlock block, MetadataKind metadataKind)
		{
			this._lazyVirtualHeap = null;
			if (StringHeap.s_virtualValues == null && metadataKind != MetadataKind.Ecma335)
			{
				StringHeap.s_virtualValues = new string[]
				{
					"System.Runtime.WindowsRuntime", "System.Runtime", "System.ObjectModel", "System.Runtime.WindowsRuntime.UI.Xaml", "System.Runtime.InteropServices.WindowsRuntime", "System.Numerics.Vectors", "Dispose", "AttributeTargets", "AttributeUsageAttribute", "Color",
					"CornerRadius", "DateTimeOffset", "Duration", "DurationType", "EventHandler`1", "EventRegistrationToken", "Exception", "GeneratorPosition", "GridLength", "GridUnitType",
					"ICommand", "IDictionary`2", "IDisposable", "IEnumerable", "IEnumerable`1", "IList", "IList`1", "INotifyCollectionChanged", "INotifyPropertyChanged", "IReadOnlyDictionary`2",
					"IReadOnlyList`1", "KeyTime", "KeyValuePair`2", "Matrix", "Matrix3D", "Matrix3x2", "Matrix4x4", "NotifyCollectionChangedAction", "NotifyCollectionChangedEventArgs", "NotifyCollectionChangedEventHandler",
					"Nullable`1", "Plane", "Point", "PropertyChangedEventArgs", "PropertyChangedEventHandler", "Quaternion", "Rect", "RepeatBehavior", "RepeatBehaviorType", "Size",
					"System", "System.Collections", "System.Collections.Generic", "System.Collections.Specialized", "System.ComponentModel", "System.Numerics", "System.Windows.Input", "Thickness", "TimeSpan", "Type",
					"Uri", "Vector2", "Vector3", "Vector4", "Windows.Foundation", "Windows.UI", "Windows.UI.Xaml", "Windows.UI.Xaml.Controls.Primitives", "Windows.UI.Xaml.Media", "Windows.UI.Xaml.Media.Animation",
					"Windows.UI.Xaml.Media.Media3D"
				};
			}
			this.Block = StringHeap.TrimEnd(block);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00018338 File Offset: 0x00016538
		[Conditional("DEBUG")]
		private static void AssertFilled()
		{
			for (int i = 0; i < StringHeap.s_virtualValues.Length; i++)
			{
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00018358 File Offset: 0x00016558
		private static MemoryBlock TrimEnd(MemoryBlock block)
		{
			if (block.Length == 0)
			{
				return block;
			}
			int num = block.Length - 1;
			while (num >= 0 && block.PeekByte(num) == 0)
			{
				num--;
			}
			if (num == block.Length - 1)
			{
				return block;
			}
			return block.GetMemoryBlockAt(0, num + 2);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000183A4 File Offset: 0x000165A4
		internal string GetString(StringHandle handle, MetadataStringDecoder utf8Decoder)
		{
			if (!handle.IsVirtual)
			{
				return this.GetNonVirtualString(handle, utf8Decoder, null);
			}
			return this.GetVirtualHandleString(handle, utf8Decoder);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000183C1 File Offset: 0x000165C1
		internal MemoryBlock GetMemoryBlock(StringHandle handle)
		{
			if (!handle.IsVirtual)
			{
				return this.GetNonVirtualStringMemoryBlock(handle);
			}
			return this.GetVirtualHandleMemoryBlock(handle);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000183DB File Offset: 0x000165DB
		internal static string GetVirtualString(StringHandle.VirtualIndex index)
		{
			return StringHeap.s_virtualValues[(int)index];
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x000183E4 File Offset: 0x000165E4
		private string GetNonVirtualString(StringHandle handle, MetadataStringDecoder utf8Decoder, byte[] prefixOpt)
		{
			char c = ((handle.StringKind == StringKind.DotTerminated) ? '.' : '\0');
			int num;
			return this.Block.PeekUtf8NullTerminated(handle.GetHeapOffset(), prefixOpt, utf8Decoder, out num, c);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00018418 File Offset: 0x00016618
		private MemoryBlock GetNonVirtualStringMemoryBlock(StringHandle handle)
		{
			char c = ((handle.StringKind == StringKind.DotTerminated) ? '.' : '\0');
			int heapOffset = handle.GetHeapOffset();
			int num;
			int utf8NullTerminatedLength = this.Block.GetUtf8NullTerminatedLength(heapOffset, out num, c);
			return new MemoryBlock(this.Block.Pointer + heapOffset, utf8NullTerminatedLength);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00018460 File Offset: 0x00016660
		private unsafe byte[] GetNonVirtualStringBytes(StringHandle handle, byte[] prefix)
		{
			MemoryBlock nonVirtualStringMemoryBlock = this.GetNonVirtualStringMemoryBlock(handle);
			byte[] array = new byte[prefix.Length + nonVirtualStringMemoryBlock.Length];
			Buffer.BlockCopy(prefix, 0, array, 0, prefix.Length);
			Marshal.Copy((IntPtr)((void*)nonVirtualStringMemoryBlock.Pointer), array, prefix.Length, nonVirtualStringMemoryBlock.Length);
			return array;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000184AC File Offset: 0x000166AC
		private string GetVirtualHandleString(StringHandle handle, MetadataStringDecoder utf8Decoder)
		{
			StringKind stringKind = handle.StringKind;
			string text;
			if (stringKind != StringKind.Virtual)
			{
				if (stringKind != StringKind.WinRTPrefixed)
				{
					throw ExceptionUtilities.UnexpectedValue(handle.StringKind);
				}
				text = this.GetNonVirtualString(handle, utf8Decoder, MetadataReader.WinRTPrefix);
			}
			else
			{
				text = StringHeap.GetVirtualString(handle.GetVirtualIndex());
			}
			return text;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00018500 File Offset: 0x00016700
		private MemoryBlock GetVirtualHandleMemoryBlock(StringHandle handle)
		{
			VirtualHeap orCreateVirtualHeap = VirtualHeap.GetOrCreateVirtualHeap(ref this._lazyVirtualHeap);
			VirtualHeap virtualHeap = orCreateVirtualHeap;
			MemoryBlock memoryBlock2;
			lock (virtualHeap)
			{
				MemoryBlock memoryBlock;
				if (!orCreateVirtualHeap.TryGetMemoryBlock(handle.RawValue, out memoryBlock))
				{
					StringKind stringKind = handle.StringKind;
					byte[] array;
					if (stringKind != StringKind.Virtual)
					{
						if (stringKind != StringKind.WinRTPrefixed)
						{
							throw ExceptionUtilities.UnexpectedValue(handle.StringKind);
						}
						array = this.GetNonVirtualStringBytes(handle, MetadataReader.WinRTPrefix);
					}
					else
					{
						array = Encoding.UTF8.GetBytes(StringHeap.GetVirtualString(handle.GetVirtualIndex()));
					}
					byte[] array2 = array;
					memoryBlock = orCreateVirtualHeap.AddBlob(handle.RawValue, array2);
				}
				memoryBlock2 = memoryBlock;
			}
			return memoryBlock2;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000185BC File Offset: 0x000167BC
		internal BlobReader GetBlobReader(StringHandle handle)
		{
			return new BlobReader(this.GetMemoryBlock(handle));
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000185CC File Offset: 0x000167CC
		internal StringHandle GetNextHandle(StringHandle handle)
		{
			if (handle.IsVirtual)
			{
				return default(StringHandle);
			}
			int num = this.Block.IndexOf(0, handle.GetHeapOffset());
			if (num == -1 || num == this.Block.Length - 1)
			{
				return default(StringHandle);
			}
			return StringHandle.FromOffset(num + 1);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00018628 File Offset: 0x00016828
		internal bool Equals(StringHandle handle, string value, MetadataStringDecoder utf8Decoder, bool ignoreCase)
		{
			if (handle.IsVirtual)
			{
				return string.Equals(this.GetString(handle, utf8Decoder), value, ignoreCase ? 5 : 4);
			}
			if (handle.IsNil)
			{
				return value.Length == 0;
			}
			char c = ((handle.StringKind == StringKind.DotTerminated) ? '.' : '\0');
			return this.Block.Utf8NullTerminatedEquals(handle.GetHeapOffset(), value, utf8Decoder, c, ignoreCase);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00018690 File Offset: 0x00016890
		internal bool StartsWith(StringHandle handle, string value, MetadataStringDecoder utf8Decoder, bool ignoreCase)
		{
			if (handle.IsVirtual)
			{
				return this.GetString(handle, utf8Decoder).StartsWith(value, ignoreCase ? 5 : 4);
			}
			if (handle.IsNil)
			{
				return value.Length == 0;
			}
			char c = ((handle.StringKind == StringKind.DotTerminated) ? '.' : '\0');
			return this.Block.Utf8NullTerminatedStartsWith(handle.GetHeapOffset(), value, utf8Decoder, c, ignoreCase);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000186F7 File Offset: 0x000168F7
		internal bool EqualsRaw(StringHandle rawHandle, string asciiString)
		{
			return this.Block.CompareUtf8NullTerminatedStringWithAsciiString(rawHandle.GetHeapOffset(), asciiString) == 0;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001870F File Offset: 0x0001690F
		internal int IndexOfRaw(int startIndex, char asciiChar)
		{
			return this.Block.Utf8NullTerminatedOffsetOfAsciiChar(startIndex, asciiChar);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001871E File Offset: 0x0001691E
		internal bool StartsWithRaw(StringHandle rawHandle, string asciiPrefix)
		{
			return this.Block.Utf8NullTerminatedStringStartsWithAsciiPrefix(rawHandle.GetHeapOffset(), asciiPrefix);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00018733 File Offset: 0x00016933
		internal int BinarySearchRaw(string[] asciiKeys, StringHandle rawHandle)
		{
			return this.Block.BinarySearch(asciiKeys, rawHandle.GetHeapOffset());
		}

		// Token: 0x04000543 RID: 1347
		private static string[] s_virtualValues;

		// Token: 0x04000544 RID: 1348
		internal readonly MemoryBlock Block;

		// Token: 0x04000545 RID: 1349
		private VirtualHeap _lazyVirtualHeap;
	}
}

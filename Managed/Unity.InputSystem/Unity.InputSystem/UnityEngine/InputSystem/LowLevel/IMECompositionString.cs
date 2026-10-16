using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E5 RID: 229
	[StructLayout(LayoutKind.Explicit, Size = 132)]
	public struct IMECompositionString : IEnumerable<char>, IEnumerable
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x000459E8 File Offset: 0x00043BE8
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x170003E5 RID: 997
		public unsafe char this[int index]
		{
			get
			{
				if (index >= this.Count || index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				fixed (char* ptr = &this.buffer.FixedElementField)
				{
					return ptr[index];
				}
			}
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00045A2C File Offset: 0x00043C2C
		public unsafe IMECompositionString(string characters)
		{
			if (string.IsNullOrEmpty(characters))
			{
				this.size = 0;
				return;
			}
			this.size = characters.Length;
			for (int i = 0; i < this.size; i++)
			{
				*((ref this.buffer.FixedElementField) + (IntPtr)i * 2) = characters[i];
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00045A80 File Offset: 0x00043C80
		public unsafe override string ToString()
		{
			fixed (char* ptr = &this.buffer.FixedElementField)
			{
				return new string(ptr, 0, this.size);
			}
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00045AA7 File Offset: 0x00043CA7
		public IEnumerator<char> GetEnumerator()
		{
			return new IMECompositionString.Enumerator(this);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00045AB9 File Offset: 0x00043CB9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040005B4 RID: 1460
		[FieldOffset(0)]
		private int size;

		// Token: 0x040005B5 RID: 1461
		[FixedBuffer(typeof(char), 64)]
		[FieldOffset(4)]
		private IMECompositionString.<buffer>e__FixedBuffer buffer;

		// Token: 0x02000214 RID: 532
		internal struct Enumerator : IEnumerator<char>, IEnumerator, IDisposable
		{
			// Token: 0x0600150F RID: 5391 RVA: 0x00062A27 File Offset: 0x00060C27
			public Enumerator(IMECompositionString compositionString)
			{
				this.m_CompositionString = compositionString;
				this.m_CurrentCharacter = '\0';
				this.m_CurrentIndex = -1;
			}

			// Token: 0x06001510 RID: 5392 RVA: 0x00062A40 File Offset: 0x00060C40
			public unsafe bool MoveNext()
			{
				int count = this.m_CompositionString.Count;
				this.m_CurrentIndex++;
				if (this.m_CurrentIndex == count)
				{
					return false;
				}
				fixed (char* ptr = &this.m_CompositionString.buffer.FixedElementField)
				{
					char* ptr2 = ptr;
					this.m_CurrentCharacter = ptr2[this.m_CurrentIndex];
				}
				return true;
			}

			// Token: 0x06001511 RID: 5393 RVA: 0x00062A9C File Offset: 0x00060C9C
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x06001512 RID: 5394 RVA: 0x00062AA5 File Offset: 0x00060CA5
			public void Dispose()
			{
			}

			// Token: 0x170005AE RID: 1454
			// (get) Token: 0x06001513 RID: 5395 RVA: 0x00062AA7 File Offset: 0x00060CA7
			public char Current
			{
				get
				{
					return this.m_CurrentCharacter;
				}
			}

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x06001514 RID: 5396 RVA: 0x00062AAF File Offset: 0x00060CAF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000BA1 RID: 2977
			private IMECompositionString m_CompositionString;

			// Token: 0x04000BA2 RID: 2978
			private char m_CurrentCharacter;

			// Token: 0x04000BA3 RID: 2979
			private int m_CurrentIndex;
		}

		// Token: 0x02000215 RID: 533
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <buffer>e__FixedBuffer
		{
			// Token: 0x04000BA4 RID: 2980
			public char FixedElementField;
		}
	}
}

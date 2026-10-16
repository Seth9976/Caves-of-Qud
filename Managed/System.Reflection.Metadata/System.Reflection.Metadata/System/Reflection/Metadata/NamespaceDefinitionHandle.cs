using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Provides a handle to a namespace definition.</summary>
	// Token: 0x0200006E RID: 110
	public readonly struct NamespaceDefinitionHandle : IEquatable<NamespaceDefinitionHandle>
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x0000B2CC File Offset: 0x000094CC
		private NamespaceDefinitionHandle(uint value)
		{
			this._value = value;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000B2D5 File Offset: 0x000094D5
		internal static NamespaceDefinitionHandle FromFullNameOffset(int stringHeapOffset)
		{
			return new NamespaceDefinitionHandle((uint)stringHeapOffset);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000B2DD File Offset: 0x000094DD
		internal static NamespaceDefinitionHandle FromVirtualIndex(uint virtualIndex)
		{
			if (!HeapHandleType.IsValidHeapOffset(virtualIndex))
			{
				Throw.TooManySubnamespaces();
			}
			return new NamespaceDefinitionHandle(2147483648U | virtualIndex);
		}

		/// <param name="handle" />
		// Token: 0x06000489 RID: 1161 RVA: 0x0000B2F8 File Offset: 0x000094F8
		public static implicit operator Handle(NamespaceDefinitionHandle handle)
		{
			return new Handle((byte)(((handle._value & 2147483648U) >> 24) | 124U), (int)(handle._value & 536870911U));
		}

		/// <param name="handle" />
		// Token: 0x0600048A RID: 1162 RVA: 0x0000B31E File Offset: 0x0000951E
		public static explicit operator NamespaceDefinitionHandle(Handle handle)
		{
			if ((handle.VType & 127) != 124)
			{
				Throw.InvalidCast();
			}
			return new NamespaceDefinitionHandle((uint)(((int)(handle.VType & 128) << 24) | handle.Offset));
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000B350 File Offset: 0x00009550
		public bool IsNil
		{
			get
			{
				return this._value == 0U;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000B35B File Offset: 0x0000955B
		internal bool IsVirtual
		{
			get
			{
				return (this._value & 2147483648U) > 0U;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000B36C File Offset: 0x0000956C
		internal int GetHeapOffset()
		{
			return (int)(this._value & 536870911U);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000B37A File Offset: 0x0000957A
		internal bool HasFullName
		{
			get
			{
				return !this.IsVirtual;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000B385 File Offset: 0x00009585
		internal StringHandle GetFullName()
		{
			return StringHandle.FromOffset(this.GetHeapOffset());
		}

		/// <param name="obj" />
		// Token: 0x06000490 RID: 1168 RVA: 0x0000B392 File Offset: 0x00009592
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is NamespaceDefinitionHandle && this.Equals((NamespaceDefinitionHandle)obj);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000491 RID: 1169 RVA: 0x0000B3AA File Offset: 0x000095AA
		public bool Equals(NamespaceDefinitionHandle other)
		{
			return this._value == other._value;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000B3BA File Offset: 0x000095BA
		public override int GetHashCode()
		{
			return (int)this._value;
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000493 RID: 1171 RVA: 0x0000B3C2 File Offset: 0x000095C2
		public static bool operator ==(NamespaceDefinitionHandle left, NamespaceDefinitionHandle right)
		{
			return left.Equals(right);
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000494 RID: 1172 RVA: 0x0000B3CC File Offset: 0x000095CC
		public static bool operator !=(NamespaceDefinitionHandle left, NamespaceDefinitionHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000309 RID: 777
		private readonly uint _value;
	}
}

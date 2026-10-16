using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000066 RID: 102
	public readonly struct AssemblyReferenceHandle : IEquatable<AssemblyReferenceHandle>
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x0000AA7A File Offset: 0x00008C7A
		private AssemblyReferenceHandle(uint value)
		{
			this._value = value;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000AA83 File Offset: 0x00008C83
		internal static AssemblyReferenceHandle FromRowId(int rowId)
		{
			return new AssemblyReferenceHandle((uint)rowId);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000AA8B File Offset: 0x00008C8B
		internal static AssemblyReferenceHandle FromVirtualIndex(AssemblyReferenceHandle.VirtualIndex virtualIndex)
		{
			return new AssemblyReferenceHandle((uint)((AssemblyReferenceHandle.VirtualIndex)(-2147483648) | virtualIndex));
		}

		/// <param name="handle" />
		// Token: 0x06000417 RID: 1047 RVA: 0x0000AA99 File Offset: 0x00008C99
		public static implicit operator Handle(AssemblyReferenceHandle handle)
		{
			return Handle.FromVToken(handle.VToken);
		}

		/// <param name="handle" />
		// Token: 0x06000418 RID: 1048 RVA: 0x0000AAA7 File Offset: 0x00008CA7
		public static implicit operator EntityHandle(AssemblyReferenceHandle handle)
		{
			return new EntityHandle(handle.VToken);
		}

		/// <param name="handle" />
		// Token: 0x06000419 RID: 1049 RVA: 0x0000AAB5 File Offset: 0x00008CB5
		public static explicit operator AssemblyReferenceHandle(Handle handle)
		{
			if (handle.Type != 35U)
			{
				Throw.InvalidCast();
			}
			return new AssemblyReferenceHandle(handle.SpecificEntityHandleValue);
		}

		/// <param name="handle" />
		// Token: 0x0600041A RID: 1050 RVA: 0x0000AAD3 File Offset: 0x00008CD3
		public static explicit operator AssemblyReferenceHandle(EntityHandle handle)
		{
			if (handle.Type != 587202560U)
			{
				Throw.InvalidCast();
			}
			return new AssemblyReferenceHandle(handle.SpecificHandleValue);
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		internal uint Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000AAFC File Offset: 0x00008CFC
		private uint VToken
		{
			get
			{
				return this._value | 587202560U;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000AB0A File Offset: 0x00008D0A
		public bool IsNil
		{
			get
			{
				return this._value == 0U;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000AB15 File Offset: 0x00008D15
		internal bool IsVirtual
		{
			get
			{
				return (this._value & 2147483648U) > 0U;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000AB26 File Offset: 0x00008D26
		internal int RowId
		{
			get
			{
				return (int)(this._value & 16777215U);
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000420 RID: 1056 RVA: 0x0000AB34 File Offset: 0x00008D34
		public static bool operator ==(AssemblyReferenceHandle left, AssemblyReferenceHandle right)
		{
			return left._value == right._value;
		}

		/// <param name="obj" />
		// Token: 0x06000421 RID: 1057 RVA: 0x0000AB44 File Offset: 0x00008D44
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is AssemblyReferenceHandle && ((AssemblyReferenceHandle)obj)._value == this._value;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000422 RID: 1058 RVA: 0x0000AB63 File Offset: 0x00008D63
		public bool Equals(AssemblyReferenceHandle other)
		{
			return this._value == other._value;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000AB74 File Offset: 0x00008D74
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000424 RID: 1060 RVA: 0x0000AB8F File Offset: 0x00008D8F
		public static bool operator !=(AssemblyReferenceHandle left, AssemblyReferenceHandle right)
		{
			return left._value != right._value;
		}

		// Token: 0x040002F5 RID: 757
		private const uint tokenType = 587202560U;

		// Token: 0x040002F6 RID: 758
		private const byte tokenTypeSmall = 35;

		// Token: 0x040002F7 RID: 759
		private readonly uint _value;

		// Token: 0x02000198 RID: 408
		internal enum VirtualIndex
		{
			// Token: 0x040009D2 RID: 2514
			System_Runtime,
			// Token: 0x040009D3 RID: 2515
			System_Runtime_InteropServices_WindowsRuntime,
			// Token: 0x040009D4 RID: 2516
			System_ObjectModel,
			// Token: 0x040009D5 RID: 2517
			System_Runtime_WindowsRuntime,
			// Token: 0x040009D6 RID: 2518
			System_Runtime_WindowsRuntime_UI_Xaml,
			// Token: 0x040009D7 RID: 2519
			System_Numerics_Vectors,
			// Token: 0x040009D8 RID: 2520
			Count
		}
	}
}

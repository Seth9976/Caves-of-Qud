using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents any metadata entity (such as a type reference, a type definition, a type specification, a method definition, or a custom attribute) or value (a string, blob, guid, or user string).</summary>
	// Token: 0x0200009F RID: 159
	public readonly struct Handle : IEquatable<Handle>
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x0000D117 File Offset: 0x0000B317
		internal static Handle FromVToken(uint vToken)
		{
			return new Handle((byte)(vToken >> 24), (int)(vToken & 16777215U));
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000D12A File Offset: 0x0000B32A
		internal Handle(byte vType, int value)
		{
			this._vType = vType;
			this._value = value;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000D13A File Offset: 0x0000B33A
		internal int RowId
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000D142 File Offset: 0x0000B342
		internal int Offset
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000D14A File Offset: 0x0000B34A
		internal uint EntityHandleType
		{
			get
			{
				return this.Type << 24;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000D155 File Offset: 0x0000B355
		internal uint Type
		{
			get
			{
				return (uint)(this._vType & 127);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000D160 File Offset: 0x0000B360
		internal uint EntityHandleValue
		{
			get
			{
				return (uint)(((int)this._vType << 24) | this._value);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0000D172 File Offset: 0x0000B372
		internal uint SpecificEntityHandleValue
		{
			get
			{
				return (uint)(((int)(this._vType & 128) << 24) | this._value);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000D18A File Offset: 0x0000B38A
		internal byte VType
		{
			get
			{
				return this._vType;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0000D192 File Offset: 0x0000B392
		internal bool IsVirtual
		{
			get
			{
				return (this._vType & 128) > 0;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000D1A3 File Offset: 0x0000B3A3
		internal bool IsHeapHandle
		{
			get
			{
				return (this._vType & 112) == 112;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		public HandleKind Kind
		{
			get
			{
				uint type = this.Type;
				if ((type & 4294967292U) == 120U)
				{
					return HandleKind.String;
				}
				return (HandleKind)type;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000D1D5 File Offset: 0x0000B3D5
		public bool IsNil
		{
			get
			{
				return (this._value | (int)(this._vType & 128)) == 0;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000D1ED File Offset: 0x0000B3ED
		internal bool IsEntityOrUserStringHandle
		{
			get
			{
				return this.Type <= 112U;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		internal int Token
		{
			get
			{
				return ((int)this._vType << 24) | this._value;
			}
		}

		/// <param name="obj" />
		// Token: 0x060005D2 RID: 1490 RVA: 0x0000D20E File Offset: 0x0000B40E
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is Handle && this.Equals((Handle)obj);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060005D3 RID: 1491 RVA: 0x0000D226 File Offset: 0x0000B426
		public bool Equals(Handle other)
		{
			return this._value == other._value && this._vType == other._vType;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000D246 File Offset: 0x0000B446
		public override int GetHashCode()
		{
			return this._value ^ ((int)this._vType << 24);
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060005D5 RID: 1493 RVA: 0x0000D258 File Offset: 0x0000B458
		public static bool operator ==(Handle left, Handle right)
		{
			return left.Equals(right);
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060005D6 RID: 1494 RVA: 0x0000D262 File Offset: 0x0000B462
		public static bool operator !=(Handle left, Handle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000D270 File Offset: 0x0000B470
		internal static int Compare(Handle left, Handle right)
		{
			return ((long)((ulong)left._value | ((ulong)left._vType << 32))).CompareTo((long)((ulong)right._value | ((ulong)right._vType << 32)));
		}

		// Token: 0x040003B5 RID: 949
		private readonly int _value;

		// Token: 0x040003B6 RID: 950
		private readonly byte _vType;

		// Token: 0x040003B7 RID: 951
		public static readonly ModuleDefinitionHandle ModuleDefinition = new ModuleDefinitionHandle(1);

		// Token: 0x040003B8 RID: 952
		public static readonly AssemblyDefinitionHandle AssemblyDefinition = new AssemblyDefinitionHandle(1);
	}
}

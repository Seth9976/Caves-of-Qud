using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes a Microsoft intermediate language (MSIL) instruction.</summary>
	// Token: 0x0200093A RID: 2362
	[ComVisible(true)]
	public readonly struct OpCode : IEquatable<OpCode>
	{
		// Token: 0x060051EE RID: 20974 RVA: 0x0010054C File Offset: 0x000FE74C
		internal OpCode(int p, int q)
		{
			this.op1 = (byte)(p & 255);
			this.op2 = (byte)((p >> 8) & 255);
			this.push = (byte)((p >> 16) & 255);
			this.pop = (byte)((p >> 24) & 255);
			this.size = (byte)(q & 255);
			this.type = (byte)((q >> 8) & 255);
			this.args = (byte)((q >> 16) & 255);
			this.flow = (byte)((q >> 24) & 255);
		}

		/// <summary>Returns the generated hash code for this Opcode.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x060051EF RID: 20975 RVA: 0x001005D9 File Offset: 0x000FE7D9
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Tests whether the given object is equal to this Opcode.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of Opcode and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this object. </param>
		// Token: 0x060051F0 RID: 20976 RVA: 0x001005E8 File Offset: 0x000FE7E8
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is OpCode))
			{
				return false;
			}
			OpCode opCode = (OpCode)obj;
			return opCode.op1 == this.op1 && opCode.op2 == this.op2;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.OpCode" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to the current instance.</param>
		// Token: 0x060051F1 RID: 20977 RVA: 0x00100627 File Offset: 0x000FE827
		public bool Equals(OpCode obj)
		{
			return obj.op1 == this.op1 && obj.op2 == this.op2;
		}

		/// <summary>Returns this Opcode as a <see cref="T:System.String" />.</summary>
		/// <returns>Returns a <see cref="T:System.String" /> containing the name of this Opcode.</returns>
		// Token: 0x060051F2 RID: 20978 RVA: 0x00100647 File Offset: 0x000FE847
		public override string ToString()
		{
			return this.Name;
		}

		/// <summary>The name of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The name of the MSIL instruction.</returns>
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060051F3 RID: 20979 RVA: 0x0010064F File Offset: 0x000FE84F
		public string Name
		{
			get
			{
				if (this.op1 == 255)
				{
					return OpCodeNames.names[(int)this.op2];
				}
				return OpCodeNames.names[256 + (int)this.op2];
			}
		}

		/// <summary>The size of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The size of the MSIL instruction.</returns>
		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060051F4 RID: 20980 RVA: 0x0010067D File Offset: 0x000FE87D
		public int Size
		{
			get
			{
				return (int)this.size;
			}
		}

		/// <summary>The type of Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The type of Microsoft intermediate language (MSIL) instruction.</returns>
		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060051F5 RID: 20981 RVA: 0x00100685 File Offset: 0x000FE885
		public OpCodeType OpCodeType
		{
			get
			{
				return (OpCodeType)this.type;
			}
		}

		/// <summary>The operand type of an Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The operand type of an MSIL instruction.</returns>
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060051F6 RID: 20982 RVA: 0x0010068D File Offset: 0x000FE88D
		public OperandType OperandType
		{
			get
			{
				return (OperandType)this.args;
			}
		}

		/// <summary>The flow control characteristics of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The type of flow control.</returns>
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060051F7 RID: 20983 RVA: 0x00100695 File Offset: 0x000FE895
		public FlowControl FlowControl
		{
			get
			{
				return (FlowControl)this.flow;
			}
		}

		/// <summary>How the Microsoft intermediate language (MSIL) instruction pops the stack.</summary>
		/// <returns>Read-only. The way the MSIL instruction pops the stack.</returns>
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060051F8 RID: 20984 RVA: 0x0010069D File Offset: 0x000FE89D
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return (StackBehaviour)this.pop;
			}
		}

		/// <summary>How the Microsoft intermediate language (MSIL) instruction pushes operand onto the stack.</summary>
		/// <returns>Read-only. The way the MSIL instruction pushes operand onto the stack.</returns>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060051F9 RID: 20985 RVA: 0x001006A5 File Offset: 0x000FE8A5
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return (StackBehaviour)this.push;
			}
		}

		/// <summary>The value of the immediate operand of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The value of the immediate operand of the MSIL instruction.</returns>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060051FA RID: 20986 RVA: 0x001006AD File Offset: 0x000FE8AD
		public short Value
		{
			get
			{
				if (this.size == 1)
				{
					return (short)this.op2;
				}
				return (short)(((int)this.op1 << 8) | (int)this.op2);
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060051FB RID: 20987 RVA: 0x001006CF File Offset: 0x000FE8CF
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.op1 == b.op1 && a.op2 == b.op2;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060051FC RID: 20988 RVA: 0x001006EF File Offset: 0x000FE8EF
		public static bool operator !=(OpCode a, OpCode b)
		{
			return a.op1 != b.op1 || a.op2 != b.op2;
		}

		// Token: 0x040031FF RID: 12799
		internal readonly byte op1;

		// Token: 0x04003200 RID: 12800
		internal readonly byte op2;

		// Token: 0x04003201 RID: 12801
		private readonly byte push;

		// Token: 0x04003202 RID: 12802
		private readonly byte pop;

		// Token: 0x04003203 RID: 12803
		private readonly byte size;

		// Token: 0x04003204 RID: 12804
		private readonly byte type;

		// Token: 0x04003205 RID: 12805
		private readonly byte args;

		// Token: 0x04003206 RID: 12806
		private readonly byte flow;
	}
}

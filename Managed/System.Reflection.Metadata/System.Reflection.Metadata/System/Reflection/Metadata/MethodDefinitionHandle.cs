using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000056 RID: 86
	public readonly struct MethodDefinitionHandle : IEquatable<MethodDefinitionHandle>
	{
		// Token: 0x06000343 RID: 835 RVA: 0x00009B02 File Offset: 0x00007D02
		private MethodDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00009B0B File Offset: 0x00007D0B
		internal static MethodDefinitionHandle FromRowId(int rowId)
		{
			return new MethodDefinitionHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000345 RID: 837 RVA: 0x00009B13 File Offset: 0x00007D13
		public static implicit operator Handle(MethodDefinitionHandle handle)
		{
			return new Handle(6, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000346 RID: 838 RVA: 0x00009B21 File Offset: 0x00007D21
		public static implicit operator EntityHandle(MethodDefinitionHandle handle)
		{
			return new EntityHandle((uint)(100663296L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000347 RID: 839 RVA: 0x00009B37 File Offset: 0x00007D37
		public static explicit operator MethodDefinitionHandle(Handle handle)
		{
			if (handle.VType != 6)
			{
				Throw.InvalidCast();
			}
			return new MethodDefinitionHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000348 RID: 840 RVA: 0x00009B54 File Offset: 0x00007D54
		public static explicit operator MethodDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 100663296U)
			{
				Throw.InvalidCast();
			}
			return new MethodDefinitionHandle(handle.RowId);
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00009B75 File Offset: 0x00007D75
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00009B80 File Offset: 0x00007D80
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600034B RID: 843 RVA: 0x00009B88 File Offset: 0x00007D88
		public static bool operator ==(MethodDefinitionHandle left, MethodDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x0600034C RID: 844 RVA: 0x00009B98 File Offset: 0x00007D98
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is MethodDefinitionHandle && ((MethodDefinitionHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600034D RID: 845 RVA: 0x00009BB7 File Offset: 0x00007DB7
		public bool Equals(MethodDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600034F RID: 847 RVA: 0x00009BE3 File Offset: 0x00007DE3
		public static bool operator !=(MethodDefinitionHandle left, MethodDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		/// <summary>Returns a handle to a <see cref="T:System.Reflection.Metadata.MethodDebugInformation" /> that corresponds to this handle.</summary>
		/// <returns>A method debug information handle that corresponds to this handle.</returns>
		// Token: 0x06000350 RID: 848 RVA: 0x00009BF6 File Offset: 0x00007DF6
		public MethodDebugInformationHandle ToDebugInformationHandle()
		{
			return MethodDebugInformationHandle.FromRowId(this._rowId);
		}

		// Token: 0x040002C5 RID: 709
		private const uint tokenType = 100663296U;

		// Token: 0x040002C6 RID: 710
		private const byte tokenTypeSmall = 6;

		// Token: 0x040002C7 RID: 711
		private readonly int _rowId;
	}
}

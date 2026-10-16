using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000EB RID: 235
	public readonly struct LabelHandle : IEquatable<LabelHandle>
	{
		/// <summary>1-based id identifying the label within the context of a <see cref="T:System.Reflection.Metadata.Ecma335.ControlFlowBuilder" />.</summary>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00019C58 File Offset: 0x00017E58
		public int Id { get; }

		// Token: 0x06000902 RID: 2306 RVA: 0x00019C60 File Offset: 0x00017E60
		internal LabelHandle(int id)
		{
			this.Id = id;
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00019C69 File Offset: 0x00017E69
		public bool IsNil
		{
			get
			{
				return this.Id == 0;
			}
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000904 RID: 2308 RVA: 0x00019C74 File Offset: 0x00017E74
		public bool Equals(LabelHandle other)
		{
			return this.Id == other.Id;
		}

		/// <param name="obj" />
		// Token: 0x06000905 RID: 2309 RVA: 0x00019C85 File Offset: 0x00017E85
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is LabelHandle && this.Equals((LabelHandle)obj);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00019CA0 File Offset: 0x00017EA0
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000907 RID: 2311 RVA: 0x00019CBB File Offset: 0x00017EBB
		public static bool operator ==(LabelHandle left, LabelHandle right)
		{
			return left.Equals(right);
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000908 RID: 2312 RVA: 0x00019CC5 File Offset: 0x00017EC5
		public static bool operator !=(LabelHandle left, LabelHandle right)
		{
			return !left.Equals(right);
		}
	}
}

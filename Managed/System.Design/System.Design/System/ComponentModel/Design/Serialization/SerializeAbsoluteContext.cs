using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Specifies that serializers should handle default values. This class cannot be inherited.</summary>
	// Token: 0x0200015F RID: 351
	public sealed class SerializeAbsoluteContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.SerializeAbsoluteContext" /> class. </summary>
		// Token: 0x06000A98 RID: 2712 RVA: 0x00002352 File Offset: 0x00000552
		public SerializeAbsoluteContext()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.SerializeAbsoluteContext" /> class with the option of binding to a specific member. </summary>
		/// <param name="member">The member to which this context is bound. Can be null.</param>
		// Token: 0x06000A99 RID: 2713 RVA: 0x000162D3 File Offset: 0x000144D3
		public SerializeAbsoluteContext(MemberDescriptor member)
		{
			this._member = member;
		}

		/// <summary>Gets the member to which this context is bound.</summary>
		/// <returns>The member to which this context is bound, or null if the context is bound to all members of an object.</returns>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x000162E2 File Offset: 0x000144E2
		public MemberDescriptor Member
		{
			get
			{
				return this._member;
			}
		}

		/// <summary>Gets a value indicating whether the given member should be serialized in this context.</summary>
		/// <returns>true if the given member should be serialized in this context; otherwise, false.</returns>
		/// <param name="member">The member to be examined for serialization.</param>
		// Token: 0x06000A9B RID: 2715 RVA: 0x000162EA File Offset: 0x000144EA
		public bool ShouldSerialize(MemberDescriptor member)
		{
			return member == this._member;
		}

		// Token: 0x04000277 RID: 631
		private MemberDescriptor _member;
	}
}

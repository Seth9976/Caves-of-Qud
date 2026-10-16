using System;

namespace System.Web.UI
{
	/// <summary>Specifies that a server control needs a tag name in its constructor.</summary>
	// Token: 0x02000151 RID: 337
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ConstructorNeedsTagAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ConstructorNeedsTagAttribute" /> class.</summary>
		// Token: 0x06000EE1 RID: 3809 RVA: 0x00002C2A File Offset: 0x00000E2A
		public ConstructorNeedsTagAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ConstructorNeedsTagAttribute" /> class.</summary>
		/// <param name="needsTag">true to add a tag to a control; otherwise, false. </param>
		// Token: 0x06000EE2 RID: 3810 RVA: 0x00028627 File Offset: 0x00026827
		public ConstructorNeedsTagAttribute(bool needsTag)
		{
			this.needsTag = needsTag;
		}

		/// <summary>Indicates whether a control needs a tag name in its constructor. This property is read-only.</summary>
		/// <returns>true if the control needs a tag in its constructor. The default is false.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00028636 File Offset: 0x00026836
		public bool NeedsTag
		{
			get
			{
				return this.needsTag;
			}
		}

		// Token: 0x04001225 RID: 4645
		private bool needsTag;
	}
}

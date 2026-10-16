using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004CD RID: 1229
	internal sealed class PropertyWellKnownAttributeData : CommonPropertyWellKnownAttributeData, ISkipLocalsInitAttributeTarget, IMemberNotNullAttributeTarget
	{
		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x0600688A RID: 26762 RVA: 0x0018838C File Offset: 0x0018658C
		// (set) Token: 0x0600688B RID: 26763 RVA: 0x00188394 File Offset: 0x00186594
		public bool HasDisallowNullAttribute
		{
			get
			{
				return this._hasDisallowNullAttribute;
			}
			set
			{
				this._hasDisallowNullAttribute = value;
			}
		}

		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x0600688C RID: 26764 RVA: 0x0018839D File Offset: 0x0018659D
		// (set) Token: 0x0600688D RID: 26765 RVA: 0x001883A5 File Offset: 0x001865A5
		public bool HasAllowNullAttribute
		{
			get
			{
				return this._hasAllowNullAttribute;
			}
			set
			{
				this._hasAllowNullAttribute = value;
			}
		}

		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x0600688E RID: 26766 RVA: 0x001883AE File Offset: 0x001865AE
		// (set) Token: 0x0600688F RID: 26767 RVA: 0x001883B6 File Offset: 0x001865B6
		public bool HasMaybeNullAttribute
		{
			get
			{
				return this._hasMaybeNullAttribute;
			}
			set
			{
				this._hasMaybeNullAttribute = value;
			}
		}

		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x06006890 RID: 26768 RVA: 0x001883BF File Offset: 0x001865BF
		// (set) Token: 0x06006891 RID: 26769 RVA: 0x001883C7 File Offset: 0x001865C7
		public bool HasNotNullAttribute
		{
			get
			{
				return this._hasNotNullAttribute;
			}
			set
			{
				this._hasNotNullAttribute = value;
			}
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x06006892 RID: 26770 RVA: 0x001883D0 File Offset: 0x001865D0
		// (set) Token: 0x06006893 RID: 26771 RVA: 0x001883D8 File Offset: 0x001865D8
		public bool HasSkipLocalsInitAttribute
		{
			get
			{
				return this._hasSkipLocalsInitAttribute;
			}
			set
			{
				this._hasSkipLocalsInitAttribute = value;
			}
		}

		// Token: 0x06006894 RID: 26772 RVA: 0x001883E1 File Offset: 0x001865E1
		public void AddNotNullMember(string memberName)
		{
			this._memberNotNullAttributeData = this._memberNotNullAttributeData.Add(memberName);
		}

		// Token: 0x06006895 RID: 26773 RVA: 0x001883F5 File Offset: 0x001865F5
		public void AddNotNullMember(ArrayBuilder<string> memberNames)
		{
			this._memberNotNullAttributeData = this._memberNotNullAttributeData.AddRange(memberNames);
		}

		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x06006896 RID: 26774 RVA: 0x00188409 File Offset: 0x00186609
		public ImmutableArray<string> NotNullMembers
		{
			get
			{
				return this._memberNotNullAttributeData;
			}
		}

		// Token: 0x06006897 RID: 26775 RVA: 0x00188411 File Offset: 0x00186611
		public void AddNotNullWhenMember(bool sense, string memberName)
		{
			if (sense)
			{
				this._memberNotNullWhenTrueAttributeData = this._memberNotNullWhenTrueAttributeData.Add(memberName);
				return;
			}
			this._memberNotNullWhenFalseAttributeData = this._memberNotNullWhenFalseAttributeData.Add(memberName);
		}

		// Token: 0x06006898 RID: 26776 RVA: 0x0018843B File Offset: 0x0018663B
		public void AddNotNullWhenMember(bool sense, ArrayBuilder<string> memberNames)
		{
			if (sense)
			{
				this._memberNotNullWhenTrueAttributeData = this._memberNotNullWhenTrueAttributeData.AddRange(memberNames);
				return;
			}
			this._memberNotNullWhenFalseAttributeData = this._memberNotNullWhenFalseAttributeData.AddRange(memberNames);
		}

		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x06006899 RID: 26777 RVA: 0x00188465 File Offset: 0x00186665
		public ImmutableArray<string> NotNullWhenTrueMembers
		{
			get
			{
				return this._memberNotNullWhenTrueAttributeData;
			}
		}

		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x0600689A RID: 26778 RVA: 0x0018846D File Offset: 0x0018666D
		public ImmutableArray<string> NotNullWhenFalseMembers
		{
			get
			{
				return this._memberNotNullWhenFalseAttributeData;
			}
		}

		// Token: 0x040019EE RID: 6638
		private bool _hasDisallowNullAttribute;

		// Token: 0x040019EF RID: 6639
		private bool _hasAllowNullAttribute;

		// Token: 0x040019F0 RID: 6640
		private bool _hasMaybeNullAttribute;

		// Token: 0x040019F1 RID: 6641
		private bool _hasNotNullAttribute;

		// Token: 0x040019F2 RID: 6642
		private bool _hasSkipLocalsInitAttribute;

		// Token: 0x040019F3 RID: 6643
		private ImmutableArray<string> _memberNotNullAttributeData = ImmutableArray<string>.Empty;

		// Token: 0x040019F4 RID: 6644
		private ImmutableArray<string> _memberNotNullWhenTrueAttributeData = ImmutableArray<string>.Empty;

		// Token: 0x040019F5 RID: 6645
		private ImmutableArray<string> _memberNotNullWhenFalseAttributeData = ImmutableArray<string>.Empty;
	}
}

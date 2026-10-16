using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004C8 RID: 1224
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class MethodWellKnownAttributeData : CommonMethodWellKnownAttributeData, ISkipLocalsInitAttributeTarget, IMemberNotNullAttributeTarget
	{
		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x06006862 RID: 26722 RVA: 0x001881A3 File Offset: 0x001863A3
		// (set) Token: 0x06006863 RID: 26723 RVA: 0x001881AB File Offset: 0x001863AB
		public bool HasDoesNotReturnAttribute
		{
			get
			{
				return this._hasDoesNotReturnAttribute;
			}
			set
			{
				this._hasDoesNotReturnAttribute = value;
			}
		}

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x06006864 RID: 26724 RVA: 0x001881B4 File Offset: 0x001863B4
		// (set) Token: 0x06006865 RID: 26725 RVA: 0x001881BC File Offset: 0x001863BC
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

		// Token: 0x06006866 RID: 26726 RVA: 0x001881C5 File Offset: 0x001863C5
		public void AddNotNullMember(string memberName)
		{
			this._memberNotNullAttributeData = this._memberNotNullAttributeData.Add(memberName);
		}

		// Token: 0x06006867 RID: 26727 RVA: 0x001881D9 File Offset: 0x001863D9
		public void AddNotNullMember(ArrayBuilder<string> memberNames)
		{
			this._memberNotNullAttributeData = this._memberNotNullAttributeData.AddRange(memberNames);
		}

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x06006868 RID: 26728 RVA: 0x001881ED File Offset: 0x001863ED
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> NotNullMembers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._memberNotNullAttributeData;
			}
		}

		// Token: 0x06006869 RID: 26729 RVA: 0x001881F5 File Offset: 0x001863F5
		public void AddNotNullWhenMember(bool sense, string memberName)
		{
			if (sense)
			{
				this._memberNotNullWhenTrueAttributeData = this._memberNotNullWhenTrueAttributeData.Add(memberName);
				return;
			}
			this._memberNotNullWhenFalseAttributeData = this._memberNotNullWhenFalseAttributeData.Add(memberName);
		}

		// Token: 0x0600686A RID: 26730 RVA: 0x0018821F File Offset: 0x0018641F
		public void AddNotNullWhenMember(bool sense, ArrayBuilder<string> memberNames)
		{
			if (sense)
			{
				this._memberNotNullWhenTrueAttributeData = this._memberNotNullWhenTrueAttributeData.AddRange(memberNames);
				return;
			}
			this._memberNotNullWhenFalseAttributeData = this._memberNotNullWhenFalseAttributeData.AddRange(memberNames);
		}

		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x0600686B RID: 26731 RVA: 0x00188249 File Offset: 0x00186449
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> NotNullWhenTrueMembers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._memberNotNullWhenTrueAttributeData;
			}
		}

		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x0600686C RID: 26732 RVA: 0x00188251 File Offset: 0x00186451
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> NotNullWhenFalseMembers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._memberNotNullWhenFalseAttributeData;
			}
		}

		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x0600686D RID: 26733 RVA: 0x00188259 File Offset: 0x00186459
		// (set) Token: 0x0600686E RID: 26734 RVA: 0x00188261 File Offset: 0x00186461
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public UnmanagedCallersOnlyAttributeData UnmanagedCallersOnlyAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._unmanagedCallersOnlyAttributeData;
			}
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			set
			{
				this._unmanagedCallersOnlyAttributeData = value;
			}
		}

		// Token: 0x040019DD RID: 6621
		private bool _hasDoesNotReturnAttribute;

		// Token: 0x040019DE RID: 6622
		private bool _hasSkipLocalsInitAttribute;

		// Token: 0x040019DF RID: 6623
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<string> _memberNotNullAttributeData = ImmutableArray<string>.Empty;

		// Token: 0x040019E0 RID: 6624
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<string> _memberNotNullWhenTrueAttributeData = ImmutableArray<string>.Empty;

		// Token: 0x040019E1 RID: 6625
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<string> _memberNotNullWhenFalseAttributeData = ImmutableArray<string>.Empty;

		// Token: 0x040019E2 RID: 6626
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private UnmanagedCallersOnlyAttributeData _unmanagedCallersOnlyAttributeData;
	}
}

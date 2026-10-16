using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004CB RID: 1227
	internal sealed class ParameterWellKnownAttributeData : CommonParameterWellKnownAttributeData
	{
		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x06006874 RID: 26740 RVA: 0x001882B4 File Offset: 0x001864B4
		// (set) Token: 0x06006875 RID: 26741 RVA: 0x001882BC File Offset: 0x001864BC
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

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x06006876 RID: 26742 RVA: 0x001882C5 File Offset: 0x001864C5
		// (set) Token: 0x06006877 RID: 26743 RVA: 0x001882CD File Offset: 0x001864CD
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

		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x06006878 RID: 26744 RVA: 0x001882D6 File Offset: 0x001864D6
		// (set) Token: 0x06006879 RID: 26745 RVA: 0x001882DE File Offset: 0x001864DE
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

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x0600687A RID: 26746 RVA: 0x001882E7 File Offset: 0x001864E7
		// (set) Token: 0x0600687B RID: 26747 RVA: 0x001882EF File Offset: 0x001864EF
		public bool? MaybeNullWhenAttribute
		{
			get
			{
				return this._maybeNullWhenAttribute;
			}
			set
			{
				this._maybeNullWhenAttribute = value;
			}
		}

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x0600687C RID: 26748 RVA: 0x001882F8 File Offset: 0x001864F8
		// (set) Token: 0x0600687D RID: 26749 RVA: 0x00188300 File Offset: 0x00186500
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

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x0600687E RID: 26750 RVA: 0x00188309 File Offset: 0x00186509
		// (set) Token: 0x0600687F RID: 26751 RVA: 0x00188311 File Offset: 0x00186511
		public bool? NotNullWhenAttribute
		{
			get
			{
				return this._notNullWhenAttribute;
			}
			set
			{
				this._notNullWhenAttribute = value;
			}
		}

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x06006880 RID: 26752 RVA: 0x0018831A File Offset: 0x0018651A
		// (set) Token: 0x06006881 RID: 26753 RVA: 0x00188322 File Offset: 0x00186522
		public bool? DoesNotReturnIfAttribute
		{
			get
			{
				return this._doesNotReturnIfAttribute;
			}
			set
			{
				this._doesNotReturnIfAttribute = value;
			}
		}

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x06006882 RID: 26754 RVA: 0x0018832B File Offset: 0x0018652B
		// (set) Token: 0x06006883 RID: 26755 RVA: 0x00188333 File Offset: 0x00186533
		public bool HasEnumeratorCancellationAttribute
		{
			get
			{
				return this._hasEnumeratorCancellationAttribute;
			}
			set
			{
				this._hasEnumeratorCancellationAttribute = value;
			}
		}

		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x06006884 RID: 26756 RVA: 0x0018833C File Offset: 0x0018653C
		public ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				return this._notNullIfParameterNotNull;
			}
		}

		// Token: 0x06006885 RID: 26757 RVA: 0x00188344 File Offset: 0x00186544
		public void AddNotNullIfParameterNotNull(string parameterName)
		{
			this._notNullIfParameterNotNull = this._notNullIfParameterNotNull.Add(parameterName);
		}

		// Token: 0x040019E4 RID: 6628
		private bool _hasAllowNullAttribute;

		// Token: 0x040019E5 RID: 6629
		private bool _hasDisallowNullAttribute;

		// Token: 0x040019E6 RID: 6630
		private bool _hasMaybeNullAttribute;

		// Token: 0x040019E7 RID: 6631
		private bool? _maybeNullWhenAttribute;

		// Token: 0x040019E8 RID: 6632
		private bool _hasNotNullAttribute;

		// Token: 0x040019E9 RID: 6633
		private bool? _notNullWhenAttribute;

		// Token: 0x040019EA RID: 6634
		private bool? _doesNotReturnIfAttribute;

		// Token: 0x040019EB RID: 6635
		private bool _hasEnumeratorCancellationAttribute;

		// Token: 0x040019EC RID: 6636
		private ImmutableHashSet<string> _notNullIfParameterNotNull = ImmutableHashSet<string>.Empty;
	}
}

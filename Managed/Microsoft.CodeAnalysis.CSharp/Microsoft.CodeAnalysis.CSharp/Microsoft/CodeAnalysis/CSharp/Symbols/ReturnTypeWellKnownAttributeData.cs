using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004CE RID: 1230
	internal sealed class ReturnTypeWellKnownAttributeData : CommonReturnTypeWellKnownAttributeData
	{
		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x0600689C RID: 26780 RVA: 0x0018849E File Offset: 0x0018669E
		// (set) Token: 0x0600689D RID: 26781 RVA: 0x001884A6 File Offset: 0x001866A6
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

		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x0600689E RID: 26782 RVA: 0x001884AF File Offset: 0x001866AF
		// (set) Token: 0x0600689F RID: 26783 RVA: 0x001884B7 File Offset: 0x001866B7
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

		// Token: 0x17001A67 RID: 6759
		// (get) Token: 0x060068A0 RID: 26784 RVA: 0x001884C0 File Offset: 0x001866C0
		public ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				return this._notNullIfParameterNotNull;
			}
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x001884C8 File Offset: 0x001866C8
		public void AddNotNullIfParameterNotNull(string parameterName)
		{
			this._notNullIfParameterNotNull = this._notNullIfParameterNotNull.Add(parameterName);
		}

		// Token: 0x040019F6 RID: 6646
		private bool _hasMaybeNullAttribute;

		// Token: 0x040019F7 RID: 6647
		private bool _hasNotNullAttribute;

		// Token: 0x040019F8 RID: 6648
		private ImmutableHashSet<string> _notNullIfParameterNotNull = ImmutableHashSet<string>.Empty;
	}
}

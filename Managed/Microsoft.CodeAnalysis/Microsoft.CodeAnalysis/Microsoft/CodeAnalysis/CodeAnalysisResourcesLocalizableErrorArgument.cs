using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200010F RID: 271
	[NullableContext(1)]
	[Nullable(0)]
	internal struct CodeAnalysisResourcesLocalizableErrorArgument : IFormattable
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0001AF50 File Offset: 0x00019150
		internal CodeAnalysisResourcesLocalizableErrorArgument(string targetResourceId)
		{
			this._targetResourceId = targetResourceId;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001AF59 File Offset: 0x00019159
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001AF63 File Offset: 0x00019163
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (this._targetResourceId != null)
			{
				return CodeAnalysisResources.ResourceManager.GetString(this._targetResourceId, formatProvider as CultureInfo) ?? string.Empty;
			}
			return string.Empty;
		}

		// Token: 0x040002A4 RID: 676
		private readonly string _targetResourceId;
	}
}

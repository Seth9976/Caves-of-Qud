using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001EA RID: 490
	internal sealed class AsyncIteratorInfo
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x000ADF84 File Offset: 0x000AC184
		internal FieldSymbol PromiseOfValueOrEndField { get; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x000ADF8C File Offset: 0x000AC18C
		internal FieldSymbol CombinedTokensField { get; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000ADF94 File Offset: 0x000AC194
		internal FieldSymbol CurrentField { get; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x000ADF9C File Offset: 0x000AC19C
		internal FieldSymbol DisposeModeField { get; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x000ADFA4 File Offset: 0x000AC1A4
		internal MethodSymbol SetResultMethod { get; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x000ADFAC File Offset: 0x000AC1AC
		internal MethodSymbol SetExceptionMethod { get; }

		// Token: 0x0600217B RID: 8571 RVA: 0x000ADFB4 File Offset: 0x000AC1B4
		public AsyncIteratorInfo(FieldSymbol promiseOfValueOrEndField, FieldSymbol combinedTokensField, FieldSymbol currentField, FieldSymbol disposeModeField, MethodSymbol setResultMethod, MethodSymbol setExceptionMethod)
		{
			this.PromiseOfValueOrEndField = promiseOfValueOrEndField;
			this.CombinedTokensField = combinedTokensField;
			this.CurrentField = currentField;
			this.DisposeModeField = disposeModeField;
			this.SetResultMethod = setResultMethod;
			this.SetExceptionMethod = setExceptionMethod;
		}
	}
}

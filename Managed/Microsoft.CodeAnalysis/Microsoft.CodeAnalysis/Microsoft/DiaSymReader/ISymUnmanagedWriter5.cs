using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000FB RID: 251
	[InterfaceType(1)]
	[Guid("DCF7780D-BDE9-45DF-ACFE-21731A32000C")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface ISymUnmanagedWriter5
	{
		// Token: 0x0600088C RID: 2188
		ISymUnmanagedDocumentWriter DefineDocument(string url, ref Guid language, ref Guid languageVendor, ref Guid documentType);

		// Token: 0x0600088D RID: 2189
		void SetUserEntryPoint(int entryMethodToken);

		// Token: 0x0600088E RID: 2190
		void OpenMethod(uint methodToken);

		// Token: 0x0600088F RID: 2191
		void CloseMethod();

		// Token: 0x06000890 RID: 2192
		uint OpenScope(int startOffset);

		// Token: 0x06000891 RID: 2193
		void CloseScope(int endOffset);

		// Token: 0x06000892 RID: 2194
		void SetScopeRange(uint scopeID, uint startOffset, uint endOffset);

		// Token: 0x06000893 RID: 2195
		unsafe void DefineLocalVariable(string name, uint attributes, uint sig, byte* signature, uint addrKind, uint addr1, uint addr2, uint startOffset, uint endOffset);

		// Token: 0x06000894 RID: 2196
		void DefineParameter(string name, uint attributes, uint sequence, uint addrKind, uint addr1, uint addr2, uint addr3);

		// Token: 0x06000895 RID: 2197
		unsafe void DefineField(uint parent, string name, uint attributes, uint sig, byte* signature, uint addrKind, uint addr1, uint addr2, uint addr3);

		// Token: 0x06000896 RID: 2198
		unsafe void DefineGlobalVariable(string name, uint attributes, uint sig, byte* signature, uint addrKind, uint addr1, uint addr2, uint addr3);

		// Token: 0x06000897 RID: 2199
		void Close();

		// Token: 0x06000898 RID: 2200
		unsafe void SetSymAttribute(uint parent, string name, int length, byte* data);

		// Token: 0x06000899 RID: 2201
		void OpenNamespace(string name);

		// Token: 0x0600089A RID: 2202
		void CloseNamespace();

		// Token: 0x0600089B RID: 2203
		void UsingNamespace(string fullName);

		// Token: 0x0600089C RID: 2204
		void SetMethodSourceRange(ISymUnmanagedDocumentWriter startDoc, uint startLine, uint startColumn, object endDoc, uint endLine, uint endColumn);

		// Token: 0x0600089D RID: 2205
		void Initialize([MarshalAs(25)] object emitter, string filename, [MarshalAs(25)] object ptrIStream, bool fullBuild);

		// Token: 0x0600089E RID: 2206
		unsafe void GetDebugInfo(ref ImageDebugDirectory debugDirectory, uint dataCount, out uint dataCountPtr, byte* data);

		// Token: 0x0600089F RID: 2207
		void DefineSequencePoints(ISymUnmanagedDocumentWriter document, int count, [MarshalAs(42, SizeParamIndex = 1)] int[] offsets, [MarshalAs(42, SizeParamIndex = 1)] int[] lines, [MarshalAs(42, SizeParamIndex = 1)] int[] columns, [MarshalAs(42, SizeParamIndex = 1)] int[] endLines, [MarshalAs(42, SizeParamIndex = 1)] int[] endColumns);

		// Token: 0x060008A0 RID: 2208
		void RemapToken(uint oldToken, uint newToken);

		// Token: 0x060008A1 RID: 2209
		void Initialize2([MarshalAs(25)] object emitter, string tempfilename, [MarshalAs(25)] object ptrIStream, bool fullBuild, string finalfilename);

		// Token: 0x060008A2 RID: 2210
		unsafe void DefineConstant(string name, object value, uint sig, byte* signature);

		// Token: 0x060008A3 RID: 2211
		void Abort();

		// Token: 0x060008A4 RID: 2212
		void DefineLocalVariable2(string name, int attributes, int localSignatureToken, uint addrKind, int index, uint addr2, uint addr3, uint startOffset, uint endOffset);

		// Token: 0x060008A5 RID: 2213
		void DefineGlobalVariable2(string name, int attributes, int sigToken, uint addrKind, uint addr1, uint addr2, uint addr3);

		// Token: 0x060008A6 RID: 2214
		void DefineConstant2([MarshalAs(21)] string name, VariantStructure value, int constantSignatureToken);

		// Token: 0x060008A7 RID: 2215
		void OpenMethod2(uint methodToken, int sectionIndex, int offsetRelativeOffset);

		// Token: 0x060008A8 RID: 2216
		void Commit();

		// Token: 0x060008A9 RID: 2217
		unsafe void GetDebugInfoWithPadding(ref ImageDebugDirectory debugDirectory, uint dataCount, out uint dataCountPtr, byte* data);

		// Token: 0x060008AA RID: 2218
		void OpenMapTokensToSourceSpans();

		// Token: 0x060008AB RID: 2219
		void CloseMapTokensToSourceSpans();

		// Token: 0x060008AC RID: 2220
		void MapTokenToSourceSpan(int token, ISymUnmanagedDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn);
	}
}

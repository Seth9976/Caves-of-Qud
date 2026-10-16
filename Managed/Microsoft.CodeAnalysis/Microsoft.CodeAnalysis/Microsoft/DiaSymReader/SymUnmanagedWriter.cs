using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.DiaSymReader
{
	// Token: 0x02000101 RID: 257
	internal abstract class SymUnmanagedWriter : IDisposable
	{
		// Token: 0x060008B9 RID: 2233
		public abstract void Dispose();

		// Token: 0x060008BA RID: 2234
		public abstract IEnumerable<ArraySegment<byte>> GetUnderlyingData();

		// Token: 0x060008BB RID: 2235
		public abstract void WriteTo(Stream stream);

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008BC RID: 2236
		// (set) Token: 0x060008BD RID: 2237
		public abstract int DocumentTableCapacity { get; set; }

		// Token: 0x060008BE RID: 2238
		public abstract int DefineDocument(string name, Guid language, Guid vendor, Guid type, Guid algorithmId, ReadOnlySpan<byte> checksum, ReadOnlySpan<byte> source);

		// Token: 0x060008BF RID: 2239
		public abstract void DefineSequencePoints(int documentIndex, int count, int[] offsets, int[] startLines, int[] startColumns, int[] endLines, int[] endColumns);

		// Token: 0x060008C0 RID: 2240
		public abstract void OpenMethod(int methodToken);

		// Token: 0x060008C1 RID: 2241
		public abstract void CloseMethod();

		// Token: 0x060008C2 RID: 2242
		public abstract void OpenScope(int startOffset);

		// Token: 0x060008C3 RID: 2243
		public abstract void CloseScope(int endOffset);

		// Token: 0x060008C4 RID: 2244
		public abstract void DefineLocalVariable(int index, string name, int attributes, int localSignatureToken);

		// Token: 0x060008C5 RID: 2245
		public abstract bool DefineLocalConstant(string name, object value, int constantSignatureToken);

		// Token: 0x060008C6 RID: 2246
		public abstract void UsingNamespace(string importString);

		// Token: 0x060008C7 RID: 2247
		public abstract void SetAsyncInfo(int moveNextMethodToken, int kickoffMethodToken, int catchHandlerOffset, ReadOnlySpan<int> yieldOffsets, ReadOnlySpan<int> resumeOffsets);

		// Token: 0x060008C8 RID: 2248
		public abstract void DefineCustomMetadata(byte[] metadata);

		// Token: 0x060008C9 RID: 2249
		public abstract void SetEntryPoint(int entryMethodToken);

		// Token: 0x060008CA RID: 2250
		public abstract void UpdateSignature(Guid guid, uint stamp, int age);

		// Token: 0x060008CB RID: 2251
		public abstract void GetSignature(out Guid guid, out uint stamp, out int age);

		// Token: 0x060008CC RID: 2252
		public abstract void SetSourceServerData(byte[] data);

		// Token: 0x060008CD RID: 2253
		public abstract void SetSourceLinkData(byte[] data);

		// Token: 0x060008CE RID: 2254
		public abstract void OpenTokensToSourceSpansMap();

		// Token: 0x060008CF RID: 2255
		public abstract void MapTokenToSourceSpan(int token, int documentIndex, int startLine, int startColumn, int endLine, int endColumn);

		// Token: 0x060008D0 RID: 2256
		public abstract void CloseTokensToSourceSpansMap();
	}
}

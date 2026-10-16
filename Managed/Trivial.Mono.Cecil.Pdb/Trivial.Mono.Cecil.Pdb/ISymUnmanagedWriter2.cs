using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Trivial.Mono.Cecil.Cil;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x02000007 RID: 7
	[Guid("0B97726E-9E6D-4f05-9A26-424022093CAA")]
	[InterfaceType(1)]
	[ComImport]
	internal interface ISymUnmanagedWriter2
	{
		// Token: 0x06000078 RID: 120
		void DefineDocument([MarshalAs(21)] [In] string url, [In] ref Guid langauge, [In] ref Guid languageVendor, [In] ref Guid documentType, [MarshalAs(28)] out ISymUnmanagedDocumentWriter pRetVal);

		// Token: 0x06000079 RID: 121
		void SetUserEntryPoint([In] int methodToken);

		// Token: 0x0600007A RID: 122
		void OpenMethod([In] int methodToken);

		// Token: 0x0600007B RID: 123
		void CloseMethod();

		// Token: 0x0600007C RID: 124
		void OpenScope([In] int startOffset, out int pRetVal);

		// Token: 0x0600007D RID: 125
		void CloseScope([In] int endOffset);

		// Token: 0x0600007E RID: 126
		void SetScopeRange_Placeholder();

		// Token: 0x0600007F RID: 127
		void DefineLocalVariable_Placeholder();

		// Token: 0x06000080 RID: 128
		void DefineParameter_Placeholder();

		// Token: 0x06000081 RID: 129
		void DefineField_Placeholder();

		// Token: 0x06000082 RID: 130
		void DefineGlobalVariable_Placeholder();

		// Token: 0x06000083 RID: 131
		void Close();

		// Token: 0x06000084 RID: 132
		void SetSymAttribute(uint parent, string name, uint data, IntPtr signature);

		// Token: 0x06000085 RID: 133
		void OpenNamespace([MarshalAs(21)] [In] string name);

		// Token: 0x06000086 RID: 134
		void CloseNamespace();

		// Token: 0x06000087 RID: 135
		void UsingNamespace([MarshalAs(21)] [In] string fullName);

		// Token: 0x06000088 RID: 136
		void SetMethodSourceRange_Placeholder();

		// Token: 0x06000089 RID: 137
		void Initialize([MarshalAs(25)] [In] object emitter, [MarshalAs(21)] [In] string filename, [In] IStream pIStream, [In] bool fFullBuild);

		// Token: 0x0600008A RID: 138
		void GetDebugInfo(out ImageDebugDirectory pIDD, [In] int cData, out int pcData, [MarshalAs(42, SizeParamIndex = 1)] [In] [Out] byte[] data);

		// Token: 0x0600008B RID: 139
		void DefineSequencePoints([MarshalAs(28)] [In] ISymUnmanagedDocumentWriter document, [In] int spCount, [MarshalAs(42, SizeParamIndex = 1)] [In] int[] offsets, [MarshalAs(42, SizeParamIndex = 1)] [In] int[] lines, [MarshalAs(42, SizeParamIndex = 1)] [In] int[] columns, [MarshalAs(42, SizeParamIndex = 1)] [In] int[] endLines, [MarshalAs(42, SizeParamIndex = 1)] [In] int[] endColumns);

		// Token: 0x0600008C RID: 140
		void RemapToken_Placeholder();

		// Token: 0x0600008D RID: 141
		void Initialize2_Placeholder();

		// Token: 0x0600008E RID: 142
		void DefineConstant_Placeholder();

		// Token: 0x0600008F RID: 143
		void Abort_Placeholder();

		// Token: 0x06000090 RID: 144
		void DefineLocalVariable2([MarshalAs(21)] [In] string name, [In] int attributes, [In] int sigToken, [In] int addrKind, [In] int addr1, [In] int addr2, [In] int addr3, [In] int startOffset, [In] int endOffset);

		// Token: 0x06000091 RID: 145
		void DefineGlobalVariable2_Placeholder();

		// Token: 0x06000092 RID: 146
		void DefineConstant2([MarshalAs(21)] [In] string name, [MarshalAs(27)] [In] object variant, [In] int sigToken);
	}
}

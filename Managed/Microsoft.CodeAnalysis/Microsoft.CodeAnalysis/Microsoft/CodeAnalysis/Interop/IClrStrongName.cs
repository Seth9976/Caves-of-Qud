using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.CodeAnalysis.Interop
{
	// Token: 0x020002CE RID: 718
	[InterfaceType(1)]
	[Guid("9FD93CCF-3280-4391-B3A9-96E1CDE77C8D")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IClrStrongName
	{
		// Token: 0x06001E04 RID: 7684
		void GetHashFromAssemblyFile([MarshalAs(20)] [In] string pszFilePath, [MarshalAs(8)] [In] [Out] ref int piHashAlg, [MarshalAs(42, SizeParamIndex = 4)] [Out] byte[] pbHash, [MarshalAs(8)] [In] int cchHash, [MarshalAs(8)] out int pchHash);

		// Token: 0x06001E05 RID: 7685
		void GetHashFromAssemblyFileW([MarshalAs(21)] [In] string pwzFilePath, [MarshalAs(8)] [In] [Out] ref int piHashAlg, [MarshalAs(42, SizeParamIndex = 4)] [Out] byte[] pbHash, [MarshalAs(8)] [In] int cchHash, [MarshalAs(8)] out int pchHash);

		// Token: 0x06001E06 RID: 7686
		void GetHashFromBlob([In] IntPtr pbBlob, [MarshalAs(8)] [In] int cchBlob, [MarshalAs(8)] [In] [Out] ref int piHashAlg, [MarshalAs(42, SizeParamIndex = 5)] [Out] byte[] pbHash, [MarshalAs(8)] [In] int cchHash, [MarshalAs(8)] out int pchHash);

		// Token: 0x06001E07 RID: 7687
		void GetHashFromFile([MarshalAs(20)] [In] string pszFilePath, [MarshalAs(8)] [In] [Out] ref int piHashAlg, [MarshalAs(42, SizeParamIndex = 4)] [Out] byte[] pbHash, [MarshalAs(8)] [In] int cchHash, [MarshalAs(8)] out int pchHash);

		// Token: 0x06001E08 RID: 7688
		void GetHashFromFileW([MarshalAs(21)] [In] string pwzFilePath, [MarshalAs(8)] [In] [Out] ref int piHashAlg, [MarshalAs(42, SizeParamIndex = 4)] [Out] byte[] pbHash, [MarshalAs(8)] [In] int cchHash, [MarshalAs(8)] out int pchHash);

		// Token: 0x06001E09 RID: 7689
		void GetHashFromHandle([In] IntPtr hFile, [MarshalAs(8)] [In] [Out] ref int piHashAlg, [MarshalAs(42, SizeParamIndex = 4)] [Out] byte[] pbHash, [MarshalAs(8)] [In] int cchHash, [MarshalAs(8)] out int pchHash);

		// Token: 0x06001E0A RID: 7690
		[return: MarshalAs(8)]
		int StrongNameCompareAssemblies([MarshalAs(21)] [In] string pwzAssembly1, [MarshalAs(21)] [In] string pwzAssembly2);

		// Token: 0x06001E0B RID: 7691
		void StrongNameFreeBuffer([In] IntPtr pbMemory);

		// Token: 0x06001E0C RID: 7692
		void StrongNameGetBlob([MarshalAs(21)] [In] string pwzFilePath, [MarshalAs(42, SizeParamIndex = 2)] [Out] byte[] pbBlob, [MarshalAs(8)] [In] [Out] ref int pcbBlob);

		// Token: 0x06001E0D RID: 7693
		void StrongNameGetBlobFromImage([In] IntPtr pbBase, [MarshalAs(8)] [In] int dwLength, [MarshalAs(42, SizeParamIndex = 3)] [Out] byte[] pbBlob, [MarshalAs(8)] [In] [Out] ref int pcbBlob);

		// Token: 0x06001E0E RID: 7694
		void StrongNameGetPublicKey([MarshalAs(21)] [In] string pwzKeyContainer, [In] IntPtr pbKeyBlob, [MarshalAs(8)] [In] int cbKeyBlob, out IntPtr ppbPublicKeyBlob, [MarshalAs(8)] out int pcbPublicKeyBlob);

		// Token: 0x06001E0F RID: 7695
		[return: MarshalAs(8)]
		int StrongNameHashSize([MarshalAs(8)] [In] int ulHashAlg);

		// Token: 0x06001E10 RID: 7696
		void StrongNameKeyDelete([MarshalAs(21)] [In] string pwzKeyContainer);

		// Token: 0x06001E11 RID: 7697
		void StrongNameKeyGen([MarshalAs(21)] [In] string pwzKeyContainer, [MarshalAs(8)] [In] int dwFlags, out IntPtr ppbKeyBlob, [MarshalAs(8)] out int pcbKeyBlob);

		// Token: 0x06001E12 RID: 7698
		void StrongNameKeyGenEx([MarshalAs(21)] [In] string pwzKeyContainer, [MarshalAs(8)] [In] int dwFlags, [MarshalAs(8)] [In] int dwKeySize, out IntPtr ppbKeyBlob, [MarshalAs(8)] out int pcbKeyBlob);

		// Token: 0x06001E13 RID: 7699
		void StrongNameKeyInstall([MarshalAs(21)] [In] string pwzKeyContainer, [In] IntPtr pbKeyBlob, [MarshalAs(8)] [In] int cbKeyBlob);

		// Token: 0x06001E14 RID: 7700
		void StrongNameSignatureGeneration([MarshalAs(21)] [In] string pwzFilePath, [MarshalAs(21)] [In] string pwzKeyContainer, [In] IntPtr pbKeyBlob, [MarshalAs(8)] [In] int cbKeyBlob, [MarshalAs(42, SizeParamIndex = 5)] [Out] byte[] ppbSignatureBlob, [MarshalAs(8)] out int pcbSignatureBlob);

		// Token: 0x06001E15 RID: 7701
		void StrongNameSignatureGenerationEx([MarshalAs(21)] [In] string wszFilePath, [MarshalAs(21)] [In] string wszKeyContainer, [In] IntPtr pbKeyBlob, [MarshalAs(8)] [In] int cbKeyBlob, out IntPtr ppbSignatureBlob, [MarshalAs(8)] out int pcbSignatureBlob, [MarshalAs(8)] [In] int dwFlags);

		// Token: 0x06001E16 RID: 7702
		void StrongNameSignatureSize([In] IntPtr pbPublicKeyBlob, [MarshalAs(8)] [In] int cbPublicKeyBlob, [MarshalAs(8)] out int pcbSize);

		// Token: 0x06001E17 RID: 7703
		[return: MarshalAs(8)]
		int StrongNameSignatureVerification([MarshalAs(21)] [In] string pwzFilePath, [MarshalAs(8)] [In] int dwInFlags);

		// Token: 0x06001E18 RID: 7704
		[return: MarshalAs(2)]
		bool StrongNameSignatureVerificationEx([MarshalAs(21)] [In] string pwzFilePath, [MarshalAs(2)] [In] bool fForceVerification, out IntPtr ptr);

		// Token: 0x06001E19 RID: 7705
		[return: MarshalAs(8)]
		int StrongNameSignatureVerificationFromImage([In] IntPtr pbBase, [MarshalAs(8)] [In] int dwLength, [MarshalAs(8)] [In] int dwInFlags);

		// Token: 0x06001E1A RID: 7706
		void StrongNameTokenFromAssembly([MarshalAs(21)] [In] string pwzFilePath, out IntPtr ppbStrongNameToken, [MarshalAs(8)] out int pcbStrongNameToken);

		// Token: 0x06001E1B RID: 7707
		void StrongNameTokenFromAssemblyEx([MarshalAs(21)] [In] string pwzFilePath, out IntPtr ppbStrongNameToken, [MarshalAs(8)] out int pcbStrongNameToken, out IntPtr ppbPublicKeyBlob, [MarshalAs(8)] out int pcbPublicKeyBlob);

		// Token: 0x06001E1C RID: 7708
		void StrongNameTokenFromPublicKey([In] IntPtr pbPublicKeyBlob, [MarshalAs(8)] [In] int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, [MarshalAs(8)] out int pcbStrongNameToken);
	}
}

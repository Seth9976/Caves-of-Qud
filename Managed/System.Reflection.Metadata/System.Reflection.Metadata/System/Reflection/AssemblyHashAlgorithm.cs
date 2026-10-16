using System;

namespace System.Reflection
{
	/// <summary>Specifies the hash algorithms used for hashing assembly files and for generating the strong name.</summary>
	// Token: 0x0200001B RID: 27
	public enum AssemblyHashAlgorithm
	{
		/// <summary>
		///   <para>A mask indicating that there is no hash algorithm.</para>
		///   <para>If you specify <see cref="F:System.Reflection.AssemblyHashAlgorithm.None" /> for a multi-module assembly, the common language runtime defaults to the SHA1 algorithm, since multi-module assemblies need to generate a hash.</para>
		/// </summary>
		// Token: 0x0400003A RID: 58
		None,
		/// <summary>
		///   <para>Retrieves the MD5 message-digest algorithm.</para>
		///   <para>Due to collision problems with MD5, Microsoft recommends SHA256.</para>
		///   <para>MD5 was developed by Rivest in 1991. It is basically MD4 with safety-belts and, while it is slightly slower than MD4, it helps provide more security. The algorithm consists of four distinct rounds, which has a slightly different design from that of MD4. Message-digest size, as well as padding requirements, remain the same.</para>
		/// </summary>
		// Token: 0x0400003B RID: 59
		MD5 = 32771,
		/// <summary>
		///   <para>Retrieves a revision of the Secure Hash Algorithm that corrects an unpublished flaw in SHA.</para>
		///   <para>Due to collision problems with SHA1, Microsoft recommends SHA256.</para>
		/// </summary>
		// Token: 0x0400003C RID: 60
		Sha1,
		/// <summary>Retrieves a version of the Secure Hash Algorithm with a hash size of 256 bits.</summary>
		// Token: 0x0400003D RID: 61
		Sha256 = 32780,
		/// <summary>Retrieves a version of the Secure Hash Algorithm with a hash size of 384 bits.</summary>
		// Token: 0x0400003E RID: 62
		Sha384,
		/// <summary>Retrieves a version of the Secure Hash Algorithm with a hash size of 512 bits.</summary>
		// Token: 0x0400003F RID: 63
		Sha512
	}
}

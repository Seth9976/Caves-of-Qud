using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001FB RID: 507
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class StrongNameKeys
	{
		// Token: 0x0600146B RID: 5227 RVA: 0x000408B0 File Offset: 0x0003EAB0
		private StrongNameKeys()
		{
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x000408B8 File Offset: 0x0003EAB8
		internal StrongNameKeys(Diagnostic diagnostic)
		{
			this.DiagnosticOpt = diagnostic;
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x000408C7 File Offset: 0x0003EAC7
		[NullableContext(0)]
		internal StrongNameKeys(ImmutableArray<byte> keyPair, ImmutableArray<byte> publicKey, RSAParameters? privateKey, [Nullable(2)] string keyContainerName, [Nullable(2)] string keyFilePath, bool hasCounterSignature)
		{
			this.KeyPair = keyPair;
			this.PublicKey = publicKey;
			this.PrivateKey = privateKey;
			this.KeyContainer = keyContainerName;
			this.KeyFilePath = keyFilePath;
			this.HasCounterSignature = hasCounterSignature;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x000408FC File Offset: 0x0003EAFC
		internal static StrongNameKeys Create([Nullable(0)] ImmutableArray<byte> publicKey, RSAParameters? privateKey, bool hasCounterSignature, CommonMessageProvider messageProvider)
		{
			if (MetadataHelpers.IsValidPublicKey(publicKey))
			{
				return new StrongNameKeys(default(ImmutableArray<byte>), publicKey, privateKey, null, null, hasCounterSignature);
			}
			return new StrongNameKeys(messageProvider.CreateDiagnostic(messageProvider.ERR_BadCompilationOptionValue, Location.None, new object[]
			{
				"CryptoPublicKey",
				BitConverter.ToString(publicKey.ToArray<byte>())
			}));
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00040958 File Offset: 0x0003EB58
		internal static StrongNameKeys Create([Nullable(2)] string keyFilePath, CommonMessageProvider messageProvider)
		{
			if (string.IsNullOrEmpty(keyFilePath))
			{
				return StrongNameKeys.None;
			}
			StrongNameKeys strongNameKeys;
			try
			{
				strongNameKeys = StrongNameKeys.CreateHelper(ImmutableArray.Create<byte>(File.ReadAllBytes(keyFilePath)), keyFilePath, false);
			}
			catch (IOException ex)
			{
				strongNameKeys = new StrongNameKeys(StrongNameKeys.GetKeyFileError(messageProvider, keyFilePath, ex.Message));
			}
			return strongNameKeys;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x000409B0 File Offset: 0x0003EBB0
		internal static StrongNameKeys CreateHelper([Nullable(0)] ImmutableArray<byte> keyFileContent, string keyFilePath, bool hasCounterSignature)
		{
			RSAParameters? rsaparameters = default(RSAParameters?);
			Tuple<ImmutableArray<byte>, ImmutableArray<byte>, RSAParameters?> tuple = StrongNameKeys.s_lastSeenKeyPair;
			ImmutableArray<byte> immutableArray;
			ImmutableArray<byte> immutableArray2;
			if (tuple != null && keyFileContent == tuple.Item1)
			{
				immutableArray = tuple.Item1;
				immutableArray2 = tuple.Item2;
				rsaparameters = tuple.Item3;
			}
			else
			{
				if (MetadataHelpers.IsValidPublicKey(keyFileContent))
				{
					immutableArray2 = keyFileContent;
					immutableArray = default(ImmutableArray<byte>);
				}
				else
				{
					if (!CryptoBlobParser.TryParseKey(keyFileContent, out immutableArray2, out rsaparameters))
					{
						throw new IOException(CodeAnalysisResources.InvalidPublicKey);
					}
					immutableArray = keyFileContent;
				}
				tuple = new Tuple<ImmutableArray<byte>, ImmutableArray<byte>, RSAParameters?>(immutableArray, immutableArray2, rsaparameters);
				Interlocked.Exchange<Tuple<ImmutableArray<byte>, ImmutableArray<byte>, RSAParameters?>>(ref StrongNameKeys.s_lastSeenKeyPair, tuple);
			}
			return new StrongNameKeys(immutableArray, immutableArray2, rsaparameters, null, keyFilePath, hasCounterSignature);
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00040A44 File Offset: 0x0003EC44
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static StrongNameKeys Create(StrongNameProvider providerOpt, string keyFilePath, string keyContainerName, bool hasCounterSignature, [Nullable(1)] CommonMessageProvider messageProvider)
		{
			if (string.IsNullOrEmpty(keyFilePath) && string.IsNullOrEmpty(keyContainerName))
			{
				return StrongNameKeys.None;
			}
			if (providerOpt == null)
			{
				return new StrongNameKeys(StrongNameKeys.GetError(keyFilePath, keyContainerName, new CodeAnalysisResourcesLocalizableErrorArgument("AssemblySigningNotSupported"), messageProvider));
			}
			return providerOpt.CreateKeys(keyFilePath, keyContainerName, hasCounterSignature, messageProvider);
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00040A94 File Offset: 0x0003EC94
		internal bool CanSign
		{
			get
			{
				return !this.KeyPair.IsDefault || this.KeyContainer != null;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00040ABC File Offset: 0x0003ECBC
		internal bool CanProvideStrongName
		{
			get
			{
				return this.CanSign || !this.PublicKey.IsDefault;
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00040AE4 File Offset: 0x0003ECE4
		internal static Diagnostic GetError([Nullable(2)] string keyFilePath, [Nullable(2)] string keyContainerName, object message, CommonMessageProvider messageProvider)
		{
			if (keyContainerName != null)
			{
				return StrongNameKeys.GetContainerError(messageProvider, keyContainerName, message);
			}
			return StrongNameKeys.GetKeyFileError(messageProvider, keyFilePath, message);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00040AFA File Offset: 0x0003ECFA
		internal static Diagnostic GetContainerError(CommonMessageProvider messageProvider, string name, object message)
		{
			return messageProvider.CreateDiagnostic(messageProvider.ERR_PublicKeyContainerFailure, Location.None, new object[] { name, message });
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00040B1B File Offset: 0x0003ED1B
		internal static Diagnostic GetKeyFileError(CommonMessageProvider messageProvider, string path, object message)
		{
			return messageProvider.CreateDiagnostic(messageProvider.ERR_PublicKeyFileFailure, Location.None, new object[] { path, message });
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00040B3C File Offset: 0x0003ED3C
		[NullableContext(2)]
		internal static bool IsValidPublicKeyString(string publicKey)
		{
			if (string.IsNullOrEmpty(publicKey) || publicKey.Length % 2 != 0)
			{
				return false;
			}
			for (int i = 0; i < publicKey.Length; i++)
			{
				char c = publicKey.get_Chars(i);
				if ((c < '0' || c > '9') && (c < 'a' || c > 'f') && (c < 'A' || c > 'F'))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040006F3 RID: 1779
		[Nullable(0)]
		internal readonly ImmutableArray<byte> KeyPair;

		// Token: 0x040006F4 RID: 1780
		[Nullable(0)]
		internal readonly ImmutableArray<byte> PublicKey;

		// Token: 0x040006F5 RID: 1781
		internal readonly RSAParameters? PrivateKey;

		// Token: 0x040006F6 RID: 1782
		[Nullable(2)]
		internal readonly Diagnostic DiagnosticOpt;

		// Token: 0x040006F7 RID: 1783
		[Nullable(2)]
		internal readonly string KeyContainer;

		// Token: 0x040006F8 RID: 1784
		[Nullable(2)]
		internal readonly string KeyFilePath;

		// Token: 0x040006F9 RID: 1785
		internal readonly bool HasCounterSignature;

		// Token: 0x040006FA RID: 1786
		internal static readonly StrongNameKeys None = new StrongNameKeys();

		// Token: 0x040006FB RID: 1787
		[Nullable(new byte[] { 2, 0, 0 })]
		private static Tuple<ImmutableArray<byte>, ImmutableArray<byte>, RSAParameters?> s_lastSeenKeyPair;
	}
}

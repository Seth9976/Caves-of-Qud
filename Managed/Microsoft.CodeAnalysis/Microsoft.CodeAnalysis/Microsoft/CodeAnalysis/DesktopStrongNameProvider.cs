using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Interop;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001F9 RID: 505
	[NullableContext(1)]
	[Nullable(0)]
	public class DesktopStrongNameProvider : StrongNameProvider
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x000403D3 File Offset: 0x0003E5D3
		internal override StrongNameFileSystem FileSystem { get; }

		// Token: 0x06001457 RID: 5207 RVA: 0x000403DB File Offset: 0x0003E5DB
		public DesktopStrongNameProvider([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> keyFileSearchPaths)
			: this(keyFileSearchPaths, StrongNameFileSystem.Instance)
		{
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x000403E9 File Offset: 0x0003E5E9
		[NullableContext(2)]
		public DesktopStrongNameProvider([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> keyFileSearchPaths = default(ImmutableArray<string>), string tempPath = null)
			: this(keyFileSearchPaths, (tempPath == null) ? StrongNameFileSystem.Instance : new StrongNameFileSystem(tempPath))
		{
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00040404 File Offset: 0x0003E604
		internal DesktopStrongNameProvider([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> keyFileSearchPaths, StrongNameFileSystem strongNameFileSystem)
		{
			if (!keyFileSearchPaths.IsDefault)
			{
				if (keyFileSearchPaths.Any<string>((string path) => !PathUtilities.IsAbsolute(path)))
				{
					throw new ArgumentException(CodeAnalysisResources.AbsolutePathExpected, "keyFileSearchPaths");
				}
			}
			this.FileSystem = strongNameFileSystem ?? StrongNameFileSystem.Instance;
			this._keyFileSearchPaths = keyFileSearchPaths.NullToEmpty<string>();
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00040474 File Offset: 0x0003E674
		internal override StrongNameKeys CreateKeys([Nullable(2)] string keyFilePath, [Nullable(2)] string keyContainerName, bool hasCounterSignature, CommonMessageProvider messageProvider)
		{
			ImmutableArray<byte> immutableArray = default(ImmutableArray<byte>);
			ImmutableArray<byte> immutableArray2 = default(ImmutableArray<byte>);
			string text = null;
			if (!string.IsNullOrEmpty(keyFilePath))
			{
				try
				{
					string text2 = DesktopStrongNameProvider.ResolveStrongNameKeyFile(keyFilePath, this.FileSystem, this._keyFileSearchPaths);
					if (text2 == null)
					{
						return new StrongNameKeys(StrongNameKeys.GetKeyFileError(messageProvider, keyFilePath, CodeAnalysisResources.FileNotFound));
					}
					return StrongNameKeys.CreateHelper(ImmutableArray.Create<byte>(this.FileSystem.ReadAllBytes(text2)), keyFilePath, hasCounterSignature);
				}
				catch (Exception ex)
				{
					return new StrongNameKeys(StrongNameKeys.GetKeyFileError(messageProvider, keyFilePath, ex.Message));
				}
			}
			if (!string.IsNullOrEmpty(keyContainerName))
			{
				try
				{
					this.ReadKeysFromContainer(keyContainerName, out immutableArray2);
					text = keyContainerName;
				}
				catch (DesktopStrongNameProvider.ClrStrongNameMissingException)
				{
					return new StrongNameKeys(StrongNameKeys.GetContainerError(messageProvider, keyContainerName, new CodeAnalysisResourcesLocalizableErrorArgument("AssemblySigningNotSupported")));
				}
				catch (Exception ex2)
				{
					return new StrongNameKeys(StrongNameKeys.GetContainerError(messageProvider, keyContainerName, ex2.Message));
				}
			}
			return new StrongNameKeys(immutableArray, immutableArray2, default(RSAParameters?), text, keyFilePath, hasCounterSignature);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00040590 File Offset: 0x0003E790
		[return: Nullable(2)]
		internal static string ResolveStrongNameKeyFile(string path, StrongNameFileSystem fileSystem, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> keyFileSearchPaths)
		{
			if (!PathUtilities.IsAbsolute(path))
			{
				foreach (string text in keyFileSearchPaths)
				{
					string text2 = PathUtilities.CombineAbsoluteAndRelativePaths(text, path);
					if (fileSystem.FileExists(text2))
					{
						return FileUtilities.TryNormalizeAbsolutePath(text2);
					}
				}
				return null;
			}
			if (fileSystem.FileExists(path))
			{
				return FileUtilities.TryNormalizeAbsolutePath(path);
			}
			return path;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000405EC File Offset: 0x0003E7EC
		[NullableContext(0)]
		internal virtual void ReadKeysFromContainer([Nullable(1)] string keyContainer, out ImmutableArray<byte> publicKey)
		{
			try
			{
				publicKey = this.GetPublicKey(keyContainer);
			}
			catch (DesktopStrongNameProvider.ClrStrongNameMissingException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IOException(ex.Message);
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00040634 File Offset: 0x0003E834
		internal override void SignFile(StrongNameKeys keys, string filePath)
		{
			if (!string.IsNullOrEmpty(keys.KeyFilePath))
			{
				this.Sign(filePath, keys.KeyPair);
				return;
			}
			this.Sign(filePath, keys.KeyContainer);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00040660 File Offset: 0x0003E860
		internal override void SignBuilder(ExtendedPEBuilder peBuilder, BlobBuilder peBlob, RSAParameters privateKey)
		{
			peBuilder.Sign(peBlob, (IEnumerable<Blob> content) => SigningUtilities.CalculateRsaSignature(content, privateKey));
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00040690 File Offset: 0x0003E890
		internal virtual IClrStrongName GetStrongNameInterface()
		{
			IClrStrongName instance;
			try
			{
				instance = ClrStrongName.GetInstance();
			}
			catch (MarshalDirectiveException obj) when (PathUtilities.IsUnixLikePlatform)
			{
				throw new DesktopStrongNameProvider.ClrStrongNameMissingException();
			}
			return instance;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x000406D4 File Offset: 0x0003E8D4
		[NullableContext(0)]
		internal ImmutableArray<byte> GetPublicKey([Nullable(1)] string keyContainer)
		{
			IClrStrongName strongNameInterface = this.GetStrongNameInterface();
			IntPtr intPtr;
			int num;
			strongNameInterface.StrongNameGetPublicKey(keyContainer, (IntPtr)0, 0, out intPtr, out num);
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			strongNameInterface.StrongNameFreeBuffer(intPtr);
			return array.AsImmutableOrNull<byte>();
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00040714 File Offset: 0x0003E914
		private void Sign(string filePath, string keyName)
		{
			try
			{
				int num;
				this.GetStrongNameInterface().StrongNameSignatureGeneration(filePath, keyName, IntPtr.Zero, 0, null, out num);
			}
			catch (DesktopStrongNameProvider.ClrStrongNameMissingException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IOException(ex.Message, ex);
			}
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00040768 File Offset: 0x0003E968
		[NullableContext(0)]
		private unsafe void Sign([Nullable(1)] string filePath, ImmutableArray<byte> keyPair)
		{
			try
			{
				IClrStrongName strongNameInterface = this.GetStrongNameInterface();
				try
				{
					byte[] array;
					byte* ptr;
					if ((array = keyPair.ToArray<byte>()) == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					int num;
					strongNameInterface.StrongNameSignatureGeneration(filePath, null, (IntPtr)((void*)ptr), keyPair.Length, null, out num);
				}
				finally
				{
					byte[] array = null;
				}
			}
			catch (DesktopStrongNameProvider.ClrStrongNameMissingException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IOException(ex.Message, ex);
			}
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x000407F4 File Offset: 0x0003E9F4
		public override int GetHashCode()
		{
			return Hash.CombineValues(this._keyFileSearchPaths, StringComparer.Ordinal, int.MaxValue);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00040810 File Offset: 0x0003EA10
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			DesktopStrongNameProvider desktopStrongNameProvider = (DesktopStrongNameProvider)obj;
			return this.FileSystem == desktopStrongNameProvider.FileSystem && this._keyFileSearchPaths.SequenceEqual<string, string>(desktopStrongNameProvider._keyFileSearchPaths, StringComparer.Ordinal);
		}

		// Token: 0x040006EF RID: 1775
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<string> _keyFileSearchPaths;

		// Token: 0x020005E6 RID: 1510
		[NullableContext(0)]
		internal sealed class ClrStrongNameMissingException : Exception
		{
		}
	}
}

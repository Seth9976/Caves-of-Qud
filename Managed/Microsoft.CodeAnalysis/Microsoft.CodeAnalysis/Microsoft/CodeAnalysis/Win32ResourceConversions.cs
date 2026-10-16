using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000162 RID: 354
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Win32ResourceConversions
	{
		// Token: 0x06000E4C RID: 3660 RVA: 0x00029074 File Offset: 0x00027274
		internal static void AppendIconToResourceStream(Stream resStream, Stream iconStream)
		{
			BinaryReader binaryReader = new BinaryReader(iconStream);
			if (binaryReader.ReadUInt16() != 0)
			{
				throw new ResourceException(CodeAnalysisResources.IconStreamUnexpectedFormat, null);
			}
			if (binaryReader.ReadUInt16() != 1)
			{
				throw new ResourceException(CodeAnalysisResources.IconStreamUnexpectedFormat, null);
			}
			ushort num = binaryReader.ReadUInt16();
			if (num == 0)
			{
				throw new ResourceException(CodeAnalysisResources.IconStreamUnexpectedFormat, null);
			}
			Win32ResourceConversions.ICONDIRENTRY[] array = new Win32ResourceConversions.ICONDIRENTRY[(int)num];
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				array[(int)num2].bWidth = binaryReader.ReadByte();
				array[(int)num2].bHeight = binaryReader.ReadByte();
				array[(int)num2].bColorCount = binaryReader.ReadByte();
				array[(int)num2].bReserved = binaryReader.ReadByte();
				array[(int)num2].wPlanes = binaryReader.ReadUInt16();
				array[(int)num2].wBitCount = binaryReader.ReadUInt16();
				array[(int)num2].dwBytesInRes = binaryReader.ReadUInt32();
				array[(int)num2].dwImageOffset = binaryReader.ReadUInt32();
			}
			for (ushort num3 = 0; num3 < num; num3 += 1)
			{
				iconStream.Position = (long)((ulong)array[(int)num3].dwImageOffset);
				if (binaryReader.ReadUInt32() == 40U)
				{
					iconStream.Position += 8L;
					array[(int)num3].wPlanes = binaryReader.ReadUInt16();
					array[(int)num3].wBitCount = binaryReader.ReadUInt16();
				}
			}
			BinaryWriter binaryWriter = new BinaryWriter(resStream);
			for (ushort num4 = 0; num4 < num; num4 += 1)
			{
				resStream.Position = (resStream.Position + 3L) & -4L;
				binaryWriter.Write(array[(int)num4].dwBytesInRes);
				binaryWriter.Write(32U);
				binaryWriter.Write(ushort.MaxValue);
				binaryWriter.Write(3);
				binaryWriter.Write(ushort.MaxValue);
				binaryWriter.Write(num4 + 1);
				binaryWriter.Write(0U);
				binaryWriter.Write(4112);
				binaryWriter.Write(0);
				binaryWriter.Write(0U);
				binaryWriter.Write(0U);
				iconStream.Position = (long)((ulong)array[(int)num4].dwImageOffset);
				binaryWriter.Write(binaryReader.ReadBytes(checked((int)array[(int)num4].dwBytesInRes)));
			}
			resStream.Position = (resStream.Position + 3L) & -4L;
			binaryWriter.Write((uint)(6 + num * 14));
			binaryWriter.Write(32U);
			binaryWriter.Write(ushort.MaxValue);
			binaryWriter.Write(14);
			binaryWriter.Write(ushort.MaxValue);
			binaryWriter.Write(32512);
			binaryWriter.Write(0U);
			binaryWriter.Write(4144);
			binaryWriter.Write(0);
			binaryWriter.Write(0U);
			binaryWriter.Write(0U);
			binaryWriter.Write(0);
			binaryWriter.Write(1);
			binaryWriter.Write(num);
			for (ushort num5 = 0; num5 < num; num5 += 1)
			{
				binaryWriter.Write(array[(int)num5].bWidth);
				binaryWriter.Write(array[(int)num5].bHeight);
				binaryWriter.Write(array[(int)num5].bColorCount);
				binaryWriter.Write(array[(int)num5].bReserved);
				binaryWriter.Write(array[(int)num5].wPlanes);
				binaryWriter.Write(array[(int)num5].wBitCount);
				binaryWriter.Write(array[(int)num5].dwBytesInRes);
				binaryWriter.Write(num5 + 1);
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000293E0 File Offset: 0x000275E0
		internal static void AppendVersionToResourceStream(Stream resStream, bool isDll, string fileVersion, string originalFileName, string internalName, string productVersion, Version assemblyVersion, string fileDescription = " ", string legalCopyright = " ", [Nullable(2)] string legalTrademarks = null, [Nullable(2)] string productName = null, [Nullable(2)] string comments = null, [Nullable(2)] string companyName = null)
		{
			BinaryWriter binaryWriter = new BinaryWriter(resStream, Encoding.Unicode);
			resStream.Position = (resStream.Position + 3L) & -4L;
			Win32ResourceConversions.VersionResourceSerializer versionResourceSerializer = new Win32ResourceConversions.VersionResourceSerializer(isDll, comments, companyName, fileDescription, fileVersion, internalName, legalCopyright, legalTrademarks, originalFileName, productName, productVersion, assemblyVersion);
			long position = resStream.Position;
			int dataSize = versionResourceSerializer.GetDataSize();
			binaryWriter.Write((uint)dataSize);
			binaryWriter.Write(32U);
			binaryWriter.Write(ushort.MaxValue);
			binaryWriter.Write(16);
			binaryWriter.Write(ushort.MaxValue);
			binaryWriter.Write(1);
			binaryWriter.Write(0U);
			binaryWriter.Write(48);
			binaryWriter.Write(0);
			binaryWriter.Write(0U);
			binaryWriter.Write(0U);
			versionResourceSerializer.WriteVerResource(binaryWriter);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00029494 File Offset: 0x00027694
		internal static void AppendManifestToResourceStream(Stream resStream, Stream manifestStream, bool isDll)
		{
			resStream.Position = (resStream.Position + 3L) & -4L;
			BinaryWriter binaryWriter = new BinaryWriter(resStream);
			binaryWriter.Write((uint)manifestStream.Length);
			binaryWriter.Write(32U);
			binaryWriter.Write(ushort.MaxValue);
			binaryWriter.Write(24);
			binaryWriter.Write(ushort.MaxValue);
			binaryWriter.Write(isDll ? 2 : 1);
			binaryWriter.Write(0U);
			binaryWriter.Write(4144);
			binaryWriter.Write(0);
			binaryWriter.Write(0U);
			binaryWriter.Write(0U);
			manifestStream.CopyTo(resStream);
		}

		// Token: 0x0200059C RID: 1436
		[NullableContext(0)]
		private struct ICONDIRENTRY
		{
			// Token: 0x0400161D RID: 5661
			internal byte bWidth;

			// Token: 0x0400161E RID: 5662
			internal byte bHeight;

			// Token: 0x0400161F RID: 5663
			internal byte bColorCount;

			// Token: 0x04001620 RID: 5664
			internal byte bReserved;

			// Token: 0x04001621 RID: 5665
			internal ushort wPlanes;

			// Token: 0x04001622 RID: 5666
			internal ushort wBitCount;

			// Token: 0x04001623 RID: 5667
			internal uint dwBytesInRes;

			// Token: 0x04001624 RID: 5668
			internal uint dwImageOffset;
		}

		// Token: 0x0200059D RID: 1437
		[Nullable(0)]
		private class VersionResourceSerializer
		{
			// Token: 0x0600355A RID: 13658 RVA: 0x00095FC8 File Offset: 0x000941C8
			internal VersionResourceSerializer(bool isDll, [Nullable(2)] string comments, [Nullable(2)] string companyName, string fileDescription, string fileVersion, string internalName, string legalCopyright, [Nullable(2)] string legalTrademark, string originalFileName, [Nullable(2)] string productName, string productVersion, Version assemblyVersion)
			{
				this._isDll = isDll;
				this._commentsContents = comments;
				this._companyNameContents = companyName;
				this._fileDescriptionContents = fileDescription;
				this._fileVersionContents = fileVersion;
				this._internalNameContents = internalName;
				this._legalCopyrightContents = legalCopyright;
				this._legalTrademarksContents = legalTrademark;
				this._originalFileNameContents = originalFileName;
				this._productNameContents = productName;
				this._productVersionContents = productVersion;
				this._assemblyVersionContents = assemblyVersion;
				this._langIdAndCodePageKey = string.Format("{0:x4}{1:x4}", 0, 1200U);
			}

			// Token: 0x0600355B RID: 13659 RVA: 0x00096058 File Offset: 0x00094258
			[return: Nullable(new byte[] { 1, 0, 1, 1 })]
			private IEnumerable<KeyValuePair<string, string>> GetVerStrings()
			{
				if (this._commentsContents != null)
				{
					yield return new KeyValuePair<string, string>("Comments", this._commentsContents);
				}
				if (this._companyNameContents != null)
				{
					yield return new KeyValuePair<string, string>("CompanyName", this._companyNameContents);
				}
				if (this._fileDescriptionContents != null)
				{
					yield return new KeyValuePair<string, string>("FileDescription", this._fileDescriptionContents);
				}
				yield return new KeyValuePair<string, string>("FileVersion", this._fileVersionContents);
				if (this._internalNameContents != null)
				{
					yield return new KeyValuePair<string, string>("InternalName", this._internalNameContents);
				}
				if (this._legalCopyrightContents != null)
				{
					yield return new KeyValuePair<string, string>("LegalCopyright", this._legalCopyrightContents);
				}
				if (this._legalTrademarksContents != null)
				{
					yield return new KeyValuePair<string, string>("LegalTrademarks", this._legalTrademarksContents);
				}
				if (this._originalFileNameContents != null)
				{
					yield return new KeyValuePair<string, string>("OriginalFilename", this._originalFileNameContents);
				}
				if (this._productNameContents != null)
				{
					yield return new KeyValuePair<string, string>("ProductName", this._productNameContents);
				}
				yield return new KeyValuePair<string, string>("ProductVersion", this._productVersionContents);
				if (this._assemblyVersionContents != null)
				{
					yield return new KeyValuePair<string, string>("Assembly Version", this._assemblyVersionContents.ToString());
				}
				yield break;
			}

			// Token: 0x17000F2D RID: 3885
			// (get) Token: 0x0600355C RID: 13660 RVA: 0x00096068 File Offset: 0x00094268
			private uint FileType
			{
				get
				{
					if (!this._isDll)
					{
						return 1U;
					}
					return 2U;
				}
			}

			// Token: 0x0600355D RID: 13661 RVA: 0x00096078 File Offset: 0x00094278
			private void WriteVSFixedFileInfo(BinaryWriter writer)
			{
				Version version;
				VersionHelper.TryParse(this._fileVersionContents, out version);
				Version version2;
				VersionHelper.TryParse(this._productVersionContents, out version2);
				writer.Write(4277077181U);
				writer.Write(65536U);
				writer.Write((uint)((version.Major << 16) | version.Minor));
				writer.Write((uint)((version.Build << 16) | version.Revision));
				writer.Write((uint)((version2.Major << 16) | version2.Minor));
				writer.Write((uint)((version2.Build << 16) | version2.Revision));
				writer.Write(63U);
				writer.Write(0U);
				writer.Write(4U);
				writer.Write(this.FileType);
				writer.Write(0U);
				writer.Write(0U);
				writer.Write(0U);
			}

			// Token: 0x0600355E RID: 13662 RVA: 0x00096146 File Offset: 0x00094346
			private static int PadKeyLen(int cb)
			{
				return Win32ResourceConversions.VersionResourceSerializer.PadToDword(cb + 6) - 6;
			}

			// Token: 0x0600355F RID: 13663 RVA: 0x00096152 File Offset: 0x00094352
			private static int PadToDword(int cb)
			{
				return (cb + 3) & -4;
			}

			// Token: 0x06003560 RID: 13664 RVA: 0x0009615C File Offset: 0x0009435C
			private static ushort SizeofVerString(string lpszKey, string lpszValue)
			{
				int num = (lpszKey.Length + 1) * 2;
				int num2 = (lpszValue.Length + 1) * 2;
				return checked((ushort)(Win32ResourceConversions.VersionResourceSerializer.PadKeyLen(num) + num2 + 6));
			}

			// Token: 0x06003561 RID: 13665 RVA: 0x00096188 File Offset: 0x00094388
			private static void WriteVersionString([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<string, string> keyValuePair, BinaryWriter writer)
			{
				ushort num = Win32ResourceConversions.VersionResourceSerializer.SizeofVerString(keyValuePair.Key, keyValuePair.Value);
				int num2 = (keyValuePair.Key.Length + 1) * 2;
				int length = keyValuePair.Value.Length;
				long position = writer.BaseStream.Position;
				writer.Write(num);
				writer.Write((ushort)(keyValuePair.Value.Length + 1));
				writer.Write(1);
				writer.Write(keyValuePair.Key.ToCharArray());
				writer.Write(0);
				writer.Write(new byte[Win32ResourceConversions.VersionResourceSerializer.PadKeyLen(num2) - num2]);
				writer.Write(keyValuePair.Value.ToCharArray());
				writer.Write(0);
			}

			// Token: 0x06003562 RID: 13666 RVA: 0x0009623B File Offset: 0x0009443B
			private static int KEYSIZE(string sz)
			{
				return Win32ResourceConversions.VersionResourceSerializer.PadKeyLen((sz.Length + 1) * 2) / 2;
			}

			// Token: 0x06003563 RID: 13667 RVA: 0x0009624E File Offset: 0x0009444E
			private static int KEYBYTES(string sz)
			{
				return Win32ResourceConversions.VersionResourceSerializer.KEYSIZE(sz) * 2;
			}

			// Token: 0x06003564 RID: 13668 RVA: 0x00096258 File Offset: 0x00094458
			private int GetStringsSize()
			{
				int num = 0;
				foreach (KeyValuePair<string, string> keyValuePair in this.GetVerStrings())
				{
					num = (num + 3) & -4;
					num += (int)Win32ResourceConversions.VersionResourceSerializer.SizeofVerString(keyValuePair.Key, keyValuePair.Value);
				}
				return num;
			}

			// Token: 0x06003565 RID: 13669 RVA: 0x000962C0 File Offset: 0x000944C0
			internal int GetDataSize()
			{
				int num = 34 + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("VS_VERSION_INFO") + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("VarFileInfo") + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("Translation") + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("StringFileInfo") + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES(this._langIdAndCodePageKey) + 52;
				return this.GetStringsSize() + num;
			}

			// Token: 0x06003566 RID: 13670 RVA: 0x00096314 File Offset: 0x00094514
			internal void WriteVerResource(BinaryWriter writer)
			{
				long position = writer.BaseStream.Position;
				int dataSize = this.GetDataSize();
				writer.Write((ushort)dataSize);
				writer.Write(52);
				writer.Write(0);
				writer.Write("VS_VERSION_INFO".ToCharArray());
				writer.Write(new byte[Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("VS_VERSION_INFO") - "VS_VERSION_INFO".Length * 2]);
				this.WriteVSFixedFileInfo(writer);
				writer.Write((ushort)(16 + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("VarFileInfo") + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("Translation")));
				writer.Write(0);
				writer.Write(1);
				writer.Write("VarFileInfo".ToCharArray());
				writer.Write(new byte[Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("VarFileInfo") - "VarFileInfo".Length * 2]);
				writer.Write((ushort)(10 + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("Translation")));
				writer.Write(4);
				writer.Write(0);
				writer.Write("Translation".ToCharArray());
				writer.Write(new byte[Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("Translation") - "Translation".Length * 2]);
				writer.Write(0);
				writer.Write(1200);
				writer.Write((ushort)(12 + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("StringFileInfo") + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES(this._langIdAndCodePageKey) + this.GetStringsSize()));
				writer.Write(0);
				writer.Write(1);
				writer.Write("StringFileInfo".ToCharArray());
				writer.Write(new byte[Win32ResourceConversions.VersionResourceSerializer.KEYBYTES("StringFileInfo") - "StringFileInfo".Length * 2]);
				writer.Write((ushort)(6 + Win32ResourceConversions.VersionResourceSerializer.KEYBYTES(this._langIdAndCodePageKey) + this.GetStringsSize()));
				writer.Write(0);
				writer.Write(1);
				writer.Write(this._langIdAndCodePageKey.ToCharArray());
				writer.Write(new byte[Win32ResourceConversions.VersionResourceSerializer.KEYBYTES(this._langIdAndCodePageKey) - this._langIdAndCodePageKey.Length * 2]);
				long position2 = writer.BaseStream.Position;
				foreach (KeyValuePair<string, string> keyValuePair in this.GetVerStrings())
				{
					long position3 = writer.BaseStream.Position;
					writer.Write(new byte[((position3 + 3L) & -4L) - position3]);
					Win32ResourceConversions.VersionResourceSerializer.WriteVersionString(keyValuePair, writer);
				}
			}

			// Token: 0x04001625 RID: 5669
			[Nullable(2)]
			private readonly string _commentsContents;

			// Token: 0x04001626 RID: 5670
			[Nullable(2)]
			private readonly string _companyNameContents;

			// Token: 0x04001627 RID: 5671
			private readonly string _fileDescriptionContents;

			// Token: 0x04001628 RID: 5672
			private readonly string _fileVersionContents;

			// Token: 0x04001629 RID: 5673
			private readonly string _internalNameContents;

			// Token: 0x0400162A RID: 5674
			private readonly string _legalCopyrightContents;

			// Token: 0x0400162B RID: 5675
			[Nullable(2)]
			private readonly string _legalTrademarksContents;

			// Token: 0x0400162C RID: 5676
			private readonly string _originalFileNameContents;

			// Token: 0x0400162D RID: 5677
			[Nullable(2)]
			private readonly string _productNameContents;

			// Token: 0x0400162E RID: 5678
			private readonly string _productVersionContents;

			// Token: 0x0400162F RID: 5679
			private readonly Version _assemblyVersionContents;

			// Token: 0x04001630 RID: 5680
			private const string vsVersionInfoKey = "VS_VERSION_INFO";

			// Token: 0x04001631 RID: 5681
			private const string varFileInfoKey = "VarFileInfo";

			// Token: 0x04001632 RID: 5682
			private const string translationKey = "Translation";

			// Token: 0x04001633 RID: 5683
			private const string stringFileInfoKey = "StringFileInfo";

			// Token: 0x04001634 RID: 5684
			private readonly string _langIdAndCodePageKey;

			// Token: 0x04001635 RID: 5685
			private const uint CP_WINUNICODE = 1200U;

			// Token: 0x04001636 RID: 5686
			private const ushort sizeVS_FIXEDFILEINFO = 52;

			// Token: 0x04001637 RID: 5687
			private readonly bool _isDll;

			// Token: 0x04001638 RID: 5688
			private const uint VFT_APP = 1U;

			// Token: 0x04001639 RID: 5689
			private const uint VFT_DLL = 2U;

			// Token: 0x0400163A RID: 5690
			private const int HDRSIZE = 6;
		}
	}
}

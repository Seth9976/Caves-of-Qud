using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Reflection.Metadata;

namespace System
{
	// Token: 0x02000009 RID: 9
	internal static class SR
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020A5 File Offset: 0x000002A5
		private static bool UsingResourceKeys()
		{
			return global::System.SR.s_usingResourceKeys;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AC File Offset: 0x000002AC
		[NullableContext(1)]
		internal static string GetResourceString(string resourceKey, [Nullable(2)] string defaultString = null)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return defaultString ?? resourceKey;
			}
			string text = null;
			try
			{
				text = global::System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text))
			{
				return defaultString;
			}
			return text;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020FC File Offset: 0x000002FC
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002125 File Offset: 0x00000325
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] object p1, [Nullable(2)] object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002153 File Offset: 0x00000353
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format([Nullable(1)] string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002186 File Offset: 0x00000386
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B2 File Offset: 0x000003B2
		[NullableContext(1)]
		internal static string Format([Nullable(2)] IFormatProvider provider, string resourceFormat, [Nullable(2)] object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021DC File Offset: 0x000003DC
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format(IFormatProvider provider, [Nullable(1)] string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000220B File Offset: 0x0000040B
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format(IFormatProvider provider, [Nullable(1)] string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002241 File Offset: 0x00000441
		[NullableContext(1)]
		internal static string Format([Nullable(2)] IFormatProvider provider, string resourceFormat, [Nullable(2)] params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(provider, resourceFormat, args);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000226E File Offset: 0x0000046E
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = global::System.SR.s_resourceManager) == null)
				{
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Reflection.Metadata.SR)));
				}
				return resourceManager;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000228E File Offset: 0x0000048E
		internal static string ImageTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("ImageTooSmall", null);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000229B File Offset: 0x0000049B
		internal static string InvalidCorHeaderSize
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidCorHeaderSize", null);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022A8 File Offset: 0x000004A8
		internal static string InvalidHandle
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidHandle", null);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022B5 File Offset: 0x000004B5
		internal static string UnexpectedHandleKind
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedHandleKind", null);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022C2 File Offset: 0x000004C2
		internal static string UnexpectedOpCode
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedOpCode", null);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022CF File Offset: 0x000004CF
		internal static string InvalidLocalSignatureToken
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidLocalSignatureToken", null);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022DC File Offset: 0x000004DC
		internal static string InvalidMetadataSectionSpan
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidMetadataSectionSpan", null);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022E9 File Offset: 0x000004E9
		internal static string InvalidMethodHeader1
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidMethodHeader1", null);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022F6 File Offset: 0x000004F6
		internal static string InvalidMethodHeader2
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidMethodHeader2", null);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002303 File Offset: 0x00000503
		internal static string InvalidPESignature
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidPESignature", null);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002310 File Offset: 0x00000510
		internal static string InvalidSehHeader
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidSehHeader", null);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000231D File Offset: 0x0000051D
		internal static string InvalidToken
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidToken", null);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000232A File Offset: 0x0000052A
		internal static string MetadataImageDoesNotRepresentAnAssembly
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataImageDoesNotRepresentAnAssembly", null);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002337 File Offset: 0x00000537
		internal static string StandaloneDebugMetadataImageDoesNotContainModuleTable
		{
			get
			{
				return global::System.SR.GetResourceString("StandaloneDebugMetadataImageDoesNotContainModuleTable", null);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002344 File Offset: 0x00000544
		internal static string PEImageNotAvailable
		{
			get
			{
				return global::System.SR.GetResourceString("PEImageNotAvailable", null);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002351 File Offset: 0x00000551
		internal static string MissingDataDirectory
		{
			get
			{
				return global::System.SR.GetResourceString("MissingDataDirectory", null);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000235E File Offset: 0x0000055E
		internal static string NotMetadataHeapHandle
		{
			get
			{
				return global::System.SR.GetResourceString("NotMetadataHeapHandle", null);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000236B File Offset: 0x0000056B
		internal static string NotMetadataTableOrUserStringHandle
		{
			get
			{
				return global::System.SR.GetResourceString("NotMetadataTableOrUserStringHandle", null);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002378 File Offset: 0x00000578
		internal static string SectionTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("SectionTooSmall", null);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002385 File Offset: 0x00000585
		internal static string StreamMustSupportReadAndSeek
		{
			get
			{
				return global::System.SR.GetResourceString("StreamMustSupportReadAndSeek", null);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002392 File Offset: 0x00000592
		internal static string UnknownFileFormat
		{
			get
			{
				return global::System.SR.GetResourceString("UnknownFileFormat", null);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000239F File Offset: 0x0000059F
		internal static string UnknownPEMagicValue
		{
			get
			{
				return global::System.SR.GetResourceString("UnknownPEMagicValue", null);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023AC File Offset: 0x000005AC
		internal static string MetadataTableNotSorted
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataTableNotSorted", null);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023B9 File Offset: 0x000005B9
		internal static string ModuleTableInvalidNumberOfRows
		{
			get
			{
				return global::System.SR.GetResourceString("ModuleTableInvalidNumberOfRows", null);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000023C6 File Offset: 0x000005C6
		internal static string UnknownTables
		{
			get
			{
				return global::System.SR.GetResourceString("UnknownTables", null);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000023D3 File Offset: 0x000005D3
		internal static string IllegalTablesInCompressedMetadataStream
		{
			get
			{
				return global::System.SR.GetResourceString("IllegalTablesInCompressedMetadataStream", null);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000023E0 File Offset: 0x000005E0
		internal static string TableRowCountSpaceTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("TableRowCountSpaceTooSmall", null);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000023ED File Offset: 0x000005ED
		internal static string OutOfBoundsRead
		{
			get
			{
				return global::System.SR.GetResourceString("OutOfBoundsRead", null);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000023FA File Offset: 0x000005FA
		internal static string OutOfBoundsWrite
		{
			get
			{
				return global::System.SR.GetResourceString("OutOfBoundsWrite", null);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002407 File Offset: 0x00000607
		internal static string MetadataHeaderTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataHeaderTooSmall", null);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002414 File Offset: 0x00000614
		internal static string MetadataSignature
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataSignature", null);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002421 File Offset: 0x00000621
		internal static string NotEnoughSpaceForVersionString
		{
			get
			{
				return global::System.SR.GetResourceString("NotEnoughSpaceForVersionString", null);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000242E File Offset: 0x0000062E
		internal static string StreamHeaderTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("StreamHeaderTooSmall", null);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000243B File Offset: 0x0000063B
		internal static string NotEnoughSpaceForStreamHeaderName
		{
			get
			{
				return global::System.SR.GetResourceString("NotEnoughSpaceForStreamHeaderName", null);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002448 File Offset: 0x00000648
		internal static string NotEnoughSpaceForStringStream
		{
			get
			{
				return global::System.SR.GetResourceString("NotEnoughSpaceForStringStream", null);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002455 File Offset: 0x00000655
		internal static string NotEnoughSpaceForBlobStream
		{
			get
			{
				return global::System.SR.GetResourceString("NotEnoughSpaceForBlobStream", null);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002462 File Offset: 0x00000662
		internal static string NotEnoughSpaceForGUIDStream
		{
			get
			{
				return global::System.SR.GetResourceString("NotEnoughSpaceForGUIDStream", null);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000246F File Offset: 0x0000066F
		internal static string NotEnoughSpaceForMetadataStream
		{
			get
			{
				return global::System.SR.GetResourceString("NotEnoughSpaceForMetadataStream", null);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000247C File Offset: 0x0000067C
		internal static string InvalidMetadataStreamFormat
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidMetadataStreamFormat", null);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002489 File Offset: 0x00000689
		internal static string MetadataTablesTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataTablesTooSmall", null);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002496 File Offset: 0x00000696
		internal static string MetadataTableHeaderTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataTableHeaderTooSmall", null);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000024A3 File Offset: 0x000006A3
		internal static string WinMDMissingMscorlibRef
		{
			get
			{
				return global::System.SR.GetResourceString("WinMDMissingMscorlibRef", null);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000024B0 File Offset: 0x000006B0
		internal static string UnexpectedStreamEnd
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedStreamEnd", null);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000024BD File Offset: 0x000006BD
		internal static string InvalidMethodRva
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidMethodRva", null);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000024CA File Offset: 0x000006CA
		internal static string CantGetOffsetForVirtualHeapHandle
		{
			get
			{
				return global::System.SR.GetResourceString("CantGetOffsetForVirtualHeapHandle", null);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000024D7 File Offset: 0x000006D7
		internal static string InvalidNumberOfSections
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidNumberOfSections", null);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000024E4 File Offset: 0x000006E4
		internal static string InvalidSignature
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidSignature", null);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000024F1 File Offset: 0x000006F1
		internal static string PEImageDoesNotHaveMetadata
		{
			get
			{
				return global::System.SR.GetResourceString("PEImageDoesNotHaveMetadata", null);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000024FE File Offset: 0x000006FE
		internal static string InvalidCodedIndex
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidCodedIndex", null);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000250B File Offset: 0x0000070B
		internal static string InvalidCompressedInteger
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidCompressedInteger", null);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002518 File Offset: 0x00000718
		internal static string InvalidDocumentName
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidDocumentName", null);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002525 File Offset: 0x00000725
		internal static string RowIdOrHeapOffsetTooLarge
		{
			get
			{
				return global::System.SR.GetResourceString("RowIdOrHeapOffsetTooLarge", null);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002532 File Offset: 0x00000732
		internal static string EnCMapNotSorted
		{
			get
			{
				return global::System.SR.GetResourceString("EnCMapNotSorted", null);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000253F File Offset: 0x0000073F
		internal static string InvalidSerializedString
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidSerializedString", null);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000254C File Offset: 0x0000074C
		internal static string StreamTooLarge
		{
			get
			{
				return global::System.SR.GetResourceString("StreamTooLarge", null);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002559 File Offset: 0x00000759
		internal static string ImageTooSmallOrContainsInvalidOffsetOrCount
		{
			get
			{
				return global::System.SR.GetResourceString("ImageTooSmallOrContainsInvalidOffsetOrCount", null);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002566 File Offset: 0x00000766
		internal static string MetadataStringDecoderEncodingMustBeUtf8
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataStringDecoderEncodingMustBeUtf8", null);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002573 File Offset: 0x00000773
		internal static string InvalidConstantValue
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidConstantValue", null);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002580 File Offset: 0x00000780
		internal static string InvalidConstantValueOfType
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidConstantValueOfType", null);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000258D File Offset: 0x0000078D
		internal static string InvalidImportDefinitionKind
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidImportDefinitionKind", null);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000259A File Offset: 0x0000079A
		internal static string ValueTooLarge
		{
			get
			{
				return global::System.SR.GetResourceString("ValueTooLarge", null);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000025A7 File Offset: 0x000007A7
		internal static string BlobTooLarge
		{
			get
			{
				return global::System.SR.GetResourceString("BlobTooLarge", null);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000025B4 File Offset: 0x000007B4
		internal static string InvalidTypeSize
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidTypeSize", null);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000025C1 File Offset: 0x000007C1
		internal static string HandleBelongsToFutureGeneration
		{
			get
			{
				return global::System.SR.GetResourceString("HandleBelongsToFutureGeneration", null);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000025CE File Offset: 0x000007CE
		internal static string InvalidRowCount
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidRowCount", null);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000025DB File Offset: 0x000007DB
		internal static string InvalidEntryPointToken
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidEntryPointToken", null);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000025E8 File Offset: 0x000007E8
		internal static string TooManySubnamespaces
		{
			get
			{
				return global::System.SR.GetResourceString("TooManySubnamespaces", null);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000025F5 File Offset: 0x000007F5
		internal static string TooManyExceptionRegions
		{
			get
			{
				return global::System.SR.GetResourceString("TooManyExceptionRegions", null);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002602 File Offset: 0x00000802
		internal static string SequencePointValueOutOfRange
		{
			get
			{
				return global::System.SR.GetResourceString("SequencePointValueOutOfRange", null);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000260F File Offset: 0x0000080F
		internal static string InvalidDirectoryRVA
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidDirectoryRVA", null);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000261C File Offset: 0x0000081C
		internal static string InvalidDirectorySize
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidDirectorySize", null);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002629 File Offset: 0x00000829
		internal static string InvalidDebugDirectoryEntryCharacteristics
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidDebugDirectoryEntryCharacteristics", null);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002636 File Offset: 0x00000836
		internal static string UnexpectedCodeViewDataSignature
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedCodeViewDataSignature", null);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002643 File Offset: 0x00000843
		internal static string UnexpectedEmbeddedPortablePdbDataSignature
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedEmbeddedPortablePdbDataSignature", null);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002650 File Offset: 0x00000850
		internal static string InvalidPdbChecksumDataFormat
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidPdbChecksumDataFormat", null);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000265D File Offset: 0x0000085D
		internal static string UnexpectedSignatureHeader
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedSignatureHeader", null);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000266A File Offset: 0x0000086A
		internal static string UnexpectedSignatureHeader2
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedSignatureHeader2", null);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002677 File Offset: 0x00000877
		internal static string NotTypeDefOrRefHandle
		{
			get
			{
				return global::System.SR.GetResourceString("NotTypeDefOrRefHandle", null);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002684 File Offset: 0x00000884
		internal static string UnexpectedSignatureTypeCode
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedSignatureTypeCode", null);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002691 File Offset: 0x00000891
		internal static string SignatureTypeSequenceMustHaveAtLeastOneElement
		{
			get
			{
				return global::System.SR.GetResourceString("SignatureTypeSequenceMustHaveAtLeastOneElement", null);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000269E File Offset: 0x0000089E
		internal static string NotTypeDefOrRefOrSpecHandle
		{
			get
			{
				return global::System.SR.GetResourceString("NotTypeDefOrRefOrSpecHandle", null);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000026AB File Offset: 0x000008AB
		internal static string UnexpectedDebugDirectoryType
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedDebugDirectoryType", null);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000026B8 File Offset: 0x000008B8
		internal static string HeapSizeLimitExceeded
		{
			get
			{
				return global::System.SR.GetResourceString("HeapSizeLimitExceeded", null);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000026C5 File Offset: 0x000008C5
		internal static string BuilderMustAligned
		{
			get
			{
				return global::System.SR.GetResourceString("BuilderMustAligned", null);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000026D2 File Offset: 0x000008D2
		internal static string BuilderAlreadyLinked
		{
			get
			{
				return global::System.SR.GetResourceString("BuilderAlreadyLinked", null);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000026DF File Offset: 0x000008DF
		internal static string ReturnedBuilderSizeTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("ReturnedBuilderSizeTooSmall", null);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000026EC File Offset: 0x000008EC
		internal static string SignatureNotVarArg
		{
			get
			{
				return global::System.SR.GetResourceString("SignatureNotVarArg", null);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000026F9 File Offset: 0x000008F9
		internal static string LabelDoesntBelongToBuilder
		{
			get
			{
				return global::System.SR.GetResourceString("LabelDoesntBelongToBuilder", null);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002706 File Offset: 0x00000906
		internal static string ControlFlowBuilderNotAvailable
		{
			get
			{
				return global::System.SR.GetResourceString("ControlFlowBuilderNotAvailable", null);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002713 File Offset: 0x00000913
		internal static string BaseReaderMustBeFullMetadataReader
		{
			get
			{
				return global::System.SR.GetResourceString("BaseReaderMustBeFullMetadataReader", null);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002720 File Offset: 0x00000920
		internal static string ModuleAlreadyAdded
		{
			get
			{
				return global::System.SR.GetResourceString("ModuleAlreadyAdded", null);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000272D File Offset: 0x0000092D
		internal static string AssemblyAlreadyAdded
		{
			get
			{
				return global::System.SR.GetResourceString("AssemblyAlreadyAdded", null);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000273A File Offset: 0x0000093A
		internal static string ExpectedListOfSize
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedListOfSize", null);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002747 File Offset: 0x00000947
		internal static string ExpectedArrayOfSize
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedArrayOfSize", null);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002754 File Offset: 0x00000954
		internal static string ExpectedNonEmptyList
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedNonEmptyList", null);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002761 File Offset: 0x00000961
		internal static string ExpectedNonEmptyArray
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedNonEmptyArray", null);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000276E File Offset: 0x0000096E
		internal static string ExpectedNonEmptyString
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedNonEmptyString", null);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000277B File Offset: 0x0000097B
		internal static string ReadersMustBeDeltaReaders
		{
			get
			{
				return global::System.SR.GetResourceString("ReadersMustBeDeltaReaders", null);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002788 File Offset: 0x00000988
		internal static string SignatureProviderReturnedInvalidSignature
		{
			get
			{
				return global::System.SR.GetResourceString("SignatureProviderReturnedInvalidSignature", null);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002795 File Offset: 0x00000995
		internal static string UnknownSectionName
		{
			get
			{
				return global::System.SR.GetResourceString("UnknownSectionName", null);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000027A2 File Offset: 0x000009A2
		internal static string HashTooShort
		{
			get
			{
				return global::System.SR.GetResourceString("HashTooShort", null);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000027AF File Offset: 0x000009AF
		internal static string UnexpectedArrayLength
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedArrayLength", null);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000027BC File Offset: 0x000009BC
		internal static string ValueMustBeMultiple
		{
			get
			{
				return global::System.SR.GetResourceString("ValueMustBeMultiple", null);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000027C9 File Offset: 0x000009C9
		internal static string MustNotReturnNull
		{
			get
			{
				return global::System.SR.GetResourceString("MustNotReturnNull", null);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000027D6 File Offset: 0x000009D6
		internal static string MetadataVersionTooLong
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataVersionTooLong", null);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000027E3 File Offset: 0x000009E3
		internal static string RowCountMustBeZero
		{
			get
			{
				return global::System.SR.GetResourceString("RowCountMustBeZero", null);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000027F0 File Offset: 0x000009F0
		internal static string RowCountOutOfRange
		{
			get
			{
				return global::System.SR.GetResourceString("RowCountOutOfRange", null);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000027FD File Offset: 0x000009FD
		internal static string SizeMismatch
		{
			get
			{
				return global::System.SR.GetResourceString("SizeMismatch", null);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000280A File Offset: 0x00000A0A
		internal static string DataTooBig
		{
			get
			{
				return global::System.SR.GetResourceString("DataTooBig", null);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002817 File Offset: 0x00000A17
		internal static string UnsupportedFormatVersion
		{
			get
			{
				return global::System.SR.GetResourceString("UnsupportedFormatVersion", null);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002824 File Offset: 0x00000A24
		internal static string DistanceBetweenInstructionAndLabelTooBig
		{
			get
			{
				return global::System.SR.GetResourceString("DistanceBetweenInstructionAndLabelTooBig", null);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002831 File Offset: 0x00000A31
		internal static string LabelNotMarked
		{
			get
			{
				return global::System.SR.GetResourceString("LabelNotMarked", null);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000283E File Offset: 0x00000A3E
		internal static string MethodHasNoExceptionRegions
		{
			get
			{
				return global::System.SR.GetResourceString("MethodHasNoExceptionRegions", null);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000284B File Offset: 0x00000A4B
		internal static string InvalidExceptionRegionBounds
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidExceptionRegionBounds", null);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002858 File Offset: 0x00000A58
		internal static string UnexpectedValue
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedValue", null);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002865 File Offset: 0x00000A65
		internal static string UnexpectedValueUnknownType
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedValueUnknownType", null);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002872 File Offset: 0x00000A72
		internal static string UnreachableLocation
		{
			get
			{
				return global::System.SR.GetResourceString("UnreachableLocation", null);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002880 File Offset: 0x00000A80
		// Note: this type is marked as 'beforefieldinit'.
		static SR()
		{
			bool flag;
			global::System.SR.s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", ref flag) && flag;
		}

		// Token: 0x04000004 RID: 4
		private static readonly bool s_usingResourceKeys;

		// Token: 0x04000005 RID: 5
		private static ResourceManager s_resourceManager;
	}
}

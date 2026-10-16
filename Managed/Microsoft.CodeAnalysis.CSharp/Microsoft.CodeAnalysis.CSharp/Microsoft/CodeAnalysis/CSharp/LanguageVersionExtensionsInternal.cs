using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001E5 RID: 485
	internal static class LanguageVersionExtensionsInternal
	{
		// Token: 0x06002151 RID: 8529 RVA: 0x000AC8F8 File Offset: 0x000AAAF8
		internal static bool IsValid(this LanguageVersion value)
		{
			if (value <= LanguageVersion.CSharp7_3)
			{
				if (value - LanguageVersion.CSharp1 > 6 && value - LanguageVersion.CSharp7_1 > 2)
				{
					return false;
				}
			}
			else if (value != LanguageVersion.CSharp8 && value != LanguageVersion.CSharp9 && value != LanguageVersion.Preview)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000AC930 File Offset: 0x000AAB30
		internal static ErrorCode GetErrorCode(this LanguageVersion version)
		{
			if (version <= LanguageVersion.CSharp7_3)
			{
				switch (version)
				{
				case LanguageVersion.CSharp1:
					return ErrorCode.ERR_FeatureNotAvailableInVersion1;
				case LanguageVersion.CSharp2:
					return ErrorCode.ERR_FeatureNotAvailableInVersion2;
				case LanguageVersion.CSharp3:
					return ErrorCode.ERR_FeatureNotAvailableInVersion3;
				case LanguageVersion.CSharp4:
					return ErrorCode.ERR_FeatureNotAvailableInVersion4;
				case LanguageVersion.CSharp5:
					return ErrorCode.ERR_FeatureNotAvailableInVersion5;
				case LanguageVersion.CSharp6:
					return ErrorCode.ERR_FeatureNotAvailableInVersion6;
				case LanguageVersion.CSharp7:
					return ErrorCode.ERR_FeatureNotAvailableInVersion7;
				default:
					switch (version)
					{
					case LanguageVersion.CSharp7_1:
						return ErrorCode.ERR_FeatureNotAvailableInVersion7_1;
					case LanguageVersion.CSharp7_2:
						return ErrorCode.ERR_FeatureNotAvailableInVersion7_2;
					case LanguageVersion.CSharp7_3:
						return ErrorCode.ERR_FeatureNotAvailableInVersion7_3;
					}
					break;
				}
			}
			else
			{
				if (version == LanguageVersion.CSharp8)
				{
					return ErrorCode.ERR_FeatureNotAvailableInVersion8;
				}
				if (version == LanguageVersion.CSharp9)
				{
					return ErrorCode.ERR_FeatureNotAvailableInVersion9;
				}
			}
			throw ExceptionUtilities.UnexpectedValue(version);
		}
	}
}

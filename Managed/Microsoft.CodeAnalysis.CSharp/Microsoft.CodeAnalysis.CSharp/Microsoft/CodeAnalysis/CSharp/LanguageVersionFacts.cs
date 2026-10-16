using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001E7 RID: 487
	public static class LanguageVersionFacts
	{
		// Token: 0x06002156 RID: 8534 RVA: 0x000ACA0C File Offset: 0x000AAC0C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static string ToDisplayString(this LanguageVersion version)
		{
			if (version <= LanguageVersion.CSharp7_3)
			{
				switch (version)
				{
				case LanguageVersion.Default:
					return "default";
				case LanguageVersion.CSharp1:
					return "1";
				case LanguageVersion.CSharp2:
					return "2";
				case LanguageVersion.CSharp3:
					return "3";
				case LanguageVersion.CSharp4:
					return "4";
				case LanguageVersion.CSharp5:
					return "5";
				case LanguageVersion.CSharp6:
					return "6";
				case LanguageVersion.CSharp7:
					return "7.0";
				default:
					switch (version)
					{
					case LanguageVersion.CSharp7_1:
						return "7.1";
					case LanguageVersion.CSharp7_2:
						return "7.2";
					case LanguageVersion.CSharp7_3:
						return "7.3";
					}
					break;
				}
			}
			else
			{
				if (version == LanguageVersion.CSharp8)
				{
					return "8.0";
				}
				if (version == LanguageVersion.CSharp9)
				{
					return "9.0";
				}
				switch (version)
				{
				case LanguageVersion.LatestMajor:
					return "latestmajor";
				case LanguageVersion.Preview:
					return "preview";
				case LanguageVersion.Latest:
					return "latest";
				}
			}
			throw ExceptionUtilities.UnexpectedValue(version);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000ACAFC File Offset: 0x000AACFC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryParse(string version, out LanguageVersion result)
		{
			if (version == null)
			{
				result = LanguageVersion.Default;
				return true;
			}
			string text = CaseInsensitiveComparison.ToLower(version);
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 906799682U)
			{
				if (num <= 448036273U)
				{
					if (num <= 385118798U)
					{
						if (num != 351563560U)
						{
							if (num != 368341179U)
							{
								if (num != 385118798U)
								{
									goto IL_045E;
								}
								if (!(text == "7.2"))
								{
									goto IL_045E;
								}
								result = LanguageVersion.CSharp7_2;
								return true;
							}
							else
							{
								if (!(text == "7.1"))
								{
									goto IL_045E;
								}
								result = LanguageVersion.CSharp7_1;
								return true;
							}
						}
						else if (!(text == "7.0"))
						{
							goto IL_045E;
						}
					}
					else if (num != 397703416U)
					{
						if (num != 401896417U)
						{
							if (num != 448036273U)
							{
								goto IL_045E;
							}
							if (!(text == "iso-2"))
							{
								goto IL_045E;
							}
							goto IL_0413;
						}
						else
						{
							if (!(text == "7.3"))
							{
								goto IL_045E;
							}
							result = LanguageVersion.CSharp7_3;
							return true;
						}
					}
					else
					{
						if (!(text == "iso-1"))
						{
							goto IL_045E;
						}
						goto IL_040E;
					}
				}
				else if (num <= 822911587U)
				{
					if (num != 673058499U)
					{
						if (num != 806133968U)
						{
							if (num != 822911587U)
							{
								goto IL_045E;
							}
							if (!(text == "4"))
							{
								goto IL_045E;
							}
							goto IL_041D;
						}
						else
						{
							if (!(text == "5"))
							{
								goto IL_045E;
							}
							goto IL_0422;
						}
					}
					else
					{
						if (!(text == "2.0"))
						{
							goto IL_045E;
						}
						goto IL_0413;
					}
				}
				else if (num <= 856466825U)
				{
					if (num != 839689206U)
					{
						if (num != 856466825U)
						{
							goto IL_045E;
						}
						if (!(text == "6"))
						{
							goto IL_045E;
						}
						goto IL_0427;
					}
					else if (!(text == "7"))
					{
						goto IL_045E;
					}
				}
				else if (num != 873244444U)
				{
					if (num != 906799682U)
					{
						goto IL_045E;
					}
					if (!(text == "3"))
					{
						goto IL_045E;
					}
					goto IL_0418;
				}
				else
				{
					if (!(text == "1"))
					{
						goto IL_045E;
					}
					goto IL_040E;
				}
				result = LanguageVersion.CSharp7;
				return true;
			}
			if (num <= 2470140894U)
			{
				if (num <= 1024243015U)
				{
					if (num != 923577301U)
					{
						if (num != 1007465396U)
						{
							if (num != 1024243015U)
							{
								goto IL_045E;
							}
							if (!(text == "8"))
							{
								goto IL_045E;
							}
						}
						else
						{
							if (!(text == "9"))
							{
								goto IL_045E;
							}
							goto IL_0455;
						}
					}
					else
					{
						if (!(text == "2"))
						{
							goto IL_045E;
						}
						goto IL_0413;
					}
				}
				else if (num <= 1232911777U)
				{
					if (num != 1097876038U)
					{
						if (num != 1232911777U)
						{
							goto IL_045E;
						}
						if (!(text == "8.0"))
						{
							goto IL_045E;
						}
					}
					else
					{
						if (!(text == "latest"))
						{
							goto IL_045E;
						}
						result = LanguageVersion.Latest;
						return true;
					}
				}
				else if (num != 1257924439U)
				{
					if (num != 2470140894U)
					{
						goto IL_045E;
					}
					if (!(text == "default"))
					{
						goto IL_045E;
					}
					result = LanguageVersion.Default;
					return true;
				}
				else
				{
					if (!(text == "preview"))
					{
						goto IL_045E;
					}
					result = LanguageVersion.Preview;
					return true;
				}
				result = LanguageVersion.CSharp8;
				return true;
			}
			if (num <= 3219336542U)
			{
				if (num != 2578913203U)
				{
					if (num != 2787178501U)
					{
						if (num != 3219336542U)
						{
							goto IL_045E;
						}
						if (!(text == "5.0"))
						{
							goto IL_045E;
						}
						goto IL_0422;
					}
					else
					{
						if (!(text == "4.0"))
						{
							goto IL_045E;
						}
						goto IL_041D;
					}
				}
				else
				{
					if (!(text == "latestmajor"))
					{
						goto IL_045E;
					}
					result = LanguageVersion.LatestMajor;
					return true;
				}
			}
			else if (num <= 3791713530U)
			{
				if (num != 3242306012U)
				{
					if (num != 3791713530U)
					{
						goto IL_045E;
					}
					if (!(text == "9.0"))
					{
						goto IL_045E;
					}
				}
				else
				{
					if (!(text == "3.0"))
					{
						goto IL_045E;
					}
					goto IL_0418;
				}
			}
			else if (num != 3946978290U)
			{
				if (num != 4235161167U)
				{
					goto IL_045E;
				}
				if (!(text == "6.0"))
				{
					goto IL_045E;
				}
				goto IL_0427;
			}
			else
			{
				if (!(text == "1.0"))
				{
					goto IL_045E;
				}
				goto IL_040E;
			}
			IL_0455:
			result = LanguageVersion.CSharp9;
			return true;
			IL_040E:
			result = LanguageVersion.CSharp1;
			return true;
			IL_0413:
			result = LanguageVersion.CSharp2;
			return true;
			IL_0418:
			result = LanguageVersion.CSharp3;
			return true;
			IL_041D:
			result = LanguageVersion.CSharp4;
			return true;
			IL_0422:
			result = LanguageVersion.CSharp5;
			return true;
			IL_0427:
			result = LanguageVersion.CSharp6;
			return true;
			IL_045E:
			result = LanguageVersion.Default;
			return false;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000ACF6B File Offset: 0x000AB16B
		public static LanguageVersion MapSpecifiedToEffectiveVersion(this LanguageVersion version)
		{
			if (version == LanguageVersion.Default || version == LanguageVersion.LatestMajor || version == LanguageVersion.Latest)
			{
				return LanguageVersion.CSharp9;
			}
			return version;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x000ACF87 File Offset: 0x000AB187
		internal static LanguageVersion CurrentVersion
		{
			get
			{
				return LanguageVersion.CSharp9;
			}
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000ACF8E File Offset: 0x000AB18E
		internal static bool DisallowInferredTupleElementNames(this LanguageVersion self)
		{
			return self < MessageID.IDS_FeatureInferredTupleNames.RequiredVersion();
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000ACF9D File Offset: 0x000AB19D
		internal static bool AllowNonTrailingNamedArguments(this LanguageVersion self)
		{
			return self >= MessageID.IDS_FeatureNonTrailingNamedArguments.RequiredVersion();
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000ACFAF File Offset: 0x000AB1AF
		internal static bool AllowAttributesOnBackingFields(this LanguageVersion self)
		{
			return self >= MessageID.IDS_FeatureAttributesOnBackingFields.RequiredVersion();
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000ACFC1 File Offset: 0x000AB1C1
		internal static bool AllowImprovedOverloadCandidates(this LanguageVersion self)
		{
			return self >= MessageID.IDS_FeatureImprovedOverloadCandidates.RequiredVersion();
		}
	}
}

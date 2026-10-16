using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis.Diagnostics.CSharp
{
	// Token: 0x0200000B RID: 11
	[DiagnosticAnalyzer("C#", new string[] { })]
	internal sealed class CSharpCompilerDiagnosticAnalyzer : CompilerDiagnosticAnalyzer
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00006DAB File Offset: 0x00004FAB
		internal override CommonMessageProvider MessageProvider
		{
			get
			{
				return Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006DB4 File Offset: 0x00004FB4
		internal override ImmutableArray<int> GetSupportedErrorCodes()
		{
			Array values = Enum.GetValues(typeof(ErrorCode));
			ImmutableArray<int>.Builder builder = ImmutableArray.CreateBuilder<int>(values.Length);
			foreach (object obj in values)
			{
				int num = (int)obj;
				if (num <= 656)
				{
					if (num <= 169)
					{
						if (num <= 28)
						{
							if (num - -2 <= 1 || num == 17 || num == 28)
							{
								continue;
							}
						}
						else if (num == 67 || num == 148 || num == 169)
						{
							continue;
						}
					}
					else if (num <= 414)
					{
						if (num == 204 || num == 402 || num == 414)
						{
							continue;
						}
					}
					else if (num <= 570)
					{
						if (num == 518 || num == 570)
						{
							continue;
						}
					}
					else if (num == 649 || num == 656)
					{
						continue;
					}
				}
				else if (num <= 5001)
				{
					if (num <= 1607)
					{
						if (num - 1555 <= 1 || num == 1558 || num == 1607)
						{
							continue;
						}
					}
					else if (num <= 4007)
					{
						if (num == 1969 || num == 4007)
						{
							continue;
						}
					}
					else if (num == 4013 || num == 5001)
					{
						continue;
					}
				}
				else if (num <= 8006)
				{
					if (num == 7022 || num == 7038 || num - 8004 <= 2)
					{
						continue;
					}
				}
				else if (num <= 8078)
				{
					if (num == 8008 || num == 8078)
					{
						continue;
					}
				}
				else if (num == 8178 || num == 8892)
				{
					continue;
				}
				builder.Add(num);
			}
			return builder.ToImmutable();
		}
	}
}

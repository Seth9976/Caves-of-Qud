using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides a simple structure for storing X509 chain status and error information.</summary>
	// Token: 0x020002E3 RID: 739
	public struct X509ChainStatus
	{
		// Token: 0x06001770 RID: 6000 RVA: 0x0005CBD5 File Offset: 0x0005ADD5
		internal X509ChainStatus(X509ChainStatusFlags flag)
		{
			this.status = flag;
			this.info = X509ChainStatus.GetInformation(flag);
		}

		/// <summary>Specifies the status of the X509 chain.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatusFlags" /> value.</returns>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x0005CBEA File Offset: 0x0005ADEA
		// (set) Token: 0x06001772 RID: 6002 RVA: 0x0005CBF2 File Offset: 0x0005ADF2
		public X509ChainStatusFlags Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		/// <summary>Specifies a description of the <see cref="P:System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus" /> value.</summary>
		/// <returns>A localizable string.</returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0005CBFB File Offset: 0x0005ADFB
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x0005CC03 File Offset: 0x0005AE03
		public string StatusInformation
		{
			get
			{
				return this.info;
			}
			set
			{
				this.info = value;
			}
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0005CC0C File Offset: 0x0005AE0C
		internal static string GetInformation(X509ChainStatusFlags flags)
		{
			if (flags <= X509ChainStatusFlags.InvalidNameConstraints)
			{
				if (flags <= X509ChainStatusFlags.RevocationStatusUnknown)
				{
					if (flags <= X509ChainStatusFlags.NotValidForUsage)
					{
						switch (flags)
						{
						case X509ChainStatusFlags.NoError:
						case X509ChainStatusFlags.NotTimeValid | X509ChainStatusFlags.NotTimeNested:
						case X509ChainStatusFlags.NotTimeValid | X509ChainStatusFlags.Revoked:
						case X509ChainStatusFlags.NotTimeNested | X509ChainStatusFlags.Revoked:
						case X509ChainStatusFlags.NotTimeValid | X509ChainStatusFlags.NotTimeNested | X509ChainStatusFlags.Revoked:
							goto IL_0125;
						case X509ChainStatusFlags.NotTimeValid:
						case X509ChainStatusFlags.NotTimeNested:
						case X509ChainStatusFlags.Revoked:
						case X509ChainStatusFlags.NotSignatureValid:
							break;
						default:
							if (flags != X509ChainStatusFlags.NotValidForUsage)
							{
								goto IL_0125;
							}
							break;
						}
					}
					else if (flags != X509ChainStatusFlags.UntrustedRoot && flags != X509ChainStatusFlags.RevocationStatusUnknown)
					{
						goto IL_0125;
					}
				}
				else if (flags <= X509ChainStatusFlags.InvalidExtension)
				{
					if (flags != X509ChainStatusFlags.Cyclic && flags != X509ChainStatusFlags.InvalidExtension)
					{
						goto IL_0125;
					}
				}
				else if (flags != X509ChainStatusFlags.InvalidPolicyConstraints && flags != X509ChainStatusFlags.InvalidBasicConstraints && flags != X509ChainStatusFlags.InvalidNameConstraints)
				{
					goto IL_0125;
				}
			}
			else if (flags <= X509ChainStatusFlags.PartialChain)
			{
				if (flags <= X509ChainStatusFlags.HasNotDefinedNameConstraint)
				{
					if (flags != X509ChainStatusFlags.HasNotSupportedNameConstraint && flags != X509ChainStatusFlags.HasNotDefinedNameConstraint)
					{
						goto IL_0125;
					}
				}
				else if (flags != X509ChainStatusFlags.HasNotPermittedNameConstraint && flags != X509ChainStatusFlags.HasExcludedNameConstraint && flags != X509ChainStatusFlags.PartialChain)
				{
					goto IL_0125;
				}
			}
			else if (flags <= X509ChainStatusFlags.CtlNotSignatureValid)
			{
				if (flags != X509ChainStatusFlags.CtlNotTimeValid && flags != X509ChainStatusFlags.CtlNotSignatureValid)
				{
					goto IL_0125;
				}
			}
			else if (flags != X509ChainStatusFlags.CtlNotValidForUsage && flags != X509ChainStatusFlags.OfflineRevocation && flags != X509ChainStatusFlags.NoIssuanceChainPolicy)
			{
				goto IL_0125;
			}
			return global::Locale.GetText(flags.ToString());
			IL_0125:
			return string.Empty;
		}

		// Token: 0x04000D44 RID: 3396
		private X509ChainStatusFlags status;

		// Token: 0x04000D45 RID: 3397
		private string info;
	}
}

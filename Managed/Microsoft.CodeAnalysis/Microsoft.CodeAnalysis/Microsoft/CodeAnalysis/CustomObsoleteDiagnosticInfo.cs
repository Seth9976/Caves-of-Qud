using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200016D RID: 365
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CustomObsoleteDiagnosticInfo : DiagnosticInfo
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0002C86A File Offset: 0x0002AA6A
		internal ObsoleteAttributeData Data { get; }

		// Token: 0x06000EFD RID: 3837 RVA: 0x0002C872 File Offset: 0x0002AA72
		internal CustomObsoleteDiagnosticInfo(CommonMessageProvider messageProvider, int errorCode, ObsoleteAttributeData data, params object[] arguments)
			: base(messageProvider, errorCode, arguments)
		{
			this.Data = data;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0002C885 File Offset: 0x0002AA85
		private CustomObsoleteDiagnosticInfo(CustomObsoleteDiagnosticInfo baseInfo, DiagnosticSeverity effectiveSeverity)
			: base(baseInfo, effectiveSeverity)
		{
			this.Data = baseInfo.Data;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0002C89C File Offset: 0x0002AA9C
		public override string MessageIdentifier
		{
			get
			{
				string diagnosticId = this.Data.DiagnosticId;
				if (!string.IsNullOrEmpty(diagnosticId))
				{
					return diagnosticId;
				}
				return base.MessageIdentifier;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x0002C8C5 File Offset: 0x0002AAC5
		public override DiagnosticDescriptor Descriptor
		{
			get
			{
				if (this._descriptor == null)
				{
					Interlocked.CompareExchange<DiagnosticDescriptor>(ref this._descriptor, this.CreateDescriptor(), null);
				}
				return this._descriptor;
			}
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0002C8E8 File Offset: 0x0002AAE8
		internal override DiagnosticInfo GetInstanceWithSeverity(DiagnosticSeverity severity)
		{
			return new CustomObsoleteDiagnosticInfo(this, severity);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002C8F4 File Offset: 0x0002AAF4
		private DiagnosticDescriptor CreateDescriptor()
		{
			DiagnosticDescriptor descriptor = base.Descriptor;
			string diagnosticId = this.Data.DiagnosticId;
			string urlFormat = this.Data.UrlFormat;
			if (diagnosticId == null && urlFormat == null)
			{
				return descriptor;
			}
			string messageIdentifier = this.MessageIdentifier;
			string text = descriptor.HelpLinkUri;
			if (urlFormat != null)
			{
				try
				{
					text = string.Format(urlFormat, messageIdentifier);
				}
				catch
				{
				}
			}
			ImmutableArray<string> immutableArray;
			if (diagnosticId == null)
			{
				immutableArray = descriptor.CustomTags.ToImmutableArray<string>();
			}
			else
			{
				int num = 1;
				ICollection<string> collection = descriptor.CustomTags as ICollection<string>;
				if (collection != null)
				{
					int count = collection.Count;
					num += count;
				}
				ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance(num);
				instance.AddRange(descriptor.CustomTags);
				instance.Add("CustomObsolete");
				immutableArray = instance.ToImmutableAndFree();
			}
			return new DiagnosticDescriptor(messageIdentifier, descriptor.Title, descriptor.MessageFormat, descriptor.Category, descriptor.DefaultSeverity, descriptor.IsEnabledByDefault, descriptor.Description, text, immutableArray);
		}

		// Token: 0x04000425 RID: 1061
		[Nullable(2)]
		private DiagnosticDescriptor _descriptor;
	}
}

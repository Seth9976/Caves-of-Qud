using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000216 RID: 534
	internal class CommonAssemblyWellKnownAttributeData<TNamedTypeSymbol> : WellKnownAttributeData, ISecurityAttributeTarget
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00043083 File Offset: 0x00041283
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x0004308B File Offset: 0x0004128B
		public string AssemblySignatureKeyAttributeSetting
		{
			get
			{
				return this._assemblySignatureKeyAttributeSetting;
			}
			set
			{
				this._assemblySignatureKeyAttributeSetting = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00043094 File Offset: 0x00041294
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x0004309C File Offset: 0x0004129C
		public ThreeState AssemblyDelaySignAttributeSetting
		{
			get
			{
				return this._assemblyDelaySignAttributeSetting;
			}
			set
			{
				this._assemblyDelaySignAttributeSetting = value;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x000430A5 File Offset: 0x000412A5
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x000430AD File Offset: 0x000412AD
		public string AssemblyKeyFileAttributeSetting
		{
			get
			{
				return this._assemblyKeyFileAttributeSetting;
			}
			set
			{
				this._assemblyKeyFileAttributeSetting = value;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x000430B6 File Offset: 0x000412B6
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x000430BE File Offset: 0x000412BE
		public string AssemblyKeyContainerAttributeSetting
		{
			get
			{
				return this._assemblyKeyContainerAttributeSetting;
			}
			set
			{
				this._assemblyKeyContainerAttributeSetting = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x000430C7 File Offset: 0x000412C7
		// (set) Token: 0x060014DC RID: 5340 RVA: 0x000430CF File Offset: 0x000412CF
		public Version AssemblyVersionAttributeSetting
		{
			get
			{
				return this._assemblyVersionAttributeSetting;
			}
			set
			{
				this._assemblyVersionAttributeSetting = value;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x000430D8 File Offset: 0x000412D8
		// (set) Token: 0x060014DE RID: 5342 RVA: 0x000430E0 File Offset: 0x000412E0
		public string AssemblyFileVersionAttributeSetting
		{
			get
			{
				return this._assemblyFileVersionAttributeSetting;
			}
			set
			{
				this._assemblyFileVersionAttributeSetting = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x000430E9 File Offset: 0x000412E9
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x000430F1 File Offset: 0x000412F1
		public string AssemblyTitleAttributeSetting
		{
			get
			{
				return this._assemblyTitleAttributeSetting;
			}
			set
			{
				this._assemblyTitleAttributeSetting = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x000430FA File Offset: 0x000412FA
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x00043102 File Offset: 0x00041302
		public string AssemblyDescriptionAttributeSetting
		{
			get
			{
				return this._assemblyDescriptionAttributeSetting;
			}
			set
			{
				this._assemblyDescriptionAttributeSetting = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x0004310B File Offset: 0x0004130B
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x00043113 File Offset: 0x00041313
		public string AssemblyCultureAttributeSetting
		{
			get
			{
				return this._assemblyCultureAttributeSetting;
			}
			set
			{
				this._assemblyCultureAttributeSetting = value;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0004311C File Offset: 0x0004131C
		// (set) Token: 0x060014E6 RID: 5350 RVA: 0x00043124 File Offset: 0x00041324
		public string AssemblyCompanyAttributeSetting
		{
			get
			{
				return this._assemblyCompanyAttributeSetting;
			}
			set
			{
				this._assemblyCompanyAttributeSetting = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0004312D File Offset: 0x0004132D
		// (set) Token: 0x060014E8 RID: 5352 RVA: 0x00043135 File Offset: 0x00041335
		public string AssemblyProductAttributeSetting
		{
			get
			{
				return this._assemblyProductAttributeSetting;
			}
			set
			{
				this._assemblyProductAttributeSetting = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x0004313E File Offset: 0x0004133E
		// (set) Token: 0x060014EA RID: 5354 RVA: 0x00043146 File Offset: 0x00041346
		public string AssemblyInformationalVersionAttributeSetting
		{
			get
			{
				return this._assemblyInformationalVersionAttributeSetting;
			}
			set
			{
				this._assemblyInformationalVersionAttributeSetting = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0004314F File Offset: 0x0004134F
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x00043157 File Offset: 0x00041357
		public string AssemblyCopyrightAttributeSetting
		{
			get
			{
				return this._assemblyCopyrightAttributeSetting;
			}
			set
			{
				this._assemblyCopyrightAttributeSetting = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x00043160 File Offset: 0x00041360
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x00043168 File Offset: 0x00041368
		public string AssemblyTrademarkAttributeSetting
		{
			get
			{
				return this._assemblyTrademarkAttributeSetting;
			}
			set
			{
				this._assemblyTrademarkAttributeSetting = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00043171 File Offset: 0x00041371
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x00043179 File Offset: 0x00041379
		public AssemblyFlags AssemblyFlagsAttributeSetting
		{
			get
			{
				return this._assemblyFlagsAttributeSetting;
			}
			set
			{
				this._assemblyFlagsAttributeSetting = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00043182 File Offset: 0x00041382
		// (set) Token: 0x060014F2 RID: 5362 RVA: 0x0004318A File Offset: 0x0004138A
		public AssemblyHashAlgorithm? AssemblyAlgorithmIdAttributeSetting
		{
			get
			{
				return this._assemblyAlgorithmIdAttributeSetting;
			}
			set
			{
				this._assemblyAlgorithmIdAttributeSetting = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00043193 File Offset: 0x00041393
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x0004319B File Offset: 0x0004139B
		public bool HasCompilationRelaxationsAttribute
		{
			get
			{
				return this._hasCompilationRelaxationsAttribute;
			}
			set
			{
				this._hasCompilationRelaxationsAttribute = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000431A4 File Offset: 0x000413A4
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x000431AC File Offset: 0x000413AC
		public bool HasReferenceAssemblyAttribute
		{
			get
			{
				return this._hasReferenceAssemblyAttribute;
			}
			set
			{
				this._hasReferenceAssemblyAttribute = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000431B5 File Offset: 0x000413B5
		public bool HasRuntimeCompatibilityAttribute
		{
			get
			{
				return this._runtimeCompatibilityWrapNonExceptionThrows != null;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x000431C4 File Offset: 0x000413C4
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x000431EA File Offset: 0x000413EA
		public bool RuntimeCompatibilityWrapNonExceptionThrows
		{
			get
			{
				return this._runtimeCompatibilityWrapNonExceptionThrows ?? true;
			}
			set
			{
				this._runtimeCompatibilityWrapNonExceptionThrows = new bool?(value);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x000431F8 File Offset: 0x000413F8
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x00043200 File Offset: 0x00041400
		public bool HasDebuggableAttribute
		{
			get
			{
				return this._hasDebuggableAttribute;
			}
			set
			{
				this._hasDebuggableAttribute = value;
			}
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00043209 File Offset: 0x00041409
		SecurityWellKnownAttributeData ISecurityAttributeTarget.GetOrCreateData()
		{
			if (this._lazySecurityAttributeData == null)
			{
				this._lazySecurityAttributeData = new SecurityWellKnownAttributeData();
			}
			return this._lazySecurityAttributeData;
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00043224 File Offset: 0x00041424
		public SecurityWellKnownAttributeData SecurityInformation
		{
			get
			{
				return this._lazySecurityAttributeData;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0004322C File Offset: 0x0004142C
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x00043234 File Offset: 0x00041434
		public HashSet<TNamedTypeSymbol> ForwardedTypes
		{
			get
			{
				return this._forwardedTypes;
			}
			set
			{
				this._forwardedTypes = value;
			}
		}

		// Token: 0x04000899 RID: 2201
		private string _assemblySignatureKeyAttributeSetting;

		// Token: 0x0400089A RID: 2202
		private ThreeState _assemblyDelaySignAttributeSetting;

		// Token: 0x0400089B RID: 2203
		private string _assemblyKeyFileAttributeSetting = WellKnownAttributeData.StringMissingValue;

		// Token: 0x0400089C RID: 2204
		private string _assemblyKeyContainerAttributeSetting = WellKnownAttributeData.StringMissingValue;

		// Token: 0x0400089D RID: 2205
		private Version _assemblyVersionAttributeSetting;

		// Token: 0x0400089E RID: 2206
		private string _assemblyFileVersionAttributeSetting;

		// Token: 0x0400089F RID: 2207
		private string _assemblyTitleAttributeSetting;

		// Token: 0x040008A0 RID: 2208
		private string _assemblyDescriptionAttributeSetting;

		// Token: 0x040008A1 RID: 2209
		private string _assemblyCultureAttributeSetting;

		// Token: 0x040008A2 RID: 2210
		private string _assemblyCompanyAttributeSetting;

		// Token: 0x040008A3 RID: 2211
		private string _assemblyProductAttributeSetting;

		// Token: 0x040008A4 RID: 2212
		private string _assemblyInformationalVersionAttributeSetting;

		// Token: 0x040008A5 RID: 2213
		private string _assemblyCopyrightAttributeSetting;

		// Token: 0x040008A6 RID: 2214
		private string _assemblyTrademarkAttributeSetting;

		// Token: 0x040008A7 RID: 2215
		private AssemblyFlags _assemblyFlagsAttributeSetting;

		// Token: 0x040008A8 RID: 2216
		private AssemblyHashAlgorithm? _assemblyAlgorithmIdAttributeSetting;

		// Token: 0x040008A9 RID: 2217
		private bool _hasCompilationRelaxationsAttribute;

		// Token: 0x040008AA RID: 2218
		private bool _hasReferenceAssemblyAttribute;

		// Token: 0x040008AB RID: 2219
		private bool? _runtimeCompatibilityWrapNonExceptionThrows;

		// Token: 0x040008AC RID: 2220
		internal const bool WrapNonExceptionThrowsDefault = true;

		// Token: 0x040008AD RID: 2221
		private bool _hasDebuggableAttribute;

		// Token: 0x040008AE RID: 2222
		private SecurityWellKnownAttributeData _lazySecurityAttributeData;

		// Token: 0x040008AF RID: 2223
		private HashSet<TNamedTypeSymbol> _forwardedTypes;
	}
}

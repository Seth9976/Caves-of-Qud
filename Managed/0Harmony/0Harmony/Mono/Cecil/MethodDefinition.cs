using System;
using System.Threading;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000238 RID: 568
	public sealed class MethodDefinition : MethodReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, ISecurityDeclarationProvider, ICustomDebugInformationProvider
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0002AA35 File Offset: 0x00028C35
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0002AA3D File Offset: 0x00028C3D
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != base.Name)
				{
					throw new InvalidOperationException();
				}
				base.Name = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0002AA62 File Offset: 0x00028C62
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x0002AA6A File Offset: 0x00028C6A
		public MethodAttributes Attributes
		{
			get
			{
				return (MethodAttributes)this.attributes;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != (MethodAttributes)this.attributes)
				{
					throw new InvalidOperationException();
				}
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0002AA8A File Offset: 0x00028C8A
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x0002AA92 File Offset: 0x00028C92
		public MethodImplAttributes ImplAttributes
		{
			get
			{
				return (MethodImplAttributes)this.impl_attributes;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != (MethodImplAttributes)this.impl_attributes)
				{
					throw new InvalidOperationException();
				}
				this.impl_attributes = (ushort)value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0002AAB2 File Offset: 0x00028CB2
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x0002AAF0 File Offset: 0x00028CF0
		public MethodSemanticsAttributes SemanticsAttributes
		{
			get
			{
				if (this.sem_attrs_ready)
				{
					return this.sem_attrs;
				}
				if (base.HasImage)
				{
					this.ReadSemantics();
					return this.sem_attrs;
				}
				this.sem_attrs = MethodSemanticsAttributes.None;
				this.sem_attrs_ready = true;
				return this.sem_attrs;
			}
			set
			{
				this.sem_attrs = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0002AAF9 File Offset: 0x00028CF9
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x0002736E File Offset: 0x0002556E
		internal MethodDefinitionProjection WindowsRuntimeProjection
		{
			get
			{
				return (MethodDefinitionProjection)this.projection;
			}
			set
			{
				this.projection = value;
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002AB08 File Offset: 0x00028D08
		internal void ReadSemantics()
		{
			if (this.sem_attrs_ready)
			{
				return;
			}
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				return;
			}
			if (!module.HasImage)
			{
				return;
			}
			object syncRoot = module.SyncRoot;
			lock (syncRoot)
			{
				if (!this.sem_attrs_ready)
				{
					module.Read<MethodDefinition>(this, delegate(MethodDefinition method, MetadataReader reader)
					{
						reader.ReadAllSemantics(method);
					});
				}
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0002AB94 File Offset: 0x00028D94
		public bool HasSecurityDeclarations
		{
			get
			{
				if (this.security_declarations != null)
				{
					return this.security_declarations.Count > 0;
				}
				return this.GetHasSecurityDeclarations(this.Module);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002ABB9 File Offset: 0x00028DB9
		public Collection<SecurityDeclaration> SecurityDeclarations
		{
			get
			{
				return this.security_declarations ?? this.GetSecurityDeclarations(ref this.security_declarations, this.Module);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0002ABD7 File Offset: 0x00028DD7
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.Module);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0002ABFC File Offset: 0x00028DFC
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0002AC1A File Offset: 0x00028E1A
		public int RVA
		{
			get
			{
				return (int)this.rva;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0002AC24 File Offset: 0x00028E24
		public bool HasBody
		{
			get
			{
				return (this.attributes & 1024) == 0 && (this.attributes & 8192) == 0 && (this.impl_attributes & 4096) == 0 && (this.impl_attributes & 1) == 0 && (this.impl_attributes & 4) == 0 && (this.impl_attributes & 3) == 0;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0002AC7C File Offset: 0x00028E7C
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0002ACFC File Offset: 0x00028EFC
		public MethodBody Body
		{
			get
			{
				MethodBody methodBody = this.body;
				if (methodBody != null)
				{
					return methodBody;
				}
				if (!this.HasBody)
				{
					return null;
				}
				if (base.HasImage && this.rva != 0U)
				{
					return this.Module.Read<MethodDefinition, MethodBody>(ref this.body, this, (MethodDefinition method, MetadataReader reader) => reader.ReadMethodBody(method));
				}
				Interlocked.CompareExchange<MethodBody>(ref this.body, new MethodBody(this), null);
				return this.body;
			}
			set
			{
				ModuleDefinition module = this.Module;
				if (module == null)
				{
					this.body = value;
					return;
				}
				object syncRoot = module.SyncRoot;
				lock (syncRoot)
				{
					this.body = value;
					if (value == null)
					{
						this.debug_info = null;
					}
				}
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0002AD5C File Offset: 0x00028F5C
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0002AD8A File Offset: 0x00028F8A
		public MethodDebugInformation DebugInformation
		{
			get
			{
				Mixin.Read(this.Body);
				if (this.debug_info == null)
				{
					Interlocked.CompareExchange<MethodDebugInformation>(ref this.debug_info, new MethodDebugInformation(this), null);
				}
				return this.debug_info;
			}
			set
			{
				this.debug_info = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0002AD93 File Offset: 0x00028F93
		public bool HasPInvokeInfo
		{
			get
			{
				return this.pinvoke != null || this.IsPInvokeImpl;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0002ADA8 File Offset: 0x00028FA8
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0002AE07 File Offset: 0x00029007
		public PInvokeInfo PInvokeInfo
		{
			get
			{
				if (this.pinvoke != null)
				{
					return this.pinvoke;
				}
				if (base.HasImage && this.IsPInvokeImpl)
				{
					return this.Module.Read<MethodDefinition, PInvokeInfo>(ref this.pinvoke, this, (MethodDefinition method, MetadataReader reader) => reader.ReadPInvokeInfo(method));
				}
				return null;
			}
			set
			{
				this.IsPInvokeImpl = true;
				this.pinvoke = value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0002AE18 File Offset: 0x00029018
		public bool HasOverrides
		{
			get
			{
				if (this.overrides != null)
				{
					return this.overrides.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<MethodDefinition, bool>(this, (MethodDefinition method, MetadataReader reader) => reader.HasOverrides(method));
				}
				return false;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0002AE74 File Offset: 0x00029074
		public Collection<MethodReference> Overrides
		{
			get
			{
				if (this.overrides != null)
				{
					return this.overrides;
				}
				if (base.HasImage)
				{
					return this.Module.Read<MethodDefinition, Collection<MethodReference>>(ref this.overrides, this, (MethodDefinition method, MetadataReader reader) => reader.ReadOverrides(method));
				}
				Interlocked.CompareExchange<Collection<MethodReference>>(ref this.overrides, new Collection<MethodReference>(), null);
				return this.overrides;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0002AEE2 File Offset: 0x000290E2
		public override bool HasGenericParameters
		{
			get
			{
				if (this.generic_parameters != null)
				{
					return this.generic_parameters.Count > 0;
				}
				return this.GetHasGenericParameters(this.Module);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0002AF07 File Offset: 0x00029107
		public override Collection<GenericParameter> GenericParameters
		{
			get
			{
				return this.generic_parameters ?? this.GetGenericParameters(ref this.generic_parameters, this.Module);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002AF25 File Offset: 0x00029125
		public bool HasCustomDebugInformations
		{
			get
			{
				Mixin.Read(this.Body);
				return !this.custom_infos.IsNullOrEmpty<CustomDebugInformation>();
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0002AF40 File Offset: 0x00029140
		public Collection<CustomDebugInformation> CustomDebugInformations
		{
			get
			{
				Mixin.Read(this.Body);
				if (this.custom_infos == null)
				{
					Interlocked.CompareExchange<Collection<CustomDebugInformation>>(ref this.custom_infos, new Collection<CustomDebugInformation>(), null);
				}
				return this.custom_infos;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0002AF6D File Offset: 0x0002916D
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x0002AF7C File Offset: 0x0002917C
		public bool IsCompilerControlled
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 0U, value);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0002AF92 File Offset: 0x00029192
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x0002AFA1 File Offset: 0x000291A1
		public bool IsPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 1U, value);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0002AFB7 File Offset: 0x000291B7
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x0002AFC6 File Offset: 0x000291C6
		public bool IsFamilyAndAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 2U, value);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0002AFDC File Offset: 0x000291DC
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x0002AFEB File Offset: 0x000291EB
		public bool IsAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 3U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 3U, value);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0002B001 File Offset: 0x00029201
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0002B010 File Offset: 0x00029210
		public bool IsFamily
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 4U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 4U, value);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0002B026 File Offset: 0x00029226
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0002B035 File Offset: 0x00029235
		public bool IsFamilyOrAssembly
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 5U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 5U, value);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0002B04B File Offset: 0x0002924B
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0002B05A File Offset: 0x0002925A
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7, 6U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7, 6U, value);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0002B070 File Offset: 0x00029270
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x0002B07F File Offset: 0x0002927F
		public bool IsStatic
		{
			get
			{
				return this.attributes.GetAttributes(16);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16, value);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002B095 File Offset: 0x00029295
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x0002B0A4 File Offset: 0x000292A4
		public bool IsFinal
		{
			get
			{
				return this.attributes.GetAttributes(32);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(32, value);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002B0BA File Offset: 0x000292BA
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x0002B0C9 File Offset: 0x000292C9
		public bool IsVirtual
		{
			get
			{
				return this.attributes.GetAttributes(64);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(64, value);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0002B0DF File Offset: 0x000292DF
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x0002B0F1 File Offset: 0x000292F1
		public bool IsHideBySig
		{
			get
			{
				return this.attributes.GetAttributes(128);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(128, value);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002B10A File Offset: 0x0002930A
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x0002B11D File Offset: 0x0002931D
		public bool IsReuseSlot
		{
			get
			{
				return this.attributes.GetMaskedAttributes(256, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(256, 0U, value);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002B137 File Offset: 0x00029337
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0002B14E File Offset: 0x0002934E
		public bool IsNewSlot
		{
			get
			{
				return this.attributes.GetMaskedAttributes(256, 256U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(256, 256U, value);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002B16C File Offset: 0x0002936C
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0002B17E File Offset: 0x0002937E
		public bool IsCheckAccessOnOverride
		{
			get
			{
				return this.attributes.GetAttributes(512);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(512, value);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0002B197 File Offset: 0x00029397
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x0002B1A9 File Offset: 0x000293A9
		public bool IsAbstract
		{
			get
			{
				return this.attributes.GetAttributes(1024);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024, value);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002B1C2 File Offset: 0x000293C2
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x0002B1D4 File Offset: 0x000293D4
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(2048);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2048, value);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0002B1ED File Offset: 0x000293ED
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0002B1FF File Offset: 0x000293FF
		public bool IsPInvokeImpl
		{
			get
			{
				return this.attributes.GetAttributes(8192);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8192, value);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002B218 File Offset: 0x00029418
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x0002B226 File Offset: 0x00029426
		public bool IsUnmanagedExport
		{
			get
			{
				return this.attributes.GetAttributes(8);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8, value);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0002B23B File Offset: 0x0002943B
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x0002B24D File Offset: 0x0002944D
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(4096);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096, value);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0002B266 File Offset: 0x00029466
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0002B278 File Offset: 0x00029478
		public bool HasSecurity
		{
			get
			{
				return this.attributes.GetAttributes(16384);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16384, value);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0002B291 File Offset: 0x00029491
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0002B2A0 File Offset: 0x000294A0
		public bool IsIL
		{
			get
			{
				return this.impl_attributes.GetMaskedAttributes(3, 0U);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetMaskedAttributes(3, 0U, value);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0002B2B6 File Offset: 0x000294B6
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x0002B2C5 File Offset: 0x000294C5
		public bool IsNative
		{
			get
			{
				return this.impl_attributes.GetMaskedAttributes(3, 1U);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetMaskedAttributes(3, 1U, value);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0002B2DB File Offset: 0x000294DB
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x0002B2EA File Offset: 0x000294EA
		public bool IsRuntime
		{
			get
			{
				return this.impl_attributes.GetMaskedAttributes(3, 3U);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetMaskedAttributes(3, 3U, value);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0002B300 File Offset: 0x00029500
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x0002B30F File Offset: 0x0002950F
		public bool IsUnmanaged
		{
			get
			{
				return this.impl_attributes.GetMaskedAttributes(4, 4U);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetMaskedAttributes(4, 4U, value);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0002B325 File Offset: 0x00029525
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0002B334 File Offset: 0x00029534
		public bool IsManaged
		{
			get
			{
				return this.impl_attributes.GetMaskedAttributes(4, 0U);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetMaskedAttributes(4, 0U, value);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0002B34A File Offset: 0x0002954A
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0002B359 File Offset: 0x00029559
		public bool IsForwardRef
		{
			get
			{
				return this.impl_attributes.GetAttributes(16);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(16, value);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0002B36F File Offset: 0x0002956F
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0002B381 File Offset: 0x00029581
		public bool IsPreserveSig
		{
			get
			{
				return this.impl_attributes.GetAttributes(128);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(128, value);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002B39A File Offset: 0x0002959A
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0002B3AC File Offset: 0x000295AC
		public bool IsInternalCall
		{
			get
			{
				return this.impl_attributes.GetAttributes(4096);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(4096, value);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0002B3C5 File Offset: 0x000295C5
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0002B3D4 File Offset: 0x000295D4
		public bool IsSynchronized
		{
			get
			{
				return this.impl_attributes.GetAttributes(32);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(32, value);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0002B3EA File Offset: 0x000295EA
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0002B3F8 File Offset: 0x000295F8
		public bool NoInlining
		{
			get
			{
				return this.impl_attributes.GetAttributes(8);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(8, value);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002B40D File Offset: 0x0002960D
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x0002B41C File Offset: 0x0002961C
		public bool NoOptimization
		{
			get
			{
				return this.impl_attributes.GetAttributes(64);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(64, value);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0002B432 File Offset: 0x00029632
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x0002B444 File Offset: 0x00029644
		public bool AggressiveInlining
		{
			get
			{
				return this.impl_attributes.GetAttributes(256);
			}
			set
			{
				this.impl_attributes = this.impl_attributes.SetAttributes(256, value);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002B45D File Offset: 0x0002965D
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x0002B466 File Offset: 0x00029666
		public bool IsSetter
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Setter);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Setter, value);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0002B470 File Offset: 0x00029670
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x0002B479 File Offset: 0x00029679
		public bool IsGetter
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Getter);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Getter, value);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0002B483 File Offset: 0x00029683
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x0002B48C File Offset: 0x0002968C
		public bool IsOther
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Other);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Other, value);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0002B496 File Offset: 0x00029696
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x0002B49F File Offset: 0x0002969F
		public bool IsAddOn
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.AddOn);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.AddOn, value);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0002B4A9 File Offset: 0x000296A9
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x0002B4B3 File Offset: 0x000296B3
		public bool IsRemoveOn
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.RemoveOn);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.RemoveOn, value);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0002B4BE File Offset: 0x000296BE
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x0002B4C8 File Offset: 0x000296C8
		public bool IsFire
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Fire);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Fire, value);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00026BD0 File Offset: 0x00024DD0
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x00026BDD File Offset: 0x00024DDD
		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0002B4D3 File Offset: 0x000296D3
		public bool IsConstructor
		{
			get
			{
				return this.IsRuntimeSpecialName && this.IsSpecialName && (this.Name == ".cctor" || this.Name == ".ctor");
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002B50B File Offset: 0x0002970B
		internal MethodDefinition()
		{
			this.token = new MetadataToken(TokenType.Method);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002B523 File Offset: 0x00029723
		public MethodDefinition(string name, MethodAttributes attributes, TypeReference returnType)
			: base(name, returnType)
		{
			this.attributes = (ushort)attributes;
			this.HasThis = !this.IsStatic;
			this.token = new MetadataToken(TokenType.Method);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00018380 File Offset: 0x00016580
		public override MethodDefinition Resolve()
		{
			return this;
		}

		// Token: 0x040003AF RID: 943
		private ushort attributes;

		// Token: 0x040003B0 RID: 944
		private ushort impl_attributes;

		// Token: 0x040003B1 RID: 945
		internal volatile bool sem_attrs_ready;

		// Token: 0x040003B2 RID: 946
		internal MethodSemanticsAttributes sem_attrs;

		// Token: 0x040003B3 RID: 947
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x040003B4 RID: 948
		private Collection<SecurityDeclaration> security_declarations;

		// Token: 0x040003B5 RID: 949
		internal uint rva;

		// Token: 0x040003B6 RID: 950
		internal PInvokeInfo pinvoke;

		// Token: 0x040003B7 RID: 951
		private Collection<MethodReference> overrides;

		// Token: 0x040003B8 RID: 952
		internal MethodBody body;

		// Token: 0x040003B9 RID: 953
		internal MethodDebugInformation debug_info;

		// Token: 0x040003BA RID: 954
		internal Collection<CustomDebugInformation> custom_infos;
	}
}

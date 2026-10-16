using System;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200007C RID: 124
	public sealed class MethodDefinition : MethodReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, ISecurityDeclarationProvider, ICustomDebugInformationProvider
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000FBC2 File Offset: 0x0000DDC2
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0000FBCA File Offset: 0x0000DDCA
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000FBEF File Offset: 0x0000DDEF
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x0000FBF7 File Offset: 0x0000DDF7
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

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000FC17 File Offset: 0x0000DE17
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x0000FC1F File Offset: 0x0000DE1F
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

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000FC3F File Offset: 0x0000DE3F
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000FC7D File Offset: 0x0000DE7D
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

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000FC86 File Offset: 0x0000DE86
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x000056A4 File Offset: 0x000038A4
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000FC93 File Offset: 0x0000DE93
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

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		public Collection<SecurityDeclaration> SecurityDeclarations
		{
			get
			{
				return this.security_declarations ?? this.GetSecurityDeclarations(ref this.security_declarations, this.Module);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000FCD6 File Offset: 0x0000DED6
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

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000FCFB File Offset: 0x0000DEFB
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000FD19 File Offset: 0x0000DF19
		public int RVA
		{
			get
			{
				return (int)this.rva;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000FD24 File Offset: 0x0000DF24
		public bool HasBody
		{
			get
			{
				return (this.attributes & 1024) == 0 && (this.attributes & 8192) == 0 && (this.impl_attributes & 4096) == 0 && (this.impl_attributes & 1) == 0 && (this.impl_attributes & 4) == 0 && (this.impl_attributes & 3) == 0;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
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
				return this.body = new MethodBody(this);
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
				}
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000FE3C File Offset: 0x0000E03C
		public MethodDebugInformation DebugInformation
		{
			get
			{
				Mixin.Read(this.Body);
				if (this.debug_info != null)
				{
					return this.debug_info;
				}
				MethodDebugInformation methodDebugInformation;
				if ((methodDebugInformation = this.debug_info) == null)
				{
					methodDebugInformation = (this.debug_info = new MethodDebugInformation(this));
				}
				return methodDebugInformation;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000FE7C File Offset: 0x0000E07C
		public bool HasPInvokeInfo
		{
			get
			{
				return this.pinvoke != null || this.IsPInvokeImpl;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000FE90 File Offset: 0x0000E090
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0000FEEF File Offset: 0x0000E0EF
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

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000FF00 File Offset: 0x0000E100
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000FF5C File Offset: 0x0000E15C
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
				return this.overrides = new Collection<MethodReference>();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
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

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000FFE5 File Offset: 0x0000E1E5
		public override Collection<GenericParameter> GenericParameters
		{
			get
			{
				return this.generic_parameters ?? this.GetGenericParameters(ref this.generic_parameters, this.Module);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00010003 File Offset: 0x0000E203
		public bool HasCustomDebugInformations
		{
			get
			{
				Mixin.Read(this.Body);
				return !this.custom_infos.IsNullOrEmpty<CustomDebugInformation>();
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00010020 File Offset: 0x0000E220
		public Collection<CustomDebugInformation> CustomDebugInformations
		{
			get
			{
				Mixin.Read(this.Body);
				Collection<CustomDebugInformation> collection;
				if ((collection = this.custom_infos) == null)
				{
					collection = (this.custom_infos = new Collection<CustomDebugInformation>());
				}
				return collection;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00010050 File Offset: 0x0000E250
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0001005F File Offset: 0x0000E25F
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

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00010075 File Offset: 0x0000E275
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00010084 File Offset: 0x0000E284
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

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001009A File Offset: 0x0000E29A
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x000100A9 File Offset: 0x0000E2A9
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

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x000100BF File Offset: 0x0000E2BF
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x000100CE File Offset: 0x0000E2CE
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

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000100E4 File Offset: 0x0000E2E4
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000100F3 File Offset: 0x0000E2F3
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

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00010109 File Offset: 0x0000E309
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00010118 File Offset: 0x0000E318
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

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0001012E File Offset: 0x0000E32E
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0001013D File Offset: 0x0000E33D
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

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00010153 File Offset: 0x0000E353
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00010162 File Offset: 0x0000E362
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

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00010178 File Offset: 0x0000E378
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00010187 File Offset: 0x0000E387
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0001019D File Offset: 0x0000E39D
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x000101AC File Offset: 0x0000E3AC
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

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000101C2 File Offset: 0x0000E3C2
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x000101D4 File Offset: 0x0000E3D4
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

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000101ED File Offset: 0x0000E3ED
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x00010200 File Offset: 0x0000E400
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

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001021A File Offset: 0x0000E41A
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00010231 File Offset: 0x0000E431
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

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001024F File Offset: 0x0000E44F
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00010261 File Offset: 0x0000E461
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

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0001027A File Offset: 0x0000E47A
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x0001028C File Offset: 0x0000E48C
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

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000102A5 File Offset: 0x0000E4A5
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x000102B7 File Offset: 0x0000E4B7
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

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000102D0 File Offset: 0x0000E4D0
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x000102E2 File Offset: 0x0000E4E2
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

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x000102FB File Offset: 0x0000E4FB
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00010309 File Offset: 0x0000E509
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

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001031E File Offset: 0x0000E51E
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00010330 File Offset: 0x0000E530
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00010349 File Offset: 0x0000E549
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0001035B File Offset: 0x0000E55B
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

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00010374 File Offset: 0x0000E574
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00010383 File Offset: 0x0000E583
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

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00010399 File Offset: 0x0000E599
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x000103A8 File Offset: 0x0000E5A8
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

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000103BE File Offset: 0x0000E5BE
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x000103CD File Offset: 0x0000E5CD
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x000103E3 File Offset: 0x0000E5E3
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x000103F2 File Offset: 0x0000E5F2
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

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00010408 File Offset: 0x0000E608
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00010417 File Offset: 0x0000E617
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

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0001042D File Offset: 0x0000E62D
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0001043C File Offset: 0x0000E63C
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

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00010452 File Offset: 0x0000E652
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00010464 File Offset: 0x0000E664
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

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001047D File Offset: 0x0000E67D
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0001048F File Offset: 0x0000E68F
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

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000104A8 File Offset: 0x0000E6A8
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x000104B7 File Offset: 0x0000E6B7
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

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000104CD File Offset: 0x0000E6CD
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x000104DB File Offset: 0x0000E6DB
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

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000104F0 File Offset: 0x0000E6F0
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x000104FF File Offset: 0x0000E6FF
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

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00010515 File Offset: 0x0000E715
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x0001051E File Offset: 0x0000E71E
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

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00010528 File Offset: 0x0000E728
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00010531 File Offset: 0x0000E731
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

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0001053B File Offset: 0x0000E73B
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00010544 File Offset: 0x0000E744
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

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001054E File Offset: 0x0000E74E
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x00010557 File Offset: 0x0000E757
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

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00010561 File Offset: 0x0000E761
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0001056B File Offset: 0x0000E76B
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00010576 File Offset: 0x0000E776
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x00010580 File Offset: 0x0000E780
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

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00004E58 File Offset: 0x00003058
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00004E65 File Offset: 0x00003065
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

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001058B File Offset: 0x0000E78B
		public bool IsConstructor
		{
			get
			{
				return this.IsRuntimeSpecialName && this.IsSpecialName && (this.Name == ".cctor" || this.Name == ".ctor");
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000105C4 File Offset: 0x0000E7C4
		internal void ReadSemantics()
		{
			if (!this.sem_attrs_ready)
			{
				ModuleDefinition module = this.Module;
				if (module != null && module.HasImage)
				{
					module.Read<MethodDefinition, MethodSemanticsAttributes>(this, (MethodDefinition method, MetadataReader reader) => reader.ReadAllSemantics(method));
				}
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00010614 File Offset: 0x0000E814
		internal MethodDefinition()
		{
			this.token = new MetadataToken(TokenType.Method);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001062C File Offset: 0x0000E82C
		public MethodDefinition(string name, MethodAttributes attributes, TypeReference returnType)
			: base(name, returnType)
		{
			this.attributes = (ushort)attributes;
			this.HasThis = !this.IsStatic;
			this.token = new MetadataToken(TokenType.Method);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00002654 File Offset: 0x00000854
		public override MethodDefinition Resolve()
		{
			return this;
		}

		// Token: 0x0400015F RID: 351
		private ushort attributes;

		// Token: 0x04000160 RID: 352
		private ushort impl_attributes;

		// Token: 0x04000161 RID: 353
		internal volatile bool sem_attrs_ready;

		// Token: 0x04000162 RID: 354
		internal MethodSemanticsAttributes sem_attrs;

		// Token: 0x04000163 RID: 355
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000164 RID: 356
		private Collection<SecurityDeclaration> security_declarations;

		// Token: 0x04000165 RID: 357
		internal uint rva;

		// Token: 0x04000166 RID: 358
		internal PInvokeInfo pinvoke;

		// Token: 0x04000167 RID: 359
		private Collection<MethodReference> overrides;

		// Token: 0x04000168 RID: 360
		internal MethodBody body;

		// Token: 0x04000169 RID: 361
		internal MethodDebugInformation debug_info;

		// Token: 0x0400016A RID: 362
		internal Collection<CustomDebugInformation> custom_infos;
	}
}

using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000042 RID: 66
	[Obsolete("PhysicMaterial has been renamed to PhysicsMaterial. Please use PhysicsMaterial instead. (UnityUpgradable) -> PhysicsMaterial", true)]
	[NativeClass(null)]
	public class PhysicMaterial : Object
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x00008F7F File Offset: 0x0000717F
		public PhysicMaterial()
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00008F7F File Offset: 0x0000717F
		public PhysicMaterial(string name)
		{
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00008F89 File Offset: 0x00007189
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x00008F91 File Offset: 0x00007191
		public float bounciness { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00008F9A File Offset: 0x0000719A
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x00008FA2 File Offset: 0x000071A2
		public float dynamicFriction { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00008FAB File Offset: 0x000071AB
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00008FB3 File Offset: 0x000071B3
		public float staticFriction { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00008FBC File Offset: 0x000071BC
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00008FC4 File Offset: 0x000071C4
		public PhysicMaterialCombine frictionCombine { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00008FCD File Offset: 0x000071CD
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00008FD5 File Offset: 0x000071D5
		public PhysicMaterialCombine bounceCombine { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00008FDE File Offset: 0x000071DE
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00008FE6 File Offset: 0x000071E6
		[Obsolete("Use PhysicMaterial.bounciness instead (UnityUpgradable) -> bounciness")]
		public float bouncyness { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00008FEF File Offset: 0x000071EF
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00008FF7 File Offset: 0x000071F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public Vector3 frictionDirection2 { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00009000 File Offset: 0x00007200
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00009008 File Offset: 0x00007208
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public float dynamicFriction2 { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00009011 File Offset: 0x00007211
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x00009019 File Offset: 0x00007219
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float staticFriction2 { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00009022 File Offset: 0x00007222
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000902A File Offset: 0x0000722A
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Vector3 frictionDirection { get; set; }
	}
}

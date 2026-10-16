using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000FD RID: 253
	[RequiredByNativeCode]
	public struct Keyframe
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x000098F4 File Offset: 0x00007AF4
		public Keyframe(float time, float value)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = 0f;
			this.m_OutTangent = 0f;
			this.m_WeightedMode = 0;
			this.m_InWeight = 0f;
			this.m_OutWeight = 0f;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00009943 File Offset: 0x00007B43
		public Keyframe(float time, float value, float inTangent, float outTangent)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
			this.m_WeightedMode = 0;
			this.m_InWeight = 0f;
			this.m_OutWeight = 0f;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00009980 File Offset: 0x00007B80
		public Keyframe(float time, float value, float inTangent, float outTangent, float inWeight, float outWeight)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
			this.m_WeightedMode = 3;
			this.m_InWeight = inWeight;
			this.m_OutWeight = outWeight;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x000099B8 File Offset: 0x00007BB8
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x000099D0 File Offset: 0x00007BD0
		public float time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x000099DC File Offset: 0x00007BDC
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x000099F4 File Offset: 0x00007BF4
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00009A00 File Offset: 0x00007C00
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00009A18 File Offset: 0x00007C18
		public float inTangent
		{
			get
			{
				return this.m_InTangent;
			}
			set
			{
				this.m_InTangent = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00009A24 File Offset: 0x00007C24
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00009A3C File Offset: 0x00007C3C
		public float outTangent
		{
			get
			{
				return this.m_OutTangent;
			}
			set
			{
				this.m_OutTangent = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00009A48 File Offset: 0x00007C48
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00009A60 File Offset: 0x00007C60
		public float inWeight
		{
			get
			{
				return this.m_InWeight;
			}
			set
			{
				this.m_InWeight = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00009A6C File Offset: 0x00007C6C
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00009A84 File Offset: 0x00007C84
		public float outWeight
		{
			get
			{
				return this.m_OutWeight;
			}
			set
			{
				this.m_OutWeight = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00009A90 File Offset: 0x00007C90
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00009AA8 File Offset: 0x00007CA8
		public WeightedMode weightedMode
		{
			get
			{
				return (WeightedMode)this.m_WeightedMode;
			}
			set
			{
				this.m_WeightedMode = (int)value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00009AB4 File Offset: 0x00007CB4
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00009ACC File Offset: 0x00007CCC
		[Obsolete("Use AnimationUtility.SetKeyLeftTangentMode, AnimationUtility.SetKeyRightTangentMode, AnimationUtility.GetKeyLeftTangentMode or AnimationUtility.GetKeyRightTangentMode instead.")]
		public int tangentMode
		{
			get
			{
				return this.tangentModeInternal;
			}
			set
			{
				this.tangentModeInternal = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00009AD8 File Offset: 0x00007CD8
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x00002925 File Offset: 0x00000B25
		internal int tangentModeInternal
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x040002EF RID: 751
		private float m_Time;

		// Token: 0x040002F0 RID: 752
		private float m_Value;

		// Token: 0x040002F1 RID: 753
		private float m_InTangent;

		// Token: 0x040002F2 RID: 754
		private float m_OutTangent;

		// Token: 0x040002F3 RID: 755
		private int m_WeightedMode;

		// Token: 0x040002F4 RID: 756
		private float m_InWeight;

		// Token: 0x040002F5 RID: 757
		private float m_OutWeight;
	}
}

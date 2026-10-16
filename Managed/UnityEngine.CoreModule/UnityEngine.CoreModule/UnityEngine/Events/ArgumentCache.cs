using System;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000340 RID: 832
	[Serializable]
	internal class ArgumentCache : ISerializationCallbackReceiver
	{
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x0004C528 File Offset: 0x0004A728
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x0004C540 File Offset: 0x0004A740
		public Object unityObjectArgument
		{
			get
			{
				return this.m_ObjectArgument;
			}
			set
			{
				this.m_ObjectArgument = value;
				this.m_ObjectArgumentAssemblyTypeName = ((value != null) ? value.GetType().AssemblyQualifiedName : string.Empty);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x0004C56C File Offset: 0x0004A76C
		public string unityObjectArgumentAssemblyTypeName
		{
			get
			{
				return this.m_ObjectArgumentAssemblyTypeName;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x0004C584 File Offset: 0x0004A784
		// (set) Token: 0x060027CB RID: 10187 RVA: 0x0004C59C File Offset: 0x0004A79C
		public int intArgument
		{
			get
			{
				return this.m_IntArgument;
			}
			set
			{
				this.m_IntArgument = value;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x0004C5A8 File Offset: 0x0004A7A8
		// (set) Token: 0x060027CD RID: 10189 RVA: 0x0004C5C0 File Offset: 0x0004A7C0
		public float floatArgument
		{
			get
			{
				return this.m_FloatArgument;
			}
			set
			{
				this.m_FloatArgument = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060027CE RID: 10190 RVA: 0x0004C5CC File Offset: 0x0004A7CC
		// (set) Token: 0x060027CF RID: 10191 RVA: 0x0004C5E4 File Offset: 0x0004A7E4
		public string stringArgument
		{
			get
			{
				return this.m_StringArgument;
			}
			set
			{
				this.m_StringArgument = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x0004C5F0 File Offset: 0x0004A7F0
		// (set) Token: 0x060027D1 RID: 10193 RVA: 0x0004C608 File Offset: 0x0004A808
		public bool boolArgument
		{
			get
			{
				return this.m_BoolArgument;
			}
			set
			{
				this.m_BoolArgument = value;
			}
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x0004C612 File Offset: 0x0004A812
		public void OnBeforeSerialize()
		{
			this.m_ObjectArgumentAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_ObjectArgumentAssemblyTypeName);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0004C612 File Offset: 0x0004A812
		public void OnAfterDeserialize()
		{
			this.m_ObjectArgumentAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_ObjectArgumentAssemblyTypeName);
		}

		// Token: 0x04000B31 RID: 2865
		[SerializeField]
		[FormerlySerializedAs("objectArgument")]
		private Object m_ObjectArgument;

		// Token: 0x04000B32 RID: 2866
		[FormerlySerializedAs("objectArgumentAssemblyTypeName")]
		[SerializeField]
		private string m_ObjectArgumentAssemblyTypeName;

		// Token: 0x04000B33 RID: 2867
		[SerializeField]
		[FormerlySerializedAs("intArgument")]
		private int m_IntArgument;

		// Token: 0x04000B34 RID: 2868
		[SerializeField]
		[FormerlySerializedAs("floatArgument")]
		private float m_FloatArgument;

		// Token: 0x04000B35 RID: 2869
		[FormerlySerializedAs("stringArgument")]
		[SerializeField]
		private string m_StringArgument;

		// Token: 0x04000B36 RID: 2870
		[SerializeField]
		private bool m_BoolArgument;
	}
}

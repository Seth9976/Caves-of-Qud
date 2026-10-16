using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements
{
	// Token: 0x020005E6 RID: 1510
	internal readonly struct VisualPanel
	{
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000A089F File Offset: 0x0009EA9F
		public static VisualPanel Null
		{
			get
			{
				return new VisualPanel(null, VisualPanelHandle.Null);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000A08AC File Offset: 0x0009EAAC
		public VisualPanelHandle Handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x000A08B4 File Offset: 0x0009EAB4
		public bool IsCreated
		{
			get
			{
				return !this.m_Handle.Equals(VisualPanelHandle.Null) && this.m_Manager.ContainsPanel(in this.m_Handle);
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000A08DC File Offset: 0x0009EADC
		internal ref VisualPanelData Data
		{
			get
			{
				return UnsafeUtility.AsRef<VisualPanelData>(this.m_Manager.GetPanelDataPtr(in this.m_Handle));
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000A08F4 File Offset: 0x0009EAF4
		public ref VisualNodeHandle RootContainer
		{
			get
			{
				return ref this.Data.RootContainer;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x000A0901 File Offset: 0x0009EB01
		public ref bool DuringLayoutPhase
		{
			get
			{
				return ref this.Data.DuringLayoutPhase;
			}
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000A090E File Offset: 0x0009EB0E
		public VisualNode GetRootContainer()
		{
			return new VisualNode(this.m_Manager, this.Data.RootContainer);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000A0926 File Offset: 0x0009EB26
		public void SetRootContainer(VisualNode node)
		{
			this.Data.RootContainer = node.Handle;
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000A093A File Offset: 0x0009EB3A
		internal VisualPanel(VisualManager manager, VisualPanelHandle handle)
		{
			this.m_Manager = manager;
			this.m_Handle = handle;
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000A094B File Offset: 0x0009EB4B
		public void Destroy()
		{
			this.m_Manager.RemovePanel(in this.m_Handle);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000A0960 File Offset: 0x0009EB60
		public BaseVisualElementPanel GetOwner()
		{
			return this.m_Manager.GetOwner(in this.m_Handle);
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000A0973 File Offset: 0x0009EB73
		public void SetOwner(BaseVisualElementPanel owner)
		{
			this.m_Manager.SetOwner(in this.m_Handle, owner);
		}

		// Token: 0x04001451 RID: 5201
		private readonly VisualManager m_Manager;

		// Token: 0x04001452 RID: 5202
		private readonly VisualPanelHandle m_Handle;
	}
}

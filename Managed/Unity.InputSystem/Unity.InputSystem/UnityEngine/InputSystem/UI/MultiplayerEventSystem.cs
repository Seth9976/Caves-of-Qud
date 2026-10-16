using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200008D RID: 141
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/UISupport.html#multiplayer-uis")]
	public class MultiplayerEventSystem : EventSystem
	{
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0003E770 File Offset: 0x0003C970
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x0003E778 File Offset: 0x0003C978
		public GameObject playerRoot
		{
			get
			{
				return this.m_PlayerRoot;
			}
			set
			{
				this.m_PlayerRoot = value;
				this.InitializePlayerRoot();
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0003E787 File Offset: 0x0003C987
		protected override void OnEnable()
		{
			base.OnEnable();
			this.InitializePlayerRoot();
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0003E795 File Offset: 0x0003C995
		protected override void OnDisable()
		{
			base.OnDisable();
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0003E7A0 File Offset: 0x0003C9A0
		private void InitializePlayerRoot()
		{
			InputSystemUIInputModule component = base.GetComponent<InputSystemUIInputModule>();
			if (component != null)
			{
				component.localMultiPlayerRoot = this.m_PlayerRoot;
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		protected override void Update()
		{
			EventSystem current = EventSystem.current;
			EventSystem.current = this;
			try
			{
				base.Update();
			}
			finally
			{
				EventSystem.current = current;
			}
		}

		// Token: 0x04000427 RID: 1063
		[Tooltip("If set, only process mouse and navigation events for any game objects which are children of this game object.")]
		[SerializeField]
		private GameObject m_PlayerRoot;
	}
}

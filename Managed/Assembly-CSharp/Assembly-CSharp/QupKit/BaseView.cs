using System;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003B4 RID: 948
	public class BaseView : BaseControl
	{
		// Token: 0x06002352 RID: 9042 RVA: 0x000B368E File Offset: 0x000B188E
		public virtual void OnCommand(string Command)
		{
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000B3690 File Offset: 0x000B1890
		public virtual void OnGUI()
		{
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000B3692 File Offset: 0x000B1892
		public virtual void Update()
		{
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000B3694 File Offset: 0x000B1894
		public virtual void OnAttach(GameObject Canvas)
		{
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000B3698 File Offset: 0x000B1898
		public void AttachTo(GameObject CanvasGroup)
		{
			base.rootObject = CanvasGroup;
			base.rootObject.SetActive(false);
			this.canvasGroup = CanvasGroup.GetComponent<CanvasGroup>();
			base.Layout = new ControlLayout(ControlAnchor.Fill);
			foreach (object obj in base.rootObject.transform)
			{
				Transform transform = (Transform)obj;
				if (!this.ChildrenByControl.ContainsKey(transform.gameObject))
				{
					base.AddChild(new AttachedControl(transform.gameObject), new ControlLayout(ControlAnchor.Custom));
				}
			}
			this.OnAttach(CanvasGroup);
			base.rootObject.SetActive(false);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000B3758 File Offset: 0x000B1958
		public void Create(string ID)
		{
			base.rootObject = new GameObject();
			base.rootObject.AddComponent<RectTransform>();
			base.rootObject.name = ID;
			this.canvasGroup = base.rootObject.AddComponent<CanvasGroup>();
			this.canvasGroup.blocksRaycasts = false;
			this.canvasGroup.interactable = false;
			base.rootObject.SetActive(false);
			base.Layout = new ControlLayout(ControlAnchor.Fill);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000B37C9 File Offset: 0x000B19C9
		public virtual void OnCreate()
		{
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000B37CB File Offset: 0x000B19CB
		public virtual void BeforeEnter()
		{
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000B37CD File Offset: 0x000B19CD
		public virtual void OnEnter()
		{
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000B37CF File Offset: 0x000B19CF
		public virtual void ShowClickthroughOverlay()
		{
			base.rootObject.SetActive(true);
			this.canvasGroup.blocksRaycasts = false;
			this.canvasGroup.interactable = false;
			base.ApplyLayout();
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000B37FB File Offset: 0x000B19FB
		public virtual void Enter()
		{
			this.BeforeEnter();
			base.rootObject.SetActive(true);
			this.canvasGroup.blocksRaycasts = true;
			this.canvasGroup.interactable = true;
			base.ApplyLayout();
			this.OnEnter();
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000B3833 File Offset: 0x000B1A33
		public virtual bool BeforeLeave()
		{
			return true;
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000B3836 File Offset: 0x000B1A36
		public virtual void OnLeave()
		{
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000B3838 File Offset: 0x000B1A38
		public virtual void Overlapped()
		{
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000B383A File Offset: 0x000B1A3A
		public virtual void Leave()
		{
			if (!this.BeforeLeave())
			{
				return;
			}
			base.rootObject.SetActive(false);
			this.canvasGroup.blocksRaycasts = false;
			this.canvasGroup.interactable = false;
			this.OnLeave();
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000B386F File Offset: 0x000B1A6F
		public virtual void DisableInput()
		{
			this.canvasGroup.interactable = false;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000B387D File Offset: 0x000B1A7D
		public virtual void EnableInput()
		{
			this.canvasGroup.interactable = true;
		}

		// Token: 0x040011D2 RID: 4562
		public LegacyViewManager views;

		// Token: 0x040011D3 RID: 4563
		public CanvasGroup canvasGroup;
	}
}

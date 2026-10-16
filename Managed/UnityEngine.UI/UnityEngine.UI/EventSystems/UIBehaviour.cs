using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000074 RID: 116
	public abstract class UIBehaviour : MonoBehaviour
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x0001BF60 File Offset: 0x0001A160
		protected virtual void Awake()
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001BF62 File Offset: 0x0001A162
		protected virtual void OnEnable()
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001BF64 File Offset: 0x0001A164
		protected virtual void Start()
		{
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001BF66 File Offset: 0x0001A166
		protected virtual void OnDisable()
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001BF68 File Offset: 0x0001A168
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001BF6A File Offset: 0x0001A16A
		public virtual bool IsActive()
		{
			return base.isActiveAndEnabled;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001BF72 File Offset: 0x0001A172
		protected virtual void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001BF74 File Offset: 0x0001A174
		protected virtual void OnBeforeTransformParentChanged()
		{
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001BF76 File Offset: 0x0001A176
		protected virtual void OnTransformParentChanged()
		{
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001BF78 File Offset: 0x0001A178
		protected virtual void OnDidApplyAnimationProperties()
		{
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001BF7A File Offset: 0x0001A17A
		protected virtual void OnCanvasGroupChanged()
		{
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001BF7C File Offset: 0x0001A17C
		protected virtual void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001BF7E File Offset: 0x0001A17E
		public bool IsDestroyed()
		{
			return this == null;
		}
	}
}

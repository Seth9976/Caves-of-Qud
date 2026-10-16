using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x020003DA RID: 986
	[RequireComponent(typeof(Toggle))]
	public class TreeViewExpander : MonoBehaviour
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x000B8F94 File Offset: 0x000B7194
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x000B8F9C File Offset: 0x000B719C
		public bool CanExpand
		{
			get
			{
				return this.m_canExpand;
			}
			set
			{
				this.m_canExpand = value;
				if (!this.m_canExpand)
				{
					if (this.m_toggle != null)
					{
						this.m_toggle.isOn = false;
						this.m_toggle.enabled = false;
					}
					this.OffGraphic.enabled = false;
					return;
				}
				if (this.m_toggle != null)
				{
					this.m_toggle.enabled = true;
					if (!this.IsOn)
					{
						this.OffGraphic.enabled = true;
					}
				}
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x000B9019 File Offset: 0x000B7219
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x000B9026 File Offset: 0x000B7226
		public bool IsOn
		{
			get
			{
				return this.m_toggle.isOn;
			}
			set
			{
				this.m_toggle.isOn = value && this.m_canExpand;
			}
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000B9040 File Offset: 0x000B7240
		private void Awake()
		{
			this.m_toggle = base.GetComponent<Toggle>();
			if (!this.m_canExpand)
			{
				this.m_toggle.isOn = false;
				this.m_toggle.enabled = false;
			}
			if (this.OffGraphic != null)
			{
				this.OffGraphic.enabled = !this.m_toggle.isOn && this.m_canExpand;
			}
			this.m_toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000B90C4 File Offset: 0x000B72C4
		private void OnEnable()
		{
			if (this.m_toggle != null)
			{
				if (this.OffGraphic != null)
				{
					this.OffGraphic.enabled = !this.m_toggle.isOn && this.m_canExpand;
				}
				if (!this.m_canExpand)
				{
					this.m_toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged));
					this.m_toggle.isOn = true;
					this.m_toggle.isOn = false;
					this.m_toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
					this.m_toggle.enabled = false;
				}
			}
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000B9175 File Offset: 0x000B7375
		private void OnDestroy()
		{
			if (this.m_toggle != null)
			{
				this.m_toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged));
			}
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000B91A4 File Offset: 0x000B73A4
		private void OnValueChanged(bool value)
		{
			if (!this.m_canExpand)
			{
				this.m_toggle.isOn = false;
				this.m_toggle.enabled = false;
			}
			if (this.OffGraphic != null)
			{
				this.OffGraphic.enabled = !value && this.m_canExpand;
			}
		}

		// Token: 0x04001263 RID: 4707
		public Graphic OffGraphic;

		// Token: 0x04001264 RID: 4708
		private Toggle m_toggle;

		// Token: 0x04001265 RID: 4709
		private bool m_canExpand;
	}
}

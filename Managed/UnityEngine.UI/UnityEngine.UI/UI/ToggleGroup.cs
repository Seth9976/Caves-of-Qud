using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200003C RID: 60
	[AddComponentMenu("UI/Toggle Group", 31)]
	[DisallowMultipleComponent]
	public class ToggleGroup : UIBehaviour
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00015CF9 File Offset: 0x00013EF9
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00015D01 File Offset: 0x00013F01
		public bool allowSwitchOff
		{
			get
			{
				return this.m_AllowSwitchOff;
			}
			set
			{
				this.m_AllowSwitchOff = value;
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00015D0A File Offset: 0x00013F0A
		protected ToggleGroup()
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00015D1D File Offset: 0x00013F1D
		protected override void Start()
		{
			this.EnsureValidState();
			base.Start();
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00015D2B File Offset: 0x00013F2B
		protected override void OnEnable()
		{
			this.EnsureValidState();
			base.OnEnable();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00015D39 File Offset: 0x00013F39
		private void ValidateToggleIsInGroup(Toggle toggle)
		{
			if (toggle == null || !this.m_Toggles.Contains(toggle))
			{
				throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[] { toggle, this }));
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00015D70 File Offset: 0x00013F70
		public void NotifyToggleOn(Toggle toggle, bool sendCallback = true)
		{
			this.ValidateToggleIsInGroup(toggle);
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				if (!(this.m_Toggles[i] == toggle))
				{
					if (sendCallback)
					{
						this.m_Toggles[i].isOn = false;
					}
					else
					{
						this.m_Toggles[i].SetIsOnWithoutNotify(false);
					}
				}
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00015DD7 File Offset: 0x00013FD7
		public void UnregisterToggle(Toggle toggle)
		{
			if (this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Remove(toggle);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00015DF4 File Offset: 0x00013FF4
		public void RegisterToggle(Toggle toggle)
		{
			if (!this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Add(toggle);
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00015E10 File Offset: 0x00014010
		public void EnsureValidState()
		{
			if (!this.allowSwitchOff && !this.AnyTogglesOn() && this.m_Toggles.Count != 0)
			{
				this.m_Toggles[0].isOn = true;
				this.NotifyToggleOn(this.m_Toggles[0], true);
			}
			IEnumerable<Toggle> enumerable = this.ActiveToggles();
			if (enumerable.Count<Toggle>() > 1)
			{
				Toggle firstActiveToggle = this.GetFirstActiveToggle();
				foreach (Toggle toggle in enumerable)
				{
					if (!(toggle == firstActiveToggle))
					{
						toggle.isOn = false;
					}
				}
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00015EBC File Offset: 0x000140BC
		public bool AnyTogglesOn()
		{
			return this.m_Toggles.Find((Toggle x) => x.isOn) != null;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00015EEE File Offset: 0x000140EE
		public IEnumerable<Toggle> ActiveToggles()
		{
			return this.m_Toggles.Where<Toggle>((Toggle x) => x.isOn);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00015F1C File Offset: 0x0001411C
		public Toggle GetFirstActiveToggle()
		{
			IEnumerable<Toggle> enumerable = this.ActiveToggles();
			if (enumerable.Count<Toggle>() <= 0)
			{
				return null;
			}
			return enumerable.First<Toggle>();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00015F44 File Offset: 0x00014144
		public void SetAllTogglesOff(bool sendCallback = true)
		{
			bool allowSwitchOff = this.m_AllowSwitchOff;
			this.m_AllowSwitchOff = true;
			if (sendCallback)
			{
				for (int i = 0; i < this.m_Toggles.Count; i++)
				{
					this.m_Toggles[i].isOn = false;
				}
			}
			else
			{
				for (int j = 0; j < this.m_Toggles.Count; j++)
				{
					this.m_Toggles[j].SetIsOnWithoutNotify(false);
				}
			}
			this.m_AllowSwitchOff = allowSwitchOff;
		}

		// Token: 0x0400017B RID: 379
		[SerializeField]
		private bool m_AllowSwitchOff;

		// Token: 0x0400017C RID: 380
		protected List<Toggle> m_Toggles = new List<Toggle>();
	}
}

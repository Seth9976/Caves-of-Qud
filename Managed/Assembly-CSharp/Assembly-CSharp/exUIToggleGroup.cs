using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public class exUIToggleGroup : exUIToggle
{
	// Token: 0x06000898 RID: 2200 RVA: 0x00037AC1 File Offset: 0x00035CC1
	public void OnCheckChanged(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onCheckChanged", this.onCheckChanged, _event);
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00037ADA File Offset: 0x00035CDA
	public override void CacheEventListeners()
	{
		base.CacheEventListeners();
		this.onCheckChanged = this.eventListenerTable["onCheckChanged"];
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00037AF8 File Offset: 0x00035CF8
	public override string[] GetEventNames()
	{
		string[] array = base.GetEventNames();
		string[] array2 = new string[array.Length + exUIToggleGroup.eventNames.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		for (int j = 0; j < exUIToggleGroup.eventNames.Length; j++)
		{
			array2[j + array.Length] = exUIToggleGroup.eventNames[j];
		}
		return array2;
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x0600089B RID: 2203 RVA: 0x00037B52 File Offset: 0x00035D52
	// (set) Token: 0x0600089C RID: 2204 RVA: 0x00037B5C File Offset: 0x00035D5C
	public int index
	{
		get
		{
			return this.index_;
		}
		set
		{
			if (this.index_ != value)
			{
				this.index_ = value;
				if (this.index_ < 0)
				{
					this.index_ = 0;
				}
				if (this.index_ > this.toggles.Count - 1)
				{
					this.index_ = this.toggles.Count - 1;
				}
				for (int i = 0; i < this.toggles.Count; i++)
				{
					exUIToggle exUIToggle = this.toggles[i];
					if (i == this.index_)
					{
						exUIToggle.isChecked = true;
					}
					else
					{
						exUIToggle.isChecked = false;
					}
				}
				this.OnCheckChanged(new exUIEvent
				{
					bubbles = false
				});
			}
		}
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00037C04 File Offset: 0x00035E04
	protected new void Awake()
	{
		base.Awake();
		for (int i = 0; i < this.toggles.Count; i++)
		{
			this.RegisterEvent(this.toggles[i]);
		}
		for (int j = 0; j < this.toggles.Count; j++)
		{
			exUIToggle exUIToggle = this.toggles[j];
			exUIToggle.isRadio = true;
			if (j == this.index_)
			{
				exUIToggle.Internal_SetChecked(true);
			}
			else
			{
				exUIToggle.Internal_SetChecked(false);
			}
		}
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00037C82 File Offset: 0x00035E82
	public void AddToggle(exUIToggle _toggle)
	{
		this.toggles.Add(_toggle);
		this.RegisterEvent(_toggle);
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00037C98 File Offset: 0x00035E98
	private void RegisterEvent(exUIToggle _toggle)
	{
		_toggle.AddEventListener("onChecked", delegate(exUIEvent _event)
		{
			this.index = this.toggles.IndexOf(_toggle);
		}, false);
	}

	// Token: 0x040005EA RID: 1514
	public new static string[] eventNames = new string[] { "onCheckChanged" };

	// Token: 0x040005EB RID: 1515
	private List<exUIEventListener> onCheckChanged;

	// Token: 0x040005EC RID: 1516
	[SerializeField]
	protected int index_;

	// Token: 0x040005ED RID: 1517
	public List<exUIToggle> toggles = new List<exUIToggle>();
}

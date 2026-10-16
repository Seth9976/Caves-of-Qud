using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class exUIToggle : exUIButton
{
	// Token: 0x0600088D RID: 2189 RVA: 0x0003793A File Offset: 0x00035B3A
	public void OnChecked(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onChecked", this.onChecked, _event);
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00037953 File Offset: 0x00035B53
	public void OnUnchecked(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onUnchecked", this.onUnchecked, _event);
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x0003796C File Offset: 0x00035B6C
	public override void CacheEventListeners()
	{
		base.CacheEventListeners();
		this.onChecked = this.eventListenerTable["onChecked"];
		this.onUnchecked = this.eventListenerTable["onUnchecked"];
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x000379A0 File Offset: 0x00035BA0
	public override string[] GetEventNames()
	{
		string[] array = base.GetEventNames();
		string[] array2 = new string[array.Length + exUIToggle.eventNames.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		for (int j = 0; j < exUIToggle.eventNames.Length; j++)
		{
			array2[j + array.Length] = exUIToggle.eventNames[j];
		}
		return array2;
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000891 RID: 2193 RVA: 0x000379FA File Offset: 0x00035BFA
	// (set) Token: 0x06000892 RID: 2194 RVA: 0x00037A04 File Offset: 0x00035C04
	public bool isChecked
	{
		get
		{
			return this.isChecked_;
		}
		set
		{
			if (this.isChecked_ != value)
			{
				this.isChecked_ = value;
				if (!this.isChecked_)
				{
					this.OnUnchecked(new exUIEvent
					{
						bubbles = false
					});
					return;
				}
				this.OnChecked(new exUIEvent
				{
					bubbles = false
				});
			}
		}
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00037A52 File Offset: 0x00035C52
	protected new void Awake()
	{
		base.Awake();
		base.AddEventListener("onClick", delegate(exUIEvent _event)
		{
			if (this.isRadio)
			{
				this.isChecked = true;
				return;
			}
			this.isChecked = !this.isChecked;
		}, false);
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x00037A72 File Offset: 0x00035C72
	public void Internal_SetChecked(bool _checked)
	{
		this.isChecked_ = _checked;
	}

	// Token: 0x040005E5 RID: 1509
	public new static string[] eventNames = new string[] { "onChecked", "onUnchecked" };

	// Token: 0x040005E6 RID: 1510
	private List<exUIEventListener> onChecked;

	// Token: 0x040005E7 RID: 1511
	private List<exUIEventListener> onUnchecked;

	// Token: 0x040005E8 RID: 1512
	[SerializeField]
	protected bool isChecked_;

	// Token: 0x040005E9 RID: 1513
	public bool isRadio;
}

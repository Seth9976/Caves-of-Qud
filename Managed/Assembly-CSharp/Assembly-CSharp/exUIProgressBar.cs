using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class exUIProgressBar : exUIControl
{
	// Token: 0x06000862 RID: 2146 RVA: 0x0003621C File Offset: 0x0003441C
	public void OnProgressChanged(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onProgressChanged", this.onProgressChanged, _event);
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00036235 File Offset: 0x00034435
	public override void CacheEventListeners()
	{
		base.CacheEventListeners();
		this.onProgressChanged = this.eventListenerTable["onProgressChanged"];
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x00036254 File Offset: 0x00034454
	public override string[] GetEventNames()
	{
		string[] array = base.GetEventNames();
		string[] array2 = new string[array.Length + exUIProgressBar.eventNames.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		for (int j = 0; j < exUIProgressBar.eventNames.Length; j++)
		{
			array2[j + array.Length] = exUIProgressBar.eventNames[j];
		}
		return array2;
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06000865 RID: 2149 RVA: 0x000362AE File Offset: 0x000344AE
	// (set) Token: 0x06000866 RID: 2150 RVA: 0x000362B8 File Offset: 0x000344B8
	public float progress
	{
		get
		{
			return this.progress_;
		}
		set
		{
			if (this.progress_ != value)
			{
				this.progress_ = Mathf.Clamp(value, 0f, 1f);
				this.UpdateBar();
				this.OnProgressChanged(new exUIRatioEvent
				{
					bubbles = false,
					ratio = this.progress_
				});
			}
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06000867 RID: 2151 RVA: 0x0003630A File Offset: 0x0003450A
	// (set) Token: 0x06000868 RID: 2152 RVA: 0x00036312 File Offset: 0x00034512
	public float barSize
	{
		get
		{
			return this.barSize_;
		}
		set
		{
			if (this.barSize_ != value)
			{
				this.barSize_ = value;
				this.UpdateBar();
			}
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0003632C File Offset: 0x0003452C
	public static void SetBarSize(exSprite _bar, float _barSize, float _progress, exUIProgressBar.Direction _direction)
	{
		if (_bar != null)
		{
			if (_direction == exUIProgressBar.Direction.Horizontal)
			{
				if (_bar.spriteType == exSpriteType.Sliced)
				{
					float num = _progress * (_barSize - _bar.leftBorderSize - _bar.rightBorderSize);
					_bar.width = num + _bar.leftBorderSize + _bar.rightBorderSize;
					return;
				}
				_bar.width = _progress * _barSize;
				return;
			}
			else
			{
				if (_bar.spriteType == exSpriteType.Sliced)
				{
					float num2 = _progress * (_barSize - _bar.topBorderSize - _bar.bottomBorderSize);
					_bar.height = num2 + _bar.topBorderSize + _bar.bottomBorderSize;
					return;
				}
				_bar.height = _progress * _barSize;
			}
		}
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x000363BC File Offset: 0x000345BC
	protected new void Awake()
	{
		base.Awake();
		Transform transform = base.transform.Find("__bar");
		if (transform)
		{
			this.bar = transform.GetComponent<exSprite>();
		}
		this.UpdateBar();
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x000363FA File Offset: 0x000345FA
	private void UpdateBar()
	{
		exUIProgressBar.SetBarSize(this.bar, this.barSize_, this.progress_, this.direction);
	}

	// Token: 0x040005BD RID: 1469
	public new static string[] eventNames = new string[] { "onProgressChanged" };

	// Token: 0x040005BE RID: 1470
	private List<exUIEventListener> onProgressChanged;

	// Token: 0x040005BF RID: 1471
	[SerializeField]
	protected float progress_;

	// Token: 0x040005C0 RID: 1472
	[SerializeField]
	protected float barSize_;

	// Token: 0x040005C1 RID: 1473
	public exUIProgressBar.Direction direction = exUIProgressBar.Direction.Horizontal;

	// Token: 0x040005C2 RID: 1474
	protected exSprite bar;

	// Token: 0x020015D2 RID: 5586
	public enum Direction
	{
		// Token: 0x040057DF RID: 22495
		Vertical,
		// Token: 0x040057E0 RID: 22496
		Horizontal
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C1 RID: 193
[RequireComponent(typeof(exSprite))]
[AddComponentMenu("ex2D/Sprite Animation")]
public class exSpriteAnimation : MonoBehaviour
{
	// Token: 0x17000108 RID: 264
	// (get) Token: 0x060007A4 RID: 1956 RVA: 0x000320EC File Offset: 0x000302EC
	public exSprite sprite
	{
		get
		{
			return this.sprite_;
		}
	}

	// Token: 0x17000109 RID: 265
	public exSpriteAnimationState this[string _name]
	{
		get
		{
			this.Init();
			exSpriteAnimationState exSpriteAnimationState;
			if (this.nameToState.TryGetValue(_name, out exSpriteAnimationState))
			{
				return exSpriteAnimationState;
			}
			return null;
		}
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0003211A File Offset: 0x0003031A
	private void Awake()
	{
		this.Init();
		if (base.enabled && this.playAutomatically && this.defaultAnimation != null)
		{
			this.Play(this.defaultAnimation.name, 0f);
		}
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x00032158 File Offset: 0x00030358
	private void LateUpdate()
	{
		if (this.curAnimation != null)
		{
			float num = Time.deltaTime * this.curAnimation.speed;
			this.Step(num);
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00032186 File Offset: 0x00030386
	private void OnDisable()
	{
		this.Stop();
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0003218E File Offset: 0x0003038E
	public void Play(string _name)
	{
		this.Play(_name, 0f);
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0003219C File Offset: 0x0003039C
	public void Play(string _name, float _time)
	{
		exSpriteAnimationState animation = this.GetAnimation(_name);
		if (animation != null)
		{
			this.Play(animation, _time);
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x000321BC File Offset: 0x000303BC
	public void PlayByFrame(string _name, int _frame)
	{
		exSpriteAnimationState animation = this.GetAnimation(_name);
		if (animation != null)
		{
			float num = 1f / animation.clip.frameRate;
			float num2 = (float)_frame * num;
			this.Play(animation, num2);
		}
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x000321F3 File Offset: 0x000303F3
	public void Stop()
	{
		this.Stop(this.curAnimation);
		this.curAnimation = null;
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x00032208 File Offset: 0x00030408
	public void Stop(exSpriteAnimationState _animState)
	{
		if (_animState != null)
		{
			if (_animState == this.curAnimation)
			{
				this.curAnimation = null;
			}
			_animState.time = 0f;
			switch (_animState.stopAction)
			{
			case exSpriteAnimationClip.StopAction.DoNothing:
				break;
			case exSpriteAnimationClip.StopAction.DefaultSprite:
				this.sprite_.textureInfo = this.defaultTextureInfo;
				return;
			case exSpriteAnimationClip.StopAction.Hide:
				this.sprite_.enabled = false;
				return;
			case exSpriteAnimationClip.StopAction.Destroy:
				global::UnityEngine.Object.Destroy(base.gameObject);
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x0003227B File Offset: 0x0003047B
	public void SetDefaultSprite()
	{
		if (this.sprite_ != null)
		{
			this.sprite_.textureInfo = this.defaultTextureInfo;
		}
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0003229C File Offset: 0x0003049C
	public void UpdateDefaultSprite(exTextureInfo _textureInfo)
	{
		this.defaultTextureInfo = _textureInfo;
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x000322A5 File Offset: 0x000304A5
	public void PlayDefault()
	{
		if (this.defaultAnimation != null)
		{
			this.Play(this.defaultAnimation.name, 0f);
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x000322CB File Offset: 0x000304CB
	public exSpriteAnimationState GetAnimation(string _name)
	{
		return this[_name];
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x000322D4 File Offset: 0x000304D4
	public exSpriteAnimationState GetCurrentAnimation()
	{
		return this.curAnimation;
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x000322DC File Offset: 0x000304DC
	public bool IsPlaying(string _name = "")
	{
		if (string.IsNullOrEmpty(_name))
		{
			return base.enabled && this.curAnimation != null;
		}
		return base.enabled && this.curAnimation != null && this.curAnimation.name == _name;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00032328 File Offset: 0x00030528
	public exSpriteAnimationClip.FrameInfo GetCurFrameInfo()
	{
		if (this.curAnimation != null && this.curIndex < this.curAnimation.clip.frameInfos.Count)
		{
			return this.curAnimation.clip.frameInfos[this.curIndex];
		}
		return null;
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00032377 File Offset: 0x00030577
	public int GetCurFrameIndex()
	{
		return this.curIndex;
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0003237F File Offset: 0x0003057F
	public exSpriteAnimationState AddAnimation(exSpriteAnimationClip _animClip)
	{
		return this.AddAnimation(_animClip.name, _animClip);
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x00032390 File Offset: 0x00030590
	public exSpriteAnimationState AddAnimation(string _name, exSpriteAnimationClip _animClip)
	{
		this.Init();
		exSpriteAnimationState exSpriteAnimationState = null;
		if (this.nameToState.TryGetValue(_name, out exSpriteAnimationState))
		{
			if (exSpriteAnimationState.clip == _animClip)
			{
				return exSpriteAnimationState;
			}
			this.animations[this.animations.IndexOf(exSpriteAnimationState.clip)] = _animClip;
		}
		else if (this.animations.IndexOf(_animClip) != -1)
		{
			exSpriteAnimationState = this.nameToState[_name];
			if (exSpriteAnimationState.clip == _animClip)
			{
				return exSpriteAnimationState;
			}
		}
		else
		{
			this.animations.Add(_animClip);
		}
		exSpriteAnimationState exSpriteAnimationState2 = new exSpriteAnimationState(_name, _animClip);
		this.nameToState[_name] = exSpriteAnimationState2;
		return exSpriteAnimationState2;
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00032428 File Offset: 0x00030628
	public void RemoveAnimation(exSpriteAnimationClip _animClip)
	{
		if (this.animations.IndexOf(_animClip) == -1)
		{
			return;
		}
		this.Init();
		this.animations.Remove(_animClip);
		this.nameToState.Remove(_animClip.name);
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0003245F File Offset: 0x0003065F
	public void Sample(exSpriteAnimationState _animState)
	{
		if (_animState != null)
		{
			if (this.curAnimation != _animState)
			{
				this.curAnimation = _animState;
				this.lastFrameIndex = -1;
			}
			this.Sample();
		}
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00032481 File Offset: 0x00030681
	public void Step(exSpriteAnimationState _animState, float _deltaTime)
	{
		if (_animState != null)
		{
			if (this.curAnimation != _animState)
			{
				this.curAnimation = _animState;
				this.lastFrameIndex = -1;
			}
			this.Step(_deltaTime);
		}
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x000324A4 File Offset: 0x000306A4
	private void Init()
	{
		if (this.nameToState == null)
		{
			this.sprite_ = base.GetComponent<exSprite>();
			this.defaultTextureInfo = this.sprite_.textureInfo;
			this.nameToState = new Dictionary<string, exSpriteAnimationState>();
			for (int i = 0; i < this.animations.Count; i++)
			{
				exSpriteAnimationClip exSpriteAnimationClip = this.animations[i];
				if (exSpriteAnimationClip != null)
				{
					exSpriteAnimationState exSpriteAnimationState = new exSpriteAnimationState(exSpriteAnimationClip);
					this.nameToState[exSpriteAnimationState.name] = exSpriteAnimationState;
					if (this.defaultAnimation == exSpriteAnimationClip)
					{
						this.curAnimation = exSpriteAnimationState;
						this.lastFrameIndex = -1;
					}
				}
			}
		}
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x00032543 File Offset: 0x00030743
	private void Play(exSpriteAnimationState _animState, float _time)
	{
		this.curAnimation = _animState;
		if (this.curAnimation != null)
		{
			this.curIndex = -1;
			this.curAnimation.time = _time;
			this.Sample();
		}
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x00032570 File Offset: 0x00030770
	private void Step(float _deltaTime)
	{
		if (this.curAnimation != null)
		{
			int num = this.curAnimation.frame;
			if (this.lastFrameIndex == num)
			{
				num++;
			}
			this.curAnimation.time += _deltaTime;
			this.Sample();
			bool flag = false;
			if (this.curAnimation.wrapMode == WrapMode.Once || this.curAnimation.wrapMode == WrapMode.Default || this.curAnimation.wrapMode == WrapMode.ClampForever)
			{
				if (this.curAnimation.speed > 0f && this.curAnimation.frame >= this.curAnimation.totalFrames)
				{
					if (this.curAnimation.wrapMode == WrapMode.ClampForever)
					{
						flag = false;
						this.curAnimation.frame = this.curAnimation.totalFrames;
						this.curAnimation.time = (float)this.curAnimation.frame / this.curAnimation.clip.frameRate;
					}
					else
					{
						flag = true;
						this.curAnimation.frame = this.curAnimation.totalFrames;
					}
				}
				else if (this.curAnimation.speed < 0f && this.curAnimation.frame < 0)
				{
					if (this.curAnimation.wrapMode == WrapMode.ClampForever)
					{
						flag = false;
						this.curAnimation.time = 0f;
						this.curAnimation.frame = 0;
					}
					else
					{
						flag = true;
						this.curAnimation.frame = 0;
					}
				}
			}
			exSpriteAnimationState exSpriteAnimationState = this.curAnimation;
			if (num <= this.curAnimation.frame)
			{
				this.curAnimation.TriggerEvents(this, num, (float)this.curAnimation.frame);
				this.lastFrameIndex = exSpriteAnimationState.frame;
			}
			if (flag)
			{
				this.Stop(exSpriteAnimationState);
				return;
			}
		}
		else
		{
			this.curIndex = -1;
		}
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x00032728 File Offset: 0x00030928
	private void Sample()
	{
		if (this.curAnimation != null)
		{
			int currentIndex = this.curAnimation.GetCurrentIndex();
			if (currentIndex >= 0 && currentIndex != this.curIndex)
			{
				this.sprite_.textureInfo = this.curAnimation.clip.frameInfos[currentIndex].textureInfo;
			}
			this.curIndex = currentIndex;
			return;
		}
		this.curIndex = -1;
	}

	// Token: 0x04000535 RID: 1333
	public exSpriteAnimationClip defaultAnimation;

	// Token: 0x04000536 RID: 1334
	public List<exSpriteAnimationClip> animations = new List<exSpriteAnimationClip>();

	// Token: 0x04000537 RID: 1335
	public bool playAutomatically = true;

	// Token: 0x04000538 RID: 1336
	private Dictionary<string, exSpriteAnimationState> nameToState;

	// Token: 0x04000539 RID: 1337
	private exSpriteAnimationState curAnimation;

	// Token: 0x0400053A RID: 1338
	private exSprite sprite_;

	// Token: 0x0400053B RID: 1339
	private exTextureInfo defaultTextureInfo;

	// Token: 0x0400053C RID: 1340
	private int lastFrameIndex = -1;

	// Token: 0x0400053D RID: 1341
	private int curIndex = -1;
}

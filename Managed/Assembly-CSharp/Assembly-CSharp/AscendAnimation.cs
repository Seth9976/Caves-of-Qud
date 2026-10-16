using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Kobold;
using Qud.UI;
using UnityEngine;
using XRL;

// Token: 0x0200017F RID: 383
public class AscendAnimation : MonoBehaviour
{
	// Token: 0x06000EC3 RID: 3779 RVA: 0x0005DC30 File Offset: 0x0005BE30
	public static Color ParseColor(string Value)
	{
		Color color = default(Color);
		if (!Value.IsNullOrEmpty())
		{
			if (Value[0] == '#')
			{
				global::UnityEngine.ColorUtility.TryParseHtmlString(Value, out color);
			}
			else
			{
				ConsoleLib.Console.ColorUtility.ColorMap.TryGetValue(Value[0], out color);
			}
		}
		return color;
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x0005DC78 File Offset: 0x0005BE78
	public static async void Play(Action OnComplete, Renderable Renderable)
	{
		bool runInBackgroundSuspend = false;
		GameObject animation = null;
		try
		{
			await The.UiContext;
			runInBackgroundSuspend = Application.runInBackground;
			Application.runInBackground = true;
			SoundManager.StopMusic("music", true, 3f);
			GameManager.OverlayRootForceoff = true;
			GameObject gameObject = GameObject.Find("UI Manager");
			AscendAnimation.uiManager = ((gameObject != null) ? gameObject.GetComponent<CanvasGroup>() : null);
			AscendAnimation._tileroot = GameObject.Find("_tileroot");
			AscendAnimation._overlayroot = GameObject.Find("_overlayroot");
			AscendAnimation.ImposterRoot = GameObject.Find("ImposterRoot");
			if (AscendAnimation.uiManager != null)
			{
				AscendAnimation.uiManager.alpha = 0f;
			}
			GameObject tileroot = AscendAnimation._tileroot;
			if (tileroot != null)
			{
				tileroot.SetActive(false);
			}
			GameObject overlayroot = AscendAnimation._overlayroot;
			if (overlayroot != null)
			{
				overlayroot.SetActive(false);
			}
			GameObject imposterRoot = AscendAnimation.ImposterRoot;
			if (imposterRoot != null)
			{
				imposterRoot.SetActive(false);
			}
			Camera.main.transform.localPosition = new Vector3(0f, 0f, 0f);
			animation = global::UnityEngine.Object.Instantiate(Resources.Load("Prefabs/UI/AscendAnimation")) as GameObject;
			animation.transform.localPosition = new Vector3(0f, 0f, 10f);
			AscendAnimation a = animation.GetComponent<AscendAnimation>();
			a.SetClimber(Renderable);
			a.Begin();
			while (!a.done)
			{
				await Task.Delay(100);
			}
			a = null;
		}
		finally
		{
			try
			{
				GameManager.OverlayRootForceoff = false;
				GameManager instance = GameManager.Instance;
				if (instance != null)
				{
					instance.PopGameView(false);
				}
				CombatJuiceManager.NoPause = false;
				if (AscendAnimation.uiManager != null)
				{
					AscendAnimation.uiManager.alpha = 1f;
				}
				GameObject tileroot2 = AscendAnimation._tileroot;
				if (tileroot2 != null)
				{
					tileroot2.SetActive(true);
				}
				GameObject overlayroot2 = AscendAnimation._overlayroot;
				if (overlayroot2 != null)
				{
					overlayroot2.SetActive(true);
				}
				GameObject imposterRoot2 = AscendAnimation.ImposterRoot;
				if (imposterRoot2 != null)
				{
					imposterRoot2.SetActive(true);
				}
				if (animation != null)
				{
					global::UnityEngine.Object.Destroy(animation);
				}
				The.Core.RenderBase(true, false);
				Application.runInBackground = runInBackgroundSuspend;
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("AscendAnimation::Complete", ex);
			}
			OnComplete();
		}
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0005DCB7 File Offset: 0x0005BEB7
	public void Awake()
	{
		this.begin = false;
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x0005DCC0 File Offset: 0x0005BEC0
	public void Begin()
	{
		this.done = false;
		this.auto = true;
		CombatJuiceManager.NoPause = true;
		this.FadeOutUI(2f);
		GameObject gameObject = GameObject.Find("_tileroot");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		GameObject gameObject2 = GameObject.Find("_overlayroot");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(false);
		}
		this.Init();
		this.Phase = 1;
		this.Speed = 10f;
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x0005DD34 File Offset: 0x0005BF34
	public void SetClimber(Renderable Renderable)
	{
		try
		{
			GameObject gameObject = this.climber[2];
			ex3DSprite2 ex3DSprite = ((gameObject != null) ? gameObject.GetComponent<ex3DSprite2>() : null);
			if (ex3DSprite != null)
			{
				exTextureInfo exTextureInfo;
				if (SpriteManager.TryGetTextureInfo(Renderable.Tile, out exTextureInfo))
				{
					ex3DSprite.textureInfo = exTextureInfo;
				}
				ex3DSprite.color = ConsoleLib.Console.ColorUtility.colorFromChar(Renderable.GetForegroundColor());
				ex3DSprite.detailcolor = ConsoleLib.Console.ColorUtility.colorFromChar(Renderable.getDetailColor());
			}
		}
		catch (Exception ex)
		{
			MetricsManager.LogException("AscendAnimation::Begin::Golem Setup", ex, "game_exception");
		}
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0005DDBC File Offset: 0x0005BFBC
	public void FadeGroup(int group, float alpha)
	{
		Color32 color;
		for (int i = 0; i < this.MaxStars; i++)
		{
			color = this.Stars[group][i].startColor;
			this.Stars[group][i].startColor = new Color32(color.r, color.g, color.b, (byte)Mathf.Lerp(0f, 255f, alpha));
		}
		color = this.line[group].startColor;
		this.line[group].startColor = new Color((float)color.r, (float)color.g, (float)color.b, alpha);
		this.line[group].endColor = new Color((float)color.r, (float)color.g, (float)color.b, alpha);
		if (this.climber[group].GetComponent<SpriteRenderer>() != null)
		{
			color = this.climber[group].GetComponent<SpriteRenderer>().color;
			this.climber[group].GetComponent<SpriteRenderer>().color = new Color((float)color.r, (float)color.g, (float)color.b, alpha);
		}
		if (this.climber[group].GetComponent<ex3DSprite2>() != null)
		{
			color = this.climber[group].GetComponent<ex3DSprite2>().detailcolor;
			this.climber[group].GetComponent<ex3DSprite2>().detailcolor = new Color((float)color.r, (float)color.g, (float)color.b, alpha);
			color = this.climber[group].GetComponent<ex3DSprite2>().color;
			this.climber[group].GetComponent<ex3DSprite2>().color = new Color((float)color.r, (float)color.g, (float)color.b, alpha);
		}
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0005DF88 File Offset: 0x0005C188
	public void Init()
	{
		for (int i = 0; i < 3; i++)
		{
			this.Stars[i] = new ParticleSystem.Particle[this.MaxStars];
			this.system[i].Stop();
			this.system[i].Clear();
			this.system[i].Emit(this.MaxStars);
			this.system[i].GetParticles(this.Stars[i]);
		}
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < this.MaxStars; k++)
			{
				this.Stars[j][k].startColor = new Color32(0, 0, 0, 0);
			}
			this.system[j].SetParticles(this.Stars[j]);
		}
		this.climber[0].transform.localPosition = new Vector3(0f, 10000f, 1f);
		for (int l = 0; l < 3; l++)
		{
			for (int m = 0; m < this.MaxStars; m++)
			{
				this.Stars[l][m].startColor = Color.white;
				this.Stars[l][m].position = new Vector3((float)global::UnityEngine.Random.Range(-Screen.width / 2, Screen.width / 2), (float)global::UnityEngine.Random.Range(-Screen.height / 2, Screen.height / 2), 100f);
				float num = Mathf.Max(0.5f, global::UnityEngine.Random.Range(10f, 50f) / 10f - (float)l);
				this.Stars[l][m].startSize = num;
				this.Stars[l][m].startColor = AscendAnimation.colors.GetRandomElement<Color32>(null);
				this.Stars[l][m].remainingLifetime = 1000f;
				this.Stars[l][m].startLifetime = 1000f;
			}
			this.system[l].SetParticles(this.Stars[l], this.MaxStars);
			this.system[l].Clear();
			this.system[l].Play();
		}
		this.FadeGroup(0, 0f);
		this.FadeGroup(1, 0f);
		this.FadeGroup(2, 0f);
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0005E1E4 File Offset: 0x0005C3E4
	private void LateUpdate()
	{
		int num = 20;
		int num2 = 25;
		int num3 = 45;
		int num4 = 55;
		Camera.main.transform.localPosition = new Vector3(0f, 0f, 0f);
		if (this.begin)
		{
			this.Phase = 0;
			this.begin = false;
			this.Begin();
		}
		this.TotalTimer += Time.deltaTime;
		if (this.Phase <= 0)
		{
			return;
		}
		if (this.Phase == 1)
		{
			if (this.TotalTimer >= (float)num)
			{
				this.TotalTimer = 0f;
				this.Phase = 2;
				SoundManager.PlayMusic("Music/Arrival", "music", false, 0f, 1f, delegate
				{
					this.Phase = 3;
					this.countdown = 0.5f;
				}, true);
			}
			else
			{
				this.FadeGroup(0, this.TotalTimer / (float)num);
			}
		}
		if (this.Phase == 3)
		{
			this.countdown -= Time.deltaTime;
			if (this.countdown < 0f)
			{
				this.Phase = 4;
				this.Speed = 10000f;
			}
		}
		this.Speed -= Time.deltaTime * 2500f;
		if (this.Speed < 10f)
		{
			this.Speed = 10f;
			if (this.Phase == 4)
			{
				this.Phase = 5;
				this.climber[0].transform.localPosition = new Vector3(0f, 0f, 1f);
			}
		}
		if (this.Phase == 4)
		{
			this.climber[0].transform.localPosition = Vector3.Lerp(new Vector3(0f, 10000f, 1f), new Vector3(0f, 0f, 1f), 1f - this.Speed / 10000f);
		}
		else if (this.Phase == 5)
		{
			this.climbTimer += Time.deltaTime;
			if (this.climbTimer >= 1f)
			{
				this.climbTimer -= 1f;
				this.climber[0].transform.localPosition += new Vector3(0f, 1f, 0f);
			}
			if (this.TotalTimer > (float)num2)
			{
				this.climber[1].transform.localPosition = new Vector3(this.climber[1].transform.localPosition.x, -100f, 1f);
				base.StartCoroutine(this.FadeOut(0, 5f));
				base.StartCoroutine(this.FadeIn(1, 10f));
				this.Phase = 6;
			}
		}
		else if (this.Phase == 6)
		{
			this.climbTimer += Time.deltaTime;
			if (this.climbTimer >= 1f)
			{
				this.climbTimer -= 1f;
				this.climber[1].transform.localPosition += new Vector3(0f, 4f, 0f);
			}
			if (this.TotalTimer > (float)num3)
			{
				this.climber[2].transform.localPosition = new Vector3(this.climber[2].transform.localPosition.x, 0f, 1f);
				base.StartCoroutine(this.FadeOut(1, 5f));
				base.StartCoroutine(this.FadeIn(2, 10f));
				this.Phase = 7;
			}
		}
		else if (this.Phase == 7)
		{
			this.climbTimer += Time.deltaTime;
			if (this.climbTimer >= 1f)
			{
				this.climbTimer -= 1f;
				this.climber[2].transform.localPosition += new Vector3(0f, 16f, 0f);
				this.climber[2].transform.localScale = new Vector3(-this.climber[2].transform.localScale.x, this.climber[2].transform.localScale.y, this.climber[2].transform.localScale.z);
			}
			if (this.TotalTimer > (float)num4)
			{
				FadeToBlack.Fade(0f, new float?((float)1), new float?((float)1), new Color?(Camera.main.backgroundColor), null);
				base.StartCoroutine(this.FadeInUI(5f));
				base.StartCoroutine(this.FadeOut(2, 5f));
				this.Phase = 8;
			}
		}
		else if (this.Phase == 8)
		{
			this.climbTimer += Time.deltaTime;
			if (this.climbTimer >= 2f)
			{
				this.climbTimer -= 2f;
				this.climber[2].transform.localPosition += new Vector3(0f, 16f, 0f);
				this.climber[2].transform.localScale = new Vector3(-this.climber[2].transform.localScale.x, this.climber[2].transform.localScale.y, this.climber[2].transform.localScale.z);
			}
		}
		for (int i = 0; i < 3; i++)
		{
			this.system[i].sizeBySpeed.yMultiplier = Mathf.Min(100f, this.Speed / 10f);
			for (int j = 0; j < this.MaxStars; j++)
			{
				ParticleSystem.Particle[] array = this.Stars[i];
				ParticleSystem.Particle[] array2 = array;
				int num5 = j;
				array2[num5].position = array2[num5].position + new Vector3(0f, (-this.Speed - array[j].startSize * 0.05f) * Time.deltaTime, 0f);
				if (array[j].position.y < (float)(-(float)Screen.height / 2))
				{
					array[j].position = new Vector3((float)global::UnityEngine.Random.Range(-Screen.width / 2, Screen.width / 2), (float)(Screen.height / 2 + global::UnityEngine.Random.Range(0, 1000)), array[j].position.z);
				}
			}
			this.system[i].SetParticles(this.Stars[i], this.MaxStars);
		}
		if (this.TotalTimer > 60f)
		{
			The.Core.RenderBase(true, false);
			this.done = true;
		}
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0005E8E7 File Offset: 0x0005CAE7
	private IEnumerator FadeOut(int group, float t)
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();
		while ((float)sw.ElapsedMilliseconds < t * 1000f)
		{
			this.FadeGroup(group, 1f - (float)sw.ElapsedMilliseconds / (t * 1000f));
			yield return new WaitForEndOfFrame();
		}
		this.FadeGroup(group, 0f);
		yield break;
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0005E904 File Offset: 0x0005CB04
	private IEnumerator FadeIn(int group, float t)
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();
		while ((float)sw.ElapsedMilliseconds < t * 1000f)
		{
			this.FadeGroup(group, (float)sw.ElapsedMilliseconds / (t * 1000f));
			yield return new WaitForEndOfFrame();
		}
		this.FadeGroup(group, 1f);
		yield break;
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x0005E921 File Offset: 0x0005CB21
	private IEnumerator FadeOutUI(float t)
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();
		while ((float)sw.ElapsedMilliseconds < t * 1000f)
		{
			if (AscendAnimation.uiManager != null)
			{
				AscendAnimation.uiManager.alpha = 1f - (float)sw.ElapsedMilliseconds / (t * 1000f);
			}
			yield return new WaitForEndOfFrame();
		}
		if (AscendAnimation.uiManager != null)
		{
			AscendAnimation.uiManager.alpha = 0f;
		}
		yield break;
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x0005E930 File Offset: 0x0005CB30
	private IEnumerator FadeInUI(float t)
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();
		while ((float)sw.ElapsedMilliseconds < t * 1000f)
		{
			if (AscendAnimation.uiManager != null)
			{
				AscendAnimation.uiManager.alpha = (float)sw.ElapsedMilliseconds / (t * 1000f);
			}
			yield return new WaitForEndOfFrame();
		}
		if (AscendAnimation.uiManager != null)
		{
			AscendAnimation.uiManager.alpha = 1f;
		}
		yield break;
	}

	// Token: 0x04000A1C RID: 2588
	public const int FADE_IN_DURATION = 18;

	// Token: 0x04000A1D RID: 2589
	public GameObject[] climber;

	// Token: 0x04000A1E RID: 2590
	public ParticleSystem[] system;

	// Token: 0x04000A1F RID: 2591
	public LineRenderer[] line;

	// Token: 0x04000A20 RID: 2592
	private ParticleSystem.Particle[][] Stars = new ParticleSystem.Particle[3][];

	// Token: 0x04000A21 RID: 2593
	public int MaxStars = 100;

	// Token: 0x04000A22 RID: 2594
	public float Speed = 10f;

	// Token: 0x04000A23 RID: 2595
	public float TotalTimer;

	// Token: 0x04000A24 RID: 2596
	public int Phase;

	// Token: 0x04000A25 RID: 2597
	public bool done;

	// Token: 0x04000A26 RID: 2598
	private static Color32[] colors = new Color32[]
	{
		new Color32(byte.MaxValue, 0, 0, byte.MaxValue),
		new Color32(0, byte.MaxValue, 0, byte.MaxValue),
		new Color32(0, 0, byte.MaxValue, byte.MaxValue),
		new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue),
		new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue),
		new Color32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue),
		new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue)
	};

	// Token: 0x04000A27 RID: 2599
	private static CanvasGroup uiManager;

	// Token: 0x04000A28 RID: 2600
	private static GameObject _tileroot;

	// Token: 0x04000A29 RID: 2601
	private static GameObject _overlayroot;

	// Token: 0x04000A2A RID: 2602
	private static GameObject ImposterRoot;

	// Token: 0x04000A2B RID: 2603
	public bool auto;

	// Token: 0x04000A2C RID: 2604
	public bool begin;

	// Token: 0x04000A2D RID: 2605
	public float countdown;

	// Token: 0x04000A2E RID: 2606
	public float climbTimer;
}

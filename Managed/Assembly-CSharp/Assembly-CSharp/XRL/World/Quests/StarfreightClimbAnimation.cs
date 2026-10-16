using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;
using XRL.Wish;

namespace XRL.World.Quests
{
	// Token: 0x02000B1F RID: 2847
	[HasWishCommand]
	public static class StarfreightClimbAnimation
	{
		// Token: 0x06007288 RID: 29320 RVA: 0x002B61FF File Offset: 0x002B43FF
		[WishCommand("climb", null)]
		public static void WishRunClimbAnimation()
		{
			AscensionSystem.WishStart();
			Task.Run(() => StarfreightClimbAnimation.RunSpindleIntroSequence(delegate
			{
				FadeToBlack.FadeIn(0f);
			})).Wait();
		}

		// Token: 0x06007289 RID: 29321 RVA: 0x002B6230 File Offset: 0x002B4430
		public static async Task RunSpindleIntroSequence(Action OnFadeOutComplete)
		{
			Thread.Sleep(10);
			Renderable renderable = new Renderable("Creatures/sw_golem_oddity.png", " ", "&y", null, 'w');
			AscensionSystem system = The.Game.GetSystem<AscensionSystem>();
			GameObject gameObject = ((system != null) ? system.Climber : null);
			if (gameObject != null)
			{
				renderable.Copy(gameObject.Render);
			}
			SemaphoreSlim complete = new SemaphoreSlim(0);
			GameManager.Instance.PushGameView("Cinematic", true);
			SoundManager.StopMusic("music", true, 3f);
			await The.UiContext;
			float num = 3f;
			float? num2 = new float?(0f);
			float? num3 = new float?((float)1);
			Color? color = new Color?(Camera.main.backgroundColor);
			FadeToBlack.Fade(num, num2, num3, null, color);
			await Task.Delay(3000);
			if (OnFadeOutComplete != null)
			{
				OnFadeOutComplete();
			}
			AscendAnimation.Play(delegate
			{
				complete.Release(1);
			}, renderable);
			Debug.Log("Arrival Starting");
			await complete.WaitAsync();
			Debug.Log("Arrival Complete");
		}
	}
}

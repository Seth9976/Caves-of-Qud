using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Qud.UI;
using XRL.World;

namespace XRL.UI.Framework
{
	// Token: 0x020014E6 RID: 5350
	public static class APIDispatch
	{
		// Token: 0x0600D124 RID: 53540 RVA: 0x004DA870 File Offset: 0x004D8A70
		public static async Task<T> RunAndWaitAsync<T>(Func<T> A)
		{
			APIDispatch.<>c__DisplayClass0_0<T> CS$<>8__locals1 = new APIDispatch.<>c__DisplayClass0_0<T>();
			CS$<>8__locals1.A = A;
			string oldView = GameManager.Instance.CurrentGameView;
			UIManager.pushSaveStack();
			try
			{
				return await NavigationController.instance.SuspendContextWhile<T>(delegate
				{
					APIDispatch.<>c__DisplayClass0_0<T>.<<RunAndWaitAsync>b__0>d <<RunAndWaitAsync>b__0>d;
					<<RunAndWaitAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
					<<RunAndWaitAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<RunAndWaitAsync>b__0>d.<>1__state = -1;
					<<RunAndWaitAsync>b__0>d.<>t__builder.Start<APIDispatch.<>c__DisplayClass0_0<T>.<<RunAndWaitAsync>b__0>d>(ref <<RunAndWaitAsync>b__0>d);
					return <<RunAndWaitAsync>b__0>d.<>t__builder.Task;
				});
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("APIDispatch (outer)", ex, "game_exception");
			}
			finally
			{
				UIManager.popSaveStack();
				ControlManager.ConsumeAllInput();
				GameManager.EnsureGameView(oldView);
			}
			return default(T);
		}

		// Token: 0x0600D125 RID: 53541 RVA: 0x004DA8B4 File Offset: 0x004D8AB4
		public static async Task RunAndWaitAsync(Action A)
		{
			APIDispatch.<>c__DisplayClass1_0 CS$<>8__locals1 = new APIDispatch.<>c__DisplayClass1_0();
			CS$<>8__locals1.A = A;
			string oldView = GameManager.Instance.CurrentGameView;
			bool wasHeld = MinEvent.UIHold;
			MinEvent.UIHold = false;
			UIManager.pushSaveStack();
			try
			{
				await NavigationController.instance.SuspendContextWhile(delegate
				{
					APIDispatch.<>c__DisplayClass1_0.<<RunAndWaitAsync>b__0>d <<RunAndWaitAsync>b__0>d;
					<<RunAndWaitAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<RunAndWaitAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<RunAndWaitAsync>b__0>d.<>1__state = -1;
					<<RunAndWaitAsync>b__0>d.<>t__builder.Start<APIDispatch.<>c__DisplayClass1_0.<<RunAndWaitAsync>b__0>d>(ref <<RunAndWaitAsync>b__0>d);
					return <<RunAndWaitAsync>b__0>d.<>t__builder.Task;
				});
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("APIDispatch (outer)", ex, "game_exception");
			}
			finally
			{
				MinEvent.UIHold = wasHeld;
				UIManager.popSaveStack();
				ControlManager.ConsumeAllInput();
				GameManager.EnsureGameView(oldView);
			}
		}
	}
}

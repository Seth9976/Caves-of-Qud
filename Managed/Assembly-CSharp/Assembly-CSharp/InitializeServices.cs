using System;
using System.Collections;
using LaundryBear;
using LaundryBear.PlatformServices.None;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000F8 RID: 248
public class InitializeServices : MonoBehaviour
{
	// Token: 0x06000962 RID: 2402 RVA: 0x0003B39A File Offset: 0x0003959A
	private void Start()
	{
		InitializeServices.AddRuntimeServices(Singleton<ServiceLocator>.Instance);
		base.StartCoroutine(this.InitializationRoutine());
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0003B3B3 File Offset: 0x000395B3
	private IEnumerator InitializationRoutine()
	{
		yield return Singleton<ServiceLocator>.Instance.Initialize(false);
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
		if (asyncOperation != null)
		{
			asyncOperation.allowSceneActivation = false;
		}
		PlatformService platformService;
		ServiceLocator.TryGetService<PlatformService>(out platformService);
		if (platformService.SetupLaunchUserAndStorage().Result)
		{
			Debug.Log("Laundry Bear's Platform Layer is ready for use.");
			if (asyncOperation != null)
			{
				asyncOperation.allowSceneActivation = true;
				yield return null;
			}
		}
		else
		{
			Debug.LogError("An error occurred while setting up the user or storage. See logs for details. Halting load.");
		}
		yield break;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0003B3BC File Offset: 0x000395BC
	public static void AddRuntimeServices(ServiceLocator instance)
	{
		IEnumerator enumerator;
		instance.AddService<Platform>(out enumerator);
	}

	// Token: 0x04000626 RID: 1574
	private const float DEBUG_TIMER = 20f;
}

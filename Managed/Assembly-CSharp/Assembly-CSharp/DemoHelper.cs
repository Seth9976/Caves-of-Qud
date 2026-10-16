using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000137 RID: 311
public class DemoHelper : MonoBehaviour
{
	// Token: 0x06000BCC RID: 3020 RVA: 0x0004EC61 File Offset: 0x0004CE61
	public void EnableObject()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x0004EC6F File Offset: 0x0004CE6F
	public void DisableObject()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x0004EC7D File Offset: 0x0004CE7D
	public void LoadNextScene(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}
}

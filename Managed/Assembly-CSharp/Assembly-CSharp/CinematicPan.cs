using System;
using System.IO;
using UnityEngine;
using XRL;

// Token: 0x02000041 RID: 65
public class CinematicPan : MonoBehaviour
{
	// Token: 0x0600022D RID: 557 RVA: 0x0000B090 File Offset: 0x00009290
	public void Begin(string id)
	{
		this.id = id;
		GameManager.MainCamera.GetComponent<LetterboxCamera>().enabled = false;
		this.camera = GameManager.MainCamera.GetComponent<Camera>();
		this.cameraTransform = this.camera.transform;
		if (File.Exists(DataManager.SavePath("pans.txt")))
		{
			string[] array = File.ReadAllLines(DataManager.SavePath("pans.txt"));
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(';', StringSplitOptions.None);
				if (array2[0] == id)
				{
					this.waitBefore = (float)Convert.ToInt32(array2[1]);
					this.waitAfter = (float)Convert.ToInt32(array2[2]);
					this.t = Convert.ToSingle(array2[3]);
					this.start = new Vector3(Convert.ToSingle(array2[4].Split(',', StringSplitOptions.None)[0]), Convert.ToSingle(array2[4].Split(',', StringSplitOptions.None)[1]), -10f);
					this.end = new Vector3(Convert.ToSingle(array2[5].Split(',', StringSplitOptions.None)[0]), Convert.ToSingle(array2[5].Split(',', StringSplitOptions.None)[1]), -10f);
					this.startZoom = Convert.ToSingle(array2[6]);
					this.endZoom = Convert.ToSingle(array2[7]);
				}
			}
		}
		if (id == "1")
		{
			this.waitBefore = 2f;
			this.waitAfter = 5f;
			this.t = 10f;
			this.start = new Vector3(GameManager.Instance.GetCellCenter(11, 22, -10).x, GameManager.Instance.GetCellCenter(11, 22, -10).y, -10f);
			this.end = new Vector3(GameManager.Instance.GetCellCenter(53, 3, -10).x, GameManager.Instance.GetCellCenter(53, 3, -10).y, -10f);
			this.startZoom = 180f;
			this.endZoom = 240f;
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000B290 File Offset: 0x00009490
	public void End()
	{
		GameManager.MainCamera.GetComponent<LetterboxCamera>().enabled = true;
		global::UnityEngine.Object.Destroy(this);
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0000B2A8 File Offset: 0x000094A8
	public void Update()
	{
		if (this.waitBefore > 0f)
		{
			this.cameraTransform.position = Vector3.Lerp(this.start, this.end, 0f);
			this.camera.orthographicSize = Mathf.Lerp(this.startZoom, this.endZoom, 0f);
			this.waitBefore -= Time.deltaTime;
			return;
		}
		if (this.elapsed < this.t)
		{
			this.elapsed += Time.deltaTime;
			this.cameraTransform.position = Vector3.Lerp(this.start, this.end, this.elapsed / this.t);
			this.camera.orthographicSize = Mathf.Lerp(this.startZoom, this.endZoom, this.elapsed / this.t);
			return;
		}
		if (this.waitAfter > 0f)
		{
			this.waitAfter -= Time.deltaTime;
			return;
		}
		this.End();
	}

	// Token: 0x04000124 RID: 292
	public string id;

	// Token: 0x04000125 RID: 293
	public float t;

	// Token: 0x04000126 RID: 294
	public float elapsed;

	// Token: 0x04000127 RID: 295
	public float waitBefore = 2f;

	// Token: 0x04000128 RID: 296
	public float waitAfter = 5f;

	// Token: 0x04000129 RID: 297
	public Vector3 start;

	// Token: 0x0400012A RID: 298
	public Vector3 end;

	// Token: 0x0400012B RID: 299
	public float startZoom;

	// Token: 0x0400012C RID: 300
	public float endZoom;

	// Token: 0x0400012D RID: 301
	public Camera camera;

	// Token: 0x0400012E RID: 302
	public Transform cameraTransform;
}

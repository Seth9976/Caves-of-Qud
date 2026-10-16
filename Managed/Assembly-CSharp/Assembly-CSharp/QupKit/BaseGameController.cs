using System;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003A2 RID: 930
	public class BaseGameController<T> : MonoBehaviour where T : class
	{
		// Token: 0x060022EC RID: 8940 RVA: 0x000B1B78 File Offset: 0x000AFD78
		public virtual void OnStart()
		{
			this.RegisterViews();
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000B1B80 File Offset: 0x000AFD80
		public virtual void RegisterViews()
		{
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000B1B82 File Offset: 0x000AFD82
		public virtual void AfterOnGUI()
		{
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000B1B84 File Offset: 0x000AFD84
		private void Start()
		{
			this.OnStart();
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000B1B8C File Offset: 0x000AFD8C
		public virtual void OnUpdate()
		{
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000B1B8E File Offset: 0x000AFD8E
		private void Update()
		{
			this.OnUpdate();
		}

		// Token: 0x0400119D RID: 4509
		public Canvas MainCanvas;
	}
}

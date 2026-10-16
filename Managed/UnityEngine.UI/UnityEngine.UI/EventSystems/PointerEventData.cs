using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000050 RID: 80
	public class PointerEventData : BaseEventData
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001819D File Offset: 0x0001639D
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x000181A5 File Offset: 0x000163A5
		public GameObject pointerEnter { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x000181AE File Offset: 0x000163AE
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x000181B6 File Offset: 0x000163B6
		public GameObject lastPress { get; private set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x000181BF File Offset: 0x000163BF
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x000181C7 File Offset: 0x000163C7
		public GameObject rawPointerPress { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x000181D0 File Offset: 0x000163D0
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x000181D8 File Offset: 0x000163D8
		public GameObject pointerDrag { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x000181E1 File Offset: 0x000163E1
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x000181E9 File Offset: 0x000163E9
		public GameObject pointerClick { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x000181F2 File Offset: 0x000163F2
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x000181FA File Offset: 0x000163FA
		public RaycastResult pointerCurrentRaycast { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00018203 File Offset: 0x00016403
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0001820B File Offset: 0x0001640B
		public RaycastResult pointerPressRaycast { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00018214 File Offset: 0x00016414
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0001821C File Offset: 0x0001641C
		public bool eligibleForClick { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00018225 File Offset: 0x00016425
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0001822D File Offset: 0x0001642D
		public int displayIndex { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00018236 File Offset: 0x00016436
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001823E File Offset: 0x0001643E
		public int pointerId { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00018247 File Offset: 0x00016447
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0001824F File Offset: 0x0001644F
		public Vector2 position { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00018258 File Offset: 0x00016458
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00018260 File Offset: 0x00016460
		public Vector2 delta { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00018269 File Offset: 0x00016469
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00018271 File Offset: 0x00016471
		public Vector2 pressPosition { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001827A File Offset: 0x0001647A
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00018282 File Offset: 0x00016482
		[Obsolete("Use either pointerCurrentRaycast.worldPosition or pointerPressRaycast.worldPosition")]
		public Vector3 worldPosition { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001828B File Offset: 0x0001648B
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00018293 File Offset: 0x00016493
		[Obsolete("Use either pointerCurrentRaycast.worldNormal or pointerPressRaycast.worldNormal")]
		public Vector3 worldNormal { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001829C File Offset: 0x0001649C
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x000182A4 File Offset: 0x000164A4
		public float clickTime { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x000182AD File Offset: 0x000164AD
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x000182B5 File Offset: 0x000164B5
		public int clickCount { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x000182BE File Offset: 0x000164BE
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x000182C6 File Offset: 0x000164C6
		public Vector2 scrollDelta { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000182CF File Offset: 0x000164CF
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x000182D7 File Offset: 0x000164D7
		public bool useDragThreshold { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000182E0 File Offset: 0x000164E0
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x000182E8 File Offset: 0x000164E8
		public bool dragging { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000182F1 File Offset: 0x000164F1
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x000182F9 File Offset: 0x000164F9
		public PointerEventData.InputButton button { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00018302 File Offset: 0x00016502
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0001830A File Offset: 0x0001650A
		public float pressure { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00018313 File Offset: 0x00016513
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x0001831B File Offset: 0x0001651B
		public float tangentialPressure { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00018324 File Offset: 0x00016524
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x0001832C File Offset: 0x0001652C
		public float altitudeAngle { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00018335 File Offset: 0x00016535
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x0001833D File Offset: 0x0001653D
		public float azimuthAngle { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00018346 File Offset: 0x00016546
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x0001834E File Offset: 0x0001654E
		public float twist { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00018357 File Offset: 0x00016557
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001835F File Offset: 0x0001655F
		public Vector2 tilt { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00018368 File Offset: 0x00016568
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00018370 File Offset: 0x00016570
		public PenStatus penStatus { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00018379 File Offset: 0x00016579
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00018381 File Offset: 0x00016581
		public Vector2 radius { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001838A File Offset: 0x0001658A
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00018392 File Offset: 0x00016592
		public Vector2 radiusVariance { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001839B File Offset: 0x0001659B
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x000183A3 File Offset: 0x000165A3
		public bool fullyExited { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000183AC File Offset: 0x000165AC
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x000183B4 File Offset: 0x000165B4
		public bool reentered { get; set; }

		// Token: 0x0600057C RID: 1404 RVA: 0x000183C0 File Offset: 0x000165C0
		public PointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.eligibleForClick = false;
			this.displayIndex = 0;
			this.pointerId = -1;
			this.position = Vector2.zero;
			this.delta = Vector2.zero;
			this.pressPosition = Vector2.zero;
			this.clickTime = 0f;
			this.clickCount = 0;
			this.scrollDelta = Vector2.zero;
			this.useDragThreshold = true;
			this.dragging = false;
			this.button = PointerEventData.InputButton.Left;
			this.pressure = 0f;
			this.tangentialPressure = 0f;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.twist = 0f;
			this.tilt = new Vector2(0f, 0f);
			this.penStatus = PenStatus.None;
			this.radius = Vector2.zero;
			this.radiusVariance = Vector2.zero;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000184B0 File Offset: 0x000166B0
		public bool IsPointerMoving()
		{
			return this.delta.sqrMagnitude > 0f;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000184D4 File Offset: 0x000166D4
		public bool IsScrolling()
		{
			return this.scrollDelta.sqrMagnitude > 0f;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000184F6 File Offset: 0x000166F6
		public Camera enterEventCamera
		{
			get
			{
				if (!(this.pointerCurrentRaycast.module == null))
				{
					return this.pointerCurrentRaycast.module.eventCamera;
				}
				return null;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001851D File Offset: 0x0001671D
		public Camera pressEventCamera
		{
			get
			{
				if (!(this.pointerPressRaycast.module == null))
				{
					return this.pointerPressRaycast.module.eventCamera;
				}
				return null;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00018544 File Offset: 0x00016744
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0001854C File Offset: 0x0001674C
		public GameObject pointerPress
		{
			get
			{
				return this.m_PointerPress;
			}
			set
			{
				if (this.m_PointerPress == value)
				{
					return;
				}
				this.lastPress = this.m_PointerPress;
				this.m_PointerPress = value;
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00018570 File Offset: 0x00016770
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Position</b>: " + this.position.ToString());
			stringBuilder.AppendLine("<b>delta</b>: " + this.delta.ToString());
			stringBuilder.AppendLine("<b>eligibleForClick</b>: " + this.eligibleForClick.ToString());
			string text = "<b>pointerEnter</b>: ";
			GameObject pointerEnter = this.pointerEnter;
			stringBuilder.AppendLine(text + ((pointerEnter != null) ? pointerEnter.ToString() : null));
			string text2 = "<b>pointerPress</b>: ";
			GameObject pointerPress = this.pointerPress;
			stringBuilder.AppendLine(text2 + ((pointerPress != null) ? pointerPress.ToString() : null));
			string text3 = "<b>lastPointerPress</b>: ";
			GameObject lastPress = this.lastPress;
			stringBuilder.AppendLine(text3 + ((lastPress != null) ? lastPress.ToString() : null));
			string text4 = "<b>pointerDrag</b>: ";
			GameObject pointerDrag = this.pointerDrag;
			stringBuilder.AppendLine(text4 + ((pointerDrag != null) ? pointerDrag.ToString() : null));
			stringBuilder.AppendLine("<b>Use Drag Threshold</b>: " + this.useDragThreshold.ToString());
			stringBuilder.AppendLine("<b>Current Raycast:</b>");
			stringBuilder.AppendLine(this.pointerCurrentRaycast.ToString());
			stringBuilder.AppendLine("<b>Press Raycast:</b>");
			stringBuilder.AppendLine(this.pointerPressRaycast.ToString());
			stringBuilder.AppendLine("<b>Display Index:</b>");
			stringBuilder.AppendLine(this.displayIndex.ToString());
			stringBuilder.AppendLine("<b>pressure</b>: " + this.pressure.ToString());
			stringBuilder.AppendLine("<b>tangentialPressure</b>: " + this.tangentialPressure.ToString());
			stringBuilder.AppendLine("<b>altitudeAngle</b>: " + this.altitudeAngle.ToString());
			stringBuilder.AppendLine("<b>azimuthAngle</b>: " + this.azimuthAngle.ToString());
			stringBuilder.AppendLine("<b>twist</b>: " + this.twist.ToString());
			stringBuilder.AppendLine("<b>tilt</b>: " + this.tilt.ToString());
			stringBuilder.AppendLine("<b>penStatus</b>: " + this.penStatus.ToString());
			stringBuilder.AppendLine("<b>radius</b>: " + this.radius.ToString());
			stringBuilder.AppendLine("<b>radiusVariance</b>: " + this.radiusVariance.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x040001B1 RID: 433
		private GameObject m_PointerPress;

		// Token: 0x040001B8 RID: 440
		public List<GameObject> hovered = new List<GameObject>();

		// Token: 0x020000BF RID: 191
		public enum InputButton
		{
			// Token: 0x04000347 RID: 839
			Left,
			// Token: 0x04000348 RID: 840
			Right,
			// Token: 0x04000349 RID: 841
			Middle
		}

		// Token: 0x020000C0 RID: 192
		public enum FramePressState
		{
			// Token: 0x0400034B RID: 843
			Pressed,
			// Token: 0x0400034C RID: 844
			Released,
			// Token: 0x0400034D RID: 845
			PressedAndReleased,
			// Token: 0x0400034E RID: 846
			NotChanged
		}
	}
}

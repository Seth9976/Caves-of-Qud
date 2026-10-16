using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200003D RID: 61
	[InputControlLayout(stateType = typeof(KeyboardState), isGenericTypeOfDevice = true)]
	public class Keyboard : InputDevice, ITextInputReceiver, IEventPreProcessor
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060004F4 RID: 1268 RVA: 0x00014D22 File Offset: 0x00012F22
		// (remove) Token: 0x060004F5 RID: 1269 RVA: 0x00014D4D File Offset: 0x00012F4D
		public event Action<char> onTextInput
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.m_TextInputListeners.Contains(value))
				{
					this.m_TextInputListeners.Append(value);
				}
			}
			remove
			{
				this.m_TextInputListeners.Remove(value);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060004F6 RID: 1270 RVA: 0x00014D5B File Offset: 0x00012F5B
		// (remove) Token: 0x060004F7 RID: 1271 RVA: 0x00014D86 File Offset: 0x00012F86
		public event Action<IMECompositionString> onIMECompositionChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.m_ImeCompositionListeners.Contains(value))
				{
					this.m_ImeCompositionListeners.Append(value);
				}
			}
			remove
			{
				this.m_ImeCompositionListeners.Remove(value);
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00014D94 File Offset: 0x00012F94
		public void SetIMEEnabled(bool enabled)
		{
			EnableIMECompositionCommand enableIMECompositionCommand = EnableIMECompositionCommand.Create(enabled);
			base.ExecuteCommand<EnableIMECompositionCommand>(ref enableIMECompositionCommand);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00014DB4 File Offset: 0x00012FB4
		public void SetIMECursorPosition(Vector2 position)
		{
			SetIMECursorPositionCommand setIMECursorPositionCommand = SetIMECursorPositionCommand.Create(position);
			base.ExecuteCommand<SetIMECursorPositionCommand>(ref setIMECursorPositionCommand);
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00014DD1 File Offset: 0x00012FD1
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x00014DDF File Offset: 0x00012FDF
		public string keyboardLayout
		{
			get
			{
				base.RefreshConfigurationIfNeeded();
				return this.m_KeyboardLayoutName;
			}
			protected set
			{
				this.m_KeyboardLayoutName = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00014DE8 File Offset: 0x00012FE8
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x00014DF0 File Offset: 0x00012FF0
		public AnyKeyControl anyKey { get; protected set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00014DF9 File Offset: 0x00012FF9
		public KeyControl spaceKey
		{
			get
			{
				return this[Key.Space];
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00014E02 File Offset: 0x00013002
		public KeyControl enterKey
		{
			get
			{
				return this[Key.Enter];
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00014E0B File Offset: 0x0001300B
		public KeyControl tabKey
		{
			get
			{
				return this[Key.Tab];
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00014E14 File Offset: 0x00013014
		public KeyControl backquoteKey
		{
			get
			{
				return this[Key.Backquote];
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00014E1D File Offset: 0x0001301D
		public KeyControl quoteKey
		{
			get
			{
				return this[Key.Quote];
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00014E26 File Offset: 0x00013026
		public KeyControl semicolonKey
		{
			get
			{
				return this[Key.Semicolon];
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00014E2F File Offset: 0x0001302F
		public KeyControl commaKey
		{
			get
			{
				return this[Key.Comma];
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00014E38 File Offset: 0x00013038
		public KeyControl periodKey
		{
			get
			{
				return this[Key.Period];
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00014E41 File Offset: 0x00013041
		public KeyControl slashKey
		{
			get
			{
				return this[Key.Slash];
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00014E4B File Offset: 0x0001304B
		public KeyControl backslashKey
		{
			get
			{
				return this[Key.Backslash];
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00014E55 File Offset: 0x00013055
		public KeyControl leftBracketKey
		{
			get
			{
				return this[Key.LeftBracket];
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00014E5F File Offset: 0x0001305F
		public KeyControl rightBracketKey
		{
			get
			{
				return this[Key.RightBracket];
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00014E69 File Offset: 0x00013069
		public KeyControl minusKey
		{
			get
			{
				return this[Key.Minus];
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00014E73 File Offset: 0x00013073
		public KeyControl equalsKey
		{
			get
			{
				return this[Key.Equals];
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00014E7D File Offset: 0x0001307D
		public KeyControl aKey
		{
			get
			{
				return this[Key.A];
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00014E87 File Offset: 0x00013087
		public KeyControl bKey
		{
			get
			{
				return this[Key.B];
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00014E91 File Offset: 0x00013091
		public KeyControl cKey
		{
			get
			{
				return this[Key.C];
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00014E9B File Offset: 0x0001309B
		public KeyControl dKey
		{
			get
			{
				return this[Key.D];
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00014EA5 File Offset: 0x000130A5
		public KeyControl eKey
		{
			get
			{
				return this[Key.E];
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00014EAF File Offset: 0x000130AF
		public KeyControl fKey
		{
			get
			{
				return this[Key.F];
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00014EB9 File Offset: 0x000130B9
		public KeyControl gKey
		{
			get
			{
				return this[Key.G];
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00014EC3 File Offset: 0x000130C3
		public KeyControl hKey
		{
			get
			{
				return this[Key.H];
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00014ECD File Offset: 0x000130CD
		public KeyControl iKey
		{
			get
			{
				return this[Key.I];
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00014ED7 File Offset: 0x000130D7
		public KeyControl jKey
		{
			get
			{
				return this[Key.J];
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00014EE1 File Offset: 0x000130E1
		public KeyControl kKey
		{
			get
			{
				return this[Key.K];
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00014EEB File Offset: 0x000130EB
		public KeyControl lKey
		{
			get
			{
				return this[Key.L];
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00014EF5 File Offset: 0x000130F5
		public KeyControl mKey
		{
			get
			{
				return this[Key.M];
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00014EFF File Offset: 0x000130FF
		public KeyControl nKey
		{
			get
			{
				return this[Key.N];
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00014F09 File Offset: 0x00013109
		public KeyControl oKey
		{
			get
			{
				return this[Key.O];
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00014F13 File Offset: 0x00013113
		public KeyControl pKey
		{
			get
			{
				return this[Key.P];
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00014F1D File Offset: 0x0001311D
		public KeyControl qKey
		{
			get
			{
				return this[Key.Q];
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00014F27 File Offset: 0x00013127
		public KeyControl rKey
		{
			get
			{
				return this[Key.R];
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00014F31 File Offset: 0x00013131
		public KeyControl sKey
		{
			get
			{
				return this[Key.S];
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00014F3B File Offset: 0x0001313B
		public KeyControl tKey
		{
			get
			{
				return this[Key.T];
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00014F45 File Offset: 0x00013145
		public KeyControl uKey
		{
			get
			{
				return this[Key.U];
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00014F4F File Offset: 0x0001314F
		public KeyControl vKey
		{
			get
			{
				return this[Key.V];
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00014F59 File Offset: 0x00013159
		public KeyControl wKey
		{
			get
			{
				return this[Key.W];
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00014F63 File Offset: 0x00013163
		public KeyControl xKey
		{
			get
			{
				return this[Key.X];
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00014F6D File Offset: 0x0001316D
		public KeyControl yKey
		{
			get
			{
				return this[Key.Y];
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00014F77 File Offset: 0x00013177
		public KeyControl zKey
		{
			get
			{
				return this[Key.Z];
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00014F81 File Offset: 0x00013181
		public KeyControl digit1Key
		{
			get
			{
				return this[Key.Digit1];
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00014F8B File Offset: 0x0001318B
		public KeyControl digit2Key
		{
			get
			{
				return this[Key.Digit2];
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00014F95 File Offset: 0x00013195
		public KeyControl digit3Key
		{
			get
			{
				return this[Key.Digit3];
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00014F9F File Offset: 0x0001319F
		public KeyControl digit4Key
		{
			get
			{
				return this[Key.Digit4];
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00014FA9 File Offset: 0x000131A9
		public KeyControl digit5Key
		{
			get
			{
				return this[Key.Digit5];
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00014FB3 File Offset: 0x000131B3
		public KeyControl digit6Key
		{
			get
			{
				return this[Key.Digit6];
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00014FBD File Offset: 0x000131BD
		public KeyControl digit7Key
		{
			get
			{
				return this[Key.Digit7];
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00014FC7 File Offset: 0x000131C7
		public KeyControl digit8Key
		{
			get
			{
				return this[Key.Digit8];
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00014FD1 File Offset: 0x000131D1
		public KeyControl digit9Key
		{
			get
			{
				return this[Key.Digit9];
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00014FDB File Offset: 0x000131DB
		public KeyControl digit0Key
		{
			get
			{
				return this[Key.Digit0];
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00014FE5 File Offset: 0x000131E5
		public KeyControl leftShiftKey
		{
			get
			{
				return this[Key.LeftShift];
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00014FEF File Offset: 0x000131EF
		public KeyControl rightShiftKey
		{
			get
			{
				return this[Key.RightShift];
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00014FF9 File Offset: 0x000131F9
		public KeyControl leftAltKey
		{
			get
			{
				return this[Key.LeftAlt];
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00015003 File Offset: 0x00013203
		public KeyControl rightAltKey
		{
			get
			{
				return this[Key.RightAlt];
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001500D File Offset: 0x0001320D
		public KeyControl leftCtrlKey
		{
			get
			{
				return this[Key.LeftCtrl];
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00015017 File Offset: 0x00013217
		public KeyControl rightCtrlKey
		{
			get
			{
				return this[Key.RightCtrl];
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00015021 File Offset: 0x00013221
		public KeyControl leftMetaKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001502B File Offset: 0x0001322B
		public KeyControl rightMetaKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00015035 File Offset: 0x00013235
		public KeyControl leftWindowsKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001503F File Offset: 0x0001323F
		public KeyControl rightWindowsKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00015049 File Offset: 0x00013249
		public KeyControl leftAppleKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00015053 File Offset: 0x00013253
		public KeyControl rightAppleKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001505D File Offset: 0x0001325D
		public KeyControl leftCommandKey
		{
			get
			{
				return this[Key.LeftMeta];
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00015067 File Offset: 0x00013267
		public KeyControl rightCommandKey
		{
			get
			{
				return this[Key.RightMeta];
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00015071 File Offset: 0x00013271
		public KeyControl contextMenuKey
		{
			get
			{
				return this[Key.ContextMenu];
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001507B File Offset: 0x0001327B
		public KeyControl escapeKey
		{
			get
			{
				return this[Key.Escape];
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00015085 File Offset: 0x00013285
		public KeyControl leftArrowKey
		{
			get
			{
				return this[Key.LeftArrow];
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001508F File Offset: 0x0001328F
		public KeyControl rightArrowKey
		{
			get
			{
				return this[Key.RightArrow];
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00015099 File Offset: 0x00013299
		public KeyControl upArrowKey
		{
			get
			{
				return this[Key.UpArrow];
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x000150A3 File Offset: 0x000132A3
		public KeyControl downArrowKey
		{
			get
			{
				return this[Key.DownArrow];
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x000150AD File Offset: 0x000132AD
		public KeyControl backspaceKey
		{
			get
			{
				return this[Key.Backspace];
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000150B7 File Offset: 0x000132B7
		public KeyControl pageDownKey
		{
			get
			{
				return this[Key.PageDown];
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x000150C1 File Offset: 0x000132C1
		public KeyControl pageUpKey
		{
			get
			{
				return this[Key.PageUp];
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000150CB File Offset: 0x000132CB
		public KeyControl homeKey
		{
			get
			{
				return this[Key.Home];
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x000150D5 File Offset: 0x000132D5
		public KeyControl endKey
		{
			get
			{
				return this[Key.End];
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x000150DF File Offset: 0x000132DF
		public KeyControl insertKey
		{
			get
			{
				return this[Key.Insert];
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x000150E9 File Offset: 0x000132E9
		public KeyControl deleteKey
		{
			get
			{
				return this[Key.Delete];
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x000150F3 File Offset: 0x000132F3
		public KeyControl capsLockKey
		{
			get
			{
				return this[Key.CapsLock];
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x000150FD File Offset: 0x000132FD
		public KeyControl scrollLockKey
		{
			get
			{
				return this[Key.ScrollLock];
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00015107 File Offset: 0x00013307
		public KeyControl numLockKey
		{
			get
			{
				return this[Key.NumLock];
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00015111 File Offset: 0x00013311
		public KeyControl printScreenKey
		{
			get
			{
				return this[Key.PrintScreen];
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0001511B File Offset: 0x0001331B
		public KeyControl pauseKey
		{
			get
			{
				return this[Key.Pause];
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00015125 File Offset: 0x00013325
		public KeyControl numpadEnterKey
		{
			get
			{
				return this[Key.NumpadEnter];
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0001512F File Offset: 0x0001332F
		public KeyControl numpadDivideKey
		{
			get
			{
				return this[Key.NumpadDivide];
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00015139 File Offset: 0x00013339
		public KeyControl numpadMultiplyKey
		{
			get
			{
				return this[Key.NumpadMultiply];
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00015143 File Offset: 0x00013343
		public KeyControl numpadMinusKey
		{
			get
			{
				return this[Key.NumpadMinus];
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001514D File Offset: 0x0001334D
		public KeyControl numpadPlusKey
		{
			get
			{
				return this[Key.NumpadPlus];
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00015157 File Offset: 0x00013357
		public KeyControl numpadPeriodKey
		{
			get
			{
				return this[Key.NumpadPeriod];
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00015161 File Offset: 0x00013361
		public KeyControl numpadEqualsKey
		{
			get
			{
				return this[Key.NumpadEquals];
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001516B File Offset: 0x0001336B
		public KeyControl numpad0Key
		{
			get
			{
				return this[Key.Numpad0];
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00015175 File Offset: 0x00013375
		public KeyControl numpad1Key
		{
			get
			{
				return this[Key.Numpad1];
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001517F File Offset: 0x0001337F
		public KeyControl numpad2Key
		{
			get
			{
				return this[Key.Numpad2];
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00015189 File Offset: 0x00013389
		public KeyControl numpad3Key
		{
			get
			{
				return this[Key.Numpad3];
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00015193 File Offset: 0x00013393
		public KeyControl numpad4Key
		{
			get
			{
				return this[Key.Numpad4];
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001519D File Offset: 0x0001339D
		public KeyControl numpad5Key
		{
			get
			{
				return this[Key.Numpad5];
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000151A7 File Offset: 0x000133A7
		public KeyControl numpad6Key
		{
			get
			{
				return this[Key.Numpad6];
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x000151B1 File Offset: 0x000133B1
		public KeyControl numpad7Key
		{
			get
			{
				return this[Key.Numpad7];
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000151BB File Offset: 0x000133BB
		public KeyControl numpad8Key
		{
			get
			{
				return this[Key.Numpad8];
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000151C5 File Offset: 0x000133C5
		public KeyControl numpad9Key
		{
			get
			{
				return this[Key.Numpad9];
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x000151CF File Offset: 0x000133CF
		public KeyControl f1Key
		{
			get
			{
				return this[Key.F1];
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000151D9 File Offset: 0x000133D9
		public KeyControl f2Key
		{
			get
			{
				return this[Key.F2];
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000151E3 File Offset: 0x000133E3
		public KeyControl f3Key
		{
			get
			{
				return this[Key.F3];
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000151ED File Offset: 0x000133ED
		public KeyControl f4Key
		{
			get
			{
				return this[Key.F4];
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000151F7 File Offset: 0x000133F7
		public KeyControl f5Key
		{
			get
			{
				return this[Key.F5];
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00015201 File Offset: 0x00013401
		public KeyControl f6Key
		{
			get
			{
				return this[Key.F6];
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001520B File Offset: 0x0001340B
		public KeyControl f7Key
		{
			get
			{
				return this[Key.F7];
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00015215 File Offset: 0x00013415
		public KeyControl f8Key
		{
			get
			{
				return this[Key.F8];
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001521F File Offset: 0x0001341F
		public KeyControl f9Key
		{
			get
			{
				return this[Key.F9];
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00015229 File Offset: 0x00013429
		public KeyControl f10Key
		{
			get
			{
				return this[Key.F10];
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00015233 File Offset: 0x00013433
		public KeyControl f11Key
		{
			get
			{
				return this[Key.F11];
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001523D File Offset: 0x0001343D
		public KeyControl f12Key
		{
			get
			{
				return this[Key.F12];
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00015247 File Offset: 0x00013447
		public KeyControl oem1Key
		{
			get
			{
				return this[Key.OEM1];
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00015251 File Offset: 0x00013451
		public KeyControl oem2Key
		{
			get
			{
				return this[Key.OEM2];
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0001525B File Offset: 0x0001345B
		public KeyControl oem3Key
		{
			get
			{
				return this[Key.OEM3];
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00015265 File Offset: 0x00013465
		public KeyControl oem4Key
		{
			get
			{
				return this[Key.OEM4];
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001526F File Offset: 0x0001346F
		public KeyControl oem5Key
		{
			get
			{
				return this[Key.OEM5];
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00015279 File Offset: 0x00013479
		public KeyControl f13Key
		{
			get
			{
				return this[Key.F13];
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00015283 File Offset: 0x00013483
		public KeyControl f14Key
		{
			get
			{
				return this[Key.F14];
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001528D File Offset: 0x0001348D
		public KeyControl f15Key
		{
			get
			{
				return this[Key.F15];
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00015297 File Offset: 0x00013497
		public KeyControl f16Key
		{
			get
			{
				return this[Key.F16];
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000152A1 File Offset: 0x000134A1
		public KeyControl f17Key
		{
			get
			{
				return this[Key.F17];
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x000152AB File Offset: 0x000134AB
		public KeyControl f18Key
		{
			get
			{
				return this[Key.F18];
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000152B5 File Offset: 0x000134B5
		public KeyControl f19Key
		{
			get
			{
				return this[Key.F19];
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x000152BF File Offset: 0x000134BF
		public KeyControl f20Key
		{
			get
			{
				return this[Key.F20];
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000152C9 File Offset: 0x000134C9
		public KeyControl f21Key
		{
			get
			{
				return this[Key.F21];
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x000152D3 File Offset: 0x000134D3
		public KeyControl f22Key
		{
			get
			{
				return this[Key.F22];
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000152DD File Offset: 0x000134DD
		public KeyControl f23Key
		{
			get
			{
				return this[Key.F23];
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x000152E7 File Offset: 0x000134E7
		public KeyControl f24Key
		{
			get
			{
				return this[Key.F24];
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x000152F1 File Offset: 0x000134F1
		public KeyControl mediaPlayPause
		{
			get
			{
				return this[Key.MediaPlayPause];
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000152FB File Offset: 0x000134FB
		public KeyControl mediaRewind
		{
			get
			{
				return this[Key.MediaRewind];
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00015305 File Offset: 0x00013505
		public KeyControl mediaForward
		{
			get
			{
				return this[Key.MediaForward];
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001530F File Offset: 0x0001350F
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00015317 File Offset: 0x00013517
		public ButtonControl shiftKey { get; protected set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00015320 File Offset: 0x00013520
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x00015328 File Offset: 0x00013528
		public ButtonControl ctrlKey { get; protected set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00015331 File Offset: 0x00013531
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x00015339 File Offset: 0x00013539
		public ButtonControl altKey { get; protected set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00015342 File Offset: 0x00013542
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0001534A File Offset: 0x0001354A
		public ButtonControl imeSelected { get; protected set; }

		// Token: 0x170001E2 RID: 482
		public KeyControl this[Key key]
		{
			get
			{
				int num = key - Key.Space;
				if (num < 0 || num >= this.m_Keys.Length)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0}: {1}", "key", key));
				}
				return this.m_Keys[num];
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00015397 File Offset: 0x00013597
		public ReadOnlyArray<KeyControl> allKeys
		{
			get
			{
				return new ReadOnlyArray<KeyControl>(this.m_Keys);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000153A4 File Offset: 0x000135A4
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x000153AB File Offset: 0x000135AB
		public static Keyboard current { get; private set; }

		// Token: 0x0600058D RID: 1421 RVA: 0x000153B3 File Offset: 0x000135B3
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Keyboard.current = this;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000153C1 File Offset: 0x000135C1
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Keyboard.current == this)
			{
				Keyboard.current = null;
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000153D8 File Offset: 0x000135D8
		protected override void FinishSetup()
		{
			string[] array = new string[]
			{
				"space", "enter", "tab", "backquote", "quote", "semicolon", "comma", "period", "slash", "backslash",
				"leftbracket", "rightbracket", "minus", "equals", "a", "b", "c", "d", "e", "f",
				"g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
				"q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
				"1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
				"leftshift", "rightshift", "leftalt", "rightalt", "leftctrl", "rightctrl", "leftmeta", "rightmeta", "contextmenu", "escape",
				"leftarrow", "rightarrow", "uparrow", "downarrow", "backspace", "pagedown", "pageup", "home", "end", "insert",
				"delete", "capslock", "numlock", "printscreen", "scrolllock", "pause", "numpadenter", "numpaddivide", "numpadmultiply", "numpadplus",
				"numpadminus", "numpadperiod", "numpadequals", "numpad0", "numpad1", "numpad2", "numpad3", "numpad4", "numpad5", "numpad6",
				"numpad7", "numpad8", "numpad9", "f1", "f2", "f3", "f4", "f5", "f6", "f7",
				"f8", "f9", "f10", "f11", "f12", "oem1", "oem2", "oem3", "oem4", "oem5",
				"IMESelectedObsoleteKey", "f13", "f14", "f15", "f16", "f17", "f18", "f19", "f20", "f21",
				"f22", "f23", "f24", "mediaPlayPause", "mediaRewind", "mediaForward"
			};
			this.m_Keys = new KeyControl[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.m_Keys[i] = base.GetChildControl<KeyControl>(array[i]);
				this.m_Keys[i].keyCode = i + Key.Space;
			}
			this.anyKey = base.GetChildControl<AnyKeyControl>("anyKey");
			this.shiftKey = base.GetChildControl<ButtonControl>("shift");
			this.ctrlKey = base.GetChildControl<ButtonControl>("ctrl");
			this.altKey = base.GetChildControl<ButtonControl>("alt");
			this.imeSelected = base.GetChildControl<ButtonControl>("IMESelected");
			base.FinishSetup();
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000158EC File Offset: 0x00013AEC
		protected override void RefreshConfiguration()
		{
			this.keyboardLayout = null;
			QueryKeyboardLayoutCommand queryKeyboardLayoutCommand = QueryKeyboardLayoutCommand.Create();
			if (base.ExecuteCommand<QueryKeyboardLayoutCommand>(ref queryKeyboardLayoutCommand) >= 0L)
			{
				this.keyboardLayout = queryKeyboardLayoutCommand.ReadLayoutName();
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00015920 File Offset: 0x00013B20
		public void OnTextInput(char character)
		{
			for (int i = 0; i < this.m_TextInputListeners.length; i++)
			{
				this.m_TextInputListeners[i](character);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00015958 File Offset: 0x00013B58
		public KeyControl FindKeyOnCurrentKeyboardLayout(string displayName)
		{
			ReadOnlyArray<KeyControl> allKeys = this.allKeys;
			for (int i = 0; i < allKeys.Count; i++)
			{
				if (string.Equals(allKeys[i].displayName, displayName, StringComparison.CurrentCultureIgnoreCase))
				{
					return allKeys[i];
				}
			}
			return null;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000159A0 File Offset: 0x00013BA0
		public void OnIMECompositionChanged(IMECompositionString compositionString)
		{
			if (this.m_ImeCompositionListeners.length > 0)
			{
				for (int i = 0; i < this.m_ImeCompositionListeners.length; i++)
				{
					this.m_ImeCompositionListeners[i](compositionString);
				}
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000159E4 File Offset: 0x00013BE4
		unsafe bool IEventPreProcessor.PreProcessEvent(InputEventPtr currentEventPtr)
		{
			if (currentEventPtr.type == 1398030676)
			{
				StateEvent* ptr = StateEvent.FromUnchecked(currentEventPtr);
				if (ptr->stateFormat == KeyboardState.Format)
				{
					KeyboardState* ptr2 = (KeyboardState*)(&ptr->stateData.FixedElementField);
					if (ptr2->Get(Key.IMESelected))
					{
						ptr2->Set(Key.IMESelected, false);
						ptr2->Set((Key)127, true);
					}
				}
			}
			return true;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00015A4C File Offset: 0x00013C4C
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x00015A54 File Offset: 0x00013C54
		protected KeyControl[] keys
		{
			get
			{
				return this.m_Keys;
			}
			set
			{
				this.m_Keys = value;
			}
		}

		// Token: 0x04000215 RID: 533
		public const int KeyCount = 110;

		// Token: 0x04000216 RID: 534
		internal const int ExtendedKeyCount = 126;

		// Token: 0x0400021D RID: 541
		private InlinedArray<Action<char>> m_TextInputListeners;

		// Token: 0x0400021E RID: 542
		private string m_KeyboardLayoutName;

		// Token: 0x0400021F RID: 543
		private KeyControl[] m_Keys;

		// Token: 0x04000220 RID: 544
		private InlinedArray<Action<IMECompositionString>> m_ImeCompositionListeners;
	}
}

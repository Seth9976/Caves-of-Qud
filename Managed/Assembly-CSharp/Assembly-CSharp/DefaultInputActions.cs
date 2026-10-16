using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

// Token: 0x02000070 RID: 112
public class DefaultInputActions : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000416 RID: 1046 RVA: 0x0001BFDA File Offset: 0x0001A1DA
	public InputActionAsset asset { get; }

	// Token: 0x06000417 RID: 1047 RVA: 0x0001BFE4 File Offset: 0x0001A1E4
	public DefaultInputActions()
	{
		this.asset = InputActionAsset.FromJson("{\r\n    \"version\": 1,\r\n    \"name\": \"DefaultInputActions\",\r\n    \"maps\": [\r\n        {\r\n            \"name\": \"Player\",\r\n            \"id\": \"df70fa95-8a34-4494-b137-73ab6b9c7d37\",\r\n            \"actions\": [\r\n                {\r\n                    \"name\": \"Move\",\r\n                    \"type\": \"Value\",\r\n                    \"id\": \"351f2ccd-1f9f-44bf-9bec-d62ac5c5f408\",\r\n                    \"expectedControlType\": \"Vector2\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": true\r\n                },\r\n                {\r\n                    \"name\": \"Look\",\r\n                    \"type\": \"Value\",\r\n                    \"id\": \"6b444451-8a00-4d00-a97e-f47457f736a8\",\r\n                    \"expectedControlType\": \"Vector2\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": true\r\n                },\r\n                {\r\n                    \"name\": \"Fire\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"6c2ab1b8-8984-453a-af3d-a3c78ae1679a\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"StickButton\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"8dab4bee-4219-41b8-ad0e-d50baa42804b\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                }\r\n            ],\r\n            \"bindings\": [\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"978bfe49-cc26-4a3d-ab7b-7d7a29327403\",\r\n                    \"path\": \"<Gamepad>/leftStick\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Gamepad\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"WASD\",\r\n                    \"id\": \"00ca640b-d935-4593-8157-c05846ea39b3\",\r\n                    \"path\": \"Dpad\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": true,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"up\",\r\n                    \"id\": \"e2062cb9-1b15-46a2-838c-2f8d72a0bdd9\",\r\n                    \"path\": \"<Keyboard>/w\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"up\",\r\n                    \"id\": \"8180e8bd-4097-4f4e-ab88-4523101a6ce9\",\r\n                    \"path\": \"<Keyboard>/upArrow\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"up\",\r\n                    \"id\": \"132a69a9-ef73-4437-98c2-49c7faf50943\",\r\n                    \"path\": \"<Keyboard>/numpad8\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"down\",\r\n                    \"id\": \"320bffee-a40b-4347-ac70-c210eb8bc73a\",\r\n                    \"path\": \"<Keyboard>/s\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"down\",\r\n                    \"id\": \"1c5327b5-f71c-4f60-99c7-4e737386f1d1\",\r\n                    \"path\": \"<Keyboard>/downArrow\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"down\",\r\n                    \"id\": \"d260d702-76e0-4329-8453-4274ab318066\",\r\n                    \"path\": \"<Keyboard>/numpad2\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"left\",\r\n                    \"id\": \"d2581a9b-1d11-4566-b27d-b92aff5fabbc\",\r\n                    \"path\": \"<Keyboard>/a\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"left\",\r\n                    \"id\": \"2e46982e-44cc-431b-9f0b-c11910bf467a\",\r\n                    \"path\": \"<Keyboard>/leftArrow\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"left\",\r\n                    \"id\": \"e15a22d4-c566-4a20-b1e8-c3c3b5f3379c\",\r\n                    \"path\": \"<Keyboard>/numpad4\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"right\",\r\n                    \"id\": \"fcfe95b8-67b9-4526-84b5-5d0bc98d6400\",\r\n                    \"path\": \"<Keyboard>/d\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"right\",\r\n                    \"id\": \"77bff152-3580-4b21-b6de-dcd0c7e41164\",\r\n                    \"path\": \"<Keyboard>/rightArrow\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"right\",\r\n                    \"id\": \"9d45fd6e-5e1f-4435-a3f3-1b34206cee0f\",\r\n                    \"path\": \"<Keyboard>/numpad6\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Keyboard&Mouse\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"1635d3fe-58b6-4ba9-a4e2-f4b964f6b5c8\",\r\n                    \"path\": \"<XRController>/{Primary2DAxis}\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"XR\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"3ea4d645-4504-4529-b061-ab81934c3752\",\r\n                    \"path\": \"<Joystick>/stick\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Joystick\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"c1f7a91b-d0fd-4a62-997e-7fb9b69bf235\",\r\n                    \"path\": \"<Gamepad>/rightStick\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Gamepad\",\r\n                    \"action\": \"Look\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"8c8e490b-c610-4785-884f-f04217b23ca4\",\r\n                    \"path\": \"<Pointer>/delta\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard&Mouse;Touch\",\r\n                    \"action\": \"Look\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"3e5f5442-8668-4b27-a940-df99bad7e831\",\r\n                    \"path\": \"<Joystick>/{Hatswitch}\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Joystick\",\r\n                    \"action\": \"Look\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"143bb1cd-cc10-4eca-a2f0-a3664166fe91\",\r\n                    \"path\": \"<Gamepad>/rightTrigger\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Gamepad\",\r\n                    \"action\": \"Fire\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"05f6913d-c316-48b2-a6bb-e225f14c7960\",\r\n                    \"path\": \"<Mouse>/leftButton\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard&Mouse\",\r\n                    \"action\": \"Fire\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"886e731e-7071-4ae4-95c0-e61739dad6fd\",\r\n                    \"path\": \"<Touchscreen>/primaryTouch/tap\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Touch\",\r\n                    \"action\": \"Fire\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"ee3d0cd2-254e-47a7-a8cb-bc94d9658c54\",\r\n                    \"path\": \"<Joystick>/trigger\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Joystick\",\r\n                    \"action\": \"Fire\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"8255d333-5683-4943-a58a-ccb207ff1dce\",\r\n                    \"path\": \"<XRController>/{PrimaryAction}\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"XR\",\r\n                    \"action\": \"Fire\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"7af91f72-0849-4f1f-9754-4dc433fb4c9d\",\r\n                    \"path\": \"<Gamepad>/rightStickPress\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"StickButton\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                }\r\n            ]\r\n        },\r\n        {\r\n            \"name\": \"UI\",\r\n            \"id\": \"272f6d14-89ba-496f-b7ff-215263d3219f\",\r\n            \"actions\": [\r\n                {\r\n                    \"name\": \"Navigate\",\r\n                    \"type\": \"PassThrough\",\r\n                    \"id\": \"c95b2375-e6d9-4b88-9c4c-c5e76515df4b\",\r\n                    \"expectedControlType\": \"Vector2\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Submit\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"7607c7b6-cd76-4816-beef-bd0341cfe950\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Cancel\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"15cef263-9014-4fd5-94d9-4e4a6234a6ef\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Point\",\r\n                    \"type\": \"PassThrough\",\r\n                    \"id\": \"32b35790-4ed0-4e9a-aa41-69ac6d629449\",\r\n                    \"expectedControlType\": \"Vector2\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": true\r\n                },\r\n                {\r\n                    \"name\": \"Click\",\r\n                    \"type\": \"PassThrough\",\r\n                    \"id\": \"3c7022bf-7922-4f7c-a998-c437916075ad\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": true\r\n                },\r\n                {\r\n                    \"name\": \"ScrollWheel\",\r\n                    \"type\": \"PassThrough\",\r\n                    \"id\": \"0489e84a-4833-4c40-bfae-cea84b696689\",\r\n                    \"expectedControlType\": \"Vector2\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"MiddleClick\",\r\n                    \"type\": \"PassThrough\",\r\n                    \"id\": \"dad70c86-b58c-4b17-88ad-f5e53adf419e\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"RightClick\",\r\n                    \"type\": \"PassThrough\",\r\n                    \"id\": \"44b200b1-1557-4083-816c-b22cbdf77ddf\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"TrackedDevicePosition\",\r\n                    \"type\": \"PassThrough\",\r\n                    \"id\": \"24908448-c609-4bc3-a128-ea258674378a\",\r\n                    \"expectedControlType\": \"Vector3\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"TrackedDeviceOrientation\",\r\n                    \"type\": \"PassThrough\",\r\n                    \"id\": \"9caa3d8a-6b2f-4e8e-8bad-6ede561bd9be\",\r\n                    \"expectedControlType\": \"Quaternion\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                }\r\n            ],\r\n            \"bindings\": [\r\n                {\r\n                    \"name\": \"Gamepad\",\r\n                    \"id\": \"809f371f-c5e2-4e7a-83a1-d867598f40dd\",\r\n                    \"path\": \"2DVector\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Navigate\",\r\n                    \"isComposite\": true,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"up\",\r\n                    \"id\": \"14a5d6e8-4aaf-4119-a9ef-34b8c2c548bf\",\r\n                    \"path\": \"<Gamepad>/leftStick/up\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Gamepad\",\r\n                    \"action\": \"Navigate\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"up\",\r\n                    \"id\": \"9144cbe6-05e1-4687-a6d7-24f99d23dd81\",\r\n                    \"path\": \"<Gamepad>/rightStick/up\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Gamepad\",\r\n                    \"action\": \"Navigate\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"up\",\r\n                    \"id\": \"1fc40080-f5e9-4353-a605-e76e03bc9d73\",\r\n                    \"path\": \"<Keyboard>/numpad8\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Gamepad\",\r\n                    \"action\": \"Navigate\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"down\",\r\n                    \"id\": \"2db08d65-c5fb-421b-983f-c71163608d67\",\r\n                    \"path\": \"<Gamepad>/leftStick/down\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Gamepad\",\r\n                    \"action\": \"Navigate\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"down\",\r\n                    \"id\": \"58748904-2ea9-4a80-8579-b500e6a76df8\",\r\n                    \"path\": \"<Gamepad>/rightStick/down\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Gamepad\",\r\n                    \"action\": \"Navigate\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"down\",\r\n                    \"id\": \"ac9c56ab-a383-496b-b152-7fa0e9882591\",\r\n                    \"path\": \"<Keyboard>/numpad2\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Gamepad\",\r\n                    \"action\": \"Navigate\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },[...string is too long...]");
		this.m_Player = this.asset.FindActionMap("Player", true);
		this.m_Player_Move = this.m_Player.FindAction("Move", true);
		this.m_Player_Look = this.m_Player.FindAction("Look", true);
		this.m_Player_Fire = this.m_Player.FindAction("Fire", true);
		this.m_Player_StickButton = this.m_Player.FindAction("StickButton", true);
		this.m_UI = this.asset.FindActionMap("UI", true);
		this.m_UI_Navigate = this.m_UI.FindAction("Navigate", true);
		this.m_UI_Submit = this.m_UI.FindAction("Submit", true);
		this.m_UI_Cancel = this.m_UI.FindAction("Cancel", true);
		this.m_UI_Point = this.m_UI.FindAction("Point", true);
		this.m_UI_Click = this.m_UI.FindAction("Click", true);
		this.m_UI_ScrollWheel = this.m_UI.FindAction("ScrollWheel", true);
		this.m_UI_MiddleClick = this.m_UI.FindAction("MiddleClick", true);
		this.m_UI_RightClick = this.m_UI.FindAction("RightClick", true);
		this.m_UI_TrackedDevicePosition = this.m_UI.FindAction("TrackedDevicePosition", true);
		this.m_UI_TrackedDeviceOrientation = this.m_UI.FindAction("TrackedDeviceOrientation", true);
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
	~DefaultInputActions()
	{
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0001C1D8 File Offset: 0x0001A3D8
	public void Dispose()
	{
		global::UnityEngine.Object.Destroy(this.asset);
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x0600041A RID: 1050 RVA: 0x0001C1E5 File Offset: 0x0001A3E5
	// (set) Token: 0x0600041B RID: 1051 RVA: 0x0001C1F2 File Offset: 0x0001A3F2
	public InputBinding? bindingMask
	{
		get
		{
			return this.asset.bindingMask;
		}
		set
		{
			this.asset.bindingMask = value;
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001C200 File Offset: 0x0001A400
	// (set) Token: 0x0600041D RID: 1053 RVA: 0x0001C20D File Offset: 0x0001A40D
	public ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			return this.asset.devices;
		}
		set
		{
			this.asset.devices = value;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001C21B File Offset: 0x0001A41B
	public ReadOnlyArray<InputControlScheme> controlSchemes
	{
		get
		{
			return this.asset.controlSchemes;
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0001C228 File Offset: 0x0001A428
	public bool Contains(InputAction action)
	{
		return this.asset.Contains(action);
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x0001C236 File Offset: 0x0001A436
	public IEnumerator<InputAction> GetEnumerator()
	{
		return this.asset.GetEnumerator();
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0001C243 File Offset: 0x0001A443
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x0001C24B File Offset: 0x0001A44B
	public void Enable()
	{
		this.asset.Enable();
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x0001C258 File Offset: 0x0001A458
	public void Disable()
	{
		this.asset.Disable();
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001C265 File Offset: 0x0001A465
	public IEnumerable<InputBinding> bindings
	{
		get
		{
			return this.asset.bindings;
		}
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0001C272 File Offset: 0x0001A472
	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return this.asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0001C281 File Offset: 0x0001A481
	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		return this.asset.FindBinding(bindingMask, out action);
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001C290 File Offset: 0x0001A490
	public global::DefaultInputActions.PlayerActions Player
	{
		get
		{
			return new global::DefaultInputActions.PlayerActions(this);
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000428 RID: 1064 RVA: 0x0001C298 File Offset: 0x0001A498
	public global::DefaultInputActions.UIActions UI
	{
		get
		{
			return new global::DefaultInputActions.UIActions(this);
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001C2A0 File Offset: 0x0001A4A0
	public InputControlScheme KeyboardMouseScheme
	{
		get
		{
			if (this.m_KeyboardMouseSchemeIndex == -1)
			{
				this.m_KeyboardMouseSchemeIndex = this.asset.FindControlSchemeIndex("Keyboard&Mouse");
			}
			return this.asset.controlSchemes[this.m_KeyboardMouseSchemeIndex];
		}
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001C2E8 File Offset: 0x0001A4E8
	public InputControlScheme GamepadScheme
	{
		get
		{
			if (this.m_GamepadSchemeIndex == -1)
			{
				this.m_GamepadSchemeIndex = this.asset.FindControlSchemeIndex("Gamepad");
			}
			return this.asset.controlSchemes[this.m_GamepadSchemeIndex];
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x0600042B RID: 1067 RVA: 0x0001C330 File Offset: 0x0001A530
	public InputControlScheme TouchScheme
	{
		get
		{
			if (this.m_TouchSchemeIndex == -1)
			{
				this.m_TouchSchemeIndex = this.asset.FindControlSchemeIndex("Touch");
			}
			return this.asset.controlSchemes[this.m_TouchSchemeIndex];
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001C378 File Offset: 0x0001A578
	public InputControlScheme JoystickScheme
	{
		get
		{
			if (this.m_JoystickSchemeIndex == -1)
			{
				this.m_JoystickSchemeIndex = this.asset.FindControlSchemeIndex("Joystick");
			}
			return this.asset.controlSchemes[this.m_JoystickSchemeIndex];
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
	public InputControlScheme XRScheme
	{
		get
		{
			if (this.m_XRSchemeIndex == -1)
			{
				this.m_XRSchemeIndex = this.asset.FindControlSchemeIndex("XR");
			}
			return this.asset.controlSchemes[this.m_XRSchemeIndex];
		}
	}

	// Token: 0x04000324 RID: 804
	private readonly InputActionMap m_Player;

	// Token: 0x04000325 RID: 805
	private List<global::DefaultInputActions.IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<global::DefaultInputActions.IPlayerActions>();

	// Token: 0x04000326 RID: 806
	private readonly InputAction m_Player_Move;

	// Token: 0x04000327 RID: 807
	private readonly InputAction m_Player_Look;

	// Token: 0x04000328 RID: 808
	private readonly InputAction m_Player_Fire;

	// Token: 0x04000329 RID: 809
	private readonly InputAction m_Player_StickButton;

	// Token: 0x0400032A RID: 810
	private readonly InputActionMap m_UI;

	// Token: 0x0400032B RID: 811
	private List<global::DefaultInputActions.IUIActions> m_UIActionsCallbackInterfaces = new List<global::DefaultInputActions.IUIActions>();

	// Token: 0x0400032C RID: 812
	private readonly InputAction m_UI_Navigate;

	// Token: 0x0400032D RID: 813
	private readonly InputAction m_UI_Submit;

	// Token: 0x0400032E RID: 814
	private readonly InputAction m_UI_Cancel;

	// Token: 0x0400032F RID: 815
	private readonly InputAction m_UI_Point;

	// Token: 0x04000330 RID: 816
	private readonly InputAction m_UI_Click;

	// Token: 0x04000331 RID: 817
	private readonly InputAction m_UI_ScrollWheel;

	// Token: 0x04000332 RID: 818
	private readonly InputAction m_UI_MiddleClick;

	// Token: 0x04000333 RID: 819
	private readonly InputAction m_UI_RightClick;

	// Token: 0x04000334 RID: 820
	private readonly InputAction m_UI_TrackedDevicePosition;

	// Token: 0x04000335 RID: 821
	private readonly InputAction m_UI_TrackedDeviceOrientation;

	// Token: 0x04000336 RID: 822
	private int m_KeyboardMouseSchemeIndex = -1;

	// Token: 0x04000337 RID: 823
	private int m_GamepadSchemeIndex = -1;

	// Token: 0x04000338 RID: 824
	private int m_TouchSchemeIndex = -1;

	// Token: 0x04000339 RID: 825
	private int m_JoystickSchemeIndex = -1;

	// Token: 0x0400033A RID: 826
	private int m_XRSchemeIndex = -1;

	// Token: 0x02001593 RID: 5523
	public struct PlayerActions
	{
		// Token: 0x0600D586 RID: 54662 RVA: 0x004F4906 File Offset: 0x004F2B06
		public PlayerActions(global::DefaultInputActions wrapper)
		{
			this.m_Wrapper = wrapper;
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x0600D587 RID: 54663 RVA: 0x004F490F File Offset: 0x004F2B0F
		public InputAction Move
		{
			get
			{
				return this.m_Wrapper.m_Player_Move;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600D588 RID: 54664 RVA: 0x004F491C File Offset: 0x004F2B1C
		public InputAction Look
		{
			get
			{
				return this.m_Wrapper.m_Player_Look;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600D589 RID: 54665 RVA: 0x004F4929 File Offset: 0x004F2B29
		public InputAction Fire
		{
			get
			{
				return this.m_Wrapper.m_Player_Fire;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600D58A RID: 54666 RVA: 0x004F4936 File Offset: 0x004F2B36
		public InputAction StickButton
		{
			get
			{
				return this.m_Wrapper.m_Player_StickButton;
			}
		}

		// Token: 0x0600D58B RID: 54667 RVA: 0x004F4943 File Offset: 0x004F2B43
		public InputActionMap Get()
		{
			return this.m_Wrapper.m_Player;
		}

		// Token: 0x0600D58C RID: 54668 RVA: 0x004F4950 File Offset: 0x004F2B50
		public void Enable()
		{
			this.Get().Enable();
		}

		// Token: 0x0600D58D RID: 54669 RVA: 0x004F495D File Offset: 0x004F2B5D
		public void Disable()
		{
			this.Get().Disable();
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x0600D58E RID: 54670 RVA: 0x004F496A File Offset: 0x004F2B6A
		public bool enabled
		{
			get
			{
				return this.Get().enabled;
			}
		}

		// Token: 0x0600D58F RID: 54671 RVA: 0x004F4977 File Offset: 0x004F2B77
		public static implicit operator InputActionMap(global::DefaultInputActions.PlayerActions set)
		{
			return set.Get();
		}

		// Token: 0x0600D590 RID: 54672 RVA: 0x004F4980 File Offset: 0x004F2B80
		public void AddCallbacks(global::DefaultInputActions.IPlayerActions instance)
		{
			if (instance == null || this.m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance))
			{
				return;
			}
			this.m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
			this.Move.started += instance.OnMove;
			this.Move.performed += instance.OnMove;
			this.Move.canceled += instance.OnMove;
			this.Look.started += instance.OnLook;
			this.Look.performed += instance.OnLook;
			this.Look.canceled += instance.OnLook;
			this.Fire.started += instance.OnFire;
			this.Fire.performed += instance.OnFire;
			this.Fire.canceled += instance.OnFire;
			this.StickButton.started += instance.OnStickButton;
			this.StickButton.performed += instance.OnStickButton;
			this.StickButton.canceled += instance.OnStickButton;
		}

		// Token: 0x0600D591 RID: 54673 RVA: 0x004F4AD8 File Offset: 0x004F2CD8
		private void UnregisterCallbacks(global::DefaultInputActions.IPlayerActions instance)
		{
			this.Move.started -= instance.OnMove;
			this.Move.performed -= instance.OnMove;
			this.Move.canceled -= instance.OnMove;
			this.Look.started -= instance.OnLook;
			this.Look.performed -= instance.OnLook;
			this.Look.canceled -= instance.OnLook;
			this.Fire.started -= instance.OnFire;
			this.Fire.performed -= instance.OnFire;
			this.Fire.canceled -= instance.OnFire;
			this.StickButton.started -= instance.OnStickButton;
			this.StickButton.performed -= instance.OnStickButton;
			this.StickButton.canceled -= instance.OnStickButton;
		}

		// Token: 0x0600D592 RID: 54674 RVA: 0x004F4C05 File Offset: 0x004F2E05
		public void RemoveCallbacks(global::DefaultInputActions.IPlayerActions instance)
		{
			if (this.m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
			{
				this.UnregisterCallbacks(instance);
			}
		}

		// Token: 0x0600D593 RID: 54675 RVA: 0x004F4C24 File Offset: 0x004F2E24
		public void SetCallbacks(global::DefaultInputActions.IPlayerActions instance)
		{
			foreach (global::DefaultInputActions.IPlayerActions playerActions in this.m_Wrapper.m_PlayerActionsCallbackInterfaces)
			{
				this.UnregisterCallbacks(playerActions);
			}
			this.m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
			this.AddCallbacks(instance);
		}

		// Token: 0x040056CE RID: 22222
		private global::DefaultInputActions m_Wrapper;
	}

	// Token: 0x02001594 RID: 5524
	public struct UIActions
	{
		// Token: 0x0600D594 RID: 54676 RVA: 0x004F4C94 File Offset: 0x004F2E94
		public UIActions(global::DefaultInputActions wrapper)
		{
			this.m_Wrapper = wrapper;
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x0600D595 RID: 54677 RVA: 0x004F4C9D File Offset: 0x004F2E9D
		public InputAction Navigate
		{
			get
			{
				return this.m_Wrapper.m_UI_Navigate;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x0600D596 RID: 54678 RVA: 0x004F4CAA File Offset: 0x004F2EAA
		public InputAction Submit
		{
			get
			{
				return this.m_Wrapper.m_UI_Submit;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x0600D597 RID: 54679 RVA: 0x004F4CB7 File Offset: 0x004F2EB7
		public InputAction Cancel
		{
			get
			{
				return this.m_Wrapper.m_UI_Cancel;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600D598 RID: 54680 RVA: 0x004F4CC4 File Offset: 0x004F2EC4
		public InputAction Point
		{
			get
			{
				return this.m_Wrapper.m_UI_Point;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x0600D599 RID: 54681 RVA: 0x004F4CD1 File Offset: 0x004F2ED1
		public InputAction Click
		{
			get
			{
				return this.m_Wrapper.m_UI_Click;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600D59A RID: 54682 RVA: 0x004F4CDE File Offset: 0x004F2EDE
		public InputAction ScrollWheel
		{
			get
			{
				return this.m_Wrapper.m_UI_ScrollWheel;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600D59B RID: 54683 RVA: 0x004F4CEB File Offset: 0x004F2EEB
		public InputAction MiddleClick
		{
			get
			{
				return this.m_Wrapper.m_UI_MiddleClick;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600D59C RID: 54684 RVA: 0x004F4CF8 File Offset: 0x004F2EF8
		public InputAction RightClick
		{
			get
			{
				return this.m_Wrapper.m_UI_RightClick;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x0600D59D RID: 54685 RVA: 0x004F4D05 File Offset: 0x004F2F05
		public InputAction TrackedDevicePosition
		{
			get
			{
				return this.m_Wrapper.m_UI_TrackedDevicePosition;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x0600D59E RID: 54686 RVA: 0x004F4D12 File Offset: 0x004F2F12
		public InputAction TrackedDeviceOrientation
		{
			get
			{
				return this.m_Wrapper.m_UI_TrackedDeviceOrientation;
			}
		}

		// Token: 0x0600D59F RID: 54687 RVA: 0x004F4D1F File Offset: 0x004F2F1F
		public InputActionMap Get()
		{
			return this.m_Wrapper.m_UI;
		}

		// Token: 0x0600D5A0 RID: 54688 RVA: 0x004F4D2C File Offset: 0x004F2F2C
		public void Enable()
		{
			this.Get().Enable();
		}

		// Token: 0x0600D5A1 RID: 54689 RVA: 0x004F4D39 File Offset: 0x004F2F39
		public void Disable()
		{
			this.Get().Disable();
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x0600D5A2 RID: 54690 RVA: 0x004F4D46 File Offset: 0x004F2F46
		public bool enabled
		{
			get
			{
				return this.Get().enabled;
			}
		}

		// Token: 0x0600D5A3 RID: 54691 RVA: 0x004F4D53 File Offset: 0x004F2F53
		public static implicit operator InputActionMap(global::DefaultInputActions.UIActions set)
		{
			return set.Get();
		}

		// Token: 0x0600D5A4 RID: 54692 RVA: 0x004F4D5C File Offset: 0x004F2F5C
		public void AddCallbacks(global::DefaultInputActions.IUIActions instance)
		{
			if (instance == null || this.m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance))
			{
				return;
			}
			this.m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
			this.Navigate.started += instance.OnNavigate;
			this.Navigate.performed += instance.OnNavigate;
			this.Navigate.canceled += instance.OnNavigate;
			this.Submit.started += instance.OnSubmit;
			this.Submit.performed += instance.OnSubmit;
			this.Submit.canceled += instance.OnSubmit;
			this.Cancel.started += instance.OnCancel;
			this.Cancel.performed += instance.OnCancel;
			this.Cancel.canceled += instance.OnCancel;
			this.Point.started += instance.OnPoint;
			this.Point.performed += instance.OnPoint;
			this.Point.canceled += instance.OnPoint;
			this.Click.started += instance.OnClick;
			this.Click.performed += instance.OnClick;
			this.Click.canceled += instance.OnClick;
			this.ScrollWheel.started += instance.OnScrollWheel;
			this.ScrollWheel.performed += instance.OnScrollWheel;
			this.ScrollWheel.canceled += instance.OnScrollWheel;
			this.MiddleClick.started += instance.OnMiddleClick;
			this.MiddleClick.performed += instance.OnMiddleClick;
			this.MiddleClick.canceled += instance.OnMiddleClick;
			this.RightClick.started += instance.OnRightClick;
			this.RightClick.performed += instance.OnRightClick;
			this.RightClick.canceled += instance.OnRightClick;
			this.TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
			this.TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
			this.TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
			this.TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
			this.TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
			this.TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
		}

		// Token: 0x0600D5A5 RID: 54693 RVA: 0x004F5064 File Offset: 0x004F3264
		private void UnregisterCallbacks(global::DefaultInputActions.IUIActions instance)
		{
			this.Navigate.started -= instance.OnNavigate;
			this.Navigate.performed -= instance.OnNavigate;
			this.Navigate.canceled -= instance.OnNavigate;
			this.Submit.started -= instance.OnSubmit;
			this.Submit.performed -= instance.OnSubmit;
			this.Submit.canceled -= instance.OnSubmit;
			this.Cancel.started -= instance.OnCancel;
			this.Cancel.performed -= instance.OnCancel;
			this.Cancel.canceled -= instance.OnCancel;
			this.Point.started -= instance.OnPoint;
			this.Point.performed -= instance.OnPoint;
			this.Point.canceled -= instance.OnPoint;
			this.Click.started -= instance.OnClick;
			this.Click.performed -= instance.OnClick;
			this.Click.canceled -= instance.OnClick;
			this.ScrollWheel.started -= instance.OnScrollWheel;
			this.ScrollWheel.performed -= instance.OnScrollWheel;
			this.ScrollWheel.canceled -= instance.OnScrollWheel;
			this.MiddleClick.started -= instance.OnMiddleClick;
			this.MiddleClick.performed -= instance.OnMiddleClick;
			this.MiddleClick.canceled -= instance.OnMiddleClick;
			this.RightClick.started -= instance.OnRightClick;
			this.RightClick.performed -= instance.OnRightClick;
			this.RightClick.canceled -= instance.OnRightClick;
			this.TrackedDevicePosition.started -= instance.OnTrackedDevicePosition;
			this.TrackedDevicePosition.performed -= instance.OnTrackedDevicePosition;
			this.TrackedDevicePosition.canceled -= instance.OnTrackedDevicePosition;
			this.TrackedDeviceOrientation.started -= instance.OnTrackedDeviceOrientation;
			this.TrackedDeviceOrientation.performed -= instance.OnTrackedDeviceOrientation;
			this.TrackedDeviceOrientation.canceled -= instance.OnTrackedDeviceOrientation;
		}

		// Token: 0x0600D5A6 RID: 54694 RVA: 0x004F5341 File Offset: 0x004F3541
		public void RemoveCallbacks(global::DefaultInputActions.IUIActions instance)
		{
			if (this.m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
			{
				this.UnregisterCallbacks(instance);
			}
		}

		// Token: 0x0600D5A7 RID: 54695 RVA: 0x004F5360 File Offset: 0x004F3560
		public void SetCallbacks(global::DefaultInputActions.IUIActions instance)
		{
			foreach (global::DefaultInputActions.IUIActions iuiactions in this.m_Wrapper.m_UIActionsCallbackInterfaces)
			{
				this.UnregisterCallbacks(iuiactions);
			}
			this.m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
			this.AddCallbacks(instance);
		}

		// Token: 0x040056CF RID: 22223
		private global::DefaultInputActions m_Wrapper;
	}

	// Token: 0x02001595 RID: 5525
	public interface IPlayerActions
	{
		// Token: 0x0600D5A8 RID: 54696
		void OnMove(InputAction.CallbackContext context);

		// Token: 0x0600D5A9 RID: 54697
		void OnLook(InputAction.CallbackContext context);

		// Token: 0x0600D5AA RID: 54698
		void OnFire(InputAction.CallbackContext context);

		// Token: 0x0600D5AB RID: 54699
		void OnStickButton(InputAction.CallbackContext context);
	}

	// Token: 0x02001596 RID: 5526
	public interface IUIActions
	{
		// Token: 0x0600D5AC RID: 54700
		void OnNavigate(InputAction.CallbackContext context);

		// Token: 0x0600D5AD RID: 54701
		void OnSubmit(InputAction.CallbackContext context);

		// Token: 0x0600D5AE RID: 54702
		void OnCancel(InputAction.CallbackContext context);

		// Token: 0x0600D5AF RID: 54703
		void OnPoint(InputAction.CallbackContext context);

		// Token: 0x0600D5B0 RID: 54704
		void OnClick(InputAction.CallbackContext context);

		// Token: 0x0600D5B1 RID: 54705
		void OnScrollWheel(InputAction.CallbackContext context);

		// Token: 0x0600D5B2 RID: 54706
		void OnMiddleClick(InputAction.CallbackContext context);

		// Token: 0x0600D5B3 RID: 54707
		void OnRightClick(InputAction.CallbackContext context);

		// Token: 0x0600D5B4 RID: 54708
		void OnTrackedDevicePosition(InputAction.CallbackContext context);

		// Token: 0x0600D5B5 RID: 54709
		void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
	}
}

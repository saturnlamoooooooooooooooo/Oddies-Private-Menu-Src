using System.Collections.Generic;
using easyInputs;
using MelonLoader;
using ONM.Mods;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Player = GorillaTagger;

namespace OddieMenuTemp
{
	internal class Menu
	{
		public static bool nojumpy;
		public static int selected = 1;
		public static List<string> buttonNames = new List<string>();
		public static List<string> buttonTypes = new List<string>();
		public static List<bool> buttonToggled = new List<bool>();
		public static bool GUIToggled = true;
		private static bool DEBOUNCE1 = false;
		private static string TAB = "plr";
		private static bool DEBOUNCE2 = false;
		private static bool DEBOUNCE3 = false;
		private static bool DEBOUNCE4 = false;
		private static bool GravityDeb = false;
		private static bool flipped = false;
		private static bool gravflip = false;
		private static bool tpg = false;
		private static bool lg = false;
		private static bool cg = false;
		private static bool esp = false;
		private static List<Renderer> esp_visible = new List<Renderer>();
		private static bool fly = false;
		private static bool boost = false;
		private static bool boost2 = false;
		private static bool istimmeh = false;
		private static bool ntf = false;
		private static bool gone = false;
		private static bool plat = false;
		private static bool kg = false;
		private static bool noclip = false;
		private static GameObject look;
		private static GameObject look2;
		private static GameObject killing;
		private static GameObject TagGun = GameObject.CreatePrimitive(0);
		private static GameObject KGun = GameObject.CreatePrimitive(0);
		private static GameObject TPGun = GameObject.CreatePrimitive(0);
		private static GameObject HUDObj;
		private static GameObject HUDObj2;
		private static GameObject MainCamera;
		private static Text MainText;
		private static string textTemp;
		private static Material TextMat = new Material(Shader.Find("GUI/Text Shader"));
		private static GameObject PlatformL = GameObject.CreatePrimitive(PrimitiveType.Cube);
		private static GameObject PlatformR = GameObject.CreatePrimitive(PrimitiveType.Cube);
		private static bool PlatformLDeb;
		private static bool PlatformRDeb;
		private static string PT;
		
        public static void UpdateMove()
		{
			HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
			HUDObj2.transform.rotation = MainCamera.transform.rotation;
		}

		public static void Update()
		{
			if (killing)
			{
				PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
				GameObject.Find("timmy").transform.position = killing.transform.position;
			}
			if (PhotonNetwork.InRoom)
			{
				PT = string.Concat(new string[]
				{
					"Room Code: ",
					PhotonNetwork.CurrentRoom.name,
					"\nPlayers in room: ",
					PhotonNetwork.CurrentRoom.PlayerCount.ToString(),
					"\nPlayer IDs: "
				});
				foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
				{
					if (!player.IsLocal)
					{
						bool isMasterClient = player.IsMasterClient;
						if (isMasterClient)
						{
							PT = string.Concat(new string[]
							{
								PT,
								"\n",
								player.NickName,
								"'s ID is ",
								player.UserId,
								" <-- MASTER"
							});
						}
						else
						{
							PT = string.Concat(new string[]
							{
								PT,
								"\n",
								player.NickName,
								"'s ID is ",
								player.UserId
							});
						}
					}
				}
				if (PhotonNetwork.LocalPlayer.IsMasterClient)
				{
					PT += "\n\nYour Info:\n <-- MASTER CLIENT -->\n";
				}
				else
				{
					PT += "\n\nYour Info:\n";
				}
				if (GameObject.Find("CodeOfConduct"))
				{
					PT = "";
					GameObject.Find("CodeOfConduct").GetComponent<Text>().text = "Room Info";
					GameObject.Find("COC Text").GetComponent<Text>().text = string.Concat(new string[]
					{
						PT,
						"Your Name: ",
						PhotonNetwork.LocalPlayer.NickName,
						"\nYour ID: ",
						PhotonNetwork.LocalPlayer.UserId
					});
					GameObject.Find("COC Text").GetComponent<Text>().alignment = TextAnchor.MiddleCenter; //cant fix the error soo
					GameObject.Find("COC Text").GetComponent<Text>().fontSize = 5;
				}
			}
			else
			{
				if (GameObject.Find("CodeOfConduct"))
				{
					GameObject.Find("CodeOfConduct").GetComponent<Text>().text = "Thanks for using OClient!";
					GameObject.Find("COC Text").GetComponent<Text>().text = "Thank you for using Oddie's Client, Made by OddieDev/oddie_vr on Discord! Menu System made by me and thanks to all my friends (gamermaskguy, FancyQuacker, lol, thatscrazy and Debber). Enjoy! (Hold both thumbsticks & trigger to move, same with toggling but with Grip) \n\nYour ActorId: " + PhotonNetwork.LocalPlayer.ActorNumber.ToString() + " \n\nYour ID: " + PhotonNetwork.LocalPlayer.UserId;
				}
			}
			UpdateMove();
			if (plat)
			{
				PlatformR.SetActive(true);
				PlatformL.SetActive(true);
				PlatformL.GetComponent<MeshRenderer>().material.color = Color.red;
				if (EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
				{
					if (PlatformLDeb)
					{
						PlatformL.transform.localScale = new Vector3(0.3f, 0.02f, 0.3f);
						PlatformL.transform.position = Player.Instance.leftHandTransform.transform.position;
						PlatformL.transform.Translate(0f, -0.05f, 0f);
					}
					PlatformLDeb = true;
				}
				else
				{
					PlatformLDeb = false;
				}
				if (EasyInputs.GetGripButtonDown(EasyHand.RightHand))
				{
					if (PlatformRDeb)
					{
						PlatformR.transform.localScale = new Vector3(0.3f, 0.02f, 0.3f);
						PlatformR.transform.position = Player.Instance.rightHandTransform.transform.position;
						PlatformR.transform.Translate(0f, -0.05f, 0f);
					}
					PlatformRDeb = true;
				}
				else
				{
					PlatformRDeb = false;
				}
			}
			else
			{
				PlatformR.SetActive(false);
				PlatformL.SetActive(false);
			}
			if (tpg)
			{
				RaycastHit raycastHit;
				if (EasyInputs.GetGripButtonDown(EasyHand.RightHand) && Physics.Raycast(Player.Instance.rightHandTransform.transform.position - Player.Instance.rightHandTransform.transform.up, -Player.Instance.rightHandTransform.transform.up, out raycastHit))
				{
					TPGun.transform.position = raycastHit.point; 
					TPGun.GetComponent<Renderer>().material.color = Color.red;
					TPGun.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
					TPGun.GetComponent<SphereCollider>().enabled = false;
					bool triggerButtonDown = EasyInputs.GetTriggerButtonDown(EasyHand.RightHand);
					if (triggerButtonDown)
					{
						Player.Instance.gameObject.transform.position = TPGun.transform.position;
					}
				}
			}
			try
			{
				if (esp)
				{
					if (EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand))
					{
						foreach (Renderer renderer in esp_visible)
						{
							renderer.enabled = false;
						}
					}
					else
					{
						foreach (Renderer renderer2 in esp_visible)
						{
							renderer2.enabled = true;
						}
					}
				}
			}
			catch
			{
			}
			try
			{
				if (istimmeh)
				{
					PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
					if (GameObject.Find("timmy"))
					{
						GameObject.Find("timmy").transform.position = Player.Instance.headCollider.gameObject.transform.position;
						GameObject.Find("timmy").transform.rotation = Player.Instance.headCollider.gameObject.transform.rotation;
						GameObject.Find("timmy").transform.GetComponent<Collider>().enabled = false;
						nojumpy = true;
					}
					else
					{
						if (GameObject.Find("timmy (2)"))
						{
							GameObject.Find("timmy (2)").transform.position = Player.Instance.headCollider.gameObject.transform.position;
							GameObject.Find("timmy (2)").transform.rotation = Player.Instance.headCollider.gameObject.transform.rotation;
							GameObject.Find("timmy (2)").transform.GetComponent<Collider>().enabled = false;
							nojumpy = true;
						}
					}
				}
			}
			catch
			{
			}
			try
			{
				if (cg)
				{
					RaycastHit raycastHit2;
					if (EasyInputs.GetGripButtonDown(EasyHand.RightHand) && Physics.Raycast(Player.Instance.rightHandTransform.transform.position - Player.Instance.rightHandTransform.transform.up, -Player.Instance.rightHandTransform.transform.up, out raycastHit2))
					{
						KGun.transform.position = raycastHit2.point;
						KGun.GetComponent<Renderer>().material.color = Color.red;
						KGun.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
						KGun.GetComponent<SphereCollider>().enabled = false;
						Photon.Realtime.Player owner = raycastHit2.collider.GetComponentInParent<PhotonView>().Owner;
						bool triggerButtonDown2 = EasyInputs.GetTriggerButtonDown(EasyHand.RightHand);
						if (triggerButtonDown2)
						{
							KGun.GetComponent<Renderer>().material.color = Color.green;
							Crash.CrashPerson(owner);
						}
					}
				}
			}
			catch
			{
			}
			try
			{
				if (lg)
				{
					RaycastHit raycastHit3; ;
					if (EasyInputs.GetGripButtonDown(EasyHand.RightHand) && Physics.Raycast(Player.Instance.rightHandTransform.transform.position - Player.Instance.rightHandTransform.transform.up, -Player.Instance.rightHandTransform.transform.up, out raycastHit3))
					{
						TagGun.transform.position = raycastHit3.point;
						TagGun.GetComponent<Renderer>().material.color = Color.red;
						TagGun.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
						TagGun.GetComponent<SphereCollider>().enabled = false;
						if (EasyInputs.GetTriggerButtonDown(EasyHand.RightHand) && look == null && raycastHit3.collider.gameObject.GetComponent("PhotonView"))
						{
							look = raycastHit3.collider.gameObject;
						}
						if (look)
						{
							if (EasyInputs.GetTriggerButtonDown(EasyHand.RightHand))
							{
								look2 = raycastHit3.collider.gameObject;
							}
						}
					}
					if (EasyInputs.GetPrimaryButtonDown(EasyHand.RightHand))
					{
						look = null;
						look2 = null;
					}
					if (look2)
					{
						PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
						look.transform.position = look2.transform.position;
						look2.GetComponent<PhotonView>().RPC("SetTaggedTime", look2.GetComponent<PhotonView>().Owner, null);
					}
				}
				else
				{
					look = null;
				}
			}
			catch
			{
			}
			try
			{
				if (kg)
				{
					RaycastHit raycastHit4;
					if (EasyInputs.GetGripButtonDown(EasyHand.RightHand) && Physics.Raycast(Player.Instance.rightHandTransform.transform.position - Player.Instance.rightHandTransform.transform.up, -Player.Instance.rightHandTransform.transform.up, out raycastHit4))
					{
						KGun.transform.position = raycastHit4.point;
						KGun.GetComponent<Renderer>().material.color = Color.red;
						KGun.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
						KGun.GetComponent<SphereCollider>().enabled = false;
						Photon.Realtime.Player owner2 = raycastHit4.collider.GetComponentInParent<PhotonView>().Owner;
						PhotonView componentInParent = raycastHit4.collider.GetComponentInParent<PhotonView>();
						bool triggerButtonDown4 = EasyInputs.GetTriggerButtonDown(EasyHand.RightHand);
						if (triggerButtonDown4)
						{
							KGun.GetComponent<Renderer>().material.color = Color.green;
							PhotonNetwork.SetMasterClient(owner2);
							PhotonNetwork.CloseConnection(owner2);
							PhotonNetwork.Destroy(componentInParent);
							PhotonNetwork.DestroyPlayerObjects(owner2);
						}
					}
				}
			}
			catch
			{
			}
			if (fly && EasyInputs.GetPrimaryButtonDown(EasyHand.RightHand))
			{
				Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
				Player.Instance.transform.position += Player.Instance.headCollider.transform.forward * Time.deltaTime * 37f;
			}
			if (ntf)
			{
				GorillaLocomotion.Player.Instance.disableMovement = false;
			}
			if (boost)
			{
				GorillaLocomotion.Player.Instance.maxJumpSpeed = 10000000f;
				GorillaLocomotion.Player.Instance.jumpMultiplier = 5f;
			}
			else
			{
				GorillaLocomotion.Player.Instance.maxJumpSpeed = 7f;
				GorillaLocomotion.Player.Instance.jumpMultiplier = 1f;
			}
			if (boost2)
			{
				GorillaLocomotion.Player.Instance.maxJumpSpeed = 10000000f;
				Noclip.Disable();
				GorillaLocomotion.Player.Instance.jumpMultiplier = 1.05f;
			}
			else
			{
				GorillaLocomotion.Player.Instance.maxJumpSpeed = 7f;
				GorillaLocomotion.Player.Instance.jumpMultiplier = 1f;
			}
			if (gone)
			{
				if (EasyInputs.GetTriggerButtonDown(EasyHand.LeftHand))
				{
					PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
				}
				if (!EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
				{
					return;
				}
				PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
				PhotonNetwork.Instantiate("PhotonVR/Player", Player.Instance.transform.position + new Vector3(0f, 0.4f, 0f), Player.Instance.transform.rotation, 90, null);
			}
			if (noclip)
			{
				if (EasyInputs.GetPrimaryButtonDown(EasyHand.LeftHand))
				{
					Noclip.Enabled();
				}
				else
				{
					Noclip.Disable();
				}
			}
			if (gravflip)
			{
				if (EasyInputs.GetPrimaryButtonDown(EasyHand.LeftHand))
				{
					if (GravityDeb)
					{
						flipped = !flipped;
						if (flipped)
						{
							Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y + 17f, Physics.gravity.z);
							Player.Instance.transform.Rotate(Vector3.forward, 180f);
						}
						else
						{
							Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y - 17f, Physics.gravity.z);
							Player.Instance.transform.Rotate(Vector3.forward, -180f);
						}
					}
					GravityDeb = false;
				}
				else
				{
					GravityDeb = true;
				}
			}
			textTemp = "";
			for (int i = 0; i < buttonNames.Count; i++)
			{
				if (i == selected)
				{
					if (buttonTypes[i] == "toggle")
					{
						textTemp = string.Concat(new string[]
						{
							textTemp,
							">",
							buttonNames[i],
							" [",
							buttonToggled[i].ToString(),
							"]\n"
						});
					}
					else
					{
						textTemp = textTemp + ">" + buttonNames[i] + "\n";
					}
				}
				else
				{
					if (buttonTypes[i] == "toggle")
					{
						textTemp = string.Concat(new string[]
						{
							textTemp,
							buttonNames[i],
							" [",
							buttonToggled[i].ToString(),
							"]\n"
						});
					}
					else
					{
						if (buttonTypes[i] == "button")
						{
							textTemp = textTemp + buttonNames[i] + "\n";
						}
						else
						{
							if (buttonTypes[i] == "header")
							{
								textTemp = textTemp + "<b>" + buttonNames[i] + "</b>\n";
							}
							else
							{
								if (buttonTypes[i] == "locked")
								{
									textTemp = textTemp + buttonNames[i] + "(LOCKED, private only) \n";
								}
								else
								{
									textTemp = textTemp + buttonNames[i] + "\n";
								}
							}
						}
					}
				}
			}
			MainText.text = "<color=orange>Oddie's Client</color>\n" + textTemp;
			if (EasyInputs.GetThumbStickButtonDown(EasyHand.LeftHand) && EasyInputs.GetThumbStickButtonDown(EasyHand.RightHand) && EasyInputs.GetTriggerButtonDown(EasyHand.RightHand))
			{
				if (!DEBOUNCE1)
				{
					if (selected >= buttonNames.Count - 1)
					{
						selected = -1;
					}
					selected++;
					if (buttonTypes[selected] == "header")
					{
						selected++;
					}
				}
				DEBOUNCE1 = true;
			}
			else
			{
				DEBOUNCE1 = false;
			}
			if (EasyInputs.GetThumbStickButtonDown(EasyHand.LeftHand) && EasyInputs.GetThumbStickButtonDown(EasyHand.RightHand) && EasyInputs.GetGripButtonDown(EasyHand.RightHand) && EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
			{
				if (!DEBOUNCE2)
				{
					GUIToggled = !GUIToggled;
					HUDObj.SetActive(GUIToggled);
				}
				DEBOUNCE2 = true;
			}
			else
			{
				DEBOUNCE2 = false;
			}
			if (EasyInputs.GetThumbStickButtonDown(EasyHand.LeftHand) && EasyInputs.GetThumbStickButtonDown(EasyHand.RightHand) && EasyInputs.GetGripButtonDown(EasyHand.RightHand) && !EasyInputs.GetGripButtonDown(EasyHand.LeftHand) && GUIToggled)
			{
				if (buttonTypes[selected] == "toggle")
				{
					if (!DEBOUNCE3)
					{
						buttonToggled[selected] = !buttonToggled[selected];
						if (TAB == "plr")
						{
							if (buttonNames[selected] == "LongArms")
							{
								if (buttonToggled[selected])
								{
									LongArms.Enable();
								}
								else
								{
									LongArms.Disable();
								}
							}
							if (buttonNames[selected] == "Gravity Flip")
							{
								if (buttonToggled[selected])
								{
									gravflip = true;
								}
								else
								{
									gravflip = false;
								}
							}
							if (buttonNames[selected] == "Speedboost")
							{
								boost = buttonToggled[selected];
							}
							if (buttonNames[selected] == "Racer's Speedboost")
							{
								boost2 = buttonToggled[selected];
							}
							if (buttonNames[selected] == "Invisible")
							{
								gone = buttonToggled[selected];
							}
							if (buttonNames[selected] == "Fly")
							{
								fly = buttonToggled[selected];
							}
							if (buttonNames[selected] == "No Death")
							{
								nojumpy = buttonToggled[selected];
							}
							if (buttonNames[selected] == "TP Gun")
							{
								tpg = buttonToggled[selected];
								cg = false;
								kg = false;
								lg = false;
							}
							if (buttonNames[selected] == "Destroy Gun")
							{
								kg = buttonToggled[selected];
								cg = false;
								tpg = false;
								lg = false;
							}
							if (buttonNames[selected] == "KILL Gun")
							{
								kg = false;
								cg = false;
								tpg = false;
								lg = buttonToggled[selected];
							}
							if (buttonNames[selected] == "FullBright")
							{
								if (buttonToggled[selected])
								{
									RenderSettings.fog = false;
									RenderSettings.ambientLight = Color.white;
								}
								else
								{
									RenderSettings.fog = true;
									RenderSettings.ambientLight = Color.black;
								}
							}
							if (buttonNames[selected] == "No Tag-Freeze")
							{
								ntf = buttonToggled[selected];
							}
							if (buttonNames[selected] == "U R Timmeh")
							{
								istimmeh = buttonToggled[selected];
							}
							if (buttonNames[selected] == "Crash Gun")
							{
								cg = buttonToggled[selected];
								tpg = false;
								lg = false;
								kg = false;
							}
							if (buttonNames[selected] == "Platforms")
							{
								plat = buttonToggled[selected];
							}
							if (buttonNames[selected] == "Noclip")
							{
								noclip = buttonToggled[selected];
							}
							if (buttonNames[selected] == "Esp")
							{
								esp = buttonToggled[selected];
							}
							if (buttonNames[selected] == "No Stalker Spawn")
							{
								GameObject.Find("Stalker manager forest").SetActive(!buttonToggled[selected]);
							}
						}
					}
					DEBOUNCE3 = true;
				}
				else
				{
					if (buttonTypes[selected] == "button")
					{
						if (buttonNames[selected] == "Kill Random (forest)")
						{
							PhotonView photonView = GorillaGameManager.instance.FindVRRigForPlayer(PhotonNetwork.PlayerListOthers[Random.Range(1, PhotonNetwork.PlayerListOthers.Count)]);
							killing = photonView.gameObject;
						}
						if (buttonNames[selected] == "Disconnect")
						{
							PhotonNetwork.Disconnect();
						}
						if (buttonNames[selected] == "Set Master")
						{
							PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
						}
						if (buttonNames[selected] == "No Name")
						{
							PhotonNetwork.LocalPlayer.nickName = "";
							Player.Instance.name = "";
						}
						if (buttonNames[selected] == "Crash")
						{
							Crash.CrashServer();
						}
						if (buttonNames[selected] == "List Hierarcy")
						{
							ButtonsHandler.Next_Page();
							foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
							{
								if (gameObject.transform.parent != null)
								{
									MelonLogger.Msg(string.Concat(new string[]
									{
										"        ",
										gameObject.name,
										" - TAGGED: ",
										gameObject.tag,
										" - PARENT: ",
										gameObject.transform.parent.name
									}));
								}
								else
								{
									MelonLogger.Msg(gameObject.name + " - TAGGED: " + gameObject.tag + " - PARENT: Hierarchy");
								}
							}
						}
					}
					else
					{
						if (!DEBOUNCE4)
						{
							if (buttonTypes[selected] == "next")
							{
								ButtonsHandler.Next_Page();
							}
						}
						DEBOUNCE4 = true;
					}
				}
			}
			else
			{
				DEBOUNCE3 = false;
				DEBOUNCE4 = false;
			}
		}
		
		public static void Init()
		{
			MainCamera = GameObject.Find("Main Camera");
			HUDObj = new GameObject();
			HUDObj2 = new GameObject();
			HUDObj2.name = "Oddie's Client";
			HUDObj.name = "Oddie's Client";
			HUDObj.AddComponent<Canvas>();
			HUDObj.AddComponent<CanvasScaler>();
			HUDObj.AddComponent<GraphicRaycaster>();
			HUDObj.GetComponent<Canvas>().enabled = true;
			HUDObj.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
			HUDObj.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
			HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(666f, 666f);
			HUDObj.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
			HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - 4.2f);
			HUDObj.transform.parent = HUDObj2.transform;
			MainText = new GameObject
			{
				transform = 
				{
					parent = HUDObj.transform
				}
			}.AddComponent<Text>();
			MainText.fontSize = 12;
			ESP();
			MainText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			MainText.rectTransform.sizeDelta = new Vector2(260f, 260f);
			MainText.rectTransform.localScale = new Vector3(0.01f, 0.01f, 1.5f);
			MainText.rectTransform.localPosition = new Vector3(0f, 0f, 1.6f);
			MainText.alignment = TextAnchor.MiddleCenter;
			MainText.material = TextMat;
		}

		public static void newOption(string opname, string optype)
		{
			buttonNames.Add(opname);
			buttonTypes.Add(optype);
			buttonToggled.Add(false);
		}

		public static void Clear()
		{
			buttonNames.Clear();
			buttonTypes.Clear();
			buttonToggled.Clear();
		}

		public static void ESP()
		{
			foreach (Renderer renderer in Object.FindObjectsOfType<Renderer>())
			{
				if (renderer.enabled)
				{
					try
					{
						if (!renderer.gameObject.GetComponent<PhotonView>())
						{
							esp_visible.Add(renderer);
							MainText.text = "Loading ESP, (" + renderer.gameObject.name + ")";
						}
					}
					catch
					{
						MainText.text = "Error while loading " + renderer.gameObject.name + ", Skipping..";
					}
				}
			}
		}
	}
}

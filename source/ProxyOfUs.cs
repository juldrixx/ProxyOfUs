using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Extensions;
using Reactor.Unstrip;
using ProxyOfUs.CustomOption;
using ProxyOfUs.RainbowMod;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace ProxyOfUs
{
	[BepInPlugin("com.juldrixx.proxyofus", "Proxy Of Us", "1.0.1")]
	[BepInDependency(ReactorPlugin.Id)]
	public class ProxyOfUs : BasePlugin
	{

		public ConfigEntry<string> Ip { get; set; }
		public ConfigEntry<ushort> Port { get; set; }
		
		public static Sprite JanitorClean;
        public static Sprite EngineerFix;
        public static Sprite SwapperSwitch;
        public static Sprite SwapperSwitchDisabled;
        public static Sprite Shift;
        public static Sprite Kill;
        public static Sprite Footprint;
        public static Sprite Rewind;
        public static Sprite NormalKill;
        public static Sprite GreyscaleKill;
        public static Sprite ShiftKill;
        public static Sprite MedicSprite;
        public static Sprite SeerSprite;
        public static Sprite SampleSprite;
        public static Sprite MorphSprite;
        public static Sprite UseButton;
        public static Sprite Camouflage;
        public static Sprite Arrow;
        public static Sprite CreateCamSprite;
        public static Sprite SecuritySprite;
        public static Sprite Abstain;
        public static Sprite MineSprite;
        public static Sprite SwoopSprite;
        public static Sprite DouseSprite;
        public static Sprite IgniteSprite;
        public static Sprite ReviveSprite;
        public static Sprite ButtonSprite;

        
        public override void Load()
		{
			this._harmony = new Harmony("com.juldrixx.proxyofus");
			
			CustomOption.Generate.GenerateAll();

			JanitorClean = CreateSprite("ProxyOfUs.Resources.Janitor.png");
			EngineerFix = CreateSprite("ProxyOfUs.Resources.Engineer.png");
			//EngineerArrow = CreateSprite("ProxyOfUs.Resources.EngineerArrow.png");
			SwapperSwitch = CreateSprite("ProxyOfUs.Resources.SwapperSwitch.png");
			SwapperSwitchDisabled = CreateSprite("ProxyOfUs.Resources.SwapperSwitchDisabled.png");
			Shift = CreateSprite("ProxyOfUs.Resources.Shift.png");
			Kill = CreateSprite("ProxyOfUs.Resources.Kill.png");
			Footprint = CreateSprite("ProxyOfUs.Resources.Footprint.png");
			Rewind = CreateSprite("ProxyOfUs.Resources.Rewind.png");
			NormalKill = CreateSprite("ProxyOfUs.Resources.NormalKill.png");
			GreyscaleKill = CreateSprite("ProxyOfUs.Resources.GreyscaleKill.png");
			ShiftKill = CreateSprite("ProxyOfUs.Resources.ShiftKill.png");
			MedicSprite = CreateSprite("ProxyOfUs.Resources.Medic.png");
			SeerSprite = CreateSprite("ProxyOfUs.Resources.Seer.png");
			SampleSprite = CreateSprite("ProxyOfUs.Resources.Sample.png");
			MorphSprite = CreateSprite("ProxyOfUs.Resources.Morph.png");
			UseButton = CreateSprite("ProxyOfUs.Resources.UseButton.png");
			Camouflage = CreateSprite("ProxyOfUs.Resources.Camouflage.png");
			Arrow = CreateSprite("ProxyOfUs.Resources.Arrow.png");
			CreateCamSprite = CreateSprite("ProxyOfUs.Resources.CreateCam.png");
			SecuritySprite = CreateSprite("ProxyOfUs.Resources.Security.png");
			Abstain = CreateSprite("ProxyOfUs.Resources.Abstain.png");
			MineSprite = CreateSprite("ProxyOfUs.Resources.Mine.png");
			SwoopSprite = CreateSprite("ProxyOfUs.Resources.Swoop.png");
			DouseSprite = CreateSprite("ProxyOfUs.Resources.Douse.png");
			IgniteSprite = CreateSprite("ProxyOfUs.Resources.Ignite.png");
			ReviveSprite = CreateSprite("ProxyOfUs.Resources.Revive.png");
			ButtonSprite = CreateSprite("ProxyOfUs.Resources.Button.png");
			
			PalettePatch.Load();
			ClassInjector.RegisterTypeInIl2Cpp<RainbowBehaviour>();
			
			Ip = Config.Bind("Custom", "Ipv4 or Hostname", "127.0.0.1");
			Port = Config.Bind("Custom", "Port", (ushort) 22023);
			var defaultRegions = ServerManager.DefaultRegions.ToList();
			var ip = Ip.Value;
			if (Uri.CheckHostName(Ip.Value).ToString() == "Dns")
			{
				foreach (var address in Dns.GetHostAddresses(Ip.Value))
				{
					if (address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
						continue;
					ip = address.ToString();
					break;
				}
			}

			/*defaultRegions.Insert(0, new RegionInfo(
				"Custom", ip, new[]
				{
					new ServerInfo($"Custom-Server", ip, Port.Value)
				})
			);
			*/

			ServerManager.DefaultRegions = defaultRegions.ToArray();
			
			this._harmony.PatchAll();
			
		}

        public static Sprite CreateSprite(string name, bool hat=false)
        {
	        var pixelsPerUnit = hat ? 225f : 100f;
	        var pivot = hat ? new Vector2(0.5f, 0.8f) : new Vector2(0.5f, 0.5f);
			
			var assembly = Assembly.GetExecutingAssembly();
			var tex = GUIExtensions.CreateEmptyTexture();
			var imageStream = assembly.GetManifestResourceStream(name);
			var img = imageStream.ReadFully();
			LoadImage(tex, img, true);
			tex.DontDestroy();
			var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, (float) tex.width, (float) tex.height), pivot, pixelsPerUnit);
			sprite.DontDestroy();
			return sprite;
		}
		
		private static void LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
		{
			_iCallLoadImage ??= IL2CPP.ResolveICall<DLoadImage>("UnityEngine.ImageConversion::LoadImage");
			var il2CPPArray = (Il2CppStructArray<byte>) data;
			_iCallLoadImage.Invoke(tex.Pointer, il2CPPArray.Pointer, markNonReadable);
		}

		private delegate bool DLoadImage(IntPtr tex, IntPtr data, bool markNonReadable);

		private static DLoadImage _iCallLoadImage;
		
		
		private Harmony _harmony;
	}
}

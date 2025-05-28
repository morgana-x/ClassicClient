using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect
{
    public class CPE
    {
        public static bool CPEEnabled = false;

        public static void SendCompatibleCPE(ClassicClient client)
        {
            List<string> compatible = new List<string>();
            foreach(var a in EnabledCPE)
            {
                if (a.Value)
                    compatible.Add(a.Key);
            }

            client.SendBytes(Network.CPE.ExtInfo.GetBytes((short)compatible.Count));

            foreach(var a in compatible)
            {
                Console.WriteLine($"Sending extension {a} {CPEPurportedVersions[a]}");

                client.SendBytes(Network.CPE.ExtEntry.GetBytes(a, CPEPurportedVersions[a]));
            }
        }
        public static Dictionary<string, int> CPEPurportedVersions = new Dictionary<string, int>();

        public static Dictionary<string, bool> EnabledCPE = new Dictionary<string, bool>()
        {
            ["EnvMapApperance"] = false,
            //["ClickDistance"] = false,
            ["CustomBlocks"] = false,
            ["HeldBlock"] = false,
            ["TextHotkey"] = false,
            //["ExtPlayerList"] = false,
            ["EnvColors"] = false,
            // ["SelectionCuboid"] = false,
            ["BlockPermissions"] = false,
            ["ChangeModel"] = false,
            ["EnvMapAppearance"] = false,
            ["EnvWeatherType"] = false,
            ["HackControl"] = false,
            //["EmoteFix"] = false,
            //["MessageTypes"] = false,
            ["LongerMessages"] = false,
            //["FullCP437"] = false,
           // ["BlockDefinitions"] = false,
            //["EnvMapAspect"] = false,
            //["PlayerClick"] = false,
            //["EntityProperty"] = false,
            //["ExtEntityPositions"] = false,
            ["TwoWayPing"] = false,
            //["InventoryOrder"] = false,
            ["InstantMOTD"] = false,
            //["FastMap"] = false,
            //["ExtendedTextures"] = false,
            //["SetHotbar"] = false,
            ["BulkBlockUpdate"] = false,
            ["SetSpawnpoint"] = false,
            //["VelocityControl"] = false,
            //["CustomParticles"] = false,
            //["PluginMessages"] = false,
            //["ExtEntityTeleport"] = false,
            ["LightingMode"] = false,
            //["CinematicGui"] = false,
            //["NotifyAction"] = false,

        };
    }
}

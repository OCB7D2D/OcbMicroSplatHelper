using System.Collections.Generic;
using System.IO;

public class MicroSplatHelperCmd : ConsoleCmdAbstract
{

    // ####################################################################
    // ####################################################################

    static readonly HarmonyFieldProxy<MicroSplatProceduralTextureConfig> VoxelMeshTerrainProcData =
        new HarmonyFieldProxy<MicroSplatProceduralTextureConfig>(typeof(VoxelMeshTerrain), "msProcData");

    // ####################################################################
    // ####################################################################

    public override void Execute(List<string> _params, CommandSenderInfo _senderInfo)
    {
        if (_params.Count == 1)
        {
            if (_params[0] == "layers")
            {
                World world = GameManager.Instance.World;
                var player = world.GetPrimaryPlayer();
                var xui = LocalPlayerUI.GetUIForPlayer(player).xui;
                var wm = xui.playerUI.windowManager;
                wm.Open("msplatLayerCfg", false/*, true, false*/);
            }
            else if (_params[0] == "shader")
            {
                World world = GameManager.Instance.World;
                var player = world.GetPrimaryPlayer();
                var xui = LocalPlayerUI.GetUIForPlayer(player).xui;
                var wm = xui.playerUI.windowManager;
                wm.Open("msplatShaderCfg", false/*, true, false*/);
            }
            else if (_params[0] == "close")
            {
                World world = GameManager.Instance.World;
                var player = world.GetPrimaryPlayer();
                var xui = LocalPlayerUI.GetUIForPlayer(player).xui;
                var wm = xui.playerUI.windowManager;
                wm.Close("msplatLayerCfg");
                wm.Close("msplatShaderCfg");
            }
            else if (_params[0] == "save")
            {
                var mod = OcbMicroSplatHelper.ModInstance;
                var path = Path.Join(mod.Path, "Config/worldglobal.xml");
                var layers = VoxelMeshTerrainProcData.Get(null);
                using (StreamWriter output = new StreamWriter(path))
                {
                    output.WriteLine("<configs><append xpath=\"/worldglobal\">");
                    output.WriteLine("  <biomes-config world=\"*\">");
                    for (int i = 0; i < layers.layers.Count; i++)
                    {
                        MicroSplatDumpCfg.DumpLayerCfg(output, layers, i, "      ");
                    }
                    output.WriteLine("  </biomes-config>");
                    output.WriteLine("</append></configs>");
                }
            }
            else if (_params[0] == "delete")
            {
                var mod = OcbMicroSplatHelper.ModInstance;
                File.Delete(Path.Join(mod.Path, "Config/worldglobal.xml"));
            }
        }
        else
        {
            Log.Warning("Must provide a command (open/close/save)");
        }
    }

    // ####################################################################
    // ####################################################################

    protected override string[] getCommands()
    {
        return new string[] { "msph" };
    }

    protected override string getDescription()
    {
        return "OCB microsplat WYSIWYG configurator";
    }

    // ####################################################################
    // ####################################################################

}


using System.Collections.Generic;
using static MicroSplatHelper;

public class XUiC_MicroSplatLayerCfg : XUiControllerOcb
{

    // ####################################################################
    // ####################################################################

    private MicroSplatHelper.Biome Biome;

    private XUiC_ComboBoxEnum<MicroSplatHelper.Biome> cbxBiome;

    private XUiC_MicroSplatLayer[] Helpers;

    // ####################################################################
    // ####################################################################

    public override void Init()
    {
        base.Init();
        Helpers = GetChildrenByType<XUiC_MicroSplatLayer>();
        cbxBiome = (XUiC_ComboBoxEnum<MicroSplatHelper.Biome>)GetChildById("cbxBiome");
        cbxBiome.OnValueChanged += CbxBiome_OnValueChanged;
    }

    // ####################################################################
    // ####################################################################

    static readonly HarmonyFieldProxy<MicroSplatProceduralTextureConfig> VoxelMeshTerrainProcData =
        new HarmonyFieldProxy<MicroSplatProceduralTextureConfig>(typeof(VoxelMeshTerrain), "msProcData");

    // ####################################################################
    // ####################################################################

    private void CbxBiome_OnValueChanged(XUiController _sender,
        MicroSplatHelper.Biome prev, MicroSplatHelper.Biome value)
    {
        Biome = value;
        UpdateUI();
    }

    // ####################################################################
    // ####################################################################

    private void UpdateUI()
    {
        HashSet<int> Configs = new HashSet<int>();
        var data = VoxelMeshTerrainProcData.Get(null);
        for (int i = 0; i < data.layers.Count; i++)
        {
            switch (Biome)
            {
                case Biome.Biome1: if (data.layers[i].biomeWeights.x > 0) Configs.Add(i); break;
                case Biome.Biome2: if (data.layers[i].biomeWeights.y > 0) Configs.Add(i); break;
                case Biome.Biome3: if (data.layers[i].biomeWeights.z > 0) Configs.Add(i); break;
                case Biome.Biome4: if (data.layers[i].biomeWeights.w > 0) Configs.Add(i); break;
                case Biome.Biome5: if (data.layers[i].biomeWeights2.x > 0) Configs.Add(i); break;
                case Biome.Biome6: if (data.layers[i].biomeWeights2.y > 0) Configs.Add(i); break;
                case Biome.Biome7: if (data.layers[i].biomeWeights2.z > 0) Configs.Add(i); break;
                case Biome.Biome8: if (data.layers[i].biomeWeights2.w > 0) Configs.Add(i); break;
            }
        }
        for (int i = 0; i < Helpers.Length; i++)
            Helpers[i].ViewComponent.IsVisible = i < Configs.Count;
        int n = 0; foreach (var cfg in Configs)
            Helpers[n++].LoadBiomeConfig(cfg);
    }

    // ####################################################################
    // ####################################################################

    public override void OnOpen()
    {
        base.OnOpen();
        UpdateUI();
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    // ####################################################################
    // ####################################################################

    public override bool AlwaysUpdate() => true;

    // ####################################################################
    // ####################################################################

}

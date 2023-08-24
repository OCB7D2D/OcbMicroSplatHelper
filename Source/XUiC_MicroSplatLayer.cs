using MusicUtils;
using System;
using UnityEngine;
using static MicroSplatPropData;

public class XUiC_MicroSplatLayer : XUiControllerOcb
{

    // ####################################################################
    // ####################################################################

    private XUiC_ComboBoxInt uiIndex;
    private XUiC_ComboBoxInt uiTexIdx;
    private XUiC_ComboBoxFloat uiWeight;
    private XUiC_ComboBoxBool uiNoiseActive;
    private XUiC_ComboBoxFloat uiNoiseFrequency;
    private XUiC_ComboBoxFloat uiNoiseOffset;
    private XUiC_ComboBoxFloat uiNoiseRangeMin;
    private XUiC_ComboBoxFloat uiNoiseRangeMax;
    private XUiC_ComboBoxEnum<MicroSplatHelper.KeyFrames> uiSlopeActive;
    private XUiC_ComboBoxFloat[] uiSlopeFrameTimes = new XUiC_ComboBoxFloat[4];
    private XUiC_ComboBoxFloat[] uiSlopeFrameValues = new XUiC_ComboBoxFloat[4];
    private XUiC_ComboBoxEnum<MicroSplatHelper.KeyFrames> uiHeightActive;
    private XUiC_ComboBoxFloat[] uiHeightFrameTimes = new XUiC_ComboBoxFloat[4];
    private XUiC_ComboBoxFloat[] uiHeightFrameValues = new XUiC_ComboBoxFloat[4];
    private XUiC_ComboBoxFloat uiUvScaleX;
    private XUiC_ComboBoxFloat uiUvScaleY;
    private XUiC_ComboBoxFloat uiMetallic;
    private XUiC_ComboBoxFloat uiSmoothness;

    // ####################################################################
    // ####################################################################

    private int BiomeProcIndex;

    // ####################################################################
    // ####################################################################

    public override void Init()
    {
        base.Init();
        GetComboBoxInt(ref uiIndex, "uiIndex");
        GetComboBoxInt(ref uiTexIdx, "uiTexIdx");
        GetComboBoxFloat(ref uiWeight, "uiWeight");
        GetComboBoxBool(ref uiNoiseActive, "uiNoiseActive");
        GetComboBoxFloat(ref uiNoiseFrequency, "uiNoiseFrequency");
        GetComboBoxFloat(ref uiNoiseOffset, "uiNoiseOffset");
        GetComboBoxFloat(ref uiNoiseRangeMin, "uiNoiseRangeMin");
        GetComboBoxFloat(ref uiNoiseRangeMax, "uiNoiseRangeMax");
        GetComboBoxEnum(ref uiSlopeActive, "uiSlopeActive");
        for (int i = 0; i < 4; i++)
        {
            GetComboBoxFloat(ref uiSlopeFrameTimes[i], $"uiSlopeFrame{i+1}Time");
            GetComboBoxFloat(ref uiSlopeFrameValues[i], $"uiSlopeFrame{i+1}Value");
        }
        GetComboBoxEnum(ref uiHeightActive, "uiHeightActive");
        for (int i = 0; i < 4; i++)
        {
            GetComboBoxFloat(ref uiHeightFrameTimes[i], $"uiHeightFrame{i + 1}Time");
            GetComboBoxFloat(ref uiHeightFrameValues[i], $"uiHeightFrame{i + 1}Value");
        }
        GetComboBoxFloat(ref uiUvScaleX, "uiUvScaleX");
        GetComboBoxFloat(ref uiUvScaleY, "uiUvScaleY");
        GetComboBoxFloat(ref uiMetallic, "uiMetallic");
        GetComboBoxFloat(ref uiSmoothness, "uiSmoothness");
    }

    // ####################################################################
    // ####################################################################

    static readonly HarmonyFieldProxy<MicroSplatPropData> VoxelMeshTerrainPropData =
        new HarmonyFieldProxy<MicroSplatPropData>(typeof(VoxelMeshTerrain), "msPropData");
    static readonly HarmonyFieldProxy<MicroSplatProceduralTextureConfig> VoxelMeshTerrainProcData =
        new HarmonyFieldProxy<MicroSplatProceduralTextureConfig>(typeof(VoxelMeshTerrain), "msProcData");

    static readonly HarmonyFieldProxy<Texture2D> VoxelMeshTerrainProcCurveTex =
        new HarmonyFieldProxy<Texture2D>(typeof(VoxelMeshTerrain), "msProcCurveTex");
    static readonly HarmonyFieldProxy<Texture2D> VoxelMeshTerrainProcParamTex =
        new HarmonyFieldProxy<Texture2D>(typeof(VoxelMeshTerrain), "msProcParamTex");

    // ####################################################################
    // ####################################################################

    public void LoadBiomeConfig(int idx)
    {
        uiIndex.Value = BiomeProcIndex = idx;
        var data = VoxelMeshTerrainProcData.Get(null);
        var props = VoxelMeshTerrainPropData.Get(null);
        if (data == null) return;
        var layer = data.layers[idx];
        uiWeight.Value = layer.weight;
        uiTexIdx.Value = layer.textureIndex;
        uiNoiseActive.Value = layer.noiseActive;
        uiNoiseFrequency.Value = layer.noiseFrequency;
        uiNoiseOffset.Value = layer.noiseOffset;
        uiNoiseRangeMin.Value = layer.noiseRange.x;
        uiNoiseRangeMax.Value = layer.noiseRange.y;
        LoadKeyFrames(layer.slopeActive, layer.slopeCurve,
            uiSlopeActive, uiSlopeFrameTimes, uiSlopeFrameValues);
        LoadKeyFrames(layer.heightActive, layer.heightCurve,
            uiHeightActive, uiHeightFrameTimes, uiHeightFrameValues);
        var uvScale = GetPropVector2(props, layer.textureIndex, PerTexVector2.SplatUVScale);
        (uiUvScaleX.Value, uiUvScaleY.Value) = (UnScale(uvScale.x), UnScale(uvScale.y));
        uiMetallic.Value = GetPropFloat(props, layer.textureIndex, PerTexFloat.Metallic);
        uiSmoothness.Value = GetPropFloat(props, layer.textureIndex, PerTexFloat.Smoothness);
    }

    private void LoadKeyFrames(bool active, AnimationCurve frames,
        XUiC_ComboBoxEnum<MicroSplatHelper.KeyFrames> cbx,
        XUiC_ComboBoxFloat[] times, XUiC_ComboBoxFloat[] values)
    {

        if (active == false) cbx.Value = MicroSplatHelper.KeyFrames.None;
        else if (frames.length == 1) cbx.Value = MicroSplatHelper.KeyFrames.One;
        else if (frames.length == 2) cbx.Value = MicroSplatHelper.KeyFrames.Two;
        else if (frames.length == 3) cbx.Value = MicroSplatHelper.KeyFrames.Three;
        else if (frames.length == 4) cbx.Value = MicroSplatHelper.KeyFrames.Four;
        else
        {
            cbx.Value = MicroSplatHelper.KeyFrames.Four;
            Log.Warning("Slope Keyframes have been truncated");
        }
        for (int i = 0; i < Math.Min(4, frames.length); i++)
        {
            var curve = frames[i];
            times[i].Value = curve.time;
            values[i].Value = curve.value;
        }
    }

    // ####################################################################
    // ####################################################################

    private void UpdateUI()
    {
        uiNoiseFrequency.Enabled =
        uiNoiseOffset.Enabled =
        uiNoiseRangeMin.Enabled =
        uiNoiseRangeMax.Enabled =
            uiNoiseActive.Value;
        UpdateKeyFrameUI(uiSlopeActive.Value,
            uiSlopeFrameTimes, uiSlopeFrameValues);
        UpdateKeyFrameUI(uiHeightActive.Value,
            uiHeightFrameTimes, uiHeightFrameValues);
        uiIndex.Enabled = false;
    }

    private static int ConfigToEnabled(MicroSplatHelper.KeyFrames cfg)
    {
        if (cfg == MicroSplatHelper.KeyFrames.None) return 0;
        else if (cfg == MicroSplatHelper.KeyFrames.One) return 1;
        else if (cfg == MicroSplatHelper.KeyFrames.Two) return 2;
        else if (cfg == MicroSplatHelper.KeyFrames.Three) return 3;
        else if (cfg == MicroSplatHelper.KeyFrames.Four) return 4;
        Log.Warning("Keyframes have been truncated ({0} => 4)", cfg);
        return 4;
    }

    private void UpdateKeyFrameUI(MicroSplatHelper.KeyFrames cfg,
        XUiC_ComboBoxFloat[] times, XUiC_ComboBoxFloat[] values)
    {
        var enabled = ConfigToEnabled(cfg);
        for (var i = 0; i < 4; i++)
        {
            times[i].Enabled = enabled > i;
            values[i].Enabled = enabled > i;
        }
    }

    protected void UpdateExistingMats(Texture curves, Texture props)
    {
        foreach (var rndr in UnityEngine.Object.FindObjectsOfType<Renderer>())
        {
            foreach (var mat in rndr.materials)
            {
                if (mat.shader == null) continue;

                if (!mat.shader.name.Contains("MicroSplat"))
                    continue;
                //     mat.shader = null;

                if (mat.HasTexture("_ProcTexCurves"))
                    mat.SetTexture("_ProcTexCurves", curves);
                else
                {
                    Log.Warning("Missing _ProcTexCurves {0}", mat.shader.name);
                }
                if (mat.HasTexture("_ProcTexParams"))
                    mat.SetTexture("_ProcTexParams", props);
                else Log.Warning("Missing _ProcTexParams {0}", mat.shader.name);

                rndr.enabled = false;
            }
        }
    }

    protected override void OnCfgUpdate(string name)
    {
        var data = VoxelMeshTerrainProcData.Get(null);
        var props = VoxelMeshTerrainPropData.Get(null);
        var layer = data.layers[BiomeProcIndex];
        layer.weight = (float)uiWeight.Value;
        layer.textureIndex = (int)uiTexIdx.Value;
        layer.noiseActive = uiNoiseActive.Value;
        layer.noiseFrequency = (float)uiNoiseFrequency.Value;
        layer.noiseOffset = (float)uiNoiseOffset.Value;
        layer.noiseRange.x = (float)uiNoiseRangeMin.Value;
        layer.noiseRange.y = (float)uiNoiseRangeMax.Value;
        UpdateKeyFrameCfg(ref layer.slopeActive, ref layer.slopeCurve,
            uiSlopeActive.Value, uiSlopeFrameTimes, uiSlopeFrameValues);
        UpdateKeyFrameCfg(ref layer.heightActive, ref layer.heightCurve,
            uiHeightActive.Value, uiHeightFrameTimes, uiHeightFrameValues);
        // Link both scales together (sync if one is moved)
        if (name == "uiUvScaleX") uiUvScaleY.Value = uiUvScaleX.Value;
        else if (name == "uiUvScaleY") uiUvScaleX.Value = uiUvScaleY.Value;
        props.SetValue(layer.textureIndex, PerTexVector2.SplatUVScale,
            new Vector2(DoScale(uiUvScaleX.Value), DoScale(uiUvScaleY.Value)));
        props.SetValue(layer.textureIndex, PerTexFloat.Metallic, (float)uiMetallic.Value);
        props.SetValue(layer.textureIndex, PerTexFloat.Smoothness, (float)uiSmoothness.Value);

        if (GameManager.IsDedicatedServer) return; // Nothing to do here
        if (MeshDescription.meshes.Length < MeshDescription.MESH_TERRAIN) return;
        var mesh = MeshDescription.meshes[MeshDescription.MESH_TERRAIN];
        var curveTex = data.GetCurveTexture();
        var paramTex = data.GetParamTexture();
        var propsTex = props.GetTexture();

        // VoxelMeshTerrainProcCurveTex.Set(null, curveTex);
        // VoxelMeshTerrainProcParamTex.Set(null, paramTex);
        // mesh.materialDistant.SetTexture("_ProcTexCurves", curveTex);
        // mesh.materialDistant.SetTexture("_ProcTexParams", paramTex);
        // mesh.material.SetTexture("_ProcTexCurves", null);
        // mesh.material.SetTexture("_ProcTexParams", null);
        // mesh.material.SetTexture("_ProcTexCurves", curveTex);
        // mesh.material.SetTexture("_ProcTexParams", paramTex);

        // Log.Out("Query current PC Layer count {0}", mesh.material.GetInt("_PCLayerCount"));
        // mesh.materialDistant.SetInt("_PCLayerCount", data.layers.Count);
        // mesh.material.SetInt("_PCLayerCount", data.layers.Count);
        // Log.Out("SET PC LAYER COUNT TO {0}", data.layers.Count);

        //  = null;
        // mesh.material.shader = mesh.materialDistant.shader;

        // UpdateExistingMats(curveTex, paramTex);

        UpdateUI();
    }

    private void UpdateKeyFrameCfg(ref bool active,
        ref AnimationCurve curve, MicroSplatHelper.KeyFrames cfg,
        XUiC_ComboBoxFloat[] times, XUiC_ComboBoxFloat[] values)
    {
        active = cfg != MicroSplatHelper.KeyFrames.None;
        curve = new AnimationCurve();
        int enabled = ConfigToEnabled(cfg);
        for (var i = 0; i < enabled; i++)
            curve.AddKey((float)times[i].Value,
                (float)values[i].Value);
    }

    // ####################################################################
    // ####################################################################

    public override void OnOpen()
    {
        base.OnOpen();
        // Remove once implemented
        LoadBiomeConfig(0);
        // Update UI after load
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

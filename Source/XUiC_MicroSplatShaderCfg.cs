using System;
using System.Numerics;
using UnityEngine;

public class XUiC_MicroSplatShaderCfg : XUiControllerOcb
{

    // ####################################################################
    // ####################################################################

    private XUiC_ComboBoxFloat uiProcBiomeCurveWeight;

    private XUiC_ComboBoxFloat uiNoiseHeightDataFreq;
    private XUiC_ComboBoxFloat uiNoiseHeightDataAmp;
    private XUiC_ComboBoxFloat uiWorldHeightRangeMin;
    private XUiC_ComboBoxFloat uiWorldHeightRangeMax;

    private XUiC_ComboBoxFloat uiDistanceResampleAlbedoStrength;
    private XUiC_ComboBoxFloat uiDistanceResampleNormalStrength;
    private XUiC_ComboBoxFloat uiDistanceResampleMaterialStrength;

    private XUiC_ComboBoxFloat uiNoiseNormal1ParamScale;
    private XUiC_ComboBoxFloat uiNoiseNormal1ParamStrength;
    private XUiC_ComboBoxFloat uiNoiseNormal2ParamScale;
    private XUiC_ComboBoxFloat uiNoiseNormal2ParamStrength;
    private XUiC_ComboBoxFloat uiNoiseNormal3ParamScale;
    private XUiC_ComboBoxFloat uiNoiseNormal3ParamStrength;

    private XUiC_ComboBoxFloat uiResampleDistanceUvScale;
    private XUiC_ComboBoxFloat uiResampleDistanceBegin;
    private XUiC_ComboBoxFloat uiResampleDistanceEnd;

    private XUiC_ComboBoxFloat uiDetailNoiseScale;
    private XUiC_ComboBoxFloat uiDetailNoiseStrength;
    private XUiC_ComboBoxFloat uiDetailNoiseFadeDist;

    private XUiC_ComboBoxFloat uiDistanceNoiseScale;
    private XUiC_ComboBoxFloat uiDistanceNoiseStrength;
    private XUiC_ComboBoxFloat uiDistanceNoiseFadeStart;
    private XUiC_ComboBoxFloat uiDistanceNoiseFadeEnd;

    private XUiC_ComboBoxFloat uiTriplanarContrast;
    private XUiC_ComboBoxFloat uiTriplanarUVScaleX;
    private XUiC_ComboBoxFloat uiTriplanarUVScaleY;
    private XUiC_ComboBoxFloat uiTriplanarUvOffsetX;
    private XUiC_ComboBoxFloat uiTriplanarUvOffsetY;

    // ####################################################################
    // ####################################################################

    public override void Init()
    {
        base.Init();
        GetComboBoxFloat(ref uiProcBiomeCurveWeight, "uiProcBiomeCurveWeight");
        GetComboBoxFloat(ref uiNoiseHeightDataFreq, "uiNoiseHeightDataFreq");
        GetComboBoxFloat(ref uiNoiseHeightDataAmp, "uiNoiseHeightDataAmp");
        GetComboBoxFloat(ref uiWorldHeightRangeMin, "uiWorldHeightRangeMin");
        GetComboBoxFloat(ref uiWorldHeightRangeMax, "uiWorldHeightRangeMax");
        GetComboBoxFloat(ref uiDistanceResampleAlbedoStrength, "uiDistanceResampleAlbedoStrength");
        GetComboBoxFloat(ref uiDistanceResampleNormalStrength, "uiDistanceResampleNormalStrength");
        GetComboBoxFloat(ref uiDistanceResampleMaterialStrength, "uiDistanceResampleMaterialStrength");
        GetComboBoxFloat(ref uiNoiseNormal1ParamScale, "uiNoiseNormal1ParamScale");
        GetComboBoxFloat(ref uiNoiseNormal1ParamStrength, "uiNoiseNormal1ParamStrength");
        GetComboBoxFloat(ref uiNoiseNormal2ParamScale, "uiNoiseNormal2ParamScale");
        GetComboBoxFloat(ref uiNoiseNormal2ParamStrength, "uiNoiseNormal2ParamStrength");
        GetComboBoxFloat(ref uiNoiseNormal3ParamScale, "uiNoiseNormal3ParamScale");
        GetComboBoxFloat(ref uiNoiseNormal3ParamStrength, "uiNoiseNormal3ParamStrength");
        GetComboBoxFloat(ref uiResampleDistanceUvScale, "uiResampleDistanceUvScale");
        GetComboBoxFloat(ref uiResampleDistanceBegin, "uiResampleDistanceBegin");
        GetComboBoxFloat(ref uiResampleDistanceEnd, "uiResampleDistanceEnd");
        GetComboBoxFloat(ref uiDetailNoiseScale, "uiDetailNoiseScale");
        GetComboBoxFloat(ref uiDetailNoiseStrength, "uiDetailNoiseStrength");
        GetComboBoxFloat(ref uiDetailNoiseFadeDist, "uiDetailNoiseFadeDist");
        GetComboBoxFloat(ref uiDistanceNoiseScale, "uiDistanceNoiseScale");
        GetComboBoxFloat(ref uiDistanceNoiseStrength, "uiDistanceNoiseStrength");
        GetComboBoxFloat(ref uiDistanceNoiseFadeStart, "uiDistanceNoiseFadeStart");
        GetComboBoxFloat(ref uiDistanceNoiseFadeEnd, "uiDistanceNoiseFadeEnd");
        GetComboBoxFloat(ref uiTriplanarContrast, "uiTriplanarContrast");
        GetComboBoxFloat(ref uiTriplanarUVScaleX, "uiTriplanarUVScaleX");
        GetComboBoxFloat(ref uiTriplanarUVScaleY, "uiTriplanarUVScaleY");
        GetComboBoxFloat(ref uiTriplanarUvOffsetX, "uiTriplanarUvOffsetX");
        GetComboBoxFloat(ref uiTriplanarUvOffsetY, "uiTriplanarUvOffsetY");
    }

    // ####################################################################
    // ####################################################################

    protected override void OnCfgUpdate(string name)
    {
        if (GameManager.IsDedicatedServer) return; // Nothing to do here
        if (MeshDescription.meshes.Length < MeshDescription.MESH_TERRAIN) return;
        var mesh = MeshDescription.meshes[MeshDescription.MESH_TERRAIN];
        UpdateMaterial(mesh.materialDistant);
        UpdateMaterial(mesh.material);
        UpdateUI(mesh.material);
    }

    // ####################################################################
    // ####################################################################

    private void UpdateMaterial(Material mat)
    {
        SetMatFloat(mat,
            "_ProcBiomeCurveWeight",
            uiProcBiomeCurveWeight);
        SetMatVector2(mat,
            "_NoiseHeightData",
            uiNoiseHeightDataFreq,
            uiNoiseHeightDataAmp);
        SetMatVector2(mat,
            "_WorldHeightRange",
            uiWorldHeightRangeMin,
            uiWorldHeightRangeMax);
        SetMatFloat(mat,
            "_DistanceResampleAlbedoStrength",
            uiDistanceResampleAlbedoStrength);
        SetMatFloat(mat,
            "_DistanceResampleNormalStrength",
            uiDistanceResampleNormalStrength);
        SetMatFloat(mat,
            "_DistanceResampleMaterialStrength",
            uiDistanceResampleMaterialStrength);
        SetMatVector2(mat,
            "_NormalNoiseScaleStrength",
            uiNoiseNormal1ParamScale,
            uiNoiseNormal1ParamStrength);
        SetMatVector2(mat,
            "_NormalNoiseScaleStrength2",
            uiNoiseNormal2ParamScale,
            uiNoiseNormal2ParamStrength);
        SetMatVector2(mat,
            "_NormalNoiseScaleStrength3",
            uiNoiseNormal3ParamScale,
            uiNoiseNormal3ParamStrength);
        SetMatVector3(mat,
            "_ResampleDistanceParams",
            uiResampleDistanceUvScale,
            uiResampleDistanceBegin,
            uiResampleDistanceEnd);
        SetMatVector3(mat,
            "_DetailNoiseScaleStrengthFade",
            uiDetailNoiseScale,
            uiDetailNoiseStrength,
            uiDetailNoiseFadeDist,
            fx: DoScale);
        SetMatVector4(mat,
            "_DistanceNoiseScaleStrengthFade",
            uiDistanceNoiseScale,
            uiDistanceNoiseStrength,
            uiDistanceNoiseFadeStart,
            uiDistanceNoiseFadeEnd,
            fx: DoScale);
        SetMatVector4(mat,
            "_TriplanarUVScale",
            uiTriplanarUVScaleX,
            uiTriplanarUVScaleY,
            uiTriplanarUvOffsetX,
            uiTriplanarUvOffsetY,
            fx: DoScale,
            fy: DoScale);
        SetMatFloat(mat,
            "_TriplanarContrast",
            uiTriplanarContrast);
    }

    // ####################################################################
    // ####################################################################

    private void LoadShaderConfig(Material mat)
    {
        GetMatFloat(mat,
            "_ProcBiomeCurveWeight",
            uiProcBiomeCurveWeight);
        GetMatVector2(mat,
            "_NoiseHeightData",
            uiNoiseHeightDataFreq,
            uiNoiseHeightDataAmp);
        GetMatVector2(mat,
            "_WorldHeightRange",
            uiWorldHeightRangeMin,
            uiWorldHeightRangeMax);
        GetMatFloat(mat,
            "_DistanceResampleAlbedoStrength",
            uiDistanceResampleAlbedoStrength);
        GetMatFloat(mat,
            "_DistanceResampleNormalStrength",
            uiDistanceResampleNormalStrength);
        GetMatFloat(mat,
            "_DistanceResampleMaterialStrength",
            uiDistanceResampleMaterialStrength);
        GetMatVector2(mat,
            "_NormalNoiseScaleStrength",
            uiNoiseNormal1ParamScale,
            uiNoiseNormal1ParamStrength);
        GetMatVector2(mat,
            "_NormalNoiseScaleStrength2",
            uiNoiseNormal2ParamScale,
            uiNoiseNormal2ParamStrength);
        GetMatVector2(mat,
            "_NormalNoiseScaleStrength3",
            uiNoiseNormal3ParamScale,
            uiNoiseNormal3ParamStrength);
        GetMatVector3(mat,
            "_ResampleDistanceParams",
            uiResampleDistanceUvScale,
            uiResampleDistanceBegin,
            uiResampleDistanceEnd);
        GetMatVector3(mat,
            "_DetailNoiseScaleStrengthFade",
            uiDetailNoiseScale,
            uiDetailNoiseStrength,
            uiDetailNoiseFadeDist,
            fx: UnScale);
        GetMatVector4(mat,
            "_DistanceNoiseScaleStrengthFade",
            uiDistanceNoiseScale,
            uiDistanceNoiseStrength,
            uiDistanceNoiseFadeStart,
            uiDistanceNoiseFadeEnd,
            fx: UnScale);
        GetMatVector4(mat,
            "_TriplanarUVScale",
            uiTriplanarUVScaleX,
            uiTriplanarUVScaleY,
            uiTriplanarUvOffsetX,
            uiTriplanarUvOffsetY,
            fx: UnScale,
            fy: UnScale);
        GetMatFloat(mat,
            "_TriplanarContrast",
            uiTriplanarContrast);
    }

    // ####################################################################
    // ####################################################################

    public override void OnOpen()
    {
        base.OnOpen();
        if (GameManager.IsDedicatedServer) return; // Nothing to do here
        if (MeshDescription.meshes.Length < MeshDescription.MESH_TERRAIN) return;
        var mesh = MeshDescription.meshes[MeshDescription.MESH_TERRAIN];
        LoadShaderConfig(mesh.material);
        UpdateUI(mesh.material);
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    // ####################################################################
    // ####################################################################

    private void UpdateUI(Material mat)
    {
        uiNoiseNormal1ParamScale.Enabled = uiNoiseNormal1ParamStrength.Enabled
            &= mat.HasTexture("_NormalNoise");
        uiNoiseNormal2ParamScale.Enabled = uiNoiseNormal2ParamStrength.Enabled
            &= mat.HasTexture("_NormalNoise2");
        uiNoiseNormal3ParamScale.Enabled = uiNoiseNormal3ParamStrength.Enabled
            &= mat.HasTexture("_NormalNoise3");
    }

    // ####################################################################
    // ####################################################################

    public override bool AlwaysUpdate() => true;

    // ####################################################################
    // ####################################################################

}

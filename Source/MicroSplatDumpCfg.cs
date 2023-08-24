using System;
using System.IO;
using System.Linq;
using System.Reflection;

public static class MicroSplatDumpCfg
{

    // ####################################################################
    // ####################################################################

    static readonly Type MSplat = ModManager.GetMod("OcbMicroSplat")
        .AllAssemblies.SelectMany(a => a.GetTypes())
        .First(x => x.Name == "OcbMicroSplat");
    static readonly MethodInfo GetLayerName = MSplat?.GetMethod("GetMicroSplatLayerName");
    static readonly MethodInfo GetTextureName = MSplat?.GetMethod("GetMicroSplatTextureName");

    // ####################################################################
    // ####################################################################

    public static void DumpLayerCfg(StreamWriter output, MicroSplatProceduralTextureConfig layers, int i, string indent = "  ")
    {
        var layer = layers.layers[i];
        var name = GetLayerName?.Invoke(null, new object[] { i });
        if (name == null) name = $"Layer{i}";
        var texture = GetTextureName?.Invoke(null, new object[] { i });
        if (texture == null) texture = $"microsplat{layer.textureIndex}";
        output.WriteLine(indent + "<biome-layer name=\"{0}\">", name);
        output.WriteLine(indent + "  <property name=\"weight\" value=\"{0}\"/>", layer.weight);
        output.WriteLine(indent + "  <property name=\"biome-weights-a\" value=\"{0}\"/>", CleanList(layer.biomeWeights));
        output.WriteLine(indent + "  <property name=\"biome-weights-b\" value=\"{0}\"/>", CleanList(layer.biomeWeights2));
        output.WriteLine(indent + "  <property name=\"microsplat-texture\" value=\"{0}\"/>", texture);
        output.WriteLine(indent + "  <property name=\"noise-active\" value=\"{0}\"/>", layer.noiseActive);
        output.WriteLine(indent + "  <property name=\"noise-frequency\" value=\"{0}\"/>", layer.noiseFrequency);
        output.WriteLine(indent + "  <property name=\"noise-offset\" value=\"{0}\"/>", layer.noiseOffset);
        output.WriteLine(indent + "  <property name=\"noise-range\" value=\"{0}\"/>", CleanList(layer.noiseRange));
        output.WriteLine(indent + "  <property name=\"slope-active\" value=\"{0}\"/>", layer.slopeActive);
        output.WriteLine(indent + "  <property name=\"slope-curve-mode\" value=\"{0}\"/>", layer.slopeCurveMode);
        if (layer.slopeCurve?.length >= 0)
        {
            output.WriteLine(indent + "  <slope-keyframes>");
            foreach (var frame in layer.slopeCurve.keys)
                output.WriteLine(indent + "    <keyframe time=\"{0}\" value=\"{1}\" inTangent=\"{2}\" outTangent=\"{3}\" inWeight=\"{4}\" outWeight=\"{5}\" weightedMode=\"{6}\" />",
                    frame.time, frame.value, frame.inTangent, frame.outTangent, frame.inWeight, frame.outWeight, frame.weightedMode);
            output.WriteLine(indent + "  </slope-keyframes>");
        }
        output.WriteLine(indent + "  <property name=\"height-active\" value=\"{0}\"/>", layer.heightActive);
        output.WriteLine(indent + "  <property name=\"height-curve-mode\" value=\"{0}\"/>", layer.heightCurveMode);
        if (layer.heightCurve?.length >= 0)
        {
            output.WriteLine(indent + "  <height-keyframes>");
            foreach (var frame in layer.heightCurve.keys)
                output.WriteLine(indent + "    <keyframe time=\"{0}\" value=\"{1}\" inTangent=\"{2}\" outTangent=\"{3}\" inWeight=\"{4}\" outWeight=\"{5}\" weightedMode=\"{6}\" />",
                    frame.time, frame.value, frame.inTangent, frame.outTangent, frame.inWeight, frame.outWeight, frame.weightedMode);
            output.WriteLine(indent + "  </height-keyframes>");
        }
        output.WriteLine(indent + "  <property name=\"erosion-active\" value=\"{0}\"/>", layer.erosionMapActive);
        output.WriteLine(indent + "  <property name=\"erosion-curve-mode\" value=\"{0}\"/>", layer.erosionCurveMode);
        output.WriteLine(indent + "  <property name=\"cavity-active\" value=\"{0}\"/>", layer.cavityMapActive);
        output.WriteLine(indent + "  <property name=\"cavity-curve-mode\" value=\"{0}\"/>", layer.cavityCurveMode);
        output.WriteLine(indent + "</biome-layer>");
    }

    private static string CleanList(object val)
    {
        if (val == null) return "";
        var str = val.ToString();
        if (str.Length == 0) return "";
        if (str[str.Length - 1] == ')')
            str = str.Remove(str.Length - 1, 1);
        if (str[0] == '(') str = str.Remove(0, 1);
        return str;
    }
}

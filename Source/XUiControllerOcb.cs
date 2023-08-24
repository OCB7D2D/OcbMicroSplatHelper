using System;
using UnityEngine;
using static MicroSplatPropData;

public class XUiControllerOcb : XUiController
{

    // ####################################################################
    // ####################################################################

    protected virtual void OnCfgUpdate(string name) { }

    // ####################################################################
    // ####################################################################

    protected void GetComboBoxInt(ref XUiC_ComboBoxInt obj, string name)
    {
        obj = GetChildById(name) as XUiC_ComboBoxInt;
        if (obj == null) Log.Error("Could not find `{0}` in UI", name);
        else obj.OnValueChanged += (sender, prev, now) => OnCfgUpdate(name);
    }

    protected void GetComboBoxFloat(ref XUiC_ComboBoxFloat obj, string name)
    {
        obj = GetChildById(name) as XUiC_ComboBoxFloat;
        if (obj == null) Log.Error("Could not find `{0}` in UI", name);
        else obj.OnValueChanged += (sender, prev, now) => OnCfgUpdate(name);
    }

    protected void GetComboBoxBool(ref XUiC_ComboBoxBool obj, string name)
    {
        obj = GetChildById(name) as XUiC_ComboBoxBool;
        if (obj == null) Log.Error("Could not find `{0}` in UI", name);
        else obj.OnValueChanged += (sender, prev, now) => OnCfgUpdate(name);
    }

    protected void GetComboBoxEnum<T>(ref XUiC_ComboBoxEnum<T> obj, string name) where T : struct, IConvertible
    {
        obj = GetChildById(name) as XUiC_ComboBoxEnum<T>;
        if (obj == null) Log.Error("Could not find `{0}` in UI", name);
        else obj.OnValueChanged += (sender, prev, now) => OnCfgUpdate(name);
    }

    // ####################################################################
    // ####################################################################

    protected void GetMatFloat(
        Material mat, string name, XUiC_ComboBoxFloat x,
        Func<float, float> fx = null)
    {
        if (mat.HasFloat(name) == false)
        {
            x.Enabled = false;
            return;
        }
        var value = mat.GetFloat(name);
        if (fx != null) value = fx(value);
        x.Enabled = true;
        x.Value = value;
    }

    protected void GetMatVector2(
        Material mat, string name, XUiC_ComboBoxFloat x, XUiC_ComboBoxFloat y,
        Func<float, float> fx = null, Func<float, float> fy = null)
    {
        if (mat.HasVector(name) == false)
        {
            x.Enabled = false;
            y.Enabled = false;
            return;
        }
        var vector = mat.GetVector(name);
        if (fx != null) vector.x = fx(vector.x);
        if (fy != null) vector.y = fy(vector.y);
        x.Enabled = true;
        y.Enabled = true;
        x.Value = vector.x;
        y.Value = vector.y;
    }

    protected void GetMatVector3(
        Material mat, string name, XUiC_ComboBoxFloat x, XUiC_ComboBoxFloat y, XUiC_ComboBoxFloat z,
        Func<float, float> fx = null, Func<float, float> fy = null, Func<float, float> fz = null)
    {
        if (mat.HasVector(name) == false)
        {
            x.Enabled = false;
            y.Enabled = false;
            z.Enabled = false;
            return;
        }
        var vector = mat.GetVector(name);
        if (fx != null) vector.x = fx(vector.x);
        if (fy != null) vector.y = fy(vector.y);
        if (fz != null) vector.z = fz(vector.z);
        x.Enabled = true;
        y.Enabled = true;
        z.Enabled = true;
        x.Value = vector.x;
        y.Value = vector.y;
        z.Value = vector.z;
    }

    protected void GetMatVector4(
        Material mat, string name, XUiC_ComboBoxFloat x, XUiC_ComboBoxFloat y, XUiC_ComboBoxFloat z, XUiC_ComboBoxFloat w,
        Func<float, float> fx = null, Func<float, float> fy = null, Func<float, float> fz = null, Func<float, float> fw = null)
    {
        if (mat.HasVector(name) == false)
        {
            x.Enabled = false;
            y.Enabled = false;
            z.Enabled = false;
            w.Enabled = false;
            return;
        }
        var vector = mat.GetVector(name);
        if (fx != null) vector.x = fx(vector.x);
        if (fy != null) vector.y = fy(vector.y);
        if (fz != null) vector.z = fz(vector.z);
        if (fw != null) vector.w = fw(vector.w);
        x.Enabled = true;
        y.Enabled = true;
        z.Enabled = true;
        w.Enabled = true;
        x.Value = vector.x;
        y.Value = vector.y;
        z.Value = vector.z;
        w.Value = vector.w;
    }

    // ####################################################################
    // ####################################################################

    protected void SetMatFloat(
    Material mat, string name, XUiC_ComboBoxFloat x,
    Func<float, float> fx = null)
    {
        if (mat.HasFloat(name) == false) return;
        var value = (float)x.Value;
        if (fx != null) value = fx(value);
        mat.SetFloat(name, value);
        // Log.Out("Set Float {0}", name);
    }

    protected void SetMatVector2(
        Material mat, string name, XUiC_ComboBoxFloat x, XUiC_ComboBoxFloat y,
        Func<float, float> fx = null, Func<float, float> fy = null)
    {
        if (mat.HasVector(name) == false) return;
        var vx = (float)x.Value;
        var vy = (float)y.Value;
        if (fx != null) vx = fx(vx);
        if (fy != null) vy = fy(vy);
        var vector = new Vector2(vx, vy);
        mat.SetVector(name, vector);
        // Log.Out("Set Vec2 {0}", name);
    }

    protected void SetMatVector3(
        Material mat, string name, XUiC_ComboBoxFloat x, XUiC_ComboBoxFloat y, XUiC_ComboBoxFloat z,
        Func<float, float> fx = null, Func<float, float> fy = null, Func<float, float> fz = null)
    {
        if (mat.HasVector(name) == false) return;
        var vx = (float)x.Value;
        var vy = (float)y.Value;
        var vz = (float)z.Value;
        if (fx != null) vx = fx(vx);
        if (fy != null) vy = fy(vy);
        if (fz != null) vz = fz(vz);
        var vector = new Vector3(vx, vy, vz);
        mat.SetVector(name, vector);
        // Log.Out("Set Vec3 {0}", name);
    }

    protected void SetMatVector4(
        Material mat, string name, XUiC_ComboBoxFloat x, XUiC_ComboBoxFloat y, XUiC_ComboBoxFloat z, XUiC_ComboBoxFloat w,
        Func<float, float> fx = null, Func<float, float> fy = null, Func<float, float> fz = null, Func<float, float> fw = null)
    {
        if (mat.HasVector(name) == false) return;
        var vx = (float)x.Value;
        var vy = (float)y.Value;
        var vz = (float)z.Value;
        var vw = (float)w.Value;
        if (fx != null) vx = fx(vx);
        if (fy != null) vy = fy(vy);
        if (fz != null) vz = fz(vz);
        if (fw != null) vw = fw(vw);
        var vector = new Vector4(vx, vy, vz, vw);
        mat.SetVector(name, vector);
        // Log.Out("Set Vec4 {0}", name);
    }

    // ####################################################################
    // ####################################################################

    protected float DoScale(float x) => Mathf.Pow(2, x);
    protected float UnScale(float x) => Mathf.Log(x) / Mathf.Log(2f);
    protected float DoScale(double x) => DoScale((float)x);

    // ####################################################################
    // ####################################################################

    protected float ToFloat(XUiC_ComboBoxFloat x,
        Func<float, float> fx = null)
    {
        var value = (float)x.Value;
        if (fx == null) return value;
        return fx(value);
    }

    protected Vector2 ToVec2(XUiC_ComboBoxFloat x, XUiC_ComboBoxFloat y,
        Func<float, float> fx = null, Func<float, float> fy = null)
        => new Vector2((float)x.Value, (float)y.Value);
    protected Vector3 ToVec3(XUiC_ComboBoxFloat x, XUiC_ComboBoxFloat y, XUiC_ComboBoxFloat z,
        Func<float, float> fx = null, Func<float, float> fy = null, Func<float, float> fz = null)
        => new Vector3((float)x.Value, (float)y.Value, (float)z.Value);
    protected Vector4 ToVec4(XUiC_ComboBoxFloat x, XUiC_ComboBoxFloat y, XUiC_ComboBoxFloat z, XUiC_ComboBoxFloat w,
        Func<float, float> fx = null, Func<float, float> fy = null, Func<float, float> fz = null, Func<float, float> fw = null)
        => new Vector4((float)x.Value, (float)y.Value, (float)z.Value, (float)w.Value);

    // ####################################################################
    // ####################################################################

    public float GetPropValue(MicroSplatPropData prop, int x, int y, int channel)
    {
        return prop.values[y * 32 + x][channel];
    }

    public Vector2 GetValue2(MicroSplatPropData prop, int x, int y, int channel)
    {
        Color color = prop.values[y * 32 + x];
        if (channel == 0) return new Vector2(color.r, color.g);
        else return new Vector2(color.b, color.a);
    }

    public float GetPropFloat(MicroSplatPropData prop, int textureIndex, PerTexFloat channel)
    {
        float num = (float)channel / 4f; int num2 = (int)num;
        int channel2 = Mathf.RoundToInt((num - num2) * 4f);
        return GetPropValue(prop, textureIndex, num2, channel2);
    }

    public Vector2 GetPropVector2(MicroSplatPropData prop, int textureIndex, PerTexVector2 channel)
    {
        float num = (float)channel / 4f;
        int num2 = (int)num;
        int channel2 = Mathf.RoundToInt((num - (float)num2) * 4f);
        return GetValue2(prop, textureIndex, num2, channel2);
    }

    // ####################################################################
    // ####################################################################

}

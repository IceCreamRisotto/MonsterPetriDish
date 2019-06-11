using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;


public class HoleText : Text
{
    //複寫mask方法為反向mask
    public override Material GetModifiedMaterial(Material baseMaterial)
    {
        var toUse = baseMaterial;
            var maskMat = StencilMaterial.Add(toUse, (1 << m_StencilValue) - 1, StencilOp.Keep, CompareFunction.NotEqual, ColorWriteMask.All, (1 << m_StencilValue) - 1, 0);
            StencilMaterial.Remove(m_MaskMaterial);
            m_MaskMaterial = maskMat;
            toUse = m_MaskMaterial;
        return toUse;
    }
}

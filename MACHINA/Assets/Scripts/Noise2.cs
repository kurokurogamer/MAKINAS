using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise2 : MonoBehaviour
{
    Shader m_shader;
    Material m_mat;
    [Range(0, 1)]
    public float horizonValue;
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (m_mat == null)
        {
            m_shader = Shader.Find("Hidden/Noise2");
            m_mat = new Material(m_shader);
            m_mat.hideFlags = HideFlags.DontSave;
        }
        // ランダムシード値を更新することで乱数を動かす
        m_mat.SetInt("_Seed", Time.frameCount);
        // 左右にずらす値をセット
        m_mat.SetFloat("_HorizonValue", horizonValue);
        Graphics.Blit(src, dest, m_mat);
    }
}

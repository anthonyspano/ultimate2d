using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DeathScreen : MonoBehaviour
{
   PostProcessVolume m_Volume;
   Vignette m_Vignette;
   ColorGrading m_ColorGrading;
   Vector4 v4;
   float w;

   void Start()
  {
     m_ColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
     m_ColorGrading.enabled.Override(true);
     m_ColorGrading.lift.value = new Vector4(0.79f, -0.21f, -0.15f, -1);
     m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_ColorGrading);

     // Create an instance of a vignette
     m_Vignette = ScriptableObject.CreateInstance<Vignette>();
     m_Vignette.enabled.Override(true);
     m_Vignette.intensity.Override(1f);
     // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
     m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
     w =0f;
     m_Volume.weight = w;
  }
   void Update()
  {
     w += 0.005f; 
     m_Volume.weight = w; 
  }

  void OnDestroy()
  {
      w = 0;
      m_Volume.weight = 0;
  }

}

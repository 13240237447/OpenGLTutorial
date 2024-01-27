using GlmNet;

namespace OpenGL;



public class Camera
{
    public Transform Transform { private set; get; }

    public Camera(vec3 initPos, vec3 initDir = default)
    {
        Transform = new Transform(initPos, initDir);
    }
  
    public void SetOffset(float pitch, float yaw)
    {
        if (Transform.Rotator.pitch  + pitch > 90)
        {
            return;
        }
        
        if (Transform.Rotator.pitch  + pitch < -90)
        {
            return;
        }
        Transform.Rotate(pitch,yaw);
    }
}
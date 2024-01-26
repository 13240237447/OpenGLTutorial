using GlmNet;

namespace OpenGL;

public class Camera
{
    public static vec3 WorldUp = new vec3(0, 1, 0);

    public static vec3 WorldForward = new vec3(0, 0, 1);

    private vec3 position;

    private vec3 dir;
    public vec3 Forward => (dir - position).Normalized();
    public vec3 Right => glm.cross(Forward, WorldUp).Normalized();

    public vec3 Up => glm.cross(Forward * - 1, Right).Normalized();
    public vec3 Position => position;

    public vec3 Direction => dir;
    public Camera(vec3 initPos, vec3 initDir = default)
    {
        position = initPos;
        dir = initDir.Normalized();
    }
    
    public void Translate(vec3 move)
    {
        position += move;
    }

}
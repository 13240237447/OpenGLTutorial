namespace OpenGL;
using GlmNet;



public struct Rotation
{
    public float pitch;

    public float yaw;

    public float roll;

    public Rotation()
    {
        yaw = -90;
        pitch = 0;
        roll = 0;
    }
}

public class Transform
{
    public static vec3 WorldUp = new vec3(0, 1, 0);

    public static vec3 WorldForward = new vec3(0, 0, -1);
    /// <summary>
    /// 位置
    /// </summary>
    public vec3 Position { set; get; }

    /// <summary>
    /// 前向量
    /// </summary>
    public vec3 Forward {private set; get; }


    public Rotation Rotator => rotator;

    private Rotation rotator;
    
    public vec3 SelfRight => glm.cross(Forward, WorldUp).Normalized();


    public vec3 SelfUp => glm.cross(-1 * Forward, SelfRight).Normalized();


    public Transform(vec3 pos, vec3 forward)
    {
        Position = pos;
        Forward = forward;
    }

    public void Translate(vec3 move)
    {
        Position += move;
    }

    public void Rotate(float pitch, float yaw)
    {
        rotator.pitch += pitch;
        rotator.yaw += yaw;
        
        vec3 front;
        front.x = MathF.Cos(glm.radians(rotator.yaw)) * MathF.Cos(glm.radians(rotator.pitch));
        front.z = MathF.Cos(glm.radians(rotator.pitch)) * MathF.Sin(glm.radians(rotator.yaw));
        front.y = MathF.Sin(glm.radians(rotator.pitch));

        Forward = front;
    }
}
using GLFW;
using GlmNet;
namespace OpenGL;

public class Camera
{
    public Transform Transform { private set; get; }

    private bool isFirstMouse = true;

    private float mouseMoveSisitivity = 0.2f;
    
    private vec2 lastMouse;

    private vec2 deltaMouseMove;

    
    public Camera(vec3 initPos, vec3 initDir = default)
    {
        Transform = new Transform(initPos, initDir);
        Program.OnProcessInput += ProcessInput;
        Program.OnMouseInput += ProcessMouse;
    }

    ~Camera()
    {
        Program.OnProcessInput -= ProcessInput;
        Program.OnMouseInput -= ProcessMouse;
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
    
    
    private void ProcessInput(Window window)
    {
        vec3? moveDir = null;
        if (Glfw.GetKey(window, Keys.W) == InputState.Press)
        {
            moveDir = Transform.Forward;
        }
        else if (Glfw.GetKey(window, Keys.S) == InputState.Press)
        {
            moveDir = Transform.Forward * -1;
        }
        else if (Glfw.GetKey(window, Keys.A) == InputState.Press)
        {
            moveDir = Transform.SelfRight * -1 ;
        }
        else if (Glfw.GetKey(window, Keys.D) == InputState.Press)
        {
            moveDir = Transform.SelfRight ;
        }

        if (moveDir != null)
        {
            moveDir = moveDir.Value.Normalized();
            float moveSpeed = 10f * Program.DeltaTime;
            Transform.Translate(moveDir.Value * moveSpeed);
        }
    }

    private void ProcessMouse(float x, float y)
    {
        if (isFirstMouse)
        {
            isFirstMouse = false;
            lastMouse.x = x;
            lastMouse.y = y;
        }
        deltaMouseMove.x = x - lastMouse.x;
        deltaMouseMove.y = lastMouse.y - y;
        lastMouse.x = x;
        lastMouse.y = y;

        var offset = deltaMouseMove * mouseMoveSisitivity;
        SetOffset(offset.y,offset.x);
    }
}
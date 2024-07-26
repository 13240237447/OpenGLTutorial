using GLFW;
using GlmNet;
using Monitor = GLFW.Monitor;
using static OpenGL.GL;

namespace OpenGL;

public static class GLUtil
{
    public static Window CreateWindow(ILesson lesson,int width, int height)
    {
        var window = Glfw.CreateWindow(width, height, lesson.GetType().Name, Monitor.None, Window.None);
        
        var screen = Glfw.PrimaryMonitor.WorkArea;
        var x = (screen.Width - width) / 2;
        var y = (screen.Height - height) / 2;
        Glfw.SetWindowPosition(window, x, y);
        
        Glfw.MakeContextCurrent(window);

        Import(Glfw.GetProcAddress);
        
        glViewport(0,0,width,height);
        
        Glfw.SetWindowSizeCallback(window, (p, h, w) =>
        {
            glViewport(0,0,h,w);
        });

        return window;
    }


    public static float GetScreenAspect()
    {
        var screen = Glfw.PrimaryMonitor.WorkArea;
        return screen.Width * 1.0f / screen.Height;
    }

    public static vec3 Normalized(this vec3 vec3)
    {
        return glm.normalize(vec3);
    }
    
}
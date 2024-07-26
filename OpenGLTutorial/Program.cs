using System;
using System.Reflection;
using GLFW;
using GlmNet;
using OpenGL;
using static OpenGL.GL;

class Program
{
    static unsafe void Main(string[] args)
    {
        // Test();
        LoadLesson(6);
    }

    public static event Action<Window> OnProcessInput; 
    
    public static event Action<float,float> OnMouseInput; 

    public static float DeltaTime { private set; get; }

    private static double lastTime;
    private static void LoadLesson(int id)
    {
        var lesson = GetLesson(id);
        {
            //初始化Graphics Library Framework
            Glfw.Init();
            //设置版本号
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            //设置OpenGL的渲染模式为核心渲染
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            

            var window = GLUtil.CreateWindow(lesson, 800, 600);

            // Glfw.SetInputMode(window,InputMode.Cursor, (int)CursorMode.Disabled);
            
            Glfw.SetCursorPositionCallback(window, MouseCallback);

            var data = lesson.PrepareData();
            
            DeltaTime = 0;
            lastTime = Glfw.Time;
            while (!Glfw.WindowShouldClose(window))
            {
                DeltaTime = (float)(Glfw.Time - lastTime);
                lastTime = Glfw.Time;
                ProcessInput(window);
                //设置清空的颜色
                glClearColor(.2f, .3f, .3f, 1);
                //清空当前颜色缓冲区
                glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

                lesson.Draw(data);

                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }

            Glfw.Terminate();
        }
    }

    private static void Test()
    {
        vec4 v = new vec4(1, 0,0,1);
        mat4 mat4 = new mat4(1);
        var trans = glm.translate(mat4, new vec3(1,1,0));
        v = trans * v;
        Console.WriteLine(v);
    }

    private static ILesson GetLesson(int id)
    {
        foreach (var type in typeof(ILesson).Assembly.GetTypes())
        {
            if (type.IsAssignableTo(typeof(ILesson)))
            {
                if (!type.IsAbstract && type.IsClass)
                {
                    var s = Activator.CreateInstance(type);
                    if (s is ILesson lesson)
                    {
                        if (lesson.Level == id)
                        {
                            return lesson;
                        }
                    }
                }
            }
        }

        return null;
    }

    static void ProcessInput(Window window)
    {
        if (Glfw.GetKey(window, Keys.Escape) == InputState.Press)
        {
            Glfw.SetWindowShouldClose(window, true);
            return;
        }
        OnProcessInput.Invoke(window);
    }

    static void MouseCallback(IntPtr window, double x, double y)
    {
        OnMouseInput?.Invoke((float)x,(float)y);
    }
}
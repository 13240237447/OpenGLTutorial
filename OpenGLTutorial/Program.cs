using System;
using System.Reflection;
using GLFW;
using OpenGL;
using static OpenGL.GL;

class Program
{
    static unsafe void Main(string[] args)
    {
        var lesson = GetLesson(2);
        {
            //初始化Graphics Library Framework
            Glfw.Init();
            //设置版本号
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            //设置OpenGL的渲染模式为核心渲染
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);


            var window = GLUtil.CreateWindow(lesson, 800, 600);

            var data = lesson.PrepareData();

            while (!Glfw.WindowShouldClose(window))
            {
                ProcessInput(window);
                //设置清空的颜色
                glClearColor(.2f, .3f, .3f, 1);
                //清空当前颜色缓冲区
                glClear(GL_COLOR_BUFFER_BIT);

                lesson.Draw(data);

                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }

            Glfw.Terminate();
        }
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
        }
    }
}
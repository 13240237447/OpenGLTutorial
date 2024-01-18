using GLFW;
using Monitor = GLFW.Monitor;
using static OpenGL.GL;

namespace OpenGL;

public static class Util
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

    private static uint CreateVertexShader(ILesson lesson)
    {
        var shader = glCreateShader(GL_VERTEX_SHADER);
        var shaderPath = $"Shader/{lesson.Level}/vertex.glsl";
        glShaderSource(shader,ReadShader(shaderPath));
        glCompileShader(shader);
        LogShader(shader);
        return shader;
    }
    
    private static uint CreateFragmentShader(ILesson lesson)
    {
        var shader = glCreateShader(GL_FRAGMENT_SHADER);
        var shaderPath = $"Shader/{lesson.Level}/fragment.glsl";
        glShaderSource(shader,ReadShader(shaderPath));
        glCompileShader(shader);
        LogShader(shader);
        return shader;
    }

    public static uint UseShader(ILesson lesson)
    {
        var vertex = CreateVertexShader(lesson);
        var fragment = CreateFragmentShader(lesson);
        var program = glCreateProgram();
        glAttachShader(program,vertex);
        glAttachShader(program,fragment);
        glLinkProgram(program);
        LogProgram(program);
        glUseProgram(program);
        glDeleteShader(vertex);
        glDeleteShader(fragment);
        return program;
    }

    private static unsafe void LogShader(uint shader)
    {
        int[] args = new int[1];
        fixed (int* pArgs = args)
        {
            glGetShaderiv(shader, GL_COMPILE_STATUS,pArgs);
            int result = *pArgs;
            Console.WriteLine($"ShaderCompile Result: {result}");
            if (result > 0)
            {
                Console.WriteLine($"Error Info: {glGetShaderInfoLog(shader)}");
            }
        }
    }

    private static unsafe void LogProgram(uint program)
    {
        int[] args = new int[1];
        fixed (int* pArgs = args)
        {
            glGetProgramiv(program, GL_LINK_STATUS, pArgs);
            int result = *pArgs;
            Console.WriteLine($"ProgramLink Result: {result}");
            if (result > 0)
            {
                Console.WriteLine($"Error Info: {glGetProgramInfoLog(program)}");
            }
        }
    }

    private static string ReadShader(string path)
    {
        return File.ReadAllText(path);
    }
    
    
}
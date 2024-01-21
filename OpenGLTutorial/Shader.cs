namespace OpenGL;
using static OpenGL.GL;

public class Shader : IDisposable
{
    private uint vertexShader;
    private uint fragmentShader;
    private uint program;
    private bool bUse;

    public Shader(ILesson lesson, string vertexSuffix = "", string fragmentSuffix = "")
    {
        vertexShader = CreateVertexShader(lesson, vertexSuffix);
        fragmentShader = CreateFragmentShader(lesson, fragmentSuffix);
        Use();
    }

    public void Use()
    {
        if (bUse)
        {
            return;
        }

        bUse = true;
        program = glCreateProgram();
        glAttachShader(program, vertexShader);
        glAttachShader(program, fragmentShader);
        glLinkProgram(program);
        LogProgram(program);
        glDeleteShader(vertexShader);
        glDeleteShader(fragmentShader);
        glUseProgram(program);
    }

    public void Dispose()
    {
        if (bUse)
        {
            glDeleteProgram(program);
        }
    }

    public void SetFloat(string uniformName, float[] values)
    {
        var location = glGetUniformLocation(program, uniformName);
        if (location >= 0)
        {
            switch (values.Length)
            {
                case 1:
                    glUniform1f(location, values[0]);
                    break;
                case 2:
                    glUniform2f(location, values[0], values[1]);
                    break;
                case 3:
                    glUniform3f(location, values[0], values[1], values[2]);
                    break;
                case 4:
                    glUniform4f(location, values[0], values[1], values[2], values[3]);
                    break;
            }
        }
        else
        {
            Console.WriteLine($"未找到uniform名为{uniformName}的位置");
        }
    }

    public void SetInt(string uniformName, int[] values)
    {
        var location = glGetUniformLocation(program, uniformName);
        if (location >= 0)
        {
            switch (values.Length)
            {
                case 1:
                    glUniform1i(location, values[0]);
                    break;
                case 2:
                    glUniform2i(location, values[0], values[1]);
                    break;
                case 3:
                    glUniform3i(location, values[0], values[1], values[2]);
                    break;
                case 4:
                    glUniform4i(location, values[0], values[1], values[2], values[3]);
                    break;
            }
        }
        else
        {
            Console.WriteLine($"未找到uniform名为{uniformName}的位置");
        }
    }
    
    public void SetBool(string uniformName, bool[] bools)
    {
        var location = glGetUniformLocation(program, uniformName);
        int[] values = new int[bools.Length];
        for (int i = 0; i < bools.Length; i++)
        {
            values[i] = bools[i] ? 1 : 0;
        }
        if (location >= 0)
        {
            switch (values.Length)
            {
                case 1:
                    glUniform1i(location, values[0]);
                    break;
                case 2:
                    glUniform2i(location, values[0], values[1]);
                    break;
                case 3:
                    glUniform3i(location, values[0], values[1], values[2]);
                    break;
                case 4:
                    glUniform4i(location, values[0], values[1], values[2], values[3]);
                    break;
            }
        }
        else
        {
            Console.WriteLine($"未找到uniform名为{uniformName}的位置");
        }
    }

    public void SetTexture(string uniformName)
    {
        var location = glGetUniformLocation(program, uniformName);
        if (location >= 0)
        {
        }
        else
        {
            Console.WriteLine($"未找到uniform名为{uniformName}的位置");
        }
    }


    private uint CreateVertexShader(ILesson lesson, string suffix = "")
    {
        var shader = glCreateShader(GL_VERTEX_SHADER);
        var shaderPath = $"Shader/{lesson.Level}/vertex{suffix}.glsl";
        glShaderSource(shader, ReadShader(shaderPath));
        glCompileShader(shader);
        LogShader(shader);
        return shader;
    }

    private uint CreateFragmentShader(ILesson lesson, string suffix = "")
    {
        var shader = glCreateShader(GL_FRAGMENT_SHADER);
        var shaderPath = $"Shader/{lesson.Level}/fragment{suffix}.glsl";
        glShaderSource(shader, ReadShader(shaderPath));
        glCompileShader(shader);
        LogShader(shader);
        return shader;
    }

    private static string ReadShader(string path)
    {
        return File.ReadAllText(path);
    }

    private unsafe void LogShader(uint shader)
    {
        int[] args = new int[1];
        fixed (int* pArgs = args)
        {
            glGetShaderiv(shader, GL_COMPILE_STATUS, pArgs);
            int result = *pArgs;
            if (result != 1)
            {
                Console.WriteLine($"ShaderCompile Error Info: {glGetShaderInfoLog(shader)}");
            }
        }
    }

    private unsafe void LogProgram(uint program)
    {
        int[] args = new int[1];
        fixed (int* pArgs = args)
        {
            glGetProgramiv(program, GL_LINK_STATUS, pArgs);
            int result = *pArgs;
            if (result != 1)
            {
                Console.WriteLine($"ProgramLink Error Info: {glGetProgramInfoLog(program)}");
            }
        }
    }
}
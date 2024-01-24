using GlmNet;

namespace OpenGL;

using GLFW;
using static OpenGL.GL;

public class Lesson5_Coordinate : ILesson
{
    public int Level => 5;

    public ELessonRun LessonRun { get; set; }


    private Texture2D texture2D0;

    private Texture2D texture2D1;

    private Shader shader;

    public object PrepareData()
    {
        glEnable(GL_DEPTH_TEST);
        return PrepareDataToMain();
    }

    public unsafe void Draw(object data)
    {
        var cubePos = GetCubePos();
        for (int i = 0; i < cubePos.Length; i++)
        {
            mat4 m = new mat4(1);
            m = glm.translate(m, cubePos[i]);
            float angle = 20.0f * i;
            if (i == 0)
            {
                m = glm.rotate(m,  (float)Glfw.Time,  new (1.0f, 0.3f, 0.5f));
            }
            if (i % 3 == 0)
            {
                m = glm.rotate(m,  (float)Glfw.Time*glm.radians(angle),  new (1.0f, 0.3f, 0.5f));
            }
            else
            {
                m = glm.rotate(m, glm.radians(angle),  new (1.0f, 0.3f, 0.5f));
            }
            shader.SetMVP(m,CreateViewMat4(),CreateProjectionMat4());
            glBindVertexArray((uint)data);
            glDrawArrays(GL_TRIANGLES, 0, 36);
        }
      
    }

    private object PrepareDataToMain()
    {
        texture2D0 = new Texture2D("container.jpg");
        texture2D1 = new Texture2D("awesomeface.png");

        uint vao = CreateMainVAO();
        shader = new Shader(this);


        shader.SetInt("texture1", 0);
        shader.SetInt("texture2", 1);


        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, texture2D0.Tex);

        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, texture2D1.Tex);

        return vao;
    }


    private mat4 CreateModelMat4()
    {
        mat4 m = new mat4(1);

        m = glm.rotate(m, (float)Glfw.Time * glm.radians(50), new vec3(0.5f, 1, 0));

        return m;
    }
    
    private mat4 CreateViewMat4()
    {
        mat4 m = new mat4(1);
        m = glm.translate(m, new vec3(0, 0, -3f));
        return m;
    }
    
    private mat4 CreateProjectionMat4()
    {
        return glm.perspective(glm.radians(45), GLUtil.GetScreenAspect(), 0.1f, 100f);
    }
    
    private unsafe uint CreateMainVAO()
    {
        var vao = glGenVertexArray();
        glBindVertexArray(vao);

        var vbo = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, vbo);

        var vertexData = PrimitiveUtil.GetCubeVertices();
        fixed (float* p = vertexData)
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertexData.Length, p, GL_STATIC_DRAW);
        }

        glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(float) * 5, (void*)0);
        glEnableVertexAttribArray(0);

        glVertexAttribPointer(1, 2, GL_FLOAT, false, sizeof(float) * 5, (void*)(sizeof(float) * 3));
        glEnableVertexAttribArray(1);

        glBindVertexArray(0);
        return vao;
    }

    
    vec3[] GetCubePos()
    {
        return new[]
        {
            new vec3( 0.0f,  0.0f,  0.0f),
            new vec3( 2.0f,  5.0f, -15.0f),
            new vec3(-1.5f, -2.2f, -2.5f),
            new vec3(-3.8f, -2.0f, -12.3f),
            new vec3( 2.4f, -0.4f, -3.5f),
            new vec3(-1.7f,  3.0f, -7.5f),
            new vec3( 1.3f, -2.0f, -2.5f),
            new vec3( 1.5f,  2.0f, -2.5f),
            new vec3( 1.5f,  0.2f, -1.5f),
            new vec3(-1.3f,  1.0f, -1.5f) 
        };
    }
   
    
}
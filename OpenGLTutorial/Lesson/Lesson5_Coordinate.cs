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
        return PrepareDataToMain();
    }

    public unsafe void Draw(object data)
    {

        glBindVertexArray((uint)data);

        glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, (void*)(0));
    }

    private object PrepareDataToMain()
    {
        texture2D0 = new Texture2D("container.jpg");
        texture2D1 = new Texture2D("awesomeface.png");

        uint vao = CreateMainVAO();
        shader = new Shader(this);


        shader.SetInt("texture1", 0);
        shader.SetInt("texture2", 1);

        shader.SetMVP(CreateModelMat4(),CreateViewMat4(),CreateProjectionMat4());

        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, texture2D0.Tex);

        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, texture2D1.Tex);

        return vao;
    }


    private mat4 CreateModelMat4()
    {
        mat4 m = new mat4(1);

        m = glm.rotate(m, glm.radians(-55), new vec3(1, 0, 0));

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
        return glm.perspective(45, GLUtil.GetScreenAspect(), 0.1f, 100f);
    }
    
    
    private unsafe uint CreateMainVAO()
    {
        var vao = glGenVertexArray();
        glBindVertexArray(vao);

        var vbo = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, vbo);

        var vertexData = PrimitiveUtil.GetRectArrayWithTexCoords();
        fixed (float* p = vertexData)
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertexData.Length, p, GL_STATIC_DRAW);
        }

        glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(float) * 5, (void*)0);
        glEnableVertexAttribArray(0);

        glVertexAttribPointer(1, 2, GL_FLOAT, false, sizeof(float) * 5, (void*)(sizeof(float) * 3));
        glEnableVertexAttribArray(1);

        var ibo = glGenBuffer();

        var indices = PrimitiveUtil.GetRectIndices();

        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ibo);

        fixed (int* p = indices)
        {
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.Length * sizeof(int), p, GL_STREAM_DRAW);
        }

        glBindVertexArray(0);
        return vao;
    }
}
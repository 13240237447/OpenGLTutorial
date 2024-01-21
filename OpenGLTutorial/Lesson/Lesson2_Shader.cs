using GLFW;

namespace OpenGL;
using static OpenGL.GL;


public class Lesson2_Shader : ILesson
{
    public int Level => 2;
    
    public ELessonRun LessonRun { get; set; }
    
    private Shader shaderProgram;

    public Lesson2_Shader()
    {
        LessonRun = ELessonRun.P2;
    }
    public object PrepareData()
    {
        switch (LessonRun)
        {
            case ELessonRun.P1:
                return P1();
            case ELessonRun.P2:
                return P2();
        }
        return Main();
    }
    
    
    private object Main()
    {
        var vao = CreateVAO();
        glPolygonMode(GL_FRONT_AND_BACK,GL_FILL);
        return vao;;
    }

    private object P1()
    {
        var vao = CreateVAO_P1();
        shaderProgram = new Shader(this,"_1","_1");
        glPolygonMode(GL_FRONT_AND_BACK,GL_FILL);
        return vao;;
    }

    private object P2()
    {
        shaderProgram = new Shader(this,"_2","_2");
        var vao = CreateVAO();
        glPolygonMode(GL_FRONT_AND_BACK,GL_FILL);
        return vao;;
    }
    
    public unsafe void Draw(object data)
    {
        switch (LessonRun)
        {
            case ELessonRun.Main:
                MainDraw(data);
                break;
            case ELessonRun.P1:
                P1Draw(data);
                break;
            case ELessonRun.P2:
                P2Draw(data);
                break;
        }
    }

    private void MainDraw(object data)
    {
        shaderProgram = new Shader(this);
        var time = Glfw.Time;
        var s = MathF.Sin((float)time) / 2 + 0.5f;
        shaderProgram.SetFloat("fragmentCol",new []{s,0,0,1});
        glBindVertexArray((uint)data);
        glDrawArrays(GL_TRIANGLES,0,3);
    }

    private void P1Draw(object data)
    {
        glBindVertexArray((uint)data);
        glDrawArrays(GL_TRIANGLES,0,3);
    }

    private void P2Draw(object data)
    {
        glBindVertexArray((uint)data);
        glDrawArrays(GL_TRIANGLES,0,3);
    }
    
    private unsafe uint CreateVAO()
    {
        var vao = glGenVertexArray();
        glBindVertexArray(vao);
      
        var vbo = glGenBuffer();
        var vertexData = PrimitiveUtil.GetTriangleArray();
        glBindBuffer(GL_ARRAY_BUFFER,vbo);
        fixed (float* p = vertexData)
        {
            glBufferData(GL_ARRAY_BUFFER,sizeof(float) *vertexData.Length,p,GL_STATIC_DRAW);
        }
        glVertexAttribPointer(0,3,GL_FLOAT,false,3*sizeof(float),NULL);
        glEnableVertexAttribArray(0);
        glBindVertexArray(0);
        return vao;
    }
    
    private unsafe uint CreateVAO_P1()
    {
        var vao = glGenVertexArray();
        glBindVertexArray(vao);
      
        var vbo = glGenBuffer();
        var vertexData = PrimitiveUtil.GetTriangleWithColorArray();
        glBindBuffer(GL_ARRAY_BUFFER,vbo);
        fixed (float* p = vertexData)
        {
            glBufferData(GL_ARRAY_BUFFER,sizeof(float) *vertexData.Length,p,GL_STATIC_DRAW);
        }
        glVertexAttribPointer(0,3,GL_FLOAT,false,7*sizeof(float),(void*)(0));
        glEnableVertexAttribArray(0);
        glVertexAttribPointer(1,4,GL_FLOAT,false,7*sizeof(float), (void*)(3* sizeof(float)));
        glEnableVertexAttribArray(1);
        glBindVertexArray(0);
        return vao;
    }
}
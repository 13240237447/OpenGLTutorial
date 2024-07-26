using GLFW;
using static OpenGL.GL;
namespace OpenGL;

public class HelloTriangle : ILesson
{
    public int Level => 1;

    private int frameCount = 0;
    public ELessonRun LessonRun { set; get; }

    public HelloTriangle()
    {
        LessonRun = ELessonRun.P3;
    }
    
    public object PrepareData()
    {
        switch (LessonRun)
        {
            case ELessonRun.Main:
                return Main();
            case ELessonRun.P1:
                return P1();
            case ELessonRun.P2:
                return P2();
            case ELessonRun.P3:
                return P1();
            
        }
        return P1();
    }

    private object Main()
    {
        var vao = CreateVAO();

        new Shader(this).Use();
        
        glPolygonMode(GL_FRONT_AND_BACK,GL_LINE);

        return vao;
    }
    
    private object P1()
    {
        var vao = CreateVAO_P1();

        new Shader(this).Use();
        
        glPolygonMode(GL_FRONT_AND_BACK,GL_FILL);

        return vao;
    }

    private object P2()
    {
        var vao1 = CreateVAO_P2_1();
        var vao2 = CreateVAO_P2_1();
        
        new Shader(this).Use();
        
        glPolygonMode(GL_FRONT_AND_BACK,GL_FILL);
        
        return (vao1, vao2);
    }

    public unsafe void Draw(object data)
    {
      
        switch (LessonRun)
        {
            case ELessonRun.Main:
                //绑定顶点数组
                glBindVertexArray((uint)data);
                glDrawElements(GL_TRIANGLES,6,GL_UNSIGNED_INT,(void *)0);
                break;
            case ELessonRun.P1:
                //绑定顶点数组
                glBindVertexArray((uint)data);
                glDrawArrays(GL_TRIANGLES,0,6);
                break;
            case ELessonRun.P2:
                //绑定顶点数组
                (uint, uint) twoVao =( (uint, uint))data;
                glBindVertexArray(twoVao.Item1);
                glDrawArrays(GL_TRIANGLES,0,3);
                glBindVertexArray(twoVao.Item2);
                glDrawArrays(GL_TRIANGLES,0,3);
                break;
            case ELessonRun.P3:
                if (frameCount < 100)
                {
                    using (new Shader(this))
                    {
                        
                    }
                }
                else
                {
                    new Shader(this,"","_red").Use();
                }
                frameCount++;
                if (frameCount > 200)
                {
                    frameCount = 0;
                }
                goto case ELessonRun.P1;
        }
    }
    

    float[] GetTriangleArray()
    {
        return new[]
        {
            -0.5f,0.5f,0,
            -0.5f,-0.5f,0,
            0.5f,0.5f,0,
            0.5f,-0.5f,0,
        };
    }


    float[] GetPractice1Array()
    {
        return new[]
        {
            -0.5f,0.5f,0,
            -0.5f,-0.5f,0,
            0.5f,0.5f,0,
            -0.5f,-0.5f,0,
            0.5f,0.5f,0,
            0.5f,-0.5f,0,
        };
    }

    float[] P2_Array()
    {
        return new[]
        {
            -0.5f,0.5f,0,
            -0.5f,-0.5f,0,
            0.5f,-0.5f,0
        };
    }
    
    
    int[] GetIndices()
    {
        return new[]
        {
            0,1,2,
            1,2,3
        };
    }
    

    private unsafe uint CreateVAO()
    {
        //申请顶点缓存vao
        var vao = glGenVertexArray();
        glBindVertexArray(vao);
        /*
         * vao可以理解成对多个vbo的封装
         * 它可以储存针对每个vbo属性的特殊操作
         */
        //申请一块buffer
        var vbo = glGenBuffer();
        var vertexData = GetTriangleArray();
        //将buffer设置为VBO类型
        //从这一刻起 使用的任何在GL_ARRAY_BUFFER目标上的缓冲调用都会用来配置当前绑定的VBO
        glBindBuffer(GL_ARRAY_BUFFER,vbo);
        fixed (float* p = vertexData)
        {
            //将顶点数据复制到缓冲的内存中
            glBufferData(GL_ARRAY_BUFFER,sizeof(float) *vertexData.Length,p,GL_STATIC_DRAW);
        }
        //跟gpu解释如何使用这些顶点数据
        glVertexAttribPointer(0,3,GL_FLOAT,false,3*sizeof(float),NULL);

        //启用顶点属性
        glEnableVertexAttribArray(0);
        
        //申请索引缓存ibo
        var ibo = glGenBuffer();
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER,ibo);
        var indicesData = GetIndices();
        fixed (int* p = indicesData)
        {
            glBufferData(GL_ELEMENT_ARRAY_BUFFER,sizeof(int) * indicesData.Length,p,GL_STATIC_DRAW);
        }
     
        //解绑vao 供其它使用
        glBindVertexArray(0);
        return vao;
    }
    
    private unsafe uint CreateVAO_P1()
    {
        var vao = glGenVertexArray();
        glBindVertexArray(vao);
      
        var vbo = glGenBuffer();
        var vertexData = GetPractice1Array();
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


    private unsafe uint CreateVAO_P2_1()
    {
        var vao = glGenVertexArray();
        glBindVertexArray(vao);

        var vbo = glGenBuffer();
        var vertexData = P2_Array();
        glBindBuffer(GL_ARRAY_BUFFER,vbo);
        fixed (float* p = vertexData)
        {
            glBufferData(GL_ARRAY_BUFFER,sizeof(float) * vertexData.Length,p,GL_STATIC_DRAW);
        }
        glVertexAttribPointer(0,3,GL_FLOAT,false,3*sizeof(float),NULL);
        glEnableVertexAttribArray(0);
        glBindVertexArray(0);
        return vao;
    }
    
    
}
using GLFW;
using static OpenGL.GL;
namespace OpenGL;

public class HelloTriangle : ILesson
{
    public int Level => 1;
    
    public object PrepareData()
    {
        var vao = CreateVAO();

        Util.UseShader(this);
        
        glPolygonMode(GL_FRONT_AND_BACK,GL_LINE);

        return vao;
    }

    public unsafe void Draw(object data)
    {
        //绑定顶点数组
        glBindVertexArray((uint)data);
        //绘制顶点数据
        glDrawElements(GL_TRIANGLES,6,GL_UNSIGNED_INT,(void *)0);

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

        //申请索引缓存ibo
        var ibo = glGenBuffer();
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER,ibo);
        var indicesData = GetIndices();
        fixed (int* p = indicesData)
        {
            glBufferData(GL_ELEMENT_ARRAY_BUFFER,sizeof(int) * indicesData.Length,p,GL_STATIC_DRAW);
        }
        
        //启用顶点属性
        glEnableVertexAttribArray(0);
     
        //解绑vao 供其它使用
        glBindVertexArray(0);
        return vao;
    }
    
}
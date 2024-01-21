namespace OpenGL;

using static OpenGL.GL;

public class Lesson3_Texture : ILesson
{
    public int Level => 3;
    
    public ELessonRun LessonRun { get; set; }

    private Texture2D texture2D;
    public object PrepareData()
    {
        texture2D = new Texture2D("wall.jpg");
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);	// set texture wrapping to GL_REPEAT (default wrapping method)
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
        // set texture filtering parameters
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
        
        return PrepareDataToMain();
    }

    public void Draw(object data)
    {
        
        glBindTexture(GL_TEXTURE_2D,texture2D.Tex);

        new Shader(this);

        glBindVertexArray((uint)data);
        
        glDrawArrays(GL_TRIANGLES,0,3);
    }


    private object PrepareDataToMain()
    {
        uint vao = CreateMainVAO();

        var shader = new Shader(this);
        return vao;
    }

    private unsafe uint CreateMainVAO()
    {
        var vao = glGenVertexArray();
        glBindVertexArray(vao);

        var vbo = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER,vbo);
        
        var vertexData = PrimitiveUtil.GetTriangleWithTexCoords();
        fixed (float* p = vertexData)
        {
            glBufferData(GL_ARRAY_BUFFER,sizeof(float) *vertexData.Length,p,GL_STATIC_DRAW);
        }
        glVertexAttribPointer(0,3,GL_FLOAT,false,sizeof(float)*5,(void*)0);
        glEnableVertexAttribArray(0);
        
        glVertexAttribPointer(1,2,GL_FLOAT,false,sizeof(float)*5,(void*)(sizeof(float) * 3));
        glEnableVertexAttribArray(1);

        glBindVertexArray(0);
        return vao;
    }
}
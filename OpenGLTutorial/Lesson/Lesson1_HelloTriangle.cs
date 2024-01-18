using GLFW;
using static OpenGL.GL;
namespace OpenGL;

public class HelloTriangle : ILesson
{
    public int Level => 1;
    
    public void Run()
    {
        //初始化Graphics Library Framework
        Glfw.Init();
        //设置版本号
        Glfw.WindowHint(Hint.ContextVersionMajor,3);
        Glfw.WindowHint(Hint.ContextVersionMinor,3);
        //设置OpenGL的渲染模式为核心渲染
        Glfw.WindowHint(Hint.OpenglProfile,Profile.Core);
        
        var window = Util.CreateWindow(this,800,600);
        
        var vao = CreateVAO();
        
        Util.UseShader(this);

        while (!Glfw.WindowShouldClose(window))
        {
            ProcessInput(window);
         
            glClearColor(.2f,.3f,.3f,1);
            glClear(GL_COLOR_BUFFER_BIT);
            
            glBindVertexArray(vao);
            glDrawArrays(GL_TRIANGLES,0,3);
       
            
            Glfw.SwapBuffers(window);
            Glfw.PollEvents();
        }
        
        Glfw.Terminate();
    }
    

    float[] GetTriangleArray()
    {
        return new[]
        {
            -0.5f,-0.5f,0,
            0.5f,-0.5f,0,
            0f,0.5f,0
        };
    }

    private unsafe uint CreateVAO()
    {
        //1绑定vao
        var vao = glGenVertexArray();
        glBindVertexArray(vao);
        
        
        var vbo = glGenBuffer();
        var data = GetTriangleArray();
        glBindBuffer(GL_ARRAY_BUFFER,vbo);
        fixed (float* p = data)
        {
            glBufferData(GL_ARRAY_BUFFER,sizeof(float) *data.Length,p,GL_STATIC_DRAW);
        }
        glVertexAttribPointer(0,3,GL_FLOAT,false,3*sizeof(float),NULL);
        glEnableVertexAttribArray(0);
     
        return vao;
    }
    
    void ProcessInput(Window window)
    {
        if (Glfw.GetKey(window,Keys.Escape) == InputState.Press)
        {
            Glfw.SetWindowShouldClose(window,true);
        }
    }
   
    public void ShutDown()
    {
    }
}
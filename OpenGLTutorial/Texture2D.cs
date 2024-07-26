namespace OpenGL;
using static OpenGL.GL;
public class Texture2D
{

    private int width;

    private int height;

    private int channels;

    private byte[] data;

    private uint tex;
    
    public int Width => width;

    public int Height => height;

    public int Channels => channels;

    public uint Tex => tex;

    public Texture2D(string path)
    {
        LoadImage(path);
        CreateTex();
    }

    private void LoadImage(string path)
    {
        byte[] imageData = File.ReadAllBytes($"Resources/{path}");
        // 调用接口从流中加载图像数据
        int requestedChannels = 0;
        data = StbNative.Native.load_from_memory(imageData, out width, out height, out channels, requestedChannels);
    }
    

    private unsafe void CreateTex()
    {
        tex = glGenTexture();
       	// set texture wrapping to GL_REPEAT (default wrapping method)
        
        // glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);	// set texture wrapping to GL_REPEAT (default wrapping method)
        // glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
        // set texture filtering parameters
        // glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
        // glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
        
        glBindTexture(GL_TEXTURE_2D,tex);

        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);	// set texture wrapping to GL_REPEAT (default wrapping method)
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
        // glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

        fixed (byte* p = data)
        {
            var format = channels == 4 ? GL_RGBA : GL_RGB;
            glTexImage2D(GL_TEXTURE_2D,0,format,width,height,0,format,GL_UNSIGNED_BYTE,p);
        }
        
        glGenerateMipmap(GL_TEXTURE_2D);
        
        glBindTexture(GL_TEXTURE_2D,0);
        
        Console.WriteLine(tex);
    }
}
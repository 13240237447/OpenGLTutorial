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
        byte[] imageData = File.ReadAllBytes($"Resources/{path}");
        // 调用接口从流中加载图像数据
        int requestedChannels = 0;
        data = StbNative.Native.load_from_memory(imageData, out width, out height, out channels, requestedChannels);
        CreateTex();
    }

    private unsafe void CreateTex()
    {
        tex = glGenTexture();
        
        glBindTexture(GL_TEXTURE_2D,tex);

        fixed (byte* p = data)
        {
            glTexImage2D(GL_TEXTURE_2D,0,GL_RGB,width,height,0,GL_RGB,GL_UNSIGNED_BYTE,p);
        }
        
        glGenerateMipmap(GL_TEXTURE_2D);
        
        glBindTexture(GL_TEXTURE_2D,0);
        
    }
}
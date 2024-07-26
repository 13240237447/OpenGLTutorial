namespace OpenGL;

public static class PrimitiveUtil
{
    
    /// <summary>
    /// 获取三角形数组
    /// </summary>
    /// <returns></returns>
    public static float[] GetTriangleArray()
    {
        return new[]
        {
            0,0.5f,0,
            -0.5f,-0.5f,0,
            0.5f,-0.5f,0,
        };
    }
    
    /// <summary>
    /// 获取一个带颜色的三角形
    /// </summary>
    /// <returns></returns>
    public static float[] GetTriangleWithColorArray()
    {
        return new[]
        {
            0f,0.5f,0, 1,0,0,1,
            -0.5f,-0.5f,0, 0,1,0,1,
            0.5f,-0.5f,0,  0,0,1,1
        };
    }
    
    public static float[] GetTriangleWithTexCoords()
    {
        return new[]
        {
            0f,0.5f,0,            0.5f,1f,
            -0.5f,-0.5f,0,        0,0,
            0.5f,-0.5f,0,         1,0
        };
    }
    

    public static float[] GetRectArrayWithTexCoords()
    {
        return new[]
        {
            -0.5f,0.5f,0,       0,1,
            -0.5f,-0.5f,0,      0,0,
            0.5f,0.5f,0,        1,1,
            0.5f,-0.5f,0,       1,0
        };
    }
    
    public static int[] GetRectIndices()
    {
        return new[]
        {
            0,1,2,
            1,2,3
        };
    }
    
    public static float[] GetCubeVertices()
    {
        return new[]
        {
            -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
            0.5f, -0.5f, -0.5f, 1.0f, 0.0f,
            0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
            0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
            -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,

            -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
            0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
            0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
            0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
            -0.5f, 0.5f, 0.5f, 0.0f, 1.0f,
            -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,

            -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
            -0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
            -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
            -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,

            0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
            0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
            0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
            0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
            0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
            0.5f, 0.5f, 0.5f, 1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
            0.5f, -0.5f, -0.5f, 1.0f, 1.0f,
            0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
            0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
            -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,

            -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
            0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
            0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
            0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
            -0.5f, 0.5f, 0.5f, 0.0f, 0.0f,
            -0.5f, 0.5f, -0.5f, 0.0f, 1.0f
        };
    }
}
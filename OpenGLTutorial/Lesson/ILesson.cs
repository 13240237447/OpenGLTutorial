namespace OpenGL;

public interface ILesson
{
    int Level { get; }

    void Run();

    void ShutDown();
}
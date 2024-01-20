namespace OpenGL;

public interface ILesson
{
    int Level { get; }

    object PrepareData();

    void Draw(object data);

}
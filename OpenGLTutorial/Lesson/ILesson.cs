namespace OpenGL;

public interface ILesson
{
    int Level { get; }

    ELessonRun LessonRun { set; get; }

    object PrepareData();

    void Draw(object data);
    
}

public enum ELessonRun
{
    Main,
    P1,
    P2,
    P3,
    P4,
    P5,
    P6
}
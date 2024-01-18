using System;
using System.Reflection;
using GLFW;
using OpenGL;

class Program
{
    static void Main(string[] args)
    {
        var lesson = GetLesson(1);
        if (lesson != null)
        {
            lesson.Run();
        }
    }

    private static ILesson GetLesson(int id)
    {
        foreach (var type in typeof(ILesson).Assembly.GetTypes())
        {
            if (type.IsAssignableTo(typeof(ILesson)))
            {
                if (!type.IsAbstract && type.IsClass)
                {
                    var s = Activator.CreateInstance(type);
                    if (s is ILesson lesson )
                    {
                        if (lesson.Level == id)
                        {
                            return lesson;
                        }
                    }
                }
                
            }
        }
        return null;
    }
    
}
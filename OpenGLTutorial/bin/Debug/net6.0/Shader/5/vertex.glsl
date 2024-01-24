#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 texCoods;

out vec2 outTexCoords;

//模型矩阵 将local -> world
uniform mat4 model;
//观察矩阵 将world -> camera space
uniform mat4 view;
//投影矩阵 将camera space -> clip space
uniform mat4 projection;

void main()
{
    gl_Position = projection * view * model * vec4(aPos.x,aPos.y,aPos.z,1.0f);
    outTexCoords = texCoods;
}
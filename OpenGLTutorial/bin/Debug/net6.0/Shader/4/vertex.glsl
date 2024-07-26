#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 texCoods;
uniform mat4 transform;

out vec2 outTexCoords;
void main()
{
    gl_Position = transform * vec4(aPos.x,aPos.y,aPos.z,1.0f);
    outTexCoords = texCoods;
}
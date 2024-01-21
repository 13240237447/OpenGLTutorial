#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 texCoods;

out vec2 outTexCoords;
void main()
{
    gl_Position = vec4(aPos.x,aPos.y,aPos.z,1.0f);
    outTexCoords = (texCoods * 2 - vec2(1,1)) / 1;
}
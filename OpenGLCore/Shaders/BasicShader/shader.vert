#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec4 aColor;

out vec4 FragCol;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
    FragCol = aColor;

    gl_Position = projection * view * vec4(aPos, 1.0);
}
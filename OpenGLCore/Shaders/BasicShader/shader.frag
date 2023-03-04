#version 330 core
out vec4 ResultColor;

in vec4 FragCol;

void main()
{
    ResultColor = FragCol;
}
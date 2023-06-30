#version 330 core
out vec4 outputColor;
uniform vec4 colorVar;

void main()
{
    outputColor = colorVar;
}
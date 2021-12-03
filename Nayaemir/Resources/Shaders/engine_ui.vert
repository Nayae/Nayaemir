#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec4 aColor;
layout (location = 2) in vec3 aTexCoord;

out vec3 fTexCoord;
out vec4 fColor;

uniform mat4 uView;
uniform mat4 uProjection;

void main()
{
    gl_Position = uProjection * uView * vec4(aPos, 1.0);
    fTexCoord = aTexCoord;
    fColor = aColor;
}
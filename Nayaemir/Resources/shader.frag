#version 330 core
in vec4 fragColor;
in vec2 texCoord;

out vec4 FragColor;

uniform sampler2D uTexture0;

void main()
{
    FragColor = texture(uTexture0, texCoord);
} 
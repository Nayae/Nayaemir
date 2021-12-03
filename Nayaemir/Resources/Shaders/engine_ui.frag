#version 330 core
in vec4 fColor;
in vec3 fTexCoord;

uniform sampler2D uTexture0;
uniform float uLight;

out vec4 FragColor;

void main()
{
    if(fTexCoord.z == 0.0) {
        FragColor = vec4(fColor);
    } else {
        FragColor = texture(uTexture0, fTexCoord.xy) * fColor;    
    }
}
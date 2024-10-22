#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Texel size must be 1.0f / ( texture_size - outline_size ) on U and V.
float2 Param_TexelSize = float2( 1.0f, 1.0f );
float4 Param_Color     = float4( 1.0f, 1.0f, 1.0f, 1.0f );

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state {
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput {
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float GetTexelAlpha( float2 texture_coords, float power_x, float power_y ) {
    float2 texel_size = float2( power_x * Param_TexelSize.x, power_y * Param_TexelSize.y );
    float2 texel_uv   = texture_coords + texel_size;
    float texel_a     = tex2D( SpriteTextureSampler, texel_uv ).a;
    
    return ceil( texel_a );
}

float4 MainPS( VertexShaderOutput input ) : COLOR {
    float2 uv    = input.TextureCoordinates;
    float4 color = tex2D( SpriteTextureSampler, uv ) * input.Color;
    float center = ceil( color.a );
    float left   = GetTexelAlpha( input.TextureCoordinates, -1.0,  0.0 ) * ceil( uv.x );
    float right  = GetTexelAlpha( input.TextureCoordinates,  1.0,  0.0 ) - floor( uv.x );
    float up     = GetTexelAlpha( input.TextureCoordinates,  0.0, -1.0 ) * ceil( uv.y );
    float down   = GetTexelAlpha( input.TextureCoordinates,  0.0,  1.0 ) - floor( uv.y );
    float total  = ( left + right + up + down );
    
    if ( center > 0.0f && total < 4.0f )
        color = Param_Color;
    
    return color;
}

technique SpriteDrawing {
	pass P0 {
		PixelShader = compile PS_SHADERMODEL MainPS( );
	}
};

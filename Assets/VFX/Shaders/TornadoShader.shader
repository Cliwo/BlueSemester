// Upgrade NOTE: upgraded instancing buffer 'Tornado' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Tornado"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0
		_TextureSpeed("TextureSpeed", Range( -10 , 10)) = 5
		_Tiling("Tiling", Float) = 10
		_WaveSpeed("WaveSpeed", Float) = 1
		_NumberOfWave("NumberOfWave", Float) = 1
		_Alpha("Alpha", Range( 0 , 1)) = 1
		_TornadoColor("TornadoColor", Color) = (0.1682093,0.5499123,0.7924528,0)
		_Emission("Emission", Float) = 5
		_FresnelPower("FresnelPower", Float) = 3
		_FresnelGlow("FresnelGlow", Range( 1 , 10)) = 1
		_FresnelColor("FresnelColor", Color) = (0.9622642,0.8125383,0.4312033,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float2 uv_texcoord;
		};

		uniform float _WaveSpeed;
		uniform float _NumberOfWave;
		uniform float _FresnelPower;
		uniform float _FresnelGlow;
		uniform float4 _FresnelColor;
		uniform float _Emission;
		uniform float4 _TornadoColor;
		uniform float _Tiling;
		uniform float _TextureSpeed;
		uniform float _Cutoff = 0;

		UNITY_INSTANCING_BUFFER_START(Tornado)
			UNITY_DEFINE_INSTANCED_PROP(float, _Alpha)
#define _Alpha_arr Tornado
		UNITY_INSTANCING_BUFFER_END(Tornado)


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float temp_output_81_0 = ( ( _Time.y * 1.0 ) + ( 1.0 * ase_vertex3Pos.y ) );
			float temp_output_93_0 = ( 0.5 * 1.0 );
			float3 temp_cast_0 = (( ( ( sin( temp_output_81_0 ) * temp_output_93_0 ) * 0.1 ) + 0.05 )).xxx;
			float3 temp_cast_1 = (( ( ( temp_output_93_0 * sin( ( temp_output_81_0 + ( 1.0 - 0.0 ) ) ) ) * 0.1 ) + 0.05 )).xxx;
			float3 temp_cast_2 = (( ase_vertexNormal.z * ( sin( ( ( ( ( 1.0 - ase_vertex3Pos.y ) + ( _Time.y * _WaveSpeed ) ) * 6.28318548202515 ) * _NumberOfWave ) ) * v.texcoord.xy.y ) * 30.0 )).xxx;
			float3 temp_cast_4 = (mul( temp_cast_2, float3x3(temp_cast_0, float3( 0,0,0 ), temp_cast_1) ).x).xxx;
			v.vertex.xyz += temp_cast_4;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV58 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode58 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV58, _FresnelPower ) );
			float2 temp_cast_0 = (_Tiling).xx;
			float2 temp_cast_1 = (( _Time.y * _TextureSpeed )).xx;
			float2 uv_TexCoord75 = i.uv_texcoord * temp_cast_0 + temp_cast_1;
			float simplePerlin3D8 = snoise( float3( uv_TexCoord75 ,  0.0 ) );
			float temp_output_78_0 = (simplePerlin3D8*0.5 + 0.5);
			o.Emission = ( ( ( fresnelNode58 * _FresnelGlow ) * _FresnelColor ) + ( _Emission * ( _TornadoColor * temp_output_78_0 ) ) ).rgb;
			o.Alpha = 1;
			float _Alpha_Instance = UNITY_ACCESS_INSTANCED_PROP(_Alpha_arr, _Alpha);
			float clampResult57 = clamp( ( ( 1.0 - temp_output_78_0 ) * (1.0 + (_Alpha_Instance - 0.0) * (0.0 - 1.0) / (1.0 - 0.0)) ) , 0.0 , 1.0 );
			clip( clampResult57 - _Cutoff );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
562.4;252;480;282;1201.543;-976.3301;1.863022;True;False
Node;AmplifyShaderEditor.CommentaryNode;73;-1872.147,-1499.125;Float;False;2619.786;1538.775;Shape grediant;35;106;38;105;71;37;34;99;100;97;98;29;26;24;96;95;21;86;94;25;93;20;84;70;28;19;83;81;18;17;13;87;80;89;82;92;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;82;-1778.443,-225.3587;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;92;-1720.253,-687.4018;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;89;-1717.233,-459.2915;Float;True;Constant;_Float1;Float 1;10;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;13;-1548.562,-1344.533;Float;True;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;18;-1503.15,-1081.873;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1689.105,-1064.787;Float;False;Property;_WaveSpeed;WaveSpeed;3;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;-1482.857,-686.9847;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-1486.728,-340.7909;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;110;-1857.796,755.4669;Float;False;2228.512;1035.371;Color & Transparency;17;1;2;7;3;75;8;79;78;50;47;51;48;53;56;54;55;57;;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;28;-1282.411,-1300.103;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;83;-1190.761,-222.2736;Float;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-1319.151,-1078.673;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;81;-1210.381,-686.2046;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-1807.796,1143.864;Float;False;Property;_TextureSpeed;TextureSpeed;1;0;Create;True;0;0;False;0;5;-4.08;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;1;-1705.494,920.6493;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1449.621,927.7539;Float;False;Property;_Tiling;Tiling;2;0;Create;True;0;0;False;0;10;8.13;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;84;-957.2701,-244.7179;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-1487.464,1068.304;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-1083.472,-1324.96;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;70;-1080.376,-1095.068;Float;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-764.6687,-1098.91;Float;False;Property;_NumberOfWave;NumberOfWave;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;75;-1229.64,1023.932;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;94;-931.8423,-685.7837;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;67;-1334.133,166.1572;Float;False;1199.417;504.9097;Fresnel;7;63;66;59;62;65;58;64;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;-759.9396,-472.4106;Float;False;2;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;86;-722.5892,-246.949;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-819.514,-1324.96;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-934.7194,1311.089;Float;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-1236.506,443.5857;Float;False;Property;_FresnelPower;FresnelPower;8;0;Create;True;0;0;False;0;3;1.87;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;-482.5273,-686.4891;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;8;-943.2086,1037.687;Float;True;Simplex3D;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;-480.6645,-457.3484;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-522.4705,-1324.96;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;59;-1284.133,217.1312;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FresnelNode;58;-1011.713,216.8512;Float;True;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-980.8247,441.3298;Float;False;Property;_FresnelGlow;FresnelGlow;9;0;Create;True;0;0;False;0;1;2.33;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;78;-693.9312,1066.733;Float;True;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;26;-275.0571,-1324.837;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;-255.309,-686.6245;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;-252.9718,-457.1852;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;50;-617.8226,852.1454;Float;False;Property;_TornadoColor;TornadoColor;6;0;Create;True;0;0;False;0;0.1682093,0.5499123,0.7924528,0;0.4130473,0.5744482,0.735849,0.3372549;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-339.9123,-1078.904;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;47;-752.5538,1531.238;Float;False;InstancedProperty;_Alpha;Alpha;5;0;Create;True;0;0;False;0;1;0.3975638;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-50.71902,-1165.123;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;66;-638.5878,435.5758;Float;False;Property;_FresnelColor;FresnelColor;10;0;Create;True;0;0;False;0;0.9622642,0.8125383,0.4312033,0;0.5829477,0.8696418,0.9433962,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;99;-24.25584,-686.5181;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.05;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-350.0612,1047.469;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-643.7142,216.2464;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;37;-69.74879,-1449.125;Float;True;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;56;-310.1929,805.4669;Float;False;Property;_Emission;Emission;7;0;Create;True;0;0;False;0;5;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-21.5657,-940.4614;Float;False;Constant;_WaveAmount;WaveAmount;10;0;Create;True;0;0;False;0;30;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;100;-22.95567,-457.7186;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.05;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;53;-444.9016,1537.395;Float;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;51;-346.4858,1297.031;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-360.7343,216.1572;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-64.48454,1047.205;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;203.2512,-1285.125;Float;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-121.2629,1406.803;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;105;231.1668,-685.9246;Float;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.ClampOpNode;57;129.1906,1407.885;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;68;271.4129,216.101;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;106;509.9106,-704.8148;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3x3;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;45;926.5616,170.7256;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Tornado;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0;True;True;0;True;Transparent;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;87;0;92;0
WireConnection;87;1;89;0
WireConnection;80;0;89;0
WireConnection;80;1;82;2
WireConnection;28;0;13;2
WireConnection;19;0;18;0
WireConnection;19;1;17;0
WireConnection;81;0;87;0
WireConnection;81;1;80;0
WireConnection;84;0;81;0
WireConnection;84;1;83;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;20;0;28;0
WireConnection;20;1;19;0
WireConnection;75;0;7;0
WireConnection;75;1;3;0
WireConnection;94;0;81;0
WireConnection;93;1;89;0
WireConnection;86;0;84;0
WireConnection;21;0;20;0
WireConnection;21;1;70;0
WireConnection;95;0;94;0
WireConnection;95;1;93;0
WireConnection;8;0;75;0
WireConnection;96;0;93;0
WireConnection;96;1;86;0
WireConnection;24;0;21;0
WireConnection;24;1;25;0
WireConnection;58;0;59;0
WireConnection;58;3;62;0
WireConnection;78;0;8;0
WireConnection;78;1;79;0
WireConnection;78;2;79;0
WireConnection;26;0;24;0
WireConnection;97;0;95;0
WireConnection;98;0;96;0
WireConnection;34;0;26;0
WireConnection;34;1;29;2
WireConnection;99;0;97;0
WireConnection;48;0;50;0
WireConnection;48;1;78;0
WireConnection;63;0;58;0
WireConnection;63;1;64;0
WireConnection;100;0;98;0
WireConnection;53;0;47;0
WireConnection;51;0;78;0
WireConnection;65;0;63;0
WireConnection;65;1;66;0
WireConnection;55;0;56;0
WireConnection;55;1;48;0
WireConnection;38;0;37;3
WireConnection;38;1;34;0
WireConnection;38;2;71;0
WireConnection;54;0;51;0
WireConnection;54;1;53;0
WireConnection;105;0;99;0
WireConnection;105;2;100;0
WireConnection;57;0;54;0
WireConnection;68;0;65;0
WireConnection;68;1;55;0
WireConnection;106;0;38;0
WireConnection;106;1;105;0
WireConnection;45;2;68;0
WireConnection;45;10;57;0
WireConnection;45;11;106;0
ASEEND*/
//CHKSM=F816708665CCB43CFE85E7F894CE6FE92AC24BF3
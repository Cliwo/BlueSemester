// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MagicCircleGradient"
{
	Properties
	{
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_cast_0 = 0;
			float2 temp_cast_1 = 1;
			float2 temp_cast_2 = -1;
			float2 temp_cast_3 = 1;
			float2 temp_output_5_0 = (temp_cast_2 + (i.uv_texcoord - temp_cast_0) * (temp_cast_3 - temp_cast_2) / (temp_cast_1 - temp_cast_0));
			float2 temp_cast_4 = 0;
			float2 temp_cast_5 = 1;
			float2 temp_cast_6 = -1;
			float2 temp_cast_7 = 1;
			float3 temp_cast_8 = (atan2( (temp_output_5_0).x , (temp_output_5_0).y )).xxx;
			o.Emission = temp_cast_8;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
7;29;1586;824;2208.065;136.4269;1.755153;False;False
Node;AmplifyShaderEditor.IntNode;11;-994.7462,231.8366;Float;False;Constant;_MaxOld;Max Old;0;0;Create;True;0;0;False;0;1;0;0;1;INT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-794.0686,-195.9444;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;12;-992.7462,311.8366;Float;False;Constant;_MinNew;Min New;0;0;Create;True;0;0;False;0;-1;0;0;1;INT;0
Node;AmplifyShaderEditor.IntNode;10;-995.7603,154.2661;Float;False;Constant;_MinOld;Min Old;0;0;Create;True;0;0;False;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.IntNode;13;-993.7462,394.8366;Float;False;Constant;_MaxNew;Max New;0;0;Create;True;0;0;False;0;1;0;0;1;INT;0
Node;AmplifyShaderEditor.TFHCRemapNode;5;-775.4673,147.4276;Float;True;5;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;1,0;False;3;FLOAT2;0,0;False;4;FLOAT2;1,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;6;-767.7399,411.9194;Float;True;True;False;True;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;17;-761.1997,618.4008;Float;True;False;True;True;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;7;-324.8602,136.0662;Float;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ATan2OpNode;15;-416.2371,434.1038;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;MagicCircleGradient;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;1;0
WireConnection;5;1;10;0
WireConnection;5;2;11;0
WireConnection;5;3;12;0
WireConnection;5;4;13;0
WireConnection;6;0;5;0
WireConnection;17;0;5;0
WireConnection;7;0;5;0
WireConnection;15;0;6;0
WireConnection;15;1;17;0
WireConnection;0;2;15;0
ASEEND*/
//CHKSM=6FE760969E45D61F6812C3E7A60172B28D5497B3
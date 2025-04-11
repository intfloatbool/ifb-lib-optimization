#pragma once

#ifdef _WIN32
	#define EXPORT __declspec(dllexport)
#else
// android
	#define EXPORT __attribute__((visibility("default")))
#endif

extern "C" {

	struct SimplifyMeshFastIndicesResult
	{
		unsigned int indicesCount;
		unsigned int* indices;
		bool isAllocated;
	};

	EXPORT SimplifyMeshFastIndicesResult* SimplifyMeshFast(float* vertices, unsigned int verticesCount, unsigned int* indices, unsigned int indicesCount, float multiplier);

	EXPORT void FreeMeshFastIndicesResult(SimplifyMeshFastIndicesResult* result);

}

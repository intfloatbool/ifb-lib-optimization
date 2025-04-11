#include "MeshOptimizerExport.h"
#include "../External/ZeuxMeshOptimizerInterface.h"
#include "../IFBConstants.h"
#include <math.h>


SimplifyMeshFastIndicesResult* SimplifyMeshFast(float* vertices, unsigned int verticesCount, unsigned int* indices, unsigned int indicesCount, float multiplier)
{
	
	IFBLib::LOG("XXX SimplifyMeshFast #1 " + std::string("vert count: ") + std::to_string(verticesCount) + std::string(", ind count: ") + std::to_string(indicesCount));
	auto res = new SimplifyMeshFastIndicesResult();
	res->isAllocated = true;
	
	auto clampMultiplier = IFBLib::Clamp(multiplier, 0.f, 1.f);
	auto simplifyCount = static_cast<int>(std::roundf(indicesCount * clampMultiplier));

	IFBLib::LOG("XXX SimplifyMeshFast #2");
	float errorThreshold = 1e6f;
	auto meshSimplRes = ZeuxMeshOptimizerInterface::SimplifyMeshFast(vertices, verticesCount, indices, indicesCount, clampMultiplier, errorThreshold);

	IFBLib::LOG("XXX SimplifyMeshFast #3");
	res->indicesCount = std::get<0>(meshSimplRes);
	res->indices = std::get<1>(meshSimplRes);

	return res;
}

void FreeMeshFastIndicesResult(SimplifyMeshFastIndicesResult* result)
{
	if (result && result->isAllocated)
	{
		IFBLib::LOG("XXX FreeMeshFastIndicesResult #1");
		delete[] result->indices;
		IFBLib::LOG("XXX FreeMeshFastIndicesResult #2");
		result->isAllocated = false;
		IFBLib::LOG("XXX FreeMeshFastIndicesResult #3");
		delete result;
		IFBLib::LOG("XXX FreeMeshFastIndicesResult #4");

	}
}

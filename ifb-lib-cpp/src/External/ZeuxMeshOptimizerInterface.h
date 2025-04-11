#pragma once
#include "ZeuxMeshOptimizer/meshoptimizer.h"
#include <tuple>
#include <vector>

namespace ZeuxMeshOptimizerInterface 
{
    // returns NEW optimized indices array -> (count, indices)
    std::tuple<size_t, unsigned int*> SimplifyMeshFast(
        float* vertexPositions, // XYZXYZ...
        size_t vertCount,
        unsigned int* indices,
        size_t indicesCount,
        float targetRatio = 0.5f,
        float errorThreshold = 1e-2f);
}
